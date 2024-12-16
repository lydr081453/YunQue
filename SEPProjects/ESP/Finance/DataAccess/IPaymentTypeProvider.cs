using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPaymentTypeDataProvider
    {
        ESP.Finance.Entity.PaymentTypeInfo GetModel(int PaymentTypeID);
        IList<PaymentTypeInfo> GetList(string term, List<SqlParameter> param);
    }
}
