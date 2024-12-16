using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace ESP.Finance.BusinessLogic
{
    public static class TicketSupplierManager
    {
        private static ESP.Finance.IDataAccess.ITicketSupplierProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ITicketSupplierProvider>.Instance; } }
     
        public static TicketSupplier GetModel(int supplierId)
        {
            return DataProvider.GetModel(supplierId);
        }

        public static IList<TicketSupplier> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
    }
}
