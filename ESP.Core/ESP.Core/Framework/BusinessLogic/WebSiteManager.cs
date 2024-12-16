
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using ESP.Utilities;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 站点控制类
    /// </summary>
    public static class WebSiteManager
    {
        #region Private Members
        //private static object _lockObj = new object();
        //private static IWebSiteDataProvider _provider = null;

        //private static IWebSiteDataProvider GetProvider()
        //{
        //    if (_provider != null)
        //        return _provider;

        //    lock (_lockObj)
        //    {
        //        if (_provider != null)
        //            return _provider;

        //        _provider = (IWebSiteDataProvider)ESP.Configuration.ConfigurationManager.GetProvider("webSiteDataProvider");
        //    }
        //    return _provider;
        //}

        private static IWebSiteDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IWebSiteDataProvider>.Instance;
        }

        #endregion

        /// <summary>
        /// 获取指定ID的站点的信息
        /// </summary>
        /// <param name="id">站点的ID</param>
        /// <returns>站点信息</returns>
        public static ESP.Framework.Entity.WebSiteInfo Get(int id)
        {
            return GetProvider().Get(id);
        }

        /// <summary>
        /// 获取所有站点的列表
        /// </summary>
        /// <returns>站点列表</returns>
        public static IList<WebSiteInfo> GetAll()
        {
            return GetProvider().GetAll();

        }

        /// <summary>
        /// 删除指定ID的站点
        /// </summary>
        /// <param name="id">要删除的站点的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 获取当前站点的信息
        /// </summary>
        /// <returns>当前站点的信息</returns>
        public static ESP.Framework.Entity.WebSiteInfo Get()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            if (context != null)
            {
                WebSiteInfo webSite = context.Items["sep_currentWebSite"] as WebSiteInfo;
                if (webSite == null)
                {
                    webSite = Get(ESP.Configuration.ConfigurationManager.WebSiteID);
                    context.Items["sep_currentWebSite"] = webSite;
                }
                return webSite;
            }
            return Get(ESP.Configuration.ConfigurationManager.WebSiteID);
        }

        /// <summary>
        /// 创建新的站点
        /// </summary>
        /// <param name="webSite">要创建的站点</param>
        public static void Create( WebSiteInfo webSite)
        {
            if (webSite.CreatedTime <= NullValues.DateTime)
                webSite.CreatedTime = DateTime.Now;

            if (webSite.Creator <= 0)
                webSite.Creator = UserManager.GetCurrentUserID();

            GetProvider().Create( webSite);
        }


        /// <summary>
        /// 更新站点信息
        /// </summary>
        /// <param name="webSite">要更新的站点的信息</param>
        public static void Update( WebSiteInfo webSite)
        {
            if (webSite.LastModifiedTime <= NullValues.DateTime)
                webSite.LastModifiedTime = DateTime.Now;

            if (webSite.LastModifier <= 0)
                webSite.LastModifier = UserManager.GetCurrentUserID();

            GetProvider().Update( webSite);
        }

        /// <summary>
        /// 获取指定Url前缀的站点
        /// </summary>
        /// <param name="urlPrefix">站点的Url前缀(不包含http://或https://，也不包含最后的反斜杠)</param>
        /// <returns>站点信息对象</returns>
        public static WebSiteInfo GetByUrlPrefix(string urlPrefix)
        {
            return GetProvider().GetByUrlPrefix(urlPrefix);
        }

        /// <summary>
        /// 获取指定ID的用户可以访问的站点的列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>可访问的站点列表</returns>
        public static IList<WebSiteInfo> GetByUser(int userId)
        {
            return GetProvider().GetByUser(userId);
        }
    }
}
