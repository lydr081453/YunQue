using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.ServiceCenter
{
    public partial class Welfare : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request["QId"];
            System.Web.UI.HtmlControls.HtmlTableCell td = (System.Web.UI.HtmlControls.HtmlTableCell)tabContent.FindControl("td" + id);
            td.Style["display"] = "block";
        }
    }
}
