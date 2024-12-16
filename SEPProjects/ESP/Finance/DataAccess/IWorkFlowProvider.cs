using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IWorkFlowDataProvider
    {
        bool Exists(int ReturnAuditID);
        int Add(ESP.Finance.Entity.WorkFlowInfo model);
        int Add(ESP.Finance.Entity.WorkFlowInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.WorkFlowInfo model);
        int Update(ESP.Finance.Entity.WorkFlowInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Delete(int Id);
        ESP.Finance.Entity.WorkFlowInfo GetModel(int Id);

        int DeleteByModelId(int ModelId,int ModelType);
        int DeleteByModelId(int ModelId, int ModelType, string term, List<System.Data.SqlClient.SqlParameter> param);
        int DeleteByModelId(int ModelId, int ModelType, string term, System.Data.SqlClient.SqlTransaction trans, List<System.Data.SqlClient.SqlParameter> param);
        int DeleteNotAudit(int ModelId, int ModelType);
        int DeleteNotAudit(int ModelId, int ModelType, System.Data.SqlClient.SqlTransaction trans);
        int DeleteByParameters(string term, List<SqlParameter> parms);
        int DeleteByParameters(string term, List<SqlParameter> parms, System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.WorkFlowInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.WorkFlowInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans);

    }
}
