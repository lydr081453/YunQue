using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IBankCancelProvider
    {
        int Add(ESP.Finance.Entity.BankCancelInfo model);
        int Add(ESP.Finance.Entity.BankCancelInfo model,SqlTransaction trans);
        bool Exists(int LogID);
        int Update(ESP.Finance.Entity.BankCancelInfo model);
        int Delete(int LogID);
        ESP.Finance.Entity.BankCancelInfo GetModel(int LogID);
        IList<ESP.Finance.Entity.BankCancelInfo> GetList(string term, List<SqlParameter> paramList);
        IList<ESP.Finance.Entity.BankCancelInfo> GetBatchList(string term, List<SqlParameter> paramList);
    }
}
