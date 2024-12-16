using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IITAssetCategoryProvider
    {
        IList<ESP.Finance.Entity.ITAssetCategoryInfo> GetList();
        ESP.Finance.Entity.ITAssetCategoryInfo GetModel(int id);
    }
}
