using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IContractStatusDataProvider
    {
        int Add(ESP.Finance.Entity.ContractStatusInfo model);
        int Update(ESP.Finance.Entity.ContractStatusInfo model);
        int Delete(int contractStatusId);
        ESP.Finance.Entity.ContractStatusInfo GetModel(int contractStatusId);

        //IList<ESP.Finance.Entity.ContractStatusInfo> GetAllList();
        //IList<ESP.Finance.Entity.ContractStatusInfo> GetList(string term);
        IList<ESP.Finance.Entity.ContractStatusInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
