using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.Common;

namespace ESP.Purchase.DataAccess
{
    public class BlackListDataProvider
    {
        public BlackListDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int BlackListID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_BlackList");
			strSql.Append(" where BlackListID=@BlackListID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BlackListID", SqlDbType.Int,4)};
			parameters[0].Value = BlackListID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Purchase.Entity.BlackListInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_BlackList(");
			strSql.Append("UserID,EmployeeName,Status)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@EmployeeName,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.EmployeeName;
			parameters[2].Value = model.Status;

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
        public int Update(ESP.Purchase.Entity.BlackListInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_BlackList set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("EmployeeName=@EmployeeName,");
			strSql.Append("Status=@Status");
			strSql.Append(" where BlackListID=@BlackListID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BlackListID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.BlackListID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.EmployeeName;
			parameters[3].Value = model.Status;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int BlackListID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_BlackList ");
			strSql.Append(" where BlackListID=@BlackListID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BlackListID", SqlDbType.Int,4)};
			parameters[0].Value = BlackListID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Purchase.Entity.BlackListInfo GetModel(int BlackListID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 BlackListID,UserID,EmployeeName,Status from T_BlackList ");
			strSql.Append(" where BlackListID=@BlackListID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BlackListID", SqlDbType.Int,4)};
			parameters[0].Value = BlackListID;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.BlackListInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
       
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<ESP.Purchase.Entity.BlackListInfo> GetList(string strWhere,List<SqlParameter> paramList)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BlackListID,UserID,EmployeeName,Status ");
			strSql.Append(" FROM T_BlackList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.BlackListInfo>(DbHelperSQL.Query(strWhere.ToString(), paramList.ToArray()));
        }
        #endregion
    }
}
