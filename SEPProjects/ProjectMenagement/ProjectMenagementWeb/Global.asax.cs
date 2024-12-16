using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SEPAdmin
{
    public class Global : System.Web.HttpApplication
    {
        private void Log(int id, string s)
        {
            using (System.IO.StreamWriter log = new System.IO.StreamWriter(HttpRuntime.AppDomainAppPath + "\\timelog.txt", true))
            {
                log.WriteLine("{0:00} {1:hh:mm:ss:ffffff} {2}", id, DateTime.Now, s);
                log.Flush();
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            ESP.Configuration.ConfigurationManager.Create();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var q = this.Request["profile"];
            if (q == "true")
                Log(0, "Begin request");

            if (this.Request.Url.AbsolutePath.EndsWith("esp_get_userinfo_html.aspx"))
            {
                var s = this.Request.QueryString["uid"];
                int uid;
                if (int.TryParse(s, out uid) && uid > 0)
                {
                    s = ESP.Web.UI.PageBase.GetUserInfo(uid);
                    Response.ContentType = "text/plain";
                    Response.Write(s);
                }

                this.CompleteRequest();
            }
        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var q = this.Request["profile"];
            if (q == "true")
                Log(0, "End request");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            ESP.Configuration.ConfigurationManager.Dispose();
        }
    }
}