using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ESP.Security;

namespace Portal.Common.Compatible
{
    class FrameworkAuthenticationModule : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(OnEnter);
        }

        void OnEnter(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            string encToken = app.Request.QueryString["_lsftoken"];

            string userCode;
            string ip;
            if (FrameworkAuthentication.ParseToken(encToken, out userCode, out ip) && ip == app.Request.UserHostAddress)
            {
                ESP.Framework.Entity.UserInfo user = ESP.Framework.BusinessLogic.UserManager.Get(userCode);
                if (user != null)
                {
                    PassportAuthenticationTicket ticket = new PassportAuthenticationTicket();
                    ticket.UserID = user.UserID;
                    ticket.Expired = DateTime.Now.AddSeconds(PassportAuthentication.AuthCookieExpiredInSeconds);
                    ESP.Security.PassportAuthentication.SetAuthCookie(ticket);

                    string url = ESP.Utilities.UrlUtility.RemoveQuery(app.Request.Url.PathAndQuery, "_lsftoken");
                    app.Response.Redirect(url);
                    app.CompleteRequest();

                }
            }
        }

        #endregion
    }
}
