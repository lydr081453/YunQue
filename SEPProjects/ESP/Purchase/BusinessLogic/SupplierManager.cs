using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类SupplierManager 的摘要说明。
    /// </summary>
    public static class SupplierManager
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(SupplierInfo model)
        {
            return SupplierDataProvider.Add(model);
        }

        /// <summary>
        /// 保存非协议供应商和采购物品
        /// </summary>
        /// <param name="model"></param>
        /// <param name="productList"></param>
        public static void AddSupplierAndProduct(SupplierInfo model, List<ProductInfo> productList)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int supplierId = SupplierDataProvider.Add(model, conn, trans);
                    foreach (ProductInfo productModel in productList)
                    {
                        productModel.supplierId = supplierId;
                        productModel.IsShow = 0;
                        ProductDataProvider.Add(productModel);
                    }
                    trans.Commit();
                }
                catch { trans.Rollback(); }
            }
        }

        /// <summary>
        /// 更新非协议供应商和采购物品
        /// </summary>
        /// <param name="model"></param>
        /// <param name="productList"></param>
        public static void UpdateSupplierAndProduct(SupplierInfo model, List<ProductInfo> productList)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SupplierDataProvider.Update(model, conn, trans);
                    foreach (ProductInfo productModel in productList)
                    {
                        productModel.supplierId = model.id;
                        productModel.IsShow = 0;
                        ProductDataProvider.Add(productModel);
                    }
                    trans.Commit();
                }
                catch { trans.Rollback(); }
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(SupplierInfo model)
        {
            return SupplierDataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            SupplierDataProvider.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static SupplierInfo GetModel(int id)
        {

            return SupplierDataProvider.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="supplierName">供应商名称</param>
        /// <param name="type">供应商类型</param>
        /// <returns></returns>
        public static SupplierInfo GetModel(string supplierName,int type)
        {
            return SupplierDataProvider.GetModel(supplierName,type);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere, List<SqlParameter> parms)
        {
            return SupplierDataProvider.GetList(strWhere, parms);
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getModelList(string terms, List<SqlParameter> parms)
        {
            return SupplierDataProvider.getModelList(terms, parms);
        }

        /// <summary>
        /// 获取供应链平台供应商（添加采购物品新改动）
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable getLinkSupplySupplierList(string terms,int typeid, List<SqlParameter> parms,string typeName)
        {
            return SupplierDataProvider.getLinkSupplySupplierList(terms, typeid, parms,typeName);
        }

        public static int insertSupplierAndLinkSupply(GeneralInfo g,SupplierInfo model, int supplyId)
        {
            return SupplierDataProvider.insertSupplierAndLinkSupply(g,model, supplyId);
        }

        public static List<SupplierInfo> getAllModelList(string terms, List<SqlParameter> parms)
        {
            return SupplierDataProvider.getModelList(terms, parms,true);
        }

        /// <summary>
        /// Gets the top model list.
        /// </summary>
        /// <param name="strTerms">The STR terms.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getTopModelList(string strTerms)
        {
            return SupplierDataProvider.getTopModelList(strTerms);
        }

        /// <summary>
        /// 根据物料类型ID获得供应商
        /// </summary>
        /// <param name="productTypeId">The product type id.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getSupplierListByProductTypeId(int productTypeId, string terms, List<SqlParameter> parms)
        {
            return SupplierDataProvider.getSupplierListByProductTypeId(productTypeId, terms, parms);
        }

        /// <summary>
        /// Gets the supplier list by product type ids.
        /// </summary>
        /// <param name="productTypes">The product types.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getSupplierListByProductTypeIds(string productTypes, string terms, List<SqlParameter> parms)
        {
            return SupplierDataProvider.getSupplierListByProductTypeIds(productTypes, terms, parms);          
        }

        /// <summary>
        /// Gets the supplier list by type names.
        /// </summary>
        /// <param name="productTypes">The product types.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getSupplierListByTypeNames(string productTypes, string terms, List<SqlParameter> parms)
        {
            return SupplierDataProvider.getSupplierListByTypeNames(productTypes, terms, parms);
        }

        public static List<SupplierInfo> getAllSupplierListByTypeNames(string productTypes, string terms, List<SqlParameter> parms)
        {
            return SupplierDataProvider.getSupplierListByTypeNames(productTypes, terms, parms,true);
        }

        public static List<SupplierInfo> getSupplierListByAuditorId(int auditorId, string terms, List<SqlParameter> parms, bool isAll)
        {
            return SupplierDataProvider.getSupplierListByAuditorId(auditorId, terms, parms, true);
        }
        /// <summary>
        /// 变更为协议供应商
        /// </summary>
        /// <param name="supplierId"></param>
        public static void ChangedTypeToAgreement(int supplierId)
        {
            SupplierDataProvider.ChangedSupplierType(supplierId, (int)Common.State.supplier_type.agreement, 1);
        }

        /// <summary>
        /// 变更为推荐供应商
        /// </summary>
        /// <param name="supplierId"></param>
        public static void ChangedTypeToRecommend(int supplierId)
        {
            SupplierDataProvider.ChangedSupplierType(supplierId, (int)Common.State.supplier_type.recommend, 0);
        }

        /// <summary>
        /// 获取供应商同步列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetSyncSupplierList(string where, List<SqlParameter> parms)
        {
            return SupplierDataProvider.GetSyncSupplierList(where, parms);
        }

        /// <summary>
        /// 同步供应商
        /// </summary>
        /// <param name="Emodel">采购部供应商</param>
        /// <param name="Smodel">供应链供应商</param>
        /// <param name="relationId">关联表Id</param>
        /// <param name="userId">操作人ID</param>
        /// <returns></returns>
        public static DataTable SyncSupplier(ESP.Purchase.Entity.SupplierInfo Emodel, ESP.Supplier.Entity.SC_Supplier Smodel, int relationId, int userId)
        {
            return SupplierDataProvider.SyncSupplier(Emodel, Smodel, relationId, userId);
        }
        #endregion  成员方法

        public static DataTable getSupplierListOrderByFeiXieYi(string strWhere, List<SqlParameter> parms,string typeid)
        {
            return SupplierDataProvider.getSupplierListOrderByFeiXieYi(strWhere, parms, typeid);
        }

        public static DataTable getSupplierListOrderByHist(string strWhere, List<SqlParameter> parms, string typeid)
        {
            return SupplierDataProvider.getSupplierListOrderByHist(strWhere, parms, typeid);
        }

        public static DataTable getSupplierListOrderByRecommend(string strWhere, List<SqlParameter> parms, string productName)
        {
            return SupplierDataProvider.getSupplierListOrderByRecommend(strWhere, parms, productName);
        }

        public static DataTable getSupplierListOrderByXieYi(string strWhere, List<SqlParameter> parms, string typeid)
        {
            return SupplierDataProvider.getSupplierListOrderByXieYi(strWhere, parms, typeid);
        }

                /// <summary>
        /// 是否为协议供应商
        /// </summary>
        /// <param name="supplierId">采购供应商ID</param>
        /// <param name="typeId">采购第3级物料ID</param>
        /// <returns></returns>
        public static bool isAgreementSupplier(int supplierId, int typeId)
        {
            return SupplierDataProvider.isAgreementSupplier(supplierId, typeId);
        }
    }
}