using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.BusinessLogic;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 站点数据访问类
    /// </summary>
    public class WebSiteDataProvider : IWebSiteDataProvider
    {
        #region Select sql script
        private const string SQL_SELECT_BY_ID = @"
---------------------------------------------------------
SELECT ws.*, 
    ISNULL(u1.Username, N'') AS CreatorName,
    ISNULL(u2.Username, N'') AS LastModifierName
FROM sep_WebSites ws
    LEFT JOIN sep_Users u1 ON ws.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON ws.LastModifier = u2.UserID
WHERE ws.WebSiteID=@WebSiteID
---------------------------------------------------------
";
        #endregion

        #region Select by UrlPrefix sql script
        private const string SQL_SELECT_BY_URLPREFIX = @"
---------------------------------------------------------
SELECT ws.*, 
    ISNULL(u1.Username, N'') AS CreatorName,
    ISNULL(u2.Username, N'') AS LastModifierName
FROM sep_WebSites ws
    LEFT JOIN sep_Users u1 ON ws.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON ws.LastModifier = u2.UserID
WHERE ws.UrlPrefix=@UrlPrefix
---------------------------------------------------------
";
        #endregion

        #region Select all sql script

        private const string SQL_SELECT_ALL = @"
---------------------------------------------------------
SELECT ws.*, 
    ISNULL(u1.Username, N'') AS CreatorName,
    ISNULL(u2.Username, N'') AS LastModifierName
FROM sep_WebSites ws
    LEFT JOIN sep_Users u1 ON ws.Creator = u1.UserID
    LEFT JOIN sep_Users u2 ON ws.LastModifier = u2.UserID
ORDER BY ws.Ordinal ASC, ws.WebSiteID ASC
---------------------------------------------------------
";
        #endregion

        #region members of IWebSiteDataProvider

        /// <summary>
        /// 获取指定ID的站点的信息
        /// </summary>
        /// <param name="id">站点的ID</param>
        /// <returns>站点信息</returns>
        public ESP.Framework.Entity.WebSiteInfo Get(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(SQL_SELECT_BY_ID);
            db.AddInParameter(cmd, "WebSiteID", System.Data.DbType.Int32, id);
            return CBO.LoadObject<WebSiteInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取所有站点的列表
        /// </summary>
        /// <returns>站点列表</returns>
        public IList<WebSiteInfo> GetAll()
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(SQL_SELECT_ALL);
            return CBO.LoadList<WebSiteInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取指定ID的用户可以访问的站点的列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>可访问的站点列表</returns>
        public IList<WebSiteInfo> GetByUser(int userId)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Modules_GetUserWebSites", userId);
            return CBO.LoadList<WebSiteInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 更新站点信息
        /// </summary>
        /// <param name="webSite">要更新的站点的信息</param>
        public void Update(ESP.Framework.Entity.WebSiteInfo webSite)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_WebSites_Update",
            webSite.WebSiteID,
            webSite.WebSiteName,
            webSite.Description,
            webSite.UrlPrefix,
            webSite.FramePagePath,
            webSite.Ordinal,
            string.Empty, //webSite.Token,
            webSite.LastModifier,
            webSite.LastModifiedTime,
            webSite.RowVersion);
            CBO.FillObject<WebSiteInfo>(db.ExecuteReader(cmd), ref webSite);

            int ret = (int)cmd.Parameters[0].Value;
            if (ret == -1)
                throw new UnmatchedRowVersionException();
            else if (ret != 0)
                throw new UnknownSqlException();
        }

        /// <summary>
        /// 创建新的站点
        /// </summary>
        /// <param name="webSite">要创建的站点</param>
        public void Create(ESP.Framework.Entity.WebSiteInfo webSite)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_WebSites_Add",

            webSite.WebSiteName,
            webSite.Description,
            webSite.UrlPrefix,
            webSite.FramePagePath,
            webSite.Ordinal,
            string.Empty, //webSite.Token,
            webSite.Creator,
            webSite.CreatedTime);
            CBO.FillObject<WebSiteInfo>(db.ExecuteReader(cmd), ref webSite);

            int ret = (int)cmd.Parameters[0].Value;
            if (ret != 0)
                throw new UnknownSqlException();
        }

        /// <summary>
        /// 删除指定ID的站点
        /// </summary>
        /// <param name="id">要删除的站点的ID</param>
        public void Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_WebSites_Delete", id);
            db.ExecuteNonQuery(cmd);

            int ret = (int)cmd.Parameters[0].Value;
            if (ret != 0)
                throw new UnknownSqlException();

        }


        /// <summary>
        /// 获取指定Url前缀的站点
        /// </summary>
        /// <param name="urlPrefix">站点的Url前缀(不包含http://或https://，也不包含最后的反斜杠)</param>
        /// <returns>站点信息对象</returns>
        public WebSiteInfo GetByUrlPrefix(string urlPrefix)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(SQL_SELECT_BY_URLPREFIX);
            db.AddInParameter(cmd, "UrlPrefix", System.Data.DbType.String, urlPrefix);
            return CBO.LoadObject<WebSiteInfo>(db.ExecuteReader(cmd));

        }

        #endregion
    }
}
