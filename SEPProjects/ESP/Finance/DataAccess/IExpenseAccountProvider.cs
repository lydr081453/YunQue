using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpenseAccountProvider
    {
        int Add(ESP.Finance.Entity.ExpenseAccountInfo model);
        int Update(ESP.Finance.Entity.ExpenseAccountInfo model);
        int CloseWorkFlow(Guid instanceId, int operatorId);
        int WorkFlowRejectToFinance1(Guid instanceId,int returnId);
        int WorkFlowReject(Guid instanceId, int returnId);
        int Delete(int id);
        ESP.Finance.Entity.ExpenseAccountInfo GetModel(int id);
        List<ESP.Finance.Entity.ExpenseAccountInfo> GetList(string term);
        ESP.Finance.Entity.ExpenseAccountInfo GetModelByReturnID(int ReturnID);
        
        bool ExpenseMoneyGreaterThan2000(int expenseAccountId);
        ESP.Finance.Entity.ExpenseAccountExtendsInfo GetWorkItemModel(int ID);
        DataSet GetMajorAuditList(string whereStr, string WhereStr2);
        DataSet GetMajorAuditListByBatch(string whereStr);
        DataSet GetMajorAlreadyAuditList(string whereStr);
        DataSet GetExportExpenseDetail(string whereStr);
        List<ESP.Finance.SqlDataAccess.WorkItem> GetWorkItemsByBatchID(int batchid);
        DataSet GetAlreadyAuditDetailList(string whereStr);
        DataSet GetExpenseOrderView(string whereStr);
    }
}
