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
    public class SC_ScaleDataProvider
    {
        public SC_ScaleDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Scale model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Scale(");
            strSql.Append("ScaleDes,ScaleStatus)");
            strSql.Append(" values (");
            strSql.Append("@ScaleDes,@ScaleStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ScaleDes", SqlDbType.NVarChar),
					new SqlParameter("@ScaleStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.ScaleDes;
            parameters[1].Value = model.ScaleStatus;

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
        public void Update(SC_Scale model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Scale set ");
            strSql.Append("ScaleDes=@ScaleDes,");
            strSql.Append("ScaleStatus=@ScaleStatus");
            strSql.Append(" where ScaleId=@ScaleId");
            SqlParameter[] parameters = {
					new SqlParameter("@ScaleId", SqlDbType.Int,4),
					new SqlParameter("@ScaleDes", SqlDbType.NVarChar),
					new SqlParameter("@ScaleStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.ScaleId;
            parameters[1].Value = model.ScaleDes;
            parameters[2].Value = model.ScaleStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ScaleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Scale ");
            strSql.Append(" where ScaleId=@ScaleId");
            SqlParameter[] parameters = {
					new SqlParameter("@ScaleId", SqlDbType.Int,4)
				};
            parameters[0].Value = ScaleId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Scale GetModel(int ScaleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_Scale ");
            strSql.Append(" where ScaleId=@ScaleId");
            SqlParameter[] parameters = {
					new SqlParameter("@ScaleId", SqlDbType.Int,4)};
            parameters[0].Value = ScaleId;
            SC_Scale model = new SC_Scale();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ScaleId = ScaleId;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ScaleDes = ds.Tables[0].Rows[0]["ScaleDes"].ToString();
                if (ds.Tables[0].Rows[0]["ScaleStatus"].ToString() != "")
                {
                    model.ScaleStatus = int.Parse(ds.Tables[0].Rows[0]["ScaleStatus"].ToString());
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
            strSql.Append("select [ScaleId],[ScaleDes],[ScaleStatus] ");
            strSql.Append(" FROM SC_Scale ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_Scale> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Scale ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Scale>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public List<SC_Scale> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Scale>(GetList(""));
        }

        #endregion  成员方法
    }
}
