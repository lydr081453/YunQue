using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IAuditLogDataProvider
    {
        int Add(ESP.Finance.Entity.AuditLogInfo model);
        int Add(ESP.Finance.Entity.AuditLogInfo model,System.Data.SqlClient.SqlTransaction trans);

        ESP.Finance.Entity.AuditLogInfo GetModel(int logid);

        IList<ESP.Finance.Entity.AuditLogInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);

        IList<ESP.Finance.Entity.AuditLogInfo> GetProjectList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.AuditLogInfo> GetProjectList(int projectId);
        IList<ESP.Finance.Entity.AuditLogInfo> GetProxyPNList(string term, List<System.Data.SqlClient.SqlParameter> param);

        IList<ESP.Finance.Entity.AuditLogInfo> GetSupporterList(string term, List<System.Data.SqlClient.SqlParameter> param);

        IList<ESP.Finance.Entity.AuditLogInfo> GetPaymentList(string term, List<System.Data.SqlClient.SqlParameter> param);

        IList<ESP.Finance.Entity.AuditLogInfo> GetReturnList(string term, List<System.Data.SqlClient.SqlParameter> param);

        IList<ESP.Finance.Entity.AuditLogInfo> GetBatchList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
