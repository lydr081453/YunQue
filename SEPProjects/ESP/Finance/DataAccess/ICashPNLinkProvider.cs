using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICashPNLinkProvider
    {
        int Add(ESP.Finance.Entity.CashPNLinkInfo model);
        bool AddAndUpdateReturn(List<ESP.Finance.Entity.CashPNLinkInfo> modelList, List<ESP.Finance.Entity.ReturnInfo> returnModel);
        int Update(ESP.Finance.Entity.CashPNLinkInfo model);
        void Delete(int CashPNLinkId);
        ESP.Finance.Entity.CashPNLinkInfo GetModel(int CashPNLinkId);
        List<ESP.Finance.Entity.CashPNLinkInfo> GetList(string terms, List<SqlParameter> parms);
    }
}
