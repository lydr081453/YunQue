using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using System.Data;
using ESP.Administrative.BusinessLogic;
using System.Net.Mail;

namespace StatisticService
{
    /// <summary>
    /// 考勤统计数据计算
    /// </summary>
    public class DataExecute
    {
        #region 变量定义
        /// <summary>
        /// 用户信息
        /// </summary>
        IList<ESP.Framework.Entity.EmployeeInfo> employeesList = null;

        /// <summary>
        /// 用户打卡记录信息
        /// </summary>
        Dictionary<int, Dictionary<long, DateTime>> clockInDic = null;

        /// <summary>
        /// OT单信息
        /// </summary>
        Dictionary<int, List<SingleOvertimeInfo>> singleDic = null;

        /// <summary>
        /// 考勤事由信息
        /// </summary>
        Dictionary<int, List<MattersInfo>> mattersDic = null;

        /// <summary>
        /// 考勤基础信息集合
        /// </summary>
        Dictionary<int, DataRow> userAttBasicDic = null;

        /// <summary>
        /// 考勤业务操作对象
        /// </summary>
        AttendanceManager attMan = new AttendanceManager();

        /// <summary>
        /// 节假日信息集合
        /// </summary>
        HashSet<int> holidays = null;

        /// <summary>
        /// 用户入职信息
        /// </summary>
        Dictionary<int, ESP.HumanResource.Entity.EmployeeJobInfo> employeeJobDic = null;
        /// <summary>
        ///  日志记录对象
        /// </summary>
        private LogManager logger = new LogManager();
        /// <summary>
        /// 员工离职信息
        /// </summary>
        Dictionary<int, ESP.HumanResource.Entity.DimissionInfo> dimissionDic = null;
        #endregion
        public DataExecute()
        { }

        /// <summary>
        /// 计算考勤统计数据
        /// </summary>
        public void StatisticAttendanceInfos()
        {
            try
            {
               // logger.Add("StatisticService: 计算考勤数据" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string attendanceBeginDay = System.Configuration.ConfigurationManager.AppSettings["AttendanceBeginDay"];
                // 统计前一个月份的考勤的月份数
                int StatBeforeMonth = int.Parse(System.Configuration.ConfigurationManager.AppSettings["StatBeforeMonth"]);
                DateTime attendanceBeginTime = DateTime.Parse(attendanceBeginDay);
                DateTime prevTime = DateTime.Now.AddMonths(-StatBeforeMonth);
                for (int i = 0; i <= StatBeforeMonth; i++)
                {
                    // 当前年份
                    int year = prevTime.AddMonths(i).Year;
                    // 当前月份
                    int month = prevTime.AddMonths(i).Month;

                    AttendanceStatisticManager attendanceStatisticManager = new AttendanceStatisticManager();
                    attendanceStatisticManager.DeleteAttendanceStatInfos(year, month);

                    string userids = "";
                    // 获得用户考勤基本信息
                    UserAttBasicInfoManager userAttBasicManager = new UserAttBasicInfoManager();
                    userAttBasicDic = userAttBasicManager.GetAllUserAttBasicInfos();

                    // 获得用户打卡记录信息
                    ClockInManager clockInManager = new ClockInManager();
                    clockInDic = clockInManager.GetClockInTimes(year, month, 0);

                    // 获得用户的OT记录信息
                    SingleOvertimeManager singleManage = new SingleOvertimeManager();
                    singleDic = singleManage.GetStatSingleOvertimeInfos(year, month, 0);

                    // 获得用户考勤事由信息
                    MattersManager mattersManager = new MattersManager();
                    mattersDic = mattersManager.GetStatModelList(year, month, 0);

                    // 获得用户当月考勤记录信息
                    Dictionary<int, List<SingleOvertimeInfo>> monthSingleDic = singleManage.GetModelListByMonth(year, month);

                    // 获得节假日信息
                    holidays = new HolidaysInfoManager().GetHolidayListByMonth(year, month);

                    // 获得入离职信息
                    employeeJobDic = attMan.GetEmployeeJobInfo("");
                    dimissionDic = attMan.GetDimissionInfo();

                    // 循环考勤人员信息集合
                    foreach (KeyValuePair<int, DataRow> keyValue in userAttBasicDic)
                    {
                        AttendanceStatisticInfo attendanceStatisticModel = new AttendanceStatisticInfo();
                        int userid = keyValue.Key;
                        DataRow userAttBasicDr = keyValue.Value;

                        // 入职信息
                        ESP.HumanResource.Entity.EmployeeJobInfo employeeJobModel;
                        if (!employeeJobDic.TryGetValue(userid, out employeeJobModel))
                        {
                            employeeJobModel = null;
                        }
                        // 离职信息
                        ESP.HumanResource.Entity.DimissionInfo dimissionModel;
                        if (!dimissionDic.TryGetValue(userid, out dimissionModel))
                        {
                            dimissionModel = null;
                        }

                        // OT事由信息集合
                        List<SingleOvertimeInfo> singleList;
                        if (!singleDic.TryGetValue(userid, out singleList))
                        {
                            singleList = new List<SingleOvertimeInfo>();
                        }

                        // 考勤事由信息集合
                        List<MattersInfo> mattersList;
                        if (!mattersDic.TryGetValue(userid, out mattersList))
                        {
                            mattersList = new List<MattersInfo>();
                        }
                        UserAttBasicInfo userAttBasicModel = new UserAttBasicInfo();
                        userAttBasicModel.PopupData(userAttBasicDr);

                        // 当月OT事由信息集合
                        List<SingleOvertimeInfo> monthSingleList;
                        if (!monthSingleDic.TryGetValue(userid, out monthSingleList))
                        {
                            monthSingleList = new List<SingleOvertimeInfo>();
                        }

                        attendanceStatisticModel.UserID = userAttBasicModel.Userid;
                        attendanceStatisticModel.EmployeeName = userAttBasicModel.EmployeeName;
                        attendanceStatisticModel.UserName = userAttBasicModel.UserName;
                        attendanceStatisticModel.UserCode = userAttBasicModel.UserCode;
                        attendanceStatisticModel.Level1ID = int.Parse(userAttBasicDr["level1id"].ToString());
                        attendanceStatisticModel.Level2ID = int.Parse(userAttBasicDr["level2id"].ToString());
                        attendanceStatisticModel.Level3ID = int.Parse(userAttBasicDr["level3id"].ToString());
                        attendanceStatisticModel.Department = userAttBasicDr["Department"].ToString();
                        attendanceStatisticModel.Position = userAttBasicDr["DepartmentPositionName"].ToString();
                        attendanceStatisticModel.AttendanceYear = year;
                        attendanceStatisticModel.AttendanceMonth = month;
                        attendanceStatisticModel.OverTimeCount = monthSingleList.Count;
                        attendanceStatisticModel.AttendanceType = userAttBasicModel.AttendanceType;

                        Dictionary<long, DateTime> clockInDictionary;
                        if (!clockInDic.TryGetValue(userid, out clockInDictionary))
                        {
                            clockInDictionary = new Dictionary<long, DateTime>();
                        }

                        attMan.GetMonthStat(userAttBasicModel, year, month, clockInDictionary, singleList, mattersList, employeeJobModel, ref attendanceStatisticModel, holidays, dimissionModel);
                        attendanceStatisticManager.Add(attendanceStatisticModel);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Add("StatisticService: " + ex.Message + "  " + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 考勤异常提醒
        /// </summary>
        public void AttendanceRemind()
        {
            DateTime today = DateTime.Now;
            int year = today.Year;
            int month = today.Month;
            // 考勤统计信息数据对象。
            AttendanceStatisticManager attendanceStatisticManager = new AttendanceStatisticManager();
            DataSet ds = attendanceStatisticManager.GetAttendanceRemind(year, month);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string userEmail = dr["Email"].ToString();
                    string username = dr["EmployeeName"].ToString();
                    if (!string.IsNullOrEmpty(userEmail))
                    {

                        ESP.Mail.MailManager.Send("考勤提醒", username + "您好，您近期的考勤存在异常，请您登录内网系统进行处理。",
                            true, new MailAddress[] { new MailAddress(userEmail) });
                    }
                }
            }
        }

    }
}
