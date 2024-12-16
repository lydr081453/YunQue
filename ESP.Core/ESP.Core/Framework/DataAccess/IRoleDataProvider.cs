

using System;
using ESP.Framework.Entity;
using System.Collections.Generic;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 角色数据接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IRoleDataProvider
    {

        /// <summary>
        /// 获取指定ID的角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>角色信息</returns>
        RoleInfo Get(int id);

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns>所有角色的列表</returns>
        IList<RoleInfo> GetAll();

        /// <summary>
        /// 创建新角色
        /// </summary>
        /// <param name="role">新角色信息</param>
        void Create(RoleInfo role);


        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">要更新的角色信息</param>
        void Update(RoleInfo role);

        /// <summary>
        /// 删除指定的角色
        /// </summary>
        /// <param name="id">角色ID</param>
        void Delete(int id);

        /// <summary>
        /// 获取指定用户直接或间接隶属的角色的ID列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>角色ID列表</returns>
        int[] GetUserRoleIDs(int userId);

        /// <summary>
        /// 将指定的实体添加到角色中
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="type">实体类型</param>
        void AddEntityToRole(int entityId, int roleId, RoleOwnerType type);

        /// <summary>
        /// 将指定的实体从角色中移除
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="type">实体类型</param>
        void RemoveEntityFromRole(int entityId, int roleId, RoleOwnerType type);

        /// <summary>
        /// 获取指定实体所属的角色列表
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="entityType">实体类型</param>
        /// <returns>实体所属于的角色的列表</returns>
        IList<RoleInfo> GetByEntity(int entityId, RoleOwnerType entityType);

        /// <summary>
        /// 获取属于指定角色的所有部门
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>属于该角色的所有部门</returns>
        IList<UserInfo> GetUsersInRole(int roleId);

        /// <summary>
        /// 获取直接属于指定角色的所有用户
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>直接属于该角色的所有用户</returns>
        IList<EmployeeInfo> GetEmployeesInRole(int roleId);

        /// <summary>
        /// 获取直接属于指定角色的所有员工
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>直接属于该角色的所有员工</returns>
        IList<DepartmentInfo> GetDepartmentsInRole(int roleId);

        /// <summary>
        /// 获取指定用户的所有的角色，包括直接和间接角色。
        /// </summary>
        /// <param name="userId">用户的ID</param>
        /// <returns>用户所属的所有角色信息的列表。</returns>
        IList<RoleInfo> GetUserRoles(int userId);
    }
}
