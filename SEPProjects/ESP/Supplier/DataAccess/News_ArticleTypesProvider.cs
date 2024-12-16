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
    public class ArticleTypesProvider
    {
        public ArticleTypesProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ArticleTypes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ArticleTypes(");
            strSql.Append("TypeName,TypeCode,CreatedDate,ModifiedDate,IsDel)");
			strSql.Append(" values (");
            strSql.Append("@TypeName,@TypeCode,@CreatedDate,@ModifiedDate,@IsDel)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime)};
			parameters[0].Value = model.TypeName;
            parameters[1].Value = model.TypeCode;
            parameters[2].Value = model.IsDel;
			parameters[3].Value = model.CreatedDate;
			parameters[4].Value = model.ModifiedDate;

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
		public void Update(ArticleTypes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ArticleTypes set ");
			strSql.Append("TypeName=@TypeName,");
			strSql.Append("TypeCode=@TypeCode,");
            strSql.Append("IsDel=@IsDel,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("ModifiedDate=@ModifiedDate");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.TypeName;
            parameters[2].Value = model.TypeCode;
            parameters[3].Value = model.IsDel;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.ModifiedDate;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ArticleTypes ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ArticleTypes GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,TypeName,TypeCode,IsDel,CreatedDate,ModifiedDate from ArticleTypes ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return CBO.FillObject<ArticleTypes>(DbHelperSQL.Query(strSql.ToString(),parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ArticleTypes> GetList(string strWhere,List<SqlParameter> parms )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,TypeName,TypeCode,IsDel,CreatedDate,ModifiedDate ");
			strSql.Append(" FROM ArticleTypes where 1=1");
			if(strWhere.Trim()!="")
			{
				strSql.Append(strWhere);
			}
			return CBO.FillCollection<ArticleTypes>(DbHelperSQL.Query(strSql.ToString(),parms.ToArray()));
		}

        #endregion
    }
}
