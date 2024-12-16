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
	/// 数据访问类CostDescriptionDAL。
	/// </summary>
    internal class CostDescriptionDataProvider : ESP.Finance.IDataAccess.ICostDescriptionDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CostDescID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_CostDescription");
			strSql.Append(" where CostDescID=@CostDescID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CostDescID", SqlDbType.Int,4)};
			parameters[0].Value = CostDescID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.CostDescriptionInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_CostDescription(");
			strSql.Append("CostDescription)");
			strSql.Append(" values (");
			strSql.Append("@CostDescription)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CostDescription", SqlDbType.NVarChar,500)};
			parameters[0].Value =model.CostDescription;

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
		public int Update(ESP.Finance.Entity.CostDescriptionInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_CostDescription set ");
			strSql.Append("CostDescription=@CostDescription");
			strSql.Append(" where CostDescID=@CostDescID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CostDescID", SqlDbType.Int,4),
					new SqlParameter("@CostDescription", SqlDbType.NVarChar,500)};
			parameters[0].Value =model.CostDescID;
			parameters[1].Value =model.CostDescription;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int CostDescID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_CostDescription ");
			strSql.Append(" where CostDescID=@CostDescID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CostDescID", SqlDbType.Int,4)};
			parameters[0].Value = CostDescID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.CostDescriptionInfo GetModel(int CostDescID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CostDescID,CostDescription from F_CostDescription ");
			strSql.Append(" where CostDescID=@CostDescID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CostDescID", SqlDbType.Int,4)};
			parameters[0].Value = CostDescID;
            return CBO.FillObject<CostDescriptionInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<CostDescriptionInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CostDescID,CostDescription ");
			strSql.Append(" FROM F_CostDescription ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<CostDescriptionInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<CostDescriptionInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}



		#endregion  成员方法
	}
}

