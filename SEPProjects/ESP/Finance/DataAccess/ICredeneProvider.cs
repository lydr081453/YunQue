using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICredeneProvider
    {
        int Add(ESP.Finance.Entity.CredenceInfo model);
        int Update(ESP.Finance.Entity.CredenceInfo model);
        int Delete(int credenceId);
        ESP.Finance.Entity.CredenceInfo GetModel(int userid);
        IList<ESP.Finance.Entity.CredenceInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
