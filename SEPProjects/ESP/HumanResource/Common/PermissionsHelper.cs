using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

namespace ESP.HumanResource.Common
{
    public static class PermissionsHelper
    {
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
    }
}
