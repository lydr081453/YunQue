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
    public class SC_ChangDiSupplierRemarkDataProvider
    {
        public SC_ChangDiSupplierRemarkDataProvider()
		{ }
        #region  成员方法
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_ChangDiSupplierRemark model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_ChangDiSupplierRemark(");
			strSql.Append("SupplierID,CreatedDate,Remark)");
			strSql.Append(" values (");
            strSql.Append("@SupplierID,@CreatedDate,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar)};
            parameters[0].Value = model.SupplierID;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = model.Remark;

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
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete SC_ChangDiSupplierRemark ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public SC_ChangDiSupplierRemark GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from SC_ChangDiSupplierRemark ");
            strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
			SC_ChangDiSupplierRemark model=new SC_ChangDiSupplierRemark();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            model.ID = id;
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
                model.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Remark"].ToString());
				return model;
			}
			else
			{
			return null;
			}
		}

        public List<SC_ChangDiSupplierRemark> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_ChangDiSupplierRemark ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_ChangDiSupplierRemark>(DbHelperSQL.Query(strSql.ToString()));
        }

		#endregion  成员方法
    }
}
