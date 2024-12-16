using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuditorManager
    {
        private static IAuditorDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IAuditorDataProvider>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="auditorType"></param>
        /// <returns></returns>
        public static AuditorInfo GetAuditor(int departmentId, string auditorType)
        {
            return GetProvider().GetAuditor(departmentId, auditorType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="auditorType"></param>
        /// <returns></returns>
        public static int GetAuditorId(int departmentId, string auditorType)
        {
            return GetProvider().GetAuditorId(departmentId, auditorType);
        }
    }
}
