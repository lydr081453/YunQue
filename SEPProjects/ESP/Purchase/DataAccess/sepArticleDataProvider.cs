using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;


namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类T_Watch。
    /// </summary>
    public class sepArticleDateProvider
    {
        public sepArticleDateProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Watch");
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
        public int Add(ESP.Purchase.Entity.sepArticleInfo model)
        {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into sep_Article(");
                    strSql.Append("SysUserID,CreatedDate,IsRead)");
                    strSql.Append(" values (");
                    strSql.Append("@SysUserID,@CreatedDate,@IsRead)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@SysUserID", SqlDbType.Int,8),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Bit)};
                    parameters[0].Value = model.SysUserID;
                    parameters[1].Value = DateTime.Now;
                    parameters[2].Value = model.IsRead;

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
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Purchase.Entity.sepArticleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_Article set ");
            strSql.Append("SysUserID=@SysUserID,");
            strSql.Append("IsRead=@IsRead");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@SysUserID", SqlDbType.Int,8),
					new SqlParameter("@IsRead", SqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.SysUserID;
            parameters[2].Value = model.IsRead;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_Article ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Purchase.Entity.sepArticleInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sep_Article ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            ESP.Purchase.Entity.sepArticleInfo model = new ESP.Purchase.Entity.sepArticleInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.SysUserID = Convert.ToInt32(ds.Tables[0].Rows[0]["SysUserID"].ToString());
                if (ds.Tables[0].Rows[0]["IsRead"].ToString() != "")
                {
                    model.IsRead = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsRead"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<ESP.Purchase.Entity.sepArticleInfo> GetList(string condition)
        {
            IList<ESP.Purchase.Entity.sepArticleInfo> list = new List<ESP.Purchase.Entity.sepArticleInfo>();
            string sql = "SELECT  * FROM sep_Article ";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " Where " + condition;
            }
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    ESP.Purchase.Entity.sepArticleInfo c = new ESP.Purchase.Entity.sepArticleInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }
        #endregion  成员方法
    }
}

