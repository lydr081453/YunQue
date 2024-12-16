using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class MediatypeManager
    {
        public static DataTable GetAllList()
        {
            return ESP.Media.DataAccess.MediatypeDataProvider.QueryInfo(null,new Hashtable());
        }
    }
}
