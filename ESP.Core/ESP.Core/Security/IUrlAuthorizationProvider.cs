using System;
using System.Collections.Generic;
using System.Web;
using ESP.Configuration;
using System.Security.Principal;

namespace ESP.Security
{
    /// <summary>
    /// 对Url进行授权检验的提供程序的抽象接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IUrlAuthorizationProvider
    {
        /// <summary>
        /// 判断用户对指定的Url是否有权进行指定的动作
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="url">要访问的Url</param>
        /// <param name="verb">对Url的动作（GET, POST....）</param>
        /// <returns>如果用户有权访问，返回true，否则返回false。</returns>
        bool IsUserAllowed(IPrincipal user, Uri url, string verb);
    }
}