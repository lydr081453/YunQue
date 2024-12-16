using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ApplyForInvioce
{
    public partial class ApplyForInvioceAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.grComplete.CausedCallback)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            this.AuditTab.BindData();
            var data = this.AuditTab.ApplyForInvioces;

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

            //var list = ESP.Finance.BusinessLogic.ProjectManager.GetApplyForInvioceAuditingProjectList();
            grComplete.DataSource = data;
            grComplete.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
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
            e.Item["GroupID"] = groupname.Substring(0, groupname.Length - 1);
            e.Item["AuditLink"] = "<a href='ApplyForInvioceAudit.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + e.Item["ProjectID"] + "&&BackUrl=ApplyForInvioceAuditList.aspx'><img src='/images/Audit.gif' border='0px' /></a>";
           
        }
    }
}