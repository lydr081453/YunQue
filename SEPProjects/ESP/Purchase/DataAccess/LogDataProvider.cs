using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类LogDataProvider。
    /// </summary>
    public class LogDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogDataProvider"/> class.
        /// </summary>
        public LogDataProvider()
        {
        }

        #region Method
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(LogInfo model, System.Web.HttpRequest request)
        {
            return Add(model, request, null, null);
        }

        public int Add(LogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Log(");
            strSql.Append("GId,Des,LogUserId,LogModifiedTime,IpAddress,Status)");
            strSql.Append(" values (");
            strSql.Append("@GId,@Des,@LogUserId,@LogModifiedTime,@IpAddress,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LogId", SqlDbType.Int,8),
					new SqlParameter("@GId", SqlDbType.Int,6),
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@LogUserId", SqlDbType.Int,6),
					new SqlParameter("@LogModifiedTime", SqlDbType.DateTime),
                    new SqlParameter("@IpAddress",SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.LogId;
            parameters[1].Value = model.Gid;
            parameters[2].Value = model.Des;
            parameters[3].Value = model.LogUserId;
            parameters[4].Value = model.LogMedifiedTeme;
            parameters[5].Value = "";
            parameters[6].Value = State.log_status_normal;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),null,null, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public int Add(LogInfo model, System.Web.HttpRequest request, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Log(");
            strSql.Append("GId,Des,LogUserId,LogModifiedTime,IpAddress,Status)");
            strSql.Append(" values (");
            strSql.Append("@GId,@Des,@LogUserId,@LogModifiedTime,@IpAddress,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LogId", SqlDbType.Int,8),
					new SqlParameter("@GId", SqlDbType.Int,6),
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@LogUserId", SqlDbType.Int,6),
					new SqlParameter("@LogModifiedTime", SqlDbType.DateTime),
                    new SqlParameter("@IpAddress",SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.LogId;
            parameters[1].Value = model.Gid;
            parameters[2].Value = model.Des;
            parameters[3].Value = model.LogUserId;
            parameters[4].Value = model.LogMedifiedTeme;
            if (request == null)
            {
                parameters[5].Value = "";
            }
            else
                parameters[5].Value = request.UserHostAddress.Trim();

            parameters[6].Value = State.log_status_normal;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Add(LogInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Log(");
            strSql.Append("GId,Des,LogUserId,LogModifiedTime,IpAddress,Status)");
            strSql.Append(" values (");
            strSql.Append("@GId,@Des,@LogUserId,@LogModifiedTime,@IpAddress,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LogId", SqlDbType.Int,8),
					new SqlParameter("@GId", SqlDbType.Int,6),
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@LogUserId", SqlDbType.Int,6),
					new SqlParameter("@LogModifiedTime", SqlDbType.DateTime),
                    new SqlParameter("@IpAddress",SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.LogId;
            parameters[1].Value = model.Gid;
            parameters[2].Value = model.Des;
            parameters[3].Value = model.LogUserId;
            parameters[4].Value = model.LogMedifiedTeme;
            parameters[5].Value = "";
            parameters[6].Value = State.log_status_normal;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LogId,GId,Des,LogUserId,LogModifiedTime,Status,IpAddress ");
            strSql.Append(" FROM T_Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" Order By LogModifiedTime");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// Gets the loglist.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<LogInfo> GetLoglist(string strWhere, List<SqlParameter> parms)
        {
            List<LogInfo> list = new List<LogInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("select a.LogId,a.GId,a.Des,a.LogUserId,a.LogModifiedTime,a.Status,a.IpAddress,b.prNo ");
            strB.Append(" FROM T_Log as a inner join T_GeneralInfo as b on a.GId = b.id");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by a.Gid desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    LogInfo c = new LogInfo();
                    c.PopupData(r);
                    c.PrNo = r["prNo"].ToString();
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static LogInfo GetModel(int logId)
        {
            StringBuilder strB = new StringBuilder();
            strB.Append("select LogId,GId,Des,LogUserId,LogModifiedTime,Status,IpAddress ");
            strB.Append(" FROM T_Log where logId=" + logId.ToString());
            DataSet ds = DbHelperSQL.Query(strB.ToString());
            LogInfo model = new LogInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.LogId = int.Parse(ds.Tables[0].Rows[0]["LogId"].ToString());
                model.Gid = int.Parse(ds.Tables[0].Rows[0]["GId"].ToString());
                model.LogUserId = int.Parse(ds.Tables[0].Rows[0]["LogUserId"].ToString());
                model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                model.Des = ds.Tables[0].Rows[0]["Des"].ToString();
                model.IpAddress = ds.Tables[0].Rows[0]["IpAddress"].ToString();
                model.LogMedifiedTeme = Convert.ToDateTime(ds.Tables[0].Rows[0]["LogModifiedTime"].ToString());
            }
            return model;
        }

        public static int Update(LogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Log set ");
            strSql.Append("Des=@Des where LogId=@LogId");
            SqlParameter[] parameters = {
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@LogId", SqlDbType.Int,4)
                                          };
            parameters[0].Value = model.Des;
            parameters[1].Value = model.LogId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Gets the loglist by G id.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<LogInfo> GetLoglistByGId(string strWhere)
        {
            List<LogInfo> list = new List<LogInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("select LogId,GId,Des,LogUserId,LogModifiedTime,Status,IpAddress ");
            strB.Append(" FROM T_Log ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by LogId", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    LogInfo c = new LogInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Deletes the specified STR where.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        public void Delete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Delete ");
            strSql.Append(" FROM T_Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// Cancels the specified STR where.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        public void Cancel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update T_Log SET ");
            strSql.Append(" Status= " + State.log_status_cancel.ToString());
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion Method
    }
}