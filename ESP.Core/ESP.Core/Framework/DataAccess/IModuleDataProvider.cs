using System;
using System.Collections.Generic;
using System.Text;
using ESP.Framework.Entity;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 功能模块抽象数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IModuleDataProvider
    {
        /// <summary>
        /// 获取指定ID的模块信息
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <returns>模块信息对象</returns>
        ModuleInfo Get(int id);

        /// <summary>
        /// 获取指定站点的模块列表
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>模块列表</returns>
        IList<ModuleInfo> GetByWebSite(int webSiteId);

        /// <summary>
        /// 更新模块信息
        /// </summary>
        /// <param name="module">要更新的模块信息对象</param>
        void Update(ModuleInfo module);

        /// <summary>
        /// 创建新的模块
        /// </summary>
        /// <param name="module">要创建的模块信息对象</param>
        void Create(ModuleInfo module);

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="id">要删除的模块的ID</param>
        void Delete(int id);

        /// <summary>
        /// 获取指定用户在指定站点有权限访问的模块的列表
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>模块列表</returns>
        IList<ModuleInfo> GetByUser(int webSiteId, int userId);

        /// <summary>
        /// 获取所有站点的所有模块的列表
        /// </summary>
        /// <returns>模块列表</returns>
        IList<ModuleInfo> GetAll();

    }
}
