using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{    
    public class PromotionManager
    {
        private static PromotionDataProvider dal = new PromotionDataProvider();
        public PromotionManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(PromotionInfo model, SnapshotsInfo snapshots, EmployeesInPositionsInfo departments, EmployeesInPositionsInfo depsOld, LogInfo logModel, bool isCommit)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {                    
                    returnValue = SnapshotsManager.Add(snapshots, trans);
                    model.salaryDetailID = returnValue;
                    returnValue = dal.Add(model, conn, trans);
                    if (isCommit)
                    {
                        EmployeesInPositionsManager.Delete(depsOld.UserID, depsOld.DepartmentPositionID, depsOld.DepartmentID, trans);
                        if (EmployeesInPositionsManager.Add(departments, LogManager.GetLogModel(logModel.LogUserName + "因为职位调整修改了" + model.sysUserName + "的所属部门的职位", logModel.LogUserId, logModel.LogUserName, logModel.SysId, logModel.SysUserName, Status.Log)))
                            returnValue = 1;
                    }
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(PromotionInfo model, SnapshotsInfo snapshots, EmployeesInPositionsInfo departments, LogInfo logModel, bool isCommit)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Update(model, conn, trans);
                    LogManager.AddLog(logModel, trans);                   
                    SnapshotsManager.Update(snapshots, trans);
                    if (isCommit)
                    {

                        returnValue = EmployeesInPositionsManager.Update(departments, LogManager.GetLogModel(logModel.LogUserName + "因为职位调整修改了" + model.sysUserName + "的所属部门的职位", logModel.LogUserId, logModel.LogUserName, logModel.SysId, logModel.SysUserName, Status.Log));

                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(PromotionInfo model, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SnapshotsManager.Delete(model.salaryDetailID, trans);
                    dal.Delete(model.id, conn, trans);
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());

                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static PromotionInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        #endregion  成员方法
    }
}
