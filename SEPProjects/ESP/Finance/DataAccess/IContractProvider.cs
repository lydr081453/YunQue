using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IContractDataProvider
    {
        int GetNewVersion(int contractId);
        int Add(ESP.Finance.Entity.ContractInfo model);
        int Add(ESP.Finance.Entity.ContractInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.ContractInfo model);
        int Update(ESP.Finance.Entity.ContractInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Delete(int contractId);
        ESP.Finance.Entity.ContractInfo GetModel(int contractId);

        //IList<ESP.Finance.Entity.ContractInfo> GetAllList();
        //IList<ESP.Finance.Entity.ContractInfo> GetList(string term);
        IList<ESP.Finance.Entity.ContractInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ContractInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.ContractInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ContractInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,System.Data.SqlClient.SqlTransaction trans);

        decimal GetTotalCostByProject(int projectId);
        decimal GetTotalAmountByProject(int projectId, int originalContractID);
        decimal GetOddAmountByProject(int projectID, int contractID, int originalContractID);
        int UpdateContractDel(int projectID, bool Del);
        int UpdateContractDel(int projectID, bool Del,System.Data.SqlClient.SqlTransaction trans);
        int UpdateContractStatus(string contractIds, ESP.Finance.Utility.ContractStatus.Status status);
        #region is in trans
        //int GetNewVersion(int contractId, bool isIntrans);
        //int Update(ESP.Finance.Entity.ContractInfo model,bool isInTrans);
        //decimal GetTotalCostByProject(int projectId, bool isInTrans);
        //decimal GetTotalAmountByProject(int projectId,bool isInTrans);

        //ESP.Finance.Entity.ContractInfo GetModel(int contractId,bool isInTrans);
        //IList<ESP.Finance.Entity.ContractInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        //IList<ESP.Finance.Entity.ContractInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
