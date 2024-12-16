using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;

namespace ESP.Supplier.DataAccess
{
    public class SC_CityDataProvider
    {
        public SC_CityDataProvider()
		{}

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_City model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_City(");
            strSql.Append("CityName,CityLevel,ProvinceId,CityStatus)");
            strSql.Append(" values (");
            strSql.Append("@CityName,@CityLevel,@ProvinceId,@CityStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CityName", SqlDbType.NVarChar),
					new SqlParameter("@CityLevel", SqlDbType.NVarChar),
					new SqlParameter("@ProvinceId", SqlDbType.Int,4),
					new SqlParameter("@CityStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.CityName;
            parameters[1].Value = model.CityLevel;
            parameters[2].Value = model.ProvinceId;
            parameters[3].Value = model.CityStatus;

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
        public void Update(SC_City model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_City set ");
            strSql.Append("CityName=@CityName,");
            strSql.Append("CityLevel=@CityLevel,");
            strSql.Append("ProvinceId=@ProvinceId,");
            strSql.Append("CityStatus=@CityStatus");
            strSql.Append(" where CityId=@CityId");
            SqlParameter[] parameters = {
					new SqlParameter("@CityId", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar),
					new SqlParameter("@CityLevel", SqlDbType.NVarChar),
					new SqlParameter("@ProvinceId", SqlDbType.Int,4),
					new SqlParameter("@CityStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.CityId;
            parameters[1].Value = model.CityName;
            parameters[2].Value = model.CityLevel;
            parameters[3].Value = model.ProvinceId;
            parameters[4].Value = model.CityStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int CityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_City ");
            strSql.Append(" where CityId=@CityId");
            SqlParameter[] parameters = {
					new SqlParameter("@CityId", SqlDbType.Int,4)
				};
            parameters[0].Value = CityId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_City GetModel(int CityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_City ");
            strSql.Append(" where CityId=@CityId");
            SqlParameter[] parameters = {
					new SqlParameter("@CityId", SqlDbType.Int,4)};
            parameters[0].Value = CityId;
            SC_City model = new SC_City();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.CityId = CityId;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CityName = ds.Tables[0].Rows[0]["CityName"].ToString();
                model.CityLevel = ds.Tables[0].Rows[0]["CityLevel"].ToString();
                if (ds.Tables[0].Rows[0]["ProvinceId"].ToString() != "")
                {
                    model.ProvinceId = int.Parse(ds.Tables[0].Rows[0]["ProvinceId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CityStatus"].ToString() != "")
                {
                    model.CityStatus = int.Parse(ds.Tables[0].Rows[0]["CityStatus"].ToString());
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
            strSql.Append("select [CityId],[CityName],[CityLevel],[ProvinceId],[CityStatus] ");
            strSql.Append(" FROM SC_City ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_City> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_City ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_City>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        #endregion  成员方法
    }
}
