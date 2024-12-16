using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICustomerAuditorProvider
    {
        int Add(ESP.Finance.Entity.CustomerAuditorInfo model);
        int Update(ESP.Finance.Entity.CustomerAuditorInfo model);
        int Delete(int branchId);
        ESP.Finance.Entity.CustomerAuditorInfo GetModel(int branchId);
        IList<ESP.Finance.Entity.CustomerAuditorInfo> GetList(string term);
    }
}
