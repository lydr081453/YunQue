using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class InvoiceDetailManager
    {
        //private readonly ESP.Finance.DataAccess.InvoiceDAL dal=new ESP.Finance.DataAccess.InvoiceDAL();

        private static ESP.Finance.IDataAccess.IInvoiceDetailDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IInvoiceDetailDataProvider>.Instance;}}
        //private const string _dalProviderName = "InvoiceDetailDALProvider";

        
		#region  成员方法



		/// <summary>
		/// 增加一条数据
		/// </summary>
         
         
         
		public static int  Add(ESP.Finance.Entity.InvoiceDetailInfo model)
		{
            //if (!string.IsNullOrEmpty(model.InvoiceCode))
            //{
            //    string term = " InvoiceCode=@InvoiceCode ";
            //    List<System.Data.SqlClient.SqlParameter> existsParam = new List<System.Data.SqlClient.SqlParameter>();
            //    System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@InvoiceCode", SqlDbType.NVarChar, 50);
            //    param.Value = model.InvoiceCode;
            //    existsParam.Add(param);

            //    if (DataProvider.Exists(term, existsParam, true))
            //    {
            //        return 0;
            //    }
            //}
            //DataProvider.UpdateStatusByPaymentID(model.PaymentID == null ? 0 : model.PaymentID.Value,(int)Utility.InvoiceStatus.Destroy,true);

            //检查是否超出发票余额
            if (!CheckerManager.CheckInvoiceOddAmount(model.InvoiceID == null ? 0 : model.InvoiceID.Value, model.Amounts == null ? 0 : model.Amounts.Value))
            {
                return -1;
            }

            //检查是否超出付款通知余额
            if (!CheckerManager.CheckPaymentOddAmount(model.PaymentID == null ? 0 : model.PaymentID.Value, model.Amounts == null ? 0 : model.Amounts.Value))
            {
                return -2;
            }
			//trans//return DataProvider.Add(model,true);
            return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static UpdateResult Update(ESP.Finance.Entity.InvoiceDetailInfo model)
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
        public static ESP.Finance.Entity.InvoiceDetailInfo GetModel(int InvoiceID)
		{
			
			return DataProvider.GetModel(InvoiceID);
		}



        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.InvoiceDetailInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.InvoiceDetailInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.InvoiceDetailInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.InvoiceDetailInfo> GetListByPayment(int paymentid,string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            if (string.IsNullOrEmpty(term))
            {
                term = " 1=1 ";
            }
            term += " and PaymentID=@PaymentID ";
            System.Data.SqlClient.SqlParameter sp = new System.Data.SqlClient.SqlParameter("@PaymentID", System.Data.SqlDbType.Int, 4);
            sp.Value = paymentid;
            if (param == null)
            {
                param = new List<System.Data.SqlClient.SqlParameter>();
            }
            param.Add(sp);
            return DataProvider.GetList(term, param);
        }

        #endregion 获得数据列表

		#endregion  成员方法

        #region IInvoiceDetailProvider 成员

         
         
        public static decimal GetTotalAmountByInvoice(int invoiceId)
        {
            //trans//return DataProvider.GetTotalAmountByInvoice(invoiceId,true);
            return DataProvider.GetTotalAmountByInvoice(invoiceId);
        }

         
         
        public static decimal GetTotalAmountByPayment(int PaymentID)
        {
            //trans//return DataProvider.GetTotalAmountByPayment(PaymentID,true);
            return DataProvider.GetTotalAmountByPayment(PaymentID);
        }

        #endregion
    }
}
