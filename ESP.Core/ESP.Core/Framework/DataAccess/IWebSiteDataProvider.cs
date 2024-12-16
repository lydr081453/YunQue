

using System;
using ESP.Framework.Entity;
using System.Collections.Generic;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 站点数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IWebSiteDataProvider
    {
        /// <summary>
        /// 获取指定ID的站点的信息
        /// </summary>
        /// <param name="id">站点的ID</param>
        /// <returns>站点信息</returns>
        WebSiteInfo Get(int id);

        /// <summary>
        /// 获取所有站点的列表
        /// </summary>
        /// <returns>站点列表</returns>
        IList<WebSiteInfo> GetAll();

        /// <summary>
        /// 更新站点信息
        /// </summary>
        /// <param name="webSite">要更新的站点的信息</param>
        void Update(WebSiteInfo webSite);

        /// <summary>
        /// 创建新的站点
        /// </summary>
        /// <param name="webSite">要创建的站点</param>
        void Create(WebSiteInfo webSite);

        /// <summary>
        /// 删除指定ID的站点
        /// </summary>
        /// <param name="id">要删除的站点的ID</param>
        void Delete(int id);

        /// <summary>
        /// 获取指定ID的用户可以访问的站点的列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>可访问的站点列表</returns>
        IList<WebSiteInfo> GetByUser(int userId);

        /// <summary>
        /// 获取指定Url前缀的站点
        /// </summary>
        /// <param name="urlPrefix">站点的Url前缀(不包含http://或https://，也不包含最后的反斜杠)</param>
        /// <returns>站点信息对象</returns>
        WebSiteInfo GetByUrlPrefix(string urlPrefix);
    }
}
