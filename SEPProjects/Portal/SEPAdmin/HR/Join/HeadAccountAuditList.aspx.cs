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
    public partial class HeadAccountAuditList : ESP.Web.UI.PageBase
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
            string userids = UserID.ToString() + ",";
            var delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(UserID);
            if (delegateList != null && delegateList.Count > 0)
            {
                foreach (var de in delegateList)
                {
                    userids += de.UserID.ToString() + ",";
                }
            }

            userids = userids.TrimEnd(',');

            var deptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" (HeadCountAuditorId in(" + userids + "))");
            string depts = string.Empty;
            foreach (var dep in deptlist)
            {
                depts += dep.DepId.ToString() + ",";
            }

            depts = depts.TrimEnd(',');


            var vpdeptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" (HeadCountDirectorId in(" + userids + "))");
            string vpdepts = string.Empty;
            foreach (var dep in vpdeptlist)
            {
                vpdepts += dep.DepId.ToString() + ",";
            }

            vpdepts = vpdepts.TrimEnd(',');

            var rcdeptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" (HCFinalAuditor in(" + userids + "))");
            string rcdepts = string.Empty;
            foreach (var dep in rcdeptlist)
            {
                rcdepts += dep.DepId.ToString() + ",";
            }

            rcdepts = rcdepts.TrimEnd(',');

             string strwhere = string.Empty;

            if (!string.IsNullOrEmpty(vpdepts) || !string.IsNullOrEmpty(depts) )
            {
               
                if (!string.IsNullOrEmpty(depts))
                {
                    if (!string.IsNullOrEmpty(vpdepts))
                        strwhere = string.Format(" ((status={0} and groupid in({1})) or (status={2} and groupid in({3})) or (status={4} and interviewVPId in({5})) )", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit, vpdepts, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitPreVPAudit, depts, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView, userids);
                    else
                        strwhere = string.Format(" ((status={0} and groupid in({1})) or (status={2} and interviewVPId in({3})))", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitPreVPAudit, depts, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView, userids);
                }
                else
                {
                    strwhere = string.Format("((status={0} and interviewVPId={1}) or (status={2} and groupid in({3})))", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView, UserID, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit, vpdepts);
                }

                strwhere += string.Format(" or (status={0} and RCUserId={1})", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit, UserID);
            }
            else if (!string.IsNullOrEmpty(rcdepts))
                strwhere = string.Format(" (status={0} and RCUserId={1})", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit, UserID);

            if (!string.IsNullOrEmpty(strwhere))
            {
                var headaccountList = new ESP.HumanResource.BusinessLogic.HeadAccountManager().GetList(" and  (" + strwhere + ")");

                this.gvHeadAccount.DataSource = headaccountList;
                this.gvHeadAccount.DataBind();
            }

        }

        protected void gvHeadAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HeadAccountInfo model = (HeadAccountInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.DepartmentViewInfo deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.GroupId);
                PositionLevelsInfo pb = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(model.LevelId);
                
                Label lblGroup = (Label)e.Row.FindControl("lblGroup");
                Label lblId = (Label)e.Row.FindControl("lblId");
              
                string strId = model.Id.ToString();

                while (strId.Length < 5)
                {
                    strId = "0" + strId;
                }
                lblId.Text = "HC" + strId;

                lblGroup.Text = deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3;
                Label lblSalary = (Label)e.Row.FindControl("lblSalary");
                lblSalary.Text = pb.SalaryLow.ToString("#,##0.00") + " - " + pb.SalaryHigh.ToString("#,##0.00");
            }
        }
    }
}
