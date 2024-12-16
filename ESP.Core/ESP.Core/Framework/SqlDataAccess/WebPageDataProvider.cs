using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.BusinessLogic;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 页面数据访问类
    /// </summary>
    public class WebPageDataProvider : IWebPageDataProvider
    {
        #region private methods
        private IList<WebPageInfo> GetList(string sql, object[] paras)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (paras != null)
            {
                foreach (object[] p in paras)
                    db.AddInParameter(cmd, (string)p[0], (DbType)p[1], p[2]);
            }
            return ESP.Framework.DataAccess.Utilities.CBO.LoadList<WebPageInfo>(db.ExecuteReader(cmd));
        }

        private WebPageInfo GetObject(string sql, object[] paras)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (paras != null)
            {
                foreach (object[] p in paras)
                    db.AddInParameter(cmd, (string)p[0], (DbType)p[1], p[2]);
            }
            return ESP.Framework.DataAccess.Utilities.CBO.LoadObject<WebPageInfo>(db.ExecuteReader(cmd));
        }
        #endregion

        #region members of IWebPageDataProvider

        /// <summary>
        /// 获取指定ID页面的信息
        /// </summary>
        /// <param name="id">页面的ID</param>
        /// <returns>页面信息</returns>
        public ESP.Framework.Entity.WebPageInfo Get(int id)
        {
            #region SQL SCRIPT

            string sql = @"
SELECT p.*, m.WebSiteID, u1.Username AS CreatorName, u2.Username AS LastModifierName 
FROM sep_WebPages p JOIN sep_Modules m ON p.ModuleID = m.ModuleID
    LEFT JOIN sep_Users u1 ON p.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON p.LastModifier = u2.UserID
WHERE p.WebPageID=@WebPageID
";
            #endregion

            return GetObject(sql, new object[]{
                new object[]{"WebPageID", System.Data.DbType.Int32,id}
            });
        }



        /// <summary>
        /// 获取所有页面的信息列表
        /// </summary>
        /// <returns>所有页面的信息列表</returns>
        public IList<WebPageInfo> GetAll()
        {
            #region SQL SCRIPT
            string sql = @"
SELECT p.*, m.WebSiteID, u1.Username AS CreatorName, u2.Username AS LastModifierName 
FROM sep_WebPages p JOIN sep_Modules m ON p.ModuleID = m.ModuleID
    LEFT JOIN sep_Users u1 ON p.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON p.LastModifier = u2.UserID
";
            #endregion

            return GetList(sql, null);
        }

        /// <summary>
        /// 获取指定站点的所有页面的列表
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>页面信息列表</returns>
        public IList<WebPageInfo> GetByWebSite(int webSiteId)
        {
            #region SQL SCRIPT
            string sql = @"
SELECT p.*, m.WebSiteID, u1.Username AS CreatorName, u2.Username AS LastModifierName 
FROM sep_WebPages p JOIN sep_Modules m ON p.ModuleID = m.ModuleID
    LEFT JOIN sep_Users u1 ON p.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON p.LastModifier = u2.UserID
WHERE m.WebSiteID=@WebSiteID
";
            #endregion

            return GetList(sql, new object[]{
                new object[]{"WebSiteID", System.Data.DbType.Int32,webSiteId}
            });

        }


        /// <summary>
        /// 获取指定模块的所有页面的列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <returns>页面信息列表</returns>
        public IList<WebPageInfo> GetByModule(int moduleId)
        {
            #region SQL SCRIPT
            string sql = @"
SELECT p.*, m.WebSiteID, u1.Username AS CreatorName, u2.Username AS LastModifierName 
FROM sep_WebPages p JOIN sep_Modules m ON p.ModuleID = m.ModuleID
    LEFT JOIN sep_Users u1 ON p.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON p.LastModifier = u2.UserID
WHERE p.ModuleID=@ModuleID
";
            #endregion

            return GetList(sql, new object[]{
                new object[]{"ModuleID", System.Data.DbType.Int32,moduleId}
            });
        }

        /// <summary>
        /// 删除指定ID的页面
        /// </summary>
        /// <param name="id">要删除的页面的ID</param>
        public void Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_WebPages_Delete", id);
            db.ExecuteNonQuery(cmd);

            int ret = (int)cmd.Parameters[0].Value;
            if (ret != 0)
                throw new UnknownSqlException();
        }

        /// <summary>
        /// 获取指定路径的页面
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="appRelativePath">页面对站点根目录的相对路径</param>
        /// <returns>页面信息</returns>
        public WebPageInfo GetByPath(int webSiteId, string appRelativePath)
        {
            #region SQL SCRIPT

            string sql = @"
SELECT p.*, m.WebSiteID, u1.Username AS CreatorName, u2.Username AS LastModifierName 
FROM sep_WebPages p JOIN sep_Modules m ON p.ModuleID = m.ModuleID
    LEFT JOIN sep_Users u1 ON p.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON p.LastModifier = u2.UserID
WHERE p.AppRelativePath=@AppRelativePath
AND m.WebSiteID=@WebSiteID
";
            #endregion

            return GetObject(sql, new object[]{
                new object[]{"AppRelativePath", System.Data.DbType.String, appRelativePath},
                new object[]{"WebSiteID", System.Data.DbType.Int32, webSiteId}
            });
        }

        /// <summary>
        /// 更新页面信息
        /// </summary>
        /// <param name="webPage">要更新的页面</param>
        public void Update(ESP.Framework.Entity.WebPageInfo webPage)
        {
            /*
     @WebPageID int
	,@AppRelativePath nvarchar(256)
	,@Description nvarchar(256)
	,@ModuleID int
	,@Creator int
	,@CreatedTime datetime
	,@LastModifier int
	,@LastModifiedTime datetime
	,@RowVersion timestamp
             */

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_WebPages_Update",
                webPage.WebPageID,
                webPage.AppRelativePath,
                webPage.Description,
                webPage.ModuleID,
                webPage.Creator,
                webPage.CreatedTime,
                webPage.LastModifier,
                webPage.LastModifiedTime,
                webPage.RowVersion);

            CBO.FillObject<WebPageInfo>(db.ExecuteReader(cmd), ref webPage);
            int errorCode = (int)cmd.Parameters[0].Value;
            if (errorCode == 0)
            {
                return;
            }

            if (errorCode == -1)
                throw new ESP.Framework.BusinessLogic.UnmatchedRowVersionException();

            throw new ESP.Framework.BusinessLogic.UnknownSqlException();
        }

        /// <summary>
        /// 创建新的页面
        /// </summary>
        /// <param name="webPage">要创建的页面</param>
        public void Create(WebPageInfo webPage)
        {

            string sql = @"
--------------------------------------------------------------------
INSERT INTO sep_WebPages
           ([AppRelativePath]
           ,[Description]
           ,[ModuleID]
           ,[Creator]
           ,[CreatedTime]
           ,[LastModifier]
           ,[LastModifiedTime])
     VALUES
           (@AppRelativePath
           ,@Description
           ,@ModuleID
           ,@Creator
           ,@CreatedTime
           ,@LastModifier
           ,@LastModifiedTime)
SELECT CAST(SCOPE_IDENTITY() AS int)
--------------------------------------------------------------------
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "AppRelativePath", DbType.String, webPage.AppRelativePath);
            db.AddInParameter(cmd, "Description", DbType.String, webPage.Description);
            db.AddInParameter(cmd, "ModuleID", DbType.Int32, webPage.ModuleID);
            db.AddInParameter(cmd, "Creator", DbType.Int32, webPage.Creator);
            db.AddInParameter(cmd, "CreatedTime", DbType.DateTime, webPage.CreatedTime);
            db.AddInParameter(cmd, "LastModifier", DbType.Int32, 0);
            db.AddInParameter(cmd, "LastModifiedTime", DbType.DateTime, NullValues.DateTime);
            int newWebPageID = (int)db.ExecuteScalar(cmd);

            webPage.WebPageID = newWebPageID;
        }

        /// <summary>
        /// 获取指定路径的页面所属的所有模块的ID
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="appRelativePath">页面对站点根目录的相对路径</param>
        /// <returns>模块ID列表</returns>
        public IList<int> GetModuleIDsByPath(int webSiteId, string appRelativePath)
        {
            #region SQL SCRIPT

            string sql = @"
SELECT m.ModuleID FROM sep_WebPages p 
    JOIN sep_Modules m ON p.ModuleID = m.ModuleID
WHERE p.AppRelativePath=@AppRelativePath
AND m.WebSiteID=@WebSiteID
";
            #endregion

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "AppRelativePath", DbType.String, appRelativePath);
            db.AddInParameter(cmd, "WebSiteID", DbType.Int32, webSiteId);
            return CBO.LoadScalarList<int>(db.ExecuteReader(cmd));
        }

        #endregion
    }
}

