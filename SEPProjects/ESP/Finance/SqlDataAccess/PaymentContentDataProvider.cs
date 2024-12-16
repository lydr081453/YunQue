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
	/// 数据访问类PaymentContentDAL。
	/// </summary>
    internal class PaymentContentDataProvider : ESP.Finance.IDataAccess.IPaymentContentDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int PaymentContentID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_PaymentContent");
			strSql.Append(" where PaymentContentID=@PaymentContentID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PaymentContentID", SqlDbType.Int,4)};
			parameters[0].Value = PaymentContentID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.PaymentContentInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_PaymentContent(");
			strSql.Append("paymentContent)");
			strSql.Append(" values (");
			strSql.Append("@paymentContent)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@paymentContent", SqlDbType.NVarChar,500)};
			parameters[0].Value =model.paymentContent;

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
		public int Update(ESP.Finance.Entity.PaymentContentInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_PaymentContent set ");
			strSql.Append("paymentContent=@paymentContent");
			strSql.Append(" where PaymentContentID=@PaymentContentID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PaymentContentID", SqlDbType.Int,4),
					new SqlParameter("@paymentContent", SqlDbType.NVarChar,500)};
			parameters[0].Value =model.PaymentContentID;
			parameters[1].Value =model.paymentContent;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int PaymentContentID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_PaymentContent ");
			strSql.Append(" where PaymentContentID=@PaymentContentID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PaymentContentID", SqlDbType.Int,4)};
			parameters[0].Value = PaymentContentID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.PaymentContentInfo GetModel(int PaymentContentID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 PaymentContentID,paymentContent from F_PaymentContent ");
			strSql.Append(" where PaymentContentID=@PaymentContentID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PaymentContentID", SqlDbType.Int,4)};
			parameters[0].Value = PaymentContentID;

            return CBO.FillObject<PaymentContentInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<PaymentContentInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PaymentContentID,paymentContent ");
			strSql.Append(" FROM F_PaymentContent ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<F_PaymentContent>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<PaymentContentInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}



		#endregion  成员方法
	}
}

