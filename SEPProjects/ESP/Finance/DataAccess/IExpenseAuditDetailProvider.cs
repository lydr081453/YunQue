using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpenseAuditDetailProvider
    {
        int Add(ESP.Finance.Entity.ExpenseAuditDetailInfo model);
        int Add(ESP.Finance.Entity.ExpenseAuditDetailInfo model,SqlTransaction trans);
        int Update(ESP.Finance.Entity.ExpenseAuditDetailInfo model);
        int Delete(int id);
        ESP.Finance.Entity.ExpenseAuditDetailInfo GetModel(int id);
        List<ESP.Finance.Entity.ExpenseAuditDetailInfo> GetList(string term);
        System.Data.DataTable GetWorkflow(int id, int level);
        int DeleteByReturnID(int returnId, SqlTransaction trans);
    }
}
