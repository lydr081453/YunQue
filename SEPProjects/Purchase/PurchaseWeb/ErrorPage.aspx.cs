using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Exception objExp = HttpContext.Current.Server.GetLastError();

        if (objExp != null)
        {
            litMsg.Text += "<br />Offending URL: " + HttpContext.Current.Request.Url.ToString();
            litMsg.Text += "<br />Source: " + objExp.Source;
            litMsg.Text += "<br />Message: " + objExp.Message;
            litMsg.Text += "<br />Stack trace: " + objExp.StackTrace;
        }
    }
}
