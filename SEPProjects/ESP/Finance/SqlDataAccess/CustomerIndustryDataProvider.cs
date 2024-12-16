using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
	/// <summary>
	/// 数据访问类CustomerIndustryDAL。
	/// </summary>
    internal class CustomerIndustryDataProvider : ESP.Finance.IDataAccess.ICustomerIndustryDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int IndustryID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_CustomerIndustry");
			strSql.Append(" where IndustryID=@IndustryID ");
			SqlParameter[] parameters = {
					new SqlParameter("@IndustryID", SqlDbType.Int,4)};
			parameters[0].Value = IndustryID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.CustomerIndustryInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_CustomerIndustry(");
			strSql.Append("IndustryCode,CategoryName,Description,ParentID)");
			strSql.Append(" values (");
			strSql.Append("@IndustryCode,@CategoryName,@Description,@ParentID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@IndustryCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,100),
					new SqlParameter("@ParentID", SqlDbType.Int,4)};
			parameters[0].Value =model.IndustryCode;
			parameters[1].Value =model.CategoryName;
			parameters[2].Value =model.Description;
			parameters[3].Value =model.ParentID;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.CustomerIndustryInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_CustomerIndustry set ");
			strSql.Append("IndustryCode=@IndustryCode,");
			strSql.Append("CategoryName=@CategoryName,");
			strSql.Append("Description=@Description,");
			strSql.Append("ParentID=@ParentID");
			strSql.Append(" where IndustryID=@IndustryID ");
			SqlParameter[] parameters = {
					new SqlParameter("@IndustryID", SqlDbType.Int,4),
					new SqlParameter("@IndustryCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,100),
					new SqlParameter("@ParentID", SqlDbType.Int,4)};
			parameters[0].Value =model.IndustryID;
			parameters[1].Value =model.IndustryCode;
			parameters[2].Value =model.CategoryName;
			parameters[3].Value =model.Description;
			parameters[4].Value =model.ParentID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int IndustryID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_CustomerIndustry ");
			strSql.Append(" where IndustryID=@IndustryID ");
			SqlParameter[] parameters = {
					new SqlParameter("@IndustryID", SqlDbType.Int,4)};
			parameters[0].Value = IndustryID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.CustomerIndustryInfo GetModel(int IndustryID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 IndustryID,IndustryCode,CategoryName,Description,ParentID from F_CustomerIndustry ");
			strSql.Append(" where IndustryID=@IndustryID ");
			SqlParameter[] parameters = {
					new SqlParameter("@IndustryID", SqlDbType.Int,4)};
			parameters[0].Value = IndustryID;

            return CBO.FillObject<CustomerIndustryInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

            //ESP.Finance.Entity.CustomerIndustryInfo model=new ESP.Finance.Entity.CustomerIndustryInfo();
            //DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            //if(ds.Tables[0].Rows.Count>0)
            //{
            //    if(ds.Tables[0].Rows[0]["IndustryID"].ToString()!="")
            //    {
            //        Entity.IndustryID=int.Parse(ds.Tables[0].Rows[0]["IndustryID"].ToString());
            //    }
            //    Entity.IndustryCode=ds.Tables[0].Rows[0]["IndustryCode"].ToString();
            //    Entity.CategoryName=ds.Tables[0].Rows[0]["CategoryName"].ToString();
            //    Entity.Description=ds.Tables[0].Rows[0]["Description"].ToString();
            //    if(ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
            //    {
            //        Entity.ParentID=int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
            //    }
            //    return model;
            //}
            //else
            //{
            //    return null;
            //}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<CustomerIndustryInfo> GetList(string term,List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select IndustryID,IndustryCode,CategoryName,Description,ParentID ");
			strSql.Append(" FROM F_CustomerIndustry ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }

            strSql.Append(" order by IndustryCode desc ");
            
            return CBO.FillCollection<CustomerIndustryInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}


		#endregion  成员方法
	}
}

