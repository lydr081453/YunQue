using System;
using System.Collections.Generic;

using System.Text;
using System.Web;
using System.Web.Configuration;
using SysConfigurationManager = System.Configuration.ConfigurationManager;
using System.Web.Security;
using System.Security.Principal;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Configuration;

namespace ESP.Security
{

    /// <summary>
    /// Passport认证模块
    /// </summary>
    public sealed class PassportAuthenticationModule : IHttpModule
    {
        // Fields
        private static bool _isConfigChecked;
        private static bool _isAuthRequired;

        /// <summary>
        /// 处置当前实例
        /// </summary>
        public void Dispose()
        {
        }


        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="app">当前应用程序实例</param>
        public void Init(HttpApplication app)
        {
            if (!_isConfigChecked)
            {
                AuthenticationSection authentication = (AuthenticationSection)System.Configuration.ConfigurationManager.GetSection("system.web/authentication");
                _isAuthRequired = true; // authentication.Mode == AuthenticationMode.None;

                _isConfigChecked = true;
            }

            if (_isAuthRequired)
            {
                app.BeginRequest += new EventHandler(this.StoreOriginalUrl);
                app.AuthenticateRequest += new EventHandler(this.OnEnter);
                app.EndRequest += new EventHandler(this.OnLeave);
            }
        }

        private void StoreOriginalUrl(object source, EventArgs eventArgs)
        {

            HttpApplication application = (HttpApplication)source;
            string p1 = application.Context.Request.Url.AbsolutePath.ToLower();
            string p2 = HttpRuntime.AppDomainAppVirtualPath.ToLower();
            if(p1.EndsWith("/") && !p2.EndsWith("/"))
                p1 = p1.Substring(0, p1.Length - 1);

            if(p1 == p2)
            {
                application.Context.RewritePath("~/Default.aspx");
            }


            //WebSiteInfo website = WebSiteManager.Get();
            //string root = application.Context.Request.IsSecureConnection ? website.HttpsRootUrl : website.HttpRootUrl;
            //root = root.ToLower();
            //string url = application.Context.Request.Url.ToString();

            //if (!url.StartsWith(root))
            //{
            //    // remove "http(s)://" and host name from url
            //    int index = url.IndexOf(':');
            //    if (index >= 0)
            //    {
            //        index = url.IndexOf('/', index + 3); // 3 is the length of "://"
            //        if (index >= 0)
            //        {
            //            url = url.Substring(index);
            //        }
            //    }
            //    // get the "http(s)://" and host name from root
            //    index = root.IndexOf(':');
            //    index = root.IndexOf('/', index + 3);
            //    if (index >= 0)
            //    {
            //        root = root.Substring(0, index);
            //    }

            //    // change the host name of url
            //    url = root + url;

            //    // redirect to the new url
            //    application.Context.Response.Redirect(url, false);
            //    application.CompleteRequest();
            //}

        }

        private void RedirectTop(string url)
        {
            WebSiteInfo website = WebSiteManager.Get();
            if (website.FramePagePath != null && website.FramePagePath.Length > 0)
            {
                string html = 
@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <title>正在重定向</title>
</head>
<body>

    <script language=""javascript"" type=""text/javascript"">
        window.onload=function(){
            var p = window;
            while (p.parent != null && p.parent != p)
                p = p.parent;
            var url = '" + ESP.Utilities.JavascriptUtility.QuoteJScriptString(url, false, false) + @"';
            p.location.href = url;
        }
    </script>

</body>
</html>";
                HttpContext.Current.Response.Write(html);
            }
            else
            {
                HttpContext.Current.Response.Redirect(url, false);
            }

            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private void OnEnter(object source, EventArgs eventArgs)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            WebSiteInfo website = WebSiteManager.Get();
            SettingsInfo settings = SettingManager.Get();

            if (IsLoggingIn(context, website, settings))
                context.SkipAuthorization = true;


            if (context.Request.QueryString[PassportAuthentication.SignOutQueryName] != null)
            {
                PassportAuthentication.RemoveAuthCookie();

                string currentPath = context.Request.RawUrl;
                currentPath = ESP.Utilities.UrlUtility.GetPathAndQuery(currentPath);
                currentPath = ESP.Utilities.UrlUtility.RemoveQuery(currentPath, PassportAuthentication.SignOutQueryName);
                currentPath = ESP.Utilities.UrlUtility.GetRelativePath(currentPath, HttpRuntime.AppDomainAppVirtualPath, false);

                if(website.FramePagePath != null && website.FramePagePath.Length > 0)
                {
                    currentPath = ESP.Utilities.UrlUtility.AddQuery(website.FramePagePath, "contentUrl", HttpUtility.UrlEncode(currentPath, context.Request.ContentEncoding));
                }

                string logoutUrl = PassportAuthentication.GetLogoutPage();
                logoutUrl = ESP.Utilities.UrlUtility.AddQuery(logoutUrl, "appid", WebSiteManager.Get().WebSiteID.ToString());
                logoutUrl = ESP.Utilities.UrlUtility.AddQuery(logoutUrl, "reqpath", HttpUtility.UrlEncode(currentPath, context.Request.ContentEncoding));

                RedirectTop(logoutUrl);

                return;
            }

            PassportAuthenticationTicket tiket;

            string token = context.Request.QueryString[PassportAuthentication.AuthTokenName];
            tiket = PassportAuthentication.DecryptTicket(token, settings.AESKey);
            if (tiket != null && tiket.UserID > 0 && tiket.IsExpired(PassportAuthentication.AuthTokenExpiredInSeconds) != true)
            {
                PassportAuthentication.SetAuthCookie(tiket);
                context.User = new GenericPrincipal(new GenericIdentity(tiket.UserID.ToString(), "SEPPassport"), new string[0]);
                string url = ESP.Utilities.UrlUtility.RemoveQuery(context.Request.Url.PathAndQuery, PassportAuthentication.AuthTokenName);
                context.Response.Redirect(url, false);
                context.ApplicationInstance.CompleteRequest();
                return;
            }

            HttpCookie cookie2 = context.Request.Cookies[PassportAuthentication.AuthCookieName];
            string cookie2Value = cookie2 == null ? null : cookie2.Value;
            cookie2Value = cookie2Value == null ? cookie2Value : HttpUtility.UrlDecode(cookie2Value);
            tiket = PassportAuthentication.DecryptTicket(cookie2Value, settings.AESKey);
            if (tiket != null && tiket.UserID > 0 && tiket.IsExpired(PassportAuthentication.AuthCookieExpiredInSeconds) != true)
            {
                PassportAuthentication.UpdateAuthCookie(tiket);
                context.User = new GenericPrincipal(new GenericIdentity(tiket.UserID.ToString(), "SEPPassport"), new string[0]);
            }
        }

        private bool IsLoggingIn(HttpContext context, WebSiteInfo website, SettingsInfo settings)
        {
            if(settings == null)
                return false;

            if (website.WebSiteID != settings.PassportWebSite)
                return false;

            //string rawUrl = context.Request.RawUrl;
            int index = website.UrlPrefix.IndexOf('/');
            string path = index < 0 ? "/" : website.UrlPrefix.Substring(index).ToLower();
            string loginUrl = ESP.Utilities.UrlUtility.ConcatUrl(path, "login.aspx");
            string logoutUrl = ESP.Utilities.UrlUtility.ConcatUrl(path, "logout.aspx");

            string filePath = context.Request.FilePath.ToLower();
            return (filePath == loginUrl || filePath == logoutUrl);
        }

        private void OnLeave(object source, EventArgs eventArgs)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            if (context.Response.StatusCode == 401)
            {
                string url = PassportAuthentication.GetLoginUrl();
                RedirectTop(url);
            }
        }
    }
}