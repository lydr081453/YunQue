using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class Clients
    {
        public static DataTable GetList()
        {
            using(SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                //ESP.Media.DataAccess.ClientsDataProvider.insertinfo(clients,trans);
                DataTable dt = ESP.Media.DataAccess.MediaitemsDataProvider.QueryInfo(" 1=1 order by a.mediaitemid desc", new Hashtable());
                //trans.Commit();
                trans.Rollback();
                //return 1;
                conn.Close();
                conn.Dispose();
                return dt;
            }
        }
    }
}
