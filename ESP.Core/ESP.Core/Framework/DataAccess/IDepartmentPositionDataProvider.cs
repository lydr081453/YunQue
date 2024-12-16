

using System;
using ESP.Framework.Entity;
using System.Collections.Generic;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 部门职务数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IDepartmentPositionDataProvider
    {
        /// <summary>
        /// 获取指定ID的部门职务
        /// </summary>
        /// <param name="id">职务ID</param>
        /// <returns>描述职务的DepartmentPositionInfo对象</returns>
        DepartmentPositionInfo Get(int id);

        /// <summary>
        /// 获取所有的职务
        /// </summary>
        /// <returns>所有职务的列表</returns>
        IList<DepartmentPositionInfo> GetAll();


        /// <summary>
        /// 创建新的职务
        /// </summary>
        /// <param name="departmentPosition">描述要创建的职务的DepartmentPositionInfo对象</param>
        void Create(DepartmentPositionInfo departmentPosition);


        /// <summary>
        /// 更新职务信息
        /// </summary>
        /// <param name="departmentPosition">要更新的职务的信息</param>
        void Update(DepartmentPositionInfo departmentPosition);

        /// <summary>
        /// 删除指定ID的职务
        /// </summary>
        /// <param name="id">要删除的职务的ID</param>
        void Delete(int id);


        //IList<DepartmentPositionInfo> GetDeparmentPositions(int departmentId);

        /// <summary>
        /// 获取指定ID的员工的职务信息
        /// </summary>
        /// <param name="userId">员工的ID</param>
        /// <returns>描述员工职务的EmployeePositionInfo对象</returns>
        IList<EmployeePositionInfo> GetEmployeePositions(int userId);

        /// <summary>
        /// 取消指定ID的员工的指定ID的职务
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <param name="departmentPositionId">职务ID</param>
        /// <param name="departmentId">职务所属的部门</param>
        void RemoveEmployeePosition(int userId, int departmentPositionId, int departmentId);

        /// <summary>
        /// 将指定ID的职务授予指定ID的员工
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <param name="departmentPositionId">职务ID</param>
        /// <param name="departmentId">职务所属的部门</param>
        /// <param name="isManager">是否是经理职务</param>
        /// <param name="isActing">是否是临时职务</param>
        void AddEmployeePosition(int userId, int departmentPositionId, int departmentId, bool isManager, bool isActing);

        /// <summary>
        /// 获取指定ID的部门可用的职务
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="isOnlyPrivate">是否只返回部门私有职务</param>
        /// <returns>职务列表</returns>
        IList<DepartmentPositionInfo> GetByDepartment(int departmentId, bool isOnlyPrivate);

        /// <summary>
        /// 获取员工职位信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="departmentPositionId">职位ID</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns>员工职位信息</returns>
        EmployeePositionInfo GetEmployeePosition(int employeeId, int departmentPositionId, int departmentId);

        /// <summary>
        /// 更新员工职位
        /// </summary>
        /// <param name="employeePosition">要更新的员工职位信息</param>
        void UpdateEmployeePosition(EmployeePositionInfo employeePosition);

        /// <summary>
        /// 根据职务代码获取职务信息
        /// </summary>
        /// <param name="code">职务代码</param>
        /// <returns>职务信息</returns>
        DepartmentPositionInfo GetByCode(string code);

        /// <summary>
        /// 检查职务代码是否已经存在
        /// </summary>
        /// <param name="code">职务代码</param>
        /// <returns>存在则返回 true，否则返回 false</returns>
        bool CodeExists(string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        DepartmentPositionInfo GetByName(int departmentId, string name);
    }
}
