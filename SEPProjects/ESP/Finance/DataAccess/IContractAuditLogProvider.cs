using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IContractAuditLogProvider
    {
        int Add(ESP.Finance.Entity.ContractAuditLogInfo model);
        IList<ESP.Finance.Entity.ContractAuditLogInfo> GetList(string term);
    }
}
