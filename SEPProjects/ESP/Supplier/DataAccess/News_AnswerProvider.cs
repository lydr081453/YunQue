using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.Common;
using System.Data.SqlClient;
using System.Data;
using Supply.Entity;
using ESP.Supplier.Common;
using ESP.ConfigCommon;

namespace Supply.DataAccess
{
    public class AnswerProvider
    {
        		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Answer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Answer(");
			strSql.Append("questionId,userId,createTime,createIp,answerContent)");
			strSql.Append(" values (");
			strSql.Append("@questionId,@userId,@createTime,@createIp,@answerContent)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@questionId", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@createIp", SqlDbType.NVarChar,30),
					new SqlParameter("@answerContent", SqlDbType.NVarChar,4000)};
			parameters[0].Value = model.questionId;
			parameters[1].Value = model.userId;
			parameters[2].Value = model.createTime;
			parameters[3].Value = model.createIp;
			parameters[4].Value = model.answerContent;

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
		public void Update(Answer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Answer set ");
			strSql.Append("questionId=@questionId,");
			strSql.Append("userId=@userId,");
			strSql.Append("createTime=@createTime,");
			strSql.Append("createIp=@createIp,");
			strSql.Append("answerContent=@answerContent");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@questionId", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@createIp", SqlDbType.NVarChar,30),
					new SqlParameter("@answerContent", SqlDbType.NVarChar,4000)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.questionId;
			parameters[2].Value = model.userId;
			parameters[3].Value = model.createTime;
			parameters[4].Value = model.createIp;
			parameters[5].Value = model.answerContent;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Answer ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Answer GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,questionId,userId,createTime,createIp,answerContent from Answer ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

            return CBO.FillObject<Answer>(DbHelperSQL.Query(strSql.ToString(),parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Answer> GetList(string strWhere,List<SqlParameter> parms)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select a.*,b.RealLastName + b.RealFirstName as username ");
			strSql.Append(" FROM Answer as a");
            strSql.Append(" left join SiteUsers as b on a.userid=b.id");
            strSql.Append(" where 1=1");
			if(strWhere.Trim()!="")
			{
				strSql.Append(strWhere);
			}
			return CBO.FillCollection<Answer>(DbHelperSQL.Query(strSql.ToString(),parms.ToArray()));
		}

        public string GetCountByQuestionId(int questionId)
        {
            string strSql = "select count(id) sumCount from answer where questionId="+questionId;
            return DbHelperSQL.Query(strSql).Tables[0].Rows[0]["sumCount"].ToString();
        }

    }
}
