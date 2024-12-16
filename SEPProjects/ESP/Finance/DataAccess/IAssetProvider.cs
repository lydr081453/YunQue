using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IAssetProvider
    {
        int Add(ESP.Finance.Entity.AssetInfo model);
        int Update(ESP.Finance.Entity.AssetInfo model);
        int Delete(int assetid);
        ESP.Finance.Entity.AssetInfo GetModel(int assetid);
        IList<ESP.Finance.Entity.AssetInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
