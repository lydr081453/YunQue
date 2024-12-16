using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpenseDataProvider
    {
        int Add(ESP.Finance.Entity.ExpenseInfo model);
        int Update(ESP.Finance.Entity.ExpenseInfo model);
        int Delete(int expenseId);
        ESP.Finance.Entity.ExpenseInfo GetModel(int expenseId);

        //IList<ESP.Finance.Entity.ExpenseInfo> GetAllList();
        //IList<ESP.Finance.Entity.ExpenseInfo> GetList(string term);
        IList<ESP.Finance.Entity.ExpenseInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
