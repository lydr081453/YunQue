using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using System.Web;
using System.Collections;

namespace ESP.Administrative.BusinessLogic
{
    public class AttendanceManager
    {
        #region 变量定义
        private ESP.Administrative.DataAccess.AttendanceDataProvider dal = new ESP.Administrative.DataAccess.AttendanceDataProvider();
        /// <summary>
        /// 各月考勤业务实现类
        /// </summary>
        private MonthStatManager monthManager = new MonthStatManager();
        /// <summary>
        /// 请假业务实现类
        /// </summary>
        private LeaveManager leaveManager = new LeaveManager();
        /// <summary>
        /// 审批记录页面
        /// </summary>
        private ApproveLogManager appLogManager = new ApproveLogManager();
        /// <summary>
        /// 考勤业务实现类
        /// </summary>
        private MattersManager mattersManager = new MattersManager();
        /// <summary>
        /// 打卡记录信息对象
        /// </summary>
        private ClockInManager clockInManager = new ClockInManager();
        #endregion

        public AttendanceManager()
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
        /// <param name="ID">数据的ID值</param>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">要添加的考勤实体对象</param>
        public int Add(AttendanceInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">要更新的考勤实体对象</param>
        public void Update(AttendanceInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="ID">待删除的数据ID</param>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="ID">数据ID值</param>
        public AttendanceInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
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
        /// 得到用户某天的出勤信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="time">时间</param>
        /// <returns>返回一个考勤实体对象</returns>
        public AttendanceInfo GetModel(int UserID, DateTime time)
        {
            String strSql = string.Format(" (AttendanceDate between '{0} 00:00:00' and '{0} 23:59:59') and userID={1} ", time.ToString("yyyy-MM-dd"), UserID);
            return dal.GetModel(strSql);
        }

        /// <summary>
        /// 获得用户的出勤信息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        /// <returns>返回用户一个月的考勤记录信息</returns>
        public List<AttendanceInfo> GetAttModelList(int UserID, int Year, int Month)
        {
            if (Year == 0 || Month == 0)
                return null;

            string strSql = string.Format(" (AttendanceDate between '{0}-{1}-01 00:00:00' and '{2}-{3}-01 00:00:00') and userID = {4}", Year, Month, (Month == 12 ? Year + 1 : Year), (Month == 12 ? 1 : Month + 1), UserID);
            List<AttendanceInfo> list = new List<AttendanceInfo>();
            DataSet ds = GetList(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AttendanceInfo model = new AttendanceInfo();
                    model.PopupData(ds.Tables[0].Rows[i]);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得所有用户的出勤信息
        /// </summary>        
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        /// <returns>返回所有用户某个月的考勤信息</returns>
        public List<AttendanceInfo> GetAttModelList(int Year, int Month)
        {
            if (Year == 0 || Month == 0)
                return null;

            string strSql = string.Format(" (AttendanceDate between '{0}-{1}-01 00:00:00' and '{2}-{3}-01 00:00:00')", Year, Month, (Month == 12 ? Year + 1 : Year), (Month == 12 ? 1 : Month + 1));
            List<AttendanceInfo> list = new List<AttendanceInfo>();
            DataSet ds = GetList(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AttendanceInfo model = new AttendanceInfo();
                    model.PopupData(ds.Tables[0].Rows[i]);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 判断用户是否迟到（打卡上班时间是该用户上班时间点加5分钟和旷工时间点之间）
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <param name="AttDate">考勤上班时间</param>
        /// <returns></returns>
        public bool CheckIsLate(int Userid, DateTime AttDate, CommuterTimeInfo commuterTimeModel)
        {
            bool b = false;
            if (AttDate.ToString("yyyy-MM-dd") != "1900-01-01"
                && AttDate >= AttDate.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).AddMinutes(Status.GoWorkTime_BufferMinute)
                && AttDate < AttDate.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).AddHours(Status.NumberOfHours_Late))
                b = true;

            return b;
        }

        /// <summary>
        /// 判断是否旷工
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <param name="AttDate">考勤日期</param>
        /// <returns></returns>
        public double CheckIsAbsent(int Userid, DateTime AttDate, CommuterTimeInfo commuterTimeModel)
        {
            double b = 0;
            if (AttDate.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                if (AttDate >= AttDate.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).AddHours(Status.GoWorkTime_AMGapPM))
                    b = 1;
                else if (AttDate >= AttDate.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).AddHours(Status.NumberOfHours_Late))
                    b = 0.5;
            }
            return b;
        }

        /// <summary>
        /// 判断是否是早退
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <param name="AttDate">下班打卡时间</param>
        /// <param name="date">考勤日期</param>
        /// <returns></returns>
        public bool CheckIsLeaveEarly(int Userid, DateTime AttDate, DateTime date, CommuterTimeInfo commuterTimeModel)
        {
            bool b = false;
            if (AttDate.ToString("yyyy-MM-dd") != "1900-01-01" &&
                AttDate < date.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay))
                b = true;
            return b;
        }

        /// <summary>
        /// 判断是否是OT, 并且OT时间超过九点未超过十点
        /// </summary>
        /// <param name="span">被检测时间</param>
        /// <returns>如果是OT返回true，否则返回false</returns>
        public bool CheckIsOverTime(TimeSpan span)
        {
            bool b = false;
            if (span != null && span > Status.OverTime)
                b = true;
            return b;
        }

        /// <summary>
        /// 判断是否是OT, 并且OT时间超过十点
        /// </summary>
        /// <param name="span">被检测时间</param>
        /// <returns>如果是OT返回true，否则返回false</returns>
        public bool CheckIsOverTwentyTwo(TimeSpan span)
        {
            bool b = false;
            if (span != null && span > Status.OverTwentyTwo)
                b = true;
            return b;
        }

        /// <summary>
        /// 判断是否假期，如果是节假日返回true，否则返回false。
        /// </summary>
        /// <param name="date">被检测时间</param>
        /// <returns>如果是则返回true, 否则返回false</returns>
        public bool CheckIsHoliday(DateTime date)
        {
            bool b = false;
            if (date != null)
            {
                HolidaysInfoManager holiManager = new HolidaysInfoManager();
                HolidaysInfo holidaysinfo = holiManager.GetHolideysInfoByDatetime(date);
                if (holidaysinfo != null && holidaysinfo.ID > 0)
                    b = true;
            }
            return b;
        }

        /// <summary>
        /// 提交考勤审批记录信息
        /// </summary>
        /// <param name="approveloginfo">考勤记录</param>
        /// <param name="approveloginfo">审批信息</param>
        /// <returns>修改成功返回true,否则返回false</returns>
        public bool SubApproveInfo(MonthStatInfo monthstatinfo, ApproveLogInfo approveloginfo)
        {
            bool b = false;
            if (monthstatinfo != null && approveloginfo != null)
            {
                monthManager.Update(monthstatinfo);
                appLogManager.Update(approveloginfo);
            }
            return b;
        }

        /// <summary>
        /// 通过请假单ID获得考勤记录日期
        /// </summary>
        /// <returns>返回一个考勤记录信息集合</returns>
        public void UpdateAttendanceByLeaveID(int leaveID)
        {
            string strsql = " LeaveID=" + leaveID + " and deleted='false'";
            DataSet ds = dal.GetList(strsql);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AttendanceInfo attendanceinfo = new AttendanceInfo();
                    attendanceinfo.PopupData(dr);
                    attendanceinfo.LeaveID = 0;
                    attendanceinfo.LeaveType = 0;
                    attendanceinfo.IsLeave = false;
                    this.Update(attendanceinfo);
                }
            }
        }

        /// <summary>
        /// 通过考勤时间计算默认的考勤情况
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="attendanceDateTime">考勤时间</param>
        /// <param name="goWorkTime">上班时间</param>
        /// <param name="OffWorkTime">下班时间</param>
        /// <param name="isLate">输出参数，是否迟到</param>
        /// <param name="isAMAbsent">输出参数，是否上午旷工</param>
        /// <param name="isPMAbsent">输出参数，是否下午旷工</param>
        /// <param name="isLeaveEarly">输出参数，是否早退</param>
        public void CalDefaultMatters(int UserID, DateTime attendanceDateTime, DateTime goWorkTime, DateTime OffWorkTime,
            out bool isLate, out bool isAMAbsent, out bool isPMAbsent, out bool isLeaveEarly, out TimeSpan span, CommuterTimeInfo commuterTimeModel)
        {
            // 异常时间数
            span = new TimeSpan(0);
            // 是否迟到
            isLate = false;
            // 是否上午旷工
            isAMAbsent = false;
            // 是否下午旷工
            isPMAbsent = false;
            // 是否早退
            isLeaveEarly = false;

            // 实际应该上班时间
            DateTime shouldGoWorkTime = attendanceDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
            // 实际应该下班时间
            DateTime shouldOffworkTime = attendanceDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);

            TimeSpan workTime = shouldOffworkTime - shouldGoWorkTime;
            int hours = workTime.Hours;
            int minutes = workTime.Minutes;
            int seconds = workTime.Seconds;
            if (workTime.TotalHours > Status.WorkingHours)
            {
                hours = Status.WorkingHours;
                minutes = 0;
                seconds = 0;
            }
            workTime = new TimeSpan(hours, minutes, seconds);

            // 如果上下班时间都为空则算为旷工
            if (goWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime
                && OffWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime
                && attendanceDateTime.Date < DateTime.Now.Date)
            {
                isPMAbsent = true;
                span = span.Add(workTime);
            }
            // 如果上班时间,下班时间为空，设置为旷工一天
            else if ((goWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime || OffWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime || goWorkTime == OffWorkTime)
                && attendanceDateTime.Date < DateTime.Now.Date)
            {
                isPMAbsent = true;
                span = span.Add(workTime);
            }
            // 如果下班时间为空，设置为早退
            //else if (OffWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime
            //    && attendanceDateTime.Date < DateTime.Now.Date)
            //{
            //    isPMAbsent = true;
            //    span = span.Add(workTime);
            //}
            else
            {
                if (goWorkTime != null && attendanceDateTime.Date <= DateTime.Now.Date)
                {
                    if (goWorkTime >= shouldOffworkTime)
                    {
                        isPMAbsent = true;
                        span = span.Add(workTime);
                    }
                    else
                    {
                        // 迟到
                        if (this.CheckIsLate(UserID, goWorkTime, commuterTimeModel))
                        {
                            isLate = true;
                            span = span.Add(goWorkTime - shouldGoWorkTime);
                        }
                        else
                        {
                            // 旷工
                            double absent = this.CheckIsAbsent(UserID, goWorkTime, commuterTimeModel);
                            if (absent != 0)
                            {
                                if (absent == 0.5)
                                {
                                    isAMAbsent = true;
                                    if (goWorkTime > shouldGoWorkTime.AddHours(Status.AMWorkingHours))
                                    {
                                        span = span.Add(shouldGoWorkTime.AddHours(Status.AMWorkingHours) - shouldGoWorkTime);
                                    }
                                    else
                                    {
                                        span = span.Add(goWorkTime - shouldGoWorkTime);
                                    }
                                }
                                else if (absent == 1)
                                {
                                    isPMAbsent = true;
                                    span = span.Add(new TimeSpan(Status.AMWorkingHours, 0, 0));
                                    if (goWorkTime > shouldGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                                    {
                                        TimeSpan tempspan = goWorkTime - shouldGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM);
                                        span = span.Add(tempspan);
                                    }
                                }
                            }
                        }
                    }
                }
                if (OffWorkTime != null && attendanceDateTime.Date < DateTime.Now.Date)
                {
                    // 如果实际下班打卡时间小于应该到岗上班时间
                    if (OffWorkTime <= shouldGoWorkTime)
                    {
                        isPMAbsent = true;
                        span = span.Add(new TimeSpan(Status.WorkingHours, 0, 0));
                    }
                    // 如果实际下班时间小于上午下班时间
                    else if (OffWorkTime < shouldGoWorkTime.AddHours(Status.AMWorkingHours)
                        && OffWorkTime < shouldOffworkTime
                        && shouldGoWorkTime.AddHours(Status.AMWorkingHours) < shouldOffworkTime)
                    {
                        TimeSpan workingHours = workTime;
                        span = span.Add(new TimeSpan(workingHours.Hours - Status.AMWorkingHours, workingHours.Minutes, workingHours.Seconds));
                        span = span.Add(shouldGoWorkTime.AddHours(Status.AMWorkingHours) - OffWorkTime);
                        isPMAbsent = true;
                        //// 早退
                        //if (attendanceDateTime.Date != DateTime.Now.Date
                        //    && OffWorkTime != DateTime.MaxValue && OffWorkTime.Date == attendanceDateTime.Date
                        //    && this.CheckIsLeaveEarly(UserID, OffWorkTime))
                        //{
                        //    isLeaveEarly = true;
                        //    span = span.Add(shouldOffworkTime - OffWorkTime);
                        //}
                    }

                    else if ((OffWorkTime <= shouldGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM) && OffWorkTime >= shouldGoWorkTime.AddHours(Status.AMWorkingHours)) && OffWorkTime < shouldOffworkTime)
                    {
                        span = span.Add(new TimeSpan(Status.WorkingHours - Status.AMWorkingHours, 0, 0));
                        isPMAbsent = true;
                    }
                    else if ((OffWorkTime > shouldGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM) && this.CheckIsLeaveEarly(UserID, OffWorkTime, attendanceDateTime, commuterTimeModel) && OffWorkTime != DateTime.MaxValue) && OffWorkTime < shouldOffworkTime)
                    {
                        span = span.Add(shouldOffworkTime - OffWorkTime);
                        isLeaveEarly = true;
                    }
                    else if (OffWorkTime < shouldGoWorkTime.AddHours(Status.AMWorkingHours)
                        && OffWorkTime < shouldOffworkTime
                        && shouldGoWorkTime.AddHours(Status.AMWorkingHours) >= shouldOffworkTime
                        && this.CheckIsLeaveEarly(UserID, OffWorkTime, attendanceDateTime, commuterTimeModel)
                        && OffWorkTime != DateTime.MaxValue)
                    {
                        isLeaveEarly = true;
                        span = span.Add(shouldOffworkTime - OffWorkTime);
                    }
                }
            }
        }

        /// <summary>
        /// 根据用户的考勤事由计算用户的考勤情况
        /// </summary>
        /// <param name="isLate">是否迟到</param>
        /// <param name="isAMAbsent">是否上午旷工</param>
        /// <param name="isPMAbsent">是否下午旷工</param>
        /// <param name="isLeaveEarly">是否早退</param>
        /// <param name="mattersInfo">考勤事由对象</param>
        public void CalAttendance(ref bool isLate, ref bool isAMAbsent, ref bool isPMAbsent, ref bool isLeaveEarly,
            MattersInfo mattersInfo, CommuterTimeInfo commuterModel, DateTime GoWorkTime,
            DateTime OffWorkTime, DateTime day, ref long abnormityTicks)
        {
            DateTime beginTime = mattersInfo.BeginTime;
            DateTime endTime = mattersInfo.EndTime;
            DateTime dayGoWorkTime = day.Date.Add(commuterModel.GoWorkTime.TimeOfDay);
            DateTime dayOffWorkTime = day.Date.Add(commuterModel.OffWorkTime.TimeOfDay);
            if (GoWorkTime >= dayOffWorkTime)
            {
                GoWorkTime = DateTime.Parse(Status.EmptyTime);
                OffWorkTime = DateTime.Parse(Status.EmptyTime);
            }
            if (beginTime > dayOffWorkTime || endTime < dayGoWorkTime)
            {
                return;
            }
            else
            {
                // 判断结束时间是否大于下班时间，将结束时间设置为下班时间
                if (endTime > dayOffWorkTime)
                {
                    endTime = dayOffWorkTime;
                }
                // 判断开始时间是否小于上班时间，将开始时间设置为上班时间
                if (beginTime < dayGoWorkTime)
                {
                    beginTime = dayGoWorkTime;
                }
                // 如果没有打ka记录
                if ((GoWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime && OffWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime) || GoWorkTime.TimeOfDay >= commuterModel.OffWorkTime.TimeOfDay)
                {
                    if (DateTime.Parse(beginTime.ToString("yyyy-MM-dd HH:mm")) >= dayGoWorkTime
                        && DateTime.Parse(endTime.ToString("yyyy-MM-dd HH:mm")) <= dayOffWorkTime)
                    {
                        if (isLate)
                        {
                            isLate = false;
                        }
                        else if (isAMAbsent)
                        {
                            isAMAbsent = false;
                        }
                        else if (isPMAbsent)
                        {
                            isPMAbsent = false;
                        }
                    }

                    TimeSpan tmpMatter = (endTime - beginTime);
                    abnormityTicks -= tmpMatter.Ticks;
                }

                // 判断事由的开始时间是否和上班时间一样，如果一样再判断事由的结束时间和上班打卡时间
                if (DateTime.Parse(beginTime.ToString("yyyy-MM-dd HH:mm")) <= dayGoWorkTime)
                {
                    if (DateTime.Parse(endTime.ToString("yyyy-MM-dd HH:mm")) >= DateTime.Parse(GoWorkTime.ToString("yyyy-MM-dd HH:mm"))
                        || (DateTime.Parse(endTime.ToString("yyyy-MM-dd HH:mm")) == GoWorkTime.Date.Add(commuterModel.GoWorkTime.TimeOfDay).AddHours(Status.AMWorkingHours)
                        && CheckTimeIsInBreakTime(GoWorkTime, commuterModel)))
                    {
                        if (isLate)
                        {
                            isLate = false;
                        }
                        if (isAMAbsent)
                        {
                            isAMAbsent = false;
                        }
                        if (isPMAbsent)
                        {
                            isPMAbsent = false;
                        }
                    }
                }
                // 判断事由的结束时间是否和下班时间一样，如果一样再判断事由的开始时间和下班打卡时间
                if (DateTime.Parse(endTime.ToString("yyyy-MM-dd HH:mm")) >= dayOffWorkTime)
                {
                    if (DateTime.Parse(beginTime.ToString("yyyy-MM-dd HH:mm")) <= DateTime.Parse(OffWorkTime.ToString("yyyy-MM-dd HH:mm"))
                        || (DateTime.Parse(beginTime.ToString("yyyy-MM-dd HH:mm")) == OffWorkTime.Date.Add(commuterModel.GoWorkTime.TimeOfDay).AddHours(Status.GoWorkTime_AMGapPM)
                        && CheckTimeIsInBreakTime(OffWorkTime, commuterModel)))
                    {
                        if (isLeaveEarly)
                        {
                            isLeaveEarly = false;
                        }
                    }
                }

                TimeSpan tempSpan = endTime - beginTime;
                if (beginTime <= dayGoWorkTime.AddHours(Status.AMWorkingHours)
                    && endTime >= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                {
                    tempSpan = tempSpan.Add(new TimeSpan(-1, 0, 0));
                }

                if (GoWorkTime.ToString("yyyy-MM-dd") != Status.EmptyTime && OffWorkTime.ToString("yyyy-MM-dd") != Status.EmptyTime && GoWorkTime < commuterModel.OffWorkTime)
                {
                    if (beginTime <= GoWorkTime && endTime >= commuterModel.OffWorkTime)
                    {
                        TimeSpan tempTimeSpan = OffWorkTime - GoWorkTime;
                        if (GoWorkTime <= dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && OffWorkTime >= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            tempTimeSpan = tempTimeSpan.Add(new TimeSpan(-1, 0, 0));
                        }
                        else if (GoWorkTime <= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM)
                            && OffWorkTime >= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            TimeSpan tempTime = GoWorkTime - dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM);
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }
                        else if (GoWorkTime <= dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && OffWorkTime >= dayGoWorkTime.AddHours(Status.AMWorkingHours))
                        {
                            TimeSpan tempTime = dayGoWorkTime.AddHours(Status.AMWorkingHours) - OffWorkTime;
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }
                        else if (GoWorkTime > dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && OffWorkTime < dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            TimeSpan tempTime = GoWorkTime - OffWorkTime;
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }

                        tempSpan = tempSpan - tempTimeSpan;
                    }
                    else if (beginTime > GoWorkTime && endTime < OffWorkTime)   // 事由开始时间大于上班打卡时间，事由结束时间小于下班打卡时间
                    {
                        tempSpan = new TimeSpan(0, 0, 0);
                    }
                    else if (beginTime < GoWorkTime && (endTime <= OffWorkTime && endTime >= GoWorkTime)) // 事由开始时间小于上班打卡时间，事由结束时间小于下班打卡时间大于上班打卡时间
                    {
                        TimeSpan tempTimeSpan = endTime - GoWorkTime;
                        if (GoWorkTime <= dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && endTime >= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            tempTimeSpan = tempTimeSpan.Add(new TimeSpan(-1, 0, 0));
                        }
                        else if (GoWorkTime <= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM)
                            && endTime >= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            TimeSpan tempTime = GoWorkTime - dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM);
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }
                        else if (GoWorkTime <= dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && endTime >= dayGoWorkTime.AddHours(Status.AMWorkingHours))
                        {
                            TimeSpan tempTime = dayGoWorkTime.AddHours(Status.AMWorkingHours) - endTime;
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }
                        else if (GoWorkTime > dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && endTime < dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            TimeSpan tempTime = GoWorkTime - endTime;
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }
                        tempSpan = tempSpan - tempTimeSpan;

                        //tempSpan = tempSpan - (endTime - GoWorkTime);
                    }
                    else if ((beginTime >= GoWorkTime && beginTime <= OffWorkTime) && endTime > OffWorkTime)   // 事由开始时间小于下班打卡时间大于上班打卡时间，事由结束时间大于下班打卡时间
                    {
                        TimeSpan tempTimeSpan = OffWorkTime - beginTime;
                        if (beginTime <= dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && OffWorkTime >= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            tempTimeSpan = tempTimeSpan.Add(new TimeSpan(-1, 0, 0));
                        }
                        else if (beginTime <= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM)
                            && OffWorkTime >= dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            TimeSpan tempTime = beginTime - dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM);
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }
                        else if (beginTime <= dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && OffWorkTime >= dayGoWorkTime.AddHours(Status.AMWorkingHours))
                        {
                            TimeSpan tempTime = dayGoWorkTime.AddHours(Status.AMWorkingHours) - OffWorkTime;
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }
                        else if (beginTime > dayGoWorkTime.AddHours(Status.AMWorkingHours)
                            && OffWorkTime < dayGoWorkTime.AddHours(Status.GoWorkTime_AMGapPM))
                        {
                            TimeSpan tempTime = beginTime - OffWorkTime;
                            tempTimeSpan = tempTimeSpan.Add(tempTime);
                        }
                        tempSpan = tempSpan - tempTimeSpan;

                        //tempSpan = tempSpan - (OffWorkTime - beginTime);
                    }
                    else
                    {
                        tempSpan = new TimeSpan(0, 0, 0);
                    }

                    abnormityTicks -= tempSpan.Ticks;
                }
               // else if ((beginTime >= GoWorkTime && endTime >= OffWorkTime) && (GoWorkTime.ToString("yyyy-MM-dd") != Status.EmptyTime && OffWorkTime.ToString("yyyy-MM-dd") != Status.EmptyTime))
                else if ((GoWorkTime.ToString("yyyy-MM-dd") != Status.EmptyTime && OffWorkTime.ToString("yyyy-MM-dd") != Status.EmptyTime))
                {
                    if (endTime.TimeOfDay >= Status.PMGoTime && beginTime.TimeOfDay < Status.PMGoTime)
                    {
                        abnormityTicks -= (endTime - beginTime).Add(new TimeSpan(-1, 0, 0)).Ticks;
                    }
                    else
                    {
                        abnormityTicks -= (endTime - beginTime).Ticks;
                    }
                }
            }
        }

        /// <summary>
        /// 判断考勤事由是否审批通过
        /// </summary>
        /// <param name="mattersModel">考勤事由对象</param>
        /// <returns>如果审批通过就返回true,否则返回false</returns>
        public bool IsPass(MattersInfo mattersModel)
        {
            bool b = false;
            if (mattersModel != null)
            {
                if (mattersModel.MatterState == Status.MattersState_Passed)
                    b = true;
            }
            return b;
        }

        /// <summary>
        /// 获得考勤记录统计信息对象
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="attendanceDateTime"></param>
        /// <param name="goWorkTime"></param>
        /// <param name="OffWorkTime"></param>
        /// <param name="attendanceDataInfo"></param>
        public AttendanceDataInfo GetAttendanceItemInfo(int UserID, DateTime attendanceDateTime, DateTime goWorkTime, DateTime OffWorkTime, CommuterTimeInfo commuterTimeModel)
        {
            AttendanceDataInfo attendanceDataModel = new AttendanceDataInfo();
            // 获得事由信息集合
            List<MattersInfo> mattersList = mattersManager.GetMattersList(UserID, attendanceDateTime);

            #region 假期
            if (this.CheckIsHoliday(attendanceDateTime))
            {

                if (mattersList != null && mattersList.Count > 0)
                {
                    foreach (MattersInfo mattersInfo in mattersList)
                    {
                        // 判断事由类型是否是出差
                        if (mattersInfo.MatterType == Status.MattersType_Travel)
                        {
                            DateTime BeginTime = mattersInfo.BeginTime;
                            DateTime EndTime = mattersInfo.EndTime;
                            DateTime calBeginTime = new DateTime();
                            DateTime calEndTime = new DateTime();
                            if (attendanceDateTime.Date == BeginTime.Date)
                            {
                                calBeginTime = BeginTime;
                            }
                            else if (attendanceDateTime.Date > BeginTime.Date)
                            {
                                calBeginTime = attendanceDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                            }
                            if (attendanceDateTime.Date == EndTime.Date)
                            {
                                calEndTime = EndTime;
                            }
                            else if (attendanceDateTime.Date < EndTime.Date)
                            {
                                calEndTime = attendanceDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                            }
                            double totalHours = (calEndTime - calBeginTime).TotalHours;
                            if (totalHours >= 8)
                                totalHours = 8;
                            else
                                totalHours = 4;
                            attendanceDataModel.EvectionHours += (decimal)totalHours;
                        }
                        // 判断事由类型是否是丧假、产假、婚假
                        if (mattersInfo.MatterType == Status.MattersType_Bereavement ||
                            mattersInfo.MatterType == Status.MattersType_Marriage ||
                            mattersInfo.MatterType == Status.MattersType_Maternity ||
                            mattersInfo.MatterType == Status.MattersType_PeiChanJia)
                        {
                            DateTime BeginTime = mattersInfo.BeginTime;
                            DateTime EndTime = mattersInfo.EndTime;
                            DateTime calBeginTime = new DateTime();
                            DateTime calEndTime = new DateTime();
                            if (attendanceDateTime.Date == BeginTime.Date)
                            {
                                calBeginTime = BeginTime;
                            }
                            else if (attendanceDateTime.Date > BeginTime.Date)
                            {
                                calBeginTime = attendanceDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                            }
                            if (attendanceDateTime.Date == EndTime.Date)
                            {
                                calEndTime = EndTime;
                            }
                            else if (attendanceDateTime.Date < EndTime.Date)
                            {
                                calEndTime = attendanceDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                            }
                            double totalHours = (calEndTime - calBeginTime).TotalHours;
                            if (totalHours >= 8)
                                totalHours = 8;
                            if (mattersInfo.MatterType == Status.MattersType_Bereavement)
                                attendanceDataModel.FuneralLeaveHours += (decimal)totalHours;
                            if (mattersInfo.MatterType == Status.MattersType_Marriage)
                                attendanceDataModel.MarriageLeaveHours += (decimal)totalHours;
                            if (mattersInfo.MatterType == Status.MattersType_Maternity)
                                attendanceDataModel.MaternityLeaveHours += (decimal)totalHours;
                            if (mattersInfo.MatterType == Status.MattersType_PeiChanJia)
                                attendanceDataModel.PeiChanJia += (decimal)totalHours;
                        }
                    }
                }
            }
            #endregion
            #region 非假日
            else
            {
                if (commuterTimeModel != null)
                {
                    if (commuterTimeModel.AttendanceType == Status.UserBasicAttendanceType_Normal)
                    {
                        #region 按打卡时间计算考勤的默认情况
                        // 是否迟到
                        bool isLate = false;
                        // 是否上午旷工
                        bool isAMAbsent = false;
                        // 是否下午旷工
                        bool isPMAbsent = false;
                        // 是否早退
                        bool isLeaveEarly = false;
                        // 考勤异常时间数
                        TimeSpan abnormityTime = new TimeSpan(0);
                        this.CalDefaultMatters(UserID, attendanceDateTime, goWorkTime, OffWorkTime,
                            out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterTimeModel);
                        long abnormityTicks = abnormityTime.Ticks;
                        #endregion

                        #region 当天的考勤事由信息
                        if (mattersList != null && mattersList.Count > 0)
                        {
                            foreach (MattersInfo mattersInfo in mattersList)
                            {
                                this.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, mattersInfo, commuterTimeModel, goWorkTime, OffWorkTime, attendanceDateTime, ref abnormityTicks);
                                DateTime BeginTime = mattersInfo.BeginTime;
                                DateTime EndTime = mattersInfo.EndTime;
                                DateTime calBeginTime = new DateTime();
                                DateTime calEndTime = new DateTime();
                                if (attendanceDateTime.Date == BeginTime.Date)
                                {
                                    calBeginTime = BeginTime;
                                }
                                else if (attendanceDateTime.Date > BeginTime.Date)
                                {
                                    calBeginTime = attendanceDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                                }
                                if (attendanceDateTime.Date == EndTime.Date)
                                {
                                    calEndTime = EndTime;
                                }
                                else if (attendanceDateTime.Date < EndTime.Date)
                                {
                                    calEndTime = attendanceDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                                }
                                #region 判断考勤类型，计算考勤统计时间
                                switch (mattersInfo.MatterType)
                                {
                                    case Status.MattersType_Annual:  // 年假
                                        double totalAnnualHours =(calEndTime - calBeginTime).TotalHours;
                                        if (calBeginTime.Hour <= 12 && calEndTime.Hour > 12)
                                            totalAnnualHours = totalAnnualHours-1;
                                        attendanceDataModel.AnnualLeaveHours += (decimal)totalAnnualHours;
                                        break;
                                    case Status.MattersType_Annual_Last:  // 补年假
                                        double totalAnnualLastHours = (calEndTime - calBeginTime).TotalHours;
                                        if (calBeginTime.Hour <= 12 && calEndTime.Hour > 12)
                                            totalAnnualLastHours = totalAnnualLastHours - 1;
                                        attendanceDataModel.LastAnnualHours += (decimal)totalAnnualLastHours;
                                        break;
                                    case Status.MattersType_Bereavement: // 丧假
                                        double totalHours2 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours2 >= 8)
                                            totalHours2 = 8;
                                        else
                                            totalHours2 = 4;
                                        attendanceDataModel.FuneralLeaveHours += (decimal)totalHours2;
                                        break;
                                    case Status.MattersType_Leave:   // 事假
                                        double totalHours3 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan = new TimeSpan(3, 0, 0);
                                        TimeSpan span = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan);
                                        if (calBeginTime.TimeOfDay < span && span < calEndTime.TimeOfDay)
                                            totalHours3--;
                                        if (totalHours3 >= 8)
                                            totalHours3 = 8;

                                        attendanceDataModel.AffiairLeaveHours += (decimal)totalHours3;
                                        break;
                                    case Status.MattersType_Marriage:   // 婚假
                                        double totalHours4 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours4 >= 8)
                                            totalHours4 = 8;
                                        else
                                            totalHours4 = 4;
                                        attendanceDataModel.MarriageLeaveHours += (decimal)totalHours4;
                                        break;
                                    case Status.MattersType_Maternity:
                                    case Status.MattersType_PeiChanJia:   // 产假
                                        double totalHours5 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours5 >= 8)
                                            totalHours5 = 8;
                                        else
                                            totalHours5 = 4;
                                        attendanceDataModel.MaternityLeaveHours += (decimal)totalHours5;
                                        break;
                                    case Status.MattersType_OffTune:   // 调休
                                        double totalHours6 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours6 >= 8)
                                            totalHours6 = 8;
                                        else
                                            totalHours6 = 4;
                                        attendanceDataModel.OffTuneHours += (decimal)totalHours6;
                                        break;
                                    case Status.MattersType_Out:      // 外出
                                        double totalHours8 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan8 = new TimeSpan(3, 0, 0);
                                        TimeSpan span8 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan8);
                                        if (calBeginTime.TimeOfDay < span8 && span8 < calEndTime.TimeOfDay)
                                            totalHours8--;
                                        if (totalHours8 >= 8)
                                            totalHours8 = 8;

                                        attendanceDataModel.EgressHours += (decimal)totalHours8;
                                        break;
                                    case Status.MattersType_Sick:     // 病假
                                        double totalHours9 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan9 = new TimeSpan(3, 0, 0);
                                        TimeSpan span9 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan9);
                                        if (calBeginTime.TimeOfDay < span9 && span9 < calEndTime.TimeOfDay)
                                            totalHours9--;
                                        if (totalHours9 >= 8)
                                            totalHours9 = 8;

                                        attendanceDataModel.SickLeaveHours += (decimal)totalHours9;
                                        break;
                                    case Status.MattersType_Travel:   // 出差
                                        double totalHours10 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours10 >= 8)
                                            totalHours10 = 8;
                                        else
                                            totalHours10 = 4;
                                        attendanceDataModel.EvectionHours += (decimal)totalHours10;
                                        break;
                                    case Status.MattersType_Other:
                                        attendanceDataModel.Other += mattersInfo.MatterContent + ";\r\n";
                                        break;
                                    case Status.MattersType_PrenatalCheck:   // 产前检查
                                        double totalHours11 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan11 = new TimeSpan(3, 0, 0);
                                        TimeSpan span11 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan11);
                                        if (calBeginTime.TimeOfDay < span11 && span11 < calEndTime.TimeOfDay)
                                            totalHours11--;
                                        if (totalHours11 >= 8)
                                            totalHours11 = 8;

                                        attendanceDataModel.PrenatalCheckHours += (decimal)totalHours11;
                                        break;
                                    case Status.MattersType_Incentive:    // 奖励假
                                        double totalHours12 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours12 >= 8)
                                            totalHours12 = 8;
                                        else
                                            totalHours12 = 4;
                                        attendanceDataModel.IncentiveHours += (decimal)totalHours12;
                                        break;
                                }
                                #endregion
                            }
                            if (attendanceDataModel.Other.EndsWith(","))
                            {
                                attendanceDataModel.Other = attendanceDataModel.Other.Substring(0, attendanceDataModel.Other.Length - 1);
                            }
                        }
                        #endregion

                        #region 判断考勤默认事由情况
                        TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                        if (isLate && remainTime.TotalMinutes >= Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            attendanceDataModel.LateCount++;
                            if (isLeaveEarly)
                            {
                                attendanceDataModel.LeaveEarlyCount++;
                            }
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Late && remainTime.TotalHours <= Status.NumberOfHours_Absent)
                        {
                            if (isLate && isLeaveEarly)
                            {
                                attendanceDataModel.LateCount++;

                                attendanceDataModel.LeaveEarlyCount++;
                            }
                            else if (isLate)
                            {
                                attendanceDataModel.LateCount++;
                            }
                            else if (isLeaveEarly)
                            {
                                attendanceDataModel.LeaveEarlyCount++;
                            }
                            else
                            {
                                attendanceDataModel.AbsentHours += 4;
                            }

                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Absent)
                        {
                            if (isLate)
                            {
                                attendanceDataModel.LateCount++;

                                attendanceDataModel.AbsentHours += 4;
                            }
                            else if (isLeaveEarly)
                            {
                                attendanceDataModel.LeaveEarlyCount++;

                                attendanceDataModel.AbsentHours += 4;
                            }
                            else
                            {
                                attendanceDataModel.AbsentHours += 8;
                            }
                        }
                        else if (isLeaveEarly && remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            attendanceDataModel.LeaveEarlyCount++;
                        }
                        else if (remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            if (goWorkTime > attendanceDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay)
                                && (goWorkTime - attendanceDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay)).TotalMinutes >= Status.GoWorkTime_BufferMinute)
                            {
                                attendanceDataModel.LateCount++;
                            }
                            else if (OffWorkTime < attendanceDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)
                                && attendanceDateTime.Date < DateTime.Now.Date
                                && attendanceDateTime.Date < DateTime.Now.Date
                                && (OffWorkTime - attendanceDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)).TotalMinutes >= 1)
                            {
                                attendanceDataModel.LeaveEarlyCount++;
                            }
                        }
                        #endregion
                    }
                    else if (commuterTimeModel.AttendanceType == Status.UserBasicAttendanceType_Special)
                    {
                        #region 按打卡时间计算考勤的默认情况
                        // 是否迟到
                        bool isLate = false;
                        // 是否上午旷工
                        bool isAMAbsent = false;
                        // 是否下午旷工
                        bool isPMAbsent = false;
                        // 是否早退
                        bool isLeaveEarly = false;
                        // 考勤异常时间数
                        TimeSpan abnormityTime = new TimeSpan(0);

                        if (goWorkTime >= new DateTime(goWorkTime.Year, goWorkTime.Month, goWorkTime.Day, commuterTimeModel.OffWorkTime.Hour, commuterTimeModel.OffWorkTime.Minute, commuterTimeModel.OffWorkTime.Second))
                        {
                            goWorkTime = DateTime.Parse(Status.EmptyTime);
                            OffWorkTime = DateTime.Parse(Status.EmptyTime);
                        }

                        if (goWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime || OffWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime || goWorkTime == OffWorkTime)
                        {
                            if (attendanceDateTime.Date < DateTime.Now.Date)
                            {
                                isPMAbsent = true;
                                TimeSpan workingHours = commuterTimeModel.OffWorkTime - commuterTimeModel.GoWorkTime;
                                int hours = workingHours.Hours;
                                int minutes = workingHours.Minutes;
                                int seconds = workingHours.Seconds;
                                if (workingHours.TotalHours > Status.WorkingHours)
                                {
                                    hours = Status.WorkingHours;
                                    minutes = 0;
                                    seconds = 0;
                                }

                                abnormityTime += abnormityTime.Add(new TimeSpan(hours, minutes, seconds));
                            }
                        }
                        else
                        {
                            if (attendanceDateTime.Date < DateTime.Now.Date)
                            {
                                TimeSpan workingHours = commuterTimeModel.OffWorkTime - commuterTimeModel.GoWorkTime;
                                int hours = workingHours.Hours;
                                int minutes = workingHours.Minutes;
                                int seconds = workingHours.Seconds;
                                DateTime currentOffTime = new DateTime(goWorkTime.Year, goWorkTime.Month, goWorkTime.Day, commuterTimeModel.OffWorkTime.Hour, commuterTimeModel.OffWorkTime.Minute, commuterTimeModel.OffWorkTime.Second);
                                TimeSpan workingHours2 = currentOffTime - goWorkTime;
                                if (OffWorkTime < currentOffTime)
                                {
                                    workingHours2 = OffWorkTime - goWorkTime;
                                }
                                int hours2 = workingHours2.Hours;
                                int minutes2 = workingHours2.Minutes;
                                int seconds2 = workingHours2.Seconds;
                                abnormityTime += abnormityTime.Add(new TimeSpan(hours - hours2, minutes - minutes2, seconds - seconds2));
                            }
                        }


                        long abnormityTicks = abnormityTime.Ticks;
                        #endregion

                        #region 当天的考勤事由信息
                        if (mattersList != null && mattersList.Count > 0)
                        {
                            foreach (MattersInfo mattersInfo in mattersList)
                            {
                                this.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, mattersInfo, commuterTimeModel, goWorkTime, OffWorkTime, attendanceDateTime, ref abnormityTicks);
                                DateTime BeginTime = mattersInfo.BeginTime;
                                DateTime EndTime = mattersInfo.EndTime;
                                DateTime calBeginTime = new DateTime();
                                DateTime calEndTime = new DateTime();
                                if (attendanceDateTime.Date == BeginTime.Date)
                                {
                                    calBeginTime = BeginTime;
                                }
                                else if (attendanceDateTime.Date > BeginTime.Date)
                                {
                                    calBeginTime = attendanceDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                                }
                                if (attendanceDateTime.Date == EndTime.Date)
                                {
                                    calEndTime = EndTime;
                                }
                                else if (attendanceDateTime.Date < EndTime.Date)
                                {
                                    calEndTime = attendanceDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                                }
                                #region 判断考勤类型，计算考勤统计时间
                                switch (mattersInfo.MatterType)
                                {
                                    case Status.MattersType_Annual:  // 年假
                                        double totalAnnualHours = (calEndTime - calBeginTime).TotalHours;
                                        if (calBeginTime.Hour <= 12)
                                            totalAnnualHours = totalAnnualHours - 1;
                                        attendanceDataModel.AnnualLeaveHours += (decimal)totalAnnualHours;
                                        break;
                                    case Status.MattersType_Annual_Last:  // 补年假
                                        double totalAnnualLastHours = (calEndTime - calBeginTime).TotalHours;
                                        if (calBeginTime.Hour <= 12)
                                            totalAnnualLastHours = totalAnnualLastHours - 1;
                                        attendanceDataModel.LastAnnualHours += (decimal)totalAnnualLastHours;
                                        break;
                                    case Status.MattersType_Bereavement: // 丧假
                                        double totalHours2 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours2 >= 8)
                                            totalHours2 = 8;
                                        else
                                            totalHours2 = 4;
                                        attendanceDataModel.FuneralLeaveHours += (decimal)totalHours2;
                                        break;
                                    case Status.MattersType_Leave:   // 事假
                                        double totalHours3 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan = new TimeSpan(3, 0, 0);
                                        TimeSpan span = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan);
                                        if (calBeginTime.TimeOfDay < span && span < calEndTime.TimeOfDay)
                                            totalHours3--;
                                        if (totalHours3 >= 8)
                                            totalHours3 = 8;

                                        attendanceDataModel.AffiairLeaveHours += (decimal)totalHours3;
                                        break;
                                    case Status.MattersType_Marriage:   // 婚假
                                        double totalHours4 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours4 >= 8)
                                            totalHours4 = 8;
                                        else
                                            totalHours4 = 4;
                                        attendanceDataModel.MarriageLeaveHours += (decimal)totalHours4;
                                        break;
                                    case Status.MattersType_PeiChanJia:
                                    case Status.MattersType_Maternity:   // 产假
                                        double totalHours5 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours5 >= 8)
                                            totalHours5 = 8;
                                        else
                                            totalHours5 = 4;
                                        attendanceDataModel.MaternityLeaveHours += (decimal)totalHours5;
                                        break;
                                    case Status.MattersType_OffTune:   // 调休
                                        double totalHours6 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours6 >= 8)
                                            totalHours6 = 8;
                                        else
                                            totalHours6 = 4;
                                        attendanceDataModel.OffTuneHours += (decimal)totalHours6;
                                        break;
                                    case Status.MattersType_Out:      // 外出
                                        double totalHours8 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan8 = new TimeSpan(3, 0, 0);
                                        TimeSpan span8 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan8);
                                        if (calBeginTime.TimeOfDay < span8 && span8 < calEndTime.TimeOfDay)
                                            totalHours8--;
                                        if (totalHours8 >= 8)
                                            totalHours8 = 8;

                                        attendanceDataModel.EgressHours += (decimal)totalHours8;
                                        break;
                                    case Status.MattersType_Sick:     // 病假
                                        double totalHours9 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan9 = new TimeSpan(3, 0, 0);
                                        TimeSpan span9 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan9);
                                        if (calBeginTime.TimeOfDay < span9 && span9 < calEndTime.TimeOfDay)
                                            totalHours9--;
                                        if (totalHours9 >= 8)
                                            totalHours9 = 8;

                                        attendanceDataModel.SickLeaveHours += (decimal)totalHours9;
                                        break;
                                    case Status.MattersType_Travel:   // 出差
                                        double totalHours10 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours10 >= 8)
                                            totalHours10 = 8;
                                        else
                                            totalHours10 = 4;
                                        attendanceDataModel.EvectionHours += (decimal)totalHours10;
                                        break;
                                    case Status.MattersType_Other:
                                        attendanceDataModel.Other += mattersInfo.MatterContent + ",";
                                        break;
                                    case Status.MattersType_PrenatalCheck:   // 产前检查
                                        double totalHours11 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan11 = new TimeSpan(3, 0, 0);
                                        TimeSpan span11 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan11);
                                        if (calBeginTime.TimeOfDay < span11 && span11 < calEndTime.TimeOfDay)
                                            totalHours11--;
                                        if (totalHours11 >= 8)
                                            totalHours11 = 8;

                                        attendanceDataModel.PrenatalCheckHours += (decimal)totalHours11;
                                        break;
                                    case Status.MattersType_Incentive:    // 奖励假
                                        double totalHours12 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours12 >= 8)
                                            totalHours12 = 8;
                                        else
                                            totalHours12 = 4;
                                        attendanceDataModel.IncentiveHours += (decimal)totalHours12;
                                        break;
                                }
                                #endregion
                            }
                            if (attendanceDataModel.Other.EndsWith(","))
                            {
                                attendanceDataModel.Other = attendanceDataModel.Other.Substring(0, attendanceDataModel.Other.Length - 1);
                            }
                        }
                        #endregion

                        #region 判断考勤默认事由情况
                        TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                        if (remainTime.TotalMinutes >= Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            //  attendanceDataModel.LateCount++;
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Late && remainTime.TotalHours <= Status.NumberOfHours_Absent)
                        {
                            attendanceDataModel.AbsentHours += 4;
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Absent)
                        {
                            attendanceDataModel.AbsentHours += 8;
                        }
                        else if (isLeaveEarly && remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            // attendanceDataModel.LeaveEarlyCount++;
                        }
                        #endregion
                    }
                    else if (commuterTimeModel.AttendanceType == Status.UserBasicAttendanceType_NotStat)
                    {
                        #region 按打卡时间计算考勤的默认情况
                        // 是否迟到
                        bool isLate = false;
                        // 是否上午旷工
                        bool isAMAbsent = false;
                        // 是否下午旷工
                        bool isPMAbsent = false;
                        // 是否早退
                        bool isLeaveEarly = false;
                        // 考勤异常时间数
                        TimeSpan abnormityTime = new TimeSpan(0);
                        if (goWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime && OffWorkTime.ToString("yyyy-MM-dd") == Status.EmptyTime && attendanceDateTime.Date < DateTime.Now.Date)
                        {
                            isPMAbsent = true;
                            abnormityTime += abnormityTime.Add(new TimeSpan(Status.WorkingHours, 0, 0));
                        }
                        long abnormityTicks = abnormityTime.Ticks;
                        #endregion

                        #region 当天的OT情况
                        SingleOvertimeManager singleManager = new SingleOvertimeManager();
                        // 获得OT单信息
                        List<SingleOvertimeInfo> singleList = singleManager.GetSingleOvertimeList(UserID, attendanceDateTime);
                        if (singleList != null && singleList.Count > 0)
                        {
                            foreach (SingleOvertimeInfo singleInfo in singleList)
                            {
                                DateTime BeginTime = singleInfo.BeginTime;
                                DateTime EndTime = singleInfo.EndTime;
                                DateTime calBeginTime = new DateTime();
                                DateTime calEndTime = new DateTime();
                                if (attendanceDateTime.Date == BeginTime.Date)
                                {
                                    calBeginTime = BeginTime;
                                }
                                else if (attendanceDateTime.Date > BeginTime.Date)
                                {
                                    calBeginTime = DateTime.Parse(attendanceDateTime.ToString("yyyy-MM-dd ") + "00:00:00");
                                }
                                if (attendanceDateTime.Date == EndTime.Date)
                                {
                                    calEndTime = EndTime;
                                }
                                else if (attendanceDateTime.Date < EndTime.Date)
                                {
                                    calEndTime = DateTime.Parse(attendanceDateTime.ToString("yyyy-MM-dd ") + "23:59:59");
                                }
                                attendanceDataModel.OverTimeHours += (decimal)(calEndTime - calBeginTime).TotalHours;
                            }
                        }
                        #endregion

                        #region 当天的考勤事由信息
                        if (mattersList != null && mattersList.Count > 0)
                        {
                            foreach (MattersInfo mattersInfo in mattersList)
                            {
                                this.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, mattersInfo, commuterTimeModel, goWorkTime, OffWorkTime, attendanceDateTime, ref abnormityTicks);
                                DateTime BeginTime = mattersInfo.BeginTime;
                                DateTime EndTime = mattersInfo.EndTime;
                                DateTime calBeginTime = new DateTime();
                                DateTime calEndTime = new DateTime();
                                if (attendanceDateTime.Date == BeginTime.Date)
                                {
                                    calBeginTime = BeginTime;
                                }
                                else if (attendanceDateTime.Date > BeginTime.Date)
                                {
                                    calBeginTime = attendanceDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                                }
                                if (attendanceDateTime.Date == EndTime.Date)
                                {
                                    calEndTime = EndTime;
                                }
                                else if (attendanceDateTime.Date < EndTime.Date)
                                {
                                    calEndTime = attendanceDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                                }
                                #region 判断考勤类型，计算考勤统计时间
                                switch (mattersInfo.MatterType)
                                {
                                    case Status.MattersType_Annual:  // 年假
                                        double totalAnnualHours = (calEndTime - calBeginTime).TotalHours;
                                        if (calBeginTime.Hour <= 12)
                                            totalAnnualHours = totalAnnualHours - 1;
                                        attendanceDataModel.AnnualLeaveHours += (decimal)totalAnnualHours;
                                        break;
                                    case Status.MattersType_Annual_Last:  // 补年假
                                        double totalAnnualLastHours = (calEndTime - calBeginTime).TotalHours;
                                        if (calBeginTime.Hour <= 12)
                                            totalAnnualLastHours = totalAnnualLastHours - 1;
                                        attendanceDataModel.LastAnnualHours += (decimal)totalAnnualLastHours;
                                        break;
                                    case Status.MattersType_Bereavement: // 丧假
                                        double totalHours2 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours2 >= 8)
                                            totalHours2 = 8;
                                        else
                                            totalHours2 = 4;
                                        attendanceDataModel.FuneralLeaveHours += (decimal)totalHours2;
                                        break;
                                    case Status.MattersType_Leave:   // 事假
                                        double totalHours3 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan = new TimeSpan(3, 0, 0);
                                        TimeSpan span = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan);
                                        if (calBeginTime.TimeOfDay < span && span < calEndTime.TimeOfDay)
                                            totalHours3--;
                                        if (totalHours3 >= 8)
                                            totalHours3 = 8;

                                        attendanceDataModel.AffiairLeaveHours += (decimal)totalHours3;
                                        break;
                                    case Status.MattersType_Marriage:   // 婚假
                                        double totalHours4 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours4 >= 8)
                                            totalHours4 = 8;
                                        else
                                            totalHours4 = 4;
                                        attendanceDataModel.MarriageLeaveHours += (decimal)totalHours4;
                                        break;
                                    case Status.MattersType_PeiChanJia:   // 产假
                                    case Status.MattersType_Maternity:   // 产假
                                        double totalHours5 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours5 >= 8)
                                            totalHours5 = 8;
                                        else
                                            totalHours5 = 4;
                                        attendanceDataModel.MaternityLeaveHours += (decimal)totalHours5;
                                        break;
                                    case Status.MattersType_OffTune:   // 调休
                                        double totalHours6 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours6 >= 8)
                                            totalHours6 = 8;
                                        else
                                            totalHours6 = 4;
                                        attendanceDataModel.OffTuneHours += (decimal)totalHours6;
                                        break;
                                    case Status.MattersType_Out:      // 外出
                                        double totalHours8 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan8 = new TimeSpan(3, 0, 0);
                                        TimeSpan span8 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan8);
                                        if (calBeginTime.TimeOfDay < span8 && span8 < calEndTime.TimeOfDay)
                                            totalHours8--;
                                        if (totalHours8 >= 8)
                                            totalHours8 = 8;

                                        attendanceDataModel.EgressHours += (decimal)totalHours8;
                                        break;
                                    case Status.MattersType_Sick:     // 病假
                                        double totalHours9 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan9 = new TimeSpan(3, 0, 0);
                                        TimeSpan span9 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan9);
                                        if (calBeginTime.TimeOfDay < span9 && span9 < calEndTime.TimeOfDay)
                                            totalHours9--;
                                        if (totalHours9 >= 8)
                                            totalHours9 = 8;

                                        attendanceDataModel.SickLeaveHours += (decimal)totalHours9;
                                        break;
                                    case Status.MattersType_Travel:   // 出差
                                        double totalHours10 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours10 >= 8)
                                            totalHours10 = 8;
                                        else
                                            totalHours10 = 4;
                                        attendanceDataModel.EvectionHours += (decimal)totalHours10;
                                        break;
                                    case Status.MattersType_Other:
                                        attendanceDataModel.Other += mattersInfo.MatterContent + ",";
                                        break;
                                    case Status.MattersType_PrenatalCheck:   // 产前检查
                                        double totalHours11 = (calEndTime - calBeginTime).TotalHours;
                                        TimeSpan tempspan11 = new TimeSpan(3, 0, 0);
                                        TimeSpan span11 = commuterTimeModel.GoWorkTime.TimeOfDay.Add(tempspan11);
                                        if (calBeginTime.TimeOfDay < span11 && span11 < calEndTime.TimeOfDay)
                                            totalHours11--;
                                        if (totalHours11 >= 8)
                                            totalHours11 = 8;

                                        attendanceDataModel.PrenatalCheckHours += (decimal)totalHours11;
                                        break;
                                    case Status.MattersType_Incentive:    // 奖励假
                                        double totalHours12 = (calEndTime - calBeginTime).TotalHours;
                                        if (totalHours12 >= 8)
                                            totalHours12 = 8;
                                        else
                                            totalHours12 = 4;
                                        attendanceDataModel.IncentiveHours += (decimal)totalHours12;
                                        break;
                                }
                                #endregion
                            }
                            if (attendanceDataModel.Other.EndsWith(","))
                            {
                                attendanceDataModel.Other = attendanceDataModel.Other.Substring(0, attendanceDataModel.Other.Length - 1);
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion
            return attendanceDataModel;
        }

        /// <summary>
        /// 计算用户的年假天数
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="year">年份</param>
        public double CalAnnualLeave(int UserID, int year, out double rewardLeaveDays)
        {
            // 年假天数
            double totalAnnualDays = 0;
            rewardLeaveDays = 0;
            ALAndRLManager alandrlManager = new ALAndRLManager();
            ALAndRLInfo alandrlModel = alandrlManager.GetALAndRLModel(UserID, year, (int)AAndRLeaveType.AnnualType);
            if (alandrlModel != null)
            {
                totalAnnualDays = (double)alandrlModel.LeaveNumber;
                rewardLeaveDays = (double)alandrlModel.RemainingNumber;
            }

            return totalAnnualDays;
        }

        /// <summary>
        /// 获得已请年假天数
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="year">年份</param>
        /// <returns>返回已请年假天数</returns>
        public double GetIsUsedAnnualDays(int UserID, int year)
        {
            double isUsedAnnualDays = 0;
            List<MattersInfo> list = mattersManager.GetMattersList(UserID, Status.MattersType_Annual);
            if (list != null && list.Count > 0)
            {
                foreach (MattersInfo matterModel in list)
                {
                    isUsedAnnualDays += (double)matterModel.TotalHours;
                }
            }
            return isUsedAnnualDays;
        }


        /// <summary>
        /// 获得剩余年假天数
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="year">计算年份</param>
        /// <returns>返回剩余年假天数</returns>
        public double GetRemainingAnnualDays(int UserID, int year)
        {
            double remainingAnnualDays = 0;
            // 个人年假总数
            double totalAnnualLeaveDays = CalAnnualLeave(UserID, year, out remainingAnnualDays);
            // 已经使用年假总数
            // double isUsedAnnualDays = GetIsUsedAnnualDays(UserID, year);
            // 剩余年假总数
            //return totalAnnualLeaveDays - (isUsedAnnualDays / 8);
            return remainingAnnualDays;
        }

        /// <summary>
        /// 获得用户奖励的年假信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="year">要计算的年份</param>
        /// <returns>返回用户奖励的年假信息</returns>
        public double GetAwardAnnualDays(int UserID, int year, out double remainingAwardDays)
        {
            remainingAwardDays = 0;
            // 年假天数
            double awardAnnualDays = 0;
            ALAndRLManager alandrlManager = new ALAndRLManager();
            ALAndRLInfo alandrlModel = alandrlManager.GetALAndRLModel(UserID, year, (int)AAndRLeaveType.RewardType);
            if (alandrlModel != null)
            {
                awardAnnualDays = (double)alandrlModel.LeaveNumber;
                remainingAwardDays = (double)alandrlModel.RemainingNumber;
            }

            //UserAttBasicInfo userAttBasicModel = userBasicManager.GetModelByUserid(UserID);
            //if (userAttBasicModel != null)
            //{
            //    DateTime calDateTime = new DateTime(year, 1, 1);
            //    // 获得用户的入职信息
            //    EmployeeJobInfo jobinfo = EmployeeJobManager.getModelBySysId(UserID);
            //    if (jobinfo != null)
            //    {
            //        // 入职时间
            //        DateTime EntryTime = jobinfo.joinDate;
            //        if (EntryTime != null && EntryTime.Year <= year)
            //        {
            //            if (EntryTime.Year < year)
            //            {
            //                // 计算用户的服务年限
            //                int serviceAge = Utility.GetServiceAge(calDateTime, EntryTime);
            //                // 判断是否有奖励年假
            //                if (Status.IsAwardAnnualLeave)
            //                {
            //                    if (serviceAge >= Status.AwardAnnualLeaveLevel3)
            //                        awardAnnualDays += Status.AwardAnnualLeaveLevel3;
            //                    else if (serviceAge >= Status.AwardAnnualLeaveLevel2)
            //                        awardAnnualDays += Status.AwardAnnualLeaveLevel2;
            //                    else if (serviceAge >= Status.AwardAnnualLeaveLevel1)
            //                        awardAnnualDays += Status.AwardAnnualLeaveLevel1;
            //                }
            //            }
            //        }
            //    }
            //}
            return awardAnnualDays;
        }

        /// <summary>
        /// 获得考勤月统计信息
        /// </summary>
        /// <param name="UersID">用户编号</param>
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        public AttendanceDataInfo GetMonthStat(int UserID, int Year, int Month, Hashtable clockInTimes,
            EmployeeJobInfo employeeJobModel, DimissionInfo dimissionModel)
        {
            // 统计开始时间
            DateTime BeginDateTime = new DateTime(Year, Month, 1);
            DateTime EndDateTime = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));

           ESP.HumanResource.Entity.DimissionFormInfo dimission = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(UserID);

           if (dimission != null && dimission.LastDay.Value.Month == Month)
            {
                EndDateTime = dimission.LastDay.Value;
            }

            AttendanceDataInfo monthstat = new AttendanceDataInfo();
            CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
            List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserID);

            // 判断上下班时间集合是否为空，如果为空就重新获取上下班时间信息
            if (clockInTimes == null)
            {
                clockInTimes = clockInManager.GetClockInTimesOfMonth(Year, Month, UserID);
            }
            // 获得员工的入职信息
            if (employeeJobModel == null)
                employeeJobModel = EmployeeJobManager.getModelBySysId(UserID);
            // 获得员工的离职信息
            if (dimissionModel == null)
                dimissionModel = DimissionManager.GetModelByUserID(UserID);

            while (BeginDateTime <= EndDateTime)
            {
                if (employeeJobModel != null && employeeJobModel.joinDate.Date <= BeginDateTime.Date
                    && (dimissionModel == null || (dimissionModel != null && BeginDateTime.Date <= dimissionModel.dimissionDate.Date)))
                {
                    // 获得用户上下班时间信息
                    CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, BeginDateTime);

                    DateTime GoWorkTime = new DateTime();
                    DateTime OffWorkTime = new DateTime();
                    object obj1 = clockInTimes[(long)BeginDateTime.Day];
                    object obj2 = clockInTimes[(long)-BeginDateTime.Day];
                    if (obj1 != null)
                    {
                        ArrayList list = (ArrayList)obj1;
                        if (list != null && list.Count > 0)
                        {
                            GoWorkTime = (DateTime)list[0];
                        }
                    }
                    else
                        GoWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                    if (obj2 != null)
                    {
                        ArrayList list = (ArrayList)obj2;
                        if (list != null && list.Count > 0)
                        {
                            OffWorkTime = (DateTime)list[0];
                        }
                    }
                    else
                        OffWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                    // 获得用户当天的考勤记录信息
                    AttendanceDataInfo attendanceDataModel =
                        this.GetAttendanceItemInfo(UserID, BeginDateTime, GoWorkTime, OffWorkTime, commuterTimeModel);

                    #region 判断用户是否是第一天入职
                    if (employeeJobModel.joinDate.Date == BeginDateTime && OffWorkTime.ToString("yyyy-MM-dd") != Status.EmptyTime)
                    {
                        attendanceDataModel.LateCount = 0;
                        attendanceDataModel.AbsentHours = 0;
                        attendanceDataModel.LeaveEarlyCount = 0;
                    }
                    #endregion

                    monthstat.AbsentHours += attendanceDataModel.AbsentHours;
                    monthstat.AffiairLeaveHours += attendanceDataModel.AffiairLeaveHours;
                    monthstat.AnnualLeaveHours += attendanceDataModel.AnnualLeaveHours;
                    monthstat.LastAnnualHours += attendanceDataModel.LastAnnualHours;
                    monthstat.EgressHours += attendanceDataModel.EgressHours;
                    monthstat.EvectionHours += attendanceDataModel.EvectionHours;
                    monthstat.FuneralLeaveHours += attendanceDataModel.FuneralLeaveHours;
                    monthstat.LateCount += attendanceDataModel.LateCount;
                    monthstat.LeaveEarlyCount += attendanceDataModel.LeaveEarlyCount;
                    monthstat.MarriageLeaveHours += attendanceDataModel.MarriageLeaveHours;
                    monthstat.MaternityLeaveHours += attendanceDataModel.MaternityLeaveHours;
                    monthstat.OffTuneHours += attendanceDataModel.OffTuneHours;
                    monthstat.Other += attendanceDataModel.Other;
                    monthstat.OverTimeHours += attendanceDataModel.OverTimeHours;
                    monthstat.HolidayOverTimeHours += attendanceDataModel.HolidayOverTimeHours;
                    monthstat.SickLeaveHours += attendanceDataModel.SickLeaveHours;
                    monthstat.IncentiveHours += attendanceDataModel.IncentiveHours;
                    monthstat.PrenatalCheckHours += attendanceDataModel.PrenatalCheckHours;

                    monthstat.SickByYear += attendanceDataModel.SickLeaveHours;
                    monthstat.AffairByYear += attendanceDataModel.AffiairLeaveHours;
                    monthstat.AnnualLeaveByYear += attendanceDataModel.AnnualLeaveHours;
                    monthstat.IncentiveByYear += attendanceDataModel.IncentiveHours;
                }

                BeginDateTime = BeginDateTime.AddDays(1);
            }

            var sicklist = (new ESP.Administrative.BusinessLogic.MonthStatManager()).GetMonthStateByYear(UserID, Year, Month);
            if (sicklist != null)
            {
                monthstat.SickByYear += sicklist.Sum(x => x.SickLeaveHours);
                monthstat.AffairByYear += sicklist.Sum(x => x.AffairLeaveHours);
                monthstat.AnnualLeaveByYear += sicklist.Sum(x => x.AnnualLeaveDays);
                monthstat.IncentiveByYear += sicklist.Sum(x => x.IncentiveHours);
            }
            return monthstat;
        }

        /// <summary>
        /// 获得指定时间段的考勤统计数据信息对象
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="beginTime">统计开始时间（开始时间和结束时间保存在一个月内）</param>
        /// <param name="endTime">统计结束时间（开始时间和结束时间保存在一个月内）</param>
        /// <param name="clockInTimes">打卡记录信息</param>
        /// <param name="employeeJobModel">用户入职信息</param>
        /// <param name="dimissionModel">用户离职信息</param>
        /// <returns>返回指定时间段的用户考勤统计信息</returns>
        public AttendanceDataInfo GetTimeQuantumStat(int userID, DateTime beginTime, DateTime endTime, Hashtable clockInTimes,
            EmployeeJobInfo employeeJobModel, DimissionInfo dimissionModel)
        {
            // 统计开始时间
            DateTime BeginDateTime = beginTime;
            DateTime EndDateTime = endTime;
            AttendanceDataInfo monthstat = new AttendanceDataInfo();
            CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
            List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(userID);

            // 判断上下班时间集合是否为空，如果为空就重新获取上下班时间信息
            if (clockInTimes == null)
            {
                clockInTimes = clockInManager.GetClockInTimesOfMonth(beginTime.Year, beginTime.Month, userID);
            }
            // 获得员工的入职信息
            if (employeeJobModel == null)
                employeeJobModel = EmployeeJobManager.getModelBySysId(userID);
            // 获得员工的离职信息
            if (dimissionModel == null)
                dimissionModel = DimissionManager.GetModelByUserID(userID);

            while (BeginDateTime <= EndDateTime)
            {
                if (employeeJobModel != null && employeeJobModel.joinDate.Date <= BeginDateTime.Date
                    && (dimissionModel == null || (dimissionModel != null && BeginDateTime.Date <= dimissionModel.dimissionDate.Date)))
                {
                    // 获得用户上下班时间信息
                    CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, BeginDateTime);

                    DateTime GoWorkTime = new DateTime();
                    DateTime OffWorkTime = new DateTime();
                    object obj1 = clockInTimes[(long)BeginDateTime.Day];
                    object obj2 = clockInTimes[(long)-BeginDateTime.Day];
                    if (obj1 != null)
                    {
                        ArrayList list = (ArrayList)obj1;
                        if (list != null && list.Count > 0)
                        {
                            GoWorkTime = (DateTime)list[0];
                        }
                    }
                    else
                        GoWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                    if (obj2 != null)
                    {
                        ArrayList list = (ArrayList)obj2;
                        if (list != null && list.Count > 0)
                        {
                            OffWorkTime = (DateTime)list[0];
                        }
                    }
                    else
                        OffWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                    // 获得用户当天的考勤记录信息
                    AttendanceDataInfo attendanceDataModel =
                        this.GetAttendanceItemInfo(userID, BeginDateTime, GoWorkTime, OffWorkTime, commuterTimeModel);

                    if (this.CheckIsHoliday(BeginDateTime))
                    {
                        attendanceDataModel.MarriageLeaveHours = 0;
                        attendanceDataModel.MaternityLeaveHours = 0;
                    }

                    monthstat.AbsentHours += attendanceDataModel.AbsentHours;
                    monthstat.AffiairLeaveHours += attendanceDataModel.AffiairLeaveHours;
                    monthstat.AnnualLeaveHours += attendanceDataModel.AnnualLeaveHours;
                    monthstat.LastAnnualHours += attendanceDataModel.LastAnnualHours;
                    monthstat.EgressHours += attendanceDataModel.EgressHours;
                    monthstat.EvectionHours += attendanceDataModel.EvectionHours;
                    monthstat.FuneralLeaveHours += attendanceDataModel.FuneralLeaveHours;
                    monthstat.LateCount += attendanceDataModel.LateCount;
                    monthstat.LeaveEarlyCount += attendanceDataModel.LeaveEarlyCount;
                    monthstat.MarriageLeaveHours += attendanceDataModel.MarriageLeaveHours;
                    monthstat.MaternityLeaveHours += attendanceDataModel.MaternityLeaveHours;
                    monthstat.OffTuneHours += attendanceDataModel.OffTuneHours;
                    monthstat.Other += attendanceDataModel.Other;
                    monthstat.OverTimeHours += attendanceDataModel.OverTimeHours;

                    // 节假日OT小时数，4-8小时内的按4小时计算，8小时以上按8小时计算，4小时以下不计算
                    decimal holidayOverTimeHours = attendanceDataModel.HolidayOverTimeHours;
                    if (holidayOverTimeHours < Status.WorkingHours / 2)
                        monthstat.HolidayOverTimeHours += 0;
                    else if (holidayOverTimeHours >= Status.WorkingHours / 2 && holidayOverTimeHours < Status.WorkingHours)
                        monthstat.HolidayOverTimeHours += Status.WorkingHours / 2;
                    else if (holidayOverTimeHours >= Status.WorkingHours)
                        monthstat.HolidayOverTimeHours += Status.WorkingHours;
                    //monthstat.HolidayOverTimeHours += attendanceDataModel.HolidayOverTimeHours;

                    monthstat.SickLeaveHours += attendanceDataModel.SickLeaveHours;
                    monthstat.IncentiveHours += attendanceDataModel.IncentiveHours;
                    monthstat.PrenatalCheckHours += attendanceDataModel.PrenatalCheckHours;
                }
                BeginDateTime = BeginDateTime.AddDays(1);
            }
            return monthstat;
        }

        /// <summary>
        /// 判断用户时间的范围，如果是一个上午或者一个下午就返回0.5，如果是全天就返回1
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>判断开始时间和结束时间分别是属于上午还是下午，计算出用户的事由时间</returns>
        public double TimeRange(ref DateTime beginTime, ref DateTime endTime, List<CommuterTimeInfo> commuterTimeList)
        {
            double timeRange = 0;
            int beginHours = beginTime.Hour;
            int endHours = endTime.Hour;
            CommuterTimeManager commuterTimeManager = new CommuterTimeManager();

            // 获得开始时间和结束时间的节假日信息集合
            HashSet<int> hashSet = new HolidaysInfoManager().GetHolidayListByMonth(beginTime, endTime);
            int holidayCount = 0;
            if (hashSet != null && hashSet.Count > 0)
            {
                holidayCount = hashSet.Count;
            }
            // 上班时间
            CommuterTimeInfo commuterTimeBeginModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, beginTime);
            CommuterTimeInfo commuterTimeEndModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, endTime);

            // 开始时间是上午，结束时间是上午
            if ((beginHours >= 0 && beginHours < 12) && (endHours >= 0 && endHours < 12))
            {
                beginTime = beginTime.Date.Add(commuterTimeBeginModel.GoWorkTime.TimeOfDay);
                endTime = endTime.Date.Add(commuterTimeEndModel.GoWorkTime.TimeOfDay).AddHours(Status.GoWorkTime_AMGapPM).AddHours(-1);
                timeRange = 0.5 * Status.WorkingHours + (((endTime - beginTime).Days) - holidayCount) * Status.WorkingHours;
            }
            // 开始时间是上午，结束时间是下午
            else if ((beginHours >= 0 && beginHours < 12) && (endHours >= 12 && endHours <= 23))
            {
                beginTime = beginTime.Date.Add(commuterTimeBeginModel.GoWorkTime.TimeOfDay);
                endTime = endTime.Date.Add(commuterTimeEndModel.OffWorkTime.TimeOfDay);
                timeRange = 1 * Status.WorkingHours + (((endTime - beginTime).Days) - holidayCount) * Status.WorkingHours;
            }
            // 开始时间是下午，结束时间是下午
            else if ((beginHours >= 12 && beginHours <= 23) && (endHours >= 12 && endHours <= 23))
            {
                beginTime = beginTime.Date.Add(commuterTimeBeginModel.GoWorkTime.TimeOfDay).AddHours(Status.GoWorkTime_AMGapPM);
                endTime = endTime.Date.Add(commuterTimeEndModel.OffWorkTime.TimeOfDay);
                timeRange = 0.5 * Status.WorkingHours + (((endTime - beginTime).Days) - holidayCount) * Status.WorkingHours;
            }
            // 开始时间是下午，结束时间是上午
            else if ((beginHours >= 12 && beginHours <= 23) && (endHours >= 0 && endHours < 12))
            {
                beginTime = beginTime.Date.Add(commuterTimeBeginModel.GoWorkTime.TimeOfDay).AddHours(Status.GoWorkTime_AMGapPM);
                endTime = endTime.Date.Add(commuterTimeEndModel.GoWorkTime.TimeOfDay).AddHours(Status.GoWorkTime_AMGapPM).AddHours(-1);
                timeRange = 1 * Status.WorkingHours + (((endTime - beginTime).Days) - holidayCount) * Status.WorkingHours;
            }
            return timeRange;
        }

        public double TimeRangeAnnual(ref DateTime beginTime, ref DateTime endTime, List<CommuterTimeInfo> commuterTimeList)
        {
            double timeRange = 0;
            int beginHours = beginTime.Hour;
            int endHours = endTime.Hour;
            CommuterTimeManager commuterTimeManager = new CommuterTimeManager();

            // 获得开始时间和结束时间的节假日信息集合
            HashSet<int> hashSet = new HolidaysInfoManager().GetHolidayListByMonth(beginTime, endTime);
            int holidayCount = 0;
            if (hashSet != null && hashSet.Count > 0)
            {
                holidayCount = hashSet.Count;
            }
            // 上班时间
            CommuterTimeInfo commuterTimeBeginModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, beginTime);
            CommuterTimeInfo commuterTimeEndModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, endTime);

            if (endTime.Year == beginTime.Year && endTime.Month == beginTime.Month && endTime.Day == beginTime.Day)
            {
                timeRange = (endTime - beginTime).Hours;
                if (beginTime.Hour <= 12 && endTime.Hour > 12)
                    timeRange = timeRange - 1;
            }
            else if (endTime.Year > beginTime.Year || (endTime.Year == beginTime.Year && endTime.Month > beginTime.Month) || (endTime.Year == beginTime.Year && endTime.Month == beginTime.Month && endTime.Day > beginTime.Day))
            {
                DateTime beginDayEndtime = beginTime.Date.Add(commuterTimeBeginModel.OffWorkTime.TimeOfDay);
                DateTime endDayBegintime = endTime.Date.Add(commuterTimeBeginModel.GoWorkTime.TimeOfDay);
                int beginTimeHours = (beginDayEndtime - beginTime).Hours;
                int endtimeHours = (endTime - endDayBegintime).Hours;

                //开始第一天判断是否减休息一小时
                if (beginTime.Hour <= 12 && beginDayEndtime.Hour > 12)
                    beginTimeHours = beginTimeHours - 1;
                //结束当天判断是否减休息一小时
                if (endDayBegintime.Hour <= 12 && endTime.Hour > 12)
                    endtimeHours = endtimeHours - 1;

                timeRange = beginTimeHours + endtimeHours + (((endTime - beginTime).Days) - holidayCount - 1) * Status.WorkingHours;
            }

            return timeRange;
        }

        /// <summary>
        /// 提交考勤数据
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns>如果考勤信息提交成功返回true,否则返回false</returns>
        public bool SubmintAttendance(int userId, int Year, int Month, bool bl)
        {
            bool b = false;
            MonthStatInfo monthStatInfo = monthManager.GetMonthStatInfoApprove(userId, Year, Month);
            AttendanceDataInfo attendanceDataInfo = this.GetMonthStat(userId, Year, Month, null, null, null);

            if (monthStatInfo != null)
            {
                // 更新月考勤信息
                monthStatInfo.Year = Year;
                monthStatInfo.Month = Month;
                monthStatInfo.UpdateTime = DateTime.Now;
                monthStatInfo.OperateorID = userId;
                monthStatInfo.OperateorDept = 0;
                monthStatInfo.AttendanceSubType = (int)AttendanceSubType.Normal;
                if (bl)
                {
                    monthStatInfo.AttendanceSubType = (int)AttendanceSubType.Dimission;
                }

                SetMonthStatInfo(ref monthStatInfo, attendanceDataInfo);
                monthManager.Update(monthStatInfo);

                // 设置考勤审批人信息，并添加一条待审批的记录信息
                #region 审批记录信息
                ApproveLogInfo applog = new ApproveLogInfo();
                applog.ApproveDateID = monthStatInfo.ID;
                applog.ApproveState = 0;
                ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(userId);
                if (opearmodel != null)
                {
                    applog.ApproveID = opearmodel.HRAdminID;
                    applog.ApproveName = opearmodel.HRAdminName;
                }
                applog.ApproveType = (int)Status.MattersSingle.MattersSingle_Attendance;
                applog.ApproveUpUserID = 0;
                applog.CreateTime = DateTime.Now;
                applog.Deleted = false;
                applog.IsLastApprove = 0;
                applog.OperateorID = userId;
                applog.Sort = 0;
                applog.UpdateTime = DateTime.Now;
                appLogManager.Add(applog);
                #endregion
                b = true;
            }
            else
            {
                // 添加一条月考勤记录信息
                ESP.Framework.Entity.EmployeeInfo empInfo = ESP.Framework.BusinessLogic.EmployeeManager.Get(userId);
                monthStatInfo = new MonthStatInfo();
                monthStatInfo.Year = Year;
                monthStatInfo.Month = Month;
                monthStatInfo.UserID = userId;
                monthStatInfo.UserCode = empInfo.Code;
                monthStatInfo.EmployeeName = empInfo.FullNameCN;
                monthStatInfo.Deleted = false;
                monthStatInfo.CreateTime = DateTime.Now;
                monthStatInfo.UpdateTime = DateTime.Now;
                monthStatInfo.OperateorID = userId;
                monthStatInfo.OperateorDept = 0;
                monthStatInfo.Sort = 0;
                monthStatInfo.AttendanceSubType = (int)AttendanceSubType.Normal;
                if (bl)
                {
                    monthStatInfo.AttendanceSubType = (int)AttendanceSubType.Dimission;
                }
                this.SetMonthStatInfo(ref monthStatInfo, attendanceDataInfo);
                int dataid = monthManager.Add(monthStatInfo);

                // 设置考勤审批人信息，并添加一条待审批的记录信息
                #region 审批记录信息
                ApproveLogInfo applog = new ApproveLogInfo();
                applog.ApproveDateID = dataid;
                applog.ApproveState = 0;
                ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(userId);
                if (opearmodel != null)
                {
                    applog.ApproveID = opearmodel.HRAdminID;
                    applog.ApproveName = opearmodel.HRAdminName;
                }
                applog.ApproveType = (int)Status.MattersSingle.MattersSingle_Attendance;
                applog.ApproveUpUserID = 0;
                applog.CreateTime = DateTime.Now;
                applog.Deleted = false;
                applog.IsLastApprove = 0;
                applog.OperateorID = userId;
                applog.Sort = 0;
                applog.UpdateTime = DateTime.Now;
                appLogManager.Add(applog);
                #endregion
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 设置各月考勤信息
        /// </summary>
        /// <param name="monthStatInfo">月考勤统计对象</param>
        /// <param name="list">一个月里每天的考勤记录对象集合</param>
        public void SetMonthStatInfo(ref MonthStatInfo monthStatInfo, AttendanceDataInfo attendanceDataInfo)
        {
            if (attendanceDataInfo != null)
            {
                monthStatInfo.AbsentDays = attendanceDataInfo.AbsentHours;
                monthStatInfo.AffairLeaveHours = attendanceDataInfo.AffiairLeaveHours;
                monthStatInfo.AnnualLeaveDays = attendanceDataInfo.AnnualLeaveHours;
                monthStatInfo.LastAnnualDays = attendanceDataInfo.LastAnnualHours;
                monthStatInfo.EgressHours = attendanceDataInfo.EgressHours;
                monthStatInfo.EvectionDays = attendanceDataInfo.EvectionHours;
                monthStatInfo.FuneralLeaveHours = attendanceDataInfo.FuneralLeaveHours;
                monthStatInfo.LateCount = attendanceDataInfo.LateCount;
                monthStatInfo.LeaveEarlyCount = attendanceDataInfo.LeaveEarlyCount;
                monthStatInfo.MarriageLeaveHours = attendanceDataInfo.MarriageLeaveHours;
                monthStatInfo.MaternityLeaveHours = attendanceDataInfo.MaternityLeaveHours;
                monthStatInfo.Offtunehours = attendanceDataInfo.OffTuneHours;
                monthStatInfo.OverTimeHours = attendanceDataInfo.OverTimeHours;
                monthStatInfo.SickLeaveHours = attendanceDataInfo.SickLeaveHours;
                monthStatInfo.HolidayOverTimeHours = attendanceDataInfo.HolidayOverTimeHours;
                monthStatInfo.PrenatalCheckHours = attendanceDataInfo.PrenatalCheckHours;
                monthStatInfo.IncentiveHours = attendanceDataInfo.IncentiveHours;
                monthStatInfo.Other = attendanceDataInfo.Other;

                monthStatInfo.IsHavePCRefund = false;
                monthStatInfo.PCRefundAmount = 0;

                monthStatInfo.State = Status.MonthStatAppState_WaitHRAdmin;  // 已经提交
                //monthStatInfo.State = Status.MonthStatAppState_WaitDirector;  // 等待总监审批
            }
        }

        /// <summary>
        /// 判断上下班时间是否是在休息时间段内
        /// </summary>
        /// <param name="time"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckTimeIsInBreakTime(DateTime time, CommuterTimeInfo model)
        {
            bool b = false;
            if (model != null)
            {
                // 中午休息开始时间
                DateTime breakBeginTime = time.Date.Add(model.GoWorkTime.TimeOfDay).AddHours(Status.AMWorkingHours);
                // 中午休息结束时间
                DateTime breakEndTime = time.Date.Add(model.GoWorkTime.TimeOfDay).AddHours(Status.GoWorkTime_AMGapPM);
                if (breakBeginTime <= time && time <= breakEndTime)
                {
                    b = true;
                }
            }
            return b;
        }

        /// <summary>
        /// 获得考勤月统计信息
        /// </summary>
        /// <param name="UersID">用户编号</param>
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        public void GetMonthStat(UserAttBasicInfo userAttBasicModel, int Year, int Month, Dictionary<long, DateTime> clockInTimes,
            List<SingleOvertimeInfo> singleList, List<MattersInfo> mattersList, EmployeeJobInfo employeeJobModel,
            ref AttendanceStatisticInfo attendanceStatisticModel, HashSet<int> holidays, DimissionInfo dimissionModel)
        {
            int UserID = userAttBasicModel.Userid;
            // 统计开始时间
            DateTime BeginDateTime = new DateTime(Year, Month, 1);
            DateTime EndDateTime = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            AttendanceDataInfo monthstat = new AttendanceDataInfo();
            CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
            List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserID);

            // 获得员工的入职信息
            if (employeeJobModel == null)
                employeeJobModel = EmployeeJobManager.getModelBySysId(userAttBasicModel.Userid);
            // 获得员工的入职信息
            if (dimissionModel == null)
                dimissionModel = DimissionManager.GetModelByUserID(userAttBasicModel.Userid);

            int lateCount1 = 0;       // 迟到30分钟内的次数
            int lateCount2 = 0;       // 迟到30分钟以上的次数
            int overTimeCount = 0;    // OT次数
            int absentCount1 = 0;     // 旷工变天
            int absentCount2 = 0;     // 旷工一天
            int absentCount3 = 0;     // 打卡记录不全
            int leaveEarlyCount = 0;  // 早退

            while (BeginDateTime <= EndDateTime && BeginDateTime.Date < DateTime.Now.Date)
            {
                if (employeeJobModel != null && employeeJobModel.joinDate.Date <= BeginDateTime.Date
                    && (dimissionModel == null || (dimissionModel != null && BeginDateTime.Date <= dimissionModel.dimissionDate.Date)))
                {
                    // 获得用户上下班时间信息
                    CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, BeginDateTime);

                    DateTime GoWorkTime = new DateTime();
                    DateTime OffWorkTime = new DateTime();
                    if (clockInTimes != null && clockInTimes.Count > 0)
                    {
                        if (clockInTimes.ContainsKey((long)BeginDateTime.Day))
                        {
                            GoWorkTime = clockInTimes[(long)BeginDateTime.Day];
                        }
                        else
                        {
                            GoWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                        }
                        if (clockInTimes.ContainsKey((long)-BeginDateTime.Day))
                        {
                            object obj2 = clockInTimes[(long)-BeginDateTime.Day];

                            if (obj2 != null)
                                OffWorkTime = (DateTime)obj2;
                            else
                                OffWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                        }
                        else
                        {
                            OffWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                        }
                    }
                    else
                    {
                        GoWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                        OffWorkTime = new DateTime(1900, 1, 1, 0, 0, 0);
                    }

                    // 获得用户当天的考勤记录信息
                    GetAttendanceItemInfo(UserID, BeginDateTime, GoWorkTime, OffWorkTime, singleList, mattersList, commuterTimeModel, holidays,
                        ref lateCount1, ref lateCount2, ref overTimeCount, ref absentCount1, ref absentCount2, ref absentCount3, ref leaveEarlyCount);
                }
                BeginDateTime = BeginDateTime.AddDays(1);
            }
            attendanceStatisticModel.LateCount1 = lateCount1;
            attendanceStatisticModel.LateCount2 = lateCount2;
            attendanceStatisticModel.AbsentCount1 = absentCount1;
            attendanceStatisticModel.AbsentCount2 = absentCount2;
            attendanceStatisticModel.AbsentCount3 = absentCount3;
            attendanceStatisticModel.LeaveEarly = leaveEarlyCount;
        }

        /// <summary>
        /// 获得考勤记录统计信息对象
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="attendanceDateTime"></param>
        /// <param name="goWorkTime"></param>
        /// <param name="OffWorkTime"></param>
        /// <param name="attendanceDataInfo"></param>
        public void GetAttendanceItemInfo(int UserID, DateTime today, DateTime clockInTime, DateTime clockOutTime, List<SingleOvertimeInfo> singleList,
            List<MattersInfo> mattersList, CommuterTimeInfo commuterTimeModel, HashSet<int> holidays,
            ref int lateCount1, ref int lateCount2, ref int overTimeCount, ref int absentCount1, ref int absentCount2, ref int absentCount3, ref int leaveEarlyCount)
        {
            DateTime tomorrow = today.AddDays(1);
            //if (userAttBasicModel.AttendanceType == Status.UserBasicAttendanceType_Normal)
            //{
            #region 根据上下班时间计算考勤事由信息
            // 是否迟到
            bool isLate = false;
            // 是否上午旷工
            bool isAMAbsent = false;
            // 是否下午旷工
            bool isPMAbsent = false;
            // 是否早退
            bool isLeaveEarly = false;
            // 考勤异常时间数
            TimeSpan abnormityTime = new TimeSpan(0);
            CalDefaultMatters(UserID, today, clockInTime, clockOutTime,
                out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterTimeModel);
            long abnormityTicks = abnormityTime.Ticks;
            #endregion

            #region 判断前一天的OT数据信息
            if (singleList != null && singleList.Count > 0)
            {
                foreach (SingleOvertimeInfo single in singleList)
                {
                    if (single != null)
                    {
                        if ((single.BeginTime >= today.AddDays(-1) && single.BeginTime < tomorrow.AddDays(-1))
                    || (single.EndTime > today.AddDays(-1) && single.EndTime <= tomorrow.AddDays(-1))
                    || (single.BeginTime <= today.AddDays(-1) && single.EndTime >= tomorrow.AddDays(-1)))
                        {

                            // 判断用户的OT开始时间是否大于用户的下班时间
                            if (single.BeginTime >= single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)
                                || (holidays.Contains(today.AddDays(-1).Day)
                                    && single.EndTime > single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)))
                            {
                                decimal overTimeHours = 0;
                                if (!holidays.Contains(today.AddDays(-1).Day))
                                {
                                    overTimeHours = single.OverTimeHours;
                                }
                                else
                                {
                                    overTimeHours = (decimal)(single.EndTime - single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)).TotalHours;
                                }
                                // 用户OT小时数大于6小时
                                if (overTimeHours >= (decimal)commuterTimeModel.WorkingDays_OverTime2)
                                {
                                    TimeSpan tempSpan = new TimeSpan(Status.AMWorkingHours, 0, 0);
                                    abnormityTicks -= tempSpan.Ticks;
                                    if (isAMAbsent)
                                    {
                                        isAMAbsent = false;
                                    }
                                    else if (isLate)
                                    {
                                        isLate = false;
                                    }
                                    else if (isPMAbsent)
                                    {
                                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                                        {
                                            isPMAbsent = true;
                                        }
                                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                                        {
                                            isAMAbsent = true;
                                        }
                                        else
                                        {
                                            isLate = true;
                                        }
                                    }
                                }
                                else if (overTimeHours >= (decimal)commuterTimeModel.WorkingDays_OverTime1)
                                {
                                    //TimeSpan tempSpan = new TimeSpan(Status.LateGoWorkTime_OverTime1, 0, 0);
                                    DateTime tempSpan = new DateTime().AddHours(commuterTimeModel.LateGoWorkTime_OverTime1);
                                    abnormityTicks -= tempSpan.Ticks;
                                    if (isLate)
                                    {
                                        isLate = false;
                                    }
                                    else if (isAMAbsent)
                                    {
                                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                                        {
                                            isPMAbsent = true;
                                        }
                                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                                        {
                                            isAMAbsent = true;
                                        }
                                        else
                                        {
                                            isLate = true;
                                        }
                                    }
                                    else if (isPMAbsent)
                                    {
                                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                                        {
                                            isPMAbsent = true;
                                        }
                                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                                        {
                                            isAMAbsent = true;
                                        }
                                        else
                                        {
                                            isLate = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 判断显示当天的考勤事由信息
            foreach (MattersInfo info in mattersList)
            {
                if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                    || (info.EndTime > today && info.EndTime <= tomorrow)
                    || (info.BeginTime <= today && info.EndTime >= tomorrow))
                {
                    CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterTimeModel, clockInTime, clockOutTime, today, ref abnormityTicks);
                }
            }
            #endregion

            #region 判断显示考勤默认事由信息
            TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
            if (!holidays.Contains(today.Day))
            {
                if (isLate && remainTime.TotalMinutes > Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                {
                    if (remainTime.TotalMinutes > Status.LateMin)
                        lateCount2++;
                    else
                        lateCount1++;
                }
                else if (remainTime.TotalHours > Status.NumberOfHours_Late && remainTime.TotalHours <= Status.NumberOfHours_Absent)
                {
                    absentCount1++;
                }
                else if (remainTime.TotalHours > Status.NumberOfHours_Absent)
                {
                    if (clockInTime.ToString("yyyy-MM-dd") == Status.EmptyTime || clockOutTime.ToString("yyyy-MM-dd") == Status.EmptyTime)
                    {
                        absentCount3++;
                    }
                    else
                    {
                        absentCount2++;
                    }
                }
                else if (isLeaveEarly && remainTime.TotalMinutes > Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                {
                    leaveEarlyCount++;
                }
                else if (remainTime.TotalMinutes > 0 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                {
                    if (clockInTime > today.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay)
                        && (clockInTime - today.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay)).TotalMinutes > Status.GoWorkTime_BufferMinute)
                    {
                        if (remainTime.TotalMinutes > Status.LateMin)
                            lateCount2++;
                        else
                            lateCount1++;
                    }
                    else if (clockOutTime < today.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)
                        && today.Date < DateTime.Now.Date
                        && (clockOutTime - today.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)).TotalMinutes > Status.GoWorkTime_BufferMinute)
                    {
                        leaveEarlyCount++;
                    }
                }
            }
            #endregion
            //}
            //else if (userAttBasicModel.AttendanceType == Status.UserBasicAttendanceType_Special)
            //{
            //    #region 根据上下班时间计算考勤事由信息
            //    // 是否迟到
            //    bool isLate = false;
            //    // 是否上午旷工
            //    bool isAMAbsent = false;
            //    // 是否下午旷工
            //    bool isPMAbsent = false;
            //    // 是否早退
            //    bool isLeaveEarly = false;
            //    TimeSpan span = new TimeSpan();
            //    if (clockInTime.ToString("yyyy-MM-dd") == Status.EmptyTime && clockOutTime.ToString("yyyy-MM-dd") == Status.EmptyTime && today.Date < DateTime.Now.Date)
            //    {
            //        isPMAbsent = true;
            //        span = span.Add(new TimeSpan(Status.WorkingHours, 0, 0));
            //    }
            //    long abnormityTicks = span.Ticks;
            //    #endregion

            //    #region 判断显示当天的考勤事由信息
            //    foreach (MattersInfo info in mattersList)
            //    {
            //        if ((info.BeginTime >= today && info.BeginTime < tomorrow)
            //            || (info.EndTime > today && info.EndTime <= tomorrow)
            //            || (info.BeginTime <= today && info.EndTime >= tomorrow))
            //        {
            //            CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, userAttBasicModel, clockInTime, clockOutTime, today, ref abnormityTicks);
            //        }
            //    }
            //    #endregion

            //    #region 判断显示考勤默认事由信息
            //    if (!holidays.Contains(today.Day))
            //    {
            //        if (isPMAbsent)
            //        {
            //            absentCount2++;
            //        }
            //    }
            //    #endregion
            //}
        }

        /// <summary>
        /// 获得用户入离职信息
        /// </summary>
        public Dictionary<int, ESP.HumanResource.Entity.EmployeeJobInfo> GetEmployeeJobInfo(string strWhere)
        {
            DataSet ds = ESP.HumanResource.BusinessLogic.EmployeeJobManager.GetList(strWhere);
            Dictionary<int, ESP.HumanResource.Entity.EmployeeJobInfo> employeeJobDic = new Dictionary<int, EmployeeJobInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int userid = int.Parse(dr["sysid"].ToString());
                    if (employeeJobDic.ContainsKey(userid))
                    {
                        employeeJobDic[userid] = CBO.FillObject<ESP.HumanResource.Entity.EmployeeJobInfo>(dr);
                    }
                    else
                    {
                        employeeJobDic.Add(userid, CBO.FillObject<ESP.HumanResource.Entity.EmployeeJobInfo>(dr));
                    }
                }
            }
            return employeeJobDic;
        }

        /// <summary>
        /// 获得离职人员信息
        /// </summary>
        /// <returns>返回离职人员信息集合</returns>
        public Dictionary<int, DimissionInfo> GetDimissionInfo()
        {
            List<DimissionInfo> dimissionList = ESP.HumanResource.BusinessLogic.DimissionManager.GetModelList("");
            Dictionary<int, DimissionInfo> dimissionDic = new Dictionary<int, DimissionInfo>();
            if (dimissionList != null && dimissionList.Count > 0)
            {
                foreach (DimissionInfo model in dimissionList)
                {
                    if (dimissionDic.ContainsKey(model.userId))
                    {
                        dimissionDic[model.userId] = model;
                    }
                    else
                    {
                        dimissionDic.Add(model.userId, model);
                    }
                }
            }
            return dimissionDic;
        }

        /// <summary>
        /// 判断用户是否取消七天提交限制
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        //public bool CheckIsOpenedUser(int userid)
        //{
        //    bool b = false;
        //    // 获得不受七天提交限制的用户信息列表
        //    DataCodeInfo datamodel = new DataCodeManager().GetDataCodeByType("OpenedUserID")[0];
        //    DataCodeInfo dataModel2 = new DataCodeManager().GetDataCodeByType("OpenedDepID")[0];
        //    IList<ESP.Framework.Entity.EmployeePositionInfo> list= ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userid);
        //    string datacode = datamodel.Code;
        //    string depcode = dataModel2.Code;
        //    if (!string.IsNullOrEmpty(datacode) && datacode.IndexOf(userid.ToString()) != -1)
        //    {
        //        b = true;
        //    }
        //    if (!string.IsNullOrEmpty(depcode) && list != null && list.Count > 0)
        //    {
        //        foreach (ESP.Framework.Entity.EmployeePositionInfo model in list)
        //        {
        //            string parentId = ESP.Framework.BusinessLogic.DepartmentManager.Get(model.DepartmentID).ParentID.ToString();
        //            if (depcode.IndexOf(parentId) != -1)
        //            {
        //                b = true;
        //            }
        //        }
        //    }
        //    return b;
        //}

        /// <summary>
        /// 判断用户是否取消七天提交限制
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public bool CheckIsOpenedUser(int userid)
        {
            //bool b = false;
            //AttGracePeriodManager manager = new AttGracePeriodManager();
            //DataSet ds = manager.GetList("userid=" + userid + " and begintime<='" 
            //    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'<=endtime");
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    b = true;
            //}

            //// 获得不受七天提交限制的部门信息列表
            //DataCodeInfo dataModel2 = new DataCodeManager().GetDataCodeByType("OpenedDepID")[0];
            //IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userid);
            //string depcode = dataModel2.Code;
            //if (!string.IsNullOrEmpty(depcode) && list != null && list.Count > 0)
            //{
            //    foreach (ESP.Framework.Entity.EmployeePositionInfo model in list)
            //    {
            //        string parentId = ESP.Framework.BusinessLogic.DepartmentManager.Get(model.DepartmentID).ParentID.ToString();
            //        if (depcode.IndexOf(parentId) != -1)
            //        {
            //            b = true;
            //        }
            //    }
            //}
            return true;
        }
        #endregion 成员方法
    }
}