using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class OperateTypeManager
    {
        public static DataTable GetAll()
        {
            return ESP.Media.DataAccess.OperatetypeDataProvider.QueryInfo(null, new Hashtable());
        }
    }
}
