using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类ProductDataProvider。
    /// </summary>
    public class ProductDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDataProvider"/> class.
        /// </summary>
        public ProductDataProvider()
        { }

        #region  成员方法

        public static int Add(ProductInfo model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ProductInfo model,SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Product(");
            strSql.Append("productType,productName,productDes,productUnit,supplierId,productClass,productprice,IsShow)");
            strSql.Append(" values (");
            strSql.Append("@productType,@productName,@productDes,@productUnit,@supplierId,@productClass,@productprice,@IsShow)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@productType", SqlDbType.Int,4),
					new SqlParameter("@productName", SqlDbType.VarChar,100),
					new SqlParameter("@productDes", SqlDbType.NVarChar),
					new SqlParameter("@productUnit", SqlDbType.VarChar,100),
                    new SqlParameter("@supplierId",SqlDbType.Int,4),
                    new SqlParameter("@productClass",SqlDbType.VarChar,200),
                    new SqlParameter("@productprice",SqlDbType.Decimal),
                    new SqlParameter("@IsShow",SqlDbType.Int,4)};
            parameters[0].Value = model.productType;
            parameters[1].Value = model.productName;
            parameters[2].Value = model.productDes;
            parameters[3].Value = model.productUnit;
            parameters[4].Value = model.supplierId;
            parameters[5].Value = model.productClass;
            parameters[6].Value = model.ProductPrice;
            parameters[7].Value = model.IsShow;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),conn,trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static int Update(ProductInfo model)
        {
            return Update(model, null, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ProductInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Product set ");
            strSql.Append("productType=@productType,");
            strSql.Append("supplierId=@supplierId,");
            strSql.Append("productName=@productName,");
            strSql.Append("productDes=@productDes,");
            strSql.Append("productUnit=@productUnit,");
            strSql.Append("productClass=@productClass,");
            strSql.Append("productprice=@productprice, ");
            strSql.Append("IsShow=@IsShow ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@productType", SqlDbType.Int,4),
                    new SqlParameter("@supplierId",SqlDbType.Int,4),
					new SqlParameter("@productName", SqlDbType.VarChar,100),
					new SqlParameter("@productDes", SqlDbType.NVarChar),
					new SqlParameter("@productUnit", SqlDbType.VarChar,100),
                    new SqlParameter("@productClass",SqlDbType.VarChar,200),
                    new SqlParameter("@productprice",SqlDbType.Decimal),
                    new SqlParameter("@IsShow",SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.productType;
            parameters[2].Value = model.supplierId;
            parameters[3].Value = model.productName;
            parameters[4].Value = model.productDes;
            parameters[5].Value = model.productUnit;
            parameters[6].Value = model.productClass;
            parameters[7].Value = model.ProductPrice;
            parameters[8].Value = model.IsShow;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Product ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一批数据
        /// </summary>
        public static int Delete(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Product ");
            //strSql.Append(" where id in (@ids)");
            strSql.Append(" where id in (" + ids + ")");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@ids", ids)
            //    };
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 屏蔽目录物品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static int DisabledData(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Product set isshow=@isshow");
            //strSql.Append(" where id in (@ids)");
            strSql.Append(" where id in (" + ids + ")");
            SqlParameter[] parameters = {
                    new SqlParameter("@isshow",0)
					//,new SqlParameter("@ids", ids)
				};
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ProductInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.*,b.supplier_name as supplierName,c.typename as typename from t_product as a ");
            strSql.Append(" inner join t_supplier as b on a.supplierId = b.id");
            strSql.Append(" inner join t_type as c on a.productType = c.typeid");
            strSql.Append(" where a.id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            ProductInfo model = new ProductInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["productType"].ToString() != "")
                {
                    model.productType = int.Parse(ds.Tables[0].Rows[0]["productType"].ToString());
                }
                model.productName = ds.Tables[0].Rows[0]["productName"].ToString();
                model.productDes = ds.Tables[0].Rows[0]["productDes"].ToString();
                model.productUnit = ds.Tables[0].Rows[0]["productUnit"].ToString();
                if (ds.Tables[0].Rows[0]["supplierId"].ToString() != "")
                {
                    model.supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierId"].ToString());
                }
                model.supplierName = ds.Tables[0].Rows[0]["suppliername"].ToString();
                model.typename = ds.Tables[0].Rows[0]["typename"].ToString();
                model.productClass = ds.Tables[0].Rows[0]["productClass"].ToString();
                if (ds.Tables[0].Rows[0]["productprice"].ToString() != "")
                {
                    model.ProductPrice = decimal.Parse(ds.Tables[0].Rows[0]["productprice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsShow"].ToString() != "")
                {
                    model.IsShow = int.Parse(ds.Tables[0].Rows[0]["IsShow"].ToString());
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
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[productType],[productName],[productDes],[productUnit],[supplierId],[productClass],[productprice],[IsShow] ");
            strSql.Append(" FROM T_Product ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1" + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得供应商的物料类别
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <returns></returns>
        public static string GetTypeNameBySupplierId(int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct b.typename from t_product as a inner join t_type as b on a.producttype=b.typeid");
            strSql.Append(" where supplierid="+ supplierId);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            string typeNames = "";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    typeNames += dr["typename"].ToString() + ",";
                }
                typeNames = typeNames.Remove(typeNames.Length - 1);
            }
            return typeNames;
        }

        public static List<ProductInfo> getModelList(string terms, List<SqlParameter> parms)
        {
            return getModelList(terms, parms, "");
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<ProductInfo> getModelList(string terms, List<SqlParameter> parms,string orderBy)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.*,b.supplier_name as supplierName,c.typename as typename from t_product as a ");
            //添加采购物品新改动
            strSql.Append(" inner join t_supplier as b on a.supplierId = b.id and b.supplier_status="+State.supplierstatus_used.ToString());
//            strSql.Append(@" inner join (select a.id as supplyId,a.supplier_name as supplyName,c.* from sc_supplier as a 
//                                            left join t_espandsupplysuppliersrelation as b on a.id=b.supplysupplierid
//                                            left join t_supplier as c on b.espsupplierid=c.id
//                                            where a.status in (3,4,5)) as b on a.supplierId = b.id and b.supplier_status=" + State.supplierstatus_used.ToString());
            strSql.Append(" inner join t_type as c on a.productType = c.typeid and c.status<>" + State.typestatus_block.ToString());
            strSql.Append(" where 1=1 {0}");
            if (orderBy.Trim() == "")
                strSql.Append(" order by a.id desc");
            else
                strSql.Append(" order by" + orderBy);
            string sql = string.Format(strSql.ToString(), terms);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql,parms))
            {
                while (r.Read())
                {
                    ProductInfo c = new ProductInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static List<ProductInfo> getModelListForMedia(string terms, List<SqlParameter> parms, string orderBy)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.*,b.supplier_name as supplierName,c.typename as typename from t_product as a ");
            strSql.Append(" inner join t_supplier as b on a.supplierId = b.id and b.supplier_status=" + State.supplierstatus_used.ToString());
            strSql.Append(" inner join t_type as c on a.productType = c.typeid and c.status<>" + State.typestatus_block.ToString());
            strSql.Append(" where 1=1 {0}");
            if (orderBy.Trim() == "")
                strSql.Append(" order by a.id desc");
            else
                strSql.Append(" order by" + orderBy);
            string sql = string.Format(strSql.ToString(), terms);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    ProductInfo c = new ProductInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }
        /// <summary>
        /// Gets the supplier product types.
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <returns></returns>
        public static string getSupplierProductTypes(int supplierId)
        {
            string typeids = "";
            DataSet ds = GetList(" and supplierid="+supplierId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    typeids += dr["productType"].ToString() + ",";
                }
                typeids = typeids.Remove(typeids.Length - 1);
            }
            return typeids;
        }

        /// <summary>
        /// 根据供应商ID获得供应商目录物品类型ID的列表
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <returns></returns>
        public static List<int> getSupplierProductTypeIdList(int supplierId)
        {
            List<int> list = new List<int>();
            DataSet ds = GetList(" and supplierid=" + supplierId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!list.Contains(int.Parse(dr["productType"].ToString())))
                    {
                        list.Add(int.Parse(dr["productType"].ToString()));
                    }
                }
            }
            return list;
        }
        #endregion  成员方法
    }
}