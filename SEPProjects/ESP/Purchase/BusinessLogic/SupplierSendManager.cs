using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;

namespace ESP.Purchase.BusinessLogic
{
    public class SupplierSendManager
    {
        public static void Add(Entity.SupplierSendInfo model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DataAccess.SupplierSendDataProvider.Delete(model.DataId, model.DataType, trans);
                    DataAccess.SupplierSendDataProvider.Add(model, trans);
                    trans.Commit();
                }
                catch { trans.Rollback(); }
            }
        }

        public static int Delete(int dataId, int dataType)
        {
            return DataAccess.SupplierSendDataProvider.Delete(dataId, dataType);
        }

        public static Entity.SupplierSendInfo GetModel(int dataId, int dataType)
        {
            return DataAccess.SupplierSendDataProvider.GetModel(dataId, dataType);
        }
    }
}
