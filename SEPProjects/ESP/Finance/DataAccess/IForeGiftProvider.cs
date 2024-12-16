using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IForeGiftProvider
    {
        int Add(ESP.Finance.Entity.ForeGiftLinkInfo model);
        int Update(ESP.Finance.Entity.ForeGiftLinkInfo model);
        int Delete(int linkId);
        ESP.Finance.Entity.ForeGiftLinkInfo GetModel(int linkId);
        IList<ESP.Finance.Entity.ForeGiftLinkInfo> GetList(string strWhere, List<SqlParameter> paramList);
        DataTable GetKillList(string strWhere, List<SqlParameter> parmList);
    }
}
