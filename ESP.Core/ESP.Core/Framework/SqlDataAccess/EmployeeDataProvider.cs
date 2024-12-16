using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.BusinessLogic;
using ESP.Configuration;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 员工数据访问类
    /// </summary>
    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        #region members of IEmployeeDataProvider

        /// <summary>
        /// 获取指定ID的员工信息
        /// </summary>
        /// <param name="id">员工ID</param>
        /// <returns>员工信息</returns>
        public ESP.Framework.Entity.EmployeeInfo Get(int id)
        {
            string sql = @"
SELECT e.*, et.TypeName, u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e 
    JOIN sep_Users u ON u.UserID = e.UserID 
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
WHERE e.UserID=@UserID
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, id);
            IDataReader reader = db.ExecuteReader(cmd);
            return CBO.LoadObject<EmployeeInfo>(reader);
        }

        /// <summary>
        /// 获取所有的员工信息
        /// </summary>
        /// <returns>所有员工信息的列表</returns>
        public IList<EmployeeInfo> GetAll()
        {
            string sql = @"
SELECT e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e
    JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            IDataReader reader = db.ExecuteReader(cmd);
            return CBO.LoadList<EmployeeInfo>(reader);
        }

        /// <summary>
        /// 获取指定员工编号的员工信息
        /// </summary>
        /// <param name="code">员工编号</param>
        /// <returns>员工信息</returns>
        public EmployeeInfo GetByCode(string code)
        {
            string sql = @"
SELECT e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e
    JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
WHERE e.Code=@Code
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Code", DbType.String, code);
            IDataReader reader = db.ExecuteReader(cmd);
            return CBO.LoadObject<EmployeeInfo>(reader);
        }

        public EmployeeInfo GetByMobile(string mobile)
        {
            string sql = @"
SELECT e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e
    JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
WHERE replace(e.MobilePhone,'-','')=@MobilePhone and e.Status in(1,3)
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "MobilePhone", DbType.String, mobile);
            IDataReader reader = db.ExecuteReader(cmd);
            return CBO.LoadObject<EmployeeInfo>(reader);
        }

        /// <summary>
        /// 获取指定类型的员工列表
        /// </summary>
        /// <param name="typeId">员工类型ID</param>
        /// <returns>员工列表</returns>
        public IList<EmployeeInfo> GetByType(int typeId)
        {
            string sql = @"
SELECT e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e
    JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
WHERE e.TypeID=@TypeID
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "TypeID", DbType.Int32, typeId);
            IDataReader reader = db.ExecuteReader(cmd);
            return CBO.LoadList<EmployeeInfo>(reader);
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="employee">员工信息</param>
        public EmployeeCreateStatus Update(ESP.Framework.Entity.EmployeeInfo employee)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Employees_Update",
                employee.UserID, employee.Code, employee.TypeID,
                employee.Phone1, employee.Phone2, employee.MobilePhone, employee.HomePhone, employee.Fax, employee.InternalEmail, employee.IM,
                employee.EmergencyContact, employee.EmergencyContactPhone,
                employee.Address, employee.City, employee.Province, employee.Country, employee.PostCode,
                employee.Address2, employee.City2, employee.Province2, employee.Country2, employee.PostCode2,
                employee.WorkAddress, employee.WorkCity, employee.WorkProvince, employee.WorkCountry, employee.WorkPostCode,
                employee.MaritalStatus, employee.Gender, employee.Birthday, employee.BirthPlace, employee.DomicilePlace, employee.IDNumber, employee.Photo,
                employee.Degree, employee.Education, employee.GraduateFrom, employee.Major, employee.GraduatedDate,
                employee.Health, employee.DiseaseInSixMonths, employee.DiseaseInSixMonthsInfo,
                employee.WorkExperience, employee.WorkSpecialty, employee.ThisYearSalary,
                employee.Status, employee.Memo,employee.Resume, employee.BaseInfoOK, employee.ContractInfoOK, employee.InsuranceInfoOK, employee.ArchiveInfoOK,
                employee.LastModifier, employee.LastModifiedTime);
            
            db.ExecuteNonQuery(cmd); 

            int errorCode = (int)cmd.Parameters[0].Value;
            if (errorCode == 0)
            {
                return EmployeeCreateStatus.Success;
            }
            else if (errorCode == -101)
            {
                return EmployeeCreateStatus.InvalidTypeID;
            }
            else if (errorCode == -102)
            {
                return EmployeeCreateStatus.DuplicateCode;
            }
            else
            {
                return EmployeeCreateStatus.ProviderError;
            }

        }

        /// <summary>
        /// 删除指定ID的员工信息
        /// </summary>
        /// <param name="id">要删除的员工信息的ID</param>
        public void Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand("UPDATE sep_Users SET IsDeleted=1 WHERE UserID=" + id);
            db.ExecuteNonQuery(cmd);

        }

        /// <summary>
        /// 生成员工代码
        /// </summary>
        /// <param name="pattern">员工代码的生成模式，其中由"{"和"}"界定的令牌会被替换个时间或自动编号。</param>
        /// <param name="numberType">编号类型。</param>
        /// <returns>员工代码</returns>
        public string CreateEmployeeCode(string pattern, string numberType)
        {
            return CreateCode(pattern, numberType);
        }

        private string CreateCode(string pattern, string numberType)
        {
            StringBuilder sb = new StringBuilder(pattern.Length);
            int start = 0;
            int end = -1;
            int spiltterIndex = -1;

            while (true)
            {
                end++;
                start = pattern.IndexOf('{', end);
                if (start < 0)
                {
                    sb.Append(pattern.Substring(end));
                    break;
                }
                if (start > end)
                    sb.Append(pattern.Substring(end, start - end));

                start++;
                end = pattern.IndexOf('}', start + 1);
                if (end < 0)
                {
                    break;
                }

                int len = end - start;
                if (len == 0 || pattern[start] == ':')
                    continue;

                spiltterIndex = pattern.IndexOf(':', start, len);
                string token = spiltterIndex < 0 ? pattern.Substring(start, len) : pattern.Substring(start, spiltterIndex - start);
                string fmt = spiltterIndex < 0 ? string.Empty : pattern.Substring(spiltterIndex + 1, end - spiltterIndex - 1);

                token = token.Trim();
                fmt = fmt.Trim();

                if (token.Length == 0)
                    continue;

                if (fmt.Length == 0)
                    fmt = null;

                switch (token)
                {
                    case "date":
                        {
                            try
                            {
                                string s = (fmt == null) ? DateTime.Now.ToString() : DateTime.Now.ToString(fmt);
                                sb.Append(s);
                            }
                            catch
                            {
                            }
                        }
                        break;
                    case "autono":
                        {
                            try
                            {
                                long no = GetDBAutoNumber(numberType);
                                if (no < 0)
                                    continue;
                                string s = (fmt == null) ? no.ToString() : no.ToString(fmt);
                                sb.Append(s);
                            }
                            catch
                            {
                            }
                        }
                        break;
                }
            }
            return sb.ToString();
        }

        private long GetDBAutoNumber(string numberType)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_AutoNumbers_Generate", numberType, -1);
            db.ExecuteNonQuery(cmd);
            int errorCode = (int)cmd.Parameters[0].Value;
            long number = (long)db.GetParameterValue(cmd, "Value");
            if (errorCode != 0)
                return -1;
            return number;
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
        public EmployeeCreateStatus Create(string username, string firstNameCN, string lastNameCN,
            string firstNameEN, string lastNameEN, string email, bool isApproved, string password, EmployeeInfo employee)
        {
            EmployeeCreateStatus status = EmployeeCreateStatus.Success;

            UserCreateStatus code = UserDataProvider.CheckCreateUserParameters(username, email, password);

            if ((code & UserCreateStatus.InvalidUserName) == UserCreateStatus.InvalidUserName)
                status |= EmployeeCreateStatus.InvalidUserName;
            if ((code & UserCreateStatus.InvalidPassword) == UserCreateStatus.InvalidPassword)
                status |= EmployeeCreateStatus.InvalidPassword;
            if ((code & UserCreateStatus.InvalidEmail) == UserCreateStatus.InvalidEmail)
                status |= EmployeeCreateStatus.InvalidEmail;

            if (string.IsNullOrEmpty(employee.Code))
                status |= EmployeeCreateStatus.InvalidCode;

            if (status != EmployeeCreateStatus.Success)
                return status;

            byte[] passwordSaltBytes = UserDataProvider.GenerateSalt();
            string passwordSalt = Convert.ToBase64String(passwordSaltBytes);
            string encPassword = UserDataProvider.EncodePassword(password, passwordSaltBytes);
            int errorCode = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetStoredProcCommand("sep_Employees_Create",
                    username, firstNameCN, lastNameCN, firstNameEN, lastNameEN, email,
                    isApproved, password, passwordSalt, ConfigurationManager.IsUniqueEmailRequired,
                    employee.UserID, employee.Code, employee.TypeID,
                    employee.Phone1, employee.Phone2, employee.MobilePhone, employee.HomePhone, employee.Fax, employee.InternalEmail, employee.IM,
                    employee.EmergencyContact, employee.EmergencyContactPhone,
                    employee.Address, employee.City, employee.Province, employee.Country, employee.PostCode,
                    employee.Address2, employee.City2, employee.Province2, employee.Country2, employee.PostCode2,
                    employee.WorkAddress, employee.WorkCity, employee.WorkProvince, employee.WorkCountry, employee.WorkPostCode,
                    employee.MaritalStatus, employee.Gender, employee.Birthday, employee.BirthPlace, employee.DomicilePlace, employee.IDNumber, employee.Photo,
                    employee.Degree, employee.Education, employee.GraduateFrom, employee.Major, employee.GraduatedDate,
                    employee.Health, employee.DiseaseInSixMonths, employee.DiseaseInSixMonthsInfo,
                    employee.WorkExperience, employee.WorkSpecialty, employee.ThisYearSalary,
                    employee.Status, employee.Memo,employee.Resume, employee.BaseInfoOK, employee.ContractInfoOK, employee.InsuranceInfoOK, employee.ArchiveInfoOK,
                    employee.LastModifier, employee.LastModifiedTime);

                CBO.FillObject<EmployeeInfo>(db.ExecuteReader(cmd), ref employee);
                errorCode = (int)cmd.Parameters[0].Value;
            }
            catch
            {
                errorCode = -999;
            }

            if (errorCode == 0)
            {
                return EmployeeCreateStatus.Success;
            }
            else if (errorCode == -1)
            {
                return EmployeeCreateStatus.DuplicateUserName;
            }
            else if (errorCode == -2)
            {
                return EmployeeCreateStatus.DuplicateEmail;
            }
            else if (errorCode == -101)
            {
                return EmployeeCreateStatus.InvalidTypeID;
            }
            else if (errorCode == -102)
            {
                return EmployeeCreateStatus.DuplicateCode;
            }
            else
            {
                return EmployeeCreateStatus.ProviderError;
            }
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
        public IList<EmployeeInfo> Search(int pageSize, int pageIndex, string orderBy, string where, DbDataParameter[] paras)
        {
            if (orderBy == null || orderBy.Length == 0)
                orderBy = "u.UserID ASC";
            if (where == null || where.Length == 0)
                where = "0=0";
            string sql = @"
;WITH t AS(
    SELECT e.UserID,
        (ROW_NUMBER() OVER (ORDER BY " + orderBy + @") - 1) AS RowIndex 
    FROM sep_Employees e
        JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    WHERE " + where + @")
SELECT e.*, et.TypeName, u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e 
    JOIN t ON e.UserID=t.UserID 
    JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
WHERE RowIndex >= @_PageIndex * @_PageSize AND RowIndex < @_PageIndex * @_PageSize + @_PageSize
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "_PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "_PageSize", DbType.Int32, pageSize);
            if (paras != null)
            {
                foreach (DbDataParameter p in paras)
                {
                    db.AddInParameter(cmd, p.Name, p.DbType, p.Value);
                }
            }

            return CBO.LoadList<EmployeeInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取职工类型列表
        /// </summary>
        /// <returns>职工类型列表，Key为类型ID，Value为类型名称</returns>
        public IDictionary<int, string> GetTypes()
        {
            IDictionary<int, string> dic = new Dictionary<int, string>();
            string sql = @"SELECT * FROM sep_EmployeeTypes";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            IDataReader reader = db.ExecuteReader(cmd);

            int idIndex = reader.GetOrdinal("TypeID");
            int nameIndex = reader.GetOrdinal("TypeName");
            while (reader.Read())
            {
                dic.Add(reader.GetInt32(idIndex), reader.GetString(nameIndex));
            }
            return dic;
        }

        /// <summary>
        /// 获得职工类型
        /// </summary>
        /// <param name="typeId">类型ID</param>
        /// <returns>类型名称</returns>
        public string GetTypeName(int typeId)
        {
            string sql = @"SELECT TypeName FROM sep_EmployeeTypes WHERE TypeID = @TypeID";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "TypeID", DbType.String, typeId);
            return db.ExecuteScalar(cmd).ToString();
        }

        /// <summary>
        /// 添加职工类型
        /// </summary>
        /// <param name="type">类型名称</param>
        /// <returns>类型ID</returns>
        public int AddType(string type)
        {
            string sql = @"INSERT INTO sep_EmployeeTypes (TypeName) VALUES (@TypeName); SELECT CAST(SCOPE_IDENTITY() AS INT) ";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "TypeName", DbType.String, type);
            return (int)db.ExecuteScalar(cmd);
        }


        /// <summary>
        /// 删除职工类型
        /// </summary>
        /// <param name="type">要删除的类型名称</param>
        public void RemoveType(string type)
        {
            string sql = @"DELETE sep_EmployeeTypes WHERE TypeName = @TypeName";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "TypeName", DbType.String, type);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除职工类型
        /// </summary>
        /// <param name="typeId">要删除的类型ID</param>
        public void RemoveType(int typeId)
        {
            string sql = @"DELETE sep_EmployeeTypes WHERE TypeID = @TypeID";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "TypeID", DbType.String, typeId);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取所有员工的列表，并以字典返回
        /// </summary>
        /// <returns>所有员工的字典列表</returns>
        public IDictionary<int, EmployeeInfo> GetDictionary()
        {
            string sql = @"
SELECT e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e
    JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);

            IDictionary<int, EmployeeInfo> dic = new Dictionary<int, EmployeeInfo>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                object obj = null;
                while (reader.Read())
                {
                    EmployeeInfo e = CBO.LoadObject<EmployeeInfo>(reader, ref obj);
                    dic.Add(e.UserID, e);
                }
            }

            return dic;
        }

        /// <summary>
        /// 根据部门ID获取员工列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>员工列表</returns>
        public IList<EmployeeInfo> GetEmployeesByDepartment(int departmentId)
        {
            string sql = @"
SELECT e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e
    JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
    JOIN sep_EmployeesInPositions ep ON ep.UserID=e.UserID
WHERE e.status in(1,3) and ep.DepartmentID=@DepartmentID AND ISNULL(u.IsDeleted,0)=0

";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentId);
            return CBO.LoadList<EmployeeInfo>(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// getting Employees count by department ID
        /// </summary>
        /// <param name="departmentId">dept ID</param>
        /// <returns>Count</returns>
        public int GetEmployeeCountByDepartment(int departmentId)
        {
            string sql = @"select count(*) from sep_Employees a join sep_EmployeesInPositions b on a.UserID =b.UserID 
join V_Department c on b.DepartmentID=c.level3Id
where Status in(1,3) and (c.level3Id= @departmentId  or c.level2Id=@departmentId or c.level1Id=@departmentId)";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "departmentId", DbType.Int32, departmentId);
            return (int)db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 搜索员工，并分页返回记录集合
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="pageIndex">要获取的记录的页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="recordCount">总记录数，用于计算总页数</param>
        /// <returns>匹配的员工列表</returns>
        public IList<EmployeeInfo> Search(string keyword, int pageIndex, int pageSize, out int recordCount)
        {
            string sql = @"
WITH t AS 
(
	SELECT
		ROW_NUMBER() OVER (ORDER BY UserID ASC) AS RowIndex, * 
	FROM [V_EmpSearch] WHERE EmpInfo LIKE '%' + @Keyword + '%' AND ISNULL(IsDeleted,0)=0
)
SELECT * FROM t 
WHERE RowIndex > @PageIndex * @PageSize AND RowIndex <= @PageIndex * @PageSize + @PageSize

SELECT @RecordCount = COUNT(*)
FROM [V_EmpSearch] WHERE EmpInfo LIKE '%' + @Keyword + '%'
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Keyword", DbType.String, keyword);
            db.AddInParameter(cmd, "PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "PageSize", DbType.Int32, pageSize);
            db.AddOutParameter(cmd, "RecordCount", DbType.Int32, 0);
            IList<EmployeeInfo> list = CBO.LoadList<EmployeeInfo>(db.ExecuteReader(cmd));
            recordCount = (int)db.GetParameterValue(cmd, "RecordCount");
            return list;
        }


        /// <summary>
        /// 根据中文名字搜索员工，并分页返回记录集合
        /// </summary>
        /// <param name="name">关键字</param>
        /// <param name="pageIndex">要获取的记录的页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="recordCount">总记录数，用于计算总页数</param>
        /// <returns>匹配的员工列表</returns>
        public IList<EmployeeInfo> SearchByChineseName(string name, int pageIndex, int pageSize, out int recordCount)
        {
            string sql = @"
WITH t AS 
(
    SELECT ROW_NUMBER() OVER (ORDER BY e.UserID ASC) AS RowIndex, 
        e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
    FROM sep_Employees e
        JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
        LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
    WHERE (u.LastNameCN + u.FirstNameCN) LIKE '%' + @NameKeyword + '%' AND ISNULL(u.IsDeleted,0)=0
)
SELECT * FROM t 
WHERE RowIndex > @PageIndex * @PageSize AND RowIndex <= @PageIndex * @PageSize + @PageSize

SELECT @RecordCount = COUNT(*)
FROM sep_Employees e
        JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    WHERE (u.LastNameCN + u.FirstNameCN) LIKE '%' + @NameKeyword + '%'
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "NameKeyword", DbType.String, name);
            db.AddInParameter(cmd, "PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "PageSize", DbType.Int32, pageSize);
            db.AddOutParameter(cmd, "RecordCount", DbType.Int32, 0);
            IList<EmployeeInfo> list = CBO.LoadList<EmployeeInfo>(db.ExecuteReader(cmd));
            recordCount = (int)db.GetParameterValue(cmd, "RecordCount");
            return list;
        }

        #endregion
    }
}
