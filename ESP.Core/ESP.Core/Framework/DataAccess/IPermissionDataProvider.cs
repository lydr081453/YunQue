

using System;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using System.Collections.Generic;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 权限数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IPermissionDataProvider
    {
        /// <summary>
        /// 获得取指定ID的权限的详细信息
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns>权限信息对象</returns>
        PermissionInfo Get(int id);

        /// <summary>
        /// 添加新的权限，即将指定的权限分配给指定的权限持有者（角色、用户等）
        /// </summary>
        /// <param name="permission">权限对象</param>
        void Add(PermissionInfo permission);

        /// <summary>
        /// 删除指定的权限分配
        /// </summary>
        /// <param name="id">要删除的权限分配的ID</param>
        void Remove(int id);

        /// <summary>
        /// 获取指定ID的权限定义
        /// </summary>
        /// <param name="id">权限定义的ID</param>
        /// <returns>权限定义</returns>
        PermissionDefinitionInfo GetDefinition(int id);

        /// <summary>
        /// 获取指定实体的权限定义列表
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="entityId">实体ID</param>
        /// <returns></returns>
        IList<PermissionDefinitionInfo> GetDefinitions(EntityType entityType, int entityId);

        /// <summary>
        /// 删除指定ID的权限定义
        /// </summary>
        /// <param name="id">要删除的权限定义的ID</param>
        void RemoveDefinition(int id);

        /// <summary>
        /// 添加新的权限定义
        /// </summary>
        /// <param name="permissionDefinition">要添加的权限定义</param>
        void AddDefinition(PermissionDefinitionInfo permissionDefinition);

        /// <summary>
        /// 获取所有的权限定义
        /// </summary>
        /// <returns>所有权限定义的列表</returns>
        IList<PermissionDefinitionInfo> GetAllDefinitions();

        /// <summary>
        /// 获取指定的权限持有者（角色、用户等）对指定的实体对象拥有的权限列表
        /// </summary>
        /// <param name="entityType">权限控制的实体的类型</param>
        /// <param name="entityId">权限控制的实体的ID</param>
        /// <param name="ownerType">权限持有者类型</param>
        /// <param name="ownerId">权限持有者ID</param>
        /// <returns>权限名字的列表</returns>
        string[] GetPermissions(EntityType entityType, int entityId, PermissionOwnerTypes ownerType, int ownerId);

        /// <summary>
        /// 获取角色拥有的权限（只针对模块）
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <param name="isFakeRole">是否为内置的伪角色</param>
        /// <returns>角色拥有的权限列表， key为权限定义， value为模块ID</returns>
        IList<KeyValuePair<int, int>> GetRolePermisssions(int roleId, bool isFakeRole);

        /// <summary>
        /// 更新角色的权限（只针对模块）
        /// </summary>
        /// <param name="list">权限列表， key为权限定义， value为模块ID</param>
        /// <param name="roleId">角色的ID</param>
        /// <param name="isFakeRole">roleId是否为系统内置的伪角色</param>
        void UpdateRolePermissions(IList<KeyValuePair<int, int>> list, int roleId, bool isFakeRole);
    }
}
