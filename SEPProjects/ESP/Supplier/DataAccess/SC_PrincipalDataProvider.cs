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
    public class SC_PrincipalDataProvider
    {
        public SC_PrincipalDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Principal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Principal(");
            strSql.Append("PrincipalDes,PrincipalStatus)");
            strSql.Append(" values (");
            strSql.Append("@PrincipalDes,@PrincipalStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PrincipalDes", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.PrincipalDes;
            parameters[1].Value = model.PrincipalStatus;

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
        public void Update(SC_Principal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Principal set ");
            strSql.Append("PrincipalDes=@PrincipalDes,");
            strSql.Append("PrincipalStatus=@PrincipalStatus");
            strSql.Append(" where PrincipalId=@PrincipalId");
            SqlParameter[] parameters = {
					new SqlParameter("@PrincipalId", SqlDbType.Int,4),
					new SqlParameter("@PrincipalDes", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.PrincipalId;
            parameters[1].Value = model.PrincipalDes;
            parameters[2].Value = model.PrincipalStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int PrincipalId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Principal ");
            strSql.Append(" where PrincipalId=@PrincipalId");
            SqlParameter[] parameters = {
					new SqlParameter("@PrincipalId", SqlDbType.Int,4)
				};
            parameters[0].Value = PrincipalId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Principal GetModel(int PrincipalId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_Principal ");
            strSql.Append(" where PrincipalId=@PrincipalId");
            SqlParameter[] parameters = {
					new SqlParameter("@PrincipalId", SqlDbType.Int,4)};
            parameters[0].Value = PrincipalId;
            SC_Principal model = new SC_Principal();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.PrincipalId = PrincipalId;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PrincipalDes = ds.Tables[0].Rows[0]["PrincipalDes"].ToString();
                if (ds.Tables[0].Rows[0]["PrincipalStatus"].ToString() != "")
                {
                    model.PrincipalStatus = int.Parse(ds.Tables[0].Rows[0]["PrincipalStatus"].ToString());
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
            strSql.Append("select [PrincipalId],[PrincipalDes],[PrincipalStatus] ");
            strSql.Append(" FROM SC_Principal ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_Principal> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Principal ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Principal>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_Principal> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Principal>(GetList(""));
        }

        #endregion  成员方法
    }
}
