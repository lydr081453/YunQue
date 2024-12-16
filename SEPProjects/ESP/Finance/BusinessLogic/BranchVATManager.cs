using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    public static class BranchVATManager
    {
        private static ESP.Finance.IDataAccess.IBranchVATProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IBranchVATProvider>.Instance; } }
       
        public static IList<BranchVATInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }
    }
}
