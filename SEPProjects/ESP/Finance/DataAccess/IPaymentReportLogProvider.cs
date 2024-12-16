using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPaymentReportLogProvider
    {
        bool Exists(int userid, DateTime dt1, DateTime dt2);
        int Add(ESP.Finance.Entity.PaymentReportLogInfo model);
    }
}

