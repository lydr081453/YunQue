

using System;
using ESP.Framework.Entity;
using System.Collections.Generic;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 部门数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IDepartmentDataProvider
    {
        /// <summary>
        /// 获取指定ID的部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>部门信息</returns>
        DepartmentInfo Get(int id);

        /// <summary>
        /// 获取所有部门的信息列表
        /// </summary>
        /// <returns>所有部门列表</returns>
        IList<DepartmentInfo> GetAll();

        /// <summary>
        /// 创建新的部门
        /// </summary>
        /// <param name="department">要创建的部门的信息</param>
        void Create(DepartmentInfo department);

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="department">新的部门信息</param>
        void Update(DepartmentInfo department);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门的ID</param>
        void Delete(int id);

        /// <summary>
        /// 获取指定ID的部门的所有直接子级部门的信息列表
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>所有直接子级部门的信息列表</returns>
        IList<DepartmentInfo> GetChildren(int id);

        /// <summary>
        /// 获取部门所有子孙部门
        /// </summary>
        /// <param name="parentId">部门ID</param>
        /// <returns>所有部门列表</returns>
        IList<DepartmentInfo> GetChildrenRecursion(int parentId);
        IList<DepartmentInfo> GetChildrenRecursionByDesc(int parentId);
        /// <summary>
        /// 获取指定名称的部门信息
        /// </summary>
        /// <param name="name">部门名称</param>
        /// <returns>部门信息</returns>
        IList<ESP.Framework.Entity.DepartmentInfo> Get(string name);

        /// <summary>
        /// 根据部门获取员工
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>员工列表</returns>
        IList<EmployeeInfo> GetEmployeesByDepartment(int departmentId);

        /// <summary>
        /// 根据部门代码获取部门信息
        /// </summary>
        /// <param name="code">部门代码</param>
        /// <returns>部门信息</returns>
        DepartmentInfo GetByCode(string code);

        /// <summary>
        /// 检查部门代码是否已经存在
        /// </summary>
        /// <param name="code">部门代码</param>
        /// <returns>存在则返回 true，否则返回 false</returns>
        bool CodeExists(string code);
    }
}
