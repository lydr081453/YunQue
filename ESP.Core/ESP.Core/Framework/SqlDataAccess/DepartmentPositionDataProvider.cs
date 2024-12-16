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
    /// 部门职务数据访问类
    /// </summary>
    public class DepartmentPositionDataProvider : IDepartmentPositionDataProvider
    {
        #region members of IDepartmentPositionDataProvider

        /// <summary>
        /// 获取指定ID的部门职务
        /// </summary>
        /// <param name="id">职务ID</param>
        /// <returns>描述职务的DepartmentPositionInfo对象</returns>
        public ESP.Framework.Entity.DepartmentPositionInfo Get(int id)
        {
            string sql = @"
---------------------------------------------------------
SELECT dp.*,
    ISNULL(d.DepartmentName, '') AS DepartmentName
FROM sep_DepartmentPositions dp
    LEFT JOIN sep_Departments d
        ON d.DepartmentID = dp.DepartmentID
WHERE dp.DepartmentPositionID = @DepartmentPositionID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentPositionID", DbType.Int32, id);
            return CBO.LoadObject<DepartmentPositionInfo>(db.ExecuteReader(cmd));
        }


        /// <summary>
        /// 获取所有的职务
        /// </summary>
        /// <returns>所有职务的列表</returns>
        public IList<DepartmentPositionInfo> GetAll()
        {
            string sql = @"
---------------------------------------------------------
SELECT dp.*,
    ISNULL(d.DepartmentName, '') AS DepartmentName
FROM sep_DepartmentPositions dp
    LEFT JOIN sep_Departments d
        ON d.DepartmentID = dp.DepartmentID
ORDER BY dp.DepartmentID ASC, dp.DepartmentPositionID ASC
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<DepartmentPositionInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 更新职务信息
        /// </summary>
        /// <param name="departmentPosition">要更新的职务的信息</param>
        public void Update(ESP.Framework.Entity.DepartmentPositionInfo departmentPosition)
        {
            // TODO: 检查RowVersion
            string sql = @"
---------------------------------------------------------
UPDATE sep_DepartmentPositions
   SET [DepartmentPositionName] = @DepartmentPositionName
      ,[PositionCode] = @PositionCode
      ,[Description] = @Description
      ,[DepartmentID] = @DepartmentID
      ,[PositionLevel] = @PositionLevel
      ,PositionBaseId=@PositionBaseId
 WHERE DepartmentPositionID=@DepartmentPositionID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentPositionID", DbType.Int32, departmentPosition.DepartmentPositionID);
            db.AddInParameter(cmd, "DepartmentPositionName", DbType.String, departmentPosition.DepartmentPositionName);
            db.AddInParameter(cmd, "PositionCode", DbType.String, departmentPosition.PositionCode);
            db.AddInParameter(cmd, "Description", DbType.String, departmentPosition.Description);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentPosition.DepartmentID);
            db.AddInParameter(cmd, "PositionLevel", DbType.Int32, departmentPosition.PositionLevel);
            db.AddInParameter(cmd, "PositionBaseId", DbType.Int32, departmentPosition.PositionBaseId);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 创建新的职务
        /// </summary>
        /// <param name="departmentPosition">描述要创建的职务的DepartmentPositionInfo对象</param>
        public void Create(ESP.Framework.Entity.DepartmentPositionInfo departmentPosition)
        {
            string sql = @"
---------------------------------------------------------
INSERT INTO sep_DepartmentPositions
           ([DepartmentPositionName]
           ,[PositionCode]
           ,[Description]
           ,[DepartmentID]
           ,[PositionLevel]
           ,PositionBaseId)
     VALUES
           (@DepartmentPositionName
           ,@PositionCode
           ,@Description
           ,@DepartmentID
           ,@PositionLevel,@PositionBaseId)
SELECT CAST(SCOPE_IDENTITY() AS int)
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentPositionName", DbType.String, departmentPosition.DepartmentPositionName);
            db.AddInParameter(cmd, "PositionCode", DbType.String, departmentPosition.PositionCode);
            db.AddInParameter(cmd, "Description", DbType.String, departmentPosition.Description);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentPosition.DepartmentID);
            db.AddInParameter(cmd, "PositionLevel", DbType.Int32, departmentPosition.PositionLevel);
            db.AddInParameter(cmd, "PositionBaseId", DbType.Int32, departmentPosition.PositionBaseId);
            int newPosID = (int)db.ExecuteScalar(cmd);

            departmentPosition.DepartmentPositionID = newPosID;
        }

        /// <summary>
        /// 删除指定ID的职务
        /// </summary>
        /// <param name="id">要删除的职务的ID</param>
        public void Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_DepartmentPositions_Delete", id);
            db.ExecuteNonQuery(cmd);
            object returnValue = cmd.Parameters[0].Value;

            if (!int.Equals(returnValue, 0))
                throw new ESP.Framework.BusinessLogic.UnknownSqlException();
        }



//        public IList<DepartmentPositionInfo> GetDeparmentPositions(int departmentId)
//        {
//            #region SQL SCRIPT
//            string sql = @"
//---------------------------------------------------------
//SELECT
//p.DepartmentPositionID,
//p.DepartmentPositionName,
//d.DepartmentID,
//d.DepartmentName,
//p.PositionLevel
//FROM sep_DepartmentPositions p
//	JOIN sep_Departments d
//		ON d.DepartmentID = p.DepartmentID
//WHERE d.DepartmentID=@DepartmentID
//---------------------------------------------------------
//";
//            #endregion

//            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
//            DbCommand cmd = db.GetSqlStringCommand(sql);
//            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentId);
//            return CBO.LoadList<DepartmentPositionInfo>(db.ExecuteReader(cmd));
//        }

        /// <summary>
        /// 获取指定ID的员工的职务信息
        /// </summary>
        /// <param name="userId">员工的ID</param>
        /// <returns>描述员工职务的EmployeePositionInfo对象</returns>
        public IList<EmployeePositionInfo> GetEmployeePositions(int userId)
        {
            #region SQL SCRIPT
            string sql = @"
---------------------------------------------------------
SELECT
u.UserID,
u.Username,
p.DepartmentPositionID,
p.DepartmentPositionName,
ep.DepartmentID,
d.DepartmentName,
p.PositionLevel,
ep.IsManager,
ep.IsActing ,
u.LastNameCN + u.FirstNameCN as UsernameCN 
FROM sep_EmployeesInPositions ep
	JOIN sep_DepartmentPositions p
		ON ep.DepartmentPositionID = p.DepartmentPositionID
	JOIN sep_Departments d
		ON d.DepartmentID = ep.DepartmentID
	JOIN sep_Users u
		ON u.UserID = ep.UserID
WHERE ep.UserID=@UserID
---------------------------------------------------------
";
            #endregion

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, userId);
            return CBO.LoadList<EmployeePositionInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 将指定ID的职务授予指定ID的员工
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <param name="departmentPositionId">职务ID</param>
        /// <param name="departmentId">职务所属的部门</param>
        /// <param name="isManager">是否是经理职务</param>
        /// <param name="isActing">是否是临时职务</param>
        public void AddEmployeePosition(int userId, int departmentPositionId, int departmentId, bool isManager, bool isActing)
        {
            #region SQL SCRIPT
            string sql = @"
-------------------------------------------------------------------------------
INSERT INTO sep_EmployeesInPositions 
    (UserID, DepartmentPositionID, DepartmentID, IsActing, IsManager)
VALUES (@UserID, @DepartmentPositionID, @DepartmentID, @IsActing, @IsManager)
-------------------------------------------------------------------------------
";
            #endregion

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, userId);
            db.AddInParameter(cmd, "DepartmentPositionID", DbType.Int32, departmentPositionId);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentId);
            db.AddInParameter(cmd, "IsManager", DbType.Boolean, isManager);
            db.AddInParameter(cmd, "IsActing", DbType.Boolean, isActing);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 取消指定ID的员工的指定ID的职务
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <param name="departmentPositionId">职务ID</param>
        /// <param name="departmentId">职务所属的部门</param>
        public void RemoveEmployeePosition(int userId, int departmentPositionId, int departmentId)
        {
            #region SQL SCRIPT
            string sql = @"
DELETE FROM sep_EmployeesInPositions 
WHERE UserID=@UserID 
    AND DepartmentPositionID=@DepartmentPositionID
    AND DepartmentID=@DepartmentID
";
            #endregion

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, userId);
            db.AddInParameter(cmd, "DepartmentPositionID", DbType.Int32, departmentPositionId);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentId);
            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 获取指定ID的部门可用的职务
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="isOnlyPrivate">是否只返回部门私有职务</param>
        /// <returns>职务列表</returns>
        public IList<DepartmentPositionInfo> GetByDepartment(int departmentId, bool isOnlyPrivate)
        {
            string sql = @"
                ---------------------------------------------------------
                SELECT * FROM sep_DepartmentPositions
                WHERE DepartmentID = @DepartmentID OR (@IsOnlyPrivate = 0 AND DepartmentID = 0)
                ORDER BY DepartmentID ASC, DepartmentPositionID ASC
                ---------------------------------------------------------
                ";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentId);
            db.AddInParameter(cmd, "IsOnlyPrivate", DbType.Boolean, isOnlyPrivate);
            return CBO.LoadList<DepartmentPositionInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取员工职位信息
        /// </summary>
        /// <param name="employeeID">员工ID</param>
        /// <param name="departmentPositionID">职位ID</param>
        /// <param name="departmentID">部门ID</param>
        /// <returns>员工职位信息</returns>
        public EmployeePositionInfo GetEmployeePosition(int employeeID, int departmentPositionID, int departmentID)
        {
            #region SQL SCRIPT
            string sql = @"
---------------------------------------------------------
SELECT top 1 
u.UserID,
u.Username,
ep.DepartmentPositionID,
p.DepartmentPositionName,
ep.DepartmentID,
d.DepartmentName,
p.PositionLevel,
ep.IsManager,
ep.IsActing,
u.LastNameCN + u.FirstNameCN as UsernameCN  
FROM sep_EmployeesInPositions ep
	JOIN sep_DepartmentPositions p
		ON ep.DepartmentPositionID = p.DepartmentPositionID
	JOIN sep_Departments d
		ON d.DepartmentID = ep.DepartmentID
	JOIN sep_Users u
		ON u.UserID = ep.UserID
WHERE ep.UserID=@UserID and ep.DepartmentPositionID=@DepartmentPositionID and ep.DepartmentID=@DepartmentID
---------------------------------------------------------
";
            #endregion

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, employeeID);
            db.AddInParameter(cmd, "DepartmentPositionID", DbType.Int32, departmentPositionID);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentID);
            return CBO.LoadObject<EmployeePositionInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 更新员工职位
        /// </summary>
        /// <param name="employeePosition">要更新的员工职位信息</param>
        public void UpdateEmployeePosition(EmployeePositionInfo employeePosition)
        {
            #region SQL SCRIPT
            string sql = @"
-------------------------------------------------------------------------------
update sep_EmployeesInPositions set 
IsManager=@IsManager,
IsActing=@IsActing
 where UserID=@UserID and DepartmentPositionID=@DepartmentPositionID and DepartmentID=@DepartmentID
-------------------------------------------------------------------------------
";
            #endregion

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, employeePosition.UserID);
            db.AddInParameter(cmd, "DepartmentPositionID", DbType.Int32, employeePosition.DepartmentPositionID);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, employeePosition.DepartmentID);
            db.AddInParameter(cmd, "IsManager", DbType.Boolean, employeePosition.IsManager);
            db.AddInParameter(cmd, "IsActing", DbType.Boolean, employeePosition.IsActing);
            db.ExecuteNonQuery(cmd);

        }


        /// <summary>
        /// 根据职务代码获取职务信息
        /// </summary>
        /// <param name="code">职务代码</param>
        /// <returns>职务信息</returns>
        public DepartmentPositionInfo GetByCode(string code)
        {
            string sql = @"
---------------------------------------------------------
SELECT dp.*,
    ISNULL(d.DepartmentName, '') AS DepartmentName
FROM sep_DepartmentPositions dp
    LEFT JOIN sep_Departments d
        ON d.DepartmentID = dp.DepartmentID
WHERE dp.PositionCode = @PositionCode
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PositionCode", DbType.String, code);
            return CBO.LoadObject<DepartmentPositionInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 检查职务代码是否已经存在
        /// </summary>
        /// <param name="code">职务代码</param>
        /// <returns>存在则返回 true，否则返回 false</returns>
        public bool CodeExists(string code)
        {
            string sql = @"
---------------------------------------------------------
SELECT PositionCode
FROM sep_DepartmentPositions
WHERE PositionCode = @PositionCode
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PositionCode", DbType.String, code);
            object obj = db.ExecuteScalar(cmd);
            if (obj == null || (obj is DBNull))
                return false;
            return true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DepartmentPositionInfo GetByName(int departmentId, string name)
        {
            string sql = @"
---------------------------------------------------------
SELECT dp.*,
    ISNULL(d.DepartmentName, '') AS DepartmentName
FROM sep_DepartmentPositions dp
    LEFT JOIN sep_Departments d
        ON d.DepartmentID = dp.DepartmentID
WHERE dp.DepartmentID = @DepartmentID AND dp.DepartmentPositionName = @DepartmentPositionName
---------------------------------------------------------
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, departmentId);
            db.AddInParameter(cmd, "DepartmentPositionName", DbType.String, name);
            return CBO.LoadObject<DepartmentPositionInfo>(db.ExecuteReader(cmd));
        }

        #endregion
    }
}
