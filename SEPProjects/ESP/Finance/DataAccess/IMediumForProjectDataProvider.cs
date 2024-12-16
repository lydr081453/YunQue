using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IMediumForProjectDataProvider
    {
        int Add(ESP.Finance.Entity.MediumForProjectInfo model);
        int Update(ESP.Finance.Entity.MediumForProjectInfo model);
        int Delete(int areaid);
        ESP.Finance.Entity.MediumForProjectInfo GetModel(int areaid);

        IList<ESP.Finance.Entity.MediumForProjectInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
