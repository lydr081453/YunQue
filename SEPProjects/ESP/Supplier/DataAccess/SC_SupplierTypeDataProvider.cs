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
    public class SC_SupplierTypeDataProvider
    {
        public SC_SupplierTypeDataProvider()
		{}
		#region  成员方法

        public static int Add(SC_SupplierType model)
        {
            return Add(model, null);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
        /// 
		public static int Add(SC_SupplierType model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_SupplierType(");
			strSql.Append("SupplierId,TypeId,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status,TypeLV)");
			strSql.Append(" values (");
			strSql.Append("@SupplierId,@TypeId,@CreatedIP,@CreatTime,@LastModifiedIP,@LastUpdateTime,@Type,@Status,@TypeLV)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
                                        new SqlParameter("@TypeLV", SqlDbType.Int,4)};
			parameters[0].Value = model.SupplierId;
			parameters[1].Value = model.TypeId;
			parameters[2].Value = model.CreatedIP;
			parameters[3].Value = model.CreatTime;
			parameters[4].Value = model.LastModifiedIP;
			parameters[5].Value = model.LastUpdateTime;
			parameters[6].Value = model.Type;
			parameters[7].Value = model.Status;
            parameters[8].Value = model.TypeLV;

            object obj = null;
            if(trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters); 
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(),trans.Connection,trans,parameters);
			if (obj == null)
			{
				return 1;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}

        public static bool Add1(SC_SupplierType model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    model.TypeLV = 3;
                    Add(model, trans);

                    SC_SupplierType modelLV2 = new SC_SupplierType();
                    SC_SupplierType modelLV1 = new SC_SupplierType();

                    modelLV2.SupplierId = model.SupplierId;
                    modelLV2.TypeId = Convert.ToInt32(new DataAccess.XML_VersionListDataProvider().GetModel(Convert.ToInt32(model.TypeId)).ClassID);
                    modelLV2.TypeLV = 2;
                    modelLV2.CreatedIP = model.CreatedIP;
                    modelLV2.CreatTime = model.CreatTime;
                    modelLV2.LastModifiedIP = model.LastModifiedIP;
                    modelLV2.LastUpdateTime = model.LastUpdateTime;
                    Add(modelLV2, trans);

                    modelLV1.SupplierId = model.SupplierId;
                    modelLV1.TypeId = Convert.ToInt32(new DataAccess.XML_VersionClassDataProvider().GetModel(Convert.ToInt32(modelLV2.TypeId)).ParentID);
                    modelLV1.TypeLV = 1;
                    modelLV1.CreatedIP = model.CreatedIP;
                    modelLV1.CreatTime = model.CreatTime;
                    modelLV1.LastModifiedIP = model.LastModifiedIP;
                    modelLV1.LastUpdateTime = model.LastUpdateTime;
                    Add(modelLV1, trans);

                    trans.Commit();
                    return true;
                }
                catch {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public static void Add1(SC_SupplierType model,SqlTransaction trans)
        {
            model.TypeLV = 3;
            Add(model, trans);

            SC_SupplierType modelLV2 = new SC_SupplierType();
            SC_SupplierType modelLV1 = new SC_SupplierType();

            modelLV2.SupplierId = model.SupplierId;
            modelLV2.TypeId = Convert.ToInt32(new DataAccess.XML_VersionListDataProvider().GetModel(Convert.ToInt32(model.TypeId)).ClassID);
            modelLV2.TypeLV = 2;
            modelLV2.CreatedIP = model.CreatedIP;
            modelLV2.CreatTime = model.CreatTime;
            modelLV2.LastModifiedIP = model.LastModifiedIP;
            modelLV2.LastUpdateTime = model.LastUpdateTime;
            Add(modelLV2, trans);

            modelLV1.SupplierId = model.SupplierId;
            modelLV1.TypeId = Convert.ToInt32(new DataAccess.XML_VersionClassDataProvider().GetModel(Convert.ToInt32(modelLV2.TypeId)).ParentID);
            modelLV1.TypeLV = 1;
            modelLV1.CreatedIP = model.CreatedIP;
            modelLV1.CreatTime = model.CreatTime;
            modelLV1.LastModifiedIP = model.LastModifiedIP;
            modelLV1.LastUpdateTime = model.LastUpdateTime;
            Add(modelLV1, trans);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(SC_SupplierType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SC_SupplierType set ");
			strSql.Append("SupplierId=@SupplierId,");
			strSql.Append("TypeId=@TypeId,");
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
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.SupplierId;
			parameters[2].Value = model.TypeId;
			parameters[3].Value = model.CreatedIP;
			parameters[4].Value = model.CreatTime;
			parameters[5].Value = model.LastModifiedIP;
			parameters[6].Value = model.LastUpdateTime;
			parameters[7].Value = model.Type;
			parameters[8].Value = model.Status;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete SC_SupplierType ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
				};
			parameters[0].Value = Id;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        public static void DeleteBySupplierId(int sid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierType ");
            strSql.Append(" where SupplierId=@SupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4)
				};
            parameters[0].Value = sid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据供应商ID和3级物料id删除一组数据
        /// </summary>
        public static void DeleteBySupplierId(int supplierid, int VersionListId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierType ");
            strSql.Append(" where id in(");
            strSql.Append("(select id from sc_suppliertype where supplierid=@supplierid and typeid=@VersionListId) , ");
            strSql.Append(" (select id from sc_suppliertype where supplierid=@supplierid and typeid=@VersionListId )+1,");
            strSql.Append(" (select id from sc_suppliertype where supplierid=@supplierid and typeid=@VersionListId )+2)");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierid", SqlDbType.Int,4),
                    new SqlParameter("@VersionListId",SqlDbType.Int,4)
				};
            parameters[0].Value = supplierid;
            parameters[1].Value = VersionListId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SC_SupplierType GetModel(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from SC_SupplierType ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;
			SC_SupplierType model=new SC_SupplierType();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.Id=Id;
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SupplierId"].ToString()!="")
				{
					model.SupplierId=int.Parse(ds.Tables[0].Rows[0]["SupplierId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TypeId"].ToString()!="")
				{
					model.TypeId=int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
				}
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
			strSql.Append("select [Id],[SupplierId],[TypeId],[CreatedIP],[CreatTime],[LastModifiedIP],[LastUpdateTime],[Type],[Status] ");
			strSql.Append(" FROM SC_SupplierType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public static List<SC_SupplierType> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierType>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public static List<SC_SupplierType> GetListBySupplierId(int sid)
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
