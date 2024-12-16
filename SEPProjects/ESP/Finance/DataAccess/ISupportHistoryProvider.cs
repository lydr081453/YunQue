using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using System.Data.SqlClient;
namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISupportHistoryProvider
    {
        int Add(SupportHistoryInfo model, SqlTransaction trans);
        int Add(ESP.Finance.Entity.SupportHistoryInfo model);
        ESP.Finance.Entity.SupportHistoryInfo GetModel(int supportId);
        IList<ESP.Finance.Entity.SupportHistoryInfo> GetListBySupport(int supportId);

    }
}
