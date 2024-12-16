using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类InvoiceBLL 的摘要说明。
	/// </summary>
     
     
    public static class InvoiceManager
	{
		//private readonly ESP.Finance.DataAccess.InvoiceDAL dal=new ESP.Finance.DataAccess.InvoiceDAL();

        private static ESP.Finance.IDataAccess.IInvoiceDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IInvoiceDataProvider>.Instance;}}
        //private const string _dalProviderName = "InvoiceDALProvider";

        
		#region  成员方法



		/// <summary>
		/// 增加一条数据
		/// </summary>
         
         
         
		public static int  Add(ESP.Finance.Entity.InvoiceInfo model)
		{
            if (!string.IsNullOrEmpty(model.InvoiceCode))
            {
                string term = " InvoiceCode=@InvoiceCode ";
                List<System.Data.SqlClient.SqlParameter> existsParam = new List<System.Data.SqlClient.SqlParameter>();
                System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@InvoiceCode", SqlDbType.NVarChar, 50);
                param.Value = model.InvoiceCode;
                existsParam.Add(param);

                //trans//if (DataProvider.Exists(term, existsParam, true))
                if (DataProvider.Exists(term, existsParam))
                {
                    return 0;
                }
            }
            //DataProvider.UpdateStatusByPaymentID(model.PaymentID == null ? 0 : model.PaymentID.Value,(int)Utility.InvoiceStatus.Destroy,true);
			//trans//return DataProvider.Add(model,true);
            return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static UpdateResult Update(ESP.Finance.Entity.InvoiceInfo model)
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
		public static DeleteResult Delete(int InvoiceID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(InvoiceID);
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
		public static ESP.Finance.Entity.InvoiceInfo GetModel(int InvoiceID)
		{
			
			return DataProvider.GetModel(InvoiceID);
		}



        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<InvoiceInfo> GetAllList()
        {
            return GetList(null);
        }


        public static bool InsertInvoiceAndDetail(InvoiceInfo model, List<InvoiceDetailInfo> dlist)
        {
            return DataProvider.InsertInvoiceAndDetail(model, dlist);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<InvoiceInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<InvoiceInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

