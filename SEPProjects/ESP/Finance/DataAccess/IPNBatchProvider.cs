using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface  IPNBatchProvider
    {
        int Add(ESP.Finance.Entity.PNBatchInfo model);
        int Add(ESP.Finance.Entity.PNBatchInfo model,SqlTransaction trans);
        int Update(ESP.Finance.Entity.PNBatchInfo model);
        int Update(ESP.Finance.Entity.PNBatchInfo model, SqlTransaction trans);
        int UpdateAmounts(ESP.Finance.Entity.PNBatchInfo model, SqlTransaction trans);
        int Delete(int BatchID);
        int Delete(int BatchID,SqlTransaction trans);
        ESP.Finance.Entity.PNBatchInfo GetModel(int BatchID);
        ESP.Finance.Entity.PNBatchInfo GetModel(int BatchID,SqlTransaction trans);
        IList<ESP.Finance.Entity.PNBatchInfo> GetList(string strWhere, List<SqlParameter> paramList);
        int Exist(string batchCode, int batchID);
        ESP.Finance.Entity.PNBatchInfo GetModelByBatchCode(string Code);
        IList<ESP.Finance.Entity.PNBatchInfo> GetWaitAuditList(int[] userIds, int batchType);
        IList<ESP.Finance.Entity.ReturnInfo> GetReturnList(int batchID);
        bool returnBatchForPurchase(int BatchID,ESP.Compatible.Employee CurrentUser,string requesition);
        string CreatePurchaseBatchCode();
        DataTable GetBatchByExpenseAccount(string whereStr);
        int AuditConsumption(ESP.Finance.Entity.PNBatchInfo batchModel, int formType, ESP.Compatible.Employee currentUser, int status, string suggestion);
        int AuditRebateRegistration(ESP.Finance.Entity.PNBatchInfo batchModel, ESP.Compatible.Employee currentUser, int status, string suggestion);
        
    }
}
