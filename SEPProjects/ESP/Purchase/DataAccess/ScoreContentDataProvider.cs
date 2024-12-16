using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Common;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
	/// 数据访问类ScoreContentDataProvider。
	/// </summary>
	public class ScoreContentDataProvider
	{
		public ScoreContentDataProvider()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ScoreContentID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_ScoreContent");
			strSql.Append(" where ScoreContentID= @ScoreContentID");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreContentID", SqlDbType.Int,4)
				};
			parameters[0].Value = ScoreContentID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.ScoreContentInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_ScoreContent(");
			strSql.Append("Description,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Description,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Description", SqlDbType.NVarChar),
                    new SqlParameter("@Remark", SqlDbType.NVarChar)};
			parameters[0].Value = model.Description;
            parameters[1].Value = model.Remark;

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
		public void Update(Entity.ScoreContentInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_ScoreContent set ");
			strSql.Append("Description=@Description,Remark=@Remark");
			strSql.Append(" where ScoreContentID=@ScoreContentID");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreContentID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar),
                    new SqlParameter("@Remark",SqlDbType.NVarChar)};
			parameters[0].Value = model.ScoreContentID;
			parameters[1].Value = model.Description;
            parameters[2].Value = model.Remark;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ScoreContentID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_ScoreContent ");
			strSql.Append(" where ScoreContentID=@ScoreContentID");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreContentID", SqlDbType.Int,4)
				};
			parameters[0].Value = ScoreContentID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Entity.ScoreContentInfo GetModel(int ScoreContentID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from T_ScoreContent ");
			strSql.Append(" where ScoreContentID=@ScoreContentID");
			SqlParameter[] parameters = {
					new SqlParameter("@ScoreContentID", SqlDbType.Int,4)};
			parameters[0].Value = ScoreContentID;
			return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ScoreContentInfo>(DbHelperSQL.Query(strSql.ToString(),parameters));
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Entity.ScoreContentInfo> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM T_ScoreContent  where 1=1");
			if(strWhere.Trim()!="")
			{
				strSql.Append(strWhere);
			}
			return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.ScoreContentInfo>(DbHelperSQL.Query(strSql.ToString()));
		}
    }
}
