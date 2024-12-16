using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICostRecordProvider
    {
        bool Exists(int RecordID);
        int Add(ESP.Finance.Entity.CostRecordInfo model);
        int Add(ESP.Finance.Entity.CostRecordInfo model, SqlTransaction trans);
        void Update(ESP.Finance.Entity.CostRecordInfo model);
        void Delete(int RecordID);
        void DeleteByProject(int projectId);
        void DeleteAll(SqlTransaction trans);
        ESP.Finance.Entity.CostRecordInfo GetModel(int bizTypeId);
        List<ESP.Finance.Entity.CostRecordInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
