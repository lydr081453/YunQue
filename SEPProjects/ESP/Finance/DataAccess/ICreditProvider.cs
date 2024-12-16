using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICreditDataProvider
    {
        int Add(ESP.Finance.Entity.CreditInfo model);
        int Update(ESP.Finance.Entity.CreditInfo model);
        int Delete(int creditId);
        ESP.Finance.Entity.CreditInfo GetModel(int creditId);

        //IList<ESP.Finance.Entity.CreditInfo> GetAllList();
        //IList<ESP.Finance.Entity.CreditInfo> GetList(string term);
        IList<ESP.Finance.Entity.CreditInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
