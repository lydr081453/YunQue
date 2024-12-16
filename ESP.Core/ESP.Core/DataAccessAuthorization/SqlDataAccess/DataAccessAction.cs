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
    /// 操作权限 SQL Server 操作类
    /// </summary>
    public class DataAccessAction : DataAccess.IDataAccessAction
    {
        #region IDataAccessAction 成员
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(ESP.DataAccessAuthorization.Entity.DataAccessAction model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ESP_DataAccessAction(");
            strSql.Append("ActionName,Action,ActionIdentity,ActionType,CustomAction,Description,AccessService,CreateTime,Creator,CreatorName)");
            strSql.Append(" values (");
            strSql.Append("@ActionName,@Action,@ActionIdentity,@ActionType,@CustomAction,@Description,@AccessService,@CreateTime,@Creator,@CreatorName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ActionName", SqlDbType.NVarChar,50),
					new SqlParameter("@Action", SqlDbType.NVarChar,50),
					new SqlParameter("@ActionIdentity", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ActionType", SqlDbType.Int,4),
					new SqlParameter("@CustomAction", SqlDbType.Bit,1),
					new SqlParameter("@Description", SqlDbType.NVarChar,50),
					new SqlParameter("@AccessService", SqlDbType.NVarChar,400),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ActionName;
            parameters[1].Value = model.Action;
            parameters[2].Value = model.ActionIdentity;
            parameters[3].Value = model.ActionType;
            parameters[4].Value = model.CustomAction;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.AccessService;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.Creator;
            parameters[9].Value = model.CreatorName;

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
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回更新数据数量</returns>
        public int Update(ESP.DataAccessAuthorization.Entity.DataAccessAction model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ESP_DataAccessAction set ");
            strSql.Append("ActionName=@ActionName,");
            strSql.Append("Action=@Action,");
            strSql.Append("ActionIdentity=@ActionIdentity,");
            strSql.Append("ActionType=@ActionType,");
            strSql.Append("CustomAction=@CustomAction,");
            strSql.Append("Description=@Description,");
            strSql.Append("AccessService=@AccessService,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatorName=@CreatorName");
            strSql.Append(" where DataAccessActionID=@DataAccessActionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DataAccessActionID", SqlDbType.Int,4),
					new SqlParameter("@ActionName", SqlDbType.NVarChar,50),
					new SqlParameter("@Action", SqlDbType.NVarChar,50),
					new SqlParameter("@ActionIdentity", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ActionType", SqlDbType.Int,4),
					new SqlParameter("@CustomAction", SqlDbType.Bit,1),
					new SqlParameter("@Description", SqlDbType.NVarChar,50),
					new SqlParameter("@AccessService", SqlDbType.NVarChar,400),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.DataAccessActionID;
            parameters[1].Value = model.ActionName;
            parameters[2].Value = model.Action;
            parameters[3].Value = model.ActionIdentity;
            parameters[4].Value = model.ActionType;
            parameters[5].Value = model.CustomAction;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.AccessService;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.Creator;
            parameters[10].Value = model.CreatorName;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="DataAccessActionID"></param>
        /// <returns>返回删除数据数量</returns>
        public int Delete(int DataAccessActionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ESP_DataAccessAction ");
            strSql.Append(" where DataAccessActionID=@DataAccessActionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DataAccessActionID", SqlDbType.Int,4)};
            parameters[0].Value = DataAccessActionID;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取某个对象
        /// </summary>
        /// <param name="DataAccessActionID">某个主键值</param>
        /// <returns>返回拥有指定主键的对象</returns>
        public ESP.DataAccessAuthorization.Entity.DataAccessAction GetModel(int DataAccessActionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 DataAccessActionID,ActionName,Action,ActionIdentity,ActionType,CustomAction,Description,AccessService,CreateTime,Creator,CreatorName from ESP_DataAccessAction ");
            strSql.Append(" where DataAccessActionID=@DataAccessActionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DataAccessActionID", SqlDbType.Int,4)};
            parameters[0].Value = DataAccessActionID;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return Framework.DataAccess.Utilities.CBO.LoadObject<Entity.DataAccessAction>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 根据查询条件获取对象的List
        /// </summary>
        /// <param name="strWhere">条件参数（这个函数有安全漏洞，需要严格控制strWhere，不能把用户的输入直接传递给strWhere变量）</param>
        /// <returns>返回对象List</returns>
        public IList<ESP.DataAccessAuthorization.Entity.DataAccessAction> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DataAccessActionID,ActionName,Action,ActionIdentity,ActionType,CustomAction,Description,AccessService,CreateTime,Creator,CreatorName ");
            strSql.Append(" FROM ESP_DataAccessAction ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere.Replace("\r\n", "").Replace("\'", "\'\'").Replace(";", ""));
            }

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            return Framework.DataAccess.Utilities.CBO.LoadList<Entity.DataAccessAction>(db.ExecuteReader(cmd));
        }

        #endregion

        /// <summary>
        /// 获取Action的Member列表
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public IList<Entity.DataAccessMember> GetDataAccessMemberList(int actionId)
        {
            string sql = "select a.* from ESP_DataAccessMember a inner join ESP_DataAccessActionMember b on a.DataAccessMemberID = b.DataAccessMemberID where b.DataAccessActionID=" + actionId;
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return Framework.DataAccess.Utilities.CBO.LoadList<Entity.DataAccessMember>(db.ExecuteReader(cmd));
        }
    }
}
