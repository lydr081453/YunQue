using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IDeadLineDataProvider
    {
        int Add(ESP.Finance.Entity.DeadLineInfo model);
        int Update(ESP.Finance.Entity.DeadLineInfo model);
        int Delete(int DeadLineInfo);
        ESP.Finance.Entity.DeadLineInfo GetModel(int id);
        ESP.Finance.Entity.DeadLineInfo GetMonthModel(int year,int month);
        IList<ESP.Finance.Entity.DeadLineInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
