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
    public class RoleGroupDataProvider : IRoleGroupDataProvider
    {
        #region members of IRoleGroupDataProvider

        /// <summary>
        /// 获取指定ID的角色组
        /// </summary>
        /// <param name="id">角色组ID</param>
        /// <returns>角色组信息</returns>
        public ESP.Framework.Entity.RoleGroupInfo Get(int id)
        {
            string sql = @"
---------------------------------------------------------
SELECT rg.*,
    ISNULL(u1.Username, '') AS CreatorName,
    ISNULL(u2.Username, '') AS LastModifierName
FROM sep_RoleGroups rg
    LEFT JOIN sep_Users u1
        ON rg.Creator = u1.UserID
    LEFT JOIN sep_Users u2
        ON rg.LastModifier = u2.UserID
WHERE rg.RoleGroupID = @RoleGroupID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "RoleGroupID", DbType.Int32, id);
            return CBO.LoadObject<RoleGroupInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取所有角色组的列表
        /// </summary>
        /// <returns>角色组信息列表</returns>
        public IList<RoleGroupInfo> GetAll()
        {
            string sql = @"
---------------------------------------------------------
SELECT rg.*,
    ISNULL(u1.Username, '') AS CreatorName,
    ISNULL(u2.Username, '') AS LastModifierName
FROM sep_RoleGroups rg
    LEFT JOIN sep_Users u1
        ON rg.Creator = u1.UserID
    LEFT JOIN sep_Users u2
        ON rg.LastModifier = u2.UserID
ORDER BY rg.ParentID ASC, rg.RoleGroupID ASC
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<RoleGroupInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 更新角色组信息
        /// </summary>
        /// <param name="roleGroup">要更新的角色组</param>
        public void Update(ESP.Framework.Entity.RoleGroupInfo roleGroup)
        {
            // TODO: 检查RowVersion
            string sql = @"
---------------------------------------------------------
UPDATE sep_RoleGroups
   SET [RoleGroupName] = @RoleGroupName
      ,[ParentID] = @ParentID
      ,[Description] = @Description
      ,[RoleGroupLevel] = @RoleGroupLevel
      ,[LastModifier] = @LastModifier
      ,[LastModifiedTime] = @LastModifiedTime
 WHERE RoleGroupID = @RoleGroupID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "RoleGroupID", DbType.Int32, roleGroup.RoleGroupID);
            db.AddInParameter(cmd, "RoleGroupName", DbType.String, roleGroup.RoleGroupName);
            db.AddInParameter(cmd, "Description", DbType.String, roleGroup.Description);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, roleGroup.ParentID);
            db.AddInParameter(cmd, "RoleGroupLevel", DbType.Int32, roleGroup.RoleGroupLevel);
            db.AddInParameter(cmd, "LastModifier", DbType.Int32, roleGroup.LastModifier);
            db.AddInParameter(cmd, "LastModifiedTime", DbType.DateTime, roleGroup.LastModifiedTime);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 创建新的角色组
        /// </summary>
        /// <param name="roleGroup">要创建的角色组</param>
        public void Create(ESP.Framework.Entity.RoleGroupInfo roleGroup)
        {
            string sql = @"
---------------------------------------------------------
INSERT INTO sep_RoleGroups
           ([RoleGroupName]
           ,[ParentID]
           ,[Description]
           ,[RoleGroupLevel]
           ,[Creator]
           ,[CreatedTime]
           ,[LastModifier]
           ,[LastModifiedTime])
     VALUES
           (@RoleGroupName
           ,@ParentID
           ,@Description
           ,@RoleGroupLevel
           ,@Creator
           ,@CreatedTime
           ,@LastModifier
           ,@LastModifiedTime)
SELECT CAST(SCOPE_IDENTITY() AS int)
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "RoleGroupID", DbType.Int32, roleGroup.RoleGroupID);
            db.AddInParameter(cmd, "RoleGroupName", DbType.String, roleGroup.RoleGroupName);
            db.AddInParameter(cmd, "Description", DbType.String, roleGroup.Description);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, roleGroup.ParentID);
            db.AddInParameter(cmd, "RoleGroupLevel", DbType.Int32, roleGroup.RoleGroupLevel);
            db.AddInParameter(cmd, "Creator", DbType.Int32, roleGroup.Creator);
            db.AddInParameter(cmd, "CreatedTime", DbType.DateTime, roleGroup.CreatedTime);
            db.AddInParameter(cmd, "LastModifier", DbType.Int32, 0);
            db.AddInParameter(cmd, "LastModifiedTime", DbType.DateTime, NullValues.DateTime);
            int newId = (int)db.ExecuteScalar(cmd);

            roleGroup.RoleGroupID = newId;
        }


        /// <summary>
        /// 删除指定ID的角色组
        /// </summary>
        /// <param name="id">要删除的角色组的ID</param>
        public void Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_RoleGroups_Delete", id);
            db.ExecuteNonQuery(cmd);
            object ret = cmd.Parameters[0].Value;
            if (!int.Equals(ret, 0))
                throw new ESP.Framework.BusinessLogic.UnknownSqlException();
        }

        #endregion
    }
}
