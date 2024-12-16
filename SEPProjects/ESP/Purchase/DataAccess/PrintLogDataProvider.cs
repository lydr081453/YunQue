using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.DataAccess
{
  public  class PrintLogDataProvider
    {
      public PrintLogDataProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Purchase.Entity.PrintLogInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_PrintLog(");
			strSql.Append("FormID,FormType,PrintCount)");
			strSql.Append(" values (");
			strSql.Append("@FormID,@FormType,@PrintCount)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.Int,4),
					new SqlParameter("@FormType", SqlDbType.Int,4),
					new SqlParameter("@PrintCount", SqlDbType.Int,4)};
			parameters[0].Value = model.FormID;
			parameters[1].Value = model.FormType;
			parameters[2].Value = model.PrintCount;

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
		public int Update(ESP.Purchase.Entity.PrintLogInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_PrintLog set ");
			strSql.Append("FormID=@FormID,");
			strSql.Append("FormType=@FormType,");
			strSql.Append("PrintCount=@PrintCount");
			strSql.Append(" where PrintID=@PrintID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PrintID", SqlDbType.Int,4),
					new SqlParameter("@FormID", SqlDbType.Int,4),
					new SqlParameter("@FormType", SqlDbType.Int,4),
					new SqlParameter("@PrintCount", SqlDbType.Int,4)};
			parameters[0].Value = model.PrintID;
			parameters[1].Value = model.FormID;
			parameters[2].Value = model.FormType;
			parameters[3].Value = model.PrintCount;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int PrintID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_PrintLog ");
			strSql.Append(" where PrintID=@PrintID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PrintID", SqlDbType.Int,4)};
			parameters[0].Value = PrintID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Purchase.Entity.PrintLogInfo GetModel(int PrintID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 PrintID,FormID,FormType,PrintCount from T_PrintLog ");
			strSql.Append(" where PrintID=@PrintID ");
			SqlParameter[] parameters = {
					new SqlParameter("@PrintID", SqlDbType.Int,4)};
			parameters[0].Value = PrintID;
           return ESP.Finance.Utility.CBO.FillObject< ESP.Purchase.Entity.PrintLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Purchase.Entity.PrintLogInfo GetModelByFormID(int FormID, int FormType)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PrintID,FormID,FormType,PrintCount from T_PrintLog ");
            strSql.Append(" where FormID=@FormID and  FormType=@FormType");
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.Int,4),
                    new SqlParameter("@FormType",SqlDbType.Int,4)};
            parameters[0].Value = FormID;
            parameters[1].Value = FormType;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.PrintLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public IList<ESP.Purchase.Entity.PrintLogInfo> GetList(string strWhere,List<SqlParameter> parameters) 
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PrintID,FormID,FormType,PrintCount ");
			strSql.Append(" FROM T_PrintLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
		 return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.PrintLogInfo>(DbHelperSQL.Query(strSql.ToString(),parameters.ToArray()));
		}

		#endregion  成员方法
    }
}
