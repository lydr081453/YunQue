using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.BusinessLogic;

namespace MediaWeb.newReporter
{
    public partial class EvaluationLogList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            gvList.DataSource = ReporterEvaluationManager.GetReporterEvaluation(int.Parse(Request[RequestName.ReporterID]), "");
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnCompare_Click(object sender, EventArgs e)
        {
            Response.Redirect("EvaluationLogCompare.aspx?ids="+Request["chk"]+"&Rid="+Request[RequestName.ReporterID]);
        }
    }
}
