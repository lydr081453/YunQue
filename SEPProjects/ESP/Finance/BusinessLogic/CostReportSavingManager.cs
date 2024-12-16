using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类AreaInfo 的摘要说明。
    /// </summary>


    public static class CostReportSavingManager
    {
        private static ESP.Finance.IDataAccess.ICostReportSavingProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICostReportSavingProvider>.Instance; } }
        
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(ESP.Finance.Entity.CostReportSavingInfo model)
        {
            return DataProvider.Add(model);
        }
    }
}
