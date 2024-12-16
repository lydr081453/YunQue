using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class CustomerAudtiorManager
    {
        private static ESP.Finance.IDataAccess.ICustomerAuditorProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICustomerAuditorProvider>.Instance; } }

        public static int Add(ESP.Finance.Entity.CustomerAuditorInfo model)
        {
            return DataProvider.Add(model);
        }
        public static int Update(ESP.Finance.Entity.CustomerAuditorInfo model)
        {
            return DataProvider.Update(model);
        }
        public static int Delete(int id)
        {
            return DataProvider.Delete(id);
        }
        public static ESP.Finance.Entity.CustomerAuditorInfo GetModel(int id)
        {
            return DataProvider.GetModel(id);
        }
        public static IList<ESP.Finance.Entity.CustomerAuditorInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }
    }
}
