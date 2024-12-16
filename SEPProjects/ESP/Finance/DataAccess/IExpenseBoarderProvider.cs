using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpenseBoarderProvider
    {
        bool Exists(string CardNo, int userId);
        int Add(ESP.Finance.Entity.ExpenseBoarderInfo model);
        int Update(ESP.Finance.Entity.ExpenseBoarderInfo model);
        int Delete(int branchId);
        ESP.Finance.Entity.ExpenseBoarderInfo GetModel(int Id);
        IList<ESP.Finance.Entity.ExpenseBoarderInfo> GetList(string term);
    }
}
