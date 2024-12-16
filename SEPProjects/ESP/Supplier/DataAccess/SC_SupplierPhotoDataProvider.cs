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
    public class SC_SupplierPhotoDataProvider
    {
        public SC_SupplierPhotoDataProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_SupplierPhoto model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_SupplierPhoto(");
			strSql.Append("SupplierId,ShowTxt,PhotoUrl,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status)");
			strSql.Append(" values (");
			strSql.Append("@SupplierId,@ShowTxt,@PhotoUrl,@CreatedIP,@CreatTime,@LastModifiedIP,@LastUpdateTime,@Type,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ShowTxt", SqlDbType.NVarChar),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.SupplierId;
			parameters[1].Value = model.ShowTxt;
			parameters[2].Value = model.PhotoUrl;
			parameters[3].Value = model.CreatedIP;
			parameters[4].Value = model.CreatTime;
			parameters[5].Value = model.LastModifiedIP;
			parameters[6].Value = model.LastUpdateTime;
			parameters[7].Value = model.Type;
			parameters[8].Value = model.Status;

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
		public void Update(SC_SupplierPhoto model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SC_SupplierPhoto set ");
			strSql.Append("SupplierId=@SupplierId,");
			strSql.Append("ShowTxt=@ShowTxt,");
			strSql.Append("PhotoUrl=@PhotoUrl,");
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
					new SqlParameter("@ShowTxt", SqlDbType.NVarChar),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.SupplierId;
			parameters[2].Value = model.ShowTxt;
			parameters[3].Value = model.PhotoUrl;
			parameters[4].Value = model.CreatedIP;
			parameters[5].Value = model.CreatTime;
			parameters[6].Value = model.LastModifiedIP;
			parameters[7].Value = model.LastUpdateTime;
			parameters[8].Value = model.Type;
			parameters[9].Value = model.Status;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete SC_SupplierPhoto ");
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
		public SC_SupplierPhoto GetModel(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from SC_SupplierPhoto ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;
			SC_SupplierPhoto model=new SC_SupplierPhoto();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.Id=Id;
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SupplierId"].ToString()!="")
				{
					model.SupplierId=int.Parse(ds.Tables[0].Rows[0]["SupplierId"].ToString());
				}
				model.ShowTxt=ds.Tables[0].Rows[0]["ShowTxt"].ToString();
				model.PhotoUrl=ds.Tables[0].Rows[0]["PhotoUrl"].ToString();
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
			strSql.Append("select [Id],[SupplierId],[ShowTxt],[PhotoUrl],[CreatedIP],[CreatTime],[LastModifiedIP],[LastUpdateTime],[Type],[Status] ");
			strSql.Append(" FROM SC_SupplierPhoto ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
        }

        public static List<SC_SupplierPhoto> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierPhoto ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierPhoto>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public static List<SC_SupplierPhoto> GetListBySupplierId(int sid)
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
