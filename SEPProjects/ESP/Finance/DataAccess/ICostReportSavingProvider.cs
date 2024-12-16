using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICostReportSavingProvider
    {
        int Add(ESP.Finance.Entity.CostReportSavingInfo model);
    }
}
