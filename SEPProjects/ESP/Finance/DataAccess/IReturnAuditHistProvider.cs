using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IReturnAuditHistDataProvider
    {
        bool Exists(int ReturnAuditID);
        int Add(ESP.Finance.Entity.ReturnAuditHistInfo model);
        int Add(ESP.Finance.Entity.ReturnAuditHistInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.ReturnAuditHistInfo model);
        int Update(ESP.Finance.Entity.ReturnAuditHistInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Delete(int AuditId);
        ESP.Finance.Entity.ReturnAuditHistInfo GetModel(int AuditId);

        int DeleteByReturnID(int ReturnID);
        int DeleteByReturnID(int ReturnID, string term, List<System.Data.SqlClient.SqlParameter> param);
        int DeleteByReturnID(int ReturnID, string term, System.Data.SqlClient.SqlTransaction trans, List<System.Data.SqlClient.SqlParameter> param);
        int DeleteNotAudit(int returnId);
        int DeleteNotAudit(int returnId,System.Data.SqlClient.SqlTransaction trans);
        int DeleteByParameters(string term, List<SqlParameter> parms);
        int DeleteByParameters(string term, List<SqlParameter> parms,System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.ReturnAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ReturnAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,SqlTransaction trans);

        #region with sql trans
        //int Add(ESP.Finance.Entity.ReturnAuditHistInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.ReturnAuditHistInfo model, bool isInTrans);
        //int DeleteByReturnID(int ReturnID, bool isInTrans);
        //int DeleteByReturnID(int ReturnID, string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);

        //ESP.Finance.Entity.ReturnAuditHistInfo GetModel(int AuditId);
        //IList<ESP.Finance.Entity.ReturnAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        #endregion
    }
}


