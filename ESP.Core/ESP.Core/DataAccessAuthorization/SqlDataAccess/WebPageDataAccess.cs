using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESP.DataAccessAuthorization.SqlDataAccess
{
    /// <summary>
    /// 数据表单操作权限SQL Server数据操作
    /// </summary>
    public class WebPageDataAccess : DataAccess.IWebPageDataAccess
    {
        #region IWebPageDataAccess 成员
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(ESP.DataAccessAuthorization.Entity.WebPageDataAccess model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ESP_WebPageDataAccess(");
            strSql.Append("WebPageID,DataAccessActionID,WorkFlowVersion,CreateTime,Creator,CreatorName)");
            strSql.Append(" values (");
            strSql.Append("@WebPageID,@DataAccessActionID,@WorkFlowVersion,@CreateTime,@Creator,@CreatorName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WebPageID", SqlDbType.Int,4),
					new SqlParameter("@DataAccessActionID", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowVersion", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.WebPageID;
            parameters[1].Value = model.DataAccessActionID;
            parameters[2].Value = model.WorkFlowVersion;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.Creator;
            parameters[5].Value = model.CreatorName;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            object obj = db.ExecuteScalar(cmd);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回更新数据数量</returns>
        public int Update(ESP.DataAccessAuthorization.Entity.WebPageDataAccess model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ESP_WebPageDataAccess set ");
            strSql.Append("WebPageID=@WebPageID,");
            strSql.Append("DataAccessActionID=@DataAccessActionID,");
            strSql.Append("WorkFlowVersion=@WorkFlowVersion,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatorName=@CreatorName");
            strSql.Append(" where WebPageDataAccessID=@WebPageDataAccessID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WebPageDataAccessID", SqlDbType.Int,4),
					new SqlParameter("@WebPageID", SqlDbType.Int,4),
					new SqlParameter("@DataAccessActionID", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowVersion", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.WebPageDataAccessID;
            parameters[1].Value = model.WebPageID;
            parameters[2].Value = model.DataAccessActionID;
            parameters[3].Value = model.WorkFlowVersion;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.Creator;
            parameters[6].Value = model.CreatorName;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="WebPageDataAccessID"></param>
        /// <returns>返回删除数据数量</returns>
        public int Delete(int WebPageDataAccessID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ESP_WebPageDataAccess ");
            strSql.Append(" where WebPageDataAccessID=@WebPageDataAccessID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WebPageDataAccessID", SqlDbType.Int,4)};
            parameters[0].Value = WebPageDataAccessID;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 获取某个对象
        /// </summary>
        /// <param name="WebPageDataAccessID">某个主键值</param>
        /// <returns>返回拥有指定主键的对象</returns>
        public ESP.DataAccessAuthorization.Entity.WebPageDataAccess GetModel(int WebPageDataAccessID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 WebPageDataAccessID,WebPageID,DataAccessActionID,WorkFlowVersion,CreateTime,Creator,CreatorName from ESP_WebPageDataAccess ");
            strSql.Append(" where WebPageDataAccessID=@WebPageDataAccessID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WebPageDataAccessID", SqlDbType.Int,4)};
            parameters[0].Value = WebPageDataAccessID;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return Framework.DataAccess.Utilities.CBO.LoadObject<Entity.WebPageDataAccess>(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 根据查询条件获取对象的List
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns>返回对象List</returns>
        public IList<ESP.DataAccessAuthorization.Entity.WebPageDataAccess> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WebPageDataAccessID,WebPageID,DataAccessActionID,WorkFlowVersion,CreateTime,Creator,CreatorName ");
            strSql.Append(" FROM ESP_WebPageDataAccess ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere.Replace("\r\n", "").Replace("\'", "\'\'").Replace(";", ""));
            }

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            return Framework.DataAccess.Utilities.CBO.LoadList<Entity.WebPageDataAccess>(db.ExecuteReader(cmd));
        }

        #endregion

        /// <summary>
        /// 获取当前页的Action列表
        /// </summary>
        /// <param name="webPageId"></param>
        /// <returns></returns>
        public IList<Entity.DataAccessAction> GetDataAccessActionList(int webPageId)
        {
            string sql = "select a.* from ESP_DataAccessAction a inner join ESP_WebPageDataAccess b on a.DataAccessActionID = b.DataAccessActionID where b.WebPageID=" + webPageId;
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return Framework.DataAccess.Utilities.CBO.LoadList<Entity.DataAccessAction>(db.ExecuteReader(cmd));
        }
    }
}
