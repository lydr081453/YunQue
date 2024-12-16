using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
public interface  IProjectHistLogProvider
    {
        int Add(ESP.Finance.Entity.ProjectHistLogInfo model, SqlTransaction trans);
        int Add(ESP.Finance.Entity.ProjectHistLogInfo model);
        ESP.Finance.Entity.ProjectHistLogInfo GetModel(int LogID);
        IList<ESP.Finance.Entity.ProjectHistLogInfo> GetList(string strWhere, List<SqlParameter> parms);
    }
}
