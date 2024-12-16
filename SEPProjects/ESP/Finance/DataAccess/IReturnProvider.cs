using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IReturnDataProvider
    {
        int Add(ESP.Finance.Entity.ReturnInfo model);
        int Add(ESP.Finance.Entity.ReturnInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.ReturnInfo model);
        int Update(ESP.Finance.Entity.ReturnInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Delete(int returnId);
        ESP.Finance.Entity.ReturnInfo GetModel(int returnId);
        ESP.Finance.Entity.ReturnInfo GetModel(int returnId,System.Data.SqlClient.SqlTransaction trans);
        int UpdateWorkFlow(int ReturnID, int workItemID, string workItemName, int processID, int instanceID);
        int UpdateProjectCode(int projectId, string projectCode);
        int UpdateProjectCode(int projectId, string projectCode,System.Data.SqlClient.SqlTransaction trans);
        int Payment(ESP.Finance.Utility.PaymentStatus status,ESP.Finance.Entity.ReturnInfo model);
        int Payment(ESP.Finance.Utility.PaymentStatus status, ESP.Finance.Entity.ReturnInfo model,System.Data.SqlClient.SqlTransaction trans);
        int UpdateIsInvoice(int ReturnID, int IsInvoice);
        decimal GetTotalPNFee(ESP.Finance.Entity.ReturnInfo returnModel);
        //IList<ESP.Finance.Entity.ReturnInfo> GetAllList();
        //IList<ESP.Finance.Entity.ReturnInfo> GetList(string term);
        IList<ESP.Finance.Entity.ReturnInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        int GetRecordsCount(string strWhere, List<System.Data.SqlClient.SqlParameter> parms);
        List<ESP.Finance.Entity.ReturnInfo> GetModelListPage(int pageSize, int pageIndex, string strWhere, List<System.Data.SqlClient.SqlParameter> parms);
        IList<ESP.Finance.Entity.ReturnInfo> GetList(string term,System.Data.SqlClient.SqlTransaction trans);
        System.Data.DataTable GetPNTableLinkPR(string terms, List<System.Data.SqlClient.SqlParameter> parms);
        System.Data.DataTable GetRecipientIds(string returnIds);
        bool returnPaymentInfo(int returnId);
        string GetNewReturnCode(System.Data.SqlClient.SqlTransaction trans);
        bool updataSatatusAndAddKillForegift(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Finance.Entity.ForeGiftLinkInfo foregift, int status);
        System.Data.DataTable GetPNTableForPurchasePN(string terms, List<System.Data.SqlClient.SqlParameter> parms);
        int Add(List<ESP.Finance.Entity.ReturnInfo> returnList);
        System.Data.DataTable GetReturnListJoinHist(int userId);
        int changeRequestor(string returnIds, int oldUserId, int newUserId,ESP.Compatible.Employee currentUser);
        int changAuditor(string returnIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser);
        System.Data.DataTable GetProxyReturnList(string term);
        int DeleteWorkFlow(int returnid);

        DataTable GetFactoringList(string term);

        #region is in a Trans
        //int UpdateWorkFlow(int ReturnID, int workItemID, string workItemName, int processID, int instanceID, bool isInTrans);
        //int UpdateProjectCode(int projectId, string projectCode, bool isInTrans);

        //ESP.Finance.Entity.ReturnInfo GetModel(int returnId,bool isInTrans);
        //int Payment(ESP.Finance.Utility.PaymentStatus status, ESP.Finance.Entity.ReturnInfo model,bool isInTrans);
        #endregion


        int GetDistinctReturnTypeByBatchID(int batchid);
        string GetDistinctBranchCodeByBatchID(int batchid);
        string GetDistinctRecipientByReturnID(int returnId);
        string GetDistinctRecipientByBatchID(int batchId);
        IList<ESP.Finance.Entity.ReturnInfo> GetWaitAuditList(string userIds, string strTerms, List<System.Data.SqlClient.SqlParameter> parms);
        IList<ESP.Finance.Entity.ReturnInfo> GetWaitAuditList(int[] userIds);
        int BatchRepayToRequest(int batchId, string reason, ESP.Compatible.Employee CurrentUser);
        int BatchRepayToFinance(int batchId, string reason, ESP.Compatible.Employee CurrentUser);
        int BatchRepayCommit(int batchId, string supplier, string bank, string account, string remark, ESP.Compatible.Employee CurrentUser);

        decimal GetTotalRefund(string projectCode, int departmentId);

        DataTable GetRecipientReport(string where);

        IList<ESP.Finance.Entity.ReturnInfo> GetPaidPNReport(int userid, DateTime dtbegin, DateTime dtend);

        IList<Finance.Entity.ReturnInfo> GetTicketBatch(int batchid);

        int GetCount(string term, List<System.Data.SqlClient.SqlParameter> param);

        int SettingOOPCurrentAudit(string returncode);
    }
}
