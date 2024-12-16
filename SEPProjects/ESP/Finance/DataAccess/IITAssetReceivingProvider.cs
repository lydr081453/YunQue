using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IITAssetReceivingProvider
    {
        int Add(ESP.Finance.Entity.ITAssetReceivingInfo model);
        int Add(ESP.Finance.Entity.ITAssetReceivingInfo model,string serverstring);
        int Update(ESP.Finance.Entity.ITAssetReceivingInfo model);
        int UpdateReturnStatus(ESP.Finance.Entity.ITAssetReceivingInfo model, string serverstring);
        int Delete(int assetid);
        ESP.Finance.Entity.ITAssetReceivingInfo GetModel(int assetid);
        IList<ESP.Finance.Entity.ITAssetReceivingInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);

        ESP.Finance.Entity.ITAssetReceivingInfo getLastModel(int AssetId);

    }
}
