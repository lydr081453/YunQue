using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.IDataAccess
{
   [ESP.Configuration.Provider]
    public interface IBankDataProvider
    {
        int Add(ESP.Finance.Entity.BankInfo model);
        int Update(ESP.Finance.Entity.BankInfo model);
        int Delete(int bankId);

        ESP.Finance.Entity.BankInfo GetModel(int bankId);
        IList<BankInfo> GetList(string term, List<SqlParameter> param);
    }
}
