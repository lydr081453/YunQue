using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class PaymentTypeManager
    {
        private static ESP.Finance.IDataAccess.IPaymentTypeDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPaymentTypeDataProvider>.Instance;}}
        private const string tablename = "PaymentTypeInfo";

        

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.PaymentTypeInfo GetModel(int PaymentTypeID)
        {
            Entity.PaymentTypeInfo pay = DataProvider.GetModel(PaymentTypeID);
            return pay;
        }


        public static IList<PaymentTypeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
    }
}
