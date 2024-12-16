using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPaymentNotifyReportDataProvider
    {
        IList<ESP.Finance.Entity.PaymentNotifyReporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
