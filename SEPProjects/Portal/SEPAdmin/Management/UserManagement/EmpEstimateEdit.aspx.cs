using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Management.UserManagement
{
    public partial class EmpEstimateEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["estimateid"]))
                {
                    int eduid = int.Parse(Request["estimateid"]);
                    ESP.HumanResource.Entity.EmpEstimateInfo model = ESP.HumanResource.BusinessLogic.EmpEstimateManager.GetModel(eduid);
                    txtEstimateDesc.Text = model.Remark;
                    this.ddlStatus.SelectedValue = model.Result;
                    this.ddlEstimate.SelectedValue = model.EstimateType;
                    this.txtEstimateDate.Text = model.EstimateDate.ToString("yyyy-MM-dd");

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request["estimateid"]))
            {
                int estimateid = int.Parse(Request["estimateid"]);
                ESP.HumanResource.Entity.EmpEstimateInfo model = ESP.HumanResource.BusinessLogic.EmpEstimateManager.GetModel(estimateid);
                ESP.HumanResource.BusinessLogic.EmpEstimateManager.Delete(estimateid);
                //string str = string.Format("art.dialog.close();art.dialog.opener.location='/Management/UserManagement/EmpMgt.aspx?userid=" + model.UserId + "&tabindex=1'");
                //ClientScript.RegisterStartupScript(typeof(string), "", str, true);

                string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

                ClientScript.RegisterStartupScript(typeof(string), "", str, true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = 0;
            if (String.IsNullOrEmpty(Request["estimateid"]))
            {
                userid = int.Parse(Request["userid"]);

                ESP.HumanResource.Entity.EmpEstimateInfo model = new ESP.HumanResource.Entity.EmpEstimateInfo();
                model.UserId = userid;
                model.EstimateType = this.ddlEstimate.SelectedItem.Text;
                if (string.IsNullOrEmpty(this.txtEstimateDate.ToString()))
                {
                    model.EstimateDate = DateTime.Now;
                }
                else
                    model.EstimateDate = Convert.ToDateTime(txtEstimateDate.Text);
                model.Result = this.ddlStatus.SelectedItem.Text;
                model.Remark = this.txtEstimateDesc.Text;
                model.CreatorId = int.Parse(CurrentUser.SysID);
                model.Creator = CurrentUser.Name;
                model.Status = 1;
                ESP.HumanResource.BusinessLogic.EmpEstimateManager.Add(model);
            }
            else
            {
                int eduid = int.Parse(Request["estimateid"]);
                ESP.HumanResource.Entity.EmpEstimateInfo model = ESP.HumanResource.BusinessLogic.EmpEstimateManager.GetModel(eduid);
                model.EstimateType = this.ddlEstimate.SelectedItem.Text;
                if (string.IsNullOrEmpty(this.txtEstimateDate.ToString()))
                {
                    model.EstimateDate = DateTime.Now;
                }
                else
                    model.EstimateDate = Convert.ToDateTime(txtEstimateDate.Text);
                model.Result = this.ddlStatus.SelectedItem.Text;
                model.Remark = this.txtEstimateDesc.Text;
                model.CreatorId = int.Parse(CurrentUser.SysID);
                model.Creator = CurrentUser.Name;
                model.Status = 1;
                ESP.HumanResource.BusinessLogic.EmpEstimateManager.Update(model);
                userid = model.UserId;
            }
            string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

            ClientScript.RegisterStartupScript(typeof(string), "", str, true);
        }
    }
}
