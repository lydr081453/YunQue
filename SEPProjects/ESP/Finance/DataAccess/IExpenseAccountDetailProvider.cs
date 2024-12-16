using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpenseAccountDetailProvider
    {
        int Add(ESP.Finance.Entity.ExpenseAccountDetailInfo model);
        int Update(ESP.Finance.Entity.ExpenseAccountDetailInfo model);
        int Update(ESP.Finance.Entity.ExpenseAccountDetailInfo model, SqlTransaction trans);
        int Delete(int id);
        int CheckPhoneInvoiceNo(int detailId, string invoiceNo);
        ESP.Finance.Entity.ExpenseAccountDetailInfo GetModel(int id);
        List<ESP.Finance.Entity.ExpenseAccountDetailInfo> GetList(string term);
        decimal GetTotalMoneyByReturnID(int expenseAccountID);
        Decimal GetTotalMoneyByReturnID(int returnID, int detailID);
        List<int> GetPhoneMonthList(int year, int userid, int detailid);
        bool ExistsMellFee(int year, int month, int userid, string type);
        decimal GetTotalOOPByReturnID(int returnid);
        int UpdateStatusByReturnID(int returnID, int status);
        ESP.Finance.Entity.ExpenseAccountDetailInfo GetParentModel(int parentId);
        List<ESP.Finance.Entity.TicketUserInfo> GetTicketUserList(string strWhere);
        int UpdateTicketUsed(string ids);
        int UpdateTicketConfirm(string ids);
        List<ESP.Finance.Entity.ExpenseAccountDetailInfo> GetIicketUsed(string term);
        List<ESP.Finance.Entity.ExpenseAccountDetailInfo> GetIicketConfirm(string term);
        List<ESP.Finance.Entity.ExpenseAccountDetailInfo> GetTicketDetail(int returnid);
        int CloseWorkflow(string instanceid,int workitemid);
        DataTable GetDetailListForReport(int batchId);
        DataTable GetIicketCheck(string strWhere);
    }
}
