using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类ExpenseBLL 的摘要说明。
	/// </summary>
     
     
    public static class ExpenseManager
	{
		//private readonly ESP.Finance.DataAccess.ExpenseDAL dal=new ESP.Finance.DataAccess.ExpenseDAL();

        private static ESP.Finance.IDataAccess.IExpenseDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IExpenseDataProvider>.Instance;}}
        //private const string _dalProviderName = "ExpenseDALProvider";

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(ESP.Finance.Entity.ExpenseInfo model)
		{
			return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static UpdateResult Update(ESP.Finance.Entity.ExpenseInfo model)
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
		public static ESP.Finance.Entity.ExpenseInfo GetModel(int ExpenseID)
		{
			
			return DataProvider.GetModel(ExpenseID);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ExpenseInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ExpenseInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ExpenseInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

