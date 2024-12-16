using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using System;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 权限数据访问类
    /// </summary>
    public class PermissionDataProvider : IPermissionDataProvider
    {
        #region members of IPermissionDataProvider

        /// <summary>
        /// 获得取指定ID的权限的详细信息
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns>权限信息对象</returns>
        public ESP.Framework.Entity.PermissionInfo Get(int id)
        {
            string sql = @"
--------------------------------------------------------------------
SELECT * FROM sep_Permissions 
WHERE PermissionID=@PermissionID
--------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PermissionID", DbType.Int32, id);
            return CBO.LoadObject<PermissionInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 添加新的权限，即将指定的权限分配给指定的权限持有者（角色、用户等）
        /// </summary>
        /// <param name="permission">权限对象</param>
        public void Add(ESP.Framework.Entity.PermissionInfo permission)
        {
            string sql = @"
--------------------------------------------------------------------
INSERT INTO sep_Permissions
           ([PermissionDefinitionID]
           ,[OwnerID]
           ,[OwnerType]
           ,[ReferredEntityID]
           ,[ReferredEntityType]
           ,[Creator]
           ,[CreatedTime])
     VALUES
           (@PermissionDefinitionID
           ,@OwnerID
           ,@OwnerType
           ,@ReferredEntityID
           ,@ReferredEntityType
           ,@Creator
           ,@CreatedTime)
SELECT CAST(SCOPE_IDENTITY() AS INT)
--------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PermissionDefinitionID", DbType.String, permission.PermissionDefinitionID);
            db.AddInParameter(cmd, "OwnerID", DbType.Int32, permission.OwnerID);
            db.AddInParameter(cmd, "OwnerType", DbType.Int32, permission.OwnerType);
            db.AddInParameter(cmd, "ReferredEntityID", DbType.Int32, permission.ReferredEntityID);
            db.AddInParameter(cmd, "ReferredEntityType", DbType.Int32, permission.ReferredEntityType);
            db.AddInParameter(cmd, "Creator", DbType.Int32, permission.Creator);
            db.AddInParameter(cmd, "CreatedTime", DbType.DateTime, permission.CreatedTime);
            int permissionID = (int)db.ExecuteScalar(cmd);

            permission.PermissionID = permissionID;
        }

        /// <summary>
        /// 获取指定的权限持有者（角色、用户等）对指定的实体对象拥有的权限列表
        /// </summary>
        /// <param name="entityType">权限控制的实体的类型</param>
        /// <param name="entityID">权限控制的实体的ID</param>
        /// <param name="ownerType">权限持有者类型</param>
        /// <param name="ownerID">权限持有者ID</param>
        /// <returns>权限名字的列表</returns>
        public string[] GetPermissions(EntityType entityType, int entityID, PermissionOwnerTypes ownerType, int ownerID)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            IList<string> list = CBO.LoadScalarList<string>(
                db.ExecuteReader("sep_Permissions_GetNameList", entityType, entityID, ownerType, ownerID)
                );

            if (list == null || list.Count == 0)
                return new string[0];

            string[] arr = new string[list.Count];
            list.CopyTo(arr, 0);

            return arr;
        }

        /// <summary>
        /// 删除指定的权限分配
        /// </summary>
        /// <param name="id">要删除的权限分配的ID</param>
        public void Remove(int id)
        {
            string sql = @"
--------------------------------------------------------------------
DELETE sep_Permissions
WHERE PermissionID=@PermissionID
--------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PermissionID", DbType.Int32, id);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取所有的权限定义
        /// </summary>
        /// <returns>所有权限定义的列表</returns>
        public IList<PermissionDefinitionInfo> GetAllDefinitions()
        {
            string sql = @"
--------------------------------------------------------------------
SELECT * FROM sep_PermissionDefinitions 
--------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<PermissionDefinitionInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取指定ID的权限定义
        /// </summary>
        /// <param name="id">权限定义的ID</param>
        /// <returns>权限定义</returns>
        public PermissionDefinitionInfo GetDefinition(int id)
        {
            string sql = @"
--------------------------------------------------------------------
SELECT * FROM sep_PermissionDefinitions 
WHERE PermissionDefinitionID=@PermissionDefinitionID
--------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PermissionDefinitionID", DbType.Int32, id);
            return CBO.LoadObject<PermissionDefinitionInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取指定实体的权限定义列表
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="entityID">实体ID</param>
        /// <returns></returns>
        public IList<PermissionDefinitionInfo> GetDefinitions(EntityType entityType, int entityID)
        {
            string sql = @"
--------------------------------------------------------------------
SELECT * FROM sep_PermissionDefinitions 
WHERE ReferredEntityID=@ReferredEntityID AND ReferredEntityType=@ReferredEntityType
--------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "ReferredEntityType", DbType.Int32, entityType);
            db.AddInParameter(cmd, "ReferredEntityID", DbType.Int32, entityID);
            return CBO.LoadList<PermissionDefinitionInfo>(db.ExecuteReader(cmd));

        }

        /// <summary>
        /// 删除指定ID的权限定义
        /// </summary>
        /// <param name="id">要删除的权限定义的ID</param>
        public void RemoveDefinition(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Permissions_DeleteDefinition", id);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 添加新的权限定义
        /// </summary>
        /// <param name="permissionDefinition">要添加的权限定义</param>
        public void AddDefinition(PermissionDefinitionInfo permissionDefinition)
        {
            string sql = @"
--------------------------------------------------------------------
INSERT INTO sep_PermissionDefinitions
           ([PermissionName]
           ,[ReferredEntityID]
           ,[ReferredEntityType]
           ,[Description]
           ,[Creator]
           ,[CreatedTime])
     VALUES
           (@PermissionName
           ,@ReferredEntityID
           ,@ReferredEntityType
           ,@Description
           ,@Creator
           ,@CreatedTime)
SELECT CAST(SCOPE_IDENTITY() AS INT)
--------------------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "PermissionName", DbType.String, permissionDefinition.PermissionName);
            db.AddInParameter(cmd, "ReferredEntityID", DbType.Int32, permissionDefinition.ReferredEntityID);
            db.AddInParameter(cmd, "ReferredEntityType", DbType.Int32, permissionDefinition.ReferredEntityType);
            db.AddInParameter(cmd, "Description", DbType.String, permissionDefinition.Description);
            db.AddInParameter(cmd, "Creator", DbType.Int32, permissionDefinition.Creator);
            db.AddInParameter(cmd, "CreatedTime", DbType.DateTime, permissionDefinition.CreatedTime);
            int permissionDefinitionID = (int)db.ExecuteScalar(cmd);

            permissionDefinition.PermissionDefinitionID = permissionDefinitionID;
        }



        /// <summary>
        /// 获取角色拥有的权限（只针对模块）
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <param name="isFakeRole">是否为内置的伪角色</param>
        /// <returns>角色拥有的权限列表， key为权限定义， value为模块ID</returns>
        public IList<KeyValuePair<int, int>> GetRolePermisssions(int roleId, bool isFakeRole)
        {
            IList<KeyValuePair<int, int>> keys = new List<KeyValuePair<int, int>>();
            IList<int> webSiteIds = new List<int>();
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            IDataReader reader = db.ExecuteReader("sep_Permissions_GetRolePermissions", roleId, isFakeRole);
            int permissionDefinitionIdRowIndex = reader.GetOrdinal("PermissionDefinitionID");
            int moduleIdRowIndex = reader.GetOrdinal("ModuleID");
            while (reader.Read())
            {
                int permissionDefinitionId = reader.GetInt32(permissionDefinitionIdRowIndex);
                int moduleId = reader.GetInt32(moduleIdRowIndex);
                keys.Add(new KeyValuePair<int, int>(permissionDefinitionId, moduleId));
            }

            return keys;
        }


        /// <summary>
        /// 更新角色的权限（只针对模块）
        /// </summary>
        /// <param name="list">权限列表， key为权限定义， value为模块ID</param>
        /// <param name="roleId">角色的ID</param>
        /// <param name="isFakeRole">roleId是否为系统内置的伪角色</param>
        public void UpdateRolePermissions(IList<KeyValuePair<int, int>> list, int roleId, bool isFakeRole)
        {
            string sqlDelete = @"
DELETE sep_Permissions
WHERE OwnerID=@RoleID AND OwnerType=@RoleOwnerType
";

            string sqlInsert = @"
INSERT INTO [sep_Permissions]
           ([PermissionDefinitionID]
           ,[OwnerID]
           ,[OwnerType]
           ,[ReferredEntityID]
           ,[ReferredEntityType]
           ,[Creator]
           ,[CreatedTime])
     VALUES
           (@PermissionDefinitionID
           ,@RoleID
           ,@RoleOwnerType
           ,@ReferredEntityID
           ,@ReferredEntityType
           ,@Creator
           ,@CreatedTime)
";

            PermissionOwnerTypes ownerType = isFakeRole ?  PermissionOwnerTypes.FakeRole : PermissionOwnerTypes.Role;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);

            string pn_PermissionDefinitionID = db.BuildParameterName("PermissionDefinitionID");
            string pn_ReferredEntityID = db.BuildParameterName("ReferredEntityID");

            DbCommand cmdDel = db.GetSqlStringCommand(sqlDelete);
            db.AddInParameter(cmdDel, "RoleID", DbType.Int32, roleId);
            db.AddInParameter(cmdDel, "RoleOwnerType", DbType.Int32, ownerType);


            DbCommand cmdIns = db.GetSqlStringCommand(sqlInsert);
            db.AddInParameter(cmdIns, "RoleID", DbType.Int32, roleId);
            db.AddInParameter(cmdIns, "RoleOwnerType", DbType.Int32, ownerType);
            db.AddInParameter(cmdIns, "ReferredEntityType", DbType.Int32, EntityType.Module);
            db.AddInParameter(cmdIns, "Creator", DbType.Int32, UserManager.GetCurrentUserID());
            db.AddInParameter(cmdIns, "CreatedTime", DbType.DateTime, DateTime.Now);

            db.AddInParameter(cmdIns, "PermissionDefinitionID", DbType.Int32);
            db.AddInParameter(cmdIns, "ReferredEntityID", DbType.Int32);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                using (DbTransaction trans = connection.BeginTransaction())
                {
                    try
                    {
                        db.ExecuteNonQuery(cmdDel, trans);
                        for (int i = 0; i < list.Count; i++)
                        {
                            KeyValuePair<int, int> item = list[i];
                            cmdIns.Parameters[pn_PermissionDefinitionID].Value = item.Key;
                            cmdIns.Parameters[pn_ReferredEntityID].Value = item.Value;
                            db.ExecuteNonQuery(cmdIns, trans);
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}
