using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.WebSite
{
    public partial class AllJob : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindingDate();
            }
        }

        private void bindingDate()
        {
            List<ESP.HumanResource.Entity.JobInfo> list = new ESP.HumanResource.BusinessLogic.JobManager().GetList(null);
            dlJob.DataSource = list;
            dlJob.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            foreach (DataListItem item in dlJob.Items)
            {
                LinkButton link = item.FindControl("LinkButton1") as LinkButton;
                if (link != null && link == sender)
                {
                    Label lbl = item.FindControl("lblJobId") as Label;
                    if (lbl != null)
                    {
                        Response.Redirect("JobDetail.aspx?id=" + int.Parse(lbl.Text));
                    }
                }
            }
        }
    }
}
