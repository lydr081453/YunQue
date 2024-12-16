using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.UserPoint.DataAccess;
using ESP.UserPoint.Entity;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ESP.UserPoint.DataAccess.Utilities;
namespace ESP.UserPoint.SqlDataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class UserPointRuleDataProvider:IUserPointRuleDataProvider
    {
        #region IUserPointRuleDataProvider 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(UserPointRuleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_UserPointRule(");
            strSql.Append("RuleName,RuleKey,Description,Points)");
            strSql.Append(" values (");
            strSql.Append("@RuleName,@RuleKey,@Description,@Points)");
            strSql.Append(";select CAST(SCOPE_IDENTITY() AS int)");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@RuleName", System.Data.DbType.String, model.RuleName);
            db.AddInParameter(cmd, "@RuleKey", System.Data.DbType.String, model.RuleKey);
            db.AddInParameter(cmd, "@Description", System.Data.DbType.String, model.Description);
            db.AddInParameter(cmd, "@Points", System.Data.DbType.Int32, model.Points);

            object obj = db.ExecuteScalar(cmd);

            return obj == null ? 0 : (int)obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(UserPointRuleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_UserPointRule set ");
            strSql.Append("RuleName=@RuleName,");
            strSql.Append("RuleKey=@RuleKey,");
            strSql.Append("Description=@Description,");
            strSql.Append("Points=@Points");
            strSql.Append(" where RuleID=@RuleID");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());

            db.AddInParameter(cmd, "@RuleID", System.Data.DbType.Int32, model.RuleID);
            db.AddInParameter(cmd, "@RuleName", System.Data.DbType.String, model.RuleName);
            db.AddInParameter(cmd, "@RuleKey", System.Data.DbType.String, model.RuleKey);
            db.AddInParameter(cmd, "@Description", System.Data.DbType.String, model.Description);
            db.AddInParameter(cmd, "@Points", System.Data.DbType.Int32, model.Points);

            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RuleID"></param>
        /// <returns></returns>
        public int Delete(int RuleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_UserPointRule ");
            strSql.Append(" where RuleID=@RuleID");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleID", SqlDbType.Int,4)
				};
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@RuleID", System.Data.DbType.Int32, RuleID);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RuleID"></param>
        /// <returns></returns>
        public UserPointRuleInfo GetModel(int RuleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_UserPointRule ");
            strSql.Append(" where RuleID=@RuleID");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleID", SqlDbType.Int,4)};

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@RuleID", System.Data.DbType.Int32, RuleID);
            return CBO.LoadObject<UserPointRuleInfo>(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keycode"></param>
        /// <returns></returns>
        public ESP.UserPoint.Entity.UserPointRuleInfo GetModelByKey(string keycode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_UserPointRule ");
            strSql.Append(" where RuleKey=@RuleKey");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleKey", SqlDbType.NVarChar,50)};

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@RuleKey", System.Data.DbType.String, keycode);
            return CBO.LoadObject<UserPointRuleInfo>(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public IList<UserPointRuleInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [RuleID],[RuleName],[RuleKey],[Description],[Points] ");
            strSql.Append(" FROM T_UserPointRule ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            return CBO.LoadList<UserPointRuleInfo>(db.ExecuteReader(cmd));
        }

        #endregion
    }
}
