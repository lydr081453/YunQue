/*********************************************************
 * 类中文名称：员工离职操作类
 * 类详细描述：
 * 
 * 
 * 
 * 
 * 主要制作人：zhouqi
 ********************************************************/
using System;
using System.Data;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{    
    public class DimissionManager
    {
        private static ESP.HumanResource.DataAccess.DimissionDataProvider dal = new ESP.HumanResource.DataAccess.DimissionDataProvider();
        public DimissionManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该条离职记录
        /// </summary>
        public static bool Exists(int id)
        {
            return dal.Exists(id);
        }

       /// <summary>
       /// 添加一条离职记录
       /// </summary>
       /// <param name="model">离职信息</param>
       /// <param name="snapshots">员工历史信息</param>
       /// <param name="employeeBase">员工的基础信息</param>
       /// <param name="logModel">日志信息</param>
       /// <param name="isTrue">是否办完离职手续</param>
       /// <returns></returns>
        public static int Add(DimissionInfo model, SnapshotsInfo snapshots, EmployeeBaseInfo employeeBase, LogInfo logModel, bool isTrue)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //添加历史信息
                    returnValue = SnapshotsManager.Add(snapshots, trans);
                    //关联到离职信息
                    model.snapshotsId = returnValue;
                    returnValue = dal.Add(model, conn, trans);
                    if (isTrue)
                    {                        
                        employeeBase.Status = ESP.HumanResource.Common.Status.Dimission;
                        //使用户无法登陆
                        ESP.Framework.BusinessLogic.UserManager.Delete(model.userId);
                        EmployeeBaseManager.Update(employeeBase, trans);
                    }
                    else
                    {   //未办理完离职手续                     
                        employeeBase.Status = Status.WaitDimission;
                        EmployeeBaseManager.Update(employeeBase, trans);
                    }

                    EmployeeWelfareManager.Update(employeeBase.EmployeeWelfareInfo, trans);

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
        /// 更新一条离职信息
        /// </summary>
        /// <param name="model">离职信息</param>
        /// <param name="snapshots">员工历史信息</param>
        /// <param name="employeeBase">员工基础信息</param>
        /// <param name="logModel">日志信息</param>
        /// <param name="isTrue">是否办完离职手续</param>
        /// <returns></returns>
        public static int Update(DimissionInfo model, SnapshotsInfo snapshots, EmployeeBaseInfo employeeBase, LogInfo logModel, bool isTrue)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SnapshotsManager.Update(snapshots, trans);
                    returnValue = dal.Update(model, conn, trans);
                    if (isTrue)
                    {
                        employeeBase.Status = Status.Dimission;
                        EmployeeBaseManager.Update(employeeBase, trans);
                    }
                    else
                    {                        
                        employeeBase.Status = Status.WaitDimission;
                        EmployeeBaseManager.Update(employeeBase, trans);
                    }
                    EmployeeWelfareManager.Update(employeeBase.EmployeeWelfareInfo, trans);

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
        /// 简单更新一条离职信息数据
        /// </summary>
        public static int Update(DimissionInfo model, SqlTransaction trans)
        {
            return dal.Update(model,trans);
        }

        /// <summary>
        /// 通过id删除一条离职数据
        /// </summary>
        public static void Delete(int id, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Delete(id, conn, trans);
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
        /// 通过id得到一个离职信息的对象实体
        /// </summary>
        public static DimissionInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 通过用户ID得到一个离职信息对象实体
        /// </summary>
        public static DimissionInfo GetModelByUserID(int Userid)
        {
            return dal.GetModelByUserID(Userid);
        }



        /// <summary>
        /// 通过条件获得离职信息列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得所有离职信息的列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 通过条件获得降序的离职对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<DimissionInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetModelList(strWhere, parms);
        }

        /// <summary>
        /// 通过条件获得离职对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<DimissionInfo> GetModelList(string strWhere)
        {
            return dal.GetModelList(strWhere);
        }

        #endregion  成员方法
    }
}
