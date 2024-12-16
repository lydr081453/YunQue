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
	/// 数据访问类ContractStatusDAL。
	/// </summary>
    internal class ContractStatusDataProvider : ESP.Finance.IDataAccess.IContractStatusDataProvider
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ContractStatusID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_ContractStatus");
			strSql.Append(" where ContractStatusID=@ContractStatusID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractStatusID", SqlDbType.Int,4)};
			parameters[0].Value = ContractStatusID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.ContractStatusInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_ContractStatus(");
			strSql.Append("ContractStatusName,Description)");
			strSql.Append(" values (");
			strSql.Append("@ContractStatusName,@Description)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractStatusName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,100)};
			parameters[0].Value =model.ContractStatusName;
			parameters[1].Value =model.Description;

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
		public int Update(ESP.Finance.Entity.ContractStatusInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_ContractStatus set ");
			strSql.Append("ContractStatusName=@ContractStatusName,");
			strSql.Append("Description=@Description");
			strSql.Append(" where ContractStatusID=@ContractStatusID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractStatusID", SqlDbType.Int,4),
					new SqlParameter("@ContractStatusName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,100)};
			parameters[0].Value =model.ContractStatusID;
			parameters[1].Value =model.ContractStatusName;
			parameters[2].Value =model.Description;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int ContractStatusID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_ContractStatus ");
			strSql.Append(" where ContractStatusID=@ContractStatusID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractStatusID", SqlDbType.Int,4)};
			parameters[0].Value = ContractStatusID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.ContractStatusInfo GetModel(int ContractStatusID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ContractStatusID,ContractStatusName,Description from F_ContractStatus ");
			strSql.Append(" where ContractStatusID=@ContractStatusID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractStatusID", SqlDbType.Int,4)};
			parameters[0].Value = ContractStatusID;

            return CBO.FillObject<ContractStatusInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

            //ESP.Finance.Entity.ContractStatusInfo model=new ESP.Finance.Entity.ContractStatusInfo();
            //DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            //if(ds.Tables[0].Rows.Count>0)
            //{
            //    if(ds.Tables[0].Rows[0]["ContractStatusID"].ToString()!="")
            //    {
            //        Entity.ContractStatusID=int.Parse(ds.Tables[0].Rows[0]["ContractStatusID"].ToString());
            //    }
            //    Entity.ContractStatusName=ds.Tables[0].Rows[0]["ContractStatusName"].ToString();
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
        public IList<ContractStatusInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ContractStatusID,ContractStatusName,Description ");
			strSql.Append(" FROM F_ContractStatus ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<ContractStatusInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<ContractStatusInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}



		#endregion  成员方法
	}
}

