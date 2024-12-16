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
    public class SC_FieldBallRoomManager
    {
        private static readonly SC_FieldBallRoomDataProvider dal = new SC_FieldBallRoomDataProvider();

        public static int Add(SC_FieldBallRoom model, ESP.Compatible.Employee user)
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
                    log.Des = user.Name + "添加了BallRoom[" + model.BallRoomName + "]";
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

        public static void Update(SC_FieldBallRoom model, ESP.Compatible.Employee user)
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
                    log.Des = user.Name + "修改了BallRoom[" + model.BallRoomName + "]";

                    new SC_LogDataProvider().Add(log, trans, conn);
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
        }

        public static SC_FieldBallRoom GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public static List<SC_FieldBallRoom> GetList(int fieldId)
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
                    SC_FieldBallRoom model = dal.GetModel(id);

                    SC_Log log = new SC_Log();
                    log.CreatTime = log.LastUpdateTime = DateTime.Now;
                    log.Des = user.Name + "删除了BallRoom[" + model.BallRoomName + "-fieldId="+model.FieldId+"]";
                    log.LogUserId = int.Parse(user.SysID);

                    int returnValue = dal.Delete(id,trans);
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
