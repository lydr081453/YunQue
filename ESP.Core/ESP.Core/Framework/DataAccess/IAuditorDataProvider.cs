using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IAuditorDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="auditorType"></param>
        /// <returns></returns>
        ESP.Framework.Entity.AuditorInfo GetAuditor(int departmentId, string auditorType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="auditorType"></param>
        /// <returns></returns>
        int GetAuditorId(int departmentId, string auditorType);
    }
}
