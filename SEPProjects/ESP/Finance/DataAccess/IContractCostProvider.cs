using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IContractCostDataProvider
    {
        int Add(ESP.Finance.Entity.ContractCostInfo model);
        int Update(ESP.Finance.Entity.ContractCostInfo model);
        int Delete(int contractCostId);
        ESP.Finance.Entity.ContractCostInfo GetModel(int contractCostId);
        decimal GetTotalCost(int projectId);

        //IList<ESP.Finance.Entity.ContractCostInfo> GetAllList();
        //IList<ESP.Finance.Entity.ContractCostInfo> GetList(string term);
        IList<ESP.Finance.Entity.ContractCostInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ContractCostInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.ContractCostInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ContractCostInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param,System.Data.SqlClient.SqlTransaction trans);

        #region is in trans
        //int Add(ESP.Finance.Entity.ContractCostInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.ContractCostInfo model, bool isInTrans);
        //int Delete(int contractCostId, bool isInTrans);
        ////ESP.Finance.Entity.ContractCostInfo GetModel(int contractCostId, bool isInTrans);
        //decimal GetTotalCost(int projectId,bool isInTrans);


        //ESP.Finance.Entity.ContractCostInfo GetModel(int contractCostId,bool isInTrans);
        //IList<ESP.Finance.Entity.ContractCostInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        //IList<ESP.Finance.Entity.ContractCostInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);

        #endregion
    }
}
