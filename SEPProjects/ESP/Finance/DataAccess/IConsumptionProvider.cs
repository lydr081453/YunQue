using ESP.Finance.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IConsumptionProvider
    {
        int Add(ESP.Finance.Entity.ConsumptionInfo model);
        int Add(ESP.Finance.Entity.ConsumptionInfo model, System.Data.SqlClient.SqlTransaction trans);
        int ImpList(PNBatchInfo batchModel, List<ESP.Finance.Entity.ConsumptionInfo> list);
        int Update(ESP.Finance.Entity.ConsumptionInfo model);
        int Update(ESP.Finance.Entity.ConsumptionInfo model, SqlTransaction trans);
        int Delete(int cid);
        ESP.Finance.Entity.ConsumptionInfo GetModel(int cid);
        int DeleteByBatch(int batchId,SqlTransaction trans);
        IList<ESP.Finance.Entity.ConsumptionInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ConsumptionInfo> GetCostList(string term);
        IList<ConsumptionInfo> GetAuditingConsumption(int projectId);
    }
}
