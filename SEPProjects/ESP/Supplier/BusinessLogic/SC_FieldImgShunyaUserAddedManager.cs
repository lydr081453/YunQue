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
    public class SC_FieldImgShunyaUserAddedManager
    {
        private static readonly SC_FieldImgShunyaUserAddedDataProvider dal = new SC_FieldImgShunyaUserAddedDataProvider();

        public static int Add(SC_FieldImgShunyaUserAdded model,ESP.Compatible.Employee user)
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
                    log.Des = user.Name + "添加了活动场地图片[" + returnValue + "]";
                    log.LogUserId = int.Parse(user.SysID);

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

        public static List<SC_FieldImgShunyaUserAdded> GetList(int fieldId)
        {
            return dal.GetList(fieldId);
        }

        public static int Delete(int id,ESP.Compatible.Employee user)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SC_Log log = new SC_Log();
                    log.CreatTime = log.LastUpdateTime = DateTime.Now;
                    log.Des = user.Name + "删除了活动场地图片[" + id + "]";
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
    }
}
