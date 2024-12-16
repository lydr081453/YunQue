using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
    public static class SupplierFinanceRelationManager
    {

        private static ESP.Finance.IDataAccess.ISupplierFinanceRelationProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ISupplierFinanceRelationProvider>.Instance; } }

        public static int Add(SupplierFinanceRelationInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Update(SupplierFinanceRelationInfo model)
        {
            return DataProvider.Update(model);
        }

        public static int Delete(int supplierid)
        {
            return DataProvider.Delete(supplierid);
        }

        public static SupplierFinanceRelationInfo GetModel(int supplierid)
        {
            return DataProvider.GetModel(supplierid);
        }

        public static IList<SupplierFinanceRelationInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
    }
}
