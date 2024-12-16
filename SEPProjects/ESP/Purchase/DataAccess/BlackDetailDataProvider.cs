using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.Common;

namespace ESP.Purchase.DataAccess
{
   public  class BlackDetailDataProvider
    {
       public BlackDetailDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int BlackDetailID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_BlackDetail");
			strSql.Append(" where BlackDetailID=@BlackDetailID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BlackDetailID", SqlDbType.Int,4)};
			parameters[0].Value = BlackDetailID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Purchase.Entity.BlackDetailInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_BlackDetail(");
			strSql.Append("AuditerID,AuditerName,AuditTime,OrderID,OrderType,SendMailTime,SenderID,SenderName,ResponseTime,ResponseContent,Status)");
			strSql.Append(" values (");
			strSql.Append("@AuditerID,@AuditerName,@AuditTime,@OrderID,@OrderType,@SendMailTime,@SenderID,@SenderName,@ResponseTime,@ResponseContent,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditerID", SqlDbType.Int,4),
					new SqlParameter("@AuditerName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditTime", SqlDbType.DateTime),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@OrderType", SqlDbType.Int,4),
					new SqlParameter("@SendMailTime", SqlDbType.DateTime),
					new SqlParameter("@SenderID", SqlDbType.NChar,10),
					new SqlParameter("@SenderName", SqlDbType.NChar,10),
					new SqlParameter("@ResponseTime", SqlDbType.DateTime),
					new SqlParameter("@ResponseContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.AuditerID;
			parameters[1].Value = model.AuditerName;
			parameters[2].Value = model.AuditTime;
			parameters[3].Value = model.OrderID;
			parameters[4].Value = model.OrderType;
			parameters[5].Value = model.SendMailTime;
			parameters[6].Value = model.SenderID;
			parameters[7].Value = model.SenderName;
			parameters[8].Value = model.ResponseTime;
			parameters[9].Value = model.ResponseContent;
			parameters[10].Value = model.Status;

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
		public int Update(ESP.Purchase.Entity.BlackDetailInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_BlackDetail set ");
			strSql.Append("AuditerID=@AuditerID,");
			strSql.Append("AuditerName=@AuditerName,");
			strSql.Append("AuditTime=@AuditTime,");
			strSql.Append("OrderID=@OrderID,");
			strSql.Append("OrderType=@OrderType,");
			strSql.Append("SendMailTime=@SendMailTime,");
			strSql.Append("SenderID=@SenderID,");
			strSql.Append("SenderName=@SenderName,");
			strSql.Append("ResponseTime=@ResponseTime,");
			strSql.Append("ResponseContent=@ResponseContent,");
			strSql.Append("Status=@Status");
			strSql.Append(" where BlackDetailID=@BlackDetailID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BlackDetailID", SqlDbType.Int,4),
					new SqlParameter("@AuditerID", SqlDbType.Int,4),
					new SqlParameter("@AuditerName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditTime", SqlDbType.DateTime),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@OrderType", SqlDbType.Int,4),
					new SqlParameter("@SendMailTime", SqlDbType.DateTime),
					new SqlParameter("@SenderID", SqlDbType.NChar,10),
					new SqlParameter("@SenderName", SqlDbType.NChar,10),
					new SqlParameter("@ResponseTime", SqlDbType.DateTime),
					new SqlParameter("@ResponseContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.BlackDetailID;
			parameters[1].Value = model.AuditerID;
			parameters[2].Value = model.AuditerName;
			parameters[3].Value = model.AuditTime;
			parameters[4].Value = model.OrderID;
			parameters[5].Value = model.OrderType;
			parameters[6].Value = model.SendMailTime;
			parameters[7].Value = model.SenderID;
			parameters[8].Value = model.SenderName;
			parameters[9].Value = model.ResponseTime;
			parameters[10].Value = model.ResponseContent;
			parameters[11].Value = model.Status;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int BlackDetailID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_BlackDetail ");
			strSql.Append(" where BlackDetailID=@BlackDetailID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BlackDetailID", SqlDbType.Int,4)};
			parameters[0].Value = BlackDetailID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Purchase.Entity.BlackDetailInfo GetModel(int BlackDetailID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 BlackDetailID,AuditerID,AuditerName,AuditTime,OrderID,OrderType,SendMailTime,SenderID,SenderName,ResponseTime,ResponseContent,Status from T_BlackDetail ");
			strSql.Append(" where BlackDetailID=@BlackDetailID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BlackDetailID", SqlDbType.Int,4)};
			parameters[0].Value = BlackDetailID;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.BlackDetailInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
       
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public IList<ESP.Purchase.Entity.BlackDetailInfo> GetList(string strWhere,List<SqlParameter> paramList)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BlackDetailID,AuditerID,AuditerName,AuditTime,OrderID,OrderType,SendMailTime,SenderID,SenderName,ResponseTime,ResponseContent,Status ");
			strSql.Append(" FROM T_BlackDetail ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.BlackDetailInfo>(DbHelperSQL.Query(strSql.ToString(), paramList.ToArray()));
		}
		#endregion  成员方法
    }
}
