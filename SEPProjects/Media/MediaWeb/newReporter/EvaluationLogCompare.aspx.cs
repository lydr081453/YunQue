using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.BusinessLogic;
using System.Data;

namespace MediaWeb.newReporter
{
    public partial class EvaluationLogCompare : ESP.Web.UI.PageBase
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
            string ids = Request["ids"];
            DataSet ds = ReporterEvaluationManager.GetReporterEvaluation(ids);
            dgList.DataSource = ds;
            dgList.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EvaluationLogList.aspx?Rid=" + Request[RequestName.ReporterID]);
        }
    }
}
