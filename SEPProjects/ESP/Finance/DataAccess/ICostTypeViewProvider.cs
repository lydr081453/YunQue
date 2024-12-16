using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICostTypeViewDataProvider
    {
        ESP.Finance.Entity.CostTypeViewInfo GetModel(int id);

        //IList<ESP.Finance.Entity.AreaInfo> GetAllList();
        //IList<ESP.Finance.Entity.AreaInfo> GetList(string term);
        IList<ESP.Finance.Entity.CostTypeViewInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
