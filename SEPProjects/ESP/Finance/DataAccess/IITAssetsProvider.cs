using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IITAssetsProvider
    {
        int Add(ESP.Finance.Entity.ITAssetsInfo model);
        int Update(ESP.Finance.Entity.ITAssetsInfo model);
        int Delete(int assetid);
        ESP.Finance.Entity.ITAssetsInfo GetModel(int assetid);
        IList<ESP.Finance.Entity.ITAssetsInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
