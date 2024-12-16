using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IBusinessCardProvider
    {
        int Add(ESP.Finance.Entity.BusinessCardInfo model);
        int Update(ESP.Finance.Entity.BusinessCardInfo model);
        int Delete(int bcid);
        ESP.Finance.Entity.BusinessCardInfo GetModel(int bcid);
        IList<ESP.Finance.Entity.BusinessCardInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        int IsHaveCard(int userid);
        BusinessCardInfo GetBusinessCard(int userid);
    }
}
