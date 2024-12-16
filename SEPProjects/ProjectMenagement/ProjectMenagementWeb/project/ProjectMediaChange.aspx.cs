using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;

namespace FinanceWeb.project
{
    public partial class ProjectMediaChange : ESP.Finance.WebPage.EditPageForProject
    {
        private int projectid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
                    this.PrepareDisplay.InitProjectInfo(projectinfo);
                }
                Bind_List();
            }
        }

        protected void Bind_List()
        {
            string strWhere = " projectId = @projectId";
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            parms.Add(new System.Data.SqlClient.SqlParameter("@projectId", projectid));
            gvList.DataSource = ESP.Finance.BusinessLogic.ProjectMediaManager.GetList(strWhere, parms).OrderBy(x => x.Id).ToList();
            gvList.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ProjectMediaInfo oldPM = ProjectMediaManager.GetModel(int.Parse(hidPmId.Value));
            oldPM.Recharge = decimal.Parse(txtOldRecharge.Text);
            oldPM.EndDate = DateTime.Parse(txtBeginDate.Text).AddDays(-1);

            ProjectMediaInfo newPM = new ProjectMediaInfo();
            newPM.BeginDate = DateTime.Parse(txtBeginDate.Text);
            newPM.SupplierId = int.Parse(hidMediaId.Value);
            newPM.ProjectId = int.Parse(hidProjectId.Value);
            newPM.CostRate = decimal.Parse(txtSupplierCostRate.Text);
            newPM.Recharge = decimal.Parse(txtRecharge.Text);

            string result = "成功";
            try{
                ProjectMediaManager.UpdateAndAdd(oldPM, newPM);
            }
            catch (Exception ex) { result = ex.Message; }
            btnSubmit.Visible = false;
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='ProjectMediaChange.aspx?ProjectID=" + hidProjectId.Value + "';alert(\"" + result + "\");",true); 
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                int Id = int.Parse(e.CommandArgument.ToString());
                ProjectMediaInfo pmInfo = ESP.Finance.BusinessLogic.ProjectMediaManager.GetModel(Id);
                labOldMediaName.Text = pmInfo.MediaName;
                labOldSupplierCostRate.Text = pmInfo.CostRate.ToString("0.00");
                hidTotalRecharge.Value = pmInfo.Recharge.ToString();
                txtOldRecharge.Text = pmInfo.Recharge.ToString("0.00");
                hidProjectId.Value = pmInfo.ProjectId.ToString();
                hidPmId.Value = pmInfo.Id.ToString();
                btnSubmit.Visible = true;
            }else if(e.CommandName == "Del"){

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectMediaChangeList.aspx");
        }
    }
}