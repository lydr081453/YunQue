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
    public class SC_SupplierInfomationDataProvider
    {
        public SC_SupplierInfomationDataProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_SupplierInfomation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_SupplierInfomation(");
			strSql.Append("SupplierId,Title,Content,attachment,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status)");
			strSql.Append(" values (");
			strSql.Append("@SupplierId,@Title,@Content,@attachment,@CreatedIP,@CreatTime,@LastModifiedIP,@LastUpdateTime,@Type,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Content", SqlDbType.NVarChar),
					new SqlParameter("@attachment", SqlDbType.NVarChar),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.SupplierId;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Content;
			parameters[3].Value = model.attachment;
			parameters[4].Value = model.CreatedIP;
			parameters[5].Value = model.CreatTime;
			parameters[6].Value = model.LastModifiedIP;
			parameters[7].Value = model.LastUpdateTime;
			parameters[8].Value = model.Type;
			parameters[9].Value = model.Status;

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
		public void Update(SC_SupplierInfomation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SC_SupplierInfomation set ");
			strSql.Append("SupplierId=@SupplierId,");
			strSql.Append("Title=@Title,");
			strSql.Append("Content=@Content,");
			strSql.Append("attachment=@attachment,");
			strSql.Append("CreatedIP=@CreatedIP,");
			strSql.Append("CreatTime=@CreatTime,");
			strSql.Append("LastModifiedIP=@LastModifiedIP,");
			strSql.Append("LastUpdateTime=@LastUpdateTime,");
			strSql.Append("Type=@Type,");
			strSql.Append("Status=@Status");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Content", SqlDbType.NVarChar),
					new SqlParameter("@attachment", SqlDbType.NVarChar),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.SupplierId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.Content;
			parameters[4].Value = model.attachment;
			parameters[5].Value = model.CreatedIP;
			parameters[6].Value = model.CreatTime;
			parameters[7].Value = model.LastModifiedIP;
			parameters[8].Value = model.LastUpdateTime;
			parameters[9].Value = model.Type;
			parameters[10].Value = model.Status;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete SC_SupplierInfomation ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
				};
			parameters[0].Value = Id;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SC_SupplierInfomation GetModel(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from SC_SupplierInfomation ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;
			SC_SupplierInfomation model=new SC_SupplierInfomation();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.Id=Id;
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SupplierId"].ToString()!="")
				{
					model.SupplierId=int.Parse(ds.Tables[0].Rows[0]["SupplierId"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				model.attachment=ds.Tables[0].Rows[0]["attachment"].ToString();
				model.CreatedIP=ds.Tables[0].Rows[0]["CreatedIP"].ToString();
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
				}
				model.LastModifiedIP=ds.Tables[0].Rows[0]["LastModifiedIP"].ToString();
				if(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString()!="")
				{
					model.LastUpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Type"].ToString()!="")
				{
					model.Type=int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
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
			strSql.Append("select [Id],[SupplierId],[Title],[Content],[attachment],[CreatedIP],[CreatTime],[LastModifiedIP],[LastUpdateTime],[Type],[Status] ");
			strSql.Append(" FROM SC_SupplierInfomation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public static List<SC_SupplierInfomation> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierInfomation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierInfomation>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public static List<SC_SupplierInfomation> GetListBySupplierId(int sid)
        {
            string strWhere = string.Empty;
            strWhere += " SupplierId=@SupplierId";
            SqlParameter[] parameters = { new SqlParameter("@SupplierId", SqlDbType.Int, 4) };
            parameters[0].Value = sid;

            return GetList(strWhere, parameters);
        }

		#endregion  成员方法
    }
}
