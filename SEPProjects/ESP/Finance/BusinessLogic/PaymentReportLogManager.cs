using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类PaymentReportLog 的摘要说明。
    /// </summary>


    public static class PaymentReportLogManager
    {
        private static ESP.Finance.IDataAccess.IPaymentReportLogProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPaymentReportLogProvider>.Instance; } }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.PaymentReportLogInfo model)
        {
            return DataProvider.Add(model);
        }

        public static bool Exists(int userid, DateTime dt1, DateTime dt2)
        {
            return DataProvider.Exists(userid,dt1,dt2);
        }
    }
}
