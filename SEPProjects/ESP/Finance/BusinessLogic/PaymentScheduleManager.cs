using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
   public static class PaymentScheduleManager
    {
       private static ESP.Finance.IDataAccess.IPaymentScheduleProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPaymentScheduleProvider>.Instance; } }

       public static int Add(ESP.Finance.Entity.PaymentScheduleInfo model)
       {
           return DataProvider.Add(model);
       }
       public static int Add(ESP.Finance.Entity.PaymentScheduleInfo model, System.Data.SqlClient.SqlTransaction trans)
       {
           return DataProvider.Add(model, trans);
       }
       public static int Update(ESP.Finance.Entity.PaymentScheduleInfo model)
       {
           return DataProvider.Update(model);
       }
       public static int Delete(int Id)
       {
           return DataProvider.Delete(Id);
       }
       public static ESP.Finance.Entity.PaymentScheduleInfo GetModel(int Id)
       {
           return DataProvider.GetModel(Id);
       }

       public static IList<ESP.Finance.Entity.PaymentScheduleInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
       {
           return DataProvider.GetList(term, param);
       }
       public static IList<ESP.Finance.Entity.PaymentScheduleInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans)
       {
           return DataProvider.GetList(term, param, trans);
       }
    }
}
