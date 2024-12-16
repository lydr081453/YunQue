using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Reports
{
    public partial class ProjectClosedCostList :ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
                BindLog();
            }
        }

        private void BindLog()
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtEndDate.Text = "";
            this.txtBeginDate.Text = "";
            Search();
        }
        private void Search()
        {
 
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
          
        }
        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataRowView dr = (System.Data.DataRowView)e.Row.DataItem;
 
            }
        }
        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }
    }
}
