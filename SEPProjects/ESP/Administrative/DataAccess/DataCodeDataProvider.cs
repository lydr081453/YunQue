using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;

namespace ESP.Administrative.DataAccess
{
    public class DataCodeDataProvider
    {
        public DataCodeDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DataCodeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_DataCode");
			strSql.Append(" where DataCodeID= @DataCodeID");
			SqlParameter[] parameters = {
					new SqlParameter("@DataCodeID", SqlDbType.Int,4)
				};
			parameters[0].Value = DataCodeID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DataCodeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_DataCode(");
			strSql.Append("Name,Type,Code,Deleted,Sort)");
			strSql.Append(" values (");
			strSql.Append("@Name,@Type,@Code,@Deleted,@Sort)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.NVarChar),
					new SqlParameter("@Code", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.Code;
			parameters[3].Value = model.Deleted;
			parameters[4].Value = model.Sort;

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
        public void Update(DataCodeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_DataCode set ");
			strSql.Append("Name=@Name,");
			strSql.Append("Type=@Type,");
			strSql.Append("Code=@Code,");
			strSql.Append("Deleted=@Deleted,");
			strSql.Append("Sort=@Sort");
			strSql.Append(" where DataCodeID=@DataCodeID");
			SqlParameter[] parameters = {
					new SqlParameter("@DataCodeID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.NVarChar),
					new SqlParameter("@Code", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
			parameters[0].Value = model.DataCodeID;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.Code;
			parameters[4].Value = model.Deleted;
			parameters[5].Value = model.Sort;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int DataCodeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_DataCode ");
			strSql.Append(" where DataCodeID=@DataCodeID");
			SqlParameter[] parameters = {
					new SqlParameter("@DataCodeID", SqlDbType.Int,4)
				};
			parameters[0].Value = DataCodeID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public DataCodeInfo GetModel(int DataCodeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from AD_DataCode ");
			strSql.Append(" where DataCodeID=@DataCodeID");
			SqlParameter[] parameters = {
					new SqlParameter("@DataCodeID", SqlDbType.Int,4)};
			parameters[0].Value = DataCodeID;
            DataCodeInfo model = new DataCodeInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.DataCodeID=DataCodeID;
			if(ds.Tables[0].Rows.Count>0)
			{
                model.PopupData(ds.Tables[0].Rows[0]);
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
            strSql.Append("select * FROM AD_DataCode ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		#endregion  成员方法
	}
}