using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
	/// <summary>
	/// 数据访问类CreditDAL。
	/// </summary>
    internal class CreditDataProvider : ESP.Finance.IDataAccess.ICreditDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CreditID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_Credit");
			strSql.Append(" where CreditID=@CreditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CreditID", SqlDbType.Int,4)};
			parameters[0].Value = CreditID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.CreditInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_Credit(");
			strSql.Append("CreditCode,CreditType,Description)");
			strSql.Append(" values (");
			strSql.Append("@CreditCode,@CreditType,@Description)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreditCode", SqlDbType.NVarChar,10),
					new SqlParameter("@CreditType", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,50)};
			parameters[0].Value =model.CreditCode;
			parameters[1].Value =model.CreditType;
			parameters[2].Value =model.Description;

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
		public int Update(ESP.Finance.Entity.CreditInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_Credit set ");
			strSql.Append("CreditCode=@CreditCode,");
			strSql.Append("CreditType=@CreditType,");
			strSql.Append("Description=@Description");
			strSql.Append(" where CreditID=@CreditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CreditID", SqlDbType.Int,4),
					new SqlParameter("@CreditCode", SqlDbType.NVarChar,10),
					new SqlParameter("@CreditType", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,50)};
			parameters[0].Value =model.CreditID;
			parameters[1].Value =model.CreditCode;
			parameters[2].Value =model.CreditType;
			parameters[3].Value =model.Description;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int CreditID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_Credit ");
			strSql.Append(" where CreditID=@CreditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CreditID", SqlDbType.Int,4)};
			parameters[0].Value = CreditID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.CreditInfo GetModel(int CreditID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CreditID,CreditCode,CreditType,Description from F_Credit ");
			strSql.Append(" where CreditID=@CreditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CreditID", SqlDbType.Int,4)};
			parameters[0].Value = CreditID;

            return CBO.FillObject<CreditInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

            //ESP.Finance.Entity.CreditInfo model=new ESP.Finance.Entity.CreditInfo();
            //DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            //if(ds.Tables[0].Rows.Count>0)
            //{
            //    if(ds.Tables[0].Rows[0]["CreditID"].ToString()!="")
            //    {
            //        Entity.CreditID=int.Parse(ds.Tables[0].Rows[0]["CreditID"].ToString());
            //    }
            //    Entity.CreditCode=ds.Tables[0].Rows[0]["CreditCode"].ToString();
            //    Entity.CreditType=ds.Tables[0].Rows[0]["CreditType"].ToString();
            //    Entity.Description=ds.Tables[0].Rows[0]["Description"].ToString();
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
        public IList<CreditInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CreditID,CreditCode,CreditType,Description ");
			strSql.Append(" FROM F_Credit ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<CreditInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<CreditInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}



		#endregion  成员方法
	}
}

