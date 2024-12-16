using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Supply.Common;
using Supply.Entity;
using System.Data;
using ESP.Supplier.Common;
using ESP.ConfigCommon;

namespace Supply.DataAccess
{
    public class AricleVideoesProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AricleVideoes model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AricleVideoes(");
            strSql.Append(@"[ArticleID],[VideoPath],[VideoOldPath],[LocalVideoPath])");
            strSql.Append(" values (");
            strSql.Append(@"@ArticleID,@VideoPath,@VideoOldPath,@LocalVideoPath)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ArticleID", SqlDbType.Int,4),
					new SqlParameter("@VideoPath", SqlDbType.NVarChar,500),
					new SqlParameter("@VideoOldPath", SqlDbType.NVarChar,500),
					new SqlParameter("@LocalVideoPath", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.ArticleID;
            parameters[1].Value = model.VideoPath;
            parameters[2].Value = model.VideoOldPath;
            parameters[3].Value = model.LocalVideoPath;

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
        public void Update(AricleVideoes model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AricleVideoes set ");

            strSql.Append(@"ArticleID = @ArticleID,
      ,VideoPath = @VideoPath,
      ,VideoOldPath = @VideoOldPath,
      ,LocalVideoPath = @LocalVideoPath");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArticleID", SqlDbType.Int,4),
					new SqlParameter("@VideoPath", SqlDbType.NVarChar,500),
					new SqlParameter("@VideoOldPath", SqlDbType.NVarChar,500),
					new SqlParameter("@LocalVideoPath", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ArticleID;
            parameters[1].Value = model.VideoPath;
            parameters[2].Value = model.VideoOldPath;
            parameters[3].Value = model.LocalVideoPath;
            parameters[4].Value = model.ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AricleVideoes ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AricleVideoes GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AricleVideoes ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            AricleVideoes model = CBO.FillObject<AricleVideoes>(DbHelperSQL.Query(strSql.ToString(), parameters));
            //if (model != null && System.Configuration.ConfigurationManager.AppSettings["supplyManagerPath"] != null)
            //    model.Body = model.Body.Replace("src=\"/", "src=\"" + System.Configuration.ConfigurationManager.AppSettings["supplyManagerPath"].ToString());
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<AricleVideoes> GetList(int Top,string strWhere,List<SqlParameter> parms,string orderBy)
        {
            strWhere += " and status != " + (int)Supply.Common.Status.NewsStatus.Deleted;
            string strSql = "";
            strSql = " select {0} * ";
            strSql += " FROM AricleVideoes where 1=1";
            strSql += strWhere + " {1} "; 
            strSql = string.Format(strSql,((Top > 0) ? " top " + Top.ToString() : ""),(orderBy == "" ? " order by id desc" : " order by " + orderBy));
            return CBO.FillCollection<AricleVideoes>(DbHelperSQL.Query(strSql,parms.ToArray()));
        }

        public int GetListCount(string strWhere, List<SqlParameter> parms)
        {
            strWhere += " and status != " + (int)Supply.Common.Status.NewsStatus.Deleted;
            string strSql = "";
            strSql = " select count(id) Nums ";
            strSql += " FROM AricleVideoes where 1=1";
            strSql += strWhere;
            return int.Parse(DbHelperSQL.Query(strSql, parms.ToArray()).Tables[0].Rows[0]["Nums"].ToString());
        }

        public List<AricleVideoes> GetList(int pageSize, int pageIndex, string strWhere, string orderBy, List<SqlParameter> parms)
        {
            if (orderBy == "")
                orderBy = " id desc";
            strWhere += " and status != " + (int)Supply.Common.Status.NewsStatus.Deleted;
            DataTable dt = new DataTable();
            string sql = @"SELECT TOP (@PageSize) * FROM(
                            select *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] from AricleVideoes
                            WHERE 1=1 @@@WHERE@@@
                            ) t
                            WHERE t.[__i_RowNumber] > @PageStart
                            ";
            sql = sql.Replace("@@@ORDERBY@@@", orderBy);
            sql = sql.Replace("@@@WHERE@@@", strWhere);
            if (parms == null)
                parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@PageSize", pageSize));
            parms.Add(new SqlParameter("@PageStart", pageSize * pageIndex));

            return CBO.FillCollection<AricleVideoes>(DbHelperSQL.Query(sql, parms.ToArray()));
        }
    }
}
