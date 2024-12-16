using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类CustomerIndustryBLL 的摘要说明。
	/// </summary>
    /// 

     
     
	public static class CustomerIndustryManager
	{
		//private readonly ESP.Finance.DataAccess.CustomerIndustryDAL dal=new ESP.Finance.DataAccess.CustomerIndustryDAL();

        private static ESP.Finance.IDataAccess.ICustomerIndustryDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICustomerIndustryDataProvider>.Instance;}}
        //private string _dalProviderName = "CustomerIndustryDALProvider";

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(ESP.Finance.Entity.CustomerIndustryInfo model)
		{
            return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static UpdateResult Update(ESP.Finance.Entity.CustomerIndustryInfo model)
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
		public static DeleteResult Delete(int IndustryID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(IndustryID);
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
		public static ESP.Finance.Entity.CustomerIndustryInfo GetModel(int IndustryID)
		{

            return DataProvider.GetModel(IndustryID);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerIndustryInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerIndustryInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerIndustryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

