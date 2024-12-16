using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class ITAssetCategoryManager
    {
        private static ESP.Finance.IDataAccess.IITAssetCategoryProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IITAssetCategoryProvider>.Instance; } }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ITAssetCategoryInfo> GetList()
        {
            return DataProvider.GetList();
        }

        public static ESP.Finance.Entity.ITAssetCategoryInfo GetModel(int Id)
        {
            return DataProvider.GetModel(Id);
        }
    }
}
