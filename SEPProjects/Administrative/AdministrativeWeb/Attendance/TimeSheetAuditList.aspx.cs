using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Administrative.BusinessLogic;

namespace AdministrativeWeb.Attendance
{
    public partial class TimeSheetAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        private void BindInfo()
        {

            ListBind();
        }

        private void ListBind()
        {
            DataTable dt = TimeSheetCommitManager.GetWaitAuditTimeSheetList(int.Parse(CurrentUser.SysID), txtKey.Text.Trim());
            gvList.DataSource = dt;
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hidUserId = (HiddenField)e.Row.FindControl("hidUserId");
                HyperLink hlyLink = (HyperLink)e.Row.FindControl("hlyLink");
                hlyLink.NavigateUrl = "TimeSheetAudit.aspx?userid=" + hidUserId.Value;
            }
        }
    }
}
