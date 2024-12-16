using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{    
    public class PassedManager
    {
        private static PassedDataProvider dal = new PassedDataProvider();
        public PassedManager()
        { }
        #region  成员方法
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(PassedInfo model, SnapshotsInfo snapshots, EmployeesInPositionsInfo departments, EmployeesInPositionsInfo oldempinpositions, LogInfo logModel, bool isTrue)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (isTrue)
                    { }
                    // salaryDetail.status = 1;
                    //  returnValue = ShunYaGroup.BLL.SEP_SalaryDetailInfo.Add(salaryDetail,  trans);
                    returnValue = SnapshotsManager.Add(snapshots, trans);
                    model.salaryDetailID = returnValue;

                    returnValue = dal.Add(model, conn, trans);
                    EmployeesInPositionsManager.Delete(oldempinpositions.UserID, oldempinpositions.DepartmentPositionID, oldempinpositions.DepartmentID, trans);
                    EmployeesInPositionsManager.Add(departments, LogManager.GetLogModel(logModel.LogUserName + "因为转正修改了" + model.sysUserName + "的所属部门", logModel.LogUserId, logModel.LogUserName, logModel.SysId, logModel.SysUserName, Status.Log), trans);
                    if (isTrue)
                        EmployeeBaseManager.updateStatus(model.sysid, Status.Passed, trans);
                    else
                        EmployeeBaseManager.updateStatus(model.sysid, Status.WaitPassed, trans);
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
        public static int Update(PassedInfo model, SnapshotsInfo snapshots, EmployeesInPositionsInfo departments, LogInfo logModel, bool isTrue)
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
                    EmployeesInPositionsManager.Update(departments, LogManager.GetLogModel(logModel.LogUserName + "因为转正修改了" + model.sysUserName + "的所属部门", logModel.LogUserId, logModel.LogUserName, logModel.SysId, logModel.SysUserName, Status.Log));
                    if (isTrue)
                        EmployeeBaseManager.updateStatus(model.sysid, Status.Passed, trans);
                    else
                        EmployeeBaseManager.updateStatus(model.sysid, Status.WaitPassed, trans);
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
        public static void Delete(PassedInfo model, LogInfo logModel)
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
        public static PassedInfo GetModel(int id)
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

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<PassedInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetModelList(strWhere, parms);
        }


        #endregion  成员方法
    }
}
