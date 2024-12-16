

using System;
using ESP.Framework.Entity;
using System.Collections.Generic;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 部门类别数据提供程序抽象接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IDepartmentTypeDataProvider
    {
        /// <summary>
        /// 获取指定ID的部门类型
        /// </summary>
        /// <param name="id">类型ID</param>
        /// <returns>部门类型信息对象</returns>
        DepartmentTypeInfo Get(int id);


        /// <summary>
        /// 获取所有部门类型列表
        /// </summary>
        /// <returns>所有部门类型信息的列表</returns>
        IList<DepartmentTypeInfo> GetAll();

        /// <summary>
        /// 创建新的部门类型
        /// </summary>
        /// <param name="departmentType">要创建的部门类型对象</param>
        void Create(DepartmentTypeInfo departmentType);

        /// <summary>
        /// 更新部门类型信息
        /// </summary>
        /// <param name="departmentType">新的部门类型信息</param>
        void Update(DepartmentTypeInfo departmentType);

        /// <summary>
        /// 删除指定ID的部门类型
        /// </summary>
        /// <param name="id">要删除的部门类型的ID</param>
        void Delete(int id);
    }
}
