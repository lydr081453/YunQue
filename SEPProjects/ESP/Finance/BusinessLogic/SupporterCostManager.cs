using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类SupporterCostBLL 的摘要说明。
	/// </summary>
     
     
    public static class SupporterCostManager
	{
        //private readonly ESP.Finance.DataAccess.SupporterCostDAL dal = new ESP.Finance.DataAccess.SupporterCostDAL();

        private static ESP.Finance.IDataAccess.ISupporterCostDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ISupporterCostDataProvider>.Instance;}}
        //private const string _dalProviderName = "SupporterCostDALProvider";

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
		 
         
        public static int  Add(ESP.Finance.Entity.SupporterCostInfo model)
		{
            if(model.SupportId == 0) return 0;
            //SupporterInfo supporter = SupporterManager.GetModel(model.SupportId);
            //decimal totalcost = DataProvider.GetTotalCostBySupporter(model.SupportId,true);
            //if (supporter.BudgetAllocation - totalcost < model.Amounts) return 0;

            if (!CheckerManager.CheckSupporterOddAmount(model.SupportId, model.Amounts))
            {
                return 0;
            }

            //trans//return DataProvider.Add(model,true);
            return DataProvider.Add(model);
  
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
         
         
		public static UpdateResult Update(ESP.Finance.Entity.SupporterCostInfo model)
		{
            if (model.SupportId == 0) return UpdateResult.AmountOverflow;
            SupporterCostInfo old = SupporterCostManager.GetModel(model.SupportCostId);
            //decimal totalcost = GetTotalCostBySupporter(model.SupportId);
            //if (supporter.BudgetAllocation - totalcost < model.Amounts) return UpdateResult.AmountOverflow;

            if (!CheckerManager.CheckSupporterOddAmount(model.SupportId, model.Amounts - old.Amounts))//检查时用新金额减去原金额再进行比较
            {
                return UpdateResult.AmountOverflow;
            }

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
		public static DeleteResult Delete(int SupportCostId)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(SupportCostId);
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
        /// 获取支持方所有成本
        /// </summary>
        /// <param name="SupporterId"></param>
        /// <returns></returns>
         
         
        public static decimal GetTotalCostBySupporter(int SupporterId)
        {
            //trans//return DataProvider.GetTotalCostBySupporter(SupporterId, true);
            return DataProvider.GetTotalCostBySupporter(SupporterId);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static ESP.Finance.Entity.SupporterCostInfo GetModel(int SupportCostId)
		{
			
			return DataProvider.GetModel(SupportCostId);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupporterCostInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupporterCostInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupporterCostInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<SupporterCostInfo> GetList(int supporterId, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(supporterId, term, param);
        }

        public static IList<SupporterCostInfo> GetList(int supporterId, System.Data.SqlClient.SqlTransaction trans)
        {
            return DataProvider.GetList(supporterId, trans);
        }

        #endregion 获得数据列表

		#endregion  成员方法
    }
}

