
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using System.Web.Caching;
using ESP.Framework.Entity;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 权限访问控制类
    /// </summary>
    public static class PermissionManager
    {

        #region Private Members
        private static IPermissionDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IPermissionDataProvider>.Instance;
        }


        private static bool IsStringInArray(string s, string[] array)
        {
            if (array == null)
                return false;

            if (s == null)
                return array.Length > 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (string.Compare(s, array[i], true) == 0)
                    return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 判断指定ID的用户对指定ID的页面是否拥有指定权限
        /// </summary>
        /// <param name="permission">请求的权限</param>
        /// <param name="pageID">页面ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>如果指定的用户对页面拥有该权限，则返回 true，否则返回 false。</returns>
        public static bool HasWebPagePermission(string permission, int pageID, int userId)
        {
            return HasPermission(permission, EntityType.WebPage, pageID, userId);
        }

        /// <summary>
        /// 判断指定ID的用户对指定ID的模块是否拥有指定权限
        /// </summary>
        /// <param name="permission">请求的权限</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>如果指定的用户对模块拥有该权限，则返回 true，否则返回 false。</returns>
        public static bool HasModulePermission(string permission, int moduleId, int userId)
        {
            return HasPermission(permission, EntityType.Module, moduleId, userId);
        }

        /// <summary>
        /// 判断指定ID的用户对指定ID的站点是否拥有指定权限
        /// </summary>
        /// <param name="permission">请求的权限</param>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>如果指定的用户对站点拥有该权限，则返回 true，否则返回 false。</returns>
        public static bool HasWebSitePermission(string permission, int webSiteId, int userId)
        {
            return HasPermission(permission, EntityType.WebSite, webSiteId, userId);
        }

        /// <summary>
        /// 判断指定的用户对指定的实体是否拥有指定的权限
        /// </summary>
        /// <param name="permission">请求的权限</param>
        /// <param name="entityType">实体类型</param>
        /// <param name="entityID">实体ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>如果指定的用户对实体拥有该权限，则返回 true，否则返回 false。</returns>
        public static bool HasPermission(string permission, EntityType entityType, int entityID, int userId)
        {
            string[] everyonePermissions = PermissionManager.GetPermissions(entityType, entityID, PermissionOwnerTypes.FakeRole, FakeRoles.Everyone);
            if (IsStringInArray(permission, everyonePermissions))
                return true;
            if (userId > 0)
            {
                string[] registeredPermissions = PermissionManager.GetPermissions(entityType, entityID, PermissionOwnerTypes.FakeRole, FakeRoles.Registered);
                if (IsStringInArray(permission, registeredPermissions))
                    return true;

                //string[] userPermission = PermissionManager.GetPermissions(entityType, entityID, PermissionOwnerTypes.User, userId);
                //if (IsStringInArray(permission, userPermission))
                //    return true;

                int[] roles = RoleManager.GetUserRoleIDs(userId);
                foreach (int r in roles)
                {
                    string[] rolePermission = PermissionManager.GetPermissions(entityType, entityID, PermissionOwnerTypes.Role, r);
                    if (IsStringInArray(permission, rolePermission))
                        return true;
                }
            }
            else
            {
                string[] anonymousPermissions = PermissionManager.GetPermissions(entityType, entityID, PermissionOwnerTypes.FakeRole, FakeRoles.Anonymous);
                if (IsStringInArray(permission, anonymousPermissions))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获得取指定ID的权限的详细信息
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns>权限信息对象</returns>
        public static PermissionInfo Get(int id)
        {
            return GetProvider().Get(id);
        }

        /// <summary>
        /// 添加新的权限，即将指定的权限分配给指定的权限持有者（角色、用户等）
        /// </summary>
        /// <param name="permission">权限对象</param>
        public static void Add( PermissionInfo permission)
        {
            GetProvider().Add( permission);
        }

        /// <summary>
        /// 删除指定的权限分配
        /// </summary>
        /// <param name="id">要删除的权限分配的ID</param>
        public static void Remove(int id)
        {
            GetProvider().Remove(id);
        }

        /// <summary>
        /// 获取角色拥有的权限
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <param name="isFakeRole">是否为内置的伪角色</param>
        /// <returns></returns>
        public static IList<KeyValuePair<int, int>> GetRolePermisssions(int roleId, bool isFakeRole)
        {
            return GetProvider().GetRolePermisssions(roleId, isFakeRole);
        }


        /// <summary>
        /// 获取指定ID的权限定义
        /// </summary>
        /// <param name="id">权限定义ID</param>
        /// <returns>权限定义对象</returns>
        public static PermissionDefinitionInfo GetDefinition(int id)
        {
            return GetProvider().GetDefinition(id);
        }
        /// <summary>
        /// 获取与指定实体关联的所有权限定义
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="entityID">实体ID</param>
        /// <returns>权限定义对象列表</returns>
        public static IList<PermissionDefinitionInfo> GetDefinitions(EntityType entityType, int entityID)
        {
            return GetProvider().GetDefinitions(entityType, entityID);
        }

        /// <summary>
        /// 获取所有的权限定义
        /// </summary>
        /// <returns>所有权限定义的列表</returns>
        public static IList<PermissionDefinitionInfo> GetAllDefinitions()
        {
            return GetProvider().GetAllDefinitions();
        }


        /// <summary>
        /// 删除指定ID的权限定义
        /// </summary>
        /// <param name="id">要删除的权限定义的ID</param>
        public static void RemoveDefinition(int id)
        {
            GetProvider().RemoveDefinition(id);
        }

        /// <summary>
        /// 添加新的权限定义
        /// </summary>
        /// <param name="permissionDefinition">要添加的权限定义</param>
        public static void AddDefinition( PermissionDefinitionInfo permissionDefinition)
        {
            GetProvider().AddDefinition( permissionDefinition);
        }

        /// <summary>
        /// 获取指定的权限持有者（角色、用户等）对指定的实体对象拥有的权限列表
        /// </summary>
        /// <param name="entityType">权限控制的实体的类型</param>
        /// <param name="entityID">权限控制的实体的ID</param>
        /// <param name="ownerType">权限持有者类型</param>
        /// <param name="ownerID">权限持有者ID</param>
        /// <returns>权限名字的列表</returns>
        public static string[] GetPermissions(EntityType entityType, int entityID, PermissionOwnerTypes ownerType, int ownerID)
        {
            return GetProvider().GetPermissions(entityType, entityID, ownerType, ownerID);;
        }


        /// <summary>
        /// 更新角色的权限（只针对模块）
        /// </summary>
        /// <param name="list">权限列表， key为权限定义， value为模块ID</param>
        /// <param name="roleId">角色的ID</param>
        /// <param name="isFakeRole">roleId是否为系统内置的伪角色</param>
        public static void UpdateRolePermissions(IList<KeyValuePair<int, int>> list, int roleId, bool isFakeRole)
        {
            GetProvider().UpdateRolePermissions(list, roleId, isFakeRole);
        }
    }


}
