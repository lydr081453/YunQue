using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ITaxDetailProvider
    {
        int Add(ESP.Finance.Entity.TaxDetailInfo model);
        int Update(ESP.Finance.Entity.TaxDetailInfo model);
        int Delete(int Id);
        ESP.Finance.Entity.TaxDetailInfo GetModel(int Id);
        IList<ESP.Finance.Entity.TaxDetailInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
