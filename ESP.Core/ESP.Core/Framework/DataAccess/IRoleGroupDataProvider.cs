

using System;
using ESP.Framework.Entity;
using System.Collections.Generic;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 角色数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IRoleGroupDataProvider
    {
        /// <summary>
        /// 获取指定ID的角色组
        /// </summary>
        /// <param name="id">角色组ID</param>
        /// <returns>角色组信息</returns>
        RoleGroupInfo Get(int id);

        /// <summary>
        /// 获取所有角色组的列表
        /// </summary>
        /// <returns>角色组信息列表</returns>
        IList<RoleGroupInfo> GetAll();

        /// <summary>
        /// 更新角色组信息
        /// </summary>
        /// <param name="roleGroup">要更新的角色组</param>
        void Update(RoleGroupInfo roleGroup);

        /// <summary>
        /// 创建新的角色组
        /// </summary>
        /// <param name="roleGroup">要创建的角色组</param>
        void Create(RoleGroupInfo roleGroup);


        /// <summary>
        /// 删除指定ID的角色组
        /// </summary>
        /// <param name="id">要删除的角色组的ID</param>
        void Delete(int id);
    }
}
