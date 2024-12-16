using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类ProductDataProvider。
    /// </summary>
    public class ProjectRelationReporterPrivateDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRelationReporterPrivateDataProvider"/> class.
        /// </summary>
        public ProjectRelationReporterPrivateDataProvider()
        { }

        #region  成员方法

        public static int Add(ProjectRelationReporterPrivateInfo model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ProjectRelationReporterPrivateInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_ProjectRelationReporterPrivate(");
            strSql.Append("ProjectID,ReporterPrivateID)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@ReporterPrivateID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,8),
					new SqlParameter("@ReporterPrivateID", SqlDbType.Int,8)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.ReporterPrivateID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),conn,trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static int Update(ProjectRelationReporterPrivateInfo model)
        {
            return Update(model, null, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ProjectRelationReporterPrivateInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ProjectRelationReporterPrivate set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ReporterPrivateID=@ReporterPrivateID,");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,8),
					new SqlParameter("@ProjectID", SqlDbType.Int,8),
                    new SqlParameter("@ReporterPrivateID",SqlDbType.Int,8),};
            parameters[0].Value = model.id;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.ReporterPrivateID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_ProjectRelationReporterPrivate ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,8)};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一批数据
        /// </summary>
        public static int Delete(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_ProjectRelationReporterPrivate ");
            //strSql.Append(" where id in (@ids)");
            strSql.Append(" where id in (" + ids + ")");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@ids", ids)
            //    };
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 屏蔽目录物品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static int DisabledData(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Product set isshow=@isshow");
            //strSql.Append(" where id in (@ids)");
            strSql.Append(" where id in (" + ids + ")");
            SqlParameter[] parameters = {
                    new SqlParameter("@isshow",0)
					//,new SqlParameter("@ids", ids)
				};
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ProjectRelationReporterPrivateInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from ProjectRelationReporterPrivateInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,8)};
            parameters[0].Value = id;
            ProjectRelationReporterPrivateInfo model = new ProjectRelationReporterPrivateInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ProjectID"].ToString() != "")
                {
                    model.ProjectID = int.Parse(ds.Tables[0].Rows[0]["ProjectID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReporterPrivateID"].ToString() != "")
                {
                    model.ReporterPrivateID = int.Parse(ds.Tables[0].Rows[0]["ReporterPrivateID"].ToString());
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
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ProjectRelationReporterPrivateInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1" + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}