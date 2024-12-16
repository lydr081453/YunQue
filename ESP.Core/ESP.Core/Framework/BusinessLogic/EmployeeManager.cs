
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 员工操作类
    /// </summary>
    public static class EmployeeManager
    {
        #region Private Members
        private static IEmployeeDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IEmployeeDataProvider>.Instance;
        }
        #endregion

        /// <summary>
        /// 获取指定ID的员工信息
        /// </summary>
        /// <param name="id">员工ID</param>
        /// <returns>员工信息</returns>
        public static ESP.Framework.Entity.EmployeeInfo Get(int id)
        {
            return GetProvider().Get(id);
        }

        /// <summary>
        /// 获取所有的员工信息
        /// </summary>
        /// <returns>所有员工信息的列表</returns>
        public static IList<EmployeeInfo> GetAll()
        {
            return GetProvider().GetAll();
        }

        /// <summary>
        /// 获取指定类型的员工列表
        /// </summary>
        /// <param name="typeId">员工类型ID</param>
        /// <returns>员工列表</returns>
        public static IList<EmployeeInfo> GetByType(int typeId)
        {
            return GetProvider().GetByType(typeId);
        }

        /// <summary>
        /// 查询符合相关条件的员工记录，并按指定的排序规则排序后分页返回
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">要返回的页码，索引从0开始</param>
        /// <param name="orderBy">
        /// 排序规则（对用户表中列的引用须使用u.前缀，
        /// 对员工表中列的引用须使用e.前缀，
        /// 如 “u.Username ASC” 即按Username列升序排列）
        /// </param>
        /// <param name="where">
        /// 查询条件（对Users表中列名的引用须使用u.前缀，对员工表中列的引用须使用e.前缀）
        /// </param>
        /// <param name="paras">
        /// 查询条件中要使用到的参数，对于SQL Server，这里的参数名不带前辍符号 "@"，
        /// 例如对于命名参数 @EmployeeName， 只需标识为 EmployeeName 即可。
        /// _PageIndex和_PageSize两个参数为本方法内部分页保留使用。
        /// </param>
        /// <returns>查询到的员工记录列表</returns>
        public static IList<EmployeeInfo> Search(int pageSize, int pageIndex, string orderBy, string where, DbDataParameter[] paras)
        {
            return GetProvider().Search(pageSize, pageIndex, orderBy, where, paras);
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="info">员工信息</param>
        public static void Update( ESP.Framework.Entity.EmployeeInfo info)
        {
            GetProvider().Update( info);
        }

        /// <summary>
        /// 删除指定ID的员工信息
        /// </summary>
        /// <param name="id">要删除的员工信息的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 创建新员工，同时创建关联用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="firstNameCN">中文名</param>
        /// <param name="lastNameCN">中文姓氏</param>
        /// <param name="firstNameEN">英文名</param>
        /// <param name="lastNameEN">英文姓氏</param>
        /// <param name="email">安全Email</param>
        /// <param name="isApproved">用户是否通过审核</param>
        /// <param name="password">密码</param>
        /// <param name="employee">员工信息</param>
        /// <returns>
        /// 操作错误状态
        /// 如果创建用户成功，返回 EmployeeCreateStatus.Success;
        /// 如果用户名效，返回 EmployeeCreateStatus.InvalidUserName;
        /// 如果密码无效，返回 EmployeeCreateStatus.InvalidPassword;
        /// 如果Email无效，返回 EmployeeCreateStatus.InvalidEmail;
        /// 如果用户名已经存在，返回 EmployeeCreateStatus.DuplicateUserName;
        /// 如果Email已经存在且配置了Email唯一，返回 EmployeeCreateStatus.DuplicateEmail;
        /// 如果员工类型ID无效，返回 EmployeeCreateStatus.InvalidTypeID;
        /// 如果员工代码无效，返回 EmployeeCreateStatus.InvalidCode;
        /// 如果员工代码已经存在，返回 EmployeeCreateStatus.DuplicateCode;
        /// 如果发生未知错误，返回 EmployeeCreateStatus.ProviderError;
        /// </returns>
        public static EmployeeCreateStatus Create(string username, string firstNameCN, string lastNameCN,
            string firstNameEN, string lastNameEN, string email, bool isApproved, string password,  EmployeeInfo employee)
        {
            //if (employee.Code == null || employee.Code.Length == 0)
            //    employee.Code = CreateEmployeeCode();

            return GetProvider().Create(username, firstNameCN, lastNameCN, firstNameEN, lastNameEN, email, isApproved, password,  employee);
        }

        /// <summary>
        /// 生成员工代码
        /// </summary>
        /// <returns>员工代码</returns>
        /// <remarks>
        /// 员工代码的生成根据配置文件中 ESP 节的 employeeCodePattern 生成，
        /// employeeCodePattern 中包含的由"{"和"}"声明的占位符会被自动替换
        /// </remarks>
        public static string CreateEmployeeCode()
        {
            string pattern = ESP.Configuration.ConfigurationManager.EmployeeCodePattern;
            return GetProvider().CreateEmployeeCode(pattern, "employee");
        }

        /// <summary>
        /// 生成员工代码
        /// </summary>
        /// <param name="pattern">员工代码的生成模式，其中由"{"和"}"界定的令牌会被替换个时间或自动编号。</param>
        /// <param name="numberType">编号类型。</param>
        /// <returns>员工代码</returns>
        public static string CreateEmployeeCode(string pattern, string numberType)
        {
            return GetProvider().CreateEmployeeCode(pattern, numberType);
        }


        /// <summary>
        /// 获取指定员工编号的员工信息
        /// </summary>
        /// <param name="code">员工编号</param>
        /// <returns>员工信息</returns>
        public static EmployeeInfo GetByCode(string code)
        {
            return GetProvider().GetByCode(code);
        }

        public static EmployeeInfo GetByMobile(string mobile)
        {
            return GetProvider().GetByMobile(mobile);
        }


        /// <summary>
        /// 获取职工类型列表
        /// </summary>
        /// <returns>职工类型列表，Key为类型ID，Value为类型名称</returns>
        public static IDictionary<int, string> GetTypes()
        {
            return GetProvider().GetTypes();
        }

        /// <summary>
        /// 添加职工类型
        /// </summary>
        /// <param name="type">类型名称</param>
        /// <returns>类型ID</returns>
        public static int AddType(string type)
        {
            return GetProvider().AddType(type);
        }

        /// <summary>
        /// 获得职工类型
        /// </summary>
        /// <param name="typeId">类型ID</param>
        /// <returns>类型名称</returns>
        public static string GetTypeName(int typeId)
        {
            return GetProvider().GetTypeName(typeId);
        }

        /// <summary>
        /// 删除职工类型
        /// </summary>
        /// <param name="type">要删除的类型名称</param>
        public static void RemoveType(string type)
        {
            GetProvider().RemoveType(type);
        }

        /// <summary>
        /// 删除职工类型
        /// </summary>
        /// <param name="typeId">要删除的类型ID</param>
        public static void RemoveType(int typeId)
        {
            GetProvider().RemoveType(typeId);
        }

        /// <summary>
        /// 获取所有员工的列表，并以字典返回
        /// </summary>
        /// <returns>所有员工的字典列表</returns>
        public static IDictionary<int, EmployeeInfo> GetDictionary()
        {
            return GetProvider().GetDictionary();
        }

        /// <summary>
        /// 根据部门ID获取员工列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>员工列表</returns>
        public static IList<EmployeeInfo> GetEmployeesByDepartment(int departmentId)
        {
            return GetProvider().GetEmployeesByDepartment(departmentId);
        }

        public static int GetEmployeeCountByDepartment(int departmentId)
        {
            return GetProvider().GetEmployeeCountByDepartment(departmentId);
        }


        /// <summary>
        /// 搜索员工
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>匹配的员工列表</returns>
        public static IList<EmployeeInfo> Search(string keyword)
        {
            int rc;
            return GetProvider().Search(keyword, 0, int.MaxValue, out rc);
        }


        /// <summary>
        /// 搜索员工，并分页返回记录集合
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="pageIndex">要获取的记录的页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="recordCount">总记录数，用于计算总页数</param>
        /// <returns>匹配的员工列表</returns>
        public static IList<EmployeeInfo> Search(string keyword, int pageIndex, int pageSize, out int recordCount)
        {
            return GetProvider().Search(keyword, pageIndex, pageSize, out recordCount);
        }
        
        /// <summary>
        /// 根据中文名字搜索员工
        /// </summary>
        /// <param name="name">关键字</param>
        /// <returns>匹配的员工列表</returns>
        public static IList<EmployeeInfo> SearchByChineseName(string name)
        {
            int rc;
            return GetProvider().SearchByChineseName(name, 0, int.MaxValue, out rc);
        }

        /// <summary>
        /// 根据中文名字搜索员工，并分页返回记录集合
        /// </summary>
        /// <param name="name">关键字</param>
        /// <param name="pageIndex">要获取的记录的页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="recordCount">总记录数，用于计算总页数</param>
        /// <returns>匹配的员工列表</returns>
        public static IList<EmployeeInfo> SearchByChineseName(string name, int pageIndex, int pageSize, out int recordCount)
        {
            return GetProvider().SearchByChineseName(name, pageIndex, pageSize, out recordCount);

        }

    }

    /// <summary>
    /// 员工创建结果
    /// </summary>
    public enum EmployeeCreateStatus : int
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 无效的用户名
        /// </summary>
        InvalidUserName = 1,

        /// <summary>
        /// 无效的密码
        /// </summary>
        InvalidPassword = 2,

        /// <summary>
        /// 无效的邮件
        /// </summary>
        InvalidEmail = 4,

        /// <summary>
        /// 用户名已经存在
        /// </summary>
        DuplicateUserName = 8,

        /// <summary>
        /// 邮件已经被使用
        /// </summary>
        DuplicateEmail = 16,

        /// <summary>
        /// 用户被驳回
        /// </summary>
        UserRejected = 32,

        /// <summary>
        /// 无效的员工类型
        /// </summary>
        InvalidTypeID = 64,

        /// <summary>
        /// 无效的员工编号
        /// </summary>
        InvalidCode = 128,

        /// <summary>
        /// 员工编号已经存在
        /// </summary>
        DuplicateCode = 256,

        /// <summary>
        /// 未知的数据提供程序错误
        /// </summary>
        ProviderError = -1
    }
}
