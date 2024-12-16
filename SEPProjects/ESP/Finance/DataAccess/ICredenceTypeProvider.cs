using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICredeneTypeProvider
    {
        int Add(ESP.Finance.Entity.CredenceTypeInfo model);
        int Update(ESP.Finance.Entity.CredenceTypeInfo model);
        int Delete(int typeId);
        ESP.Finance.Entity.CredenceTypeInfo GetModel(int typed);
        IList<ESP.Finance.Entity.CredenceTypeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}

