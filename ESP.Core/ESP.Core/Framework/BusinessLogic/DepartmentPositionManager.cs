
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 部门职位控制类
    /// </summary>
    public static class DepartmentPositionManager
    {
        #region Private Members
        private static IDepartmentPositionDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IDepartmentPositionDataProvider>.Instance;
        }

        #endregion

        /// <summary>
        /// 获取指定ID的部门职务
        /// </summary>
        /// <param name="id">职务ID</param>
        /// <returns>描述职务的DepartmentPositionInfo对象</returns>
        public static ESP.Framework.Entity.DepartmentPositionInfo Get(int id)
        {
            return GetProvider().Get(id);
        }

        /// <summary>
        /// 获取所有的职务
        /// </summary>
        /// <returns>所有职务的列表</returns>
        public static IList<DepartmentPositionInfo> GetAll()
        {
            return GetProvider().GetAll();
        }

        /// <summary>
        /// 获取指定ID的部门可用的职务
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>职务列表</returns>
        public static IList<DepartmentPositionInfo> GetByDepartment(int departmentId)
        {
            return GetByDepartment(departmentId, false);
        }

        /// <summary>
        /// 获取指定ID的部门可用的职务
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="isOnlyPrivate">是否只返回部门私有职务</param>
        /// <returns>职务列表</returns>
        public static IList<DepartmentPositionInfo> GetByDepartment(int departmentId, bool isOnlyPrivate)
        {
            return GetProvider().GetByDepartment(departmentId, isOnlyPrivate);
        }

        /// <summary>
        /// 创建新的职务
        /// </summary>
        /// <param name="departmentPosition">描述要创建的职务的DepartmentPositionInfo对象</param>
        public static void Create( ESP.Framework.Entity.DepartmentPositionInfo departmentPosition)
        {
            GetProvider().Create( departmentPosition);
        }

        /// <summary>
        /// 更新职务信息
        /// </summary>
        /// <param name="departmentPosition">要更新的职务的信息</param>
        public static void Update( ESP.Framework.Entity.DepartmentPositionInfo departmentPosition)
        {
            GetProvider().Update( departmentPosition);
        }

        /// <summary>
        /// 删除指定ID的职务
        /// </summary>
        /// <param name="id">要删除的职务的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 获取指定ID的员工的职务信息
        /// </summary>
        /// <param name="userId">员工的ID</param>
        /// <returns>描述员工职务的EmployeePositionInfo对象</returns>
        public static IList<EmployeePositionInfo> GetEmployeePositions(int userId)
        {
            return GetProvider().GetEmployeePositions(userId);
        }

        /// <summary>
        /// 将指定ID的职务授予指定ID的员工
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <param name="departmentPositionId">职务ID</param>
        /// <param name="departmentId">职务所属的部门</param>
        /// <param name="isManager">是否是经理职务</param>
        /// <param name="isActing">是否是临时职务</param>
        public static void AddEmployeePosition(int userId, int departmentPositionId, int departmentId, bool isManager, bool isActing)
        {
            GetProvider().AddEmployeePosition(userId, departmentPositionId, departmentId, isManager, isActing);
        }

        /// <summary>
        /// 取消指定ID的员工的指定ID的职务
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <param name="departmentPositionId">职务ID</param>
        /// <param name="departmentId">职务所属的部门</param>
        public static void RemoveEmployeePosition(int userId, int departmentPositionId, int departmentId)
        {
            GetProvider().RemoveEmployeePosition(userId, departmentPositionId, departmentId);
        }


        /// <summary>
        /// 获取员工职位信息
        /// </summary>
        /// <param name="employeeID">员工ID</param>
        /// <param name="departmentPositionID">职位ID</param>
        /// <param name="departmentID">部门ID</param>
        /// <returns>员工职位信息</returns>
        public static EmployeePositionInfo GetEmployeePosition(int employeeID, int departmentPositionID, int departmentID)
        {
            return GetProvider().GetEmployeePosition(employeeID, departmentPositionID, departmentID);
        }

        /// <summary>
        /// 更新员工职位
        /// </summary>
        /// <param name="employeePosition">要更新的员工职位信息</param>
        public static void UpdateEmployeePosition(EmployeePositionInfo employeePosition)
        {
            GetProvider().UpdateEmployeePosition(employeePosition);
        }



        /// <summary>
        /// 根据职务代码获取职务信息
        /// </summary>
        /// <param name="code">职务代码</param>
        /// <returns>职务信息</returns>
        public static DepartmentPositionInfo GetByCode(string code)
        {
            return GetProvider().GetByCode(code);
        }

        /// <summary>
        /// 检查职务代码是否已经存在
        /// </summary>
        /// <param name="code">职务代码</param>
        /// <returns>存在则返回 true，否则返回 false</returns>
        public static bool CodeExists(string code)
        {
            return GetProvider().CodeExists(code);
        }

        /// <summary>
        /// 根据名称查询指定部门下的职务。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DepartmentPositionInfo GetByName(int departmentId, string name)
        {
            return GetProvider().GetByName(departmentId, name);
        }
    }
}
