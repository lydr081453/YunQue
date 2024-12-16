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
    public class UserPointRecordDataProvider:IUserPointRecordDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public UserPointRecordDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.UserPoint.Entity.UserPointRecordInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            int obj = 0;
            //userpointrecord insert
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_UserPointRecord(");
            strSql.Append("UserID,RuleID,GiftID,Points,Memo,OperationTime,RefrenceID)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@RuleID,@GiftID,@Points,@Memo,@OperationTime,RefrenceID)");

            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(cmd, "@UserID", System.Data.DbType.Int32, model.UserID);
            db.AddInParameter(cmd, "@RuleID", System.Data.DbType.Int32, model.RuleID);
            db.AddInParameter(cmd, "@GiftID", System.Data.DbType.Int32, model.GiftID);
            db.AddInParameter(cmd, "@Points", System.Data.DbType.Int32, model.Points);
            db.AddInParameter(cmd, "@Memo", System.Data.DbType.String, model.Memo);
            db.AddInParameter(cmd, "@OperationTime", System.Data.DbType.DateTime, model.OperationTime);
            db.AddInParameter(cmd, "@RefrenceID", System.Data.DbType.Int32, model.RefrenceID);

            //userpoint count
            StringBuilder strsql2 = new StringBuilder();
            strsql2.Append("select count(*) from t_UserPoint where UserId=@UserId");
            DbCommand cmd2 = db.GetSqlStringCommand(strsql2.ToString());
            db.AddInParameter(cmd2, "@UserID", System.Data.DbType.Int32, model.UserID);
            int userPointCount = Convert.ToInt32(db.ExecuteScalar(cmd2));

            //userpoint insert
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into T_UserPoint(");
            strSql3.Append("UserID,SP)");
            strSql3.Append(" values (");
            strSql3.Append("@UserID,@SP)");
            strSql3.Append(";select CAST(SCOPE_IDENTITY() AS int)");
            DbCommand cmd3 = db.GetSqlStringCommand(strSql3.ToString());
            db.AddInParameter(cmd3, "@UserID", System.Data.DbType.Int32, model.UserID);
            db.AddInParameter(cmd3, "@SP", System.Data.DbType.Int32, model.Points);

            //userpint update
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("update T_UserPoint set ");
            strSql4.Append("SP=SP+@SP");
            strSql4.Append(" where UserID=@UserID");
            DbCommand cmd4 = db.GetSqlStringCommand(strSql4.ToString());
            db.AddInParameter(cmd4, "@SP", System.Data.DbType.Int32, model.Points);
            db.AddInParameter(cmd4, "@UserID", System.Data.DbType.Int32, model.UserID);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                using (DbTransaction trans = connection.BeginTransaction())
                {
                    try
                    {
                        obj += db.ExecuteNonQuery(cmd, trans);
                        if (userPointCount > 0)
                        {//update
                            obj += db.ExecuteNonQuery(cmd4, trans);
                        }
                        else
                        { //insert
                            obj += db.ExecuteNonQuery(cmd3, trans);
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
            return obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modellist"></param>
        /// <returns></returns>
        public int Add(IList<ESP.UserPoint.Entity.UserPointRecordInfo> modellist)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            int obj = 0;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                using (DbTransaction trans = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (ESP.UserPoint.Entity.UserPointRecordInfo model in modellist)
                        {
                            //userpointrecord insert
                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("insert into T_UserPointRecord(");
                            strSql.Append("UserID,RuleID,GiftID,Points,Memo,OperationTime,RefrenceID)");
                            strSql.Append(" values (");
                            strSql.Append("@UserID,@RuleID,@GiftID,@Points,@Memo,@OperationTime,@RefrenceID)");

                            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
                            db.AddInParameter(cmd, "@UserID", System.Data.DbType.Int32, model.UserID);
                            db.AddInParameter(cmd, "@RuleID", System.Data.DbType.Int32, model.RuleID);
                            db.AddInParameter(cmd, "@GiftID", System.Data.DbType.Int32, model.GiftID);
                            db.AddInParameter(cmd, "@Points", System.Data.DbType.Int32, model.Points);
                            db.AddInParameter(cmd, "@Memo", System.Data.DbType.String, model.Memo);
                            db.AddInParameter(cmd, "@OperationTime", System.Data.DbType.DateTime, model.OperationTime);
                            db.AddInParameter(cmd, "@RefrenceID", System.Data.DbType.Int32, model.RefrenceID);

                            //userpoint count
                            StringBuilder strsql2 = new StringBuilder();
                            strsql2.Append("select count(*) from t_UserPoint where UserId=@UserId");
                            DbCommand cmd2 = db.GetSqlStringCommand(strsql2.ToString());
                            db.AddInParameter(cmd2, "@UserID", System.Data.DbType.Int32, model.UserID);
                            int userPointCount = Convert.ToInt32(db.ExecuteScalar(cmd2,trans));

                            //userpoint insert
                            StringBuilder strSql3 = new StringBuilder();
                            strSql3.Append("insert into T_UserPoint(");
                            strSql3.Append("UserID,SP)");
                            strSql3.Append(" values (");
                            strSql3.Append("@UserID,@SP)");
                            strSql3.Append(";select CAST(SCOPE_IDENTITY() AS int)");
                            DbCommand cmd3 = db.GetSqlStringCommand(strSql3.ToString());
                            db.AddInParameter(cmd3, "@UserID", System.Data.DbType.Int32, model.UserID);
                            db.AddInParameter(cmd3, "@SP", System.Data.DbType.Int32, model.Points);

                            //userpint update
                            StringBuilder strSql4 = new StringBuilder();
                            strSql4.Append("update T_UserPoint set ");
                            strSql4.Append("SP=SP+@SP");
                            strSql4.Append(" where UserID=@UserID");
                            DbCommand cmd4 = db.GetSqlStringCommand(strSql4.ToString());
                            db.AddInParameter(cmd4, "@SP", System.Data.DbType.Int32, model.Points);
                            db.AddInParameter(cmd4, "@UserID", System.Data.DbType.Int32, model.UserID);


                            obj += db.ExecuteNonQuery(cmd, trans);
                            if (userPointCount > 0)
                            {//update
                                obj += db.ExecuteNonQuery(cmd4, trans);
                            }
                            else
                            { //insert
                                obj += db.ExecuteNonQuery(cmd3, trans);
                            }
                          
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
            return obj;
        }
      
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<UserPointRecordInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [UserID],[RuleID],[GiftID],[Points],[Memo],[OperationTime],[TimeStamp],[RefrenceID] ");
            strSql.Append(" FROM T_UserPointRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            return CBO.LoadList<UserPointRecordInfo>(db.ExecuteReader(cmd));
        }

        #endregion  成员方法
    }
}
