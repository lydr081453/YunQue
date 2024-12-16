using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICostDescriptionDataProvider
    {
        int Add(ESP.Finance.Entity.CostDescriptionInfo model);
        int Update(ESP.Finance.Entity.CostDescriptionInfo model);
        int Delete(int costDesId);
        ESP.Finance.Entity.CostDescriptionInfo GetModel(int costDesId);

        //IList<ESP.Finance.Entity.CostDescriptionInfo> GetAllList();
        //IList<ESP.Finance.Entity.CostDescriptionInfo> GetList(string term);
        IList<ESP.Finance.Entity.CostDescriptionInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
