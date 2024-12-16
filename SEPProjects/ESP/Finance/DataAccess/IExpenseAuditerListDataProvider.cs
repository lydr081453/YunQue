using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpenseAuditerListDataProvider
    {
        int Add(ESP.Finance.Entity.ExpenseAuditerListInfo model);
        int Add(ESP.Finance.Entity.ExpenseAuditerListInfo model, SqlTransaction trans);
        int Update(ESP.Finance.Entity.ExpenseAuditerListInfo model);
        int Delete(int id);
        ESP.Finance.Entity.ExpenseAuditerListInfo GetModel(int id);
        List<ESP.Finance.Entity.ExpenseAuditerListInfo> GetList(string term);
        int DeleteByReturnID(int id);
        int DeleteByReturnID(int id, SqlTransaction trans);
    }
}
