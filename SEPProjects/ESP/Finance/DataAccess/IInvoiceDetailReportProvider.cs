using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IInvoiceDetailReportDataProvider
    {
        IList<ESP.Finance.Entity.InvoiceDetailReporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
