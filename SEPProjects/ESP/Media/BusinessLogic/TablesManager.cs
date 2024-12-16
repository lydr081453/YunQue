using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using ESP.Media.Entity;
using ESP.Media.Access;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class TablesManager
    {
        public static DataTable GetAll()
        {
            return ESP.Media.DataAccess.TablesDataProvider.QueryInfo(null, new Hashtable());
        }


        public static TablesInfo GetModel(int id)
        {
            return ESP.Media.DataAccess.TablesDataProvider.Load(id);
        }


        public static TablesInfo GetModel(string tablename)
        {
            string sql = "select * from media_tables as a where a.tablename = @tablename";
            SqlParameter param = new SqlParameter("@tablename", SqlDbType.NVarChar, 100);
            param.Value = tablename;
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
            if (dt == null || dt.Rows.Count == 0) return null;
            int id = dt.Rows[0]["id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["id"]);
            if (id > 0)
            {
                return GetModel(id);
            }
            return null;
        }


        public static TablesInfo GetModel(int id,SqlTransaction trans)
        {
            return ESP.Media.DataAccess.TablesDataProvider.Load(id,trans);
        }


        public static TablesInfo GetModel(string tablename,SqlTransaction trans)
        {
            string sql = "select * from media_tables as a where a.tablename = @tablename";
            SqlParameter param = new SqlParameter("@tablename", SqlDbType.NVarChar, 100);
            param.Value = tablename;
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
            if (dt == null || dt.Rows.Count == 0) return null;
            int id = dt.Rows[0]["id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["id"]);
            if (id > 0)
            {
                return GetModel(id,trans);
            }
            return null;
        }
    }
}
