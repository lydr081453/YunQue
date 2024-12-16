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
    /// 模块定义数据访问类
    /// </summary>
    public class ModuleDataProvider : IModuleDataProvider
    {
        #region SQL_SELECT_COMMON (const string)
        private const string SQL_SELECT_COMMON = @"
SELECT m.*, wp.AppRelativePath AS DefaultPageUrl
FROM sep_Modules m
    LEFT JOIN sep_WebPages wp 
        ON m.DefaultPageID = wp.WebPageID
";
        #endregion

        #region IModuleDataProvider 成员

        /// <summary>
        /// 获取指定ID的模块定义
        /// </summary>
        /// <param name="id">要获取的模块定义的ID</param>
        /// <returns>模块定义信息</returns>
        public ESP.Framework.Entity.ModuleInfo Get(int id)
        {
            string sql = SQL_SELECT_COMMON + " WHERE m.ModuleID=@ModuleID";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "ModuleID", DbType.Int32, id);
            IDataReader reader = db.ExecuteReader(cmd);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadObject<ModuleInfo>(reader);
        }

        /// <summary>
        /// 获取指定站点的所有模块定义列表
        /// </summary>
        /// <param name="webSiteId">要获取的模块列表的站点ID</param>
        /// <returns>该站点所有模块的列表</returns>
        public IList<ESP.Framework.Entity.ModuleInfo> GetByWebSite(int webSiteId)
        {
            string sql = SQL_SELECT_COMMON + " WHERE m.WebSiteID=@WebSiteID ORDER BY m.ParentID ASC, m.Ordinal ASC, m.ModuleID ASC";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "WebSiteID", DbType.Int32, webSiteId);
            IDataReader reader = db.ExecuteReader(cmd);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadList<ModuleInfo>(reader);

        }

        /// <summary>
        /// 更新模块定义
        /// </summary>
        /// <param name="module">要更新的模块</param>
        public void Update(ESP.Framework.Entity.ModuleInfo module)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Modules_Update",
                    module.ModuleID,
                    module.WebSiteID,
                    module.ModuleName,
                    module.Description,
                    module.ParentID,
                    module.DefaultPageID,
                    module.Node,
                    module.NodePath,
                    module.NodeLevel,
                    module.NodeType,
                    module.Ordinal,
                    module.LastModifier,
                    module.LastModifiedTime,
                    module.RowVersion);

            db.ExecuteNonQuery(cmd);

            int ret = (int)cmd.Parameters[0].Value;
            if (ret == -1)
                throw new UnmatchedRowVersionException();
            else if (ret != 0)
                throw new UnknownSqlException();
        }

        /// <summary>
        /// 创建新的模块定义
        /// </summary>
        /// <param name="module">要创建的模块定义信息</param>
        public void Create(ESP.Framework.Entity.ModuleInfo module)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Modules_Create",
               module.WebSiteID,
               module.ModuleName,
               module.Description,
               module.ParentID,
               module.DefaultPageID,
               //module.Node,
               //module.NodePath,
               //module.NodeLevel,
               module.NodeType,
               module.Ordinal,
               module.Creator,
               module.CreatedTime);

            CBO.FillObject<ModuleInfo>(db.ExecuteReader(cmd), ref module);

            int ret = (int)cmd.Parameters[0].Value;
            if (ret != 0)
                throw new UnknownSqlException();

        }

        /// <summary>
        /// 删除指定ID的模块定义
        /// </summary>
        /// <param name="id">要删除的模块的ID</param>
        public void Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Modules_Delete", id);
            db.ExecuteNonQuery(cmd);

            int ret = (int)cmd.Parameters[0].Value;
            if (ret != 0)
                throw new UnknownSqlException();

        }

        /// <summary>
        /// 获取指定站点中指定用户可访问的模块列表
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>可访问模块列表</returns>
        public IList<ModuleInfo> GetByUser(int webSiteId, int userId)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            return CBO.LoadList<ModuleInfo>(db.ExecuteReader("sep_Modules_GetUserModules", webSiteId, userId));
        }

        /// <summary>
        /// 获取系统中所有的模块定义的列表
        /// </summary>
        /// <returns>所有模块定义的列表</returns>
        public IList<ModuleInfo> GetAll()
        {
            string sql = @"
SELECT m.*, 
    wp.AppRelativePath AS DefaultPageUrl
FROM sep_Modules m
    LEFT JOIN sep_WebPages wp 
        ON m.DefaultPageID = wp.WebPageID
ORDER BY m.Ordinal ASC, m.ModuleID ASC
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<ModuleInfo>(db.ExecuteReader(cmd));
        }

        #endregion
    }
}
