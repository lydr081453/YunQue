using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.IT
{
    public partial class EmailClosing : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            string userid = Request["userId"];
            if (!string.IsNullOrEmpty(userid))
            {
                ESP.HumanResource.Entity.EmailClosingInfo model = ESP.HumanResource.BusinessLogic.EmailClosingManager.GetModel(int.Parse(userid));
                ESP.HumanResource.Entity.DimissionFormInfo dimission = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(int.Parse(userid));
                this.lblDept.Text = model.DeptName;
                this.lblEmail.Text = model.Email;
                this.lblKeepDate.Text = model.KeepDate.ToString("yyyy-MM-dd");
                this.lblUserName.Text = model.NameCN;
                this.lblLastDay.Text = dimission.LastDay.Value.ToString("yyyy-MM-dd");

            }
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            string userid = Request["userId"];
            if (!string.IsNullOrEmpty(userid))
            {
                ESP.HumanResource.Entity.EmailClosingInfo model = ESP.HumanResource.BusinessLogic.EmailClosingManager.GetModel(int.Parse(userid));

                model.Status = 1;
                model.OperatorId = CurrentUserID;
                model.CloseDate = DateTime.Now;

                ESP.HumanResource.BusinessLogic.EmailClosingManager.Update(model);

                ShowCompleteMessage("离职邮箱状态已经更新！", "EmailClosingList.aspx");

            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmailClosingList.aspx");
        }
    }
}