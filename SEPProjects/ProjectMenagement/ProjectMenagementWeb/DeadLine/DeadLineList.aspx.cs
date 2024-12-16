using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.DeadLine
{
    public partial class DeadLineList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                Search();
        }
        protected void lbNewDeadLine_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeadLineEdit.aspx");
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }

        private void Search()
        {
            IList<ESP.Finance.Entity.DeadLineInfo> deadlineList = ESP.Finance.BusinessLogic.DeadLineManager.GetAllList();
            this.gvG.DataSource = deadlineList;
            this.gvG.DataBind();
        }
        protected void lbNewProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeardLineEdit.aspx");
        }

          protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
          {
              if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
              {
                  e.Row.Cells[0].Visible = false;
              }
          }
          protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
          { 
          
          }
    }
}
