using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IMaterialFinanceRelationProvider
    {
        int Add(ESP.Finance.Entity.MaterialFinanceRelationInfo model);
        int Update(ESP.Finance.Entity.MaterialFinanceRelationInfo model);
        int Delete(int branchId);
        ESP.Finance.Entity.MaterialFinanceRelationInfo GetModel(int materialId,int materialType);
        IList<ESP.Finance.Entity.MaterialFinanceRelationInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);

    }
}
