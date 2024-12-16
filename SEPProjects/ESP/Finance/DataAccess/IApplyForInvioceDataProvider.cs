using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IApplyForInvioceDataProvider
    {
        int Add(ESP.Finance.Entity.ApplyForInvioceInfo model);
        int Update(ESP.Finance.Entity.ApplyForInvioceInfo model);
        int Delete(int id);
        ESP.Finance.Entity.ApplyForInvioceInfo GetModel(int id);
        IList<ESP.Finance.Entity.ApplyForInvioceInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        int UpdateStatus(string Ids, ESP.Finance.Utility.ApplyForInvioceStatus.Status status);
    }
}
