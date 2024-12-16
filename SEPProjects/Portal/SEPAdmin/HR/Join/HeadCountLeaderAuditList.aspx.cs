using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Join
{
    public partial class HeadCountLeaderAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeadAccountEdit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        { }

        private void BindData()
        {
            var deptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" DirectorId=" + UserID.ToString());
            string depts = string.Empty;
            foreach (var dep in deptlist)
            {
                depts += dep.DepId.ToString() + ",";
            }

            depts = depts.TrimEnd(',');
            string strwhere = string.Empty;
            int financeDirectorId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AdvanceID"]);
            if (UserID == financeDirectorId)
            {
                strwhere = string.Format("status={0} or (status={1} and interviewVPId={2}) or status={3}", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.VPApproved, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView, UserID, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit);
            }
            else
            {
                if (!string.IsNullOrEmpty(depts))
                {
                    strwhere = string.Format(" (status={0} and groupid in({1}) or (status={2} and interviewVPId={3}))", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit, depts, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView, UserID);

                }
                else
                {
                    return;
                }

            }

            var headaccountList = new ESP.HumanResource.BusinessLogic.HeadAccountManager().GetList(" and  (" + strwhere + ")");

            this.gvHeadAccount.DataSource = headaccountList;
            this.gvHeadAccount.DataBind();
        }

        protected void gvHeadAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HeadAccountInfo model = (HeadAccountInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.DepartmentViewInfo deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.GroupId);
                PositionLevelsInfo pb = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(model.LevelId);

                Label lblGroup = (Label)e.Row.FindControl("lblGroup");

                lblGroup.Text = deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3;
                Label lblSalary = (Label)e.Row.FindControl("lblSalary");
                lblSalary.Text = pb.SalaryLow.ToString("#,##0.00") + " - " + pb.SalaryHigh.ToString("#,##0.00");
            }
        }
    }
}
