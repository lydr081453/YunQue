using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using System.Data.SqlClient;
using System.Data;
using ESP.Supplier.Entity;

namespace ESP.Supplier.DataAccess
{
    public class SC_NoCheckSupplierScoreDataProvider
    {
        public SC_NoCheckSupplierScoreDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SC_NoCheckSupplierScore");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_NoCheckSupplierScore model, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_NoCheckSupplierScore(");
            strSql.Append("qaid,questionnum,supplierid,score,scoreType,createTime,createIP,creator)");
            strSql.Append(" values (");
            strSql.Append("@qaid,@questionnum,@supplierid,@score,@scoreType,@createTime,@createIP,@creator)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@qaid", SqlDbType.Int,4),
					new SqlParameter("@questionnum", SqlDbType.NVarChar,50),
					new SqlParameter("@supplierid", SqlDbType.Int,4),
					new SqlParameter("@score", SqlDbType.Decimal,9),
					new SqlParameter("@scoreType", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.SmallDateTime),
					new SqlParameter("@createIP", SqlDbType.NVarChar,50),
					new SqlParameter("@creator", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.qaid;
            parameters[1].Value = model.questionnum;
            parameters[2].Value = model.supplierid;
            parameters[3].Value = model.score;
            parameters[4].Value = model.scoreType;
            parameters[5].Value = model.createTime;
            parameters[6].Value = model.createIP;
            parameters[7].Value = model.creator;

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
        /// 更新一条数据
        /// </summary>
        public void Update(SC_NoCheckSupplierScore model, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_NoCheckSupplierScore set ");
            strSql.Append("qaid=@qaid,");
            strSql.Append("questionnum=@questionnum,");
            strSql.Append("supplierid=@supplierid,");
            strSql.Append("score=@score,");
            strSql.Append("scoreType=@scoreType,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createIP=@createIP,");
            strSql.Append("creator=@creator");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@qaid", SqlDbType.Int,4),
					new SqlParameter("@questionnum", SqlDbType.NVarChar,50),
					new SqlParameter("@supplierid", SqlDbType.Int,4),
					new SqlParameter("@score", SqlDbType.Decimal,9),
					new SqlParameter("@scoreType", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.SmallDateTime),
					new SqlParameter("@createIP", SqlDbType.NVarChar,50),
					new SqlParameter("@creator", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.qaid;
            parameters[2].Value = model.questionnum;
            parameters[3].Value = model.supplierid;
            parameters[4].Value = model.score;
            parameters[5].Value = model.scoreType;
            parameters[6].Value = model.createTime;
            parameters[7].Value = model.createIP;
            parameters[8].Value = model.creator;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_NoCheckSupplierScore ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void DeleteBySupplierId(int supplierid, int scoretype, SqlTransaction trans, SqlConnection conn)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_NoCheckSupplierScore ");
            strSql.Append(" where supplierid=@supplierid and scoreType=@scoreType ");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierid", SqlDbType.Int,4),
                                        new SqlParameter("@scoreType",SqlDbType.Int,4)};
            parameters[0].Value = supplierid;
            parameters[1].Value = scoretype;

            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_NoCheckSupplierScore GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,qaid,questionnum,supplierid,score,scoreType,createTime,createIP,creator from SC_NoCheckSupplierScore ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            SC_NoCheckSupplierScore model = new SC_NoCheckSupplierScore();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["qaid"].ToString() != "")
                {
                    model.qaid = int.Parse(ds.Tables[0].Rows[0]["qaid"].ToString());
                }
                model.questionnum = ds.Tables[0].Rows[0]["questionnum"].ToString();
                if (ds.Tables[0].Rows[0]["supplierid"].ToString() != "")
                {
                    model.supplierid = int.Parse(ds.Tables[0].Rows[0]["supplierid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["score"].ToString() != "")
                {
                    model.score = decimal.Parse(ds.Tables[0].Rows[0]["score"].ToString());
                }
                if (ds.Tables[0].Rows[0]["scoreType"].ToString() != "")
                {
                    model.scoreType = int.Parse(ds.Tables[0].Rows[0]["scoreType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(ds.Tables[0].Rows[0]["createTime"].ToString());
                }
                model.createIP = ds.Tables[0].Rows[0]["createIP"].ToString();
                model.creator = ds.Tables[0].Rows[0]["creator"].ToString();
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
            strSql.Append("select id,qaid,questionnum,supplierid,score,scoreType,createTime,createIP,creator ");
            strSql.Append(" FROM SC_NoCheckSupplierScore ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,qaid,questionnum,supplierid,score,scoreType,createTime,createIP,creator ");
            strSql.Append(" FROM SC_NoCheckSupplierScore ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

    }
}
