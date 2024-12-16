using System;
using System.Collections.Generic;
using System.Web;
using ESP.Configuration;
using System.Security.Principal;

namespace ESP.Security
{
    /// <summary>
    ///Url授权控制类
    /// </summary>
    public static class UrlAuthorization
    {
        /// <summary>
        /// 判断用户是否有权限访问指定的Url
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="url">路径</param>
        /// <param name="verb">动作</param>
        /// <returns>如果有权访问返回true，否则返回false。</returns>
        public static bool IsUserAllowed(IPrincipal user, Uri url, string verb)
        {
            return ESP.Configuration.ProviderHelper<IUrlAuthorizationProvider>.Instance.IsUserAllowed(user, url, verb);
        }
    }
}