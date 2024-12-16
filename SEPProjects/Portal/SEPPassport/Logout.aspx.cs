using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Configuration;

namespace PassportWeb
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (Request.Browser["supportsEmptyStringInCookieValue"] == "false")
            {
                str = "NoCookie";
            }
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str);
            cookie.HttpOnly = true;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Expires = new DateTime(0x7cf, 10, 12);
            cookie.Secure = FormsAuthentication.RequireSSL;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            Response.Cookies.Add(cookie);

            string returnUrl = GetReturnUrl();
            if (returnUrl != null)
                Response.Redirect(returnUrl);
        }

        private string GetReturnUrl()
        {
            int appid;
            string reqpath;

            if (!int.TryParse(Request["appid"], out appid))
                return null;

            if (appid <= 0 || appid == ConfigurationManager.WebSiteID)
                return null;


            WebSiteInfo webSite = WebSiteManager.Get(appid);
            if (webSite == null)
                return null;

            string urlPrefix = webSite.UrlPrefix;
            reqpath = Request["reqpath"];
            reqpath = "/Default.aspx";

            string url = ESP.Utilities.UrlUtility.ConcatUrl("http://" + urlPrefix, reqpath);

            return url;
        }
    }
}
