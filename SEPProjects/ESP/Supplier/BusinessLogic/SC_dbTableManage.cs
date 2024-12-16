using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.Common;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_dbTableManage
    {
        public DataTable getTable(List<int> ID, string TableName, string Version)
        {
            string str = "";
            for (int i = 0; i < ID.Count; i++)
            {
                str += ID[i].ToString() + " ";
            }
            str = str.Trim().Replace(" ", ",");

            string sqlstr = "select *  from " + TableName + " where Version='" + Version + "' And id in (" + str + ")";

            DataSet ds =  new DataLib.BLL.dbFunction().GetList(sqlstr, TableName);
            DataTable dt = new DataTable();
            if(ds.Tables.Count > 0)
            {
             dt = ds.Tables[0];
            }
            return dt;
        }

        public DataSet getDataSource(string strWhere)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@WhereSql", SqlDbType.VarChar,2000),
					};
            parameters[0].Value = strWhere;
            return DbHelperSQL.RunProcedure("xml_getTable", parameters, "ds");
        }

        public DataSet getDataSource(int ClassID)
        {
            string strWhere = " And TableID=" + ClassID.ToString();
            return getDataSource(strWhere);
        }
    }
}
