
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using ESP.Framework;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 角色控制类
    /// </summary>
    public static class RoleGroupManager
    {
        #region Private Memebers
        private static IRoleGroupDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IRoleGroupDataProvider>.Instance;
        }

        private static void PopulateChildren(Tree<RoleGroupInfo> parent, RoleGroupInfo parentGroup, IList<RoleGroupInfo> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                RoleGroupInfo rg = list[i];
                if (rg.ParentID != parentGroup.RoleGroupID)
                    continue;

                Tree<RoleGroupInfo> child = new Tree<RoleGroupInfo>(rg.RoleGroupID, rg.RoleGroupName, rg);
                parent.AddChild(child);
                PopulateChildren(child, rg, list);
            }
        }
        #endregion

        /// <summary>
        /// 获取指定ID的角色组
        /// </summary>
        /// <param name="id">角色组ID</param>
        /// <returns>角色组信息</returns>
        public static ESP.Framework.Entity.RoleGroupInfo Get(int id)
        {
            return GetProvider().Get(id);;
        }

        /// <summary>
        /// 获取所有角色组的列表
        /// </summary>
        /// <returns>角色组信息列表</returns>
        public static IList<RoleGroupInfo> GetAll()
        {
            return GetProvider().GetAll();
        }

        /// <summary>
        /// 更新角色组信息
        /// </summary>
        /// <param name="roleGroup">要更新的角色组</param>
        public static void Update( ESP.Framework.Entity.RoleGroupInfo roleGroup)
        {
            GetProvider().Update( roleGroup);
        }

        /// <summary>
        /// 创建新的角色组
        /// </summary>
        /// <param name="roleGroup">要创建的角色组</param>
        public static void Create( ESP.Framework.Entity.RoleGroupInfo roleGroup)
        {
            GetProvider().Create( roleGroup);
        }

        /// <summary>
        /// 删除指定ID的角色组
        /// </summary>
        /// <param name="id">要删除的角色组的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 获取所有角色组的列表，并以树形结构返回
        /// </summary>
        /// <returns>所有角色组的树形集合</returns>
        public static Tree<RoleGroupInfo> GetRoleGroupTree()
        {
            IList<RoleGroupInfo> list = GetProvider().GetAll();
            if (list == null || list.Count == 0)
                return null;

            for (int i = 0; i < list.Count; i++)
            {
                RoleGroupInfo rg = list[i];
                if (rg.ParentID != 0)
                    continue;

                Tree<RoleGroupInfo> t = new Tree<RoleGroupInfo>(rg.RoleGroupID, rg.RoleGroupName, rg);
                PopulateChildren(t, rg, list);
                return t;
            }

            return null;
        }
    }
}
