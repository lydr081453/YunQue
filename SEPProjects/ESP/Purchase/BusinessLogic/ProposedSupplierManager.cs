using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ProposedSupplierManager 的摘要说明。
    /// </summary>
    public static class ProposedSupplierManager
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static bool Exists(int id)
        {
            return ProposedSupplierDataHelper.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(ProposedSupplierInfo model)
        {
            return ProposedSupplierDataHelper.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(ProposedSupplierInfo model)
        {
            return ProposedSupplierDataHelper.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            ProposedSupplierDataHelper.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ProposedSupplierInfo GetModel(int id)
        {
            return ProposedSupplierDataHelper.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="supplierName">供应商名称</param>
        /// <returns></returns>
        public static ProposedSupplierInfo GetModel(string supplierName)
        {
            return ProposedSupplierDataHelper.GetModel(supplierName);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere, List<SqlParameter> parms)
        {
            return ProposedSupplierDataHelper.GetList(strWhere, parms);
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getModelList(string terms, List<SqlParameter> parms)
        {
            return ProposedSupplierDataHelper.getModelList(terms, parms);
        }

        /// <summary>
        /// Gets the top model list.
        /// </summary>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getTopModelList()
        {
            return ProposedSupplierDataHelper.getTopModelList();
        }

        /// <summary>
        /// 根据物料类型ID获得供应商
        /// </summary>
        /// <param name="productTypeId">The product type id.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getSupplierListByProductTypeId(int productTypeId, string terms, List<SqlParameter> parms)
        {
            return ProposedSupplierDataHelper.getSupplierListByProductTypeId(productTypeId, terms, parms);
        }

        /// <summary>
        /// Gets the supplier list by product type ids.
        /// </summary>
        /// <param name="productTypes">The product types.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getSupplierListByProductTypeIds(string productTypes, string terms, List<SqlParameter> parms)
        {
            return ProposedSupplierDataHelper.getSupplierListByProductTypeIds(productTypes, terms, parms);
        }

        /// <summary>
        /// Gets the supplier list by type names.
        /// </summary>
        /// <param name="productTypes">The product types.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getSupplierListByTypeNames(string productTypes, string terms, List<SqlParameter> parms)
        {
            return ProposedSupplierDataHelper.getSupplierListByTypeNames(productTypes, terms, parms);
        }
        #endregion  成员方法
    }
}