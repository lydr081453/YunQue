using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IReturnInvoiceProvider
    {
        int Add(ESP.Finance.Entity.ReturnInvoiceInfo model);
        int Update(ESP.Finance.Entity.ReturnInvoiceInfo model);
        int Delete(int branchId);
        ESP.Finance.Entity.ReturnInvoiceInfo GetModel(int branchId);
        ESP.Finance.Entity.ReturnInvoiceInfo GetModelByReturnID(int ReturnID);
        IList<ESP.Finance.Entity.ReturnInvoiceInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
