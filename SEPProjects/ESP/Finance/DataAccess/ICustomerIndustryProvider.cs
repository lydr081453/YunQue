using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICustomerIndustryDataProvider
    {
        int Add(ESP.Finance.Entity.CustomerIndustryInfo model);
        int Update(ESP.Finance.Entity.CustomerIndustryInfo model);
        int Delete(int custIndustryId);
        ESP.Finance.Entity.CustomerIndustryInfo GetModel(int custIndustryId);

        //IList<ESP.Finance.Entity.CustomerIndustryInfo> GetAllList();
        //IList<ESP.Finance.Entity.CustomerIndustryInfo> GetList(string term);
        IList<ESP.Finance.Entity.CustomerIndustryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
