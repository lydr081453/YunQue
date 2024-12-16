using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_SupplierFieldManager
    {
        private static readonly SC_SupplierFieldDataProvider dal = new SC_SupplierFieldDataProvider();

        public static int Add(SC_SupplierField model,ESP.Compatible.Employee user)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int returnValue = dal.Add(model, trans);

                    SC_Log log = new SC_Log();
                    log.CreatTime = log.LastUpdateTime = DateTime.Now;
                    log.Des = user.Name + "添加了活动场地[" + model.FieldName+"]";
                    log.IpAddress = model.AddIp;
                    log.LogUserId = model.EditUserId;

                    new SC_LogDataProvider().Add(log, trans, conn);
                    trans.Commit();
                    return returnValue;
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static void Update(SC_SupplierField model, ESP.Compatible.Employee user)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);

                    SC_Log log = new SC_Log();
                    log.CreatTime = log.LastUpdateTime = DateTime.Now;
                    log.Des = user.Name + "修改了活动场地[" + model.FieldName + "]";
                    log.IpAddress = model.AddIp;
                    log.LogUserId = model.EditUserId;

                    new SC_LogDataProvider().Add(log, trans, conn);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    trans.Rollback();
                }
            }
        }

        public static SC_SupplierField GetModel(int fieldId)
        {
            return dal.GetModel(fieldId);
        }

        public static int Delete(int id, ESP.Compatible.Employee user)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SC_SupplierField model = dal.GetModel(id);

                    SC_Log log = new SC_Log();
                    log.CreatTime = log.LastUpdateTime = DateTime.Now;
                    log.Des = user.Name + "删除了活动场地[" + model.FieldName + "-fieldId=" + model.Id + "]";
                    log.LogUserId = int.Parse(user.SysID);

                    int returnValue = dal.Delete(id, trans);
                    new SC_LogDataProvider().Add(log, trans, conn);
                    trans.Commit();
                    return returnValue;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static List<SC_SupplierField> GetList(int supplierId)
        {
            return dal.GetList(supplierId);
        }

        public static DataTable GetSupplierIds()
        {
            return dal.GetSupplierIds();
        }
    }
}
