using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_SupplierActiveEmailSendLogDataProvider
    {
        public SC_SupplierActiveEmailSendLogDataProvider()
		{ }
        #region  成员方法
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_SupplierActiveEmailSendLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_SupplierActiveEmailSendLog(");
			strSql.Append("SupplierID,CreatedDate,sysId,sysName)");
			strSql.Append(" values (");
            strSql.Append("@SupplierID,@CreatedDate,@sysId,@sysName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@sysId",SqlDbType.Int,4),
                    new SqlParameter("@sysName",SqlDbType.NVarChar,50)                    
                                        };
            parameters[0].Value = model.SupplierID;
			parameters[1].Value = DateTime.Now;
            parameters[2].Value = model.SysId;
            parameters[3].Value = model.SysName;

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
		/// 得到一个对象实体
		/// </summary>
		public SC_SupplierActiveEmailSendLog GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from SC_SupplierActiveEmailSendLog ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;
			SC_SupplierActiveEmailSendLog model=new SC_SupplierActiveEmailSendLog();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.ID=id;
			if(ds.Tables[0].Rows.Count>0)
			{
                model.SupplierID = Convert.ToInt32(ds.Tables[0].Rows[0]["SupplierID"]);
				return model;
			}
			else
			{
			return null;
			}
		}

        public List<SC_SupplierActiveEmailSendLog> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierActiveEmailSendLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierActiveEmailSendLog>(DbHelperSQL.Query(strSql.ToString()));
        }

		#endregion  成员方法
    }
}
