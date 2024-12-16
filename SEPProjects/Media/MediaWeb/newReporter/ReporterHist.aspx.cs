using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MediaWeb.newReporter
{
    public partial class ReporterHist : System.Web.UI.Page
    {
        int reporterId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.ReporterID]))
                reporterId = int.Parse(Request[RequestName.ReporterID]);

            if (!IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            gvList.DataSource =  ESP.Media.BusinessLogic.ReportersManager.GetHistFullInfoByClientID(reporterId);
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void gvList_DataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEditor = (Label)e.Row.FindControl("lblEditor");
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(dr["createdbyuserid"].ToString()));
                lblEditor.Text = emp.Name;
            }
        }


        protected void btnCompare_Click(object sender, EventArgs e)
        {
           // Response.Redirect("EvaluationLogCompare.aspx?ids=" + Request["chk"] + "&Rid=" + Request[RequestName.ReporterID]);
        }
    }
}
