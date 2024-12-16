using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类LogBLL 的摘要说明。
	/// </summary>


    public static class PaymentExtensionManager
	{
		//private readonly ESP.Finance.DataAccess.LogDAL dal=new ESP.Finance.DataAccess.LogDAL();

        private static ESP.Finance.IDataAccess.IPaymentExtensionDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPaymentExtensionDataProvider>.Instance; } }
        //private const string _dalProviderName = "LogDALProvider";

        
		#region  成员方法




        /// <summary>
        /// 增加一条数据
        /// </summary>


        public static int Add(ESP.Finance.Entity.PaymentExtensionInfo model)
        {
            try
            {
                //trans//return DataProvider.Add(model,true);
                return DataProvider.Add(model);
            }
            catch (System.Exception)
            {
                return 0;
            }
            
        }


		/// <summary>
		/// 更新一条数据
		/// </summary>


        public static UpdateResult Update(ESP.Finance.Entity.PaymentExtensionInfo model)
		{
            int res = 0;
            try
            {
                //trans//res = DataProvider.Update(model,true);
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
		public static DeleteResult Delete(int id)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(id);
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
        public static ESP.Finance.Entity.PaymentExtensionInfo GetModel(int id)
		{
			
			return DataProvider.GetModel(id);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<PaymentExtensionInfo> GetAllList()
        {
            return DataProvider.GetList(null,new List<SqlParameter>());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<PaymentExtensionInfo> GetList(string term)
        {
            return DataProvider.GetList(term,new List<SqlParameter>());
        }

 
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

