using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ProductManager 的摘要说明。
    /// </summary>
    public static class ProductManager
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(ProductInfo model)
        {
            return ProductDataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(ProductInfo model)
        {
            return ProductDataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            ProductDataProvider.Delete(id);
        }

        /// <summary>
        /// 删除一批数据
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public static int Delete(string ids)
        {
            return ProductDataProvider.Delete(ids);
        }

        /// <summary>
        /// 屏蔽目录物品
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public static int DisabledData(string ids)
        {
            return ProductDataProvider.DisabledData(ids);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ProductInfo GetModel(int id)
        {
            return ProductDataProvider.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return ProductDataProvider.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return ProductDataProvider.GetList("");
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<ProductInfo> getModelList(string terms, List<SqlParameter> parms)
        {
            return ProductDataProvider.getModelList(terms, parms,"");
        }

        public static List<ProductInfo> getModelList(string terms, List<SqlParameter> parms, string orderBy)
        {
            return ProductDataProvider.getModelList(terms, parms, orderBy);
        }

        public static List<ProductInfo> getModelListForMedia(string terms, List<SqlParameter> parms, string orderBy)
        {
            return ProductDataProvider.getModelListForMedia(terms, parms, orderBy);
        }

        //根据typeid和name获得目录物品的list
        /// <summary>
        /// Gets the list by type id.
        /// </summary>
        /// <param name="tid">The tid.</param>
        /// <param name="strterm">The strterm.</param>
        /// <returns></returns>
        public static List<ProductInfo> getListByTypeId(int tid, string strterm)
        {
            string terms = string.Empty;
            List<SqlParameter> parms = new List<SqlParameter>();

            if(tid >0)
            {
                terms += "and a.productType=" + tid.ToString();
            }
            //and a.IsShow=1 
            if(strterm != "")
            {
                terms += " and ( a.productName like '%'+@productName+'%'";
                parms.Add(new SqlParameter("@productName", strterm));
                terms += " or a.productDes like '%'+@productDes+'%')";
                parms.Add(new SqlParameter("@productDes", strterm));
            }

            return ProductDataProvider.getModelList(terms, parms);
        }

        //根据supplierId和name获得目录物品的list
        /// <summary>
        /// Gets the list by supplier id.
        /// </summary>
        /// <param name="sid">The sid.</param>
        /// <param name="strterm">The strterm.</param>
        /// <returns></returns>
        public static List<ProductInfo> getListBySupplierId(int sid, string strterm)
        {
            string terms = string.Empty;
            List<SqlParameter> parms = new List<SqlParameter>();

            if (sid > 0)
            {
                terms += "and a.supplierId=" + sid.ToString();
            }
            //and a.IsShow=1 
            if (strterm != "")
            {
                terms += "  and ( a.productName like '%'+@productName+'%'";
                parms.Add(new SqlParameter("@productName", strterm));
                terms += " or a.productDes like '%'+@productDes+'%')";
                parms.Add(new SqlParameter("@productDes", strterm));
            }

            return ProductDataProvider.getModelList(terms, parms);
        }

        /// <summary>
        /// 模糊查询产品列表
        /// </summary>
        /// <param name="likeValue">The like value.</param>
        /// <param name="isShow">if set to <c>true</c> [is show].</param>
        /// <returns></returns>
        public static List<ProductInfo> getListByLike(string likeValue,bool isShow)
        {
            string terms = "";
            if (isShow)
            {
                terms += " and a.IsShow=1 ";
            }
            terms += @" and (a.productname like '%'+@likeValue+'%' or a.productdes like '%'+@likeValue+'%' or b.supplier_name like '%'+@likeValue+'%'
                                or c.typename like '%'+@likeValue+'%')";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@likeValue", likeValue));
            return ProductDataProvider.getModelList(terms, parms);
        }

        /// <summary>
        /// 获得供应商的物料类别
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <returns></returns>
        public static string GetTypeNameBySupplierId(int supplierId)
        {
            return ProductDataProvider.GetTypeNameBySupplierId(supplierId);
        }

        /// <summary>
        /// Gets the supplier product types.
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <returns></returns>
        public static string getSupplierProductTypes(int supplierId)
        {
            return ProductDataProvider.getSupplierProductTypes(supplierId);
        }

        /// <summary>
        /// 根据供应商ID获得供应商目录物品类型ID的列表
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <returns></returns>
        public static List<int> getSupplierProductTypeIdList(int supplierId)
        {
            return ProductDataProvider.getSupplierProductTypeIdList(supplierId);
        }

        /// <summary>
        /// Gets the type list by supplier id.
        /// </summary>
        /// <param name="sid">The sid.</param>
        /// <param name="strterm">The strterm.</param>
        /// <returns></returns>
        public static List<List<string>> GetTypeListBySupplierId(int sid, string strterm)
        {
            string terms = string.Empty;
            List<SqlParameter> parms = new List<SqlParameter>();
            List<List<string>> list = new List<List<string>>();

            if (sid > 0)
            {
                terms += "and a.supplierId=" + sid.ToString();
            }
            if (strterm != "")
            {
                terms += " and ( a.productName like '%'+@productName+'%'";
                parms.Add(new SqlParameter("@productName", strterm));
                terms += " or a.productDes like '%'+@productDes+'%')";
                parms.Add(new SqlParameter("@productDes", strterm));
            }

            List<ProductInfo> modellist= ProductDataProvider.getModelList(terms, parms);
            foreach(ProductInfo p in modellist)
            {
                List<string> l = new List<string>();
                l.Add(p.productType.ToString());
                l.Add(p.typename);
                list.Add(l);
            }
            return list;
        }
        #endregion  成员方法
    }
}