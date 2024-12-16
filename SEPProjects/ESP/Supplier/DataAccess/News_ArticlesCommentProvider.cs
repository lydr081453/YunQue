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
    public class ArticlesCommentProvider
    {
        public ArticlesCommentProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ArticlesComment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ArticlesComment(");
			strSql.Append("CommentBody,CommentTitle,CreatedDate,CreatedUserID,ArticleID)");
			strSql.Append(" values (");
			strSql.Append("@CommentBody,@CommentTitle,@CreatedDate,@CreatedUserID,@ArticleID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CommentBody", SqlDbType.Text),
					new SqlParameter("@CommentTitle", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ArticleID", SqlDbType.Int,4)};
			parameters[0].Value = model.CommentBody;
			parameters[1].Value = model.CommentTitle;
			parameters[2].Value = model.CreatedDate;
			parameters[3].Value = model.CreatedUserID;
			parameters[4].Value = model.ArticleID;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public void Update(ArticlesComment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ArticlesComment set ");
			strSql.Append("CommentBody=@CommentBody,");
			strSql.Append("CommentTitle=@CommentTitle,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("ArticleID=@ArticleID");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CommentBody", SqlDbType.Text),
					new SqlParameter("@CommentTitle", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ArticleID", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CommentBody;
			parameters[2].Value = model.CommentTitle;
			parameters[3].Value = model.CreatedDate;
			parameters[4].Value = model.CreatedUserID;
			parameters[5].Value = model.ArticleID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ArticlesComment ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ArticlesComment GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CommentBody,CommentTitle,CreatedDate,CreatedUserID,ArticleID from ArticlesComment ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return CBO.FillObject<ArticlesComment>(DbHelperSQL.Query(strSql.ToString(),parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ArticlesComment> GetList(string strWhere,List<SqlParameter> parms)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CommentBody,CommentTitle,CreatedDate,CreatedUserID,ArticleID ");
			strSql.Append(" FROM ArticlesComment where 1=1");
			if(strWhere.Trim()!="")
			{
				strSql.Append(strWhere);
			}
            strSql.Append(" order by id desc");
			return CBO.FillCollection<ArticlesComment>(DbHelperSQL.Query(strSql.ToString(),parms.ToArray()));
        }        

        public int GetListCount(string strWhere, List<SqlParameter> parms)
        {
            string strSql = "";
            strSql = " select count(id) Nums ";
            strSql += " FROM ArticlesComment where 1=1";
            strSql += strWhere;
            return int.Parse(DbHelperSQL.Query(strSql, parms.ToArray()).Tables[0].Rows[0]["Nums"].ToString());
        }

        public List<ArticlesComment> GetList(int pageSize, int pageIndex, string strWhere, string orderBy, List<SqlParameter> parms)
        {
            if (orderBy == "")
                orderBy = " id desc";
            DataTable dt = new DataTable();
            string sql = @"SELECT TOP (@PageSize) * FROM(
                            select *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] from ArticlesComment
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

            return CBO.FillCollection<ArticlesComment>(DbHelperSQL.Query(sql, parms.ToArray()));
        }

        #endregion
    }
}