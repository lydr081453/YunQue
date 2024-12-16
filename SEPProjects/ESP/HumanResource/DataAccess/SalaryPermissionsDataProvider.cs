using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Collections.Generic;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class SalaryPermissionsDataProvider
	{
		public SalaryPermissionsDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sep_SalaryPermissions");
			strSql.Append(" where ID= @ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SalaryPermissionsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sep_SalaryPermissions(");
			strSql.Append("SalaryPermissions,Description)");
			strSql.Append(" values (");
			strSql.Append("@SalaryPermissions,@Description)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SalaryPermissions", SqlDbType.Decimal,9),
					new SqlParameter("@Description", SqlDbType.NVarChar)};
			parameters[0].Value = model.SalaryPermissions;
			parameters[1].Value = model.Description;

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
		public void Update(SalaryPermissionsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sep_SalaryPermissions set ");
			strSql.Append("SalaryPermissions=@SalaryPermissions,");
			strSql.Append("Description=@Description");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@SalaryPermissions", SqlDbType.Decimal,9),
					new SqlParameter("@Description", SqlDbType.NVarChar)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.SalaryPermissions;
			parameters[2].Value = model.Description;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete sep_SalaryPermissions ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SalaryPermissionsInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from sep_SalaryPermissions ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
            SalaryPermissionsInfo model = new SalaryPermissionsInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.ID=ID;
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SalaryPermissions"].ToString()!="")
				{
					model.SalaryPermissions=decimal.Parse(ds.Tables[0].Rows[0]["SalaryPermissions"].ToString());
				}
				model.Description=ds.Tables[0].Rows[0]["Description"].ToString();
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
			strSql.Append("select [ID],[SalaryPermissions],[Description] ");
			strSql.Append(" FROM sep_SalaryPermissions ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "sep_SalaryPermissions";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}
