

using System;
using ESP.Framework.Entity;
using System.Collections.Generic;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 页面数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IWebPageDataProvider
    {
        /// <summary>
        /// 获取指定ID页面的信息
        /// </summary>
        /// <param name="id">页面的ID</param>
        /// <returns>页面信息</returns>
        WebPageInfo Get(int id);


        /// <summary>
        /// 获取所有页面的信息列表
        /// </summary>
        /// <returns>所有页面的信息列表</returns>
        IList<WebPageInfo> GetAll();

        /// <summary>
        /// 获取指定路径的页面
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="appRelativePath">页面对站点根目录的相对路径</param>
        /// <returns>页面信息</returns>
        WebPageInfo GetByPath(int webSiteId, string appRelativePath);

        /// <summary>
        /// 创建新的页面
        /// </summary>
        /// <param name="webPage">要创建的页面</param>
        void Create(WebPageInfo webPage);

        /// <summary>
        /// 更新页面信息
        /// </summary>
        /// <param name="webPage">要更新的页面</param>
        void Update(WebPageInfo webPage);

        /// <summary>
        /// 删除指定ID的页面
        /// </summary>
        /// <param name="id">要删除的页面的ID</param>
        void Delete(int id);


        /// <summary>
        /// 获取指定站点的所有页面的列表
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>页面信息列表</returns>
        IList<WebPageInfo> GetByWebSite(int webSiteId);

        /// <summary>
        /// 获取指定模块的所有页面的列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns>页面信息列表</returns>
        IList<WebPageInfo> GetByModule(int moduleId);

        /// <summary>
        /// 获取指定路径的页面所属的所有模块的Id
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="appRelativePath">页面对站点根目录的相对路径</param>
        /// <returns>模块ID列表</returns>
        IList<int> GetModuleIDsByPath(int webSiteId, string appRelativePath);
    }
}
