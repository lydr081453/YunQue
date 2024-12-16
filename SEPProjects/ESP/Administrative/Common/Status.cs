using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Common
{
    public class Statics 
    {
        public static string[] RequestForSeal_SealType = {"公章","合同章","法人章","其他" };
        public static string[] RequestForSeal_FileType = {"公告类","规章制度类","结算单","证明类","营业执照","开户许可证" };
        public static string[] RequestForSealStatus_Names = { "审批驳回", "保存", "审批中", "审批完成" };
    }

    public class Status
    {
        public enum RequestForSealStatus
        {
            Rejected = 0,
            Save = 1,
            Auditing =2,
            Audited = 3

        }

        #region 考勤时间点和是否执行弹性
        /// <summary>
        /// 是否执行弹性工作日 1表示是，0表示否
        /// </summary>
        public static string FlexibleWorking = ESP.Configuration.ConfigurationManager.SafeAppSettings["FlexibleWorking"];

        /// <summary>
        /// 默认打卡记录时间
        /// </summary>
        public static string ApproveTime = ESP.Configuration.ConfigurationManager.SafeAppSettings["ApproveTime"];

        // 迟到时间点
        private static TimeSpan _lateTime;
        /// <summary>
        /// 迟到时间点
        /// </summary>
        public static TimeSpan LateTime
        {
            get
            {
                string[] lateTimeArr =
                    ESP.Configuration.ConfigurationManager.SafeAppSettings["LateTime"].Split(new char[] { ':' });
                _lateTime = new TimeSpan(Convert.ToInt32(lateTimeArr[0]), Convert.ToInt32(lateTimeArr[1]), 0);
                return _lateTime;
            }
        }

        //上午旷工时间点
        private static TimeSpan _amAbsentTime;
        /// <summary>
        /// 上午旷工时间点
        /// </summary>
        public static TimeSpan AMAbsentTime
        {
            get
            {
                string[] amAbsentTimeArr =
                    ESP.Configuration.ConfigurationManager.SafeAppSettings["AMAbsentTime"].Split(new char[] { ':' });
                _amAbsentTime = new TimeSpan(Convert.ToInt32(amAbsentTimeArr[0]), Convert.ToInt32(amAbsentTimeArr[1]), 0);
                return _amAbsentTime;
            }

        }

        // 下午旷工时间点
        private static TimeSpan _pmAbsentTime;
        /// <summary>
        /// 下午旷工时间点
        /// </summary>
        public static TimeSpan PMAbsentTime
        {
            get
            {
                string[] pmAbsentTime =
                    ESP.Configuration.ConfigurationManager.SafeAppSettings["PMAbsentTime"].Split(new char[] { ':' });
                _pmAbsentTime = new TimeSpan(Convert.ToInt32(pmAbsentTime[0]), Convert.ToInt32(pmAbsentTime[1]), 0);
                return _pmAbsentTime;
            }
        }

        /// <summary>
        /// 每日工作小时数
        /// </summary>
        public static TimeSpan HoursWorked =
            new TimeSpan(Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["HoursWorked"]), 0, 0);

        // 下班时间点
        private static TimeSpan _offWorkTime;
        /// <summary>
        /// 下班时间点
        /// </summary>
        public static TimeSpan OffWorkTime
        {
            get
            {
                string[] offWorkTimeArr =
                    ESP.Configuration.ConfigurationManager.SafeAppSettings["OffWorkTime"].Split(new char[] { ':' });
                _offWorkTime = new TimeSpan(Convert.ToInt32(offWorkTimeArr[0]), Convert.ToInt32(offWorkTimeArr[1]), 0);
                return _offWorkTime;
            }
        }

        // 上班时间点
        private static TimeSpan _goWorkTime;
        /// <summary>
        /// 上班时间点
        /// </summary>
        public static TimeSpan GoWorkTime
        {
            get
            {
                string[] goWorkTimeArr =
                    ESP.Configuration.ConfigurationManager.SafeAppSettings["GoWorkTime"].Split(new char[] { ':' });
                _goWorkTime = new TimeSpan(Convert.ToInt32(goWorkTimeArr[0]), Convert.ToInt32(goWorkTimeArr[1]), 0);
                return _goWorkTime;
            }
        }

        // OT时间点
        private static TimeSpan _overTime;
        /// <summary>
        /// OT时间点
        /// </summary>
        public static TimeSpan OverTime
        {
            get
            {
                string[] overTimeArr =
                    ESP.Configuration.ConfigurationManager.SafeAppSettings["OverTime"].Split(new char[] { ':' });
                _overTime = new TimeSpan(Convert.ToInt32(overTimeArr[0]), Convert.ToInt32(overTimeArr[1]), 0);
                return _overTime;
            }
        }

        // OT过22点
        private static TimeSpan _overTwentyTwo;
        /// <summary>
        /// OT过22点
        /// </summary>
        public static TimeSpan OverTwentyTwo
        {
            get
            {
                string[] overTwentyTwoArr =
                    ESP.Configuration.ConfigurationManager.SafeAppSettings["OverTime2"].Split(new char[] { ':' });
                _overTwentyTwo = new TimeSpan(Convert.ToInt32(overTwentyTwoArr[0]), Convert.ToInt32(overTwentyTwoArr[1]), 0);
                return _overTwentyTwo;
            }
        }

        /// <summary>
        /// 空的时间值 1900-01-01
        /// </summary>
        public static string EmptyTime = ESP.Configuration.ConfigurationManager.SafeAppSettings["EmptyTime"];

        /// <summary>
        /// 上午下班时间
        /// </summary>
        public static TimeSpan AMOffTime
        {
            get
            {
                return new TimeSpan(12, 30, 0);
            }
        }

        /// <summary>
        /// 下午上班时间
        /// </summary>
        public static TimeSpan PMGoTime
        {
            get
            {
                return new TimeSpan(13, 30, 0);
            }
        }
        #endregion

        #region 考勤不同图标标签元素
        #region 未提交的
        /// <summary>
        /// 默认编辑页面
        /// </summary>
        public static string defaultUrl = "{0}MattersEdit.aspx?matterid={1}&tabtype={2}&backurl=AttendanceSelect.aspx";
        /// <summary>
        /// 未提交迟到图标
        /// </summary>
        public static string noSublateImg = "<span class=\"spanimage_align\"><img src=\"../images/calendar/1chidao.jpg\" title=\"迟到\"/>&nbsp;</span>";
        /// <summary>
        /// 未提交旷工
        /// </summary>
        public static string noSubabsent = "<span class=\"spanimage_align\"><img src=\"../images/calendar/1kuanggong.jpg\" title=\"旷工\"/>&nbsp;</span>";
        /// <summary>
        /// 未提交早退
        /// </summary>
        public static string noSubleaveEarly = "<span class=\"spanimage_align\"><img src=\"../images/calendar/1zaotui.jpg\" title=\"早退\"/>&nbsp;</span>";
        /// <summary>
        /// 未提交请假
        /// </summary>
        public static string noSubleave = "<span class=\"spanimage_align\"><a href=\"{0}\" {1} style=\"text-decoration:underline;\"><img src=\"../images/calendar/1qingjia.jpg\" title=\"请假 未提交\"/>&nbsp;</a></span>";
        /// <summary>
        /// 未提交OT
        /// </summary>
        public static string noSubovertime = "<span class=\"spanimage_align\"><a href=\"{0}\" {1} style=\"text-decoration:underline;\"><img src=\"../images/calendar/1jiaban.jpg\" title=\"OT 未提交\"/>&nbsp;</a></span>";
        /// <summary>
        /// 未提交年休假
        /// </summary>
        public static string noSubannualLeave = "<span class=\"spanimage_align\"><a href=\"{0}\" {1} style=\"text-decoration:underline;\"><img src=\"../images/calendar/1nianjia.jpg\" title=\"年假 未提交\"/>&nbsp;</a></span>";
        /// <summary>
        /// 未提交出差
        /// </summary>
        public static string noSubevection = "<span class=\"spanimage_align\"><a href=\"{0}\" {1} style=\"text-decoration:underline;\"><img src=\"../images/calendar/1chuchai.jpg\" title=\"出差 未提交\"/>&nbsp;</a></span>";
        /// <summary>
        /// 未提交外出
        /// </summary>
        public static string noSubegress = "<span class=\"spanimage_align\"><a href=\"{0}\" {1} style=\"text-decoration:underline;\"><img src=\"../images/calendar/1waichu.jpg\" title=\"外出 未提交\"/>&nbsp;</a></span>";
        /// <summary>
        /// 未提交其他
        /// </summary>
        public static string noSubother = "<span class=\"spanimage_align\"><a href=\"{0}\" {1} style=\"text-decoration:underline;\"><img src=\"../images/calendar/1qita.jpg\" title=\"其他 未提交\"/>&nbsp;</a></span>";
        /// <summary>
        /// 未提交调休
        /// </summary>
        public static string noSuboffTune = "<span class=\"spanimage_align\"><a href=\"{0}\" {1} style=\"text-decoration:underline;\"><img src=\"../images/calendar/1tiaoxiu.jpg\" title=\"调休 未提交\"/>&nbsp;</a></span>";
        #endregion
        #region 未审批通过的
        /// <summary>
        /// 未审批迟到图标
        /// </summary>
        public static string noApplateImg = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2chidao.jpg\" title=\"迟到\"/>&nbsp;</span>";
        /// <summary>
        /// 未审批旷工
        /// </summary>
        public static string noAppabsent = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2kuanggong.jpg\" title=\"旷工\"/>&nbsp;</span>";
        /// <summary>
        /// 未审批早退
        /// </summary>
        public static string noAppleaveEarly = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2zaotui.jpg\" title=\"早退\"/>&nbsp;</span>";
        /// <summary>
        /// 未审批请假
        /// </summary>
        public static string noAppleave = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2qingjia.jpg\" title=\"请假 等待审批\"/>&nbsp;</span>";
        /// <summary>
        /// 未审批OT
        /// </summary>
        public static string noAppovertime = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2jiaban.jpg\" title=\"OT 等待审批\"/>&nbsp;</span>";
        /// <summary>
        /// 年休假
        /// </summary>
        public static string noAppannualLeave = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2nianjia.jpg\" title=\"年假 等待审批\"/>&nbsp;</span>";
        /// <summary>
        /// 未审批出差
        /// </summary>
        public static string noAppevection = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2chuchai.jpg\" title=\"出差 等待审批\"/>&nbsp;</span>";
        /// <summary>
        /// 未审批外出
        /// </summary>
        public static string noAppegress = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2waichu.jpg\" title=\"外出\"/>&nbsp;</span>";
        /// <summary>
        /// 未审批其他
        /// </summary>
        public static string noAppother = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2qita.jpg\" title=\"其他\"/>&nbsp;</span>";
        /// <summary>
        /// 未审批调休
        /// </summary>
        public static string noAppoffTune = "<span class=\"spanimage_align\"><img src=\"../images/calendar/2tiaoxiu.jpg\" title=\"调休 等待审批\"/>&nbsp;</span>";
        #endregion
        #region 审批通过
        /// <summary>
        /// 迟到图标
        /// </summary>
        public static string lateImg = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3chidao.jpg\" title=\"迟到\"/>&nbsp;</span>";
        /// <summary>
        /// 旷工
        /// </summary>
        public static string absent = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3kuanggong.jpg\" title=\"旷工\"/>&nbsp;</span>";
        /// <summary>
        /// 早退
        /// </summary>
        public static string leaveEarly = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3zaotui.jpg\" title=\"早退\"/>&nbsp;</span>";
        /// <summary>
        /// 请假
        /// </summary>
        public static string leave = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3qingjia.jpg\" title=\"请假 审批通过\"/>&nbsp;</span>";
        /// <summary>
        /// OT
        /// </summary>
        public static string overtime = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3jiaban.jpg\" title=\"OT 审批通过\"/>&nbsp;</span>";
        /// <summary>
        /// 年休假
        /// </summary>
        public static string annualLeave = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3nianjia.jpg\" title=\"年假 审批通过\"/>&nbsp;</span>";
        /// <summary>
        /// 出差
        /// </summary>
        public static string evection = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3chuchai.jpg\" title=\"出差 审批通过\"/>&nbsp;</span>";
        /// <summary>
        /// 外出
        /// </summary>
        public static string egress = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3waichu.jpg\" title=\"外出\"/>&nbsp;</span>";
        /// <summary>
        /// 其他
        /// </summary>
        public static string other = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3qita.jpg\" title=\"其他\"/>&nbsp;</span>";
        /// <summary>
        /// 调休
        /// </summary>
        public static string offTune = "<span class=\"spanimage_align\"><img src=\"../images/calendar/3tiaoxiu.jpg\" title=\"调休 审批通过\"/>&nbsp;</span>";
        #endregion
        #endregion

        #region 日期显示的颜色
        /// <summary>
        /// 非正常
        /// </summary>
        public static string improper = "red";
        /// <summary>
        /// 正常
        /// </summary>
        public static string normal = "black";
        /// <summary>
        /// 节假日
        /// </summary>
        public static string weekend = "green";
        /// <summary>
        /// 晚上过九点
        /// </summary>
        public static string overNine = "blue";
        /// <summary>
        /// 手动录入的时间
        /// </summary>
        public static string operatorTime = "darkred";
        #endregion

        #region 考情统计项标识
        /// <summary>
        /// 考情统计项标识
        /// </summary>
        public enum AttendanceStatItem
        {
            /// <summary>
            /// 工作天数
            /// </summary>
            WorkHours = 1,
            /// <summary>
            /// 迟到次数
            /// </summary>
            LateCount = 2,
            /// <summary>
            /// 早退次数
            /// </summary>
            LeaveEarlyCount = 3,
            /// <summary>
            /// 病假天数
            /// </summary>
            SickLeaveHours = 4,
            /// <summary>
            /// 事假天数
            /// </summary>
            AffairLeaveHours = 5,
            /// <summary>
            /// 年假天数
            /// </summary>
            AnnualLeaveDays = 6,
            /// <summary>
            /// 产假天数
            /// </summary>
            MaternityLeaveDays = 7,
            /// <summary>
            /// 婚嫁天数
            /// </summary>
            MarriageLeaveDays = 8,
            /// <summary>
            /// 丧家天数
            /// </summary>
            FuneralLeaveDays = 9,
            /// <summary>
            /// 旷工天数
            /// </summary>
            AbsentDays = 10,
            /// <summary>
            /// OT天数
            /// </summary>
            OverTimeHours = 11,
            /// <summary>
            /// 出差天数
            /// </summary>
            EvectionDays = 12,
            /// <summary>
            /// 外出天数
            /// </summary>
            EgressHours = 13,
            /// <summary>
            /// 补年假天数
            /// </summary>
            LastAnnualDays = 14
        }
        #endregion

        #region OT单审批状态 状态，1未提交，2等待审批，3审批通过，4等待团队HR审批，5审批驳回，6撤销状态
        /// <summary>
        /// 未提交
        /// </summary>
        public const int OverTimeState_NotSubmit = 1;
        /// <summary>
        /// 等待审批
        /// </summary>
        public const int OverTimeState_WaitDirector = 2;
        /// <summary>
        /// 审批通过
        /// </summary>
        public const int OverTimeState_Passed = 3;
        /// <summary>
        /// 等待团队HR审批
        /// </summary>
        public const int OverTimeState_WaitHR = 4;
        /// <summary>
        /// 审批驳回
        /// </summary>
        public const int OverTimeState_Overrule = 5;
        /// <summary>
        /// 撤销状态
        /// </summary>
        public const int OverTimeState_Cancel = 6;
        #endregion

        #region 考勤事由类型
        /// <summary>
        /// 病假
        /// </summary>
        public const int MattersType_Sick = 1;
        /// <summary>
        /// 事假
        /// </summary>
        public const int MattersType_Leave = 2;
        /// <summary>
        /// 年假
        /// </summary>
        public const int MattersType_Annual = 3;
        /// <summary>
        /// 婚假
        /// </summary>
        public const int MattersType_Marriage = 4;
        /// <summary>
        /// 产假
        /// </summary>
        public const int MattersType_Maternity = 5;
        /// <summary>
        /// 丧假
        /// </summary>
        public const int MattersType_Bereavement = 6;
        /// <summary>
        /// 出差
        /// </summary>
        public const int MattersType_Travel = 7;
        /// <summary>
        /// 外出
        /// </summary>
        public const int MattersType_Out = 8;
        /// <summary>
        /// 调休
        /// </summary>
        public const int MattersType_OffTune = 9;
        /// <summary>
        /// 其他
        /// </summary>
        public const int MattersType_Other = 10;
        /// <summary>
        /// 产前检查
        /// </summary>
        public const int MattersType_PrenatalCheck = 11;
        /// <summary>
        /// 福利假
        /// </summary>
        public const int MattersType_Incentive = 12;
        /// <summary>
        /// 晚到申请
        /// </summary>
        public const int MattersType_OTLate = 13;
        /// <summary>
        /// 陪产假
        /// </summary>
        public const int MattersType_PeiChanJia = 14;
        /// <summary>
        /// 补休去年年假
        /// </summary>
        public const int MattersType_Annual_Last = 15;
        #endregion

        #region 考勤事由审批状态
        /// <summary>
        /// 未提交审批
        /// </summary>
        public static int MattersState_NoSubmit = 1;
        /// <summary>
        /// 已审批通过
        /// </summary>
        public static int MattersState_Passed = 2;
        /// <summary>
        /// 等待总监审批
        /// </summary>
        public static int MattersState_WaitDirector = 3;
        /// <summary>
        /// 等待人力审批
        /// </summary>
        public static int MattersState_WaitHR = 4;
        /// <summary>
        /// 审批驳回
        /// </summary>
        public static int MattersState_Overrule = 5;
        /// <summary>
        /// 撤销状态
        /// </summary>
        public static int MattersState_Cancel = 6;
        #endregion

        #region 考勤单据类型
        /// <summary>
        /// 考勤单据类型:考勤月统计,请假单,OT单,出差单,调休单
        /// </summary>
        public enum MattersSingle
        {
            /// <summary>
            /// 考勤月统计
            /// </summary>
            MattersSingle_Attendance = 1,
            /// <summary>
            /// 请假单
            /// </summary>
            MattersSingle_Leave = 2,
            /// <summary>
            /// OT单
            /// </summary>
            MattersSingle_OverTime = 3,
            /// <summary>
            /// 出差单
            /// </summary>
            MattersSingle_Travel = 4,
            /// <summary>
            /// 调休单
            /// </summary>
            MattersSingle_OffTune = 5,
            /// <summary>
            /// 其他事由单
            /// </summary>
            MattersSingle_Other = 6,
            /// <summary>
            /// 外出单
            /// </summary>
            MattersSingle_Out = 7,
            /// <summary>
            /// 晚到申请
            /// </summary>
            MattersSingle_OTLate = 8,

        }
        #endregion

        #region 人员考勤类型
        /// <summary>
        ///  正常
        /// </summary>
        public static int UserBasicAttendanceType_Normal = 1;
        /// <summary>
        /// 弹性
        /// </summary>
        public static int UserBasicAttendanceType_Spring = 2;
        /// <summary>
        /// 特殊
        /// </summary>
        public static int UserBasicAttendanceType_Special = 3;
        /// <summary>
        /// 不统计考勤，但是记录假期
        /// </summary>
        public static int UserBasicAttendanceType_NotStat = 4;
        #endregion

        #region OT单使用状态
        /// <summary>
        /// 未使用
        /// </summary>
        public static int SingleOverTimeUserState_NoUse = 1;
        /// <summary>
        /// 已经使用
        /// </summary>
        public static int SingleOverTimeUserState_Used = 2;
        /// <summary>
        /// 部分使用
        /// </summary>
        public static int SingleOverTimeUserState_PartUse = 3;
        #endregion

        #region OT类型
        /// <summary>
        /// 工作日
        /// </summary>
        public static int OverTimeType_Working = 1;
        /// <summary>
        /// 节假日
        /// </summary>
        public static int OverTimeType_Holiday = 2;
        #endregion

        #region 考勤时间参数配置
        #region 考勤基本信息配置
        /// <summary>
        /// 一天工作小时数
        /// </summary>
        public static int WorkingHours = 8;
        /// <summary>
        /// 上午工作小时数
        /// </summary>
        public static int AMWorkingHours = 3;
        /// <summary>
        /// 下班打卡最后截止时间
        /// </summary>
        public static string OffWorkTimePoint = "06:00:00";
        /// <summary>
        /// 迟到分钟
        /// </summary>
        public static int LateMin = 30;
        /// <summary>
        /// 系统启用时间
        /// </summary>
        public static DateTime systemStratTime = new DateTime(2009, 7, 1);
        #endregion

        #region 考勤事由提交期限设置
        /// <summary>
        /// 提交期限自考勤异常当日起7天内有效
        /// </summary>
        public static int SubmitTerm = 0;
        /// <summary>
        /// 执行限期提交考勤事由的开始时间
        /// </summary>
        public static DateTime ExecuteRestrictTime = new DateTime(2009, 8, 10);
        #endregion

        #region OT
        /// <summary>
        /// 工作日OT超过3小时算OT，并且第二天可以晚来一小时
        /// </summary>
        public static int WorkingDays_OverTime1 = 3;
        /// <summary>
        /// OT超过三个小时可以晚来一个小时
        /// </summary>
        public static int LateGoWorkTime_OverTime1 = 1;
        /// <summary>
        /// 工作日OT超过6小时算OT，并且第二天可以晚来
        /// </summary>
        public static int WorkingDays_OverTime2 = 6;
        /// <summary>
        /// 节假日OT超过8小时按8小时计算
        /// </summary>
        public static int Holiday_OverTime1 = 8;
        /// <summary>
        /// 节假日OT最短小时数
        /// </summary>
        public static int Holiday_MinOverTime = 4;
        #endregion

        #region 上下班时间
        /// <summary>
        /// 上午上班时间和下午上班时间的时间间隔,四个小时
        /// </summary>
        public static int GoWorkTime_AMGapPM = 4;
        /// <summary>
        /// 缺勤多长时间算迟到，一个小时内
        /// </summary>
        public static int NumberOfHours_Late = 1;
        /// <summary>
        /// 缺勤1个小时以上，3个小时以下算旷工半天，如果缺勤3个小时以上算旷工1天
        /// </summary>
        public static int NumberOfHours_Absent = 3;
        /// <summary>
        /// 上午上班时间的缓冲时间，6分钟
        /// </summary>
        public static int GoWorkTime_BufferMinute = 1;
        /// <summary>
        /// 默认上班时间
        /// </summary>
        public static DateTime DefaultGoWorkTime = new DateTime(1900, 01, 01, 9, 30, 0);
        /// <summary>
        /// 默认下班时间
        /// </summary>
        public static DateTime DefaultOffWorkTime = new DateTime(1900, 01, 01, 18, 30, 0);
        /// <summary>
        /// 北京默认上班时间
        /// </summary>
        public static DateTime BJDefaultGoWorkTime = new DateTime(1900, 01, 01, 9, 30, 0);
        /// <summary>
        /// 北京默认下班时间
        /// </summary>
        public static DateTime BJDefaultOffWorkTime = new DateTime(1900, 01, 01, 18, 30, 0);

        #endregion

        #region 年假
        /// <summary>
        /// 年假天数最大值
        /// </summary>
        public static int MaxAnnualLeaveDay = 15;
        /// <summary>
        /// 年假结转最后有效日期
        /// </summary>
        public static DateTime ALCarryOverLastDate = new DateTime(2009, 6, 30);
        /// <summary>
        /// 一年的天数
        /// </summary>
        public static int OneYearDays = 365;
        /// <summary>
        /// 是否有奖励年假
        /// </summary>
        public static bool IsAwardAnnualLeave = true;
        /// <summary>
        /// 奖励年假1，3天
        /// </summary>
        public static int AwardAnnualLeaveLevel1 = 3;
        /// <summary>
        /// 奖励年假2，5天
        /// </summary>
        public static int AwardAnnualLeaveLevel2 = 5;
        /// <summary>
        /// 奖励年假3，8天
        /// </summary>
        public static int AwardAnnualLeaveLevel3 = 8;
        #region 年假截止日期信息
        /// <summary>
        /// 年假截止月份
        /// </summary>
        public static int AnnualLeaveLastMonth = 12;
        /// <summary>
        /// 年假截止日期
        /// </summary>
        public static int AnnualLeaveLastDay = 31;
        #endregion
        #endregion
        #endregion

        #region 考勤月统计审批状态
        /// <summary>
        /// 未提交
        /// </summary>
        public static int MonthStatAppState_NoSubmit = 1;
        /// <summary>
        /// 等待总监审批
        /// </summary>
        public static int MonthStatAppState_WaitDirector = 2;
        /// <summary>
        /// 等待考勤统计员审批
        /// </summary>
        public static int MonthStatAppState_WaitStatisticians = 3;
        /// <summary>
        /// 等待HRAdmin审批
        /// </summary>
        public static int MonthStatAppState_WaitHRAdmin = 4;
        /// <summary>
        /// 等待团队经理审批
        /// </summary>
        public static int MonthStatAppState_WaitManager = 5;
        /// <summary>
        /// 等待考勤管理员审批
        /// </summary>
        public static int MonthStatAppState_WaitADAdmin = 6;
        /// <summary>
        /// 审批通过
        /// </summary>
        public static int MonthStatAppState_Passed = 7;
        /// <summary>
        /// 审批驳回
        /// </summary>
        public static int MonthStatAppState_Overrule = 8;
        #endregion

        #region 审批记录表状态标识
        /// <summary>
        /// 考勤统计信息未审批
        /// </summary>
        public static int ApproveState_NoPassed = 0;
        /// <summary>
        /// 考勤统计信息审批通过
        /// </summary>
        public static int ApproveState_Passed = 1;
        /// <summary>
        /// 考勤统计信息审批驳回
        /// </summary>
        public static int ApproveState_Overrule = 2;
        #endregion

        #region 考勤驳回后开放的天数
        public static int ApproveGracePeriodDays = 3;
        #endregion

        #region 笔记本租赁
        /// <summary>
        /// 享有笔记本租赁福利的最小工作天数
        /// </summary>
        public static int MinWorkingDays = 15;
        /// <summary>
        /// 笔记本租赁15个工作日计算不包含的事由类型有：病假，事假，产假，调休，婚假，丧假，产前检查
        /// </summary>
        public static string NoRefundMatterTypes =
            MattersType_Sick + "," + MattersType_Leave + "," + MattersType_Maternity + "," + MattersType_OffTune + "," + MattersType_Marriage + "," + MattersType_Bereavement + "," + MattersType_PrenatalCheck + "," + MattersType_PeiChanJia;
        /// <summary>
        /// 笔记本租赁的金额
        /// </summary>
        //public static int RefundAmount = 150;
        #endregion


        /// <summary>
        /// TimeSheet类型名称
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> TimeSheetTypeName()
        {
            Dictionary<int, string> name = new Dictionary<int, string>();
            name.Add(1, "工作记录");
            name.Add(2, "长假");
            name.Add(10, "迟到");
            name.Add(11, "早退");
            name.Add(12, "旷工");
            return name;
        }

        public static string[] TimeSheetCommitStatusNames = {"未提交","驳回","提交","已审批","待HR审批" };
    }

    /// <summary>
    /// TimeSheet提交状态
    /// </summary>
    public enum TimeSheetCommitStatus
    {
        /// <summary>
        /// 保存
        /// </summary>
        Save = 0,
        /// <summary>
        /// 审批驳回
        /// </summary>
        Reject = 1,
        /// <summary>
        /// 已提交
        /// </summary>
        Commit = 2,
        /// <summary>
        /// 审批通过
        /// </summary>
        Passed = 3,
        /// <summary>
        /// 待HR审批
        /// </summary>
        WaitHR = 4
        
    }

    /// <summary>
    /// TimeSheetCommit类型
    /// </summary>
    public enum TimeSheetCommitType
    {
        /// <summary>
        /// 正常记录
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 长假
        /// </summary>
        Holiday = 2
    }

    /// <summary>
    /// TimeSheet类型
    /// </summary>
    public enum TimeSheetType
    {
        /// <summary>
        /// TimeSheet
        /// </summary>
        TimeSheet = 1,
        /// <summary>
        /// 长假
        /// </summary>
        Holiday = 2,
        /// <summary>
        /// 迟到
        /// </summary>
        Late = 10,
        /// <summary>
        /// 早退
        /// </summary>
        LeaveEarly = 11,
        /// <summary>
        /// 旷工
        /// </summary>
        StayAway = 12

    }

    /// <summary>
    /// 门卡信息状态
    /// </summary>
    public enum CardNoState
    {
        /// <summary>
        /// 所有状态
        /// </summary>
        AllState = 0,
        /// <summary>
        /// 启用状态
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 停用状态
        /// </summary>
        UnEnable = 2,
    }

    /// <summary>
    /// 考勤类型（正常，弹性，特殊）
    /// </summary>
    public enum AttendanceType
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 弹性工作制
        /// </summary>
        Flexitime = 2,
        /// <summary>
        /// 特殊，如总监不计算考勤
        /// </summary>
        Special = 3,
    }

    /// <summary>
    /// 年假基数
    /// </summary>
    public enum AnnualLeaveBase
    {
        /// <summary>
        /// 普通员工
        /// </summary>
        Normal = 7,
        /// <summary>
        /// SAE以上员工
        /// </summary>
        Special = 10,
    }

    /// <summary>
    /// 门卡使用状态
    /// </summary>
    public enum CardUseState
    {
        /// <summary>
        /// 启用状态
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 停用状态
        /// </summary>
        UnEnable = 2,
    }

    /// <summary>
    /// 门卡信息库中门卡使用状态，0未使用,1已使用,2作废
    /// </summary>
    public enum CardStoreState
    {
        /// <summary>
        /// 未使用
        /// </summary>
        NotUsed = 0,
        /// <summary>
        /// 已使用
        /// </summary>
        Used = 1,
        /// <summary>
        /// 作废
        /// </summary>
        BlankOut = 2,
    }

    /// <summary>
    /// 任务处理状态
    /// </summary>
    public enum TaskType
    {
        /// <summary>
        /// 启用门卡
        /// </summary>
        EnableCard = 1,
        /// <summary>
        /// 停用门卡
        /// </summary>
        UnEnableCard = 2,
    }

    /// <summary>
    /// 地区编号，也就是个分公司编号
    /// </summary>
    public enum AreaID
    {
        /// <summary>
        /// 总部
        /// </summary>
        HeadOffic = 19,
        /// <summary>
        /// 重庆
        /// </summary>
        ChongqingOffic = 230
    }

    /// <summary>
    /// 年假、福利假类型
    /// </summary>
    public enum AAndRLeaveType
    {
        /// <summary>
        /// 年假类型
        /// </summary>
        AnnualType = 1,
        /// <summary>
        /// 福利假类型
        /// </summary>
        RewardType = 2,
    }

    /// <summary>
    /// IT租赁类型
    /// </summary>
    public enum RefundType : int
    {
        /// <summary>
        /// 笔记本租赁
        /// </summary>
        NetBookType = 1,
        PublicAsset = 2,
    }

    /// <summary>
    /// IT租赁状态
    /// </summary>
    public enum RefundStatus : int
    {
        /// <summary>
        /// 未启动状态
        /// </summary>
        UnEnableStatus = 0,
        /// <summary>
        /// 开始状态
        /// </summary>
        BeginStatus = 1,
        /// <summary>
        /// 结束状态
        /// </summary>
        EndStatus = 2,
    }

    /// <summary>
    /// 节假日类型
    /// </summary>
    public enum HolidayType
    {
        /// <summary>
        /// 公休假
        /// </summary>
        SabbaticalLeave = 1,
        /// <summary>
        /// 法定节假日
        /// </summary>
        LegalHoliday = 2,
        /// <summary>
        /// 半天假
        /// </summary>
        halfHoliday = 3,
    }

    /// <summary>
    /// 考勤提交方式（正常提交、离职提交）
    /// </summary>
    public enum AttendanceSubType
    {
        /// <summary>
        /// 正常提交
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 离职提交
        /// </summary>
        Dimission = 2,
    }

    /// <summary>
    /// 休假记录类型
    /// </summary>
    public enum OverWorkRecords_Types
    {
        /// <summary>
        /// 调休
        /// </summary>
        OffTune = 1,
        /// <summary>
        /// 年假
        /// </summary>
        AnnualLeave = 2,
        /// <summary>
        /// 奖励假
        /// </summary>
        Incentive = 3,
    }

    /// <summary>
    /// TimeSheet类别ID
    /// </summary>
    public enum TimeSheetCategoryIds
    {
        /// <summary>
        /// 迟到
        /// </summary>
        LateId = 4,
        /// <summary>
        /// 早退
        /// </summary>
        LeaveEarlyId = 5,
        /// <summary>
        /// 病假
        /// </summary>
        SickLeaveId = 16,
        /// <summary>
        /// 事假
        /// </summary>
        AffairLeaveId = 17,
        /// <summary>
        /// 年假
        /// </summary>
        AnnualLeaveId = 7,
        /// <summary>
        /// 产假
        /// </summary>
        MaternityLeaveId = 12,
        /// <summary>
        /// 婚假
        /// </summary>
        MarriageLeaveId = 13,
        /// <summary>
        /// 丧假
        /// </summary>
        FuneralLeaveId = 14,
        /// <summary>
        /// 旷工
        /// </summary>
        AbsentId = 6,
        /// <summary>
        /// OT
        /// </summary>
        OvertimesId = 10,
        /// <summary>
        /// 出差
        /// </summary>
        EvectionsId = 15,
        /// <summary>
        /// 外出
        /// </summary>
        EgressId = 9,
        /// <summary>
        /// 调休
        /// </summary>
        OffTuneId = 11,
        /// <summary>
        /// 产检
        /// </summary>
        PrenatalCheckId = 18,
        /// <summary>
        /// 奖励假
        /// </summary>
        IncentiveId = 8,
        /// <summary>
        /// 行政 （假期类Parent)
        /// </summary>
        AdministrativeId = 25,
        /// <summary>
        /// 晚到申请
        /// </summary>
        LateExclude=78,
    }
}