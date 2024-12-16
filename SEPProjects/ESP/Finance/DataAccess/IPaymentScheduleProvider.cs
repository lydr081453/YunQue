using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPaymentScheduleProvider
    {
        int Add(ESP.Finance.Entity.PaymentScheduleInfo model);
        int Add(ESP.Finance.Entity.PaymentScheduleInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.PaymentScheduleInfo model);
        int Delete(int paymentId);
        ESP.Finance.Entity.PaymentScheduleInfo GetModel(int paymentId);

        IList<ESP.Finance.Entity.PaymentScheduleInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.PaymentScheduleInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);
    }
}
