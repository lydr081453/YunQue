using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface  IConsumptionAuditProvider
    {
        int Add(ESP.Finance.Entity.ConsumptionAuditInfo model);
        int Add(ESP.Finance.Entity.ConsumptionAuditInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.ConsumptionAuditInfo model);
        int Update(ESP.Finance.Entity.ConsumptionAuditInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Delete(int AuditId);
        int Delete(int AuditId, System.Data.SqlClient.SqlTransaction trans);
        ESP.Finance.Entity.ConsumptionAuditInfo GetModel(int AuditId);
        IList<ESP.Finance.Entity.ConsumptionAuditInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ConsumptionAuditInfo> GetList(string term, System.Data.SqlClient.SqlTransaction trans, params System.Data.SqlClient.SqlParameter[] param);

        int DeleteByBatchID(int BatchId, int formType);
        int DeleteByBatchID(int BatchId, int formType, System.Data.SqlClient.SqlTransaction trans);
        int DeleteByBatchID(int BatchId, int formType, string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);

    }
}



