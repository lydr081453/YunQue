using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISupporterAuditHistDataProvider
    {
        int Add(ESP.Finance.Entity.SupporterAuditHistInfo model);
        int Update(ESP.Finance.Entity.SupporterAuditHistInfo model);
        int Update(ESP.Finance.Entity.SupporterAuditHistInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Delete(int AuditId);
        ESP.Finance.Entity.SupporterAuditHistInfo GetModel(int AuditId);

        int DeleteBySupporterId(int SupporterId);
        int DeleteBySupporterId(int SupporterId, string term, List<System.Data.SqlClient.SqlParameter> param);
        int DeleteBySupporterId(int SupporterId, string term, List<System.Data.SqlClient.SqlParameter> param,System.Data.SqlClient.SqlTransaction trans);

        IList<ESP.Finance.Entity.SupporterAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);


        #region with sql trans
        //int Add(ESP.Finance.Entity.SupporterAuditHistInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.SupporterAuditHistInfo model, bool isInTrans);

        //int DeleteBySupporterId(int SupporterId, bool isInTrans);
        //int DeleteBySupporterId(int SupporterId, string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);

        //ESP.Finance.Entity.SupporterAuditHistInfo GetModel(int AuditId,bool isInTrans);
        //IList<ESP.Finance.Entity.SupporterAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
