using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess;
using System;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 部门数据访问类
    /// </summary>
    public class DepartmentDataProvider : IDepartmentDataProvider
    {
        #region members of IDepartmentDataProvider

        /// <summary>
        /// 获取指定ID的部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>部门信息</returns>
        public ESP.Framework.Entity.DepartmentInfo Get(int id)
        {
            string sql = @"
---------------------------------------------------------
SELECT d.*,
    ISNULL(dt.DepartmentTypeName, '') AS DepartmentTypeName
FROM sep_Departments d
    LEFT JOIN sep_DepartmentTypes dt
        ON d.DepartmentTypeID = dt.DepartmentTypeID
WHERE d.DepartmentID = @DepartmentID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, id);
            return CBO.LoadObject<DepartmentInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取所有部门的信息列表
        /// </summary>
        /// <returns>所有部门列表</returns>
        public IList<DepartmentInfo> GetAll()
        {
            string sql = @"
---------------------------------------------------------
SELECT d.*,
    ISNULL(dt.DepartmentTypeName, '') AS DepartmentTypeName
FROM sep_Departments d
    LEFT JOIN sep_DepartmentTypes dt
        ON d.DepartmentTypeID = dt.DepartmentTypeID
WHERE d.departmentname not like '%作废%'
ORDER BY d.ParentID ASC, d.Ordinal ASC, d.DepartmentLevel ASC, d.DepartmentID ASC
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<DepartmentInfo>(db.ExecuteReader(cmd));
        }


        /// <summary>
        /// 检查部门代码是否已经存在
        /// </summary>
        /// <param name="code">部门代码</param>
        /// <returns>存在则返回 true，否则返回 false</returns>
        public bool CodeExists(string code)
        {
            string sql = @"
---------------------------------------------------------
SELECT DepartmentCode 
FROM sep_Departments
WHERE DepartmentCode = @DepartmentCode
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object obj = db.ExecuteScalar(cmd);
            
            if(obj == null || (obj is DBNull))
                return false;
            return true;
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="department">新的部门信息</param>
        public void Update(DepartmentInfo department)
        {
            // TODO: 检查RowVersion
            string sql = @"
---------------------------------------------------------
UPDATE sep_Departments
   SET [DepartmentName] = @DepartmentName
      ,[DepartmentCode] = @DepartmentCode
      ,[Description] = @Description
      ,[ParentID] = @ParentID
      ,[DepartmentLevel] = @DepartmentLevel
      ,[DepartmentTypeID] = @DepartmentTypeID
      ,[Ordinal] = @Ordinal
      ,[Status] = @Status
      ,[SalaryAmount]=@SalaryAmount
 WHERE DepartmentID=@DepartmentID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, department.DepartmentID);
            db.AddInParameter(cmd, "DepartmentName", DbType.String, department.DepartmentName);
            db.AddInParameter(cmd, "DepartmentCode", DbType.String, department.DepartmentCode);
            db.AddInParameter(cmd, "Description", DbType.String, department.Description);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, department.ParentID);
            db.AddInParameter(cmd, "DepartmentLevel", DbType.Int32, department.DepartmentLevel);
            db.AddInParameter(cmd, "DepartmentTypeID", DbType.Int32, department.DepartmentTypeID);
            db.AddInParameter(cmd, "Ordinal", DbType.Int32, department.Ordinal);
            db.AddInParameter(cmd, "Status", DbType.Int32, department.Status);
            db.AddInParameter(cmd, "SalaryAmount", DbType.Decimal, department.SalaryAmount);
            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 创建新的部门
        /// </summary>
        /// <param name="department">要创建的部门的信息</param>
        public void Create(DepartmentInfo department)
        {
            string sql = @"
---------------------------------------------------------
INSERT INTO sep_Departments
           ([DepartmentName]
           ,[DepartmentCode]
           ,[Description]
           ,[ParentID]
           ,[DepartmentLevel]
           ,[DepartmentTypeID]
           ,[Ordinal]
           ,[Status]
           ,[SalaryAmount])
     VALUES
           (@DepartmentName
           ,@DepartmentCode
           ,@Description
           ,@ParentID
           ,@DepartmentLevel
           ,@DepartmentTypeID
           ,@Ordinal
           ,@Status
           ,@SalaryAmount)
SELECT CAST(SCOPE_IDENTITY() AS int)
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentName", DbType.String, department.DepartmentName);
            db.AddInParameter(cmd, "DepartmentCode", DbType.String, department.DepartmentCode);
            db.AddInParameter(cmd, "Description", DbType.String, department.Description);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, department.ParentID);
            db.AddInParameter(cmd, "DepartmentLevel", DbType.Int32, department.DepartmentLevel);
            db.AddInParameter(cmd, "DepartmentTypeID", DbType.Int32, department.DepartmentTypeID);
            db.AddInParameter(cmd, "Ordinal", DbType.Int32, department.Ordinal);
            db.AddInParameter(cmd, "Status", DbType.Int32, department.Status);
            db.AddInParameter(cmd, "SalaryAmount", DbType.Decimal, department.SalaryAmount);
            int newDepID = (int)db.ExecuteScalar(cmd);

            department.DepartmentID = newDepID;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门的ID</param>
        public void Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Departments_Delete", id);
            db.ExecuteNonQuery(cmd);
            object ret = cmd.Parameters[0].Value;
            if (!int.Equals(ret, 0))
                throw new ESP.Framework.BusinessLogic.UnknownSqlException();
        }

        /// <summary>
        /// 获取指定ID的部门的所有直接子级部门的信息列表
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>所有直接子级部门的信息列表</returns>
        public IList<DepartmentInfo> GetChildren(int id)
        {
            string sql = @"
---------------------------------------------------------
SELECT d.*,
    ISNULL(dt.DepartmentTypeName, '') AS DepartmentTypeName
FROM sep_Departments d
    LEFT JOIN sep_DepartmentTypes dt
        ON d.DepartmentTypeID = dt.DepartmentTypeID
WHERE d.ParentID = @DepartmentID and d.departmentname not like '%作废%' order by ordinal
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, id);
            return CBO.LoadList<DepartmentInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取部门所有子孙部门
        /// </summary>
        /// <param name="parentId">部门ID</param>
        /// <returns>所有部门列表</returns>
        public IList<DepartmentInfo> GetChildrenRecursion(int parentId)
        {
            string sql = @"
---------------------------------------------------------
WITH CTE
AS
(
	SELECT DepartmentID FROM sep_Departments WHERE ParentID = @ParentID and departmentname not like '%作废%'
UNION ALL
	SELECT d.DepartmentID FROM sep_Departments d
		JOIN CTE c ON c.DepartmentID=d.ParentID WHERE d.departmentname not like '%作废%'
)
SELECT d.*,
    ISNULL(dt.DepartmentTypeName, '') AS DepartmentTypeName
FROM sep_Departments d
    LEFT JOIN sep_DepartmentTypes dt
        ON d.DepartmentTypeID = dt.DepartmentTypeID
	JOIN CTE c
		ON d.DepartmentID = c.DepartmentID WHERE d.departmentname not like '%作废%'
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, parentId);
            return CBO.LoadList<DepartmentInfo>(db.ExecuteReader(cmd));
        }


        public IList<DepartmentInfo> GetChildrenRecursionByDesc(int descAreaId)
        {
            string sql = @"
---------------------------------------------------------
WITH CTE
AS
(
	SELECT DepartmentID FROM sep_Departments WHERE  Description=@Description and DepartmentLevel=3 and departmentname not like '%作废%'
UNION ALL
	SELECT d.DepartmentID FROM sep_Departments d
		JOIN CTE c ON c.DepartmentID=d.ParentID WHERE d.departmentname not like '%作废%'
)
SELECT d.*,
    ISNULL(dt.DepartmentTypeName, '') AS DepartmentTypeName
FROM sep_Departments d
    LEFT JOIN sep_DepartmentTypes dt
        ON d.DepartmentTypeID = dt.DepartmentTypeID
	JOIN CTE c
		ON d.DepartmentID = c.DepartmentID WHERE d.departmentname not like '%作废%'
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Description", DbType.String, descAreaId.ToString());
            return CBO.LoadList<DepartmentInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取指定名称的部门信息
        /// </summary>
        /// <param name="name">部门名称</param>
        /// <returns>部门信息</returns>
        public IList<DepartmentInfo> Get(string name)
        {
            string sql = @"
---------------------------------------------------------
SELECT d.*,
    ISNULL(dt.DepartmentTypeName, '') AS DepartmentTypeName
FROM sep_Departments d
    LEFT JOIN sep_DepartmentTypes dt
        ON d.DepartmentTypeID = dt.DepartmentTypeID
WHERE d.DepartmentName = @DepartmentName
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentName", DbType.String, name);
            return CBO.LoadList<DepartmentInfo>(db.ExecuteReader(cmd));
        }



        /// <summary>
        /// 根据部门代码获取部门信息
        /// </summary>
        /// <param name="code">部门代码</param>
        /// <returns>部门信息</returns>
        public DepartmentInfo GetByCode(string code)
        {
            string sql = @"
---------------------------------------------------------
SELECT d.*,
    ISNULL(dt.DepartmentTypeName, '') AS DepartmentTypeName
FROM sep_Departments d
    LEFT JOIN sep_DepartmentTypes dt
        ON d.DepartmentTypeID = dt.DepartmentTypeID
WHERE d.DepartmentCode = @DepartmentCode
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentCode", DbType.String, code);
            return CBO.LoadObject<DepartmentInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 根据部门获取员工
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>员工列表</returns>
        public IList<EmployeeInfo> GetEmployeesByDepartment(int departmentId)
        {
            string sql = @"
---------------------------------------------------------
SELECT e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
FROM sep_Employees e
    JOIN sep_Users u ON u.UserID = e.UserID AND ISNULL(u.IsDeleted,0) = 0
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
    JOIN sep_EmployeesInPositions ep ON ep.UserID=e.UserID
WHERE ep.DepartmentID=@DepartmentID AND ISNULL(u.IsDeleted,0)=0
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentId);
            return CBO.LoadList<EmployeeInfo>(db.ExecuteReader(cmd));

        }

        #endregion
    }
}
