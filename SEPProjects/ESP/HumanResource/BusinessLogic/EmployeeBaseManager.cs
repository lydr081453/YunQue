/*********************************************************
 * 类中文名称：员工信息操作类
 * 类详细描述：
 * 
 * 
 * 
 * 
 * 主要制作人：zhouqi
 ********************************************************/
using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class EmployeeBaseManager
    {
        private static EmployeeBaseDataProvider dal = new EmployeeBaseDataProvider();

        private static UsersDataProvider usersDal = new UsersDataProvider();

        public EmployeeBaseManager()
        { }

        #region  成员方法

        /// <summary>
        /// 通过用户名和用户ID判断是否有该用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool checkUserCodeExists(string username, int userid)
        {
            return dal.checkUserCodeExists(username, userid);
        }

        /// <summary>
        /// 通过用户名判断是否有该用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool checkUserCodeExists(string username)
        {
            return dal.checkUserCodeExists(username);
        }

        /// <summary>
        /// 添加一条员工信息
        /// </summary>
        /// <param name="userModel">员工登陆所需信息</param>
        /// <param name="empModel">员工基本信息</param>
        /// <param name="depsModel">员工部门职位信息</param>
        /// <param name="logModel">日志信息</param>
        /// <returns></returns>
        public static int Add(UsersInfo userModel, EmployeeBaseInfo empModel, EmployeesInPositionsInfo depsModel, LogInfo logModel)
        {
            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + empModel.IDNumber + "' and a.idnumber<>''");
            if (ds.Tables[0].Rows.Count > 0)
                return 0;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        int userID = usersDal.Add(userModel, trans);
                        empModel.UserID = userID;
                        empModel.EmployeeWelfareInfo.sysid = userID;
                        empModel.EmployeeJobInfo.sysid = userID;
                        depsModel.UserID = userID;

                        dal.Add(empModel, trans);
                        //添加员工工资、合同信息
                        EmployeeJobManager.Add(empModel.EmployeeJobInfo, trans);
                        //添加员工社保信息
                        EmployeeWelfareManager.Add(empModel.EmployeeWelfareInfo, trans);
                        EmployeesInPositionsManager.Add(depsModel, trans);
                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return userID;
                    }
                    catch (Exception ex)
                    {
                        ESP.Logging.Logger.Add(ex.ToString());
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 添加一条带历史记录的员工信息
        /// </summary>
        /// <param name="empModel">员工基本信息</param>
        /// <param name="userModel">员工登陆信息</param>
        /// <param name="depsModel">员工部门职位信息</param>
        /// <param name="snapshots">员工历史信息</param>
        /// <param name="logModel">日志信息</param>
        /// <returns></returns>
        public static bool Add(EmployeeBaseInfo empModel, UsersInfo userModel, EmployeesInPositionsInfo depsModel, SnapshotsInfo snapshots, LogInfo logModel)
        {
            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + empModel.IDNumber + "' and a.idnumber<>''");
            if (ds.Tables[0].Rows.Count > 0)
                return false;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        int userID = usersDal.Add(userModel, trans);
                        empModel.UserID = userID;
                        empModel.EmployeeWelfareInfo.sysid = userID;
                        empModel.EmployeeJobInfo.sysid = userID;
                        depsModel.UserID = userID;
                        snapshots.UserID = userID;

                        dal.Add(empModel, trans);
                        EmployeeJobManager.Add(empModel.EmployeeJobInfo, trans);
                        EmployeeWelfareManager.Add(empModel.EmployeeWelfareInfo, trans);
                        EmployeesInPositionsManager.Add(depsModel, trans);
                        SnapshotsManager.Add(snapshots, trans);
                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        ESP.Logging.Logger.Add(ex.ToString());
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 离职人员重新入职
        /// </summary>
        public static bool Add(EmployeeBaseInfo empModel, UsersInfo userModel, UsersInfo olduser, EmployeesInPositionsInfo depsModel, SnapshotsInfo snapshots, DimissionInfo diminfo, LogInfo logModel)
        {

            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + empModel.IDNumber + "' and a.idnumber<>'' and a.userid<>" + empModel.UserID.ToString());
            if (ds.Tables[0].Rows.Count > 0)
                return false;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        int userID = usersDal.Add(userModel, trans);
                        empModel.UserID = userID;
                        empModel.EmployeeWelfareInfo.sysid = userID;
                        empModel.EmployeeJobInfo.sysid = userID;
                        depsModel.UserID = userID;
                        depsModel.BeginDate = DateTime.Now;
                        snapshots.UserID = userID;

                        dal.Add(empModel, trans);
                        EmployeeJobManager.Add(empModel.EmployeeJobInfo, trans);
                        EmployeeWelfareManager.Add(empModel.EmployeeWelfareInfo, trans);
                        EmployeesInPositionsManager.Add(depsModel, trans);
                        SnapshotsManager.Add(snapshots, trans);

                        usersDal.Update(olduser);
                        DimissionManager.Update(diminfo, trans);


                        LogManager.AddLog(logModel, trans);

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        ESP.Logging.Logger.Add(ex.ToString());
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新员工信息为已发送入职邮件
        /// </summary>
        public static void Update(EmployeeBaseInfo model)
        {

            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + model.IDNumber + "' and a.idnumber<>'' and a.status<>5 and a.userid <>" + model.UserID.ToString());
            if (ds.Tables[0].Rows.Count > 0)
                throw new Exception("身份证号有重复");
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        dal.Update(model, trans);
                        ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(model.UserID);
                        if (user != null && user.Email != model.InternalEmail)
                        {
                            user.Email = model.InternalEmail;
                            new ESP.HumanResource.DataAccess.UsersDataProvider().Update(user, trans);
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 更新一条员工完整信息
        /// </summary>
        /// <param name="userModel">员工登陆信息</param>
        /// <param name="model">员工基本信息</param>
        /// <param name="deps">员工部门职位信息</param>
        /// <param name="depsOld">旧员工部门职位信息</param>
        /// <param name="snapshots">员工历史信息</param>
        /// <param name="logModel">日志信息</param>
        /// <returns></returns>
        public static bool Update(UsersInfo userModel, EmployeeBaseInfo model, EmployeesInPositionsInfo deps, EmployeesInPositionsInfo depsOld, SnapshotsInfo snapshots, LogInfo logModel)
        {

            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + model.IDNumber + "' and a.idnumber<>'' and a.userid <>" + model.UserID.ToString());
            if (ds.Tables[0].Rows.Count > 0)
                return false;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        usersDal.Update(userModel, trans);
                        dal.Update(model, trans);
                        EmployeeJobManager.Update(model.EmployeeJobInfo, trans);
                        EmployeeWelfareManager.Update(model.EmployeeWelfareInfo, trans);
                        EmployeesInPositionsManager.Delete(depsOld.UserID, depsOld.DepartmentPositionID, depsOld.DepartmentID, trans);
                        EmployeesInPositionsManager.Add(deps, trans);
                        snapshots.UserID = model.UserID;
                        SnapshotsManager.Add(snapshots, trans);
                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.ToString());
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新一条员工完整信息
        /// </summary>
        /// <param name="userModel">员工登陆信息</param>
        /// <param name="model">员工基本信息</param>
        /// <param name="deps">员工部门职位信息</param>
        /// <param name="depsOld">旧员工部门职位信息</param>
        /// <param name="snapshots">员工历史信息</param>
        /// <param name="logModel">日志信息</param>
        /// <returns></returns>
        public static bool Update(UsersInfo userModel, EmployeeBaseInfo model, ESP.HumanResource.Entity.LogInfo logModel)
        {

            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + model.IDNumber + "' and a.idnumber<>'' and a.status in(1,3) and a.userid <>" + model.UserID.ToString());
            if (ds.Tables[0].Rows.Count > 0)
                return false;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        usersDal.Update(userModel, trans);
                        dal.Update(model, trans);
                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.ToString());
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新一条不带历史的员工信息
        /// </summary>
        /// <param name="userModel">员工登陆信息</param>
        /// <param name="model">员工基本信息</param>
        /// <param name="deps">员工部门职位信息</param>
        /// <param name="depsOld">旧员工部门职位信息</param>
        /// <param name="logModel">日志信息</param>
        /// <returns></returns>
        public static bool Update(UsersInfo userModel, EmployeeBaseInfo model, EmployeesInPositionsInfo deps, EmployeesInPositionsInfo depsOld, LogInfo logModel)
        {
            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + model.IDNumber + "' and a.idnumber<>'' and a.userid <>" + model.UserID.ToString());
            var telList = TelManager.GetModelList(" and tel ='" + model.Phone1 + "'");
            if (ds.Tables[0].Rows.Count > 0)
                return false;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        usersDal.Update(userModel, trans);
                        dal.Update(model, trans);
                        EmployeeJobManager.Update(model.EmployeeJobInfo, trans);
                        EmployeeWelfareManager.Update(model.EmployeeWelfareInfo, trans);

                        if (telList != null && telList.Count > 0)
                        {
                            var telModel = telList[0];
                            telModel.Status = 0;
                            ESP.HumanResource.BusinessLogic.TelManager.Update(telModel, trans);
                        }
                        EmployeesInPositionsManager.Delete(depsOld.UserID, depsOld.DepartmentPositionID, depsOld.DepartmentID, trans);
                        EmployeesInPositionsManager.Add(deps, trans);
                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 更新一条员工及其部门职位信息
        /// </summary>
        /// <param name="model">员工基本信息</param>
        /// <param name="deps">员工部门职位信息</param>
        /// <param name="depsOld">旧员工部门职位信息</param>
        /// <param name="logModel">日志信息</param>
        /// <returns></returns>
        public static bool Update(EmployeeBaseInfo model, EmployeesInPositionsInfo deps, EmployeesInPositionsInfo depsOld, LogInfo logModel)
        {
            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + model.IDNumber + "' and a.idnumber<>'' and a.userid <>" + model.UserID.ToString());
            if (ds.Tables[0].Rows.Count > 0)
                return false;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        dal.Update(model, trans);
                        EmployeeJobManager.Update(model.EmployeeJobInfo, trans);
                        EmployeeWelfareManager.Update(model.EmployeeWelfareInfo, trans);
                        EmployeesInPositionsManager.Delete(depsOld.UserID, depsOld.DepartmentPositionID, depsOld.DepartmentID, trans);
                        EmployeesInPositionsManager.Add(deps, trans);
                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.ToString());
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新一条员工的基本信息
        /// </summary>
        /// <param name="model">员工信息</param>
        /// <param name="logModel">日志信息</param>
        /// <returns></returns>
        public static int Update(EmployeeBaseInfo model, LogInfo logModel)
        {
            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + model.IDNumber + "' and a.idnumber<>'' and a.userid <>" + model.UserID.ToString());
            if (ds.Tables[0].Rows.Count > 0)
                return 0;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        dal.Update(model, trans);
                        if (logModel != null)
                        {
                            LogManager.AddLog(logModel, trans);
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.ToString());
                        return 0;
                    }
                    return 1;
                }
            }
        }

        /// <summary>
        /// 更新一条数据的保险信息
        /// </summary>
        public static int Update(List<EmployeeBaseInfo> list, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {

                    foreach (EmployeeBaseInfo emp in list)
                    {
                        dal.Update(emp, trans);
                        EmployeeWelfareManager.Update(emp.EmployeeWelfareInfo, trans);
                        SnapshotsManager.Add(emp.EmployeesCurrentSnapshot, trans);
                    }
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    return 0;
                }
                return 1;
            }
        }

        /// <summary>
        /// 待入职员工入职
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public static bool UpdateReadyJobStatus(EmployeeBaseInfo model, UsersInfo user, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model.TypeID != 4)
                    {
                        model.Status = Status.Entry;
                        user.Status = Status.Entry;
                    }
                    else
                    {
                        model.Status = Status.Fieldword;
                        user.Status = Status.Fieldword;
                    }

                    user.IsApproved = true;
                    dal.Update(model, trans);
                    UsersManager.Update(user, trans);
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 待入职员工入职
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <param name="snap"></param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public static bool UpdateReadyJobStatus(EmployeeBaseInfo model, UsersInfo user, SnapshotsInfo snap, LogInfo logModel)
        {
            //TELInfo tel = null;
            //if (!string.IsNullOrEmpty(model.Phone1))
            //{
            //    var list = TelManager.GetModelList(" and tel='" + model.Phone1 + "'");
            //    if (list != null && list.Count > 0)
            //    {
            //        tel = list[0];
            //    }
            //    if (tel != null)
            //        tel.Status = 0;
            //}

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model.TypeID != 4)
                    {
                        model.Status = Status.Entry;
                        user.Status = Status.Entry;
                        snap.Status = Status.Entry;
                        user.IsApproved = true;
                        new UsersDataProvider().AddRoleByUserid(user.UserID, conn, trans);
                    }
                    else
                    {
                        model.Status = Status.Fieldword;
                        user.Status = Status.Fieldword;
                        snap.Status = Status.Fieldword;

                    }

                    dal.Update(model, trans);
                    EmployeeWelfareManager.Update(model.EmployeeWelfareInfo,trans);
                    SnapshotsManager.Add(snap, trans);
                    UsersManager.Update(user, trans);
                    LogManager.AddLog(logModel, trans);
                    //if (tel != null)
                    //{
                    //    TelManager.Update(tel, trans);
                    //}
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 更新一条员工基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <param name="snapshots"></param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public static bool Update(EmployeeBaseInfo model, UsersInfo user, SnapshotsInfo snapshots, LogInfo logModel)
        {
            //DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + model.IDNumber + "' and a.status in(1,3) and a.userid <>" + model.UserID.ToString());
            //if (ds.Tables[0].Rows.Count > 0)
            //    return false;
            //else
            //{

                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        dal.Update(model, trans);
                        UsersManager.Update(user, trans);
                        if (model.EmployeeJobInfo.sysid > 0)
                        {
                            EmployeeJobManager.Update(model.EmployeeJobInfo, trans);
                        }
                        else
                        {
                            model.EmployeeJobInfo.sysid = model.UserID;
                            EmployeeJobManager.Add(model.EmployeeJobInfo, trans);
                        }
                        if (model.EmployeeWelfareInfo.sysid > 0)
                        {
                            EmployeeWelfareManager.Update(model.EmployeeWelfareInfo, trans);
                        }
                        else
                        {
                            model.EmployeeWelfareInfo.sysid = model.UserID;
                            EmployeeWelfareManager.Add(model.EmployeeWelfareInfo, trans);
                        }

                        LogManager.AddLog(logModel, trans);
                        new ESP.Administrative.BusinessLogic.ALAndRLManager().Update(null, trans.Connection, trans);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.ToString());
                        return false;
                    }
                    return true;
                }
           // }
        }

        public DataSet GetEmployeeList(string strWhere)
        {

            return dal.GetEmployeeList(strWhere);
        }

        /// <summary>
        /// 更新一条员工基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <param name="snapshots"></param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public static bool Update(EmployeeBaseInfo model, UsersInfo user, SnapshotsInfo snapshots, LogInfo logModel, ESP.Administrative.Entity.ALAndRLInfo alandrlModel, ESP.Administrative.Entity.UserAttBasicInfo userAttBasicModel)
        {
            DataSet ds = dal.GetEmployeeList(" and a.idnumber='" + model.IDNumber + "' and a.idnumber<>'' and a.userid <>" + model.UserID.ToString());
            if (ds.Tables[0].Rows.Count > 0)
                return false;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        dal.Update(model, trans);
                        UsersManager.Update(user, trans);
                        if (model.EmployeeJobInfo.sysid > 0)
                        {
                            EmployeeJobManager.Update(model.EmployeeJobInfo, trans);
                        }
                        else
                        {
                            model.EmployeeJobInfo.sysid = model.UserID;
                            EmployeeJobManager.Add(model.EmployeeJobInfo, trans);
                        }
                        if (model.EmployeeWelfareInfo.sysid > 0)
                        {
                            EmployeeWelfareManager.Update(model.EmployeeWelfareInfo, trans);
                        }
                        else
                        {
                            model.EmployeeWelfareInfo.sysid = model.UserID;
                            EmployeeWelfareManager.Add(model.EmployeeWelfareInfo, trans);
                        }
                        if (snapshots.id > 0)
                        {
                            SnapshotsManager.Add(snapshots, trans);
                        }
                        else
                        {
                            snapshots.UserID = model.UserID;
                            SnapshotsManager.Add(snapshots, trans);
                        }
                        LogManager.AddLog(logModel, trans);
                        if (alandrlModel != null)
                            new ESP.Administrative.BusinessLogic.ALAndRLManager().Update(alandrlModel, trans.Connection, trans);
                        if (userAttBasicModel != null)
                            new ESP.Administrative.DataAccess.UserAttBasicDataProvider().Update(userAttBasicModel, trans.Connection, trans);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.ToString());
                        return false;
                    }
                    return true;
                }
            }
        }

        /// <summary>
        /// 通过员工ID删除一条员工记录(逻辑删除)
        /// </summary>
        public static bool Delete(int sysid, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //  EmployeeJobManager.DeleteBySysUserID(sysid, trans);
                    //  EmployeeWelfareManager.DeleteBySysUserID(sysid, trans);
                    //    dal.Delete(sysid, trans);  

                    EmployeeBaseInfo info = dal.GetModel(sysid);
                    info.Status = Status.IsDeleted;
                    Update(info);
                    if (logModel != null)
                    {
                        LogManager.AddLog(logModel, trans);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    return false;
                }
                return true;
            }
        }


        public static void Delete(int sysid)
        {
            dal.Delete(sysid, null);
        }

        /// <summary>
        /// 通过员工ID得到一条员工信息
        /// </summary>
        public static EmployeeBaseInfo GetModel(int sysid)
        {
            return dal.GetModel(sysid);
        }

        /// <summary>
        /// 用户Code得到一个对象实体
        /// </summary>
        public static EmployeeBaseInfo GetModel(string Code)
        {
            return dal.GetModel(Code);
        }


        public static int GetModelByMobile(string mobile)
        {
            return dal.GetModelByMobile(mobile);
        }

        /// <summary>
        /// 通过条件获得员工的数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 通过条件获得员工的数据列表
        /// </summary>
        public static DataSet GetListForExport(string strWhere)
        {
            return dal.GetListForExport(strWhere);
        }


        public static DataSet GetListForExpress(string strWhere)
        {
            return dal.GetListForExpress(strWhere);
        }

        public static DataSet GetUserListYesWeCan()
        {
            return dal.GetUserListYesWeCan();
        }

        /// <summary>
        /// 通过条件获得员工的数据列表（速度慢，通过用户ID，循环查询数据库员工关联信息）
        /// </summary>
        public static List<EmployeeBaseInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<EmployeeBaseInfo> modelList = new List<EmployeeBaseInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                EmployeeBaseInfo model;
                EmployeeJobInfo jobModel;
                EmployeeWelfareInfo welfModel;

                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EmployeeBaseInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                        jobModel = EmployeeJobManager.getModelBySysId(model.UserID);
                        model.EmployeeJobInfo = jobModel;
                        welfModel = EmployeeWelfareManager.getModelBySysId(model.UserID);
                        model.EmployeeWelfareInfo = welfModel;

                        
                    }

                    model.IM = ds.Tables[0].Rows[n]["IM"].ToString();
                    model.EmergencyContact = ds.Tables[0].Rows[n]["EmergencyContact"].ToString();
                    model.EmergencyContactPhone = ds.Tables[0].Rows[n]["EmergencyContactPhone"].ToString();
                    model.Address = ds.Tables[0].Rows[n]["Address"].ToString();
                    model.City = ds.Tables[0].Rows[n]["City"].ToString();
                    model.Province = ds.Tables[0].Rows[n]["Province"].ToString();
                    model.Country = ds.Tables[0].Rows[n]["Country"].ToString();
                    model.PostCode = ds.Tables[0].Rows[n]["PostCode"].ToString();
                    model.Address2 = ds.Tables[0].Rows[n]["Adress2"].ToString();
                    model.City2 = ds.Tables[0].Rows[n]["City2"].ToString();
                    model.Code = ds.Tables[0].Rows[n]["Code"].ToString();
                    model.Province2 = ds.Tables[0].Rows[n]["Province2"].ToString();
                    model.Country2 = ds.Tables[0].Rows[n]["Country2"].ToString();
                    model.PostCode2 = ds.Tables[0].Rows[n]["PostCode2"].ToString();
                    model.WorkAddress = ds.Tables[0].Rows[n]["WorkAddress"].ToString();
                    model.WorkCity = ds.Tables[0].Rows[n]["WorkCity"].ToString();
                    model.WorkProvince = ds.Tables[0].Rows[n]["WorkProvince"].ToString();
                    model.WorkCountry = ds.Tables[0].Rows[n]["WorkCountry"].ToString();
                    model.WorkPostCode = ds.Tables[0].Rows[n]["WorkPostCode"].ToString();
                    if (ds.Tables[0].Rows[n]["MaritalStatus"].ToString() != "")
                    {
                        model.MaritalStatus =  int.Parse(ds.Tables[0].Rows[n]["MaritalStatus"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Gender"].ToString() != "")
                    {
                        model.Gender =  int.Parse(ds.Tables[0].Rows[n]["Gender"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["TypeID"].ToString() != "")
                    {
                        model.TypeID = int.Parse(ds.Tables[0].Rows[n]["TypeID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(ds.Tables[0].Rows[n]["Birthday"].ToString());
                    }
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();
                    model.BirthPlace = ds.Tables[0].Rows[n]["BirthPlace"].ToString();
                    model.IDNumber = ds.Tables[0].Rows[0]["IDNumber"].ToString();
                    model.DomicilePlace = ds.Tables[0].Rows[n]["DomicilePlace"].ToString();
                    model.Photo = ds.Tables[0].Rows[n]["Photo"].ToString();
                    model.Degree = ds.Tables[0].Rows[n]["Degree"].ToString();
                    model.Education = ds.Tables[0].Rows[n]["Education"].ToString();
                    model.GraduateFrom = ds.Tables[0].Rows[n]["GraduatedFrom"].ToString();
                    model.Major = ds.Tables[0].Rows[n]["Major"].ToString();
                    if (ds.Tables[0].Rows[n]["GraduatedDate"].ToString() != "")
                    {
                        model.GraduatedDate = DateTime.Parse(ds.Tables[0].Rows[n]["GraduatedDate"].ToString());
                    }
                    model.Health = ds.Tables[0].Rows[n]["Health"].ToString();
                    model.Phone1 = ds.Tables[0].Rows[n]["Phone1"].ToString();
                    model.DiseaseInSixMonths = ds.Tables[0].Rows[n]["DiseaseInSixMonths"].ToString();
                    model.DiseaseInSixMonthsInfo = ds.Tables[0].Rows[n]["DiseaseInSixMonthsInfo"].ToString();
                    model.WorkExperience = ds.Tables[0].Rows[n]["WorkExperience"].ToString();
                    model.WorkSpecialty = ds.Tables[0].Rows[n]["WorkSpecialty"].ToString();
                    model.ThisYearSalary = ds.Tables[0].Rows[n]["ThisYearSalary"].ToString();

                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    model.Memo = ds.Tables[0].Rows[n]["Memo"].ToString();
                    if (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.BaseInfoOK = true;
                        }
                        else
                        {
                            model.BaseInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ContractInfoOK = true;
                        }
                        else
                        {
                            model.ContractInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.InsuranceInfoOK = true;
                        }
                        else
                        {
                            model.InsuranceInfoOK = false;
                        }
                    }
                    model.Phone2 = ds.Tables[0].Rows[n]["Phone2"].ToString();
                    if (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ArchiveInfoOK = true;
                        }
                        else
                        {
                            model.ArchiveInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Creator"].ToString() != "")
                    {
                        model.Creator = int.Parse(ds.Tables[0].Rows[n]["Creator"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreatedTime"].ToString() != "")
                    {
                        model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[n]["CreatedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifier"].ToString() != "")
                    {
                        model.LastModifier = int.Parse(ds.Tables[0].Rows[n]["LastModifier"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifiedTime"].ToString() != "")
                    {
                        model.LastModifiedTime = DateTime.Parse(ds.Tables[0].Rows[n]["LastModifiedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["RowVersion"].ToString() != "")
                    {
                        model.RowVersion = (byte[])ds.Tables[0].Rows[n]["RowVersion"];
                    }
                    model.MobilePhone = ds.Tables[0].Rows[n]["MobilePhone"].ToString();
                    model.HomePhone = ds.Tables[0].Rows[n]["HomePhone"].ToString();
                    model.Fax = ds.Tables[0].Rows[n]["Fax"].ToString();
                    model.InternalEmail = ds.Tables[0].Rows[n]["InternalEmail"].ToString();

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Resume = ds.Tables[0].Rows[n]["Resume"].ToString();
                    if (ds.Tables[0].Rows[n]["IsForeign"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsForeign"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsForeign"].ToString().ToLower() == "true"))
                        {
                            model.IsForeign = true;
                        }
                        else
                        {
                            model.IsForeign = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsCertification"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsCertification"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsCertification"].ToString().ToLower() == "true"))
                        {
                            model.IsCertification = true;
                        }
                        else
                        {
                            model.IsCertification = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["WageMonths"].ToString() != "")
                    {
                        model.WageMonths = int.Parse(ds.Tables[0].Rows[n]["WageMonths"].ToString());
                    }
                    model.IPCode = ds.Tables[0].Rows[n]["IPCode"].ToString();
                    if (ds.Tables[0].Rows[n]["IsSendMail"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsSendMail"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsSendMail"].ToString().ToLower() == "true"))
                        {
                            model.IsSendMail = true;
                        }
                        else
                        {
                            model.IsSendMail = false;
                        }
                    }
                    model.CommonName = ds.Tables[0].Rows[n]["CommonName"].ToString();
                    if (ds.Tables[0].Rows[n]["DimissionStatus"] != DBNull.Value)
                        model.DimissionStatus = int.Parse(ds.Tables[0].Rows[n]["DimissionStatus"].ToString());
                    else
                        model.DimissionStatus = 0;
                    model.PrivateEmail = ds.Tables[0].Rows[n]["PrivateEmail"].ToString();
                    if (ds.Tables[0].Rows[n]["OwnedPC"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["OwnedPC"].ToString() == "1") || (ds.Tables[0].Rows[n]["OwnedPC"].ToString().ToLower() == "true"))
                        {
                            model.OwnedPC = true;
                        }
                        else
                        {
                            model.OwnedPC = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString() != "")
                    {
                        model.OfferLetterTemplate = int.Parse(ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterSender"].ToString() != "")
                    {
                        model.OfferLetterSender = int.Parse(ds.Tables[0].Rows[n]["OfferLetterSender"].ToString());
                    }
                    var objOfferLetterSendTime = ds.Tables[0].Rows[n]["OfferLetterSendTime"];
                    if (!(objOfferLetterSendTime is DBNull))
                    {
                        model.OfferLetterSendTime = (DateTime)objOfferLetterSendTime;
                    }
                    if (ds.Tables[0].Rows[n]["IsExamen"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsExamen"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsExamen"].ToString().ToLower() == "true"))
                        {
                            model.IsExamen = true;
                        }
                        else
                        {
                            model.IsExamen = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Seniority"].ToString() != "")
                    {
                        model.Seniority = int.Parse(ds.Tables[0].Rows[n]["Seniority"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["workBegin"].ToString() != "")
                    {
                        model.WorkBegin = DateTime.Parse(ds.Tables[0].Rows[n]["workBegin"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["JoinDate"].ToString() != "")
                    {
                        model.JoinDate = DateTime.Parse(ds.Tables[0].Rows[n]["JoinDate"].ToString());
                    }

                    model.BranchCode = ds.Tables[0].Rows[n]["BranchCode"].ToString();

                    if (ds.Tables[0].Rows[n]["ContractYear"].ToString() != "")
                    {
                        model.ContractYear = int.Parse(ds.Tables[0].Rows[n]["ContractYear"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ContractBeginDate"].ToString() != "")
                    {
                        model.ContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractBeginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ContractEndDate"].ToString() != "")
                    {
                        model.ContractEndDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractEndDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString() != "")
                    {
                        model.FirstContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString() != "")
                    {
                        model.FirstContractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["ContractSignDate"].ToString() != "")
                    {
                        model.ContractSignDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractSignDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString() != "")
                    {
                        model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["ProbationDate"].ToString() != "")
                    {
                        model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[0]["ProbationDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["SocialSecurityType"].ToString() != "")
                    {
                        model.SocialSecurityType = int.Parse(ds.Tables[0].Rows[n]["SocialSecurityType"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString() != "")
                    {
                        model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ProbationDate"].ToString() != "")
                    {
                        model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[n]["ProbationDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["HasChild"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["HasChild"].ToString() == "1") || (ds.Tables[0].Rows[n]["HasChild"].ToString().ToLower() == "true"))
                        {
                            model.HasChild = true;
                        }
                        else
                        {
                            model.HasChild = false;
                        }
                    }

                    model.SalaryBank = ds.Tables[0].Rows[n]["SalaryBank"].ToString();
                    model.SalaryCardNo = ds.Tables[0].Rows[n]["SalaryCardNo"].ToString();


                    if (ds.Tables[0].Rows[n]["Pay"].ToString() != "")
                    {
                        model.Pay = decimal.Parse(ds.Tables[0].Rows[n]["Pay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Performance"].ToString() != "")
                    {
                        model.Performance = decimal.Parse(ds.Tables[0].Rows[n]["Performance"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Attendance"].ToString() != "")
                    {
                        model.Attendance = decimal.Parse(ds.Tables[0].Rows[n]["Attendance"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["IDValid"].ToString() != "")
                    {
                        model.IDValid = DateTime.Parse(ds.Tables[0].Rows[n]["IDValid"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public static List<EmployeeBaseInfo> GetModelList(string strWhere, string server)
        {
            DataSet ds = dal.GetList(strWhere, server);
            List<EmployeeBaseInfo> modelList = new List<EmployeeBaseInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                EmployeeBaseInfo model;
                EmployeeJobInfo jobModel;
                EmployeeWelfareInfo welfModel;

                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EmployeeBaseInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                        jobModel = EmployeeJobManager.getModelBySysId(model.UserID);
                        model.EmployeeJobInfo = jobModel;
                        welfModel = EmployeeWelfareManager.getModelBySysId(model.UserID);
                        model.EmployeeWelfareInfo = welfModel;


                    }

                    model.IM = ds.Tables[0].Rows[n]["IM"].ToString();
                    model.EmergencyContact = ds.Tables[0].Rows[n]["EmergencyContact"].ToString();
                    model.EmergencyContactPhone = ds.Tables[0].Rows[n]["EmergencyContactPhone"].ToString();
                    model.Address = ds.Tables[0].Rows[n]["Address"].ToString();
                    model.City = ds.Tables[0].Rows[n]["City"].ToString();
                    model.Province = ds.Tables[0].Rows[n]["Province"].ToString();
                    model.Country = ds.Tables[0].Rows[n]["Country"].ToString();
                    model.PostCode = ds.Tables[0].Rows[n]["PostCode"].ToString();
                    model.Address2 = ds.Tables[0].Rows[n]["Adress2"].ToString();
                    model.City2 = ds.Tables[0].Rows[n]["City2"].ToString();
                    model.Code = ds.Tables[0].Rows[n]["Code"].ToString();
                    model.Province2 = ds.Tables[0].Rows[n]["Province2"].ToString();
                    model.Country2 = ds.Tables[0].Rows[n]["Country2"].ToString();
                    model.PostCode2 = ds.Tables[0].Rows[n]["PostCode2"].ToString();
                    model.WorkAddress = ds.Tables[0].Rows[n]["WorkAddress"].ToString();
                    model.WorkCity = ds.Tables[0].Rows[n]["WorkCity"].ToString();
                    model.WorkProvince = ds.Tables[0].Rows[n]["WorkProvince"].ToString();
                    model.WorkCountry = ds.Tables[0].Rows[n]["WorkCountry"].ToString();
                    model.WorkPostCode = ds.Tables[0].Rows[n]["WorkPostCode"].ToString();
                    if (ds.Tables[0].Rows[n]["MaritalStatus"].ToString() != "")
                    {
                        model.MaritalStatus = int.Parse(ds.Tables[0].Rows[n]["MaritalStatus"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Gender"].ToString() != "")
                    {
                        model.Gender = int.Parse(ds.Tables[0].Rows[n]["Gender"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["TypeID"].ToString() != "")
                    {
                        model.TypeID = int.Parse(ds.Tables[0].Rows[n]["TypeID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(ds.Tables[0].Rows[n]["Birthday"].ToString());
                    }
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();
                    model.BirthPlace = ds.Tables[0].Rows[n]["BirthPlace"].ToString();
                    model.IDNumber = ds.Tables[0].Rows[0]["IDNumber"].ToString();
                    model.DomicilePlace = ds.Tables[0].Rows[n]["DomicilePlace"].ToString();
                    model.Photo = ds.Tables[0].Rows[n]["Photo"].ToString();
                    model.Degree = ds.Tables[0].Rows[n]["Degree"].ToString();
                    model.Education = ds.Tables[0].Rows[n]["Education"].ToString();
                    model.GraduateFrom = ds.Tables[0].Rows[n]["GraduatedFrom"].ToString();
                    model.Major = ds.Tables[0].Rows[n]["Major"].ToString();
                    if (ds.Tables[0].Rows[n]["GraduatedDate"].ToString() != "")
                    {
                        model.GraduatedDate = DateTime.Parse(ds.Tables[0].Rows[n]["GraduatedDate"].ToString());
                    }
                    model.Health = ds.Tables[0].Rows[n]["Health"].ToString();
                    model.Phone1 = ds.Tables[0].Rows[n]["Phone1"].ToString();
                    model.DiseaseInSixMonths = ds.Tables[0].Rows[n]["DiseaseInSixMonths"].ToString();
                    model.DiseaseInSixMonthsInfo = ds.Tables[0].Rows[n]["DiseaseInSixMonthsInfo"].ToString();
                    model.WorkExperience = ds.Tables[0].Rows[n]["WorkExperience"].ToString();
                    model.WorkSpecialty = ds.Tables[0].Rows[n]["WorkSpecialty"].ToString();
                    model.ThisYearSalary = ds.Tables[0].Rows[n]["ThisYearSalary"].ToString();

                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    model.Memo = ds.Tables[0].Rows[n]["Memo"].ToString();
                    if (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.BaseInfoOK = true;
                        }
                        else
                        {
                            model.BaseInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ContractInfoOK = true;
                        }
                        else
                        {
                            model.ContractInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.InsuranceInfoOK = true;
                        }
                        else
                        {
                            model.InsuranceInfoOK = false;
                        }
                    }
                    model.Phone2 = ds.Tables[0].Rows[n]["Phone2"].ToString();
                    if (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ArchiveInfoOK = true;
                        }
                        else
                        {
                            model.ArchiveInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Creator"].ToString() != "")
                    {
                        model.Creator = int.Parse(ds.Tables[0].Rows[n]["Creator"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreatedTime"].ToString() != "")
                    {
                        model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[n]["CreatedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifier"].ToString() != "")
                    {
                        model.LastModifier = int.Parse(ds.Tables[0].Rows[n]["LastModifier"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifiedTime"].ToString() != "")
                    {
                        model.LastModifiedTime = DateTime.Parse(ds.Tables[0].Rows[n]["LastModifiedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["RowVersion"].ToString() != "")
                    {
                        model.RowVersion = (byte[])ds.Tables[0].Rows[n]["RowVersion"];
                    }
                    model.MobilePhone = ds.Tables[0].Rows[n]["MobilePhone"].ToString();
                    model.HomePhone = ds.Tables[0].Rows[n]["HomePhone"].ToString();
                    model.Fax = ds.Tables[0].Rows[n]["Fax"].ToString();
                    model.InternalEmail = ds.Tables[0].Rows[n]["InternalEmail"].ToString();

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Resume = ds.Tables[0].Rows[n]["Resume"].ToString();
                    if (ds.Tables[0].Rows[n]["IsForeign"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsForeign"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsForeign"].ToString().ToLower() == "true"))
                        {
                            model.IsForeign = true;
                        }
                        else
                        {
                            model.IsForeign = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsCertification"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsCertification"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsCertification"].ToString().ToLower() == "true"))
                        {
                            model.IsCertification = true;
                        }
                        else
                        {
                            model.IsCertification = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["WageMonths"].ToString() != "")
                    {
                        model.WageMonths = int.Parse(ds.Tables[0].Rows[n]["WageMonths"].ToString());
                    }
                    model.IPCode = ds.Tables[0].Rows[n]["IPCode"].ToString();
                    if (ds.Tables[0].Rows[n]["IsSendMail"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsSendMail"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsSendMail"].ToString().ToLower() == "true"))
                        {
                            model.IsSendMail = true;
                        }
                        else
                        {
                            model.IsSendMail = false;
                        }
                    }
                    model.CommonName = ds.Tables[0].Rows[n]["CommonName"].ToString();
                    if (ds.Tables[0].Rows[n]["DimissionStatus"] != DBNull.Value)
                        model.DimissionStatus = int.Parse(ds.Tables[0].Rows[n]["DimissionStatus"].ToString());
                    else
                        model.DimissionStatus = 0;
                    model.PrivateEmail = ds.Tables[0].Rows[n]["PrivateEmail"].ToString();
                    if (ds.Tables[0].Rows[n]["OwnedPC"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["OwnedPC"].ToString() == "1") || (ds.Tables[0].Rows[n]["OwnedPC"].ToString().ToLower() == "true"))
                        {
                            model.OwnedPC = true;
                        }
                        else
                        {
                            model.OwnedPC = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString() != "")
                    {
                        model.OfferLetterTemplate = int.Parse(ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterSender"].ToString() != "")
                    {
                        model.OfferLetterSender = int.Parse(ds.Tables[0].Rows[n]["OfferLetterSender"].ToString());
                    }
                    var objOfferLetterSendTime = ds.Tables[0].Rows[n]["OfferLetterSendTime"];
                    if (!(objOfferLetterSendTime is DBNull))
                    {
                        model.OfferLetterSendTime = (DateTime)objOfferLetterSendTime;
                    }
                    if (ds.Tables[0].Rows[n]["IsExamen"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsExamen"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsExamen"].ToString().ToLower() == "true"))
                        {
                            model.IsExamen = true;
                        }
                        else
                        {
                            model.IsExamen = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Seniority"].ToString() != "")
                    {
                        model.Seniority = int.Parse(ds.Tables[0].Rows[n]["Seniority"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["workBegin"].ToString() != "")
                    {
                        model.WorkBegin = DateTime.Parse(ds.Tables[0].Rows[n]["workBegin"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["JoinDate"].ToString() != "")
                    {
                        model.JoinDate = DateTime.Parse(ds.Tables[0].Rows[n]["JoinDate"].ToString());
                    }

                    model.BranchCode = ds.Tables[0].Rows[n]["BranchCode"].ToString();

                    if (ds.Tables[0].Rows[n]["ContractYear"].ToString() != "")
                    {
                        model.ContractYear = int.Parse(ds.Tables[0].Rows[n]["ContractYear"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ContractBeginDate"].ToString() != "")
                    {
                        model.ContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractBeginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ContractEndDate"].ToString() != "")
                    {
                        model.ContractEndDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractEndDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString() != "")
                    {
                        model.FirstContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString() != "")
                    {
                        model.FirstContractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["ContractSignDate"].ToString() != "")
                    {
                        model.ContractSignDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractSignDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString() != "")
                    {
                        model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["ProbationDate"].ToString() != "")
                    {
                        model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[0]["ProbationDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["SocialSecurityType"].ToString() != "")
                    {
                        model.SocialSecurityType = int.Parse(ds.Tables[0].Rows[n]["SocialSecurityType"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString() != "")
                    {
                        model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ProbationDate"].ToString() != "")
                    {
                        model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[n]["ProbationDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["HasChild"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["HasChild"].ToString() == "1") || (ds.Tables[0].Rows[n]["HasChild"].ToString().ToLower() == "true"))
                        {
                            model.HasChild = true;
                        }
                        else
                        {
                            model.HasChild = false;
                        }
                    }

                    model.SalaryBank = ds.Tables[0].Rows[n]["SalaryBank"].ToString();
                    model.SalaryCardNo = ds.Tables[0].Rows[n]["SalaryCardNo"].ToString();


                    if (ds.Tables[0].Rows[n]["Pay"].ToString() != "")
                    {
                        model.Pay = decimal.Parse(ds.Tables[0].Rows[n]["Pay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Performance"].ToString() != "")
                    {
                        model.Performance = decimal.Parse(ds.Tables[0].Rows[n]["Performance"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Attendance"].ToString() != "")
                    {
                        model.Attendance = decimal.Parse(ds.Tables[0].Rows[n]["Attendance"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["IDValid"].ToString() != "")
                    {
                        model.IDValid = DateTime.Parse(ds.Tables[0].Rows[n]["IDValid"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public static List<EmployeeBaseInfo> GetModelListForExport(string strWhere)
        {
            DataSet ds = dal.GetListForExport(strWhere);
            List<EmployeeBaseInfo> modelList = new List<EmployeeBaseInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                EmployeeBaseInfo model;
                EmployeeJobInfo jobModel;
                EmployeeWelfareInfo welfModel;
                List<EmployeesInPositionsInfo> positionlist = null;

                   
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EmployeeBaseInfo();
                   
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                        jobModel = EmployeeJobManager.getModelBySysId(model.UserID);
                        model.EmployeeJobInfo = jobModel;
                        welfModel = EmployeeWelfareManager.getModelBySysId(model.UserID);
                        model.EmployeeWelfareInfo = welfModel;
                        positionlist = new List<EmployeesInPositionsInfo>();
                        EmployeesInPositionsInfo position = new EmployeesInPositionsInfo();
                        position.UserID = model.UserID;
                        position.CompanyID = int.Parse( ds.Tables[0].Rows[n]["CompanyID"].ToString());
                        position.CompanyName = ds.Tables[0].Rows[n]["CompanyName"].ToString();
                        position.DepartmentID = int.Parse(ds.Tables[0].Rows[n]["DepartmentID"].ToString());
                        position.DepartmentName = ds.Tables[0].Rows[n]["DepartmentName"].ToString();
                        position.GroupID = int.Parse(ds.Tables[0].Rows[n]["GroupID"].ToString());
                        position.GroupName = ds.Tables[0].Rows[n]["GroupName"].ToString();
                        position.DepartmentPositionName = ds.Tables[0].Rows[n]["joinJob"].ToString();
                        positionlist.Add(position);
                        model.EmployeesInPositionsList = positionlist;

                    }

                    model.IM = ds.Tables[0].Rows[n]["IM"].ToString();
                    model.EmergencyContact = ds.Tables[0].Rows[n]["EmergencyContact"].ToString();
                    model.EmergencyContactPhone = ds.Tables[0].Rows[n]["EmergencyContactPhone"].ToString();
                    model.Address = ds.Tables[0].Rows[n]["Address"].ToString();
                    model.City = ds.Tables[0].Rows[n]["City"].ToString();
                    model.Province = ds.Tables[0].Rows[n]["Province"].ToString();
                    model.Country = ds.Tables[0].Rows[n]["Country"].ToString();
                    model.PostCode = ds.Tables[0].Rows[n]["PostCode"].ToString();
                    model.Address2 = ds.Tables[0].Rows[n]["Adress2"].ToString();
                    model.City2 = ds.Tables[0].Rows[n]["City2"].ToString();
                    model.Code = ds.Tables[0].Rows[n]["Code"].ToString();
                    model.Province2 = ds.Tables[0].Rows[n]["Province2"].ToString();
                    model.Country2 = ds.Tables[0].Rows[n]["Country2"].ToString();
                    model.PostCode2 = ds.Tables[0].Rows[n]["PostCode2"].ToString();
                    model.WorkAddress = ds.Tables[0].Rows[n]["WorkAddress"].ToString();
                    model.WorkCity = ds.Tables[0].Rows[n]["WorkCity"].ToString();
                    model.WorkProvince = ds.Tables[0].Rows[n]["WorkProvince"].ToString();
                    model.WorkCountry = ds.Tables[0].Rows[n]["WorkCountry"].ToString();
                    model.WorkPostCode = ds.Tables[0].Rows[n]["WorkPostCode"].ToString();
                    if (ds.Tables[0].Rows[n]["MaritalStatus"].ToString() != "")
                    {
                        model.MaritalStatus = int.Parse(ds.Tables[0].Rows[n]["MaritalStatus"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Gender"].ToString() != "")
                    {
                        model.Gender = int.Parse(ds.Tables[0].Rows[n]["Gender"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["TypeID"].ToString() != "")
                    {
                        model.TypeID = int.Parse(ds.Tables[0].Rows[n]["TypeID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(ds.Tables[0].Rows[n]["Birthday"].ToString());
                    }
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();
                    model.BirthPlace = ds.Tables[0].Rows[n]["BirthPlace"].ToString();
                    model.IDNumber = ds.Tables[0].Rows[0]["IDNumber"].ToString();
                    model.DomicilePlace = ds.Tables[0].Rows[n]["DomicilePlace"].ToString();
                    model.Photo = ds.Tables[0].Rows[n]["Photo"].ToString();
                    model.Degree = ds.Tables[0].Rows[n]["Degree"].ToString();
                    model.Education = ds.Tables[0].Rows[n]["Education"].ToString();
                    model.GraduateFrom = ds.Tables[0].Rows[n]["GraduatedFrom"].ToString();
                    model.Major = ds.Tables[0].Rows[n]["Major"].ToString();
                    if (ds.Tables[0].Rows[n]["GraduatedDate"].ToString() != "")
                    {
                        model.GraduatedDate = DateTime.Parse(ds.Tables[0].Rows[n]["GraduatedDate"].ToString());
                    }
                    model.Health = ds.Tables[0].Rows[n]["Health"].ToString();
                    model.Phone1 = ds.Tables[0].Rows[n]["Phone1"].ToString();
                    model.DiseaseInSixMonths = ds.Tables[0].Rows[n]["DiseaseInSixMonths"].ToString();
                    model.DiseaseInSixMonthsInfo = ds.Tables[0].Rows[n]["DiseaseInSixMonthsInfo"].ToString();
                    model.WorkExperience = ds.Tables[0].Rows[n]["WorkExperience"].ToString();
                    model.WorkSpecialty = ds.Tables[0].Rows[n]["WorkSpecialty"].ToString();
                    model.ThisYearSalary = ds.Tables[0].Rows[n]["ThisYearSalary"].ToString();

                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    model.Memo = ds.Tables[0].Rows[n]["Memo"].ToString();
                    if (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.BaseInfoOK = true;
                        }
                        else
                        {
                            model.BaseInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ContractInfoOK = true;
                        }
                        else
                        {
                            model.ContractInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.InsuranceInfoOK = true;
                        }
                        else
                        {
                            model.InsuranceInfoOK = false;
                        }
                    }
                    model.Phone2 = ds.Tables[0].Rows[n]["Phone2"].ToString();
                    if (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ArchiveInfoOK = true;
                        }
                        else
                        {
                            model.ArchiveInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Creator"].ToString() != "")
                    {
                        model.Creator = int.Parse(ds.Tables[0].Rows[n]["Creator"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreatedTime"].ToString() != "")
                    {
                        model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[n]["CreatedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifier"].ToString() != "")
                    {
                        model.LastModifier = int.Parse(ds.Tables[0].Rows[n]["LastModifier"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifiedTime"].ToString() != "")
                    {
                        model.LastModifiedTime = DateTime.Parse(ds.Tables[0].Rows[n]["LastModifiedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["RowVersion"].ToString() != "")
                    {
                        model.RowVersion = (byte[])ds.Tables[0].Rows[n]["RowVersion"];
                    }
                    model.MobilePhone = ds.Tables[0].Rows[n]["MobilePhone"].ToString();
                    model.HomePhone = ds.Tables[0].Rows[n]["HomePhone"].ToString();
                    model.Fax = ds.Tables[0].Rows[n]["Fax"].ToString();
                    model.InternalEmail = ds.Tables[0].Rows[n]["InternalEmail"].ToString();

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Resume = ds.Tables[0].Rows[n]["Resume"].ToString();
                    if (ds.Tables[0].Rows[n]["IsForeign"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsForeign"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsForeign"].ToString().ToLower() == "true"))
                        {
                            model.IsForeign = true;
                        }
                        else
                        {
                            model.IsForeign = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsCertification"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsCertification"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsCertification"].ToString().ToLower() == "true"))
                        {
                            model.IsCertification = true;
                        }
                        else
                        {
                            model.IsCertification = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["WageMonths"].ToString() != "")
                    {
                        model.WageMonths = int.Parse(ds.Tables[0].Rows[n]["WageMonths"].ToString());
                    }
                    model.IPCode = ds.Tables[0].Rows[n]["IPCode"].ToString();
                    if (ds.Tables[0].Rows[n]["IsSendMail"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsSendMail"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsSendMail"].ToString().ToLower() == "true"))
                        {
                            model.IsSendMail = true;
                        }
                        else
                        {
                            model.IsSendMail = false;
                        }
                    }
                    model.CommonName = ds.Tables[0].Rows[n]["CommonName"].ToString();
                    if (ds.Tables[0].Rows[n]["DimissionStatus"] != DBNull.Value)
                        model.DimissionStatus = int.Parse(ds.Tables[0].Rows[n]["DimissionStatus"].ToString());
                    else
                        model.DimissionStatus = 0;
                    model.PrivateEmail = ds.Tables[0].Rows[n]["PrivateEmail"].ToString();
                    if (ds.Tables[0].Rows[n]["OwnedPC"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["OwnedPC"].ToString() == "1") || (ds.Tables[0].Rows[n]["OwnedPC"].ToString().ToLower() == "true"))
                        {
                            model.OwnedPC = true;
                        }
                        else
                        {
                            model.OwnedPC = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString() != "")
                    {
                        model.OfferLetterTemplate = int.Parse(ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterSender"].ToString() != "")
                    {
                        model.OfferLetterSender = int.Parse(ds.Tables[0].Rows[n]["OfferLetterSender"].ToString());
                    }
                    var objOfferLetterSendTime = ds.Tables[0].Rows[n]["OfferLetterSendTime"];
                    if (!(objOfferLetterSendTime is DBNull))
                    {
                        model.OfferLetterSendTime = (DateTime)objOfferLetterSendTime;
                    }
                    if (ds.Tables[0].Rows[n]["IsExamen"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsExamen"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsExamen"].ToString().ToLower() == "true"))
                        {
                            model.IsExamen = true;
                        }
                        else
                        {
                            model.IsExamen = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Seniority"].ToString() != "")
                    {
                        model.Seniority = int.Parse(ds.Tables[0].Rows[n]["Seniority"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["workBegin"].ToString() != "")
                    {
                        model.WorkBegin = DateTime.Parse(ds.Tables[0].Rows[n]["workBegin"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["JoinDate"].ToString() != "")
                    {
                        model.JoinDate = DateTime.Parse(ds.Tables[0].Rows[n]["JoinDate"].ToString());
                    }

                    model.BranchCode = ds.Tables[0].Rows[n]["BranchCode"].ToString();

                    if (ds.Tables[0].Rows[n]["ContractYear"].ToString() != "")
                    {
                        model.ContractYear = int.Parse(ds.Tables[0].Rows[n]["ContractYear"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ContractBeginDate"].ToString() != "")
                    {
                        model.ContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractBeginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ContractEndDate"].ToString() != "")
                    {
                        model.ContractEndDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractEndDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString() != "")
                    {
                        model.FirstContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString() != "")
                    {
                        model.FirstContractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["ContractSignDate"].ToString() != "")
                    {
                        model.ContractSignDate = DateTime.Parse(ds.Tables[0].Rows[n]["ContractSignDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString() != "")
                    {
                        model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["ProbationDate"].ToString() != "")
                    {
                        model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[0]["ProbationDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["SocialSecurityType"].ToString() != "")
                    {
                        model.SocialSecurityType = int.Parse(ds.Tables[0].Rows[n]["SocialSecurityType"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString() != "")
                    {
                        model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ProbationDate"].ToString() != "")
                    {
                        model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[n]["ProbationDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["HasChild"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["HasChild"].ToString() == "1") || (ds.Tables[0].Rows[n]["HasChild"].ToString().ToLower() == "true"))
                        {
                            model.HasChild = true;
                        }
                        else
                        {
                            model.HasChild = false;
                        }
                    }

                    model.SalaryBank = ds.Tables[0].Rows[n]["SalaryBank"].ToString();
                    model.SalaryCardNo = ds.Tables[0].Rows[n]["SalaryCardNo"].ToString();


                    if (ds.Tables[0].Rows[n]["Pay"].ToString() != "")
                    {
                        model.Pay = decimal.Parse(ds.Tables[0].Rows[n]["Pay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Performance"].ToString() != "")
                    {
                        model.Performance = decimal.Parse(ds.Tables[0].Rows[n]["Performance"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Attendance"].ToString() != "")
                    {
                        model.Attendance = decimal.Parse(ds.Tables[0].Rows[n]["Attendance"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["IDValid"].ToString() != "")
                    {
                        model.IDValid = DateTime.Parse(ds.Tables[0].Rows[n]["IDValid"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }


        public static List<EmployeeBaseInfo> GetModelListForHC(string strWhere)
        {
            DataSet ds = dal.GetListForHC(strWhere);
            List<EmployeeBaseInfo> modelList = new List<EmployeeBaseInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                EmployeeBaseInfo model;
                EmployeeJobInfo jobModel;
                EmployeeWelfareInfo welfModel;

                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EmployeeBaseInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                        jobModel = EmployeeJobManager.getModelBySysId(model.UserID);
                        model.EmployeeJobInfo = jobModel;
                        welfModel = EmployeeWelfareManager.getModelBySysId(model.UserID);
                        model.EmployeeWelfareInfo = welfModel;
                    }
                    model.IM = ds.Tables[0].Rows[n]["IM"].ToString();
                    model.EmergencyContact = ds.Tables[0].Rows[n]["EmergencyContact"].ToString();
                    model.EmergencyContactPhone = ds.Tables[0].Rows[n]["EmergencyContactPhone"].ToString();
                    model.Address = ds.Tables[0].Rows[n]["Address"].ToString();
                    model.City = ds.Tables[0].Rows[n]["City"].ToString();
                    model.Province = ds.Tables[0].Rows[n]["Province"].ToString();
                    model.Country = ds.Tables[0].Rows[n]["Country"].ToString();
                    model.PostCode = ds.Tables[0].Rows[n]["PostCode"].ToString();
                    model.Address2 = ds.Tables[0].Rows[n]["Adress2"].ToString();
                    model.City2 = ds.Tables[0].Rows[n]["City2"].ToString();
                    model.Code = ds.Tables[0].Rows[n]["Code"].ToString();
                    model.Province2 = ds.Tables[0].Rows[n]["Province2"].ToString();
                    model.Country2 = ds.Tables[0].Rows[n]["Country2"].ToString();
                    model.PostCode2 = ds.Tables[0].Rows[n]["PostCode2"].ToString();
                    model.WorkAddress = ds.Tables[0].Rows[n]["WorkAddress"].ToString();
                    model.WorkCity = ds.Tables[0].Rows[n]["WorkCity"].ToString();
                    model.WorkProvince = ds.Tables[0].Rows[n]["WorkProvince"].ToString();
                    model.WorkCountry = ds.Tables[0].Rows[n]["WorkCountry"].ToString();
                    model.WorkPostCode = ds.Tables[0].Rows[n]["WorkPostCode"].ToString();
                    if (ds.Tables[0].Rows[n]["MaritalStatus"].ToString() != "")
                    {
                        model.MaritalStatus = int.Parse(ds.Tables[0].Rows[n]["MaritalStatus"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Gender"].ToString() != "")
                    {
                        model.Gender =  int.Parse(ds.Tables[0].Rows[n]["Gender"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["TypeID"].ToString() != "")
                    {
                        model.TypeID = int.Parse(ds.Tables[0].Rows[n]["TypeID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(ds.Tables[0].Rows[n]["Birthday"].ToString());
                    }
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();
                    model.BirthPlace = ds.Tables[0].Rows[n]["BirthPlace"].ToString();
                    model.IDNumber = ds.Tables[0].Rows[0]["IDNumber"].ToString();
                    model.DomicilePlace = ds.Tables[0].Rows[n]["DomicilePlace"].ToString();
                    model.Photo = ds.Tables[0].Rows[n]["Photo"].ToString();
                    model.Degree = ds.Tables[0].Rows[n]["Degree"].ToString();
                    model.Education = ds.Tables[0].Rows[n]["Education"].ToString();
                    model.GraduateFrom = ds.Tables[0].Rows[n]["GraduatedFrom"].ToString();
                    model.Major = ds.Tables[0].Rows[n]["Major"].ToString();
                    if (ds.Tables[0].Rows[n]["GraduatedDate"].ToString() != "")
                    {
                        model.GraduatedDate = DateTime.Parse(ds.Tables[0].Rows[n]["GraduatedDate"].ToString());
                    }
                    model.Health = ds.Tables[0].Rows[n]["Health"].ToString();
                    model.Phone1 = ds.Tables[0].Rows[n]["Phone1"].ToString();
                    model.DiseaseInSixMonths = ds.Tables[0].Rows[n]["DiseaseInSixMonths"].ToString();
                    model.DiseaseInSixMonthsInfo = ds.Tables[0].Rows[n]["DiseaseInSixMonthsInfo"].ToString();
                    model.WorkExperience = ds.Tables[0].Rows[n]["WorkExperience"].ToString();
                    model.WorkSpecialty = ds.Tables[0].Rows[n]["WorkSpecialty"].ToString();
                    model.ThisYearSalary = ds.Tables[0].Rows[n]["ThisYearSalary"].ToString();

                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    model.Memo = ds.Tables[0].Rows[n]["Memo"].ToString();
                    if (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.BaseInfoOK = true;
                        }
                        else
                        {
                            model.BaseInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ContractInfoOK = true;
                        }
                        else
                        {
                            model.ContractInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.InsuranceInfoOK = true;
                        }
                        else
                        {
                            model.InsuranceInfoOK = false;
                        }
                    }
                    model.Phone2 = ds.Tables[0].Rows[n]["Phone2"].ToString();
                    if (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ArchiveInfoOK = true;
                        }
                        else
                        {
                            model.ArchiveInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Creator"].ToString() != "")
                    {
                        model.Creator = int.Parse(ds.Tables[0].Rows[n]["Creator"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreatedTime"].ToString() != "")
                    {
                        model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[n]["CreatedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifier"].ToString() != "")
                    {
                        model.LastModifier = int.Parse(ds.Tables[0].Rows[n]["LastModifier"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifiedTime"].ToString() != "")
                    {
                        model.LastModifiedTime = DateTime.Parse(ds.Tables[0].Rows[n]["LastModifiedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["RowVersion"].ToString() != "")
                    {
                        model.RowVersion = (byte[])ds.Tables[0].Rows[n]["RowVersion"];
                    }
                    model.MobilePhone = ds.Tables[0].Rows[n]["MobilePhone"].ToString();
                    model.HomePhone = ds.Tables[0].Rows[n]["HomePhone"].ToString();
                    model.Fax = ds.Tables[0].Rows[n]["Fax"].ToString();
                    model.InternalEmail = ds.Tables[0].Rows[n]["InternalEmail"].ToString();

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Resume = ds.Tables[0].Rows[n]["Resume"].ToString();
                    if (ds.Tables[0].Rows[n]["IsForeign"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsForeign"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsForeign"].ToString().ToLower() == "true"))
                        {
                            model.IsForeign = true;
                        }
                        else
                        {
                            model.IsForeign = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsCertification"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsCertification"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsCertification"].ToString().ToLower() == "true"))
                        {
                            model.IsCertification = true;
                        }
                        else
                        {
                            model.IsCertification = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["WageMonths"].ToString() != "")
                    {
                        model.WageMonths = int.Parse(ds.Tables[0].Rows[n]["WageMonths"].ToString());
                    }
                    model.IPCode = ds.Tables[0].Rows[n]["IPCode"].ToString();
                    if (ds.Tables[0].Rows[n]["IsSendMail"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsSendMail"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsSendMail"].ToString().ToLower() == "true"))
                        {
                            model.IsSendMail = true;
                        }
                        else
                        {
                            model.IsSendMail = false;
                        }
                    }
                    model.CommonName = ds.Tables[0].Rows[n]["CommonName"].ToString();
                    if (ds.Tables[0].Rows[n]["DimissionStatus"] != DBNull.Value)
                        model.DimissionStatus = int.Parse(ds.Tables[0].Rows[n]["DimissionStatus"].ToString());
                    else
                        model.DimissionStatus = 0;
                    model.PrivateEmail = ds.Tables[0].Rows[n]["PrivateEmail"].ToString();
                    if (ds.Tables[0].Rows[n]["OwnedPC"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["OwnedPC"].ToString() == "1") || (ds.Tables[0].Rows[n]["OwnedPC"].ToString().ToLower() == "true"))
                        {
                            model.OwnedPC = true;
                        }
                        else
                        {
                            model.OwnedPC = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString() != "")
                    {
                        model.OfferLetterTemplate = int.Parse(ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterSender"].ToString() != "")
                    {
                        model.OfferLetterSender = int.Parse(ds.Tables[0].Rows[n]["OfferLetterSender"].ToString());
                    }
                    var objOfferLetterSendTime = ds.Tables[0].Rows[n]["OfferLetterSendTime"];
                    if (!(objOfferLetterSendTime is DBNull))
                    {
                        model.OfferLetterSendTime = (DateTime)objOfferLetterSendTime;
                    }
                    if (ds.Tables[0].Rows[n]["IsExamen"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsExamen"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsExamen"].ToString().ToLower() == "true"))
                        {
                            model.IsExamen = true;
                        }
                        else
                        {
                            model.IsExamen = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Seniority"].ToString() != "")
                    {
                        model.Seniority = int.Parse(ds.Tables[0].Rows[n]["Seniority"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString() != "")
                    {
                        model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ProbationDate"].ToString() != "")
                    {
                        model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[n]["ProbationDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["HasChild"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["HasChild"].ToString() == "1") || (ds.Tables[0].Rows[n]["HasChild"].ToString().ToLower() == "true"))
                        {
                            model.HasChild = true;
                        }
                        else
                        {
                            model.HasChild = false;
                        }
                    }

                    model.SalaryBank = ds.Tables[0].Rows[n]["SalaryBank"].ToString();
                    model.SalaryCardNo = ds.Tables[0].Rows[n]["SalaryCardNo"].ToString();


                    if (ds.Tables[0].Rows[n]["Pay"].ToString() != "")
                    {
                        model.Pay = decimal.Parse(ds.Tables[0].Rows[n]["Pay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Performance"].ToString() != "")
                    {
                        model.Performance = decimal.Parse(ds.Tables[0].Rows[n]["Performance"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Attendance"].ToString() != "")
                    {
                        model.Attendance = decimal.Parse(ds.Tables[0].Rows[n]["Attendance"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["IDValid"].ToString() != "")
                    {
                        model.IDValid = DateTime.Parse(ds.Tables[0].Rows[n]["IDValid"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }


        /// <summary>
        /// 获得待入职人员信息列表（速度慢，通过用户ID，循环查询数据库员工关联信息）
        /// </summary>
        public static List<EmployeeBaseInfo> GetWaitEntryModelList(int userid ,string strWhere)
        {
            DataSet ds = dal.GetWaitEntryList(userid,strWhere);
            List<EmployeeBaseInfo> modelList = new List<EmployeeBaseInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                EmployeeBaseInfo model;
                EmployeeJobInfo jobModel;
                EmployeeWelfareInfo welfModel;

                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EmployeeBaseInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                        jobModel = EmployeeJobManager.getModelBySysId(model.UserID);
                        model.EmployeeJobInfo = jobModel;
                        welfModel = EmployeeWelfareManager.getModelBySysId(model.UserID);
                        model.EmployeeWelfareInfo = welfModel;
                    }
                    model.IM = ds.Tables[0].Rows[n]["IM"].ToString();
                    model.EmergencyContact = ds.Tables[0].Rows[n]["EmergencyContact"].ToString();
                    model.EmergencyContactPhone = ds.Tables[0].Rows[n]["EmergencyContactPhone"].ToString();
                    model.Address = ds.Tables[0].Rows[n]["Address"].ToString();
                    model.City = ds.Tables[0].Rows[n]["City"].ToString();
                    model.Province = ds.Tables[0].Rows[n]["Province"].ToString();
                    model.Country = ds.Tables[0].Rows[n]["Country"].ToString();
                    model.PostCode = ds.Tables[0].Rows[n]["PostCode"].ToString();
                    model.Address2 = ds.Tables[0].Rows[n]["Adress2"].ToString();
                    model.City2 = ds.Tables[0].Rows[n]["City2"].ToString();
                    model.Code = ds.Tables[0].Rows[n]["Code"].ToString();
                    model.Province2 = ds.Tables[0].Rows[n]["Province2"].ToString();
                    model.Country2 = ds.Tables[0].Rows[n]["Country2"].ToString();
                    model.PostCode2 = ds.Tables[0].Rows[n]["PostCode2"].ToString();
                    model.WorkAddress = ds.Tables[0].Rows[n]["WorkAddress"].ToString();
                    model.WorkCity = ds.Tables[0].Rows[n]["WorkCity"].ToString();
                    model.WorkProvince = ds.Tables[0].Rows[n]["WorkProvince"].ToString();
                    model.WorkCountry = ds.Tables[0].Rows[n]["WorkCountry"].ToString();
                    model.WorkPostCode = ds.Tables[0].Rows[n]["WorkPostCode"].ToString();
                    if (ds.Tables[0].Rows[n]["MaritalStatus"].ToString() != "")
                    {
                        model.MaritalStatus = int.Parse(ds.Tables[0].Rows[n]["MaritalStatus"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Gender"].ToString() != "")
                    {
                        model.Gender = int.Parse(ds.Tables[0].Rows[n]["Gender"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["TypeID"].ToString() != "")
                    {
                        model.TypeID = int.Parse(ds.Tables[0].Rows[n]["TypeID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(ds.Tables[0].Rows[n]["Birthday"].ToString());
                    }
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();
                    model.BirthPlace = ds.Tables[0].Rows[n]["BirthPlace"].ToString();
                    model.IDNumber = ds.Tables[0].Rows[0]["IDNumber"].ToString();
                    model.DomicilePlace = ds.Tables[0].Rows[n]["DomicilePlace"].ToString();
                    model.Photo = ds.Tables[0].Rows[n]["Photo"].ToString();
                    model.Degree = ds.Tables[0].Rows[n]["Degree"].ToString();
                    model.Education = ds.Tables[0].Rows[n]["Education"].ToString();
                    model.GraduateFrom = ds.Tables[0].Rows[n]["GraduatedFrom"].ToString();
                    model.Major = ds.Tables[0].Rows[n]["Major"].ToString();
                    if (ds.Tables[0].Rows[n]["GraduatedDate"].ToString() != "")
                    {
                        model.GraduatedDate = DateTime.Parse(ds.Tables[0].Rows[n]["GraduatedDate"].ToString());
                    }
                    model.Health = ds.Tables[0].Rows[n]["Health"].ToString();
                    model.Phone1 = ds.Tables[0].Rows[n]["Phone1"].ToString();
                    model.DiseaseInSixMonths = ds.Tables[0].Rows[n]["DiseaseInSixMonths"].ToString();
                    model.DiseaseInSixMonthsInfo = ds.Tables[0].Rows[n]["DiseaseInSixMonthsInfo"].ToString();
                    model.WorkExperience = ds.Tables[0].Rows[n]["WorkExperience"].ToString();
                    model.WorkSpecialty = ds.Tables[0].Rows[n]["WorkSpecialty"].ToString();
                    model.ThisYearSalary = ds.Tables[0].Rows[n]["ThisYearSalary"].ToString();

                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    model.Memo = ds.Tables[0].Rows[n]["Memo"].ToString();
                    if (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["BaseInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["BaseInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.BaseInfoOK = true;
                        }
                        else
                        {
                            model.BaseInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ContractInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ContractInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ContractInfoOK = true;
                        }
                        else
                        {
                            model.ContractInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["InsuranceInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.InsuranceInfoOK = true;
                        }
                        else
                        {
                            model.InsuranceInfoOK = false;
                        }
                    }
                    model.Phone2 = ds.Tables[0].Rows[n]["Phone2"].ToString();
                    if (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[n]["ArchiveInfoOK"].ToString().ToLower() == "true"))
                        {
                            model.ArchiveInfoOK = true;
                        }
                        else
                        {
                            model.ArchiveInfoOK = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Creator"].ToString() != "")
                    {
                        model.Creator = int.Parse(ds.Tables[0].Rows[n]["Creator"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreatedTime"].ToString() != "")
                    {
                        model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[n]["CreatedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifier"].ToString() != "")
                    {
                        model.LastModifier = int.Parse(ds.Tables[0].Rows[n]["LastModifier"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastModifiedTime"].ToString() != "")
                    {
                        model.LastModifiedTime = DateTime.Parse(ds.Tables[0].Rows[n]["LastModifiedTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["RowVersion"].ToString() != "")
                    {
                        model.RowVersion = (byte[])ds.Tables[0].Rows[n]["RowVersion"];
                    }
                    model.MobilePhone = ds.Tables[0].Rows[n]["MobilePhone"].ToString();
                    model.HomePhone = ds.Tables[0].Rows[n]["HomePhone"].ToString();
                    model.Fax = ds.Tables[0].Rows[n]["Fax"].ToString();
                    model.InternalEmail = ds.Tables[0].Rows[n]["InternalEmail"].ToString();

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Resume = ds.Tables[0].Rows[n]["Resume"].ToString();
                    if (ds.Tables[0].Rows[n]["IsForeign"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsForeign"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsForeign"].ToString().ToLower() == "true"))
                        {
                            model.IsForeign = true;
                        }
                        else
                        {
                            model.IsForeign = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsCertification"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsCertification"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsCertification"].ToString().ToLower() == "true"))
                        {
                            model.IsCertification = true;
                        }
                        else
                        {
                            model.IsCertification = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["WageMonths"].ToString() != "")
                    {
                        model.WageMonths = int.Parse(ds.Tables[0].Rows[n]["WageMonths"].ToString());
                    }
                    model.IPCode = ds.Tables[0].Rows[n]["IPCode"].ToString();
                    if (ds.Tables[0].Rows[n]["IsSendMail"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsSendMail"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsSendMail"].ToString().ToLower() == "true"))
                        {
                            model.IsSendMail = true;
                        }
                        else
                        {
                            model.IsSendMail = false;
                        }
                    }
                    model.CommonName = ds.Tables[0].Rows[n]["CommonName"].ToString();
                    if (ds.Tables[0].Rows[n]["DimissionStatus"] != DBNull.Value)
                        model.DimissionStatus = int.Parse(ds.Tables[0].Rows[n]["DimissionStatus"].ToString());
                    else
                        model.DimissionStatus = 0;
                    model.PrivateEmail = ds.Tables[0].Rows[n]["PrivateEmail"].ToString();
                    if (ds.Tables[0].Rows[n]["OwnedPC"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["OwnedPC"].ToString() == "1") || (ds.Tables[0].Rows[n]["OwnedPC"].ToString().ToLower() == "true"))
                        {
                            model.OwnedPC = true;
                        }
                        else
                        {
                            model.OwnedPC = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString() != "")
                    {
                        model.OfferLetterTemplate = int.Parse(ds.Tables[0].Rows[n]["OfferLetterTemplate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["OfferLetterSender"].ToString() != "")
                    {
                        model.OfferLetterSender = int.Parse(ds.Tables[0].Rows[n]["OfferLetterSender"].ToString());
                    }
                    var objOfferLetterSendTime = ds.Tables[0].Rows[n]["OfferLetterSendTime"];
                    if (!(objOfferLetterSendTime is DBNull))
                    {
                        model.OfferLetterSendTime = (DateTime)objOfferLetterSendTime;
                    }
                    if (ds.Tables[0].Rows[n]["IsExamen"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsExamen"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsExamen"].ToString().ToLower() == "true"))
                        {
                            model.IsExamen = true;
                        }
                        else
                        {
                            model.IsExamen = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["Seniority"].ToString() != "")
                    {
                        model.Seniority = int.Parse(ds.Tables[0].Rows[n]["Seniority"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString() != "")
                    {
                        model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[n]["AnnualLeaveBase"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ProbationDate"].ToString() != "")
                    {
                        model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[n]["ProbationDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["HasChild"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["HasChild"].ToString() == "1") || (ds.Tables[0].Rows[n]["HasChild"].ToString().ToLower() == "true"))
                        {
                            model.HasChild = true;
                        }
                        else
                        {
                            model.HasChild = false;
                        }
                    }

                    model.SalaryBank = ds.Tables[0].Rows[n]["SalaryBank"].ToString();
                    model.SalaryCardNo = ds.Tables[0].Rows[n]["SalaryCardNo"].ToString();


                    if (ds.Tables[0].Rows[n]["Pay"].ToString() != "")
                    {
                        model.Pay = decimal.Parse(ds.Tables[0].Rows[n]["Pay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Performance"].ToString() != "")
                    {
                        model.Performance = decimal.Parse(ds.Tables[0].Rows[n]["Performance"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Attendance"].ToString() != "")
                    {
                        model.Attendance = decimal.Parse(ds.Tables[0].Rows[n]["Attendance"].ToString());
                    }

                    if (ds.Tables[0].Rows[n]["IDValid"].ToString() != "")
                    {
                        model.IDValid = DateTime.Parse(ds.Tables[0].Rows[n]["IDValid"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 绑定用户的所有历史信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IList<EmployeeBaseInfo> GetSnapshotsProperties(IList<EmployeeBaseInfo> list)
        {
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].EmployeesSnapshotsList = SnapshotsManager.GetModelList(" userid =" + list[i].UserID.ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 绑定用户最新历史信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IList<EmployeeBaseInfo> GetCurrentSnapshotProperty(IList<EmployeeBaseInfo> list)
        {
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].EmployeesCurrentSnapshot = SnapshotsManager.GetTopModel(list[i].UserID);
                }
            }
            return list;
        }

        public static int updateStatus(int id, int status, SqlTransaction trans)
        {
            return dal.updateStatus(id, status, trans);
        }

        public static void Update(EmployeeBaseInfo employeeBase, SqlTransaction trans)
        {
            dal.Update(employeeBase, trans);
        }

        public static void updateUserPhoto(int id, string photofilename)
        {
            dal.updateUserPhoto(id, photofilename);
        }

        [AjaxPro.AjaxMethod]
        public static List<ESP.Framework.Entity.DepartmentPositionInfo> GetPositionsByDepId(int depId)
        {
            return (List<ESP.Framework.Entity.DepartmentPositionInfo>)ESP.Framework.BusinessLogic.DepartmentPositionManager.GetByDepartment(depId);
        }

        /// <summary>
        /// 批量更新员工和历史的数据
        /// </summary>
        /// <param name="emplist"></param>
        /// <param name="snaplist"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static int UpdateEmployees(List<EmployeeBaseInfo> emplist, List<SnapshotsInfo> snaplist, string log)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ESP.HumanResource.Entity.EmployeeBaseInfo tp2 in emplist)
                    {
                        dal.Update(tp2, trans);
                    }
                    foreach (ESP.HumanResource.Entity.SnapshotsInfo tp1 in snaplist)
                    {

                        SnapshotsManager.Add(tp1, trans);
                    }
                    returnValue = 1;
                    ESP.Logging.Logger.Add(log);
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
        /// 获得用户的工作地部门ID数组
        /// </summary>
        /// <param name="userids">用户ID字符串</param>
        /// <returns></returns>
        public static string[] GetUserWorkDepartmentID(string userids)
        {
            return dal.GetUserWorkDepartmentID(userids);
        }

        /// <summary>
        /// 获得通讯录人员部门信息
        /// </summary>
        /// <returns>返回通讯录人员部门信息</returns>
        public static DataSet GetAddressBookList(int userid)
        {
            return dal.GetAddressBookList(userid);
        }
        #endregion  成员方法
    }
}
