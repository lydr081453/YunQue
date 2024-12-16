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
    public class SC_IndustriesDataProvider
    {
        public SC_IndustriesDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Industries model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Industries(");
            strSql.Append("IndustryName,IndustryStatus)");
            strSql.Append(" values (");
            strSql.Append("@IndustryName,@IndustryStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@IndustryName", SqlDbType.NVarChar),
					new SqlParameter("@IndustryStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.IndustryName;
            parameters[1].Value = model.IndustryStatus;

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
        public void Update(SC_Industries model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Industries set ");
            strSql.Append("IndustryName=@IndustryName,");
            strSql.Append("IndustryStatus=@IndustryStatus");
            strSql.Append(" where IndustryID=@IndustryID");
            SqlParameter[] parameters = {
					new SqlParameter("@IndustryID", SqlDbType.Int,4),
					new SqlParameter("@IndustryName", SqlDbType.NVarChar),
					new SqlParameter("@IndustryStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.IndustryID;
            parameters[1].Value = model.IndustryName;
            parameters[2].Value = model.IndustryStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int IndustryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Industries ");
            strSql.Append(" where IndustryID=@IndustryID");
            SqlParameter[] parameters = {
					new SqlParameter("@IndustryID", SqlDbType.Int,4)
				};
            parameters[0].Value = IndustryID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Industries GetModel(int IndustryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_Industries ");
            strSql.Append(" where IndustryID=@IndustryID");
            SqlParameter[] parameters = {
					new SqlParameter("@IndustryID", SqlDbType.Int,4)};
            parameters[0].Value = IndustryID;
            SC_Industries model = new SC_Industries();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.IndustryID = IndustryID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.IndustryName = ds.Tables[0].Rows[0]["IndustryName"].ToString();
                if (ds.Tables[0].Rows[0]["IndustryStatus"].ToString() != "")
                {
                    model.IndustryStatus = int.Parse(ds.Tables[0].Rows[0]["IndustryStatus"].ToString());
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
            strSql.Append("select [IndustryID],[IndustryName],[IndustryStatus] ");
            strSql.Append(" FROM SC_Industries ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_Industries> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Industries ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Industries>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_Industries> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Industries>(GetList(""));
        }

        #endregion  成员方法
    }
}
