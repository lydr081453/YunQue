using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class ContractAuditLogManager
    {
        private static ESP.Finance.IDataAccess.IContractAuditLogProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IContractAuditLogProvider>.Instance; } }
        public static int Add(ESP.Finance.Entity.ContractAuditLogInfo model)
        {
            return DataProvider.Add(model);
        }
        public static IList<ESP.Finance.Entity.ContractAuditLogInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }
    }
}
