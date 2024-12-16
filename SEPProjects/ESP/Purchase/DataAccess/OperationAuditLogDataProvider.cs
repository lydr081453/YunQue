using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类OperationAuditLogDataProvider。
    /// </summary>
    public class OperationAuditLogDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationAuditLogDataProvider"/> class.
        /// </summary>
        public OperationAuditLogDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_OperationAuditLog");
            strSql.Append(" where ID= @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(OperationAuditLogInfo model,System.Web.HttpRequest request)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_OperationAuditLog(");
            strSql.Append("Gid,auditorId,auditorName,auditTime,audtiStatus,auditRemark,IpAddress)");
            strSql.Append(" values (");
            strSql.Append("@Gid,@auditorId,@auditorName,@auditTime,@audtiStatus,@auditRemark,@IpAddress)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4),
					new SqlParameter("@auditorId", SqlDbType.Int,4),
					new SqlParameter("@auditorName", SqlDbType.VarChar,100),
					new SqlParameter("@auditTime", SqlDbType.DateTime),
					new SqlParameter("@audtiStatus", SqlDbType.Int,4),
					new SqlParameter("@auditRemark", SqlDbType.NVarChar),
                    new SqlParameter("@IpAddress",SqlDbType.VarChar,20)                                        
                                        };
            parameters[0].Value = model.Gid;
            parameters[1].Value = model.auditorId;
            parameters[2].Value = model.auditorName;
            parameters[3].Value = model.auditTime;
            parameters[4].Value = model.audtiStatus;
            parameters[5].Value = model.auditRemark;
            parameters[6].Value = request.UserHostAddress.Trim();

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(OperationAuditLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OperationAuditLog set ");
            strSql.Append("Gid=@Gid,");
            strSql.Append("auditorId=@auditorId,");
            strSql.Append("auditorName=@auditorName,");
            strSql.Append("auditTime=@auditTime,");
            strSql.Append("audtiStatus=@audtiStatus,");
            strSql.Append("auditRemark=@auditRemark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Gid", SqlDbType.Int,4),
					new SqlParameter("@auditorId", SqlDbType.Int,4),
					new SqlParameter("@auditorName", SqlDbType.VarChar,100),
					new SqlParameter("@auditTime", SqlDbType.DateTime),
					new SqlParameter("@audtiStatus", SqlDbType.Int,4),
					new SqlParameter("@auditRemark", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Gid;
            parameters[2].Value = model.auditorId;
            parameters[3].Value = model.auditorName;
            parameters[4].Value = model.auditTime;
            parameters[5].Value = model.audtiStatus;
            parameters[6].Value = model.auditRemark;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="ID">The ID.</param>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OperationAuditLog ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除申请单的业务审核日志
        /// </summary>
        /// <param name="gid"></param>
        public void DeleteByGid(int gid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OperationAuditLog ");
            strSql.Append(" where GID=@GID");
            SqlParameter[] parameters = {
					new SqlParameter("@GID", SqlDbType.Int,4)
				};
            parameters[0].Value = gid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据gid删除业务审核数据
        /// </summary>
        /// <param name="gid">The gid.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        public void DeleteByGid(int gid, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OperationAuditLog ");
            strSql.Append(" where Gid=@Gid");
            SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4)
				};
            parameters[0].Value = gid;
            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public OperationAuditLogInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_OperationAuditLog ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            OperationAuditLogInfo model = new OperationAuditLogInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = ID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Gid"].ToString() != "")
                {
                    model.Gid = int.Parse(ds.Tables[0].Rows[0]["Gid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["auditorId"].ToString() != "")
                {
                    model.auditorId = int.Parse(ds.Tables[0].Rows[0]["auditorId"].ToString());
                }
                model.auditorName = ds.Tables[0].Rows[0]["auditorName"].ToString();
                if (ds.Tables[0].Rows[0]["auditTime"].ToString() != "")
                {
                    model.auditTime = DateTime.Parse(ds.Tables[0].Rows[0]["auditTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["audtiStatus"].ToString() != "")
                {
                    model.audtiStatus = int.Parse(ds.Tables[0].Rows[0]["audtiStatus"].ToString());
                }
                model.auditRemark = ds.Tables[0].Rows[0]["auditRemark"].ToString();
                model.IpAddress = ds.Tables[0].Rows[0]["IpAddress"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["IpAddress"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得申请单的业务审核信息
        /// </summary>
        /// <param name="gid">申请单ID</param>
        /// <param name="auditorId">业务审核人ID</param>
        /// <returns></returns>
        public OperationAuditLogInfo GetModel(int gid, int auditorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_OperationAuditLog ");
            strSql.Append(" where gid=@GID and auditorId=@auditorId order by id desc");
            SqlParameter[] parameters = {
					new SqlParameter("@GID", SqlDbType.Int,4),
                    new SqlParameter("@auditorId",SqlDbType.Int,4)};
            parameters[0].Value = gid;
            parameters[1].Value = auditorId;
            OperationAuditLogInfo model = new OperationAuditLogInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ID = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                if (ds.Tables[0].Rows[0]["Gid"].ToString() != "")
                {
                    model.Gid = int.Parse(ds.Tables[0].Rows[0]["Gid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["auditorId"].ToString() != "")
                {
                    model.auditorId = int.Parse(ds.Tables[0].Rows[0]["auditorId"].ToString());
                }
                model.auditorName = ds.Tables[0].Rows[0]["auditorName"].ToString();
                if (ds.Tables[0].Rows[0]["auditTime"].ToString() != "")
                {
                    model.auditTime = DateTime.Parse(ds.Tables[0].Rows[0]["auditTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["audtiStatus"].ToString() != "")
                {
                    model.audtiStatus = int.Parse(ds.Tables[0].Rows[0]["audtiStatus"].ToString());
                }
                model.auditRemark = ds.Tables[0].Rows[0]["auditRemark"].ToString();
                model.IpAddress = ds.Tables[0].Rows[0]["IpAddress"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["IpAddress"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[Gid],[auditorId],[auditorName],[auditTime],[audtiStatus],[auditRemark],IpAddress");
            strSql.Append(" FROM T_OperationAuditLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得审核人的审核历史
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="auditorId"></param>
        /// <returns></returns>
        public DataSet GetAuditLog(int auditorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT     * ");
            strSql.Append("    FROM         T_OperationAuditLog as a ");
            strSql.Append("    inner join t_generalinfo as b on a.gid=b.id ");
            strSql.Append("    where b.status!=-1 and a.auditorId ="+auditorId);
            strSql.Append("    order by a.audittime desc ");

            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// Gets the loglist by G id.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<OperationAuditLogInfo> GetLoglistByGId(string strWhere)
        {
            List<OperationAuditLogInfo> list = new List<OperationAuditLogInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("select * ");
            strB.Append(" FROM T_OperationAuditLog ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by id ", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    OperationAuditLogInfo c = new OperationAuditLogInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        #endregion  成员方法
    }
}