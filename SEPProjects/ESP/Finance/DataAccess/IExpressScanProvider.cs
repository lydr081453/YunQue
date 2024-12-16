 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpressScanProvider
    {
        bool Exists(string expressNo);
        int Add(ESP.Finance.Entity.ExpressScanInfo model);
        int Update(ESP.Finance.Entity.ExpressScanInfo model);
        int Delete(int id);
        ESP.Finance.Entity.ExpressScanInfo GetModel(int id);
        ESP.Finance.Entity.ExpressScanInfo GetModel(string expNo);
        IList<ESP.Finance.Entity.ExpressScanInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}