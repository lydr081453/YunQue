using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.BusinessLogic;

namespace MediaWeb.Media
{
    public partial class ReporterEvaluationLog : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ESP.Media.Entity.ReporterEvaluation model = ReporterEvaluationManager.Get(int.Parse(Request["id"]));
                litUser.Text = model.UserName;
                litDate.Text = model.CreateDate.ToString("yyyy-MM-dd hh:ss");
                labEvaluation.Text = model.Evaluation;
                labReason.Text = model.Reason;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporterEvaluationList.aspx?Rid=" + Request[RequestName.ReporterID]);
        }
    }
}
