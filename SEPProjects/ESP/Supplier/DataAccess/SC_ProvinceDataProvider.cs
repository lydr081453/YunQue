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
    public class SC_ProvinceDataProvider
    {
        public SC_ProvinceDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Province model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Province(");
            strSql.Append("ProvinceName,CountryId,ProvinceStatus)");
            strSql.Append(" values (");
            strSql.Append("@ProvinceName,@CountryId,@ProvinceStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar),
					new SqlParameter("@CountryId", SqlDbType.Int,4),
					new SqlParameter("@ProvinceStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.ProvinceName;
            parameters[1].Value = model.CountryId;
            parameters[2].Value = model.ProvinceStatus;

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
        public void Update(SC_Province model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Province set ");
            strSql.Append("ProvinceName=@ProvinceName,");
            strSql.Append("CountryId=@CountryId,");
            strSql.Append("ProvinceStatus=@ProvinceStatus");
            strSql.Append(" where ProvinceId=@ProvinceId");
            SqlParameter[] parameters = {
					new SqlParameter("@ProvinceId", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar),
					new SqlParameter("@CountryId", SqlDbType.Int,4),
					new SqlParameter("@ProvinceStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.ProvinceId;
            parameters[1].Value = model.ProvinceName;
            parameters[2].Value = model.CountryId;
            parameters[3].Value = model.ProvinceStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ProvinceId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Province ");
            strSql.Append(" where ProvinceId=@ProvinceId");
            SqlParameter[] parameters = {
					new SqlParameter("@ProvinceId", SqlDbType.Int,4)
				};
            parameters[0].Value = ProvinceId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Province GetModel(int ProvinceId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_Province ");
            strSql.Append(" where ProvinceId=@ProvinceId");
            SqlParameter[] parameters = {
					new SqlParameter("@ProvinceId", SqlDbType.Int,4)};
            parameters[0].Value = ProvinceId;
            SC_Province model = new SC_Province();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ProvinceId = ProvinceId;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ProvinceName = ds.Tables[0].Rows[0]["ProvinceName"].ToString();
                if (ds.Tables[0].Rows[0]["CountryId"].ToString() != "")
                {
                    model.CountryId = int.Parse(ds.Tables[0].Rows[0]["CountryId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProvinceStatus"].ToString() != "")
                {
                    model.ProvinceStatus = int.Parse(ds.Tables[0].Rows[0]["ProvinceStatus"].ToString());
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
            strSql.Append("select [ProvinceId],[ProvinceName],[CountryId],[ProvinceStatus] ");
            strSql.Append(" FROM SC_Province ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        public List<SC_Province> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Province ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Province>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        #endregion  成员方法
    }
}
