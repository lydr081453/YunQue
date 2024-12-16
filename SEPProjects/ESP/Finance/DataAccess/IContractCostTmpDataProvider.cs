using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IContractCostTmpDataProvider
    {
        int Add(ESP.Finance.Entity.ContractCostTmpInfo model);
        int Update(ESP.Finance.Entity.ContractCostTmpInfo model);
        int Delete(int contractCostId);
        ESP.Finance.Entity.ContractCostTmpInfo GetModel(int contractCostId);
        decimal GetTotalCost(int projectId);
        IList<ESP.Finance.Entity.ContractCostTmpInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ContractCostTmpInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.ContractCostTmpInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ContractCostTmpInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);

    }
}
