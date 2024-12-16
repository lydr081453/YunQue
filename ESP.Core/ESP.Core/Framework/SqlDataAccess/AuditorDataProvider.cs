using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    class AuditorDataProvider : IAuditorDataProvider
    {
        #region IAuditorDataProvider 成员

        public ESP.Framework.Entity.AuditorInfo GetAuditor(int departmentId, string auditorType)
        {
            throw new NotImplementedException();
        }

        public int GetAuditorId(int departmentId, string auditorType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
