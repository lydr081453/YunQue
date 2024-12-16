using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类OperationAuditDataProvider。
    /// </summary>
    public class OperationAuditDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationAuditDataProvider"/> class.
        /// </summary>
        public OperationAuditDataProvider()
        { }

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(OperationAuditInfo model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(OperationAuditInfo model,SqlConnection conn,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_OperationAudit(");
            strSql.Append("general_id,auditorId,aduitType)");
            strSql.Append(" values (");
            strSql.Append("@general_id,@auditorId,@aduitType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@general_id", SqlDbType.Int,4),
					new SqlParameter("@auditorId", SqlDbType.Int,4),
					new SqlParameter("@aduitType", SqlDbType.Int,4)};
            parameters[0].Value = model.general_id;
            parameters[1].Value = model.auditorId;
            parameters[2].Value = model.aduitType;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),conn,trans, parameters);
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
        public void Update(OperationAuditInfo model)
        {
            Update(model, null, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(OperationAuditInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OperationAudit set ");
            strSql.Append("general_id=@general_id,");
            strSql.Append("auditorId=@auditorId,");
            strSql.Append("aduitType=@aduitType");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@general_id", SqlDbType.Int,4),
					new SqlParameter("@auditorId", SqlDbType.Int,4),
					new SqlParameter("@aduitType", SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.general_id;
            parameters[2].Value = model.auditorId;
            parameters[3].Value = model.aduitType;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int gid, SqlConnection conn, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OperationAudit ");
            strSql.Append(" where general_id=@gid ");
            SqlParameter[] parameters = {
					new SqlParameter("@gid", SqlDbType.Int,4)};
            parameters[0].Value = gid;

            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int gid,int type,SqlConnection conn,SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OperationAudit ");
            strSql.Append(" where general_id=@general_id and aduittype=@aduittype ");
            SqlParameter[] parameters = {
					new SqlParameter("@general_id", SqlDbType.Int,4),new SqlParameter("@aduittype", SqlDbType.Int,4)};
            parameters[0].Value = gid;
            parameters[1].Value = type;
            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }

        /// <summary>
        /// 根据申请单ID，审核人类型获得对象
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="auditType"></param>
        /// <returns></returns>
        public OperationAuditInfo GetModel(int gid,int auditType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  id,general_id,auditorId,aduitType from T_OperationAudit ");
            strSql.Append(" where general_id=@general_id and  aduitType=@aduitType");
            SqlParameter[] parameters = {
					new SqlParameter("@general_id", SqlDbType.Int,4),
                    new SqlParameter("@aduitType",SqlDbType.Int,4)
                                        };
            parameters[0].Value = gid;
            parameters[1].Value = auditType;

            OperationAuditInfo model = new OperationAuditInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["general_id"].ToString() != "")
                {
                    model.general_id = int.Parse(ds.Tables[0].Rows[0]["general_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["auditorId"].ToString() != "")
                {
                    model.auditorId = int.Parse(ds.Tables[0].Rows[0]["auditorId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["aduitType"].ToString() != "")
                {
                    model.aduitType = int.Parse(ds.Tables[0].Rows[0]["aduitType"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public OperationAuditInfo GetModelByUserId(int gid, int userId,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  id,general_id,auditorId,aduitType from T_OperationAudit ");
            strSql.Append(" where general_id=@general_id and  auditorId=@auditorId");
            SqlParameter[] parameters = {
					new SqlParameter("@general_id", SqlDbType.Int,4),
                    new SqlParameter("@auditorId",SqlDbType.Int,4)
                                        };
            parameters[0].Value = gid;
            parameters[1].Value = userId;

            OperationAuditInfo model = new OperationAuditInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(),trans.Connection,trans, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["general_id"].ToString() != "")
                {
                    model.general_id = int.Parse(ds.Tables[0].Rows[0]["general_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["auditorId"].ToString() != "")
                {
                    model.auditorId = int.Parse(ds.Tables[0].Rows[0]["auditorId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["aduitType"].ToString() != "")
                {
                    model.aduitType = int.Parse(ds.Tables[0].Rows[0]["aduitType"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OperationAuditInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,general_id,auditorId,aduitType from T_OperationAudit ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            OperationAuditInfo model = new OperationAuditInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["general_id"].ToString() != "")
                {
                    model.general_id = int.Parse(ds.Tables[0].Rows[0]["general_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["auditorId"].ToString() != "")
                {
                    model.auditorId = int.Parse(ds.Tables[0].Rows[0]["auditorId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["aduitType"].ToString() != "")
                {
                    model.aduitType = int.Parse(ds.Tables[0].Rows[0]["aduitType"].ToString());
                }
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
            strSql.Append("select id,general_id,auditorId,aduitType ");
            strSql.Append(" FROM T_OperationAudit ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}