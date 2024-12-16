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
    public class UserPointDataProvider:IUserPointDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public UserPointDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.UserPoint.Entity.UserPointInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_UserPoint(");
            strSql.Append("UserID,SP)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@SP)");

            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@UserID", System.Data.DbType.Int32, model.UserID);
            db.AddInParameter(cmd, "@SP", System.Data.DbType.Int32, model.SP);

            object obj = db.ExecuteNonQuery(cmd);

            return obj == null ? 0 : (int)obj;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.UserPoint.Entity.UserPointInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_UserPoint set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("SP=@SP");
            strSql.Append(" where ID=@ID");
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@UserID", System.Data.DbType.Int32, model.UserID);
            db.AddInParameter(cmd, "@SP", System.Data.DbType.Int32, model.SP);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_UserPoint ");
            strSql.Append(" where UserId=@UserId");
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@UserId", System.Data.DbType.Int32, UserId);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.UserPoint.Entity.UserPointInfo GetModel(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_UserPoint ");
            strSql.Append(" where UserId=@UserId");

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@UserId", System.Data.DbType.Int32, UserId);
            return CBO.LoadObject<UserPointInfo>(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<UserPointInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [UserID],[SP],[TimeStamp] ");
            strSql.Append(" FROM T_UserPoint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            return CBO.LoadList<UserPointInfo>(db.ExecuteReader(cmd));
        }

        #endregion  成员方法
    }
}

