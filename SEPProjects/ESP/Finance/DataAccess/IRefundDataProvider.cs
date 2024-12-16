using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IRefundDataProvider
    {
        int Add(ESP.Finance.Entity.RefundInfo model);
        int Add(ESP.Finance.Entity.RefundInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.RefundInfo model);
        int Update(ESP.Finance.Entity.RefundInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Delete(int refundId);
        string CreateRefundCode();
        ESP.Finance.Entity.RefundInfo GetModel(int refundId);
        ESP.Finance.Entity.RefundInfo GetModel(int refundId, System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.RefundInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        int GetRecordsCount(string strWhere, List<System.Data.SqlClient.SqlParameter> parms);
        List<ESP.Finance.Entity.RefundInfo> GetModelListPage(int pageSize, int pageIndex, string strWhere, List<System.Data.SqlClient.SqlParameter> parms);
        IList<ESP.Finance.Entity.RefundInfo> GetList(string term, System.Data.SqlClient.SqlTransaction trans);
        int CommitWorkflow(RefundInfo refundModel, List<ESP.Finance.Entity.WorkFlowInfo> operationList);
        IList<RefundInfo> GetWaitAuditList(int[] userIds);
        int RefundAudit(ESP.Finance.Entity.RefundInfo refundModel, ESP.Compatible.Employee currentUser, int status, string suggestion, int NextFinanceAuditor);
    }
}
