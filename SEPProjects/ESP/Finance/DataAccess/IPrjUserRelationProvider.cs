using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
       [ESP.Configuration.Provider]
    public interface  IPrjUserRelationProvider
    {
           ESP.Finance.Entity.PrjUserRelationInfo GetModel(int PID);
           IList<ESP.Finance.Entity.PrjUserRelationInfo> GetList(string strWhere, List<System.Data.SqlClient.SqlParameter> paramlist);

    }
}
