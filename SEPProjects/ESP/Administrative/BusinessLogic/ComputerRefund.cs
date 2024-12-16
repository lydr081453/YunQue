using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;

namespace ESP.Administrative.BusinessLogic
{
    /// <summary>
    /// 笔记本报销业务操作类
    /// </summary>
    public class ComputerRefund
    {
        /// <summary>
        /// 报销记录业务操作类
        /// </summary>
        private static RefundManager refundManager = new RefundManager();

        public ComputerRefund()
        {

        }

        /// <summary>
        /// 判断是否申请了笔记本报销，并且当月工作满15天，如果是返回true，否则返回false
        /// </summary>
        /// <param name="userid">用户标号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="isEnoughWorkingDays">是否具有足够的工作天数</param>
        /// <param name="noDefundMattersTime">15个工作日的计算不包含的考勤事由的总时间</param>
        /// <returns>如果申请了笔记本报销返回true，否则返回false</returns>
        public static bool isComputerRefund(int userid, int year, int month, out bool isEnoughWorkingDays, decimal noDefundMatterTimes)
        {
            isEnoughWorkingDays = false;
            int refundDayCount = 0;
            // 假期数量
            int holidayCount = 0;
            // 一个月总共有多少天
            int daysInMonth = DateTime.DaysInMonth(year, month);
            
            // 如果员工是当月离职
            ESP.HumanResource.Entity.DimissionFormInfo dimissionModel = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(userid);
            
            RefundInfo refundModel = refundManager.GetRefundInfos(userid, RefundType.NetBookType).FirstOrDefault();

            ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(userid);

            if (dimissionModel != null && dimissionModel.LastDay != null && dimissionModel.LastDay.Value.Month == month)
            {
                daysInMonth = dimissionModel.LastDay.Value.Day;
            }
            else if (dimissionModel == null && (year == DateTime.Now.Year && month == DateTime.Now.Month))
            {
                daysInMonth = DateTime.Now.Day;
            }

            if (empModel.JoinDate != null && empModel.JoinDate.Value.Year == year && empModel.JoinDate.Value.Month == month)
            {
                daysInMonth = daysInMonth - empModel.JoinDate.Value.Day + 1;
            }

            refundDayCount = daysInMonth;

            

            // 每月的第一天
            DateTime firstDate = new DateTime(year, month, 1);
            // 每月的最后一天
            DateTime lastDate = new DateTime(year, month, daysInMonth);

            
            // 如果为申请笔记本报销
            if (refundModel != null && (refundModel.Type == (int)RefundType.NetBookType && refundModel.Status == (int)Common.RefundStatus.BeginStatus))//改为建河租赁本登记，有租赁信息的不予报销
                return false;
            else
            {
                noDefundMatterTimes = 0;
                firstDate = new DateTime(year, month, 1);
                AttendanceManager attendanceManager = new AttendanceManager();
                HolidaysInfoDataProvider holidaysDAL = new HolidaysInfoDataProvider();

                if (empModel.JoinDate != null && empModel.JoinDate.Value.Year == year && empModel.JoinDate.Value.Month == month)
                {
                    firstDate = empModel.JoinDate.Value;
                }

                DateTime beginDateTime = firstDate;
                DateTime endDateTime = lastDate;

                AttendanceDataInfo dataInfo = attendanceManager.GetTimeQuantumStat(userid, beginDateTime, endDateTime, null, null, null);
                // 旷工，病假，事假，产假，婚假，丧假，调休,产前检查
                noDefundMatterTimes +=
                    ((dataInfo.AbsentHours + dataInfo.SickLeaveHours + dataInfo.AffiairLeaveHours + dataInfo.MaternityLeaveHours
                    + dataInfo.MarriageLeaveHours + dataInfo.FuneralLeaveHours + dataInfo.OffTuneHours + dataInfo.PrenatalCheckHours)
                    - dataInfo.HolidayOverTimeHours);
                // 获得时间段内假期数量
                HashSet<int> h = holidaysDAL.GetHolidayListByMonthDate(beginDateTime, endDateTime);
                holidayCount = h.Count;

                // 获得用户当月考勤统计信息
                decimal totalDays = noDefundMatterTimes / Status.WorkingHours;
                decimal workingDays = refundDayCount - (totalDays + holidayCount);

                if (workingDays >= Status.MinWorkingDays)
                {
                    isEnoughWorkingDays = true;
                    return true;
                }
                else
                {
                    isEnoughWorkingDays = false;
                    return true;
                }
            }


        }
    }
}