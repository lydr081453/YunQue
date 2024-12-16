using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类AuditBackUpDataProvider。
    /// </summary>
    public class AuditBackUpDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditBackUpDataProvider"/> class.
        /// </summary>
        public AuditBackUpDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(AuditBackUpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_AuditBackUp(");
            strSql.Append("userId,backupUserId,BeginDate,EndDate)");
            strSql.Append(" values (");
            strSql.Append("@userId,@backupUserId,@BeginDate,@EndDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@backupUserId", SqlDbType.Int,4),
					new SqlParameter("@BeginDate", SqlDbType.VarChar,50),
					new SqlParameter("@EndDate", SqlDbType.VarChar,50)};
            parameters[0].Value = model.userId;
            parameters[1].Value = model.backupUserId;
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
        public int Update(AuditBackUpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_AuditBackUp set ");
            strSql.Append("userId=@userId,");
            strSql.Append("backupUserId=@backupUserId,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("EndDate=@EndDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@backupUserId", SqlDbType.Int,4),
					new SqlParameter("@BeginDate", SqlDbType.VarChar,50),
					new SqlParameter("@EndDate", SqlDbType.VarChar,50)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.userId;
            parameters[2].Value = model.backupUserId;
            parameters[3].Value = model.BeginDate;
            parameters[4].Value = model.EndDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_AuditBackUp ");
            strSql.Append(" where id=@id");
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
        public AuditBackUpInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_AuditBackUp ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            AuditBackUpInfo model = new AuditBackUpInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                {
                    model.userId = int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["backupUserId"].ToString() != "")
                {
                    model.backupUserId = int.Parse(ds.Tables[0].Rows[0]["backupUserId"].ToString());
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public AuditBackUpInfo GetModelByUserId(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from T_AuditBackUp ");
            strSql.Append(" where begindate<getdate() and  enddate > getdate() and userId=@userId");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
            parameters[0].Value = userId;
            AuditBackUpInfo model = new AuditBackUpInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                {
                    model.userId = int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["backupUserId"].ToString() != "")
                {
                    model.backupUserId = int.Parse(ds.Tables[0].Rows[0]["backupUserId"].ToString());
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

        /// <summary>
        /// 检查是否是可用代初审人
        /// </summary>
        /// <param name="sysUserId">The sys user id.</param>
        /// <returns>
        /// 	<c>true</c> if [is back up user] [the specified sys user id]; otherwise, <c>false</c>.
        /// </returns>
        public bool isBackUpUser(int sysUserId)
        {
            AuditBackUpInfo model = GetModelByUserId(sysUserId);
            if (model != null && DateTime.Parse(model.BeginDate) <= DateTime.Today && DateTime.Today <= DateTime.Parse(model.EndDate))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[userId],[backupUserId],[BeginDate],[EndDate] ");
            strSql.Append(" FROM T_AuditBackUp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}