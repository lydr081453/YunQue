using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类TaxRateBLL 的摘要说明。
	/// </summary>
     
     
    public static class TaxRateManager
	{
		//private readonly ESP.Finance.DataAccess.TaxRateDAL dal=new ESP.Finance.DataAccess.TaxRateDAL();

        private static ESP.Finance.IDataAccess.ITaxRateDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ITaxRateDataProvider>.Instance;}}
        //private const string _dalProviderName = "TaxRateDALProvider";

        
		#region  成员方法



		/// <summary>
		/// 增加一条数据
		/// </summary>
         
         
		public static int Add(ESP.Finance.Entity.TaxRateInfo model)
		{
            //string term = " CustomerID=@CustomerID and BizTypeID=@BizTypeID and BranchID=@BranchID ";
            List<SqlParameter> existsParam = new List<SqlParameter>();
            SqlParameter param = new SqlParameter("@CustomerID", SqlDbType.Int, 4);
            param.Value = model.CustomerID;
            existsParam.Add(param);


            param = new SqlParameter("@BizTypeID", SqlDbType.Int, 4);
            param.Value = model.BizTypeID;
            existsParam.Add(param);

            param = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            param.Value = model.BranchID;
            existsParam.Add(param);


            //if (DataProvider.Exists(term, existsParam, true))//如果存在此客户则更新
            //{
            //    int res = DataProvider.UpdateByCustomer(model, true);
            //    if (res > 0)
            //    {
            //        return model.TaxRateID;
            //    }
            //    else return 0;
            //}
            if (model.BizTypeID == null)
            {
                model.BizTypeID = 0;
            }
            if (model.BranchID == null)
            {
                model.BranchID = 0;
            }
            //trans//return DataProvider.Add(model,true);
            return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static UpdateResult Update(ESP.Finance.Entity.TaxRateInfo model)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static DeleteResult Delete(int TaxRateID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(TaxRateID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
		}

        /// <summary>
        /// 根据客户得到一个对象实体
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static ESP.Finance.Entity.TaxRateInfo GetModel(int bizTypeId, int branchId)
        {
            return DataProvider.GetModel(bizTypeId,bizTypeId);
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static ESP.Finance.Entity.TaxRateInfo GetModel(int TaxRateID)
		{

            return DataProvider.GetModel(TaxRateID);
		}

        /// <summary>
        /// 获得分公司对应的合同税率（新方法NewFinance还没有）
        /// </summary>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<List<string>> getTaxRateByBranch(int bizTypeId, int branchId)
        {
            
            List<List<string>> list = new List<List<string>>();
            List<string> c = new List<string>();
           
            IList<ESP.Finance.Entity.TaxRateInfo> taxlist = GetList(branchId, null, null);
            if (taxlist.Count > 0)
            {
                for (int i = 0; i < taxlist.Count; i++)
                {
                    List<string> s = new List<string>();
                    s.Add(taxlist[i].Remark.ToString());
                    s.Add(taxlist[i].TaxRateID.ToString()+","+taxlist[i].TaxRate.ToString());
                    list.Add(s);
                }
            }
            else
            {
                c.Add("0");
                c.Add("无数据");
                list.Add(c); 
            }
            return list;
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<TaxRateInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<TaxRateInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<TaxRateInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }


        public static IList<ESP.Finance.Entity.TaxRateInfo> GetList(int branchId, string term, List<System.Data.SqlClient.SqlParameter> param)
        {

            return DataProvider.GetList( branchId, term, param);            
        }

        #endregion 获得数据列表

		#endregion  成员方法
	}
}

