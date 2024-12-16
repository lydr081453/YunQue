using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Supplier.Common;


namespace ESP.Supplier.DataAccess
{
    public class SC_SupplierprotocolTypeProvider
    {
        public bool insertInfos(string[] supplierAndTypeIds, int supplierType,int userId)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    for (int i = 0; i < supplierAndTypeIds.Length; i++)
                    {
                        string delSql = "delete sc_supplierprotocolType where supplierId=" + supplierAndTypeIds[i].Split('-')[0] + " and typeId=" + supplierAndTypeIds[i].Split('-')[1];
                        string insertSql = "insert sc_supplierprotocolType values('" + supplierAndTypeIds[i].Split('-')[0] + "','" + supplierAndTypeIds[i].Split('-')[1] + "','" + supplierType + "','" + userId + "','" + DateTime.Now + "')";
                        DbHelperSQL.ExecuteSql(delSql);//删除已设置的供应商物料类别
                        DbHelperSQL.ExecuteSql(insertSql);//添加
                    }
                    trans.Commit();
                    return true;
                }
                catch {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public int GetListForSupplier(int supplierid, int typeid)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                try
                {
                    string insertSql = "select count(*) from sc_supplierprotocolType where  suppliertype=1 and supplierId=" + supplierid + " and supplierType=" + typeid;
                    DataTable dt= DbHelperSQL.Query(insertSql).Tables[0];
                    return int.Parse(dt.Rows[0][0].ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }


        public int GetListForSupplier(int supplierid)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                try
                {
                    string insertSql = "select count(*) from sc_supplierprotocolType where suppliertype=1 and supplierId=" + supplierid  ;
                    DataTable dt = DbHelperSQL.Query(insertSql).Tables[0];
                    return int.Parse(dt.Rows[0][0].ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}
