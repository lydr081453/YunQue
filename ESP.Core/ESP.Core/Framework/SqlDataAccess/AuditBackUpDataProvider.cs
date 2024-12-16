using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 代理审核人数据访问类
    /// </summary>
    public class AuditBackUpDataProvider : IAuditBackUpDataProvider
    {
        #region IAuditBackUpDataProvider 成员

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(AuditBackUpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_AuditBackUp(");
            strSql.Append("UserID,BackupUserID,BeginDate,EndDate,status,type)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@BackupUserID,@BeginDate,@EndDate,@Status,@Type)");
            strSql.Append(";select CAST(SCOPE_IDENTITY() AS int)");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@UserID", System.Data.DbType.Int32, model.UserID);
            db.AddInParameter(cmd, "@BackupUserID", System.Data.DbType.Int32, model.BackupUserID);
            db.AddInParameter(cmd, "@BeginDate", System.Data.DbType.DateTime, model.BeginDate);
            db.AddInParameter(cmd, "@EndDate", System.Data.DbType.DateTime, model.EndDate);
            db.AddInParameter(cmd, "@Status", System.Data.DbType.Int32, model.Status);
            db.AddInParameter(cmd, "@Type", System.Data.DbType.Int32, model.Type);
            object obj = db.ExecuteScalar(cmd);
            
            return obj == null ? 0 : (int)obj;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Update(AuditBackUpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_AuditBackUp set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("BackupUserID=@BackupUserID,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("Type=@Type");
            strSql.Append(" where ID=@ID");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@ID", System.Data.DbType.Int32, model.ID);
            db.AddInParameter(cmd, "@UserID", System.Data.DbType.Int32, model.UserID);
            db.AddInParameter(cmd, "@BackupUserID", System.Data.DbType.Int32, model.BackupUserID);
            db.AddInParameter(cmd, "@BeginDate", System.Data.DbType.DateTime, model.BeginDate);
            db.AddInParameter(cmd, "@EndDate", System.Data.DbType.DateTime, model.EndDate);
            db.AddInParameter(cmd, "@Status", System.Data.DbType.Int32, model.Status);
            db.AddInParameter(cmd, "@Type", System.Data.DbType.Int32, model.Type);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_AuditBackUp ");
            strSql.Append(" where ID=@ID");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@ID", System.Data.DbType.Int32, id);
            db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        public void DeleteByUserId(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_AuditBackUp ");
            strSql.Append(" where userid=@userid");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@userid", System.Data.DbType.Int32, userid);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public AuditBackUpInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sep_AuditBackUp ");
            strSql.Append(" where ID=@ID");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@ID", System.Data.DbType.Int32, id);
            return CBO.LoadObject<AuditBackUpInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public AuditBackUpInfo GetModelByUserID(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from sep_AuditBackUp ");
            strSql.Append(" where BeginDate < GETDATE() and  EndDate > GETDATE() and UserID=@UserID and status=1 and type=1");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@UserID", System.Data.DbType.Int32, userId);
            return CBO.LoadObject<AuditBackUpInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 检查是否是可用代初审人
        /// </summary>
        /// <param name="sysUserId">The sys user id.</param>
        /// <returns>
        /// 	<c>true</c> if [is back up user] [the specified sys user id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBackUpUser(int sysUserId)
        {
            AuditBackUpInfo model = GetModelByUserID(sysUserId);
            if (model != null && model.BeginDate <= DateTime.Today && DateTime.Today <= model.EndDate)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[UserID],[BackupUserID],[BeginDate],[EndDate],[Status],[Type] ");
            strSql.Append(" FROM sep_AuditBackUp ");
            if (strWhere != null && strWhere.Trim().Length > 0)
            {
                strSql.Append(" where " + strWhere);
            }

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据BackUpUserID获取代理设置对象列表
        /// </summary>
        /// <param name="backUpUserId">要获取的代理设置记录的BackUpUserID</param>
        /// <returns>代理设置记录对象列表</returns>
        public IList<AuditBackUpInfo> GetModelsByBackUpUserID(int backUpUserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sep_AuditBackUp ");
            strSql.Append(" where BeginDate < GETDATE() and  EndDate > GETDATE() and BackupUserID=@BackupUserID and status=1 and type=1");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@BackupUserID", System.Data.DbType.Int32, backUpUserId);
            return CBO.LoadList<AuditBackUpInfo>(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 获取离职委托实例
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AuditBackUpInfo GetLayOffModelByUserID(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from sep_AuditBackUp ");
            strSql.Append(" where UserID=@UserID and status=1 and type=2");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@UserID", System.Data.DbType.Int32, userId);
            return CBO.LoadObject<AuditBackUpInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取离职委托列表
        /// </summary>
        /// <param name="backUpUserId"></param>
        /// <returns></returns>
        public IList<AuditBackUpInfo> GetLayOffModelsByBackUpUserID(int backUpUserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sep_AuditBackUp ");
            strSql.Append(" where BackupUserID=@BackupUserID and status=1 and type=2");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@BackupUserID", System.Data.DbType.Int32, backUpUserId);
            return CBO.LoadList<AuditBackUpInfo>(db.ExecuteReader(cmd));
        }
        #endregion
    }
}
