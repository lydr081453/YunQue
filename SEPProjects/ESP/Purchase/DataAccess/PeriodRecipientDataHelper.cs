using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类PeriodRecipientDataHelper。
    /// </summary>
    public class PeriodRecipientDataHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodRecipientDataHelper"/> class.
        /// </summary>
        public PeriodRecipientDataHelper()
        { }

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PeriodRecipientInfo model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PeriodRecipientInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_PeriodRecipient(");
            strSql.Append("periodId,recipientId)");
            strSql.Append(" values (");
            strSql.Append("@periodId,@recipientId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@periodId", SqlDbType.Int,4),
					new SqlParameter("@recipientId", SqlDbType.Int,4)};
            parameters[0].Value = model.periodId;
            parameters[1].Value = model.recipientId;

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
        public void Update(PeriodRecipientInfo model)
        {
            Update(model, null, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(PeriodRecipientInfo model,SqlConnection conn,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_PeriodRecipient set ");
            strSql.Append("periodId=@periodId,");
            strSql.Append("recipientId=@recipientId");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@periodId", SqlDbType.Int,4),
					new SqlParameter("@recipientId", SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.periodId;
            parameters[2].Value = model.recipientId;

            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            Delete(id, null, null);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlConnection conn, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_PeriodRecipient ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }




        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PeriodRecipientInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,periodId,recipientId from T_PeriodRecipient ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            PeriodRecipientInfo model = new PeriodRecipientInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["periodId"].ToString() != "")
                {
                    model.periodId = int.Parse(ds.Tables[0].Rows[0]["periodId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["recipientId"].ToString() != "")
                {
                    model.recipientId = int.Parse(ds.Tables[0].Rows[0]["recipientId"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public PeriodRecipientInfo GetModelByPeriodId(int periodid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,periodId,recipientId from T_PeriodRecipient ");
            strSql.Append(" where periodid=@periodid ");
            SqlParameter[] parameters = {
					new SqlParameter("@periodid", SqlDbType.Int,4)};
            parameters[0].Value = periodid;

            PeriodRecipientInfo model = new PeriodRecipientInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["periodId"].ToString() != "")
                {
                    model.periodId = int.Parse(ds.Tables[0].Rows[0]["periodId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["recipientId"].ToString() != "")
                {
                    model.recipientId = int.Parse(ds.Tables[0].Rows[0]["recipientId"].ToString());
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
            strSql.Append("select id,periodId,recipientId ");
            strSql.Append(" FROM T_PeriodRecipient ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 收货单是否关联付款申请
        /// </summary>
        /// <param name="recipientId">收货单Id</param>
        /// <returns></returns>
        public bool isJoinPeriod(int recipientId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id ");
            strSql.Append(" FROM T_PeriodRecipient ");
            strSql.Append(" where recipientId=" + recipientId);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        #endregion  成员方法
    }
}