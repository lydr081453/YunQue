using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IAuditHistoryDataProvider
    {
        int Add(ESP.Finance.Entity.AuditHistoryInfo model);
        int Add(ESP.Finance.Entity.AuditHistoryInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.AuditHistoryInfo model);
        int Update(ESP.Finance.Entity.AuditHistoryInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Delete(int AuditId);
        int Delete(int AuditId,System.Data.SqlClient.SqlTransaction trans);
        ESP.Finance.Entity.AuditHistoryInfo GetModel(int AuditId);
        IList<ESP.Finance.Entity.AuditHistoryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.AuditHistoryInfo> GetList(string term, System.Data.SqlClient.SqlTransaction trans, params System.Data.SqlClient.SqlParameter[] param);
                
        int DeleteByProjectId(int ProjectId);
        int DeleteByProjectId(int ProjectId, System.Data.SqlClient.SqlTransaction trans);
        int DeleteByProjectId(int ProjectId, string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);


        #region with sql trans
        //int Add(ESP.Finance.Entity.AuditHistoryInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.AuditHistoryInfo model, bool isInTrans);
        //int DeleteByProjectId(int ProjectId,  bool isInTrans);
        //int DeleteByProjectId(int ProjectId, string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);

        //ESP.Finance.Entity.AuditHistoryInfo GetModel(int AuditId,bool isInTrans);
        //IList<ESP.Finance.Entity.AuditHistoryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);

        #endregion
    }
}
