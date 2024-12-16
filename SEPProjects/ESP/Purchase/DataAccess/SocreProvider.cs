using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using System.Data;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
	/// 数据访问类ScoreDataProvider。
	/// </summary>
	public class ScoreDataProvider
	{
		public ScoreDataProvider()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ScoreID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Score");
			strSql.Append(" where ScoreID= @ScoreID");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreID", SqlDbType.Int,4)
				};
			parameters[0].Value = ScoreID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.ScoreInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Score(");
			strSql.Append("ScoreName,Scores,ScoreContentID,ScoreContent,IsNeedRemark)");
			strSql.Append(" values (");
			strSql.Append("@ScoreName,@Scores,@ScoreContentID,@ScoreContent,@IsNeedRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreName", SqlDbType.NVarChar),
					new SqlParameter("@Scores", SqlDbType.Int,4),
					new SqlParameter("@ScoreContentID", SqlDbType.Int,4),
					new SqlParameter("@ScoreContent", SqlDbType.NVarChar),
                    new SqlParameter("@IsNeedRemark",SqlDbType.Bit)};
			parameters[0].Value = model.ScoreName;
			parameters[1].Value = model.Scores;
			parameters[2].Value = model.ScoreContentID;
			parameters[3].Value = model.ScoreContent;
            parameters[4].Value = model.IsNeedRemark;

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
		public void Update(Entity.ScoreInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Score set ");
			strSql.Append("ScoreName=@ScoreName,");
			strSql.Append("Scores=@Scores,");
			strSql.Append("ScoreContentID=@ScoreContentID,");
			strSql.Append("ScoreContent=@ScoreContent,IsNeedRemark=@IsNeedRemark");
			strSql.Append(" where ScoreID=@ScoreID");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreID", SqlDbType.Int,4),
					new SqlParameter("@ScoreName", SqlDbType.NVarChar),
					new SqlParameter("@Scores", SqlDbType.Int,4),
					new SqlParameter("@ScoreContentID", SqlDbType.Int,4),
					new SqlParameter("@ScoreContent", SqlDbType.NVarChar),
                    new SqlParameter("@IsNeedRemark",SqlDbType.Bit)};
			parameters[0].Value = model.ScoreID;
			parameters[1].Value = model.ScoreName;
			parameters[2].Value = model.Scores;
			parameters[3].Value = model.ScoreContentID;
			parameters[4].Value = model.ScoreContent;
            parameters[5].Value = model.IsNeedRemark;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ScoreID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_Score ");
			strSql.Append(" where ScoreID=@ScoreID");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreID", SqlDbType.Int,4)
				};
			parameters[0].Value = ScoreID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Entity.ScoreInfo GetModel(int ScoreID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from T_Score ");
			strSql.Append(" where ScoreID=@ScoreID");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreID", SqlDbType.Int,4)};
			parameters[0].Value = ScoreID;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ScoreInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Entity.ScoreInfo> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM T_Score where 1=1");
			if(strWhere.Trim()!="")
			{
				strSql.Append(strWhere);
			}
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.ScoreInfo>(DbHelperSQL.Query(strSql.ToString()));
		}
    }
}
