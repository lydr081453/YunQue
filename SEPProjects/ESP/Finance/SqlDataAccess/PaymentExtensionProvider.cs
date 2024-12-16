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
	/// 数据访问类LogDAL。
	/// </summary>
    internal class PaymentExtensionProvider : ESP.Finance.IDataAccess.IPaymentExtensionDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.PaymentExtensionInfo model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into F_PaymentExtension(");
            strSql.Append("PaymentId,PaidDate,PaidRemark,logDate,logUserID,OldPaidDate)");
			strSql.Append(" values (");
            strSql.Append("@PaymentId,@PaidDate,@PaidRemark,@logDate,@logUserID,@OldPaidDate)");
            strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PaymentId", SqlDbType.Int,4),
					new SqlParameter("@PaidDate", SqlDbType.DateTime),
					new SqlParameter("@PaidRemark", SqlDbType.NVarChar,1000),
					new SqlParameter("@logDate", SqlDbType.DateTime),
					new SqlParameter("@logUserID", SqlDbType.Int,4),
					new SqlParameter("@OldPaidDate", SqlDbType.DateTime)};
            parameters[0].Value = model.PaymentId;
            parameters[1].Value = model.PaidDate;
            parameters[2].Value = model.PaidRemark;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.LogUserId;
            parameters[5].Value = model.OldPaidDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
		}

        //        /// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(ESP.Finance.Entity.PaymentExtensionInfo model)
        //{
        //    return Update(model, false);
        //}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.PaymentExtensionInfo model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update F_PaymentExtension set ");
            strSql.Append("PaymentId=@PaymentId,");
            strSql.Append("PaidDate=@PaidDate,");
            strSql.Append("PaidRemark=@PaidRemark,");
            strSql.Append("logDate=@logDate,");
            strSql.Append("logUserID=@logUserID,");
            strSql.Append("OldPaidDate=@OldPaidDate");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4),
					new SqlParameter("@PaymentId", SqlDbType.Int,4),
					new SqlParameter("@PaidDate", SqlDbType.DateTime),
					new SqlParameter("@PaidRemark", SqlDbType.NVarChar,1000),
					new SqlParameter("@logDate", SqlDbType.DateTime),
					new SqlParameter("@logUserID", SqlDbType.Int,4),
					new SqlParameter("@OldPaidDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PaymentId;
            parameters[2].Value = model.PaidDate;
            parameters[3].Value = model.PaidRemark;
            parameters[4].Value = DateTime.Now;
            parameters[5].Value = model.LogUserId;
            parameters[6].Value = model.OldPaidDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),  parameters);
		}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public int Delete(int LogID)
        //{
        //    return Delete(LogID, false);
        //}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int LogID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete F_PaymentExtension ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = LogID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.PaymentExtensionInfo GetModel(int LogID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * from F_PaymentExtension ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = LogID;
            return CBO.FillObject<PaymentExtensionInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}



		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<PaymentExtensionInfo> GetList(string term,List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM F_PaymentExtension ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<PaymentExtensionInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}

            return CBO.FillCollection<PaymentExtensionInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}

		

		#endregion  成员方法


    }
}

