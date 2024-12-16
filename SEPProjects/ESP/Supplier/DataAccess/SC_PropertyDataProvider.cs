using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;

namespace ESP.Supplier.DataAccess
{
    public class SC_PropertyDataProvider
    {
        public SC_PropertyDataProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_Property model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_Property(");
			strSql.Append("PropertyDes,PropertyStatus)");
			strSql.Append(" values (");
			strSql.Append("@PropertyDes,@PropertyStatus)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PropertyDes", SqlDbType.NVarChar),
					new SqlParameter("@PropertyStatus", SqlDbType.Int,4)};
			parameters[0].Value = model.PropertyDes;
			parameters[1].Value = model.PropertyStatus;

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
		public void Update(SC_Property model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SC_Property set ");
			strSql.Append("PropertyDes=@PropertyDes,");
			strSql.Append("PropertyStatus=@PropertyStatus");
			strSql.Append(" where PropertyId=@PropertyId");
			SqlParameter[] parameters = {
					new SqlParameter("@PropertyId", SqlDbType.Int,4),
					new SqlParameter("@PropertyDes", SqlDbType.NVarChar),
					new SqlParameter("@PropertyStatus", SqlDbType.Int,4)};
			parameters[0].Value = model.PropertyId;
			parameters[1].Value = model.PropertyDes;
			parameters[2].Value = model.PropertyStatus;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int PropertyId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete SC_Property ");
			strSql.Append(" where PropertyId=@PropertyId");
			SqlParameter[] parameters = {
					new SqlParameter("@PropertyId", SqlDbType.Int,4)
				};
			parameters[0].Value = PropertyId;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SC_Property GetModel(int PropertyId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from SC_Property ");
			strSql.Append(" where PropertyId=@PropertyId");
			SqlParameter[] parameters = {
					new SqlParameter("@PropertyId", SqlDbType.Int,4)};
			parameters[0].Value = PropertyId;
			SC_Property model=new SC_Property();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.PropertyId=PropertyId;
			if(ds.Tables[0].Rows.Count>0)
			{
				model.PropertyDes=ds.Tables[0].Rows[0]["PropertyDes"].ToString();
				if(ds.Tables[0].Rows[0]["PropertyStatus"].ToString()!="")
				{
					model.PropertyStatus=int.Parse(ds.Tables[0].Rows[0]["PropertyStatus"].ToString());
				}
				return model;
			}
			else
			{
			return null;
			}
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select [PropertyId],[PropertyDes],[PropertyStatus] ");
			strSql.Append(" FROM SC_Property ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public List<SC_Property> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Property>(GetList(""));
        }

		#endregion  成员方法
    }
}
