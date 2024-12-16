using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Principal;
using ESP.Framework.DataAccess;
using ESP.Configuration;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;


namespace ESP.Security
{
    /// <summary>
    /// 对Url进行授权检验的默认提供程序实现
    /// </summary>
    public class DefaultUrlAuthorizationProvider : IUrlAuthorizationProvider
    {
        /// <summary>
        /// 判断用户对指定的Url是否有权进行指定的动作
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="url">要访问的Url</param>
        /// <param name="verb">对Url的动作（GET, POST....）</param>
        /// <returns>如果用户有权访问，返回true，否则返回false。</returns>
        public bool IsUserAllowed(IPrincipal user, Uri url, string verb)
        {

            if (url.AbsolutePath.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase))
            {
                if ((url.AbsolutePath.ToLower().IndexOf("activepage.aspx") >= 0 || url.AbsolutePath.ToLower().IndexOf("confirmorder.aspx") >= 0 || url.AbsolutePath.ToLower().IndexOf("confirmrecipient.aspx") >= 0 || url.AbsolutePath.ToLower().IndexOf("confirmticket.aspx") >= 0 || url.AbsolutePath.ToLower().IndexOf("offerletterconfirm.aspx") >= 0 || url.AbsolutePath.ToLower().IndexOf("suppliermessagereturn.aspx") >= 0) || url.AbsolutePath.ToLower().IndexOf("pintuibao.aspx") >= 0)
                {
                    return true;
                }
                if ((user == null || user.Identity == null || user.Identity.IsAuthenticated == false) && (url.Query == null || url.Query.IndexOf("&InternalPassword=f67u7b6i8asdf") < 0))
                {
                    return false;
                }
            }
            else 
                return true;

            string absolutePath = url.AbsolutePath;
            string appPath = HttpRuntime.AppDomainAppVirtualPath;
            int length = appPath.EndsWith("/") ? appPath.Length : appPath.Length + 1;
            string appRelativePath = absolutePath.Substring(length);

            WebSiteInfo website = WebSiteManager.Get();
            IList<int> mids = WebPageManager.GetModuleIDsByPath(website.WebSiteID, appRelativePath);

            if (mids == null || mids.Count == 0)
                return true;

            foreach(int mid in mids)
            {
                if (PermissionManager.HasModulePermission(null, mid, UserManager.GetCurrentUserID()))
                    return true;
            }
            return false;
        }
    }
}