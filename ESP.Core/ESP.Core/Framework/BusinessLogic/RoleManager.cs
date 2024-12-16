
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// Role 访问控制类
    /// </summary>
    public static class RoleManager
    {
        #region Private Memebers
        private static IRoleDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IRoleDataProvider>.Instance;
        }

        #endregion

        /// <summary>
        /// 获取指定ID的角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>角色对象</returns>
        public static ESP.Framework.Entity.RoleInfo Get(int id)
        {
            return GetProvider().Get(id);
        }

        /// <summary>
        /// 获取指定实体所属于的角色列表
        /// </summary>
        /// <param name="entityID">实体ID</param>
        /// <param name="entityType">实体类型</param>
        /// <returns>角色列表</returns>
        public static IList<ESP.Framework.Entity.RoleInfo> GetByEntity(int entityID, RoleOwnerType entityType)
        {
            return GetProvider().GetByEntity(entityID, entityType);
        }

        /// <summary>
        /// 获取加入到指定ID的角色中的部门的列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>部门列表</returns>
        public static IList<DepartmentInfo> GetDepartmentsInRole(int roleId)
        {
            return GetProvider().GetDepartmentsInRole(roleId);

        }

        /// <summary>
        /// 获取加入到指定ID的角色中的用户的列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>用户列表</returns>
        public static IList<UserInfo> GetUsersInRole(int roleId)
        {
            return GetProvider().GetUsersInRole(roleId);
        }

        /// <summary>
        /// 获取加入到指定ID的角色中的员工的列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>员工列表</returns>
        public static IList<EmployeeInfo> GetEmployeesInRole(int roleId)
        {
            return GetProvider().GetEmployeesInRole(roleId);
        }

        /// <summary>
        /// 获取所有的角色的列表
        /// </summary>
        /// <returns>角色列表</returns>
        public static IList<RoleInfo> GetAll()
        {
            return GetProvider().GetAll();
        }

        /// <summary>
        /// 创建新的角色
        /// </summary>
        /// <param name="role">要创建的角色对象</param>
        public static void Create( ESP.Framework.Entity.RoleInfo role)
        {
            if (role.Creator <= 0)
                role.Creator = UserManager.GetCurrentUserID();
            if (role.CreatedTime <= NullValues.DateTime)
                role.CreatedTime = DateTime.Now;

            GetProvider().Create( role);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">要更新的角色对象</param>
        public static void Update( ESP.Framework.Entity.RoleInfo role)
        {
            if (role.LastModifier <= 0)
                role.LastModifier = UserManager.GetCurrentUserID();
            if (role.LastModifiedTime <= NullValues.DateTime)
                role.LastModifiedTime = DateTime.Now;

            GetProvider().Update( role);
        }

        /// <summary>
        /// 删除指定ID的角色
        /// </summary>
        /// <param name="id">要删除的角色的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 将指定ID的部门添加到角色
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <param name="roleId">角色ID</param>
        public static void AddDepartmentToRole(int departmentID, int roleId)
        {
            GetProvider().AddEntityToRole(departmentID, roleId, RoleOwnerType.Department);
        }

        /// <summary>
        /// 将指定ID的用户添加到角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        public static void AddUserToRole(int userId, int roleId)
        {
            GetProvider().AddEntityToRole(userId, roleId, RoleOwnerType.User);
        }

        /// <summary>
        /// 将指定的实体添加到角色
        /// </summary>
        /// <param name="entityID">实体ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="type">实体类型</param>
        public static void AddEntityToRole(int entityID, int roleId, RoleOwnerType type)
        {
            GetProvider().AddEntityToRole(entityID, roleId, type);
        }

        /// <summary>
        /// 将指定的实体从角色中删除
        /// </summary>
        /// <param name="entityID">实体ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="type">实体类型</param>
        public static void RemoveEntityFromRole(int entityID, int roleId, RoleOwnerType type)
        {
            GetProvider().RemoveEntityFromRole(entityID, roleId, type);
        }

        /// <summary>
        /// 获取用户所属于的角色的ID列表，包含该用户所属的部门所拥有的角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>角色ID列表</returns>
        public static int[] GetUserRoleIDs(int userId)
        {
            return GetProvider().GetUserRoleIDs(userId);;
        }


        /// <summary>
        /// 获取用户所属于的角色的ID列表，包含该用户所属的部门所拥有的角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>角色ID列表</returns>
        public static IList<RoleInfo> GetUserRoles(int userId)
        {
            return GetProvider().GetUserRoles(userId); ;
        }

    }
}
