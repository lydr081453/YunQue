using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
     
     
    public static class SupporterExpenseManager
    {

        private static ESP.Finance.IDataAccess.ISupporterExpenseDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ISupporterExpenseDataProvider>.Instance;}}
        //private const string _dalProviderName = "SupporterExpenseDALProvider";

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
         
         
		public static int Add(ESP.Finance.Entity.SupporterExpenseInfo model)
		{
			return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
         
         
        public static UpdateResult Update(ESP.Finance.Entity.SupporterExpenseInfo model)
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
		public static DeleteResult Delete(int ExpenseID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(ExpenseID);
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
		/// 得到一个对象实体
		/// </summary>
        public static ESP.Finance.Entity.SupporterExpenseInfo GetModel(int ExpenseID)
		{
			return DataProvider.GetModel(ExpenseID);
		}


        /// <summary>
        /// 得到支持方全部费用
        /// </summary>
        /// <param name="supporterId"></param>
        /// <returns></returns>
         
         
        public static decimal GetTotalExpense(int supporterId)
        {
            return DataProvider.GetTotalExpense(supporterId);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.SupporterExpenseInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.SupporterExpenseInfo> GetList(string term)
        {
            return GetList(term, null);
        }

        public static IList<ESP.Finance.Entity.SupporterExpenseInfo> GetList(int supportId,SqlTransaction trans)
        {
            return DataProvider.GetList(supportId, trans);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.SupporterExpenseInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

		#endregion  成员方法
    }
}
