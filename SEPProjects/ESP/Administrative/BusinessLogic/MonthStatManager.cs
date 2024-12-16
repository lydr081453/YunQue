using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;

namespace ESP.Administrative.BusinessLogic
{
    public class MonthStatManager
    {
        private readonly ESP.Administrative.DataAccess.MonthStatDataProvider dal = new ESP.Administrative.DataAccess.MonthStatDataProvider();
        public MonthStatManager()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Administrative.Entity.MonthStatInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.MonthStatInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 更新一组数据
        /// </summary>
        public int Update(List<ESP.Administrative.Entity.MonthStatInfo> list,List<ESP.Administrative.Entity.ApproveLogInfo> listAdd,List<ESP.Administrative.Entity.ApproveLogInfo> listUpdate)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (list != null && list.Count > 0)
                    {
                        foreach (ESP.Administrative.Entity.MonthStatInfo model in list)
                        {
                            returnValue = dal.Update(model, trans);

                            AttGracePeriodDataProvider manager = new AttGracePeriodDataProvider();
                            AttGracePeriodInfo attGracePeriodModel = new AttGracePeriodInfo();
                            attGracePeriodModel.UserID = model.UserID;
                            attGracePeriodModel.UserCode = model.UserCode;
                            attGracePeriodModel.EmployeeName = model.EmployeeName;
                            attGracePeriodModel.BeginTime = DateTime.Now;
                            attGracePeriodModel.EndTime = DateTime.Now.AddDays(Status.ApproveGracePeriodDays);
                            ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID());
                            attGracePeriodModel.Remark = userModel.FullNameCN + "审批驳回" + model.EmployeeName + "的月度考勤系统自动开通提交限制";
                            attGracePeriodModel.CreateTime = DateTime.Now;
                            attGracePeriodModel.OperatorID = userModel.UserID;
                            attGracePeriodModel.OperatorName = userModel.FullNameCN;
                            attGracePeriodModel.UpdateTime = DateTime.Now;
                            manager.Add(attGracePeriodModel);
                        }
                    }
                    if (listAdd != null && list.Count > 0)
                    {
                        foreach (ESP.Administrative.Entity.ApproveLogInfo infoadd in listAdd)
                        {
                            returnValue = new ApproveLogManager().Add(infoadd, conn, trans);
                        }
                    }
                    if (listUpdate != null && listUpdate.Count > 0)
                    {
                        foreach (ESP.Administrative.Entity.ApproveLogInfo infoupdate in listUpdate)
                        {
                            returnValue = new ApproveLogManager().Update(infoupdate, conn, trans);
                        }
                    }
                    ESP.Logging.Logger.Add("Update More MonthStatInfo.", "AdiministrativeWeb", ESP.Logging.LogLevel.Information);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    returnValue = 0;
                    ESP.Logging.Logger.Add(ex.ToString(), "AdiministrativeWeb", ESP.Logging.LogLevel.Error, ex);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新一组数据
        /// </summary>
        public int Update(ESP.Administrative.Entity.MonthStatInfo monthModel, ESP.Administrative.Entity.ApproveLogInfo appModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (monthModel != null)
                    {
                            returnValue = dal.Update(monthModel, trans);
                    }
                    if (appModel != null)
                    {
                            returnValue = new ApproveLogManager().Update(appModel, conn, trans);
                    }
                    ESP.Logging.Logger.Add("Update More MonthStatInfo.", "AdiministrativeWeb", ESP.Logging.LogLevel.Information);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    returnValue = 0;
                    ESP.Logging.Logger.Add(ex.ToString(), "AdiministrativeWeb", ESP.Logging.LogLevel.Error, ex);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新一组数据
        /// </summary>
        public int Update(ESP.Administrative.Entity.MonthStatInfo monthModel, ESP.Administrative.Entity.ApproveLogInfo appModel,  ESP.Administrative.Entity.ApproveLogInfo newModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (monthModel != null)
                    {
                        returnValue = dal.Update(monthModel, trans);
                    }
                    if (appModel != null)
                    {
                        returnValue = new ApproveLogManager().Update(appModel, conn, trans);
                    }
                    if (newModel != null)
                    {
                        returnValue = new ApproveLogManager().Add(newModel, conn, trans);
                    }
                    ESP.Logging.Logger.Add("Update More MonthStatInfo.", "AdiministrativeWeb", ESP.Logging.LogLevel.Information);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    returnValue = 0;
                    ESP.Logging.Logger.Add(ex.ToString(), "AdiministrativeWeb", ESP.Logging.LogLevel.Error, ex);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public int Delete(int userid,int year ,int month)
        {
            return dal.Delete(userid,year,month);
        }

        /// <summary>
        /// 添加导入的批量数据
        /// </summary>
        public int AddandUpdate(List<ESP.Administrative.Entity.MonthStatInfo> listUpdate, List<ESP.Administrative.Entity.MonthStatInfo> listAdd, string log)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ESP.Administrative.Entity.MonthStatInfo tp2 in listAdd)
                    {
                        returnValue = dal.Add(tp2, trans);
                    }
                    foreach (ESP.Administrative.Entity.MonthStatInfo tp1 in listUpdate)
                    {

                        returnValue = dal.Update(tp1,trans);
                    }
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
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.MonthStatInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得员工某年某月的考勤记录
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public ESP.Administrative.Entity.MonthStatInfo GetMonthStatInfo(int userid, int year, int month)
        {
            return dal.GetMonthStatInfo(userid, year, month);
        }

        /// <summary>
        /// 获得员工某年某月的考勤记录
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public ESP.Administrative.Entity.MonthStatInfo GetMonthStatInfo(int userid, int year, int month, int state)
        {
            return dal.GetMonthStatInfo(userid, year, month, state);
        }

        /// <summary>
        /// 获得指定ID的考勤记录
        /// </summary>
        /// <param name="ID">编号</param>        
        /// <returns>返回一个事由类型的集合</returns>
        public List<ESP.Administrative.Entity.MonthStatInfo> GetMonthStatsList(string stringWhere)
        {
            DataSet ds = dal.GetList(" ID in ( " + stringWhere + ")");
            List<ESP.Administrative.Entity.MonthStatInfo> list = new List<ESP.Administrative.Entity.MonthStatInfo>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null
                && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.Administrative.Entity.MonthStatInfo model = new ESP.Administrative.Entity.MonthStatInfo();
                    model.PopupData(dr);

                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得员工某年某月的考勤记录
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public ESP.Administrative.Entity.MonthStatInfo GetMonthStatInfoApprove(int userid, int year, int month)
        {
            return dal.GetMonthStatInfoApprove(userid, year, month);
        }

        public ESP.Administrative.Entity.MonthStatInfo GetMonthStatInfoApprove(int userid, int year, int month,SqlTransaction trans)
        {
            return dal.GetMonthStatInfoApproveByTrans(userid, year, month,trans);
        }

        /// <summary>
        /// 获得用户全年考情累计数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="year">年份</param>
        /// <returns>返回一个累计数</returns>
        public decimal GetUserAllYearSickLeaveDay(int userId, int year, Status.AttendanceStatItem item)
        {
            decimal WorkHours = 0; // 工作天数
            decimal LateCount = 0;  // 迟到次数
            decimal LeaveEarlyCount = 0;  // 早退次数
            decimal SickLeaveHours = 0;  // 病假天数
            decimal AffairLeaveHours = 0;  // 事假天数
            decimal AnnualLeaveDays = 0;  // 年假天数
            decimal LastAnnualDays = 0;  // 补年假天数
            decimal MaternityLeaveDays = 0;  // 产假天数
            decimal MarriageLeaveDays = 0;  // 婚嫁天数
            decimal FuneralLeaveDays = 0;  // 丧家天数
            decimal AbsentDays = 0;  // 旷工天数
            decimal OverTimeHours = 0;  // OT天数
            decimal EvectionDays = 0;  // 出差天数
            decimal EgressHours = 0;  // 外出天数
            
            decimal returnNum = 0;
            if (userId != 0 && year != 0)
            {
                DataSet ds = dal.GetMonthStatInfo(userId, year);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        WorkHours += decimal.Parse(dr["WorkHours"].ToString());
                        LateCount += decimal.Parse(dr["LateCount"].ToString());
                        LeaveEarlyCount += decimal.Parse(dr["LeaveEarlyCount"].ToString());
                        SickLeaveHours += decimal.Parse(dr["SickLeaveHours"].ToString());
                        AffairLeaveHours += decimal.Parse(dr["AffairLeaveHours"].ToString());
                        AnnualLeaveDays += decimal.Parse(dr["AnnualLeaveDays"].ToString());
                        LastAnnualDays +=dr["LastAnnualDays"].ToString()==""? 0 : decimal.Parse(dr["LastAnnualDays"].ToString());
                        MaternityLeaveDays += decimal.Parse(dr["MaternityLeaveHours"].ToString());
                        MarriageLeaveDays += decimal.Parse(dr["MarriageLeaveHours"].ToString());
                        FuneralLeaveDays += decimal.Parse(dr["FuneralLeaveHours"].ToString());
                        AbsentDays += decimal.Parse(dr["AbsentDays"].ToString());
                        OverTimeHours += decimal.Parse(dr["OverTimeHours"].ToString());
                        EvectionDays += decimal.Parse(dr["EvectionDays"].ToString());
                        EgressHours += decimal.Parse(dr["EgressHours"].ToString());
                    }
                   
                    // 判断要返回给客户的累计值
                    switch (item)
                    { 
                        case Status.AttendanceStatItem.AbsentDays:
                            returnNum = AbsentDays;
                            break;
                        case Status.AttendanceStatItem.AffairLeaveHours:
                            returnNum = AffairLeaveHours;
                            break;
                        case Status.AttendanceStatItem.AnnualLeaveDays:
                            returnNum = AnnualLeaveDays;
                            break;
                        case Status.AttendanceStatItem.LastAnnualDays:
                            returnNum = LastAnnualDays;
                            break;
                        case Status.AttendanceStatItem.EgressHours:
                            returnNum = EgressHours;
                            break;
                        case Status.AttendanceStatItem.EvectionDays:
                            returnNum = EvectionDays;
                            break;
                        case Status.AttendanceStatItem.FuneralLeaveDays:
                            returnNum = FuneralLeaveDays;
                            break;
                        case Status.AttendanceStatItem.LateCount:
                            returnNum = LateCount;
                            break;
                        case Status.AttendanceStatItem.LeaveEarlyCount:
                            returnNum = LeaveEarlyCount;
                            break;
                        case Status.AttendanceStatItem.MarriageLeaveDays:
                            returnNum = MarriageLeaveDays;
                            break;
                        case Status.AttendanceStatItem.MaternityLeaveDays:
                            returnNum = MaternityLeaveDays;
                            break;
                        case Status.AttendanceStatItem.OverTimeHours:
                            returnNum = OverTimeHours;
                            break;
                        case Status.AttendanceStatItem.SickLeaveHours:
                            returnNum = SickLeaveHours;
                            break;
                        case Status.AttendanceStatItem.WorkHours:
                            returnNum = WorkHours;
                            break;
                        default:
                            returnNum = 0;
                            break;
                    }
                }
            }
            return returnNum;
        }

        /// <summary>
        /// 获得用户有权限查看的月考勤统计信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回月统计信息</returns>
        public Dictionary<int, ESP.Administrative.Entity.MonthStatInfo> GetMonthStatInfoByUserID(int userId, int year, int month)
        {
            string userids = new UserAttBasicInfoManager().GetStatUserIDs(userId);
            return dal.GetMonthStatInfoByUserID(userids, year, month);
        }
        /// <summary>
        /// 获得指定用户的年度考勤，不含指定月份额
        /// </summary>
        /// <param name="userid">指定用户</param>
        /// <param name="year">年份</param>
        /// <param name="excludeMonth">除指定月份外</param>
        /// <returns></returns>
        public List<MonthStatInfo> GetMonthStateByYear(int userid, int year,int excludeMonth)
        {
            return dal.GetMonthStateByYear(userid, year, excludeMonth);
        }

        /// <summary>
        /// 尝试去操作用户的某条数据，如果用户提交了该月的考勤记录，就不可以对数据任何操作，否则是可以操作的
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="dataEndTime">数据的结束时间</param>
        /// <returns>如果返回true则表示可以对该数据进行操作，否则不可以对该数据进行任何操作</returns>
        public bool TryOperateData(int userId, DateTime dataEndTime)
        {
            bool b = true;
            int year = dataEndTime.Year;
            int month = dataEndTime.Month;
            MonthStatInfo monthStatModel = GetMonthStatInfoApprove(userId, year, month);
            if (monthStatModel != null && monthStatModel.State != Status.MonthStatAppState_NoSubmit && monthStatModel.State != Status.MonthStatAppState_Overrule)
            {
                b = false;
            }
            return b;
        }
        #endregion  成员方法

        /// <summary>
        /// 是否申请了笔记本报销
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isEnoughWorkingDays"></param>
        /// <returns></returns>
        public bool isComputerRefund(int userid, int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            // 如果员工是当月离职
            ESP.HumanResource.Entity.DimissionInfo dimissionModel = ESP.HumanResource.BusinessLogic.DimissionManager.GetModelByUserID(userid);
            if (dimissionModel != null && dimissionModel.dimissionDate != null && dimissionModel.dimissionDate.Month == month)
            {
                daysInMonth = dimissionModel.dimissionDate.Day;
            }

            // 每月的第一天
            DateTime firstDate = new DateTime(year, month, 1);
            // 每月的最后一天
            DateTime lastDate = new DateTime(year, month, daysInMonth);

            List<RefundInfo> refundList = null;
            RefundManager refundManager = new RefundManager();
            refundList = refundManager.GetRefundInfos(userid, RefundType.NetBookType);
            // 如果为申请笔记本报销
            if (refundList == null || refundList.Count == 0)
                return false;
            return true;
        }
    }
}

