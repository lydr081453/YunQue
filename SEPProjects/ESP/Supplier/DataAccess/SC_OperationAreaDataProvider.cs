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
    public class SC_OperationAreaDataProvider
    {
        public SC_OperationAreaDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SC_OperationArea");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_OperationArea model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_OperationArea(");
			strSql.Append("supplierId,operationArea,percentum)");
			strSql.Append(" values (");
			strSql.Append("@supplierId,@operationArea,@percentum)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@supplierId", SqlDbType.Int,4),
					new SqlParameter("@operationArea", SqlDbType.NVarChar,50),
					new SqlParameter("@percentum", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.supplierId;
			parameters[1].Value = model.operationArea;
			parameters[2].Value = model.percentum;

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
		public void Update(SC_OperationArea model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SC_OperationArea set ");
			strSql.Append("supplierId=@supplierId,");
			strSql.Append("operationArea=@operationArea,");
			strSql.Append("percentum=@percentum");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@supplierId", SqlDbType.Int,4),
					new SqlParameter("@operationArea", SqlDbType.NVarChar,50),
					new SqlParameter("@percentum", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.supplierId;
			parameters[2].Value = model.operationArea;
			parameters[3].Value = model.percentum;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SC_OperationArea ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SC_OperationArea GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,supplierId,operationArea,percentum from SC_OperationArea ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			SC_OperationArea model=new SC_OperationArea();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["supplierId"].ToString()!="")
				{
					model.supplierId=int.Parse(ds.Tables[0].Rows[0]["supplierId"].ToString());
				}
				model.operationArea=ds.Tables[0].Rows[0]["operationArea"].ToString();
				model.percentum=ds.Tables[0].Rows[0]["percentum"].ToString();
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
			strSql.Append("select id,supplierId,operationArea,percentum ");
			strSql.Append(" FROM SC_OperationArea ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,supplierId,operationArea,percentum ");
			strSql.Append(" FROM SC_OperationArea ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

	

		#endregion  成员方法
    }
}
