using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using System.Collections.Generic;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 系统设置数据访问实现类
    /// </summary>
    public class SettingsDataProvider : ISettingsDataProvider
    {
        #region ISettingsDataProvider 成员


        /// <summary>
        /// 获取站点的所有设置
        /// </summary>
        /// <param name="webSiteId">设置所属的站点ID，如果为0表示返回公共设置</param>
        /// <param name="includeSystemSettings">如果站点不为0， 该参数指定是否同时返回公共设置</param>
        /// <returns>站点的所有设置项</returns>
        public IList<ESP.Framework.Entity.SettingInfo> GetSettings(int webSiteId, bool includeSystemSettings)
        {
            string sql = @"
SELECT SettingValue=ISNULL(ISNULL(s.SettingValue,s2.SettingValue),sd.DefaultValue), 
	sd.SettingName, sd.DefinitionID, sd.ValueType, 
    IsInherited=CASE ISNULL(s.WebSiteID, -1) + ISNULL(s2.WebSiteID, -1) WHEN -1 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END
FROM sep_SysSettingDefinitions sd
	LEFT JOIN sep_SysSettings s ON sd.DefinitionID=s.DefinitionID AND s.WebSiteID=@WebSiteID
	LEFT JOIN sep_SysSettings s2 ON sd.DefinitionID=s2.DefinitionID AND s2.WebSiteID=0
WHERE (sd.WebSiteID=@WebSiteID OR (@IncludeSystemSettings=1 OR (sd.WebSiteID=0 AND sd.IsOverridable=1)))
ORDER BY sd.WebSiteID ASC, sd.Ordinal ASC
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "WebSiteID", System.Data.DbType.Int32, webSiteId);
            db.AddInParameter(cmd, "IncludeSystemSettings", System.Data.DbType.Boolean, includeSystemSettings);
            return CBO.LoadList<SettingInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取站点指定名称的设置项
        /// </summary>
        /// <param name="webSiteId">设置所属的站点ID，如果为0表示返回公共设置</param>
        /// <param name="settingName">设置的名称</param>
        /// <param name="includeSystemSettings">如果站点不为0， 该参数指定是否同时返回公共设置</param>
        /// <returns>站点指定名称的设置项</returns>
        public ESP.Framework.Entity.SettingInfo GetSetting(int webSiteId, string settingName, bool includeSystemSettings)
        {
            string sql = @"
SELECT SettingValue=ISNULL(ISNULL(s.SettingValue,s2.SettingValue),sd.DefaultValue), 
	sd.SettingName, sd.DefinitionID, sd.ValueType, 
    IsInherited=CASE ISNULL(s.WebSiteID, -1) + ISNULL(s2.WebSiteID, -1) WHEN -1 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END
FROM sep_SysSettingDefinitions sd
	LEFT JOIN sep_SysSettings s ON sd.DefinitionID=s.DefinitionID AND s.WebSiteID=@WebSiteID
	LEFT JOIN sep_SysSettings s2 ON sd.DefinitionID=s2.DefinitionID AND s2.WebSiteID=0
WHERE (sd.WebSiteID=@WebSiteID OR (@IncludeSystemSettings=1 OR (sd.WebSiteID=0 AND sd.IsOverridable=1)))
	AND sd.SettingName=N'PortalWebSite'
ORDER BY sd.WebSiteID ASC, sd.Ordinal ASC
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "WebSiteID", System.Data.DbType.Int32, webSiteId);
            db.AddInParameter(cmd, "IncludeSystemSettings", System.Data.DbType.Boolean, includeSystemSettings);
            return CBO.LoadObject<SettingInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 添加设置定义
        /// </summary>
        /// <param name="definition">设置定义</param>
        /// <return>新定义的标识ID</return>
        public int AddDefinition(SettingDefinitionInfo definition)
        {
            string sql = @"
INSERT INTO sep_SysSettingDefinitions
           ([SettingName]
           ,[DefinitionName]
           ,[ValueType]
           ,[EditorType]
           ,[ValidationExpression]
           ,[Description]
           ,[DefaultValue]
           ,[WebSiteID]
           ,[Ordinal]
           ,[IsOverridable])
     VALUES
           (@SettingName
           ,@DefinitionName
           ,@ValueType
           ,@EditorType
           ,@ValidationExpression
           ,@Description
           ,@DefaultValue
           ,@WebSiteID
           ,@Ordinal
           ,@IsOverridable)
CAST(SCOPE_IDENTITY() AS int) 
            ";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "SettingName", System.Data.DbType.String, definition.SettingName);
            db.AddInParameter(cmd, "ValueType", System.Data.DbType.String, definition.ValueType);
            db.AddInParameter(cmd, "EditorType", System.Data.DbType.Int32, definition.EditorType);
            db.AddInParameter(cmd, "ValidationExpression", System.Data.DbType.String, definition.ValidationExpression);
            db.AddInParameter(cmd, "Description", System.Data.DbType.String, definition.Description);
            db.AddInParameter(cmd, "DefaultValue", System.Data.DbType.String, definition.Description);
            db.AddInParameter(cmd, "WebSiteID", System.Data.DbType.Int32, definition.WebSiteID);
            db.AddInParameter(cmd, "Ordinal", System.Data.DbType.Int32, definition.Ordinal);
            db.AddInParameter(cmd, "IsOverridable", System.Data.DbType.Boolean, definition.IsOverridable);

            int id = (int)db.ExecuteScalar(cmd);
            definition.DefinitionID = id;
            return id;

        }


        /// <summary>
        /// 获取站点的所有设置定义
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>所有设置定义的列表</returns>
        /// <remarks>
        /// 返回值中包括公共设置定义中可重载的定义。
        /// </remarks>
        public IList<SettingDefinitionInfo> GetDefinitions(int webSiteId)
        {
            string sql = @"
SELECT * FROM sep_SysSettingDefinitions sd
WHERE (sd.WebSiteID=3 OR (sd.WebSiteID=0 AND sd.IsOverridable=1))
ORDER BY sd.WebSiteID ASC, sd.Ordinal ASC
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "WebSiteID", System.Data.DbType.Int32, webSiteId);
            return CBO.LoadList<SettingDefinitionInfo>(db.ExecuteReader(cmd));
        }




        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="value">设置项信息</param>
        /// <param name="webSiteId">设置所属的站点的ID，如果为0， 则表示是公共设置</param>
        public void SaveSetting(SettingInfo value, int webSiteId)
        {
            string sql = @"

DECLARE @SettingID int


SELECT @SettingID = [SettingID] 
FROM [sep_SysSettings] WITH (UPDLOCK HOLDLOCK)
WHERE DefinitionID=@DefinitionID 
	AND WebSiteID=@WebSiteID

IF (@SettingID IS NULL)
BEGIN
	INSERT INTO [sep_SysSettings]
			   ([SettingValue]
			   ,[WebSiteID]
			   ,[DefinitionID])
		 VALUES
			   (@SettingValue
			   ,@WebSiteID
			   ,@DefinitionID)
END
ELSE
BEGIN
	UPDATE [sep_SysSettings]
	   SET [SettingValue] = @SettingValue
	 WHERE SettingID=@SettingID
END

";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "SettingName", System.Data.DbType.String, value.SettingName);
            db.AddInParameter(cmd, "WebSiteID", System.Data.DbType.Int32, webSiteId);
            db.AddInParameter(cmd, "DefinitionID", System.Data.DbType.Int32, value.DefinitionID);
            db.ExecuteNonQuery(cmd);
        }

        #endregion
    }
}
