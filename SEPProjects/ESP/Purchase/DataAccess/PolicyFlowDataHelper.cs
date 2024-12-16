using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类PolicyFlowDataHelper。
    /// </summary>
    public class PolicyFlowDataHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyFlowDataHelper"/> class.
        /// </summary>
        public PolicyFlowDataHelper()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(PolicyFlowInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_PolicyFlow(");
            strSql.Append("title,synopsis,contents)");
            strSql.Append(" values (");
            strSql.Append("@title,@synopsis,@contents)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,200),
					new SqlParameter("@synopsis", SqlDbType.NVarChar,500),
					new SqlParameter("@contents", SqlDbType.VarChar,200)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.synopsis;
            parameters[2].Value = model.contents;

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
        public int Update(PolicyFlowInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_PolicyFlow set ");
            strSql.Append("title=@title,");
            strSql.Append("synopsis=@synopsis,");
            strSql.Append("contents=@contents");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,200),
					new SqlParameter("@synopsis", SqlDbType.NVarChar,500),
					new SqlParameter("@contents", SqlDbType.VarChar,200)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.title;
            parameters[2].Value = model.synopsis;
            parameters[3].Value = model.contents;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_PolicyFlow ");
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
        public PolicyFlowInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_PolicyFlow ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            PolicyFlowInfo model = new PolicyFlowInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.synopsis = ds.Tables[0].Rows[0]["synopsis"].ToString();
                model.contents = ds.Tables[0].Rows[0]["contents"].ToString();
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
        /// <param name="parm">The parm.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere, SqlParameter parm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[title],[synopsis],[contents] ");
            strSql.Append(" FROM T_PolicyFlow");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by id desc");
            if (parm == null)
            {
                return DbHelperSQL.Query(strSql.ToString());
            }
            else
            {
                return DbHelperSQL.Query(strSql.ToString(), parm);
            }
        }
        #endregion  成员方法
    }
}