using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    ///RecipientLogDataHelper 的摘要说明
    /// </summary>
    public class RecipientLogDataHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipientLogDataHelper"/> class.
        /// </summary>
        public RecipientLogDataHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region Method
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(RecipientLogInfo model,System.Web.HttpRequest request)
        {
            return Add(model,request, null, null);
        }

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        private int Add(RecipientLogInfo model,System.Web.HttpRequest request, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_RecipientLog(");
            strSql.Append("RId,Des,LogUserId,LogModifiedTime,Status,IpAddress)");
            strSql.Append(" values (");
            strSql.Append("@RId,@Des,@LogUserId,@LogModifiedTime,@Status,@IpAddress)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LogId", SqlDbType.Int,8),
					new SqlParameter("@RId", SqlDbType.Int,6),
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@LogUserId", SqlDbType.Int,6),
					new SqlParameter("@LogModifiedTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@IpAddress",SqlDbType.VarChar,20)
            };
            parameters[0].Value = model.LogId;
            parameters[1].Value = model.Rid;
            parameters[2].Value = model.Des;
            parameters[3].Value = model.LogUserId;
            parameters[4].Value = model.LogMedifiedTeme;
            parameters[5].Value = State.log_status_normal;
            parameters[6].Value = request.UserHostAddress.Trim();

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

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LogId,RId,Des,LogUserId,LogModifiedTime,Status ");
            strSql.Append(" FROM T_RecipientLog ");
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
        public static List<RecipientLogInfo> GetLoglist(string strWhere, List<SqlParameter> parms)
        {
            List<RecipientLogInfo> list = new List<RecipientLogInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("select a.LogId,a.RId,a.Des,a.LogUserId,a.LogModifiedTime,a.Status,a.IpAddress,b.* ");
            strB.Append(" FROM T_RecipientLog as a inner join T_Recipient as b on a.RId = b.id");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by a.RId desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    RecipientLogInfo c = new RecipientLogInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the loglist by R id.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<RecipientLogInfo> GetLoglistByRId(string strWhere)
        {
            List<RecipientLogInfo> list = new List<RecipientLogInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("select LogId,RId,Des,LogUserId,LogModifiedTime,Status,IpAddress ");
            strB.Append(" FROM T_RecipientLog ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by LogId", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    RecipientLogInfo c = new RecipientLogInfo();
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
            strSql.Append(" FROM T_RecipientLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public ESP.Purchase.Entity.RecipientLogInfo GetModel(int logId)
        {
            RecipientLogInfo c = new RecipientLogInfo();
            StringBuilder strB = new StringBuilder();
            strB.Append("select * ");
            strB.Append(" FROM T_RecipientLog ");
            strB.Append(" where LogId=" + logId.ToString());
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strB.ToString()))
            {
               
                while (r.Read())
                {
                    c.PopupData(r);
                }
                r.Close();
            }
            return c;
        }

        public int Update(RecipientLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_RecipientLog set ");
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
        /// Cancels the specified STR where.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        public void Cancel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update T_RecipientLog SET ");
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