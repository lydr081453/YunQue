using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPNBatchRelationProvider
    {
        bool Exists(int batchID, int ReturnID);
        int Add(ESP.Finance.Entity.PNBatchRelationInfo model);
        int Add(ESP.Finance.Entity.PNBatchRelationInfo model,SqlTransaction trans);
        int Add(List<ESP.Finance.Entity.PNBatchRelationInfo> relationList);
        int Add(List<ESP.Finance.Entity.PNBatchRelationInfo> relationList,SqlTransaction trans);
        int Update(ESP.Finance.Entity.PNBatchRelationInfo model);
        int Delete(int BatchRelationID);
        int DeleteByReturnID(int batchid,ESP.Finance.Entity.ReturnInfo model);
        ESP.Finance.Entity.PNBatchRelationInfo GetModel(int BatchRelationID);
        ESP.Finance.Entity.PNBatchRelationInfo GetModelByReturnId(int returnId,SqlTransaction trans);
        IList<ESP.Finance.Entity.PNBatchRelationInfo> GetList(string strWhere, List<SqlParameter> parameters);
        IList<ESP.Finance.Entity.PNBatchRelationInfo> GetList(string strWhere, List<SqlParameter> parameters, SqlTransaction trans);
        int DeleteByBatchID(int BatchID);
        int DeleteByBatchID(int BatchID,SqlTransaction trans);
        int Delete(int batchid, int returnid);
    }
}
