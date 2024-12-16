using ESP.Finance.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IRebateRegistrationDataProvider
    {
        int Add(ESP.Finance.Entity.RebateRegistrationInfo model);
        int Update(ESP.Finance.Entity.RebateRegistrationInfo model);
        int Delete(int areaid);
        ESP.Finance.Entity.RebateRegistrationInfo GetModel(int areaid);

        //IList<ESP.Finance.Entity.RebateRegistrationInfo> GetAllList();
        //IList<ESP.Finance.Entity.RebateRegistrationInfo> GetList(string term);
        IList<ESP.Finance.Entity.RebateRegistrationInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        int ImpList(PNBatchInfo batchModel, List<ESP.Finance.Entity.RebateRegistrationInfo> list);
        int DeleteByBatch(int batchId, SqlTransaction trans);
    }
}
