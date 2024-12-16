using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类FilialeAuditBackUpDataProvider。
    /// </summary>
    public class FilialeAuditBackUpDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilialeAuditBackUpDataProvider"/> class.
        /// </summary>
        public FilialeAuditBackUpDataProvider()
        { }

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FilialeAuditBackUpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_FilialeAuditBackUp(");
            strSql.Append("userid,isBackupUser,BeginDate,EndDate)");
            strSql.Append(" values (");
            strSql.Append("@userid,@isBackupUser,@BeginDate,@EndDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@isBackupUser", SqlDbType.Int,4),
					new SqlParameter("@BeginDate", SqlDbType.VarChar,50),
					new SqlParameter("@EndDate", SqlDbType.VarChar,50)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.isBackupUser;
            parameters[2].Value = model.BeginDate;
            parameters[3].Value = model.EndDate;

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
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Update(FilialeAuditBackUpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_FilialeAuditBackUp set ");
            strSql.Append("userid=@userid,");
            strSql.Append("isBackupUser=@isBackupUser,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("EndDate=@EndDate");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@isBackupUser", SqlDbType.Int,4),
					new SqlParameter("@BeginDate", SqlDbType.VarChar,50),
					new SqlParameter("@EndDate", SqlDbType.VarChar,50)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.isBackupUser;
            parameters[3].Value = model.BeginDate;
            parameters[4].Value = model.EndDate;

           return  DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_FilialeAuditBackUp ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public FilialeAuditBackUpInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,userid,isBackupUser,BeginDate,EndDate from T_FilialeAuditBackUp ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            FilialeAuditBackUpInfo model = new FilialeAuditBackUpInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isBackupUser"].ToString() != "")
                {
                    model.isBackupUser = int.Parse(ds.Tables[0].Rows[0]["isBackupUser"].ToString());
                }
                model.BeginDate = ds.Tables[0].Rows[0]["BeginDate"].ToString();
                model.EndDate = ds.Tables[0].Rows[0]["EndDate"].ToString();
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
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,userid,isBackupUser,BeginDate,EndDate ");
            strSql.Append(" FROM T_FilialeAuditBackUp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 通过用户ID得到一个对象实体
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public FilialeAuditBackUpInfo GetModelByUserid(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_FilialeAuditBackUp ");
            strSql.Append(" where userid=@userid");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)};
            parameters[0].Value = userid;
            FilialeAuditBackUpInfo model = new FilialeAuditBackUpInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isBackupUser"].ToString() != "")
                {
                    model.isBackupUser = int.Parse(ds.Tables[0].Rows[0]["isBackupUser"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BeginDate"].ToString() != "")
                {
                    model.BeginDate = ds.Tables[0].Rows[0]["BeginDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    model.EndDate = ds.Tables[0].Rows[0]["EndDate"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion  成员方法
    }
}