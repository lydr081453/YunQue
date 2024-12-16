using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IBizDescriptionDataProvider
    {
        int Add(ESP.Finance.Entity.BizDescriptionInfo model);
        int Update(ESP.Finance.Entity.BizDescriptionInfo model);
        int Delete(int bizDesId);
        ESP.Finance.Entity.BizDescriptionInfo GetModel(int bizDesId);

        //IList<ESP.Finance.Entity.BizDescriptionInfo> GetAllList();
        //IList<ESP.Finance.Entity.BizDescriptionInfo> GetList(string term);
        IList<ESP.Finance.Entity.BizDescriptionInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
