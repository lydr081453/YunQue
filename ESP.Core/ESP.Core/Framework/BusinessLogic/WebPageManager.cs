
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 页面控制类
    /// </summary>
    public static class WebPageManager
    {
        #region Private Members
        //private static object _lockObj = new object();
        //private static IWebPageDataProvider _provider = null;

        //private static IWebPageDataProvider GetProvider()
        //{
        //    if (_provider != null)
        //        return _provider;

        //    lock (_lockObj)
        //    {
        //        if (_provider != null)
        //            return _provider;

        //        _provider = (IWebPageDataProvider)ESP.Configuration.ConfigurationManager.GetProvider("webPageDataProvider");
        //    }
        //    return _provider;
        //}

        private static IWebPageDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IWebPageDataProvider>.Instance;
        }
        #endregion

        /// <summary>
        /// 获取指定ID页面的信息
        /// </summary>
        /// <param name="id">页面的ID</param>
        /// <returns>页面信息</returns>
        public static ESP.Framework.Entity.WebPageInfo Get(int id)
        {
            return GetProvider().Get(id);
        }


        /// <summary>
        /// 获取所有页面的信息列表
        /// </summary>
        /// <returns>所有页面的信息列表</returns>
        public static IList<WebPageInfo> GetAll()
        {
            IList<WebPageInfo> list = GetProvider().GetAll();
            return list;
        }

        /// <summary>
        /// 创建新的页面
        /// </summary>
        /// <param name="webPage">要创建的页面</param>
        public static void Create( ESP.Framework.Entity.WebPageInfo webPage)
        {
            if (webPage.CreatedTime <= NullValues.DateTime)
                webPage.CreatedTime = DateTime.Now;

            if (webPage.Creator <= 0)
                webPage.Creator = UserManager.GetCurrentUserID();

            webPage.LastModifier = 0;
            webPage.LastModifiedTime = NullValues.DateTime;

            GetProvider().Create( webPage);
        }

        /// <summary>
        /// 更新页面信息
        /// </summary>
        /// <param name="webPage">要更新的页面</param>
        public static void Update( ESP.Framework.Entity.WebPageInfo webPage)
        {
            GetProvider().Update( webPage);
        }

        /// <summary>
        /// 删除指定ID的页面
        /// </summary>
        /// <param name="id">要删除的页面的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 获取指定路径的页面
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="appRelativePath">页面对站点根目录的相对路径</param>
        /// <returns>页面信息</returns>
        public static WebPageInfo GetByPath(int webSiteId, string appRelativePath)
        {
            return GetProvider().GetByPath(webSiteId, appRelativePath);
        }


        /// <summary>
        /// 获取指定路径的页面所属的所有模块的ID
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="appRelativePath">页面对站点根目录的相对路径</param>
        /// <returns>模块ID列表</returns>
        public static IList<int> GetModuleIDsByPath(int webSiteId, string appRelativePath)
        {
            return GetProvider().GetModuleIDsByPath(webSiteId, appRelativePath);
        }

        /// <summary>
        /// 获取指定模块的所有页面的列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns>页面信息列表</returns>
        public static IList<WebPageInfo> GetByModule(int moduleId)
        {
            return GetProvider().GetByModule(moduleId);
        }

        /// <summary>
        /// 获取指定站点的所有页面的列表
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>页面信息列表</returns>
        public static IList<WebPageInfo> GetByWebSite(int webSiteId)
        {
            return GetProvider().GetByWebSite(webSiteId);
        }
    }
}
