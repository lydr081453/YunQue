

using System;
using ESP.Framework.Entity;
using System.Collections.Generic;
using ESP.Framework.BusinessLogic;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 员工数据抽象访问类
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IEmployeeDataProvider
    {
        /// <summary>
        /// 获取指定ID的员工信息
        /// </summary>
        /// <param name="id">员工ID</param>
        /// <returns>员工信息</returns>
        EmployeeInfo Get(int id);

        /// <summary>
        /// 获取指定员工编号的员工信息
        /// </summary>
        /// <param name="code">员工编号</param>
        /// <returns>员工信息</returns>
        EmployeeInfo GetByCode(string code);

        /// <summary>
        /// 获取指定员工编号的员工信息
        /// </summary>
        /// <param name="code">员工编号</param>
        /// <returns>员工信息</returns>
        EmployeeInfo GetByMobile(string mobile);
        /// <summary>
        /// 获取所有的员工信息
        /// </summary>
        /// <returns>所有员工信息的列表</returns>
        IList<EmployeeInfo> GetAll();

        /// <summary>
        /// 获取指定类型的员工列表
        /// </summary>
        /// <param name="typeId">员工类型ID</param>
        /// <returns>员工列表</returns>
        IList<EmployeeInfo> GetByType(int typeId);

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="employee">员工信息</param>
        EmployeeCreateStatus Update(EmployeeInfo employee);

        /// <summary>
        /// 删除指定ID的员工信息
        /// </summary>
        /// <param name="id">要删除的员工信息的ID</param>
        void Delete(int id);

        /// <summary>
        /// 生成员工代码
        /// </summary>
        /// <param name="pattern">员工代码的生成模式，其中由"{"和"}"界定的令牌会被替换个时间或自动编号。</param>
        /// <param name="numberType">编号类型。</param>
        /// <returns>员工代码</returns>
        string CreateEmployeeCode(string pattern, string numberType);

        /// <summary>
        /// 创建新员工，同时创建关联用户
        /// </summary>
        /// <param name="userName">用户名</param>
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
        /// 如果员工类型ID无效，返回 EmployeeCreateStatus.InvalidTypeId;
        /// 如果员工代码无效，返回 EmployeeCreateStatus.InvalidCode;
        /// 如果员工代码已经存在，返回 EmployeeCreateStatus.DuplicateCode;
        /// 如果发生未知错误，返回 EmployeeCreateStatus.ProviderError;
        /// </returns>
        EmployeeCreateStatus Create(string userName, string firstNameCN, string lastNameCN,
            string firstNameEN, string lastNameEN, string email, bool isApproved, string password, EmployeeInfo employee);

        /// <summary>
        /// 查询符合相关条件的员工记录，并按指定的排序规则排序后分页返回
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">要返回的页码，索引从0开始</param>
        /// <param name="orderBy">
        /// 排序规则（对用户表中列的引用须使用u.前缀，
        /// 对员工表中列的引用须使用e.前缀，
        /// 如 “u.UserName ASC” 即按UserName列升序排列）
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
        IList<EmployeeInfo> Search(int pageSize, int pageIndex, string orderBy, string where, DbDataParameter[] paras);

        /// <summary>
        /// 获取职工类型列表
        /// </summary>
        /// <returns>职工类型列表，Key为类型ID，Value为类型名称</returns>
        IDictionary<int, string> GetTypes();

        /// <summary>
        /// 添加职工类型
        /// </summary>
        /// <param name="type">类型名称</param>
        /// <returns>类型ID</returns>
        int AddType(string type);

        /// <summary>
        /// 删除职工类型
        /// </summary>
        /// <param name="type">要删除的类型名称</param>
        void RemoveType(string type);

        /// <summary>
        /// 删除职工类型
        /// </summary>
        /// <param name="typeId">要删除的类型ID</param>
        void RemoveType(int typeId);

        /// <summary>
        /// 获得职工类型
        /// </summary>
        /// <param name="typeId">类型ID</param>
        /// <returns>类型名称</returns>
        string GetTypeName(int typeId);

        /// <summary>
        /// 获取所有员工的列表，并以字典返回
        /// </summary>
        /// <returns>所有员工的字典列表</returns>
        IDictionary<int, EmployeeInfo> GetDictionary();


        /// <summary>
        /// 根据部门ID获取员工列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>员工列表</returns>
        IList<EmployeeInfo> GetEmployeesByDepartment(int departmentId);

        /// <summary>
        /// getting employees count by department ID
        /// </summary>
        /// <param name="departmentId">dept Id</param>
        /// <returns>count</returns>
        int GetEmployeeCountByDepartment(int departmentId);

        /// <summary>
        /// 搜索员工，并分页返回记录集合
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="pageIndex">要获取的记录的页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="recordCount">总记录数，用于计算总页数</param>
        /// <returns>匹配的员工列表</returns>
        IList<EmployeeInfo> Search(string keyword, int pageIndex, int pageSize, out int recordCount);

        /// <summary>
        /// 根据中文名字搜索员工，并分页返回记录集合
        /// </summary>
        /// <param name="name">关键字</param>
        /// <param name="pageIndex">要获取的记录的页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="recordCount">总记录数，用于计算总页数</param>
        /// <returns>匹配的员工列表</returns>
        IList<EmployeeInfo> SearchByChineseName(string name, int pageIndex, int pageSize, out int recordCount);
    }
}
