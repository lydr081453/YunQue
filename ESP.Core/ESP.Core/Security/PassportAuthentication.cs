using System;
using System.Collections.Generic;
using System.Text;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using System.Security.Cryptography;
using System.Web;
using ESP.Configuration;
using SysConfigurationManager = System.Configuration.ConfigurationManager;
using System.Web.Configuration;

namespace ESP.Security
{
    /// <summary>
    /// Passport登录控制类
    /// </summary>
    public static class PassportAuthentication
    {
        /// <summary>
        /// 获取用于登录按钮的Url
        /// </summary>
        /// <returns></returns>
        public static string GetLoginUrl()
        {
            WebSiteInfo website = WebSiteManager.Get();

            string currentPath = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                currentPath = context.Request.RawUrl;
                //currentPath = ESP.Utilities.UrlUtility.GetPathAndQuery(currentPath);
                currentPath = ESP.Utilities.UrlUtility.GetRelativePath(currentPath, HttpRuntime.AppDomainAppVirtualPath, false);
            }
            if (currentPath == null)
                currentPath = string.Empty;

            if (!string.IsNullOrEmpty(website.FramePagePath))
            {
                currentPath = ESP.Utilities.UrlUtility.AddQuery(website.FramePagePath, "contentUrl", HttpUtility.UrlEncode(currentPath, context.Request.ContentEncoding));
            }
            return GetLoginUrl(website.WebSiteID, currentPath);
        }

        /// <summary>
        /// 获取登录页面
        /// </summary>
        /// <returns>登录页面</returns>
        public static string GetLoginUrl(int currentWebSiteID, string returnUrl)
        {
            if (returnUrl == null)
                returnUrl = string.Empty;

            SettingsInfo settings = SettingManager.Get();

            if (settings == null || settings.PassportWebSite <= 0)
                return null;

            WebSiteInfo webSite = WebSiteManager.Get(settings.PassportWebSite);
            if (webSite == null)
                return null;

            string loginUrl = ESP.Utilities.UrlUtility.ConcatUrl(webSite.HttpRootUrl, "login.aspx");

            HttpContext context = HttpContext.Current;
            System.Text.Encoding encoding = context == null ? System.Text.Encoding.UTF8 : context.Request.ContentEncoding;
            bool isAuthenticated = context == null ? false : context.Request.IsAuthenticated;

            loginUrl = ESP.Utilities.UrlUtility.AddQuery(loginUrl, "appid", currentWebSiteID.ToString());
            loginUrl = ESP.Utilities.UrlUtility.AddQuery(loginUrl, "reqpath", HttpUtility.UrlEncode(returnUrl, encoding));

            if (isAuthenticated)
                loginUrl = ESP.Utilities.UrlUtility.AddQuery(loginUrl, "noauto", "1");

            return loginUrl;
        }



        /// <summary>
        /// 获取用于退出登录按钮的Url
        /// </summary>
        /// <returns>退出登录的Url</returns>
        public static string GetLogoutUrl()
        {
            string currentPath = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                currentPath = context.Request.RawUrl;
                //currentPath = ESP.Utilities.UrlUtility.GetPathAndQuery(currentPath);
                currentPath = ESP.Utilities.UrlUtility.GetRelativePath(currentPath, HttpRuntime.AppDomainAppVirtualPath, false);
            }
            if (currentPath == null)
                currentPath = string.Empty;
            return GetLogoutUrl(currentPath);
        }

        /// <summary>
        /// 获取用于退出登录按钮的Url
        /// </summary>
        /// <param name="returnUrl">退出登录后返回的Url</param>
        /// <returns>退出登录的Url</returns>
        public static string GetLogoutUrl(string returnUrl)
        {
            return ESP.Utilities.UrlUtility.AddQuery(returnUrl, SignOutQueryName, "");
        }

        ///// <summary>
        ///// 获取用于登录按钮的Url
        ///// </summary>
        ///// <returns></returns>
        //public static string GetLoginUrl()
        //{
        //    string loginUrl = GetLoginUrlPath();
        //    string contextItem = (string)HttpContext.Current.Items[PassportAuthentication.SEP_ORIGINALURL_CONTEXT_ITEM_KEY];
        //    string currentPath = contextItem == null ? HttpContext.Current.Request.Url.PathAndQuery : contextItem; 
        //    loginUrl = ESP.Utilities.UrlUtility.AddQuery(loginUrl, "appid", WebSiteManager.Get().WebSiteID.ToString());
        //    loginUrl = ESP.Utilities.UrlUtility.AddQuery(loginUrl, "reqpath", HttpUtility.UrlEncode(currentPath, HttpContext.Current.Request.ContentEncoding));
        //    loginUrl = ESP.Utilities.UrlUtility.AddQuery(loginUrl, "noauto", "1");
        //    return loginUrl;
        //}

        /// <summary>
        /// 获取退出页面
        /// </summary>
        /// <returns>退出页面</returns>
        internal static string GetLogoutPage()
        {
            SettingsInfo settings = SettingManager.Get();

            if (settings == null || settings.PassportWebSite <= 0)
                return null;

            WebSiteInfo webSite = WebSiteManager.Get(settings.PassportWebSite);
            if (webSite == null)
                return null;

            return ESP.Utilities.UrlUtility.ConcatUrl(webSite.HttpRootUrl, "logout.aspx");
        }

        /// <summary>
        /// 加密票据
        /// </summary>
        /// <param name="ticket">要加密的票据</param>
        /// <param name="key">密钥</param>
        /// <returns>加密结果</returns>
        public static string EncryptTicket(PassportAuthenticationTicket ticket, string key)
        {
            if (ticket == null)
                return null;

            try
            {
                byte[] buf = ticket.ToBytes();

                byte[] encBuf = ESP.Utilities.CryptoUtility.EncryptData(buf, key);

                return Convert.ToBase64String(encBuf) + "," + ticket.UserName;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 解密票据
        /// </summary>
        /// <param name="token">加密后的票据</param>
        /// <param name="key">密钥</param>
        /// <returns>解密出的票据</returns>
        public static PassportAuthenticationTicket DecryptTicket(string token, string key)
        {
            if (token == null || token.Length == 0)
                return null;

            try
            {
                int index = token.IndexOf(',');
                if(index >= 0)
                {
                    token = token.Substring(0, index);
                }

                byte[] encBuf = Convert.FromBase64String(token);
                byte[] buf = ESP.Utilities.CryptoUtility.DecryptData(encBuf, key);
                PassportAuthenticationTicket ticket = PassportAuthenticationTicket.FromBytes(buf);

                return ticket;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 令牌过期时间（秒）
        /// </summary>
        public static int AuthTokenExpiredInSeconds
        {
            get
            {
                return 30;
            }
        }

        /// <summary>
        /// 票据过期时间（秒，但精确到分钟）
        /// </summary>
        public static int AuthCookieExpiredInSeconds
        {
            get
            {
                return 20 * 60;
            }
        }

        /// <summary>
        /// 登录的Url查询参数名称
        /// </summary>
        public static string AuthTokenName
        {
            get
            {
                return "_ppauth";
            }
        }

        /// <summary>
        /// 退出登录的Url查询参数名称
        /// </summary>
        public static string SignOutQueryName
        {
            get
            {
                return "_logout";
            }
        }

        /// <summary>
        /// 认证Cookie的名字
        /// </summary>
        public static string AuthCookieName
        {
            get
            {
                return "ppauth";
            }
        }

        /// <summary>
        /// 设置认证票据
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="username">用户名</param>
        public static void SetAuthCookie(int userId, string username)
        {
            PassportAuthenticationTicket ticket = new PassportAuthenticationTicket();
            ticket.UserID = userId;
            ticket.UserName = username;
            //ticket.Expired = DateTime.UtcNow.AddSeconds(PassportAuthentication.AuthCookieExpiredInSeconds);
            SetAuthCookie(ticket);
        }

        /// <summary>
        /// 设置认证Cookie
        /// </summary>
        /// <param name="tiket">认证票据</param>
        public static void SetAuthCookie(PassportAuthenticationTicket tiket)
        {
            SettingsInfo settings = SettingManager.Get();

            tiket.Expired = DateTime.UtcNow.AddSeconds(PassportAuthentication.AuthCookieExpiredInSeconds);
            string cookieValue = HttpUtility.UrlEncode(PassportAuthentication.EncryptTicket(tiket, settings.AESKey));

            HttpCookie cookie = new HttpCookie(PassportAuthentication.AuthCookieName, cookieValue);
            cookie.Path = "/";
            cookie.Domain = settings.TopDomain;
            HttpContext.Current.Response.Cookies.Remove(AuthCookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 如有必要则更新认证Cookie
        /// </summary>
        /// <param name="tiket">认证票据</param>
        public static void UpdateAuthCookie(PassportAuthenticationTicket tiket)
        {
            if ((tiket.Expired - DateTime.UtcNow).TotalSeconds < PassportAuthentication.AuthCookieExpiredInSeconds / 2)
            {
                PassportAuthentication.SetAuthCookie(tiket);
            }
        }

        /// <summary>
        /// 移除认证Cookie
        /// </summary>
        public static void RemoveAuthCookie()
        {
            SettingsInfo settings = SettingManager.Get();
            //tiket.Expired = DateTime.Now.AddSeconds(PassportAuthentication.TicketExpiredInSeconds);
            //string cookieValue = PassportAuthentication.EncryptTicket(tiket, settings.EncryptionKey);

            HttpContext current = HttpContext.Current;
            string value = current.Request.Browser["supportsEmptyStringInCookieValue"] == "false" ? "NoCookie" : string.Empty;
            //if (current.Request.Browser["supportsEmptyStringInCookieValue"] == "false")
            //{
            //    str = "NoCookie";
            //}
            HttpCookie cookie = new HttpCookie(AuthCookieName, value);
            cookie.HttpOnly = true;
            cookie.Expires = new DateTime(1999, 10, 12);
            cookie.Path = "/";
            cookie.Domain = settings.TopDomain;
            current.Response.Cookies.Remove(AuthCookieName);
            current.Response.Cookies.Add(cookie);
        }

        //internal const string SEP_ORIGINALURL_CONTEXT_ITEM_KEY = "SEP_ORIGINALURL";
    }
}
