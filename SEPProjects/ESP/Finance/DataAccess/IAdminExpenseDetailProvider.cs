using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IAdminExpenseDetailProvider
    {
        int Update(ESP.Finance.Entity.AdminExpenseDetailInfo model);
        ESP.Finance.Entity.AdminExpenseDetailInfo GetModel(int AreaId, int ExpenseTypeId);
        int Update(ESP.Finance.Entity.AdminExpenseDetailInfo model,System.Data.SqlClient.SqlTransaction trans);
        ESP.Finance.Entity.AdminExpenseDetailInfo GetModel(int AreaId, int ExpenseTypeId, System.Data.SqlClient.SqlTransaction trans);
    }
}
