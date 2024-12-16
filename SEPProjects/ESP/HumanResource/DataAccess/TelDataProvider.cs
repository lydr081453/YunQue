using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data;
using System.Data.SqlClient;
using ESP.Administrative.Common;
using System.Security.Policy;
using Microsoft.Office.Interop.Word;
using System.Collections;

namespace ESP.HumanResource.DataAccess
{
    /// <summary>
    /// 分机
    /// </summary>
    public class TelDataProvider
    {

        public TelDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ad_tel");
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
        public int Add(TELInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ad_tel(");
            strSql.Append("Tel,Status)");
            strSql.Append(" values (");
            strSql.Append("@Tel,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Tel", SqlDbType.NVarChar),
                    new SqlParameter("@status",SqlDbType.Int,4)};
            parameters[0].Value = model.Tel;
            parameters[1].Value = model.Status;

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
        public int Update(TELInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ad_tel set ");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Tel", SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Tel;
            parameters[2].Value = model.Status;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TELInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ad_tel set ");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Tel", SqlDbType.NVarChar),
                    new SqlParameter("@status",SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Tel;
            parameters[2].Value = model.Status;

            if (trans != null)
                return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
            else
                return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ad_tel ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TELInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ad_tel ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            TELInfo model = new TELInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.Id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                if (ds.Tables[0].Rows[0]["tel"].ToString() != "")
                {
                    model.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public TELInfo GetModel(string tel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ad_tel ");
            strSql.Append(" where tel=@tel");
            SqlParameter[] parameters = {
					new SqlParameter("@tel", SqlDbType.NVarChar)};
            parameters[0].Value = tel;
            TELInfo model = new TELInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                if (ds.Tables[0].Rows[0]["tel"].ToString() != "")
                {
                    model.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
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
            strSql.Append("select [id],[tel],[status] ");
            strSql.Append(" FROM ad_tel ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where status=1 " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<TELInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            string strSql = "select * from ad_tel where status=1 ";
            strSql += strWhere;
            strSql += " order by id desc";
            List<TELInfo> list = new List<TELInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms))
            {
                while (r.Read())
                {
                    TELInfo model = new TELInfo();
                    model.PopupData(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<TELInfo> GetModelList(string strWhere)
        {
            string strSql = "select * from ad_tel where status=1 ";
            strSql += strWhere;
            List<TELInfo> list = new List<TELInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    TELInfo model = new TELInfo();
                    model.PopupData(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

        #endregion  成员方法
    }
}
