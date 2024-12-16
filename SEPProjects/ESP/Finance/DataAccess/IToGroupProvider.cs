using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
  public interface IToGroupProvider
    {
        int Add(ESP.Finance.Entity.ToGroupInfo model);
        int Update(ESP.Finance.Entity.ToGroupInfo model);
        int Delete(int toId);
        ESP.Finance.Entity.ToGroupInfo GetModel(int DepartmentId);
        IList<ESP.Finance.Entity.ToGroupInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
