
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 部门类型控制
    /// </summary>
    public static class DepartmentTypeManager
    {
        #region Private Members
        private static IDepartmentTypeDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IDepartmentTypeDataProvider>.Instance;
        }
        #endregion

        /// <summary>
        /// 获取指定ID的部门类型
        /// </summary>
        /// <param name="id">类型ID</param>
        /// <returns>部门类型信息对象</returns>
        public static ESP.Framework.Entity.DepartmentTypeInfo Get(int id)
        {
            return GetProvider().Get(id);
        }

        /// <summary>
        /// 获取所有部门类型列表
        /// </summary>
        /// <returns>所有部门类型信息的列表</returns>
        public static IList<DepartmentTypeInfo> GetAll()
        {
            return GetProvider().GetAll();
        }

        /// <summary>
        /// 更新部门类型信息
        /// </summary>
        /// <param name="departmentType">新的部门类型信息</param>
        public static void Update( ESP.Framework.Entity.DepartmentTypeInfo departmentType)
        {
            GetProvider().Update( departmentType);
        }

        /// <summary>
        /// 创建新的部门类型
        /// </summary>
        /// <param name="departmentType">要创建的部门类型对象</param>
        public static void Create( ESP.Framework.Entity.DepartmentTypeInfo departmentType)
        {
            GetProvider().Create( departmentType);
        }

        /// <summary>
        /// 删除指定ID的部门类型
        /// </summary>
        /// <param name="id">要删除的部门类型的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }
    }
}
