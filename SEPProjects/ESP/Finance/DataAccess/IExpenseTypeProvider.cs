using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpenseTypeProvider
    {
        int Add(ESP.Finance.Entity.ExpenseTypeInfo model);
        int Update(ESP.Finance.Entity.ExpenseTypeInfo model);
        int Delete(int id);
        ESP.Finance.Entity.ExpenseTypeInfo GetModel(int id);
        List<ESP.Finance.Entity.ExpenseTypeInfo> GetList(string term);
    }
}
