using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.project
{
    public partial class ProjectAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.grComplete.CausedCallback)
            {
                ListBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        private void ListBind()
        {
            this.AuditTab.BindData();
            var data = this.AuditTab.Projects;

            var keyword = txtKey.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(x =>
                    (x.SerialCode != null && x.SerialCode.Contains(keyword))
                    || (x.BusinessDescription != null && x.BusinessDescription.Contains(keyword))
                    || (x.ProjectCode != null && x.ProjectCode.Contains(keyword))
                    || (x.GroupName != null && x.GroupName.Contains(keyword))
                    || (x.BranchName != null && x.BranchName.Contains(keyword))
                    || (x.BusinessTypeName != null && x.BusinessTypeName.Contains(keyword))
                    || (x.ApplicantEmployeeName != null && x.ApplicantEmployeeName.Contains(keyword))
                ).ToList();
            }

            grComplete.DataSource = data; // ESP.Finance.BusinessLogic.ProjectManager.GetWaitAuditList(GetDelegateUser(), terms, parms);
            grComplete.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            grComplete.ItemDataBound += new ComponentArt.Web.UI.Grid.ItemDataBoundEventHandler(grComplete_ItemDataBound);
            grComplete.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(grComplete_NeedRebind);
            grComplete.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(grComplete_PageIndexChanged);

        }

        void grComplete_NeedRebind(object sender, EventArgs e)
        {
            ListBind();
        }

        void grComplete_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            grComplete.CurrentPageIndex = e.NewIndex;
        }

        void grComplete_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(int.Parse(e.Item["GroupID"].ToString()), depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            e.Item["StatusText"] = State.SetState(int.Parse(e.Item["Status"].ToString()));
            e.Item["GroupID"] = groupname.Substring(0, groupname.Length - 1);
            e.Item["ApplicantEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(int.Parse(e.Item["ApplicantUserID"].ToString())) + "');\">" + e.Item["ApplicantEmployeeName"] + "</a>";
            e.Item["LogId"] = "<a href='ProjectHist.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + e.Item["ProjectID"] + "' target='_blank'><img title='项目号历史' src='/images/history.gif' border='0px;' /></a>";
            e.Item["AuditStatus"] = "<a href='ProjectWorkFlow.aspx?Type=project&FlowID=" + e.Item["ProjectID"] + "' target='_blank'><img src='/images/AuditStatus.gif' border='0px;' title='审批状态'/></a>";
            e.Item["AuditLink"] = "<a href='" + string.Format((int.Parse(e.Item["Status"].ToString()) == 1 || int.Parse(e.Item["Status"].ToString()) == 11 ? Common.ProjectBizUrl : Common.ProjectFinanceUrl), e.Item["ProjectID"]) + "&BackUrl=ProjectAuditList.aspx'><img src='/images/Audit.gif' border='0px' /></a>";
        }

        private string GetDelegateUser()
        {
            string users = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
            {
                users += model.UserID.ToString() + ",";
            }
            users += CurrentUser.SysID;
            return users;
        }
    }
}
