using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_CountryDataProvider
    {
        public SC_CountryDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Country model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Country(");
            strSql.Append("CountryNum,CountryName,CountryStatus)");
            strSql.Append(" values (");
            strSql.Append("@CountryNum,@CountryName,@CountryStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CountryNum", SqlDbType.NVarChar),
					new SqlParameter("@CountryName", SqlDbType.NVarChar),
					new SqlParameter("@CountryStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.CountryNum;
            parameters[1].Value = model.CountryName;
            parameters[2].Value = model.CountryStatus;

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
        public void Update(SC_Country model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Country set ");
            strSql.Append("CountryNum=@CountryNum,");
            strSql.Append("CountryName=@CountryName,");
            strSql.Append("CountryStatus=@CountryStatus");
            strSql.Append(" where CountryID=@CountryID");
            SqlParameter[] parameters = {
					new SqlParameter("@CountryID", SqlDbType.Int,4),
					new SqlParameter("@CountryNum", SqlDbType.NVarChar),
					new SqlParameter("@CountryName", SqlDbType.NVarChar),
					new SqlParameter("@CountryStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.CountryID;
            parameters[1].Value = model.CountryNum;
            parameters[2].Value = model.CountryName;
            parameters[3].Value = model.CountryStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int CountryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Country ");
            strSql.Append(" where CountryID=@CountryID");
            SqlParameter[] parameters = {
					new SqlParameter("@CountryID", SqlDbType.Int,4)
				};
            parameters[0].Value = CountryID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Country GetModel(int CountryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_Country ");
            strSql.Append(" where CountryID=@CountryID");
            SqlParameter[] parameters = {
					new SqlParameter("@CountryID", SqlDbType.Int,4)};
            parameters[0].Value = CountryID;
            SC_Country model = new SC_Country();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.CountryID = CountryID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CountryNum = ds.Tables[0].Rows[0]["CountryNum"].ToString();
                model.CountryName = ds.Tables[0].Rows[0]["CountryName"].ToString();
                if (ds.Tables[0].Rows[0]["CountryStatus"].ToString() != "")
                {
                    model.CountryStatus = int.Parse(ds.Tables[0].Rows[0]["CountryStatus"].ToString());
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
            strSql.Append("select [CountryID],[CountryNum],[CountryName],[CountryStatus] ");
            strSql.Append(" FROM SC_Country ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_Country> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Country ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Country>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        #endregion  成员方法
    }
}
