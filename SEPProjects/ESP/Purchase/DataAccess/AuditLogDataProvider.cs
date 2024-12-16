using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类AuditLogDataProvider。
    /// </summary>
    public class AuditLogDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditLogDataProvider"/> class.
        /// </summary>
        public AuditLogDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_auditLog");
            strSql.Append(" where id= @id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AuditLogInfo model, System.Web.HttpRequest request, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_auditLog(");
            strSql.Append("gid,remark,remarkDate,prno,audituserid,auditusername,audittype,IpAddress)");
            strSql.Append(" values (");
            strSql.Append("@gid,@remark,@remarkDate,@prno,@audituserid,@auditusername,@audittype,@IpAddress)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@gid", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@remarkDate", SqlDbType.DateTime),
                    new SqlParameter("@prno",SqlDbType.VarChar,20),
                    new SqlParameter("@audituserid",SqlDbType.Int,4),
                    new SqlParameter("@auditusername",SqlDbType.NVarChar,100),
                    new SqlParameter("@audittype",SqlDbType.Int,4),
                    new SqlParameter("@IpAddress",SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = model.gid;
            parameters[1].Value = model.remark;
            parameters[2].Value = model.remarkDate;
            parameters[3].Value = model.prNo;
            parameters[4].Value = model.auditUserId;
            parameters[5].Value = model.auditUserName;
            parameters[6].Value = model.auditType;
            parameters[7].Value = request.UserHostAddress.Trim();

            object obj = null;
            if(trans == null)
                DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                DbHelperSQL.GetSingle(strSql.ToString(),trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Add(AuditLogInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_auditLog(");
            strSql.Append("gid,remark,remarkDate,prno,audituserid,auditusername,audittype,IpAddress)");
            strSql.Append(" values (");
            strSql.Append("@gid,@remark,@remarkDate,@prno,@audituserid,@auditusername,@audittype,@IpAddress)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@gid", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@remarkDate", SqlDbType.DateTime),
                    new SqlParameter("@prno",SqlDbType.VarChar,20),
                    new SqlParameter("@audituserid",SqlDbType.Int,4),
                    new SqlParameter("@auditusername",SqlDbType.NVarChar,100),
                    new SqlParameter("@audittype",SqlDbType.Int,4),
                    new SqlParameter("@IpAddress",SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = model.gid;
            parameters[1].Value = model.remark;
            parameters[2].Value = model.remarkDate;
            parameters[3].Value = model.prNo;
            parameters[4].Value = model.auditUserId;
            parameters[5].Value = model.auditUserName;
            parameters[6].Value = model.auditType;
            parameters[7].Value = "";

            object obj = null;
            if (trans == null)
                DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
        public void Update(AuditLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_auditLog set ");
            strSql.Append("gid=@gid,");
            strSql.Append("remark=@remark,");
            strSql.Append("remarkDate=@remarkDate,prno=@prno,audituserid=@audituserid,auditusername=@auditusername,audittype=@audittype");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@gid", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@remarkDate", SqlDbType.DateTime),
                    new SqlParameter("@prno",SqlDbType.VarChar,20),
                    new SqlParameter("@audituserid",SqlDbType.Int,4),
                    new SqlParameter("@auditusername",SqlDbType.NVarChar,100),
                    new SqlParameter("@audittype",SqlDbType.Int,4)                                        
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.gid;
            parameters[2].Value = model.remark;
            parameters[3].Value = model.remarkDate;
            parameters[4].Value = model.prNo;
            parameters[5].Value = model.auditUserId;
            parameters[6].Value = model.auditUserName;
            parameters[7].Value = model.auditType;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_auditLog ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AuditLogInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_auditLog ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            AuditLogInfo model = new AuditLogInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["gid"].ToString() != "")
                {
                    model.gid = int.Parse(ds.Tables[0].Rows[0]["gid"].ToString());
                }
                model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                if (ds.Tables[0].Rows[0]["remarkDate"].ToString() != "")
                {
                    model.remarkDate = DateTime.Parse(ds.Tables[0].Rows[0]["remarkDate"].ToString());
                }
                model.prNo = ds.Tables[0].Rows[0]["prno"].ToString();
                model.auditUserId = ds.Tables[0].Rows[0]["audituserid"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["audituserid"].ToString());
                model.auditUserName = ds.Tables[0].Rows[0]["auditusername"].ToString();
                model.auditType = ds.Tables[0].Rows[0]["audittype"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["audittype"].ToString());
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_auditLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public IList<ESP.Purchase.Entity.AuditLogInfo> GetModelListByGID(int Gid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_auditLog ");
            strSql.Append(" where Gid=" + Gid.ToString());
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.AuditLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
        #endregion  成员方法
    }
}

