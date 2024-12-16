using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IAreaDataProvider
    {
        int Add(ESP.Finance.Entity.AreaInfo model);
        int Update(ESP.Finance.Entity.AreaInfo model);
        int Delete(int areaid);
        ESP.Finance.Entity.AreaInfo GetModel(int areaid);

        //IList<ESP.Finance.Entity.AreaInfo> GetAllList();
        //IList<ESP.Finance.Entity.AreaInfo> GetList(string term);
        IList<ESP.Finance.Entity.AreaInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
