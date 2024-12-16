using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.ServiceCenter
{
    public partial class HRQA : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           
            string id = Request["QId"];
            System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)tabContent.FindControl("tr" + id);
            tr.Style["display"] = "block";
        }
    }
}
