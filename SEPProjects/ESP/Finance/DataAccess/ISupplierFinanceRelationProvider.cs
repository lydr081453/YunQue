using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISupplierFinanceRelationProvider
    {
        int Add(ESP.Finance.Entity.SupplierFinanceRelationInfo model);
        int Update(ESP.Finance.Entity.SupplierFinanceRelationInfo model);
        int Delete(int relationId);
        ESP.Finance.Entity.SupplierFinanceRelationInfo GetModel(int userid);
        IList<ESP.Finance.Entity.SupplierFinanceRelationInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
