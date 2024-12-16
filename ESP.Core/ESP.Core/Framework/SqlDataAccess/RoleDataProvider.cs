using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 角色数据访问类
    /// </summary>
    public class RoleDataProvider : IRoleDataProvider
    {
        #region members of IRoleDataProvider

        /// <summary>
        /// 获取指定ID的角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>角色信息</returns>
        public ESP.Framework.Entity.RoleInfo Get(int id)
        {
            string sql = @"
---------------------------------------------------------
SELECT r.*,
    ISNULL(rg.RoleGroupName, '') AS RoleGroupName,
    ISNULL(u1.Username, '') AS CreatorName,
    ISNULL(u2.Username, '') AS LastModifierName
FROM sep_Roles r
    LEFT JOIN sep_RoleGroups rg
        ON rg.RoleGroupID = r.RoleGroupID
    LEFT JOIN sep_Users u1
        ON r.Creator = u1.UserID
    LEFT JOIN sep_Users u2
        ON r.LastModifier = u2.UserID
WHERE r.RoleID = @RoleID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "RoleID", DbType.Int32, id);
            return CBO.LoadObject<RoleInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns>所有角色的列表</returns>
        public IList<ESP.Framework.Entity.RoleInfo> GetAll()
        {
            string sql = @"
---------------------------------------------------------
SELECT r.*,
    ISNULL(rg.RoleGroupName, '') AS RoleGroupName,
    ISNULL(u1.Username, '') AS CreatorName,
    ISNULL(u2.Username, '') AS LastModifierName
FROM sep_Roles r
    LEFT JOIN sep_RoleGroups rg
        ON rg.RoleGroupID = r.RoleGroupID
    LEFT JOIN sep_Users u1
        ON r.Creator = u1.UserID
    LEFT JOIN sep_Users u2
        ON r.LastModifier = u2.UserID
ORDER BY r.RoleGroupID ASC, r.RoleID ASC
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<RoleInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 创建新角色
        /// </summary>
        /// <param name="role">新角色信息</param>
        public void Create(ESP.Framework.Entity.RoleInfo role)
        {
            string sql = @"
---------------------------------------------------------
INSERT INTO sep_Roles
           ([RoleName]
           ,[Description]
           ,[RoleGroupID]
           ,[Creator]
           ,[CreatedTime]
           ,[LastModifier]
           ,[LastModifiedTime])
     VALUES
           (@RoleName
           ,@Description
           ,@RoleGroupID
           ,@Creator
           ,@CreatedTime
           ,@LastModifier
           ,@LastModifiedTime)
SELECT CAST(SCOPE_IDENTITY() AS int)
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            //db.AddInParameter(cmd, "RoleID", DbType.Int32, role.RoleID);
            db.AddInParameter(cmd, "RoleName", DbType.String, role.RoleName);
            db.AddInParameter(cmd, "Description", DbType.String, role.Description);
            db.AddInParameter(cmd, "RoleGroupID", DbType.Int32, role.RoleGroupID);
            db.AddInParameter(cmd, "Creator", DbType.Int32, role.Creator);
            db.AddInParameter(cmd, "CreatedTime", DbType.DateTime, role.CreatedTime);
            db.AddInParameter(cmd, "LastModifier", DbType.Int32, 0);
            db.AddInParameter(cmd, "LastModifiedTime", DbType.DateTime, NullValues.DateTime);
            int newRoleID = (int)db.ExecuteScalar(cmd);

            role.RoleID =  newRoleID;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">要更新的角色信息</param>
        public void Update(ESP.Framework.Entity.RoleInfo role)
        {
            // TODO: 检查RowVersion
            string sql = @"
---------------------------------------------------------
UPDATE sep_Roles
   SET [RoleName] = @RoleName
      ,[Description] = @Description
      ,[RoleGroupID] = @RoleGroupID
      ,[LastModifier] = @LastModifier
      ,[LastModifiedTime] = @LastModifiedTime
 WHERE RoleID=@RoleID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "RoleID", DbType.Int32, role.RoleID);
            db.AddInParameter(cmd, "RoleName", DbType.String, role.RoleName);
            db.AddInParameter(cmd, "Description", DbType.String, role.Description);
            db.AddInParameter(cmd, "RoleGroupID", DbType.Int32, role.RoleGroupID);
            db.AddInParameter(cmd, "LastModifier", DbType.Int32, role.LastModifier);
            db.AddInParameter(cmd, "LastModifiedTime", DbType.DateTime, role.LastModifiedTime);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除指定的角色
        /// </summary>
        /// <param name="id">角色ID</param>
        public void Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Roles_Delete", id);
            db.ExecuteNonQuery(cmd);
            object ret = cmd.Parameters[0].Value;
            if (!int.Equals(ret, 0))
                throw new ESP.Framework.BusinessLogic.UnknownSqlException();
        }

        /// <summary>
        /// 获取指定用户直接或间接隶属的角色的ID列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>角色ID列表</returns>
        public int[] GetUserRoleIDs(int userId)
        {
            string cmdtext = @"
---------------------------------------------------------------------
			SELECT RoleID
			FROM sep_EmployeesInPositions ep
				JOIN sep_UsersInRoles ur 
					ON ep.DepartmentID = ur.UserID AND ur.OwnerType=@UserInRoleType_Department
			WHERE ep.UserID=@UserID
            UNION
			SELECT RoleID FROM sep_UsersInRoles WHERE UserID=@UserID AND OwnerType=@UserInRoleType_User
            
---------------------------------------------------------------------
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(cmdtext);
            db.AddInParameter(cmd, "UserID", DbType.Int32, userId);
            db.AddInParameter(cmd, "UserInRoleType_Department", DbType.Int32, RoleOwnerType.Department);
            db.AddInParameter(cmd, "UserInRoleType_User", DbType.Int32, RoleOwnerType.User);
            IList<int> list = CBO.LoadScalarList<int>(db.ExecuteReader(cmd));
            int[] arr = new int[list.Count];
            list.CopyTo(arr, 0);
            return arr;
        }

        /// <summary>
        /// 将指定的实体添加到角色中
        /// </summary>
        /// <param name="entityID">实体ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="type">实体类型</param>
        public void AddEntityToRole(int entityID, int roleId, RoleOwnerType type)
        {
            string cmdText = @"
IF(NOT EXISTS(
	SELECT * FROM sep_UsersInRoles
	WHERE UserID=@UserID AND RoleID=@RoleID AND OwnerType=@OwnerType
))
INSERT INTO sep_UsersInRoles (UserID, RoleID, OwnerType) VALUES(@UserID, @RoleID, @OwnerType)
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(cmdText);
            db.AddInParameter(cmd, "UserID", DbType.Int32, entityID);
            db.AddInParameter(cmd, "RoleID", DbType.Int32, roleId);
            db.AddInParameter(cmd, "OwnerType", DbType.Int32, type);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 将指定的实体从角色中移除
        /// </summary>
        /// <param name="entityID">实体ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="type">实体类型</param>
        public void RemoveEntityFromRole(int entityID, int roleId, RoleOwnerType type)
        {
            string cmdText = "DELETE FROM sep_UsersInRoles WHERE UserID=@UserID AND RoleID=@RoleID AND OwnerType=@OwnerType";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(cmdText);
            db.AddInParameter(cmd, "UserID", DbType.Int32, entityID);
            db.AddInParameter(cmd, "RoleID", DbType.Int32, roleId);
            db.AddInParameter(cmd, "OwnerType", DbType.Int32, type);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取指定实体所属的角色列表
        /// </summary>
        /// <param name="entityID">实体ID</param>
        /// <param name="entityType">实体类型</param>
        /// <returns>实体所属于的角色的列表</returns>
        public IList<RoleInfo> GetByEntity(int entityID, RoleOwnerType entityType)
        {
            string sql = @"
---------------------------------------------------------------------
SELECT r.*, u1.Username AS CreatorName, u2.Username AS LastModifierName
FROM sep_UsersInRoles ur
    JOIN sep_Roles r on r.RoleID = ur.RoleID
    LEFT JOIN sep_Users u1 ON r.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON r.LastModifier = u2.UserID
WHERE ur.UserID=@UserID AND ur.OwnerType = @OwnerType
---------------------------------------------------------------------
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, entityID);
            db.AddInParameter(cmd, "OwnerType", DbType.Int32, entityType);
            return CBO.LoadList<RoleInfo>(db.ExecuteReader(cmd));
        }


        /// <summary>
        /// 获取属于指定角色的所有部门
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>属于该角色的所有部门</returns>
        public IList<DepartmentInfo> GetDepartmentsInRole(int roleId)
        {
            string sql = @"
---------------------------------------------------------------------
SELECT d.*, 
    dt.DepartmentTypeName
FROM sep_UsersInRoles ur
    JOIN sep_Departments d on d.DepartmentID = ur.UserID AND ur.OwnerType = @OwnerTypeDepartment
    LEFT JOIN sep_DepartmentTypes dt ON dt.DepartmentTypeID = d.DepartmentTypeID
WHERE ur.RoleID=@RoleID
---------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "RoleID", DbType.Int32, roleId);
            db.AddInParameter(cmd, "OwnerTypeDepartment", DbType.Int32, RoleOwnerType.Department);
            return CBO.LoadList<DepartmentInfo>(db.ExecuteReader(cmd));

        }

        /// <summary>
        /// 获取直接属于指定角色的所有用户
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>直接属于该角色的所有用户</returns>
        public IList<UserInfo> GetUsersInRole(int roleId)
        {
            string sql = @"
---------------------------------------------------------------------
SELECT u.[UserID]
      ,u.[Username]
      ,u.FirstNameCN
      ,u.LastNameCN
      ,u.FirstNameEN
      ,u.LastNameEN
      ,u.[Email]
      ,u.[CreatedDate]
      ,u.[LastActivityDate]
      ,u.[Status]
      ,u.[IsApproved]
      ,u.[IsLockedOut]
      ,u.[LastLoginDate]
      ,u.[LastPasswordChangedDate]
      ,u.[LastLockoutDate]
      ,u.[Comment]
FROM sep_UsersInRoles ur
    JOIN sep_Users u ON u.UserID = ur.UserID AND ur.OwnerType = @OwnerTypeUser AND ISNULL(u.IsDeleted,0)=0
WHERE ur.RoleID=@RoleID
---------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "RoleID", DbType.Int32, roleId);
            db.AddInParameter(cmd, "OwnerTypeUser", DbType.Int32, RoleOwnerType.User);
            return CBO.LoadList<UserInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取直接属于指定角色的所有员工
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>直接属于该角色的所有员工</returns>
        public IList<EmployeeInfo> GetEmployeesInRole(int roleId)
        {
            string sql = @"
---------------------------------------------------------------------
SELECT e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email,
    u1.Username AS CreatorName, 
    u2.Username AS LastModifierName,
    et.TypeName
FROM sep_UsersInRoles ur
    JOIN sep_Users u ON u.UserID = ur.UserID AND ur.OwnerType = @OwnerTypeUser AND u.ISNULL(u.IsDeleted,0)=0
    JOIN sep_Employees e ON e.UserID = u.UserID
    LEFT JOIN sep_EmployeeTypes et ON et.TypeID = e.TypeID
    LEFT JOIN sep_Users u1 ON e.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON e.LastModifier = u2.UserID
WHERE ur.RoleID=@RoleID
---------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "RoleID", DbType.Int32, roleId);
            db.AddInParameter(cmd, "OwnerTypeUser", DbType.Int32, RoleOwnerType.User);
            return CBO.LoadList<EmployeeInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取指定用户的所有的角色，包括直接和间接角色。
        /// </summary>
        /// <param name="userId">用户的ID</param>
        /// <returns>用户所属的所有角色信息的列表。</returns>
        public IList<RoleInfo> GetUserRoles(int userId)
        {
            string cmdtext = @"
---------------------------------------------------------------------
			SELECT  r.*,
                ISNULL(rg.RoleGroupName, '') AS RoleGroupName,
                ISNULL(u1.Username, '') AS CreatorName,
                ISNULL(u2.Username, '') AS LastModifierName
			FROM sep_EmployeesInPositions ep
				JOIN sep_UsersInRoles ur 
					ON ep.DepartmentID = ur.UserID AND ur.OwnerType=@UserInRoleType_Department
                JOIN sep_Roles r ON ur.RoleID = r.RoleID
                LEFT JOIN sep_RoleGroups rg ON rg.RoleGroupID = r.RoleGroupID
                LEFT JOIN sep_Users u1 ON r.Creator = u1.UserID
                LEFT JOIN sep_Users u2 ON r.LastModifier = u2.UserID
			WHERE ep.UserID=@UserID
        UNION
			SELECT  r.*,
                ISNULL(rg.RoleGroupName, '') AS RoleGroupName,
                ISNULL(u1.Username, '') AS CreatorName,
                ISNULL(u2.Username, '') AS LastModifierName
            FROM sep_UsersInRoles ur
                JOIN sep_Roles r ON ur.RoleID = r.RoleID
                LEFT JOIN sep_RoleGroups rg ON rg.RoleGroupID = r.RoleGroupID
                LEFT JOIN sep_Users u1 ON r.Creator = u1.UserID
                LEFT JOIN sep_Users u2 ON r.LastModifier = u2.UserID
            WHERE ur.UserID=@UserID AND ur.OwnerType=@UserInRoleType_User
            
---------------------------------------------------------------------
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(cmdtext);
            db.AddInParameter(cmd, "UserID", DbType.Int32, userId);
            db.AddInParameter(cmd, "UserInRoleType_Department", DbType.Int32, RoleOwnerType.Department);
            db.AddInParameter(cmd, "UserInRoleType_User", DbType.Int32, RoleOwnerType.User);
            IList<RoleInfo> list = CBO.LoadList<RoleInfo>(db.ExecuteReader(cmd));
            return list;
        }

        #endregion
    }
}
