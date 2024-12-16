using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ESP.Administrative.Entity;
using System.Web;
using System.IO;
using System.Data;
using ESP.Administrative.BusinessLogic;

namespace ESP.Administrative.Common
{
    public class FileHelper
    {
        #region 导出个人月考勤详细信息
        /// <summary>
        /// 导出个人当月的考勤记录信息
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="mapPath">站点的物理路径</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="userId">用户编号</param>
        /// <param name="attendanceDateModel">考勤月统计数据</param>
        /// <param name="clockInTimes">时间记录信息</param>
        /// <param name="holidays">节假日信息集合</param>
        /// <param name="matters">考勤事由信息</param>
        /// <param name="overtimes">OT事由信息</param>
        /// <param name="response">请求响应对象</param>
        /// <returns></returns>
        public static string ExportStatistics(DataSet ds, string mapPath, int year, int month, int userId, AttendanceDataInfo attendanceDateModel,
            Hashtable clockInTimes, HashSet<int> holidays, IList<MattersInfo> matters, IList<SingleOvertimeInfo> overtimes,
            ESP.HumanResource.Entity.EmployeeJobInfo employeeJobModel, List<CommuterTimeInfo> commuterTimeList, HttpResponse response,
            ESP.HumanResource.Entity.DimissionInfo dimissionModel)
        {
            CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
            string filename = mapPath + "ExcelTemplate\\" + "UserMonthStat.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            ESP.Framework.Entity.EmployeeInfo employeeInfo = ESP.Framework.BusinessLogic.EmployeeManager.Get(userId);
            IList<ESP.Framework.Entity.EmployeePositionInfo> empPositionlist = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userId);
            string depstr = "";
            if (empPositionlist != null && empPositionlist.Count > 0)
            {
                foreach (ESP.Framework.Entity.EmployeePositionInfo empPosition in empPositionlist)
                {
                    depstr += empPosition.DepartmentName + ",";
                }
            }
            sheet.Cells[2, 2] = year + "年" + month + "月";
            sheet.Cells[2, 5] = employeeInfo.FullNameCN;
            sheet.Cells[2, 8] = depstr.TrimEnd(',');

            string[] weekName = new string[] { "日", "一", "二", "三", "四", "五", "六" };
            DateTime beginDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            while (beginDate <= endDate)
            {
                // 获得用户上下班时间
                CommuterTimeInfo commuterModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, beginDate);

                sheet.Cells[3 + beginDate.Day, 1] = beginDate.Day;
                sheet.Cells[3 + beginDate.Day, 2] = weekName[(int)beginDate.DayOfWeek];
                object objin = clockInTimes[(long)beginDate.Date.Day];
                object objout = clockInTimes[(long)-beginDate.Date.Day];

                sheet.Cells[3 + beginDate.Day, 3] = objin == null ? "" : ((DateTime)((ArrayList)objin)[0]).ToString("HH:mm");
                sheet.Cells[3 + beginDate.Day, 4] = objout == null ? "" : ((DateTime)((ArrayList)objout)[0]).ToString("HH:mm");

                if (objin != null)
                {
                    DateTime clockInTime = ((DateTime)((ArrayList)objin)[0]);
                    if (holidays.Contains(beginDate.Date.Day))
                    {
                        // 设置上班时间的颜色
                        sheet.get_Range("C" + (3 + beginDate.Day), "C" + (3 + beginDate.Day)).Font.ColorIndex = 10;  // 将字体颜色设置为绿色
                    }
                    else
                    {
                        // 设置上班时间的颜色
                        if (clockInTime.Date == beginDate.Date
                            && clockInTime.TimeOfDay > commuterModel.GoWorkTime.TimeOfDay.Add(new TimeSpan(0, Status.GoWorkTime_BufferMinute, 0)))
                        {
                            sheet.get_Range("C" + (3 + beginDate.Day), "C" + (3 + beginDate.Day)).Font.ColorIndex = 3;  // 将字体颜色设置为红色
                        }
                    }
                }
                if (objout != null)
                {
                    DateTime clockOutTime = ((DateTime)((ArrayList)objout)[0]);
                    if (holidays.Contains(beginDate.Date.Day))
                    {
                        // 设置下班时间的颜色
                        sheet.get_Range("D" + (3 + beginDate.Day), "D" + (3 + beginDate.Day)).Font.ColorIndex = 10;  // 将字体颜色设置为绿色
                    }
                    else
                    {
                        // 设置下班时间的颜色
                        // 判断时间是否正常，如果不正常则显示相对应的颜色
                        if (clockOutTime.Date == beginDate.Date &&
                            clockOutTime.TimeOfDay < commuterModel.OffWorkTime.TimeOfDay)
                        {
                            sheet.get_Range("D" + (3 + beginDate.Day), "D" + (3 + beginDate.Day)).Font.ColorIndex = 3;  // 将字体颜色设置为红色
                        }
                        // 计算工作日OT超过几点后有效
                        //TimeSpan span = commuterModel.OffWorkTime.TimeOfDay.Add(new TimeSpan(Status.WorkingDays_OverTime1, 0, 0));
                        TimeSpan span = commuterModel.OffWorkTime.AddHours(commuterModel.WorkingDays_OverTime1).TimeOfDay;
                        if (clockOutTime.Date > beginDate.Date
                            || (clockOutTime.Date == beginDate.Date
                            && clockOutTime.TimeOfDay > span))
                        {
                            sheet.get_Range("D" + (3 + beginDate.Day), "D" + (3 + beginDate.Day)).Font.ColorIndex = 5;  // 将字体颜色设置为红色
                        }
                    }
                }

                #region 考勤事由信息统计
                //if (employeeJobModel != null && employeeJobModel.joinDate.Date <= today)
                //{
                //    // 上班时间
                //    DateTime clockIn = clockInTimes[(long)beginDate.Date.Day] == null
                //        ? new DateTime(1900, 1, 1) : (DateTime)clockInTimes[(long)beginDate.Date.Day];
                //    // 下班时间
                //    DateTime clockOut = clockInTimes[(long)-beginDate.Date.Day] == null
                //        ? new DateTime(1900, 1, 1) : (DateTime)clockInTimes[(long)-beginDate.Date.Day];
                //    #region 判断人员考勤的类型，如果是考勤是正常（普通员工）的就计算考勤情况
                //    if (userBasicModel.AttendanceType == Status.UserBasicAttendanceType_Normal)
                //    {
                //        #region 判断显示当天的OT信息
                //        foreach (SingleOvertimeInfo info in overtimes)
                //        {
                //            if ((info.BeginTime >= today && info.BeginTime < tomorrow) || (info.EndTime >= today && info.EndTime < tomorrow))
                //            {
                //                // 判断OT事由审批状态是否是审批通过
                //                if (info.Approvestate == Status.OverTimeState_Passed)
                //                {
                //                    sheet.Cells[3 + beginDate.Day, 8] = string.Format("{0:F1}", info.OverTimeHours);
                //                }
                //            }
                //        }
                //        #endregion

                //        #region 根据上下班时间计算考勤事由信息
                //        // 是否迟到
                //        bool isLate = false;
                //        // 是否上午旷工
                //        bool isAMAbsent = false;
                //        // 是否下午旷工
                //        bool isPMAbsent = false;
                //        // 是否早退
                //        bool isLeaveEarly = false;
                //        // 考勤异常时间数
                //        TimeSpan abnormityTime = new TimeSpan(0);

                //        new AttendanceManager().CalDefaultMatters(userId, today, clockIn, clockOut,
                //            out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime);
                //        long abnormityTicks = abnormityTime.Ticks;
                //        #endregion

                //        #region 计算前一天的OT信息，判断是否去除当天的迟到和矿工
                //        // 获得前一天的OT单信息，并且是审批通过的OT单
                //        List<SingleOvertimeInfo> beforeDaySingleList = new SingleOvertimeManager().GetSingleOvertimeList(userId, today.AddDays(-1), true);
                //        if (beforeDaySingleList != null && beforeDaySingleList.Count > 0)
                //        {
                //            foreach (SingleOvertimeInfo single in beforeDaySingleList)
                //            {
                //                if (single != null)
                //                {
                //                    // 判断用户的OT开始时间是否大于用户的下班时间
                //                    if (single.BeginTime >= DateTime.Parse(single.BeginTime.ToString("yyyy-MM-dd ")
                //                        + userBasicModel.OffWorkTime))
                //                    {
                //                        // 用户OT小时数大于6小时
                //                        if (single.OverTimeHours >= Status.WorkingDays_OverTime2)
                //                        {
                //                            if (isAMAbsent)
                //                            {
                //                                isAMAbsent = false;
                //                            }
                //                            else if (isLate)
                //                            {
                //                                isLate = false;
                //                            }
                //                            else if (isPMAbsent)
                //                            {
                //                                DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                //                                DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                //                                TimeSpan span = clockIn - pmGoDateTime;
                //                                // 判断用户超出时间是否是在迟到的时间范围内
                //                                if (span.TotalHours < Status.NumberOfHours_Late)
                //                                {
                //                                    isLate = true;
                //                                }
                //                                // 判断用户超出时间是否是在旷工的时间范围内
                //                                else if (span.TotalHours < Status.NumberOfHours_Absent)
                //                                {
                //                                    isAMAbsent = true;
                //                                }
                //                                // 否则的话用户超出的时间已经超过了旷工半天的时间了
                //                                else
                //                                {
                //                                    isPMAbsent = true;
                //                                }
                //                            }
                //                        }
                //                        else if (single.OverTimeHours >= Status.WorkingDays_OverTime1)
                //                        {
                //                            if (isLate)
                //                            {
                //                                isLate = false;
                //                            }
                //                            else if (isAMAbsent)
                //                            {
                //                                DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                //                                DateTime pmGoDateTime = goDateTime.AddHours(Status.NumberOfHours_Late);
                //                                TimeSpan span = clockIn - pmGoDateTime;
                //                                // 判断用户超出时间是否是在迟到的时间范围内
                //                                if (span.TotalHours < Status.NumberOfHours_Late)
                //                                {
                //                                    isLate = true;
                //                                }
                //                                // 判断用户超出时间是否是在旷工的时间范围内
                //                                else if (span.TotalHours < Status.NumberOfHours_Absent)
                //                                {
                //                                    isAMAbsent = true;
                //                                }
                //                                // 否则的话用户超出的时间已经超过了旷工半天的时间了
                //                                else
                //                                {
                //                                    isPMAbsent = true;
                //                                }
                //                            }
                //                            else if (isPMAbsent)
                //                            {
                //                                DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                //                                DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                //                                TimeSpan span = clockIn - pmGoDateTime;
                //                                // 判断用户超出时间是否是在迟到的时间范围内
                //                                if (span.TotalHours < Status.NumberOfHours_Late)
                //                                {
                //                                    isLate = true;
                //                                }
                //                                // 判断用户超出时间是否是在旷工的时间范围内
                //                                else if (span.TotalHours < Status.NumberOfHours_Absent)
                //                                {
                //                                    isAMAbsent = true;
                //                                }
                //                                // 否则的话用户超出的时间已经超过了旷工半天的时间了
                //                                else
                //                                {
                //                                    isPMAbsent = true;
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //        #endregion

                //        #region 判断显示当天的考勤事由信息
                //        foreach (MattersInfo info in matters)
                //        {
                //            if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                //                || (info.EndTime > today && info.EndTime <= tomorrow)
                //                || (info.BeginTime <= today && info.EndTime >= tomorrow))
                //            {
                //                // 判断考勤事由是否已经审批通过
                //                if (info.MatterState == Status.MattersState_Passed)
                //                {
                //                    new AttendanceManager().CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, userBasicModel, clockIn, clockOut, today, ref abnormityTicks);
                //                    string matterType;
                //                    switch (info.MatterType)
                //                    {
                //                        case 1:  // 病假
                //                            sheet.Cells[3 + beginDate.Day, 9] = string.Format("{0:F1}", info.TotalHours);
                //                            break;
                //                        case 2:  // 事假
                //                            sheet.Cells[3 + beginDate.Day, 10] = string.Format("{0:F1}", info.TotalHours);
                //                            break;
                //                        case 3:  // 年假
                //                            sheet.Cells[3 + beginDate.Day, 11] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 4:  // 婚假
                //                            sheet.Cells[3 + beginDate.Day, 12] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 5:  // 产假
                //                            sheet.Cells[3 + beginDate.Day, 14] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 6:  // 丧假
                //                            sheet.Cells[3 + beginDate.Day, 13] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 7:  // 出差
                //                            sheet.Cells[3 + beginDate.Day, 15] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 8:  // 外出
                //                            sheet.Cells[3 + beginDate.Day, 16] = string.Format("{0:F1}", info.TotalHours);
                //                            break;
                //                        case 9:  // 调休
                //                            sheet.Cells[3 + beginDate.Day, 17] = string.Format("{0:F1}", info.TotalHours);
                //                            break;
                //                        case 10: // 其他
                //                            sheet.Cells[3 + beginDate.Day, 18] = string.Format("{0:F1}", info.TotalHours);
                //                            break;

                //                        default:
                //                            continue;
                //                    }
                //                }
                //            }
                //        }
                //        #endregion

                //        #region 判断显示考勤默认事由信息
                //        if (!holidays.Contains(today.Day))
                //        {
                //            if (isLate)
                //            {
                //                sheet.Cells[3 + beginDate.Day, 5] = "1";  // 迟到一次
                //            }
                //            else if (isAMAbsent)
                //            {
                //                sheet.Cells[3 + beginDate.Day, 7] = "0.5";  // 旷工0.5天
                //            }
                //            else if (isPMAbsent)
                //            {
                //                sheet.Cells[3 + beginDate.Day, 7] = "1";    // 旷工1天 
                //            }
                //            if (isLeaveEarly)
                //            {
                //                sheet.Cells[3 + beginDate.Day, 6] = "1";
                //            }
                //        }
                //        #endregion
                //    }
                //    #endregion
                //    else if (userBasicModel.AttendanceType == Status.UserBasicAttendanceType_Special)
                //    {
                //        #region 判断显示当天的考勤事由信息
                //        foreach (MattersInfo info in matters)
                //        {
                //            if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                //                || (info.EndTime > today && info.EndTime <= tomorrow)
                //                || (info.BeginTime <= today && info.EndTime >= tomorrow))
                //            {
                //                // 判断考勤事由是否已经审批通过
                //                if (info.MatterState == Status.MattersState_Passed)
                //                {
                //                    switch (info.MatterType)
                //                    {
                //                        case 1:  // 病假
                //                            sheet.Cells[3 + beginDate.Day, 9] = string.Format("{0:F1}", info.TotalHours);
                //                            break;
                //                        case 2:  // 事假
                //                            sheet.Cells[3 + beginDate.Day, 10] = string.Format("{0:F1}", info.TotalHours);
                //                            break;
                //                        case 3:  // 年假
                //                            sheet.Cells[3 + beginDate.Day, 11] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 4:  // 婚假
                //                            sheet.Cells[3 + beginDate.Day, 12] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 5:  // 产假
                //                            sheet.Cells[3 + beginDate.Day, 14] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 6:  // 丧假
                //                            sheet.Cells[3 + beginDate.Day, 13] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 7:  // 出差
                //                            sheet.Cells[3 + beginDate.Day, 15] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                //                            break;
                //                        case 8:  // 外出
                //                            sheet.Cells[3 + beginDate.Day, 16] = string.Format("{0:F1}", info.TotalHours);
                //                            break;
                //                        case 9:  // 调休
                //                            sheet.Cells[3 + beginDate.Day, 17] = string.Format("{0:F1}", info.TotalHours);
                //                            break;
                //                        case 10: // 其他
                //                            sheet.Cells[3 + beginDate.Day, 18] = string.Format("{0:F1}", info.TotalHours);
                //                            break;

                //                        default:
                //                            continue;
                //                    }
                //                }
                //            }
                //        }
                //        #endregion
                //    }
                //}
                #endregion

                DateTime today = beginDate.Date;
                DateTime tomorrow = today.AddDays(1);
                if (employeeJobModel != null && employeeJobModel.joinDate.Date <= today
                    && (dimissionModel == null || (dimissionModel != null && today <= dimissionModel.dimissionDate.Date)))
                {
                    //// 上班时间
                    //DateTime clockIn = clockInTimes[(long)beginDate.Date.Day] == null
                    //    ? new DateTime(1900, 1, 1) : (DateTime)clockInTimes[(long)beginDate.Date.Day];
                    //// 下班时间
                    //DateTime clockOut = clockInTimes[(long)-beginDate.Date.Day] == null
                    //    ? new DateTime(1900, 1, 1) : (DateTime)clockInTimes[(long)-beginDate.Date.Day];
                    ArrayList clockinList = (ArrayList)clockInTimes[(long)beginDate.Date.Day];
                    ArrayList clockoutList = (ArrayList)clockInTimes[(long)-beginDate.Date.Day];
                    // 上班时间
                    DateTime clockIn = new DateTime(1900, 1, 1);
                    if (clockinList != null && clockinList.Count > 0)
                    {
                        clockIn = (DateTime)clockinList[0];
                    }
                    // 下班时间
                    DateTime clockOut = new DateTime(1900, 1, 1);
                    if (clockoutList != null && clockoutList.Count > 0)
                    {
                        clockOut = (DateTime)clockoutList[0];
                    }

                    #region 判断人员考勤的类型，如果是考勤是正常（普通员工）的就计算考勤情况
                    if (commuterModel.AttendanceType == Status.UserBasicAttendanceType_Normal)
                    {
                        #region 判断显示当天的OT信息
                        foreach (SingleOvertimeInfo info in overtimes)
                        {
                            if ((info.BeginTime >= today && info.BeginTime < tomorrow) || (info.EndTime >= today && info.EndTime < tomorrow))
                            {
                                // 判断OT事由审批状态是否是审批通过
                                if (info.Approvestate == Status.OverTimeState_Passed)
                                {
                                    sheet.Cells[3 + beginDate.Day, 8] = string.Format("{0:F1}", info.OverTimeHours);
                                }
                            }
                        }
                        #endregion

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
                        new AttendanceManager().CalDefaultMatters(userId, today, clockIn, clockOut,
                            out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterModel);
                        long abnormityTicks = abnormityTime.Ticks;
                        #endregion

                        #region 计算前一天的OT信息，判断是否去除当天的迟到和矿工
                        // 获得前一天的OT单信息，并且是审批通过的OT单
                        List<SingleOvertimeInfo> beforeDaySingleList = new SingleOvertimeManager().GetSingleOvertimeList(userId, today.AddDays(-1), true);
                        if (beforeDaySingleList != null && beforeDaySingleList.Count > 0)
                        {
                            foreach (SingleOvertimeInfo single in beforeDaySingleList)
                            {
                                if (single != null)
                                {
                                    // 判断用户的OT开始时间是否大于用户的下班时间
                                    if (single.BeginTime >= single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)
                                        || (holidays.Contains(today.AddDays(-1).Day)
                                            && single.EndTime > single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)))
                                    {
                                        decimal overTimeHours = 0;
                                        if (!holidays.Contains(today.AddDays(-1).Day))
                                        {
                                            overTimeHours = single.OverTimeHours;
                                        }
                                        else
                                        {
                                            overTimeHours = (decimal)(single.EndTime - single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)).TotalHours;
                                        }
                                        // 用户OT小时数大于6小时
                                        if (overTimeHours >= (decimal)commuterModel.WorkingDays_OverTime2)
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
                                        else if (overTimeHours >= (decimal)commuterModel.WorkingDays_OverTime1)
                                        {
                                            //TimeSpan tempSpan = new TimeSpan(Status.LateGoWorkTime_OverTime1, 0, 0);
                                            DateTime tempSpan = new DateTime().AddHours(commuterModel.LateGoWorkTime_OverTime1);
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
                        #endregion

                        #region 判断显示当天的考勤事由信息
                        foreach (MattersInfo info in matters)
                        {
                            if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                                || (info.EndTime > today && info.EndTime <= tomorrow)
                                || (info.BeginTime <= today && info.EndTime >= tomorrow))
                            {
                                // 判断考勤事由是否已经审批通过
                                if (info.MatterState == Status.MattersState_Passed)
                                {
                                    new AttendanceManager().CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterModel, clockIn, clockOut, today, ref abnormityTicks);
                                    string matterType;
                                    switch (info.MatterType)
                                    {
                                        case 1:  // 病假
                                            sheet.Cells[3 + beginDate.Day, 9] = string.Format("{0:F1}", info.TotalHours);
                                            break;
                                        case 2:  // 事假
                                            sheet.Cells[3 + beginDate.Day, 10] = string.Format("{0:F1}", info.TotalHours);
                                            break;
                                        case 3:  // 年假
                                            sheet.Cells[3 + beginDate.Day, 11] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 4:  // 婚假
                                            sheet.Cells[3 + beginDate.Day, 12] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 5:  // 产假
                                            sheet.Cells[3 + beginDate.Day, 14] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 6:  // 丧假
                                            sheet.Cells[3 + beginDate.Day, 13] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 7:  // 出差
                                            sheet.Cells[3 + beginDate.Day, 15] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 8:  // 外出
                                            sheet.Cells[3 + beginDate.Day, 16] = string.Format("{0:F1}", info.TotalHours);
                                            break;
                                        case 9:  // 调休
                                            sheet.Cells[3 + beginDate.Day, 17] = string.Format("{0:F1}", info.TotalHours);
                                            break;
                                        case 10: // 其他
                                            sheet.Cells[3 + beginDate.Day, 18] = string.Format("{0:F1}", info.TotalHours);
                                            break;

                                        default:
                                            continue;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 判断显示考勤默认事由信息
                        TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                        if (!holidays.Contains(today.Day))
                        {
                            if (isLate && remainTime.TotalMinutes > Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                            {
                                sheet.Cells[3 + beginDate.Day, 5] = "1";  // 迟到一次
                            }
                            else if (remainTime.TotalHours > Status.NumberOfHours_Late && remainTime.TotalHours <= Status.NumberOfHours_Absent)
                            {
                                sheet.Cells[3 + beginDate.Day, 7] = "0.5";  // 旷工0.5天
                            }
                            else if (remainTime.TotalHours > Status.NumberOfHours_Absent)
                            {
                                sheet.Cells[3 + beginDate.Day, 7] = "1";    // 旷工1天 
                            }
                            else if (isLeaveEarly && remainTime.TotalMinutes > Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                            {
                                sheet.Cells[3 + beginDate.Day, 6] = "1";
                            }
                            else if (remainTime.TotalMinutes > 0 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                            {
                                if (clockIn > today.Date.Add(commuterModel.GoWorkTime.TimeOfDay)
                                    && (clockIn - today.Date.Add(commuterModel.GoWorkTime.TimeOfDay)).TotalMinutes > Status.GoWorkTime_BufferMinute)
                                {
                                    sheet.Cells[3 + beginDate.Day, 5] = "1";  // 迟到一次
                                }
                                else if (clockOut < today.Date.Add(commuterModel.OffWorkTime.TimeOfDay)
                                    && today.Date < DateTime.Now.Date
                                    && (clockOut - today.Date.Add(commuterModel.OffWorkTime.TimeOfDay)).TotalMinutes > Status.GoWorkTime_BufferMinute)
                                {
                                    sheet.Cells[3 + beginDate.Day, 6] = "1";
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                    else if (commuterModel.AttendanceType == Status.UserBasicAttendanceType_Special)
                    {
                        #region 判断显示当天的OT信息
                        foreach (SingleOvertimeInfo info in overtimes)
                        {
                            if ((info.BeginTime >= today && info.BeginTime < tomorrow) || (info.EndTime >= today && info.EndTime < tomorrow))
                            {
                                // 判断OT事由审批状态是否是审批通过
                                if (info.Approvestate == Status.OverTimeState_Passed)
                                {
                                    sheet.Cells[3 + beginDate.Day, 8] = string.Format("{0:F1}", info.OverTimeHours);
                                }
                            }
                        }
                        #endregion

                        #region 根据上下班时间计算考勤事由信息
                        // 是否迟到
                        bool isLate = false;
                        // 是否上午旷工
                        bool isAMAbsent = false;
                        // 是否下午旷工
                        bool isPMAbsent = false;
                        // 是否早退
                        bool isLeaveEarly = false;
                        TimeSpan span = new TimeSpan();
                        if (clockIn.ToString("yyyy-MM-dd") == Status.EmptyTime && clockOut.ToString("yyyy-MM-dd") == Status.EmptyTime && today.Date < DateTime.Now.Date)
                        {
                            isPMAbsent = true;
                            span = span.Add(new TimeSpan(Status.WorkingHours, 0, 0));
                        }
                        long abnormityTicks = span.Ticks;
                        #endregion

                        #region 判断显示当天的考勤事由信息
                        foreach (MattersInfo info in matters)
                        {
                            if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                                || (info.EndTime > today && info.EndTime <= tomorrow)
                                || (info.BeginTime <= today && info.EndTime >= tomorrow))
                            {
                                // 判断考勤事由是否已经审批通过
                                if (info.MatterState == Status.MattersState_Passed)
                                {
                                    new AttendanceManager().CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterModel, clockIn, clockOut, today, ref abnormityTicks);
                                    string matterType;
                                    switch (info.MatterType)
                                    {
                                        case 1:  // 病假
                                            sheet.Cells[3 + beginDate.Day, 9] = string.Format("{0:F1}", info.TotalHours);
                                            break;
                                        case 2:  // 事假
                                            sheet.Cells[3 + beginDate.Day, 10] = string.Format("{0:F1}", info.TotalHours);
                                            break;
                                        case 3:  // 年假
                                            sheet.Cells[3 + beginDate.Day, 11] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 4:  // 婚假
                                            sheet.Cells[3 + beginDate.Day, 12] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 5:  // 产假
                                            sheet.Cells[3 + beginDate.Day, 14] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 6:  // 丧假
                                            sheet.Cells[3 + beginDate.Day, 13] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 7:  // 出差
                                            sheet.Cells[3 + beginDate.Day, 15] = string.Format("{0:F1}", info.TotalHours / Status.WorkingHours);
                                            break;
                                        case 8:  // 外出
                                            sheet.Cells[3 + beginDate.Day, 16] = string.Format("{0:F1}", info.TotalHours);
                                            break;
                                        case 9:  // 调休
                                            sheet.Cells[3 + beginDate.Day, 17] = string.Format("{0:F1}", info.TotalHours);
                                            break;
                                        case 10: // 其他
                                            sheet.Cells[3 + beginDate.Day, 18] = string.Format("{0:F1}", info.TotalHours);
                                            break;

                                        default:
                                            continue;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 判断显示考勤默认事由信息
                        if (!holidays.Contains(today.Day))
                        {
                            if (isLate)
                            {
                                sheet.Cells[3 + beginDate.Day, 5] = "1";  // 迟到一次
                            }
                            else if (isAMAbsent)
                            {
                                sheet.Cells[3 + beginDate.Day, 7] = "0.5";  // 旷工0.5天
                            }
                            else if (isPMAbsent)
                            {
                                sheet.Cells[3 + beginDate.Day, 7] = "1";    // 旷工1天 
                            }
                            if (isLeaveEarly)
                            {
                                sheet.Cells[3 + beginDate.Day, 6] = "1";
                            }
                        }
                        #endregion
                    }
                }

                beginDate = beginDate.AddDays(1);
            }

            #region 考勤统计信息
            // 判断月考勤统计信息是否为空
            if (attendanceDateModel != null)
            {
                sheet.Cells[37, 1] = attendanceDateModel.LateCount;
                if (attendanceDateModel.LateCount > 0)
                    sheet.Cells[37, 1] = attendanceDateModel.LateCount;    // 迟到
                else
                    sheet.Cells[37, 1] = "";    // 迟到

                if (attendanceDateModel.LeaveEarlyCount > 0)
                    sheet.Cells[37, 2] = attendanceDateModel.LeaveEarlyCount;   // 早退
                else
                    sheet.Cells[37, 2] = "";

                if (attendanceDateModel.AbsentHours > 0)
                    sheet.Cells[37, 3] = string.Format("{0:F1}", attendanceDateModel.AbsentHours / Status.WorkingHours);    // 旷工
                else
                    sheet.Cells[37, 3] = "";

                if (attendanceDateModel.OverTimeHours > 0)
                    sheet.Cells[37, 4] = string.Format("{0:F1}", attendanceDateModel.OverTimeHours);   // OT
                else
                    sheet.Cells[37, 4] = "";

                if (attendanceDateModel.SickLeaveHours > 0)
                    sheet.Cells[37, 5] = string.Format("{0:F1}", attendanceDateModel.SickLeaveHours); // 病假
                else
                    sheet.Cells[37, 5] = "";

                if (attendanceDateModel.AffiairLeaveHours > 0)
                    sheet.Cells[37, 7] = string.Format("{0:F1}", attendanceDateModel.AffiairLeaveHours);  // 事假
                else
                    sheet.Cells[37, 7] = "";

                if (attendanceDateModel.AnnualLeaveHours > 0)
                    sheet.Cells[37, 8] = string.Format("{0:F1}", attendanceDateModel.AnnualLeaveHours / Status.WorkingHours);  // 年假
                else
                    sheet.Cells[37, 8] = "";

                if (attendanceDateModel.MarriageLeaveHours > 0)
                    sheet.Cells[37, 10] = string.Format("{0:F1}", attendanceDateModel.MarriageLeaveHours / Status.WorkingHours);  // 婚假
                else
                    sheet.Cells[37, 10] = "";

                if (attendanceDateModel.FuneralLeaveHours > 0)
                    sheet.Cells[37, 11] = string.Format("{0:F1}", attendanceDateModel.FuneralLeaveHours / Status.WorkingHours);   // 丧假
                else
                    sheet.Cells[37, 11] = "";

                if (attendanceDateModel.MaternityLeaveHours > 0)
                    sheet.Cells[37, 12] = string.Format("{0:F1}", attendanceDateModel.MaternityLeaveHours / Status.WorkingHours);  // 产假
                else
                    sheet.Cells[37, 12] = "";

                if (attendanceDateModel.EvectionHours > 0)
                    sheet.Cells[37, 13] = string.Format("{0:F1}", attendanceDateModel.EvectionHours / Status.WorkingHours);  // 出差
                else
                    sheet.Cells[37, 13] = "";

                if (attendanceDateModel.EgressHours > 0)
                    sheet.Cells[37, 14] = string.Format("{0:F1}", attendanceDateModel.EgressHours);   // 外出
                else
                    sheet.Cells[37, 14] = "";

                if (attendanceDateModel.OffTuneHours > 0)
                    sheet.Cells[37, 15] = string.Format("{0:F1}", attendanceDateModel.OffTuneHours);   // 调休
                else
                    sheet.Cells[37, 15] = "";
                // 其他
                sheet.Cells[37, 16] = attendanceDateModel.Other;

            }
            #endregion

            workbook.Saved = true;

            //string tmpFileName = employeeInfo.FullNameCN + year + "年" + month + "月考勤.xls";
            string tmpFileName = userId + "_UserMonthStat" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                sheet = null;
                workbook = null;
                app = null;
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, employeeInfo.FullNameCN + year + "年" + month + "月考勤.xls", response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sheet = null;
                workbook = null;
                app = null;
            }
            return "";
        }

        /// <summary>
        /// 导出当月考勤统计信息
        /// </summary>
        /// <param name="pathandname">文件路劲</param>
        /// <param name="filename">文件名称</param>
        /// <param name="showFileName">显示文件名称</param>
        /// <param name="response"></param>
        public static void outExcel(string pathandname, string filename, string showFileName, HttpResponse response)
        {
            response.Clear();
            FileStream fin = new FileStream(pathandname, FileMode.Open);
            response.Charset = "GB2312";
            response.AddHeader("Content-Disposition", "attachment;   filename=" + System.Web.HttpUtility.UrlEncode(showFileName, System.Text.Encoding.UTF8));
            response.AddHeader("Connection", "Close");
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.ContentType = "application/ms-excel";    //设置输出文件类型为excel文件。 
            response.AddHeader("Content-Length", fin.Length.ToString());

            byte[] buf = new byte[1024];
            while (true)
            {
                int length = fin.Read(buf, 0, buf.Length);
                if (length > 0)
                    response.OutputStream.Write(buf, 0, length);
                if (length < buf.Length)
                    break;
            }
            fin.Close();
            response.Flush();
            response.Close();
            FileInfo finfo = new FileInfo(pathandname);
            finfo.Delete();
        }
        #endregion

        #region 导出月统计考勤信息
        /// <summary>
        /// 获取月统计信息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <param name="serverPath"></param>
        /// <param name="response"></param>
        public static void Export(string content, string type, int userId, string serverPath, HttpResponse response)
        {
            if (!string.IsNullOrEmpty(content) && !(null == type))
            {
                //try
                //{
                    string strWhere = content;
                    string typevalue = type;
                    int status = Status.MonthStatAppState_Passed;
                    //switch (typevalue)
                    //{
                    //    case "3": status = Status.MonthStatAppState_WaitStatisticians; break;
                    //    case "4": status = Status.MonthStatAppState_WaitManager; break;  // 如果是团队HRAdmin导出，就导出等待总经理审批的考勤统计数据
                    //    case "6": status = Status.MonthStatAppState_Passed; break;       // 如果是人力Admin导出，就导出审批通过的考勤统计数据
                    //}

                    string[] str = strWhere.Split('|');
                    string year = "";
                    string month = "";
                    if (str.Length == 2)
                    {
                        year = str[0].ToString();
                        month = str[1].ToString();
                    }
                    ApproveLogManager approveLogMan = new ApproveLogManager();
                    string userids = new UserAttBasicInfoManager().GetStatUserIDs(userId);
                    if (!string.IsNullOrEmpty(userids))
                    {
                        userids = userids.TrimEnd(',');
                    }

                    // 获得在职人员考勤统计数据
                    DataSet ds = approveLogMan.GetExprotApprovedListNormal(userId.ToString(), status, int.Parse(year), int.Parse(month), userids, (int)AttendanceSubType.Normal);
                    // 获得当月离职员工考勤统计数据
                    DataSet dimissionDs = approveLogMan.GetExprotApprovedList2(userId.ToString(), status, int.Parse(year), int.Parse(month), userids, (int)AttendanceSubType.Dimission);

                    DateTime tempTime = DateTime.Now;
                    ExportStatistics(ds, serverPath, year, month, response, status, userId, dimissionDs);
                   // ESP.Logging.Logger.Add("导出考勤统计表花费时间：" + (DateTime.Now - tempTime).TotalSeconds);
                //}
                //catch (Exception ex)
                //{
                  //  ESP.Logging.Logger.Add(ex.ToString());
                //}
            }
        }

        #region
        ///// <summary>
        ///// 获取月统计信息
        ///// </summary>
        ///// <param name="content"></param>
        ///// <param name="type"></param>
        ///// <param name="userId"></param>
        ///// <param name="serverPath"></param>
        ///// <param name="response"></param>
        //public static void ExportTemp(string content, string type, int userId, string serverPath, HttpResponse response, string userids)
        //{
        //    if (!string.IsNullOrEmpty(content) && !(null == type))
        //    {
        //        try
        //        {
        //            string strWhere = content;
        //            string typevalue = type;
        //            int status = 0;
        //            switch (typevalue)
        //            {
        //                case "3": status = Status.MonthStatAppState_WaitStatisticians; break;
        //                case "4": status = Status.MonthStatAppState_WaitManager; break;  // 如果是团队HRAdmin导出，就导出等待总经理审批的考勤统计数据
        //                case "6": status = Status.MonthStatAppState_Passed; break;       // 如果是人力Admin导出，就导出审批通过的考勤统计数据
        //            }

        //            string[] str = strWhere.Split('|');
        //            string year = "";
        //            string month = "";
        //            if (str.Length == 2)
        //            {
        //                year = str[0].ToString();
        //                month = str[1].ToString();
        //            }
        //            DataSet ds = new ApproveLogManager().GetExprotApprovedList2(userId.ToString(), status, int.Parse(year), int.Parse(month), userids);
        //            ExportStatisticsTemp(ds, serverPath, year, month, response, status, userId);
        //        }
        //        catch (Exception ex)
        //        {
        //            ESP.Logging.Logger.Add(ex.ToString());
        //        }
        //    }
        //}

        /// <summary>
        /// 导出月统计信息
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="mapPath"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        //private static string ExportStatisticsTemp(DataSet ds, string mapPath, string year, string month, HttpResponse response, int status, int userID)
        //{
        //    Dictionary<string, List<DataRow>> dictrionary = new Dictionary<string, List<DataRow>>();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            string teamName = dr["level2"].ToString();
        //            if (dictrionary.ContainsKey(teamName))
        //            {
        //                dictrionary[teamName].Add(dr);
        //            }
        //            else
        //            {
        //                List<DataRow> list = new List<DataRow>();
        //                list.Add(dr);
        //                dictrionary.Add(teamName, list);
        //            }
        //        }
        //    }

        //    string filename = mapPath + "ExcelTemplate\\" + "MonthStat.xls";
        //    Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
        //    Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
        //    Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

        //    sheet.Cells[2, 2] = year + "年" + month + "月1日至" + year + "年" + month + "月" + DateTime.DaysInMonth(int.Parse(year), int.Parse(month)) + "日";
        //    int startRowIndex = 4;//起始行索引
        //    int rowIndex = 0; //顺序行索引
        //    AttendanceManager attMan = new AttendanceManager();

        //    if (dictrionary != null && dictrionary.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, List<DataRow>> pair in dictrionary)
        //        {
        //            string key = pair.Key;
        //            sheet.Cells[startRowIndex + rowIndex, 1] = key;
        //            rowIndex++;
        //            int number = 1;
        //            foreach (DataRow dr in pair.Value)
        //            {
        //                sheet.Cells[startRowIndex + rowIndex, 1] = number++;
        //                sheet.Cells[startRowIndex + rowIndex, 2] = dr["usercode"].ToString();
        //                sheet.Cells[startRowIndex + rowIndex, 3] = dr["ApplicantName"].ToString();
        //                sheet.Cells[startRowIndex + rowIndex, 4] = dr["level3"].ToString();
        //                AttendanceDataInfo attdatainfo = attMan.GetMonthStat(int.Parse(dr["ApplicantID"].ToString()),
        //                   int.Parse(year), int.Parse(month), null, null, null);
        //                sheet.Cells[startRowIndex + rowIndex, 5] = attdatainfo.LateCount == 0 ? "" : attdatainfo.LateCount.ToString();
        //                sheet.Cells[startRowIndex + rowIndex, 6] = attdatainfo.LeaveEarlyCount == 0 ? "" : attdatainfo.LeaveEarlyCount.ToString();
        //                sheet.Cells[startRowIndex + rowIndex, 7] = (((decimal)attdatainfo.AbsentHours) / Status.WorkingHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 8] = ((decimal)attdatainfo.OverTimeHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 9] = ((decimal)attdatainfo.SickLeaveHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 10] = new MonthStatManager().GetUserAllYearSickLeaveDay(int.Parse(dr["ApplicantID"].ToString()), int.Parse(year), Status.AttendanceStatItem.SickLeaveHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 11] = ((decimal)attdatainfo.AffiairLeaveHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 12] = (((decimal)attdatainfo.AnnualLeaveHours) / Status.WorkingHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 13] = (((decimal)attdatainfo.MarriageLeaveHours) / Status.WorkingHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 14] = (((decimal)attdatainfo.FuneralLeaveHours) / Status.WorkingHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 15] = (((decimal)attdatainfo.MaternityLeaveHours) / Status.WorkingHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 16] = (((decimal)attdatainfo.EvectionHours) / Status.WorkingHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 17] = (((decimal)attdatainfo.EgressHours)).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 18] = (((decimal)attdatainfo.OffTuneHours) / Status.WorkingHours).ToString("#.##");
        //                sheet.Cells[startRowIndex + rowIndex, 19] = attdatainfo.Other;
        //                sheet.Cells[startRowIndex + rowIndex, 21] = dr["approveremark"].ToString();
        //                sheet.Cells[startRowIndex + rowIndex, 22] = (((decimal)attdatainfo.HolidayOverTimeHours) / Status.WorkingHours).ToString("#.##");

        //                //sheet.Cells[startRowIndex + rowIndex, 5] =  dr["LateCount"].ToString() == "0" ? "" : dr["LateCount"].ToString();
        //                //sheet.Cells[startRowIndex + rowIndex, 6] = dr["LeaveEarlyCount"].ToString() == "0" ? "" : dr["LeaveEarlyCount"].ToString();
        //                //sheet.Cells[startRowIndex + rowIndex, 7] = (decimal.Parse(dr["AbsentDays"].ToString()) / Status.WorkingHours).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 8] = (decimal.Parse(dr["OverTimeHours"].ToString())).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 9] = decimal.Parse(dr["SickLeaveHours"].ToString()).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 10] = new MonthStatManager().GetUserAllYearSickLeaveDay(int.Parse(dr["ApplicantID"].ToString()), int.Parse(year), Status.AttendanceStatItem.SickLeaveHours).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 11] = decimal.Parse(dr["AffairLeaveHours"].ToString()).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 12] = (decimal.Parse(dr["AnnualLeaveDays"].ToString()) / Status.WorkingHours).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 13] = (decimal.Parse(dr["MarriageLeaveHours"].ToString()) / Status.WorkingHours).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 14] = (decimal.Parse(dr["FuneralLeaveHours"].ToString()) / Status.WorkingHours).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 15] = (decimal.Parse(dr["MaternityLeaveHours"].ToString()) / Status.WorkingHours).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 16] = (decimal.Parse(dr["EvectionDays"].ToString()) / Status.WorkingHours).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 17] = (decimal.Parse(dr["EgressHours"].ToString())).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 18] = (decimal.Parse(dr["OffTuneHours"].ToString()) / Status.WorkingHours).ToString("#.##");
        //                //sheet.Cells[startRowIndex + rowIndex, 19] = dr["Other"].ToString();
        //                rowIndex++;
        //            }
        //            rowIndex++;
        //        }
        //    }
        //    workbook.Saved = true;

        //    string tmpFileName = "MonthStat" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
        //    try
        //    {
        //        workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        workbook.Close(null, null, null);
        //        app.Workbooks.Close();
        //        app.Application.Quit();
        //        app.Quit();

        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

        //        sheet = null;
        //        workbook = null;
        //        app = null;
        //        outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {

        //    }
        //    return "";
        //}
        #endregion

        /// <summary>
        /// 导出月统计信息
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="mapPath"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string ExportStatistics(DataSet ds, string mapPath, string year, string month, HttpResponse response, int status, int userID, DataSet dimissionDs)
        {
            #region 在职人员考勤信息
            Dictionary<string, List<DataRow>> dictrionary = new Dictionary<string, List<DataRow>>();
            Dictionary<string, string> userdictrionarys = new Dictionary<string, string>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string teamName = dr["level2"].ToString();
                    string userid = dr["ApplicantID"].ToString();
                    if (userdictrionarys.ContainsKey(userid))
                    {
                        continue;
                    }
                    else
                    {
                        userdictrionarys.Add(userid, userid);
                    }
                    if (dictrionary.ContainsKey(teamName))
                    {
                        dictrionary[teamName].Add(dr);
                    }
                    else
                    {
                        List<DataRow> list = new List<DataRow>();
                        list.Add(dr);
                        dictrionary.Add(teamName, list);
                    }
                }
            }
            #endregion

            #region 离职人员考勤信息
            Dictionary<string, List<DataRow>> dimDictrionary = new Dictionary<string, List<DataRow>>();
            Dictionary<string, string> dimUserDictrionarys = new Dictionary<string, string>();

            if (dimissionDs != null && dimissionDs.Tables.Count > 0 && dimissionDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dimissionDs.Tables[0].Rows)
                {
                    string teamName = dr["level2"].ToString();
                    string userid = dr["ApplicantID"].ToString();
                    if (dimUserDictrionarys.ContainsKey(userid))
                    {
                        continue;
                    }
                    else
                    {
                        dimUserDictrionarys.Add(userid, userid);
                    }
                    if (dimDictrionary.ContainsKey(teamName))
                    {
                        dimDictrionary[teamName].Add(dr);
                    }
                    else
                    {
                        List<DataRow> list = new List<DataRow>();
                        list.Add(dr);
                        dimDictrionary.Add(teamName, list);
                    }
                }
            }
            #endregion

            string filename = mapPath + "ExcelTemplate\\" + "MonthStat.xlsx";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];
            Microsoft.Office.Interop.Excel.Worksheet dimSheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[2];
            sheet.Name = month + "月考勤总表";
            int dimMonth = int.Parse(month) == 12 ? 1 : int.Parse(month) + 1;
            int dimYear = int.Parse(month) == 12 ? int.Parse(year) + 1 : int.Parse(year);
            dimSheet.Name = dimMonth + "月离职考勤表";

            sheet.Cells[2, 2] = year + "年" + month + "月1日至" + year + "年" + month + "月" + DateTime.DaysInMonth(int.Parse(year), int.Parse(month)) + "日";
            dimSheet.Cells[2, 2] = dimYear + "年" + dimMonth + "月1日至" + dimYear + "年" + dimMonth + "月" + DateTime.DaysInMonth(dimYear, dimMonth) + "日";
            int startRowIndex = 4;//起始行索引
            int rowIndex = 0; //顺序行索引

            #region 在职人员考勤表
            if (dictrionary != null && dictrionary.Count > 0)
            {
                foreach (KeyValuePair<string, List<DataRow>> pair in dictrionary)
                {
                    string key = pair.Key;
                    sheet.Cells[startRowIndex + rowIndex, 1] = key;
                    rowIndex++;
                    int number = 1;
                    foreach (DataRow dr in pair.Value)
                    {
                        decimal noDefunTime = 0;
                        StringBuilder strBuilder = new StringBuilder();
                        sheet.Cells[startRowIndex + rowIndex, 1] = number++;
                        sheet.Cells[startRowIndex + rowIndex, 2] = dr["usercode"].ToString();
                        sheet.Cells[startRowIndex + rowIndex, 3] = dr["ApplicantName"].ToString();
                        sheet.Cells[startRowIndex + rowIndex, 4] = dr["level3"].ToString();
                        // 迟到
                        string lateCount = dr["LateCount"].ToString();
                        sheet.Cells[startRowIndex + rowIndex, 5] = lateCount == "0" ? "" : lateCount;
                        string strLateCount = lateCount == "0" ? "" : "迟到" + lateCount + "次" + ",";
                        strBuilder.Append(strLateCount);

                        // 早退
                        string leaveEarlyCount = dr["LeaveEarlyCount"].ToString();
                        sheet.Cells[startRowIndex + rowIndex, 6] = leaveEarlyCount == "0" ? "" : leaveEarlyCount;
                        string strLeaveEarlyCount = leaveEarlyCount == "0" ? "" : "早退" + leaveEarlyCount + "次" + ",";
                        strBuilder.Append(strLeaveEarlyCount);

                        // 旷工
                        int absentDays = (int)(decimal.Parse(dr["AbsentDays"].ToString()) / Status.WorkingHours);
                        int absentHours = (int)(decimal.Parse(dr["AbsentDays"].ToString()) % Status.WorkingHours);
                        string tempabsentDays = (absentDays == 0 ? "" : absentDays + "D") + (absentHours == 0 ? "" : absentHours + "H");
                        sheet.Cells[startRowIndex + rowIndex, 7] = tempabsentDays;
                        string strAbsentDays = decimal.Parse(dr["AbsentDays"].ToString()) == 0 ? "" : "旷工" + tempabsentDays + ",";
                        strBuilder.Append(strAbsentDays);

                        // 病假
                        decimal SickLeaveTime = decimal.Parse(dr["SickLeaveHours"].ToString());
                        int sickLeaveDays = (int)(SickLeaveTime / Status.WorkingHours);
                        int sickLeaveHours = (int)(SickLeaveTime % Status.WorkingHours);
                        string tempSickLeaveHours = (sickLeaveDays == 0 ? "" : sickLeaveDays + "D") + (sickLeaveHours == 0 ? "" : sickLeaveHours + "H");
                        sheet.Cells[startRowIndex + rowIndex, 8] = tempSickLeaveHours;
                        string strSickLeaveHours = SickLeaveTime == 0 ? "" : "病假" + tempSickLeaveHours + ",";
                        strBuilder.Append(strSickLeaveHours);

                        noDefunTime += SickLeaveTime;

                        // 年度累计病假
                        decimal userAllYearSickLeave = new MonthStatManager().GetUserAllYearSickLeaveDay(int.Parse(dr["ApplicantID"].ToString()),
                            int.Parse(year), Status.AttendanceStatItem.SickLeaveHours);
                        int userAllYearSickLeaveDays = (int)(userAllYearSickLeave / Status.WorkingHours);
                        int userAllYearSickLeaveHours = (int)(userAllYearSickLeave % Status.WorkingHours);
                        string tempUserAllYearSickLeaveHours = (userAllYearSickLeaveDays == 0 ? "" : userAllYearSickLeaveDays + "D") + (userAllYearSickLeaveHours == 0 ? "" : userAllYearSickLeaveHours + "H");
                        sheet.Cells[startRowIndex + rowIndex, 9] = tempUserAllYearSickLeaveHours;

                        // 事假
                        decimal AffairLeaveTime = decimal.Parse(dr["AffairLeaveHours"].ToString());
                        int affairLeaveDays = (int)(AffairLeaveTime / Status.WorkingHours);
                        int affairLeaveHours = (int)(AffairLeaveTime % Status.WorkingHours);
                        string tempAffairLeaveHours = (affairLeaveDays == 0 ? "" : affairLeaveDays + "D") + (affairLeaveHours == 0 ? "" : affairLeaveHours + "H");
                        sheet.Cells[startRowIndex + rowIndex, 10] = tempAffairLeaveHours;
                        string strAffairLeaveHours = AffairLeaveTime == 0 ? "" : "事假" + tempAffairLeaveHours + ",";
                        strBuilder.Append(strAffairLeaveHours);

                        noDefunTime += AffairLeaveTime;

                        // 年假
                        int annualLeaveDays = (int)(decimal.Parse(dr["AnnualLeaveDays"].ToString()) / Status.WorkingHours);
                        int annualLeaveHours = (int)(decimal.Parse(dr["AnnualLeaveDays"].ToString()) % Status.WorkingHours);
                        string strAnnualLeaveHours = (annualLeaveDays == 0 ? "" : annualLeaveDays + "D") + (annualLeaveHours == 0 ? "" : annualLeaveHours + "H");
                        sheet.Cells[startRowIndex + rowIndex, 11] = strAnnualLeaveHours;
                        string strAnnualLeaveDays = decimal.Parse(dr["AnnualLeaveDays"].ToString()) == 0 ? "" : "年假" + strAnnualLeaveHours + ",";
                        strBuilder.Append(strAnnualLeaveDays);

                        // 年假补去年
                        int annualLastDays = (int)(decimal.Parse(dr["LastAnnualDays"].ToString()) / Status.WorkingHours);
                        int annualLastHours = (int)(decimal.Parse(dr["LastAnnualDays"].ToString()) % Status.WorkingHours);
                        string strAnnualLastHours = (annualLastDays == 0 ? "" : annualLastDays + "D") + (annualLastHours == 0 ? "" : annualLastHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 12] = strAnnualLastHours;
                        string strAnnualLastDays = decimal.Parse(dr["LastAnnualDays"].ToString()) == 0 ? "" : "年假(补)" + strAnnualLastHours + ",";
                        strBuilder.Append(strAnnualLastDays);

                        // 工作日OT
                        //decimal othours = 0;
                        //if (dr["OverTimeHours"]!=DBNull.Value)
                        //{
                        //    othours = decimal.Parse(dr["OverTimeHours"].ToString());
                        //}
                        //string tempOverTimeHours = othours == 0 ? "" : othours.ToString("#.##");
                        //string strTempOverTimeHours = tempOverTimeHours == "" ? "" : tempOverTimeHours + "H";
                        //sheet.Cells[startRowIndex + rowIndex, 12] = strTempOverTimeHours;
                        //string strOverTimeHours = othours == 0 ? "" : "工作日OT" + strTempOverTimeHours + "H";
                        //strBuilder.Append(strOverTimeHours);

                        //int overTimeDays = (int)(decimal.Parse(dr["OverTimeHours"].ToString()) / Status.WorkingHours);
                        //int overTimeHours = (int)(decimal.Parse(dr["OverTimeHours"].ToString()) % Status.WorkingHours);
                        //string tempOverTimeHours = (overTimeDays == 0 ? "" : overTimeDays + "D") + (overTimeHours == 0 ? "" : overTimeHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 12] = tempOverTimeHours;
                        //string strOverTimeHours = decimal.Parse(dr["OverTimeHours"].ToString()) == 0 ? "" : "工作日OT" + tempOverTimeHours + ",";
                        //strBuilder.Append(strOverTimeHours);

                        // 节假日OT
                        //decimal otweekend = 0;
                        //if (dr["HolidayOverTimeHours"] != DBNull.Value)
                        //{
                        //    otweekend = decimal.Parse(dr["HolidayOverTimeHours"].ToString());
                        //}
                        //int holidayOverTimeDays = (int)(otweekend / Status.WorkingHours);
                        //int holidayOverTimeHours = (int)(otweekend % Status.WorkingHours);
                        //string tempHolidayOverTimeHours = (holidayOverTimeDays == 0 ? "" : holidayOverTimeDays + "D") + (holidayOverTimeHours == 0 ? "" : holidayOverTimeHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 13] = tempHolidayOverTimeHours;
                        //string strHolidayOverTimeHours = otweekend == 0 ? "" : "节假日OT" + tempHolidayOverTimeHours + ",";
                        //strBuilder.Append(strHolidayOverTimeHours);

                        // 产假
                        decimal MaternityLeaveTime = decimal.Parse(dr["MaternityLeaveHours"].ToString());
                        int maternityLeaveDays = (int)(MaternityLeaveTime / Status.WorkingHours);
                        int maternityLeaveHours = (int)(MaternityLeaveTime % Status.WorkingHours);
                        string strMaternityLeaveHours = (maternityLeaveDays == 0 ? "" : maternityLeaveDays + "D") + (maternityLeaveHours == 0 ? "" : maternityLeaveHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 13] = maternityLeaveHours;
                        string strMaternityLeaveDays = MaternityLeaveTime == 0 ? "" : "产假" + strMaternityLeaveHours + ",";
                        strBuilder.Append(strMaternityLeaveDays);

                        noDefunTime += MaternityLeaveTime;

                        StringBuilder strBuilderOther = new StringBuilder();
                        // 婚假
                        decimal MarriageLeaveTime = decimal.Parse(dr["MarriageLeaveHours"].ToString());
                        int marriageLeaveDays = (int)(MarriageLeaveTime / Status.WorkingHours);
                        int marriageLeaveHours = (int)(MarriageLeaveTime % Status.WorkingHours);
                        string strMarriageLeaveHours = (marriageLeaveDays == 0 ? "" : marriageLeaveDays + "D") + (marriageLeaveHours == 0 ? "" : marriageLeaveHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 11] = tempmarriageLeaveHours;
                        string strMarriageLeaveDays = MarriageLeaveTime == 0 ? "" : "婚假" + strMarriageLeaveHours + ",";
                        strBuilderOther.Append(strMarriageLeaveDays);

                        noDefunTime += MarriageLeaveTime;

                        // 丧假
                        decimal FuneralLeaveTime = decimal.Parse(dr["FuneralLeaveHours"].ToString());
                        int funeralLeaveDays = (int)(FuneralLeaveTime / Status.WorkingHours);
                        int funeralLeaveHours = (int)(FuneralLeaveTime % Status.WorkingHours);
                        string strFuneralLeaveHours = (funeralLeaveDays == 0 ? "" : funeralLeaveDays + "D") + (funeralLeaveHours == 0 ? "" : funeralLeaveHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 12] = funeralLeaveHours;
                        string strFuneralLeaveDays = FuneralLeaveTime == 0 ? "" : "丧假" + strFuneralLeaveHours + ",";
                        strBuilderOther.Append(strFuneralLeaveDays);

                        noDefunTime += FuneralLeaveTime;

                        //// 出差
                        //sheet.Cells[startRowIndex + rowIndex, 16] = (decimal.Parse(dr["EvectionDays"].ToString()) / Status.WorkingHours).ToString("#.##") + "D";
                        //// 外出
                        //int egressDays = (int)(decimal.Parse(dr["AffairLeaveHours"].ToString()) / Status.WorkingHours);
                        //int egressHours = (int)(decimal.Parse(dr["AffairLeaveHours"].ToString()) % Status.WorkingHours);
                        //sheet.Cells[startRowIndex + rowIndex, 17] =
                        //    (egressDays == 0 ? "" : egressDays + "D") + (egressHours == 0 ? "" : egressHours + "H");
                        // 调休
                        decimal OffTuneTime = decimal.Parse(dr["OffTuneHours"].ToString());
                        int offTuneDays = (int)(OffTuneTime / Status.WorkingHours);
                        int offTuneHours = (int)(OffTuneTime % Status.WorkingHours);
                        string strOffTuneHours = (offTuneDays == 0 ? "" : offTuneDays + "D") + (offTuneHours == 0 ? "" : offTuneHours + "H");
                        string strOffTuneDays = OffTuneTime == 0 ? "" : "调休" + strOffTuneHours + ",";
                        strBuilderOther.Append(strOffTuneDays);

                        noDefunTime += OffTuneTime;

                        // 产前检查
                        decimal PrenatalCheckTime = decimal.Parse(dr["PrenatalCheckHours"].ToString());
                        int prenatalCheckDays = (int)(PrenatalCheckTime / Status.WorkingHours);
                        int prenatalCheckHours = (int)(PrenatalCheckTime % Status.WorkingHours);
                        string strPrenatalCheckHours = (prenatalCheckDays == 0 ? "" : prenatalCheckDays + "D") + (prenatalCheckHours == 0 ? "" : prenatalCheckHours + "H");
                        string strPrenatalCheckDays = PrenatalCheckTime == 0 ? "" : "产检" + strPrenatalCheckHours + ",";
                        strBuilderOther.Append(strPrenatalCheckDays);

                        noDefunTime += PrenatalCheckTime;


                        // 福利假
                        //decimal IncentiveHoursTime = decimal.Parse(dr["IncentiveHours"].ToString());
                        //int IncentiveHoursDays = (int)(IncentiveHoursTime / Status.WorkingHours);
                        //int IncentiveHoursHours = (int)(IncentiveHoursTime % Status.WorkingHours);
                        //string strIncentiveHours = (IncentiveHoursDays == 0 ? "" : IncentiveHoursDays + "D") + (IncentiveHoursHours == 0 ? "" : IncentiveHoursHours + "H");
                        //string strIncentiveHoursDays = IncentiveHoursTime == 0 ? "" : "福利假" + strIncentiveHours + ",";
                        //strBuilderOther.Append(strIncentiveHoursDays);

                        //noDefunTime += IncentiveHoursTime;

                        //sheet.Cells[startRowIndex + rowIndex, 18] = (decimal.Parse(dr["OffTuneHours"].ToString()) / Status.WorkingHours).ToString("#.##") + "D";
                        //sheet.Cells[startRowIndex + rowIndex, 19] = dr["Other"].ToString();
                        // 备注内容
                        sheet.Cells[startRowIndex + rowIndex, 12] = strBuilder.ToString().EndsWith(",") ? strBuilder.ToString().TrimEnd(new char[] { ',' }) : strBuilder.ToString();
                        // 其他内容
                        sheet.Cells[startRowIndex + rowIndex, 13] = strBuilderOther.ToString().EndsWith(",") ? strBuilderOther.ToString().TrimEnd(new char[] { ',' }) : strBuilderOther.ToString();
                        // 审批记录信息
                        sheet.Cells[startRowIndex + rowIndex, 16] = dr["approveremark"].ToString();

                        rowIndex++;
                    }
                    rowIndex++;
                }

                sheet.Cells[startRowIndex + rowIndex + 1, 5] = "制表人：" + ESP.Framework.BusinessLogic.UserManager.Get(userID).FullNameCN;
                sheet.Cells[startRowIndex + rowIndex + 1, 10] = "团队总经理：";
            }
            #endregion

            #region 离职人员考勤表
            startRowIndex = 4;//起始行索引
            rowIndex = 0; //顺序行索引
            if (dimDictrionary != null && dimDictrionary.Count > 0)
            {
                foreach (KeyValuePair<string, List<DataRow>> pair in dimDictrionary)
                {
                    string key = pair.Key;
                    dimSheet.Cells[startRowIndex + rowIndex, 1] = key;
                    rowIndex++;
                    int number = 1;
                    foreach (DataRow dr in pair.Value)
                    {
                        decimal noDefunTime = 0;
                        StringBuilder strBuilder = new StringBuilder();
                        dimSheet.Cells[startRowIndex + rowIndex, 1] = number++;
                        dimSheet.Cells[startRowIndex + rowIndex, 2] = dr["usercode"].ToString();
                        dimSheet.Cells[startRowIndex + rowIndex, 3] = dr["ApplicantName"].ToString();
                        dimSheet.Cells[startRowIndex + rowIndex, 4] = dr["level3"].ToString();
                        // 迟到
                        string lateCount = dr["LateCount"].ToString();
                        dimSheet.Cells[startRowIndex + rowIndex, 5] = lateCount == "0" ? "" : lateCount;
                        string strLateCount = lateCount == "0" ? "" : "迟到" + lateCount + "次" + ",";
                        strBuilder.Append(strLateCount);

                        // 早退
                        string leaveEarlyCount = dr["LeaveEarlyCount"].ToString();
                        dimSheet.Cells[startRowIndex + rowIndex, 6] = leaveEarlyCount == "0" ? "" : leaveEarlyCount;
                        string strLeaveEarlyCount = leaveEarlyCount == "0" ? "" : "早退" + leaveEarlyCount + "次" + ",";
                        strBuilder.Append(strLeaveEarlyCount);

                        // 旷工
                        int absentDays = (int)(decimal.Parse(dr["AbsentDays"].ToString()) / Status.WorkingHours);
                        int absentHours = (int)(decimal.Parse(dr["AbsentDays"].ToString()) % Status.WorkingHours);
                        string tempabsentDays = (absentDays == 0 ? "" : absentDays + "D") + (absentHours == 0 ? "" : absentHours + "H");
                        dimSheet.Cells[startRowIndex + rowIndex, 7] = tempabsentDays;
                        string strAbsentDays = decimal.Parse(dr["AbsentDays"].ToString()) == 0 ? "" : "旷工" + tempabsentDays + ",";
                        strBuilder.Append(strAbsentDays);

                        // 病假
                        decimal SickLeaveTime = decimal.Parse(dr["SickLeaveHours"].ToString());
                        int sickLeaveDays = (int)(SickLeaveTime / Status.WorkingHours);
                        int sickLeaveHours = (int)(SickLeaveTime % Status.WorkingHours);
                        string tempSickLeaveHours = (sickLeaveDays == 0 ? "" : sickLeaveDays + "D") + (sickLeaveHours == 0 ? "" : sickLeaveHours + "H");
                        dimSheet.Cells[startRowIndex + rowIndex, 8] = tempSickLeaveHours;
                        string strSickLeaveHours = SickLeaveTime == 0 ? "" : "病假" + tempSickLeaveHours + ",";
                        strBuilder.Append(strSickLeaveHours);

                        noDefunTime += SickLeaveTime;

                        // 年度累计病假
                        decimal userAllYearSickLeave = new MonthStatManager().GetUserAllYearSickLeaveDay(int.Parse(dr["ApplicantID"].ToString()),
                            int.Parse(year), Status.AttendanceStatItem.SickLeaveHours);
                        int userAllYearSickLeaveDays = (int)(userAllYearSickLeave / Status.WorkingHours);
                        int userAllYearSickLeaveHours = (int)(userAllYearSickLeave % Status.WorkingHours);
                        string tempUserAllYearSickLeaveHours = (userAllYearSickLeaveDays == 0 ? "" : userAllYearSickLeaveDays + "D") + (userAllYearSickLeaveHours == 0 ? "" : userAllYearSickLeaveHours + "H");
                        dimSheet.Cells[startRowIndex + rowIndex, 9] = tempUserAllYearSickLeaveHours;

                        // 事假
                        decimal AffairLeaveTime = decimal.Parse(dr["AffairLeaveHours"].ToString());
                        int affairLeaveDays = (int)(AffairLeaveTime / Status.WorkingHours);
                        int affairLeaveHours = (int)(AffairLeaveTime % Status.WorkingHours);
                        string tempAffairLeaveHours = (affairLeaveDays == 0 ? "" : affairLeaveDays + "D") + (affairLeaveHours == 0 ? "" : affairLeaveHours + "H");
                        dimSheet.Cells[startRowIndex + rowIndex, 10] = tempAffairLeaveHours;
                        string strAffairLeaveHours = AffairLeaveTime == 0 ? "" : "事假" + tempAffairLeaveHours + ",";
                        strBuilder.Append(strAffairLeaveHours);

                        noDefunTime += AffairLeaveTime;

                        // 年假
                        int annualLeaveDays = (int)(decimal.Parse(dr["AnnualLeaveDays"].ToString()) / Status.WorkingHours);
                        int annualLeaveHours = (int)(decimal.Parse(dr["AnnualLeaveDays"].ToString()) % Status.WorkingHours);
                        string strAnnualLeaveHours = (annualLeaveDays == 0 ? "" : annualLeaveDays + "D") + (annualLeaveHours == 0 ? "" : annualLeaveHours + "H");
                        dimSheet.Cells[startRowIndex + rowIndex, 11] = strAnnualLeaveHours;
                        string strAnnualLeaveDays = decimal.Parse(dr["AnnualLeaveDays"].ToString()) == 0 ? "" : "年假" + strAnnualLeaveHours + ",";
                        strBuilder.Append(strAnnualLeaveDays);

                        // 年假补去年
                        int annualLastDays = (int)(decimal.Parse(dr["LastAnnualDays"].ToString()) / Status.WorkingHours);
                        int annualLastHours = (int)(decimal.Parse(dr["LastAnnualDays"].ToString()) % Status.WorkingHours);
                        string strAnnualLastHours = (annualLastDays == 0 ? "" : annualLastDays + "D") + (annualLastHours == 0 ? "" : annualLastHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 12] = strAnnualLastHours;
                        string strAnnualLastDays = decimal.Parse(dr["LastAnnualDays"].ToString()) == 0 ? "" : "年假(补)" + strAnnualLastHours + ",";
                        strBuilder.Append(strAnnualLastDays);

                        // 工作日OT
                        //decimal othours = 0;
                        //if (dr["OverTimeHours"] != DBNull.Value)
                        //{
                        //    othours = decimal.Parse(dr["OverTimeHours"].ToString());
                        //}
                        //string tempOverTimeHours = othours == 0
                        //    ? "" : (othours).ToString("#.##");
                        //string strTempOverTimeHours = tempOverTimeHours == "" ? "" : tempOverTimeHours + "H";
                        //dimSheet.Cells[startRowIndex + rowIndex, 12] = strTempOverTimeHours;
                        //string strOverTimeHours = othours == 0 ? "" : "工作日OT" + strTempOverTimeHours + "H";
                        //strBuilder.Append(strOverTimeHours);

                        //int overTimeDays = (int)(decimal.Parse(dr["OverTimeHours"].ToString()) / Status.WorkingHours);
                        //int overTimeHours = (int)(decimal.Parse(dr["OverTimeHours"].ToString()) % Status.WorkingHours);
                        //string tempOverTimeHours = (overTimeDays == 0 ? "" : overTimeDays + "D") + (overTimeHours == 0 ? "" : overTimeHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 12] = tempOverTimeHours;
                        //string strOverTimeHours = decimal.Parse(dr["OverTimeHours"].ToString()) == 0 ? "" : "工作日OT" + tempOverTimeHours + ",";
                        //strBuilder.Append(strOverTimeHours);

                        // 节假日OT
                        //decimal otweekend = 0;
                        //if (dr["HolidayOverTimeHours"] != DBNull.Value)
                        //{
                        //    otweekend = decimal.Parse(dr["HolidayOverTimeHours"].ToString());
                        //}
                        //int holidayOverTimeDays = (int)(otweekend / Status.WorkingHours);
                        //int holidayOverTimeHours = (int)(otweekend % Status.WorkingHours);
                        //string tempHolidayOverTimeHours = (holidayOverTimeDays == 0 ? "" : holidayOverTimeDays + "D") + (holidayOverTimeHours == 0 ? "" : holidayOverTimeHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 13] = tempHolidayOverTimeHours;
                        //string strHolidayOverTimeHours = otweekend == 0 ? "" : "节假日OT" + tempHolidayOverTimeHours + ",";
                        //strBuilder.Append(strHolidayOverTimeHours);

                        // 产假
                        decimal MaternityLeaveTime = decimal.Parse(dr["MaternityLeaveHours"].ToString());
                        int maternityLeaveDays = (int)(MaternityLeaveTime / Status.WorkingHours);
                        int maternityLeaveHours = (int)(MaternityLeaveTime % Status.WorkingHours);
                        string strMaternityLeaveHours = (maternityLeaveDays == 0 ? "" : maternityLeaveDays + "D") + (maternityLeaveHours == 0 ? "" : maternityLeaveHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 15] = maternityLeaveHours;
                        string strMaternityLeaveDays = MaternityLeaveTime == 0 ? "" : "产假" + maternityLeaveHours + ",";
                        strBuilder.Append(strMaternityLeaveDays);

                        noDefunTime += MaternityLeaveTime;

                        StringBuilder strBuilderOther = new StringBuilder();
                        // 婚假
                        decimal MarriageLeaveTime = decimal.Parse(dr["MarriageLeaveHours"].ToString());
                        int marriageLeaveDays = (int)(MarriageLeaveTime / Status.WorkingHours);
                        int marriageLeaveHours = (int)(MarriageLeaveTime % Status.WorkingHours);
                        string strMarriageLeaveHours = (marriageLeaveDays == 0 ? "" : marriageLeaveDays + "D") + (marriageLeaveHours == 0 ? "" : marriageLeaveHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 13] = tempmarriageLeaveHours;
                        string strMarriageLeaveDays = MarriageLeaveTime == 0 ? "" : "婚假" + strMarriageLeaveHours + ",";
                        strBuilderOther.Append(strMarriageLeaveDays);

                        noDefunTime += MarriageLeaveTime;

                        // 丧假
                        decimal FuneralLeaveTime = decimal.Parse(dr["FuneralLeaveHours"].ToString());
                        int funeralLeaveDays = (int)(FuneralLeaveTime / Status.WorkingHours);
                        int funeralLeaveHours = (int)(FuneralLeaveTime % Status.WorkingHours);
                        string strFuneralLeaveHours = (funeralLeaveDays == 0 ? "" : funeralLeaveDays + "D") + (funeralLeaveHours == 0 ? "" : funeralLeaveHours + "H");
                        //sheet.Cells[startRowIndex + rowIndex, 14] = funeralLeaveHours;
                        string strFuneralLeaveDays = FuneralLeaveTime == 0 ? "" : "丧假" + strFuneralLeaveHours + ",";
                        strBuilderOther.Append(strFuneralLeaveDays);

                        noDefunTime += FuneralLeaveTime;

                        //// 出差
                        //sheet.Cells[startRowIndex + rowIndex, 16] = (decimal.Parse(dr["EvectionDays"].ToString()) / Status.WorkingHours).ToString("#.##") + "D";
                        //// 外出
                        //int egressDays = (int)(decimal.Parse(dr["AffairLeaveHours"].ToString()) / Status.WorkingHours);
                        //int egressHours = (int)(decimal.Parse(dr["AffairLeaveHours"].ToString()) % Status.WorkingHours);
                        //sheet.Cells[startRowIndex + rowIndex, 17] =
                        //    (egressDays == 0 ? "" : egressDays + "D") + (egressHours == 0 ? "" : egressHours + "H");
                        // 调休
                        //decimal OffTuneTime = decimal.Parse(dr["OffTuneHours"].ToString());
                        //int offTuneDays = (int)(OffTuneTime / Status.WorkingHours);
                        //int offTuneHours = (int)(OffTuneTime % Status.WorkingHours);
                        //string strOffTuneHours = (offTuneDays == 0 ? "" : offTuneDays + "D") + (offTuneHours == 0 ? "" : offTuneHours + "H");
                        //string strOffTuneDays = OffTuneTime == 0 ? "" : "调休" + strOffTuneHours + ",";
                        //strBuilderOther.Append(strOffTuneDays);

                        //noDefunTime += OffTuneTime;

                        // 产前检查
                        decimal PrenatalCheckTime = decimal.Parse(dr["PrenatalCheckHours"].ToString());
                        int prenatalCheckDays = (int)(PrenatalCheckTime / Status.WorkingHours);
                        int prenatalCheckHours = (int)(PrenatalCheckTime / Status.WorkingHours);
                        string strPrenatalCheckHours = (prenatalCheckDays == 0 ? "" : prenatalCheckDays + "D") + (prenatalCheckHours == 0 ? "" : prenatalCheckHours + "H");
                        string strPrenatalCheckDays = PrenatalCheckTime == 0 ? "" : "产检" + strPrenatalCheckHours + ",";
                        strBuilderOther.Append(strPrenatalCheckDays);

                        noDefunTime += PrenatalCheckTime;

                        //sheet.Cells[startRowIndex + rowIndex, 18] = (decimal.Parse(dr["OffTuneHours"].ToString()) / Status.WorkingHours).ToString("#.##") + "D";
                        //sheet.Cells[startRowIndex + rowIndex, 19] = dr["Other"].ToString();
                        // 备注内容
                        dimSheet.Cells[startRowIndex + rowIndex, 12] = strBuilder.ToString().EndsWith(",") ? strBuilder.ToString().TrimEnd(new char[] { ',' }) : strBuilder.ToString();
                        // 其他内容
                        dimSheet.Cells[startRowIndex + rowIndex, 13] = strBuilderOther.ToString().EndsWith(",") ? strBuilderOther.ToString().TrimEnd(new char[] { ',' }) : strBuilderOther.ToString();
                        // 审批记录信息
                        dimSheet.Cells[startRowIndex + rowIndex, 16] = dr["approveremark"].ToString();

                        bool isRefund = false;
                        // 是否申请了笔记本报销
                        if (isRefund)
                            dimSheet.Cells[startRowIndex + rowIndex, 14] = "√";
                        int pcRefundAmount = 0;
                        dimSheet.Cells[startRowIndex + rowIndex, 15] = pcRefundAmount > 0 ? pcRefundAmount + "" : "";

                        rowIndex++;
                    }
                    rowIndex++;
                }

                dimSheet.Cells[startRowIndex + rowIndex + 1, 5] = "制表人：" + ESP.Framework.BusinessLogic.UserManager.Get(userID).FullNameCN;
                dimSheet.Cells[startRowIndex + rowIndex + 1, 10] = "团队总经理：";
            }
            #endregion
            workbook.Saved = true;

            string tmpFileName = "MonthStat" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xlsx";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                sheet = null;
                workbook = null;
                app = null;
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return "";
        }

        #region
        ///// <summary>
        ///// 获取月统计信息
        ///// </summary>
        ///// <param name="content"></param>
        ///// <param name="type"></param>
        ///// <param name="userId"></param>
        ///// <param name="serverPath"></param>
        ///// <param name="response"></param>
        //public static void ExportTemp(string content, string type, int userId, string serverPath, HttpResponse response, string userids)
        //{
        //    if (!string.IsNullOrEmpty(content) && !(null == type))
        //    {
        //        try
        //        {
        //            string strWhere = content;
        //            string typevalue = type;
        //            int status = 0;
        //            switch (typevalue)
        //            {
        //                case "3": status = Status.MonthStatAppState_WaitStatisticians; break;
        //                case "4": status = Status.MonthStatAppState_WaitManager; break;  // 如果是团队HRAdmin导出，就导出等待总经理审批的考勤统计数据
        //                case "6": status = Status.MonthStatAppState_Passed; break;       // 如果是人力Admin导出，就导出审批通过的考勤统计数据
        //            }

        //            string[] str = strWhere.Split('|');
        //            string year = "";
        //            string month = "";
        //            if (str.Length == 2)
        //            {
        //                year = str[0].ToString();
        //                month = str[1].ToString();
        //            }

        //            ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(107);

        //            IList<ESP.Framework.Entity.UserInfo> userList = ESP.Framework.BusinessLogic.UserManager.GetAll();
        //            //DataSet ds = new ApproveLogManager().GetExprotApprovedList2(userId.ToString(), status, int.Parse(year), int.Parse(month), userids);
        //            ExportStatisticsTemp(userList, serverPath, year, month, response, status, userId);
        //        }
        //        catch (Exception ex)
        //        {
        //            ESP.Logging.Logger.Add(ex.ToString());
        //        }
        //    }
        //}

        //private static string ExportStatisticsTemp(IList<ESP.Framework.Entity.UserInfo> userList, string mapPath, string year, string month, HttpResponse response, int status, int userID)
        //{
        //    //Dictionary<string, List<DataRow>> dictrionary = new Dictionary<string, List<DataRow>>();
        //    //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    //{
        //    //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    //    {
        //    //        string teamName = dr["level2"].ToString();
        //    //        if (dictrionary.ContainsKey(teamName))
        //    //        {
        //    //            dictrionary[teamName].Add(dr);
        //    //        }
        //    //        else
        //    //        {
        //    //            List<DataRow> list = new List<DataRow>();
        //    //            list.Add(dr);
        //    //            dictrionary.Add(teamName, list);
        //    //        }
        //    //    }
        //    //}

        //    string filename = mapPath + "ExcelTemplate\\" + "MonthStat.xls";
        //    Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
        //    Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
        //    Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

        //    sheet.Cells[2, 2] = year + "年" + month + "月1日至" + year + "年" + month + "月" + DateTime.DaysInMonth(int.Parse(year), int.Parse(month)) + "日";
        //    int startRowIndex = 4;//起始行索引
        //    int rowIndex = 0; //顺序行索引
        //    AttendanceManager attMan = new AttendanceManager();

        //    if (userList != null && userList.Count > 0)
        //    {
        //        foreach (ESP.Framework.Entity.UserInfo userinfo in userList)
        //        {
        //            AttendanceDataInfo attdatainfo = attMan.GetMonthStat(userinfo.UserID,
        //                   int.Parse(year), int.Parse(month), null, null, null);

        //            sheet.Cells[startRowIndex + rowIndex, 2] = userinfo.UserID;
        //            sheet.Cells[startRowIndex + rowIndex, 3] = userinfo.FullNameCN;
        //            sheet.Cells[startRowIndex + rowIndex, 6] = ((decimal)attdatainfo.AnnualLeaveHours).ToString("#.##");
        //            rowIndex++;
        //        }
        //    }
        //    workbook.Saved = true;

        //    string tmpFileName = "MonthStat" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
        //    try
        //    {
        //        workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        workbook.Close(null, null, null);
        //        app.Workbooks.Close();
        //        app.Application.Quit();
        //        app.Quit();

        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

        //        sheet = null;
        //        workbook = null;
        //        app = null;
        //        outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {

        //    }
        //    return "";
        //}
        #endregion

        /// <summary>
        /// 获取月统计信息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <param name="serverPath"></param>
        /// <param name="response"></param>
        public static void ExportTemp(string content, string type, int userId, string serverPath, HttpResponse response, string userids)
        {
            if (!string.IsNullOrEmpty(content) && !(null == type))
            {
                try
                {
                    string strWhere = content;
                    string typevalue = type;
                    int status = 0;
                    switch (typevalue)
                    {
                        case "3": status = Status.MonthStatAppState_WaitStatisticians; break;
                        case "4": status = Status.MonthStatAppState_WaitManager; break;  // 如果是团队HRAdmin导出，就导出等待总经理审批的考勤统计数据
                        case "6": status = Status.MonthStatAppState_Passed; break;       // 如果是人力Admin导出，就导出审批通过的考勤统计数据
                    }

                    string[] str = strWhere.Split('|');
                    string year = "";
                    string month = "";
                    if (str.Length == 2)
                    {
                        year = str[0].ToString();
                        month = str[1].ToString();
                    }
                    IList<ESP.Framework.Entity.DepartmentInfo> list1 = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(107);
                    List<ESP.Framework.Entity.EmployeeInfo> list2 = new List<ESP.Framework.Entity.EmployeeInfo>();
                    if (list1 != null && list1.Count > 0)
                    {
                        foreach (ESP.Framework.Entity.DepartmentInfo d in list1)
                        {
                            list2.AddRange(ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(d.DepartmentID));
                        }
                    }

                    //IList<ESP.Framework.Entity.UserInfo> userList = ESP.Framework.BusinessLogic.UserManager.GetAll();
                    //DataSet ds = new ApproveLogManager().GetExprotApprovedList2(userId.ToString(), status, int.Parse(year), int.Parse(month), userids);
                    ExportStatisticsTemp(list2, serverPath, year, month, response, status, userId);
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                }
            }
        }

        private static string ExportStatisticsTemp(List<ESP.Framework.Entity.EmployeeInfo> userList, string mapPath, string year, string month, HttpResponse response, int status, int userID)
        {
            string filename = mapPath + "ExcelTemplate\\" + "AnnualLeaveState.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            int startRowIndex = 3;//起始行索引
            AttendanceManager attMan = new AttendanceManager();

            if (userList != null && userList.Count > 0)
            {
                for (int i = 7; i <= 12; i++)
                {
                    int number = 1;
                    int rowIndex = 0; //顺序行索引
                    foreach (ESP.Framework.Entity.EmployeeInfo userinfo in userList)
                    {
                        AttendanceDataInfo attdatainfo = attMan.GetMonthStat(userinfo.UserID,
                               int.Parse(year), i, null, null, null);

                        sheet.Cells[startRowIndex + rowIndex, 1] = number++;
                        sheet.Cells[startRowIndex + rowIndex, 2] = userinfo.Code;
                        sheet.Cells[startRowIndex + rowIndex, 3] = userinfo.FullNameCN;

                        int annualLeaveDays = (int)(((decimal)attdatainfo.AnnualLeaveHours) / Status.WorkingHours);
                        int annualLeaveHours = (int)(((decimal)attdatainfo.AnnualLeaveHours) % Status.WorkingHours);
                        string strAnnualLeaveHours = (annualLeaveDays == 0 ? "" : annualLeaveDays + "D") + (annualLeaveHours == 0 ? "" : annualLeaveHours + "H");
                        sheet.Cells[startRowIndex + rowIndex, i - 3] = strAnnualLeaveHours;
                        rowIndex++;
                    }
                }
            }
            workbook.Saved = true;

            string tmpFileName = "MonthStat" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                sheet = null;
                workbook = null;
                app = null;
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return "";
        }

        private static void outExcel(string pathandname, string filename, HttpResponse response)
        {
            response.Clear();
            FileStream fin = new FileStream(pathandname, FileMode.Open);
            response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
            response.AddHeader("Connection", "Close");
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.AddHeader("Content-Length", fin.Length.ToString());
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.ContentType = "application/ms-excel";    //设置输出文件类型为excel文件。 
            
            byte[] buf = new byte[1024];
            while (true)
            {
                int length = fin.Read(buf, 0, buf.Length);
                if (length > 0)
                    response.OutputStream.Write(buf, 0, length);
                if (length < buf.Length)
                    break;
            }
            fin.Close();
            response.Flush();
            response.Close();
            FileInfo finfo = new FileInfo(pathandname);
            finfo.Delete();
        }
        #endregion

        #region 导出门卡信息
        /// <summary>
        /// 导出门卡信息
        /// </summary>
        public static void ExprotCardNos(DataSet ds, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "UserCardNoInfo.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int startRowIndex = 2;
                int rowIndex = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sheet.Cells[startRowIndex + rowIndex, 1] = rowIndex;
                    sheet.Cells[startRowIndex + rowIndex, 2] = dr["usercode"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 3] = dr["EmployeeName"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 4] = dr["CardNo"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 5] = dr["Department"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 6] = dr["CardState"].ToString() == "1" ? "使用中" : "停用";
                    rowIndex++;
                }
            }
            workbook.Saved = true;

            string tmpFileName = "UserCardNoInfo_" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                sheet = null;
                workbook = null;
                app = null;
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, "门卡信息" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        #endregion

        #region 导出考勤统计信息
        /// <summary>
        /// 导出考勤统计信息
        /// </summary>
        public static void ExprotAttendanceStat(DataSet ds, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "AttendanceStat.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int startRowIndex = 3;
                int rowIndex = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sheet.Cells[startRowIndex + rowIndex, 1] = rowIndex;
                    sheet.Cells[startRowIndex + rowIndex, 2] = dr["UserCode"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 3] = dr["EmployeeName"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 4] = dr["Department"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 5] = dr["Position"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 6] = dr["AttendanceYear"].ToString() + "年" + dr["AttendanceMonth"].ToString() + "月";
                    sheet.Cells[startRowIndex + rowIndex, 7] = dr["LateCount1"].ToString() == "0" ? "" : dr["LateCount1"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 8] = dr["LateCount2"].ToString() == "0" ? "" : dr["LateCount2"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 9] = dr["OvertimeCount"].ToString() == "0" ? "" : dr["OvertimeCount"].ToString();

                    sheet.Cells[startRowIndex + rowIndex, 10] = dr["AbsentCount1"].ToString() == "0" ? "" : dr["AbsentCount1"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 11] = dr["AbsentCount2"].ToString() == "0" ? "" : dr["AbsentCount2"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 12] = dr["AbsentCount3"].ToString() == "0" ? "" : dr["AbsentCount3"].ToString();
                    sheet.Cells[startRowIndex + rowIndex, 13] = dr["LeaveEarly"].ToString() == "0" ? "" : dr["LeaveEarly"].ToString();
                    rowIndex++;
                }
            }
            workbook.Saved = true;

            string tmpFileName = "AttendanceStat_" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                sheet = null;
                workbook = null;
                app = null;
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, "考勤统计信息" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        #endregion
    }
}
