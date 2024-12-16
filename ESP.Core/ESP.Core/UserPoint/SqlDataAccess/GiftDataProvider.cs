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
    public class GiftDataProvider:IGiftDataProvider
    {
        #region IGiftDataProvider 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(GiftInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Gift(");
            strSql.Append("Code,Name,Description,Points,Count,State,CreateTime,Creator,Images)");
            strSql.Append(" values (");
            strSql.Append("@Code,@Name,@Description,@Points,@Count,@State,@CreateTime,@Creator,@Images)");
            strSql.Append(";select CAST(SCOPE_IDENTITY() AS int)");
            
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@Code", System.Data.DbType.String, model.Code);
            db.AddInParameter(cmd, "@Name", System.Data.DbType.String, model.Name);
            db.AddInParameter(cmd, "@Description", System.Data.DbType.String, model.Description);
            db.AddInParameter(cmd, "@Points", System.Data.DbType.Int32, model.Points);
            db.AddInParameter(cmd, "@Count", System.Data.DbType.Int32, model.Count);
            db.AddInParameter(cmd, "@State", System.Data.DbType.Int32, model.State);
            db.AddInParameter(cmd, "@CreateTime", System.Data.DbType.DateTime, model.CreateTime);
            db.AddInParameter(cmd, "@Creator", System.Data.DbType.Int32, model.Creator);
            db.AddInParameter(cmd, "@Images", System.Data.DbType.String, model.Images);


            object obj = db.ExecuteScalar(cmd);

            return obj == null ? 0 : (int)obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(GiftInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Gift set ");
            strSql.Append("Code=@Code,");
            strSql.Append("Name=@Name,");
            strSql.Append("Description=@Description,");
            strSql.Append("Points=@Points,");
            strSql.Append("Count=@Count,");
            strSql.Append("State=@State,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Creator=@Creator,Images=@Images");
            strSql.Append(" where GiftID=@GiftID");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());

            db.AddInParameter(cmd, "@GiftID", System.Data.DbType.Int32, model.GiftID);
            db.AddInParameter(cmd, "@Code", System.Data.DbType.String, model.Code);
            db.AddInParameter(cmd, "@Name", System.Data.DbType.String, model.Name);
            db.AddInParameter(cmd, "@Description", System.Data.DbType.String, model.Description);
            db.AddInParameter(cmd, "@Points", System.Data.DbType.Int32, model.Points);
            db.AddInParameter(cmd, "@Count", System.Data.DbType.Int32, model.Count);
            db.AddInParameter(cmd, "@State", System.Data.DbType.Int32, model.State);
            db.AddInParameter(cmd, "@CreateTime", System.Data.DbType.DateTime, model.CreateTime);
            db.AddInParameter(cmd, "@Creator", System.Data.DbType.Int32, model.Creator);
            db.AddInParameter(cmd, "@Images", System.Data.DbType.String, model.Images);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="GiftID"></param>
        /// <returns></returns>
        public int Delete(int GiftID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Gift ");
            strSql.Append(" where GiftID=@GiftID");
            SqlParameter[] parameters = {
					new SqlParameter("@GiftID", SqlDbType.Int,4)
				};
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@GiftID", System.Data.DbType.Int32, GiftID);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="GiftID"></param>
        /// <returns></returns>
        public GiftInfo GetModel(int GiftID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Gift ");
            strSql.Append(" where GiftID=@GiftID");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@GiftID", System.Data.DbType.Int32, GiftID);
            return CBO.LoadObject<GiftInfo>(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public IList<GiftInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [GiftID],[Code],[Name],[Description],[Points],[Count],[State],[CreateTime],[Creator],[Images],[TimeStamp] ");
            strSql.Append(" FROM T_Gift ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            return CBO.LoadList<GiftInfo>(db.ExecuteReader(cmd));
        }

        #endregion
    }
}
