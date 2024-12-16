using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Framework.Entity;
using System.Collections;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;

namespace Portal.WebSite
{
    public partial class AttendanceSelect : ESP.Web.UI.PageBase
    {
        protected string UserIcon
        {
            get
            {
                if (UserID > 0)
                {
                    ESP.Framework.Entity.EmployeeInfo info = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                    if (info != null && info.Photo.Trim().Length > 0)
                    {
                        return Portal.Common.Global.USER_ICON_FOLDER + info.Photo;
                    }
                }
                return "";
            }
        }


        private void getUserInfo()
        {
            string script = string.Empty;
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(UserID);
            IList<string> depts = emp.GetDepartmentNames();
            string deptstr = string.Empty;
            if (depts != null && depts.Count != 0)
            {
                for (int i = 0; i < depts.Count; i++)
                {
                    deptstr += depts[i].ToString() + ",";
                }
            }
            string empname = emp.Name == string.Empty ? "&nbsp;" : emp.Name;
            string empITCode = emp.ITCode == string.Empty ? "&nbsp;" : emp.ITCode;
            string empMobile = emp.Mobile == string.Empty ? "&nbsp;" : emp.Mobile;
            string empEmail = emp.EMail == string.Empty ? "&nbsp;" : emp.EMail;
            string empTel = emp.Telephone == string.Empty ? "&nbsp;" : emp.Telephone;
            string strdept = deptstr.TrimEnd(',') == string.Empty ? "&nbsp;" : deptstr.TrimEnd(',');

            script += "<span class=\"title_red\">" + empname + "</span><br />";
            script += "<span class=\"text_red\">员工账号：" + empITCode + "</span><br />";
            script += "员工编号：" + emp.ID + "<br />";
            script += "所属部门：" + strdept + "<br />";

            labUserInfo.Text = script;

        }



        private void getAssetInfo()
        {
            string script = "<font style='color:orange;'>●</font>&nbsp;&nbsp;";
            string assetString = string.Empty;
            string terms = " usercode=@usercode and status=@status";
            List<SqlParameter> parms = new List<SqlParameter>();
            int icount = 0;
            parms.Add(new SqlParameter("@usercode", CurrentUser.ID));
            parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Received));

            IList<ESP.Finance.Entity.ITAssetReceivingInfo> list = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(terms, parms);
            if (list != null && list.Count > 0)
            {
                foreach (ESP.Finance.Entity.ITAssetReceivingInfo rec in list)
                {
                    if (icount >= 2)
                        break;
                    ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(rec.AssetId);
                    if (model != null)
                    {
                        assetString += script + model.AssetName + "&nbsp;&nbsp;" + rec.ReceiveDate.ToString("yyyy-MM-dd") + "<br/>";
                        icount++;
                    }
                }
                if (list.Count > 2)
                    assetString += "<font style='color:orange;'>●</font>&nbsp;&nbsp; <span onclick=\"ShowAssetMsg(" + CurrentUserID + ")\" onmouseover=\"this.style.cursor='pointer',this.style.color='#f97b02'\" onmouseout=\"this.style.cursor='auto',this.style.color='#666666'\" >更多(" + list.Count + ")...</span>";
            }

            this.lblAsset.Text = assetString;

        }

        #region 变量定义
        // 考勤业务对象类
        private ESP.Administrative.BusinessLogic.AttendanceManager attMan =
            new ESP.Administrative.BusinessLogic.AttendanceManager();
        /// <summary>
        /// 每月考勤业务对象类
        /// </summary>
        private MonthStatManager monthManager = new MonthStatManager();
        /// <summary>
        /// 请假单业务对象
        /// </summary>
        private LeaveManager leaveManager = new LeaveManager();
        /// <summary>
        /// OT单业务对象
        /// </summary>
        private SingleOvertimeManager singleOverTime = new SingleOvertimeManager();
        /// <summary>
        /// 考勤时间记录信息业务对象
        /// </summary>
        private ClockInManager clockInManager = new ClockInManager();
        /// <summary>
        /// 用户考勤基本信息业务对象
        /// </summary>
        private UserAttBasicInfoManager userBasicManager = new UserAttBasicInfoManager();
        /// <summary>
        /// 上下班时间业务对象
        /// </summary>
        private CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
        /// <summary>
        /// 用户提示信息
        /// </summary>
        private string Tips = "";
        /// <summary>
        /// 事由业务对象
        /// </summary>
        private MattersManager mattersManager = new MattersManager();
        /// <summary>
        /// 上下班时间集合
        /// </summary>
        Hashtable clockInTimes = null;
        /// <summary>
        /// 考勤事由内容信息
        /// </summary>
        IList<MattersInfo> matters = null;
        /// <summary>
        /// OT信息集合
        /// </summary>
        IList<SingleOvertimeInfo> overtimes = null;
        /// <summary>
        /// 当月节假日信息集合
        /// </summary>
        HashSet<int> holidays = null;
        /// <summary>
        /// 当前用户考勤基本信息
        /// </summary>
        UserAttBasicInfo userBasicModel = null;
        /// <summary>
        /// 员工入职信息
        /// </summary>
        EmployeeJobInfo employeeJobModel = null;
        /// <summary>
        /// 月考勤统计信息
        /// </summary>
        AttendanceDataInfo attdatainfo = null;
        /// <summary>
        /// 员工离职信息
        /// </summary>
        ESP.HumanResource.Entity.DimissionInfo dimissionModel = null;
        /// <summary>
        /// 上下班时间信息集合
        /// </summary>
        List<CommuterTimeInfo> commuterTimeList = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int currentuserdept = CurrentUser.GetDepartmentIDs()[0];
                // string CalenderVisible = System.Configuration.ConfigurationManager.AppSettings["CalenderVisible"];
                //if (CalenderVisible.IndexOf("," + currentuserdept + ",") >= 0)
                //{
                //    cldAttendance.MinDate = new DateTime(2013, 1, 1);
                //}
                // 默认选择当前日期
                cldAttendance.SelectedDate = DateTime.Now;
                cldAttendance.MaxDate = DateTime.Now.AddYears(1);  // 设置日历控件的最大日期为当前时间加一年
                getUserInfo();
                getAssetInfo();
                imgUserIcon.Src = UserIcon;

                int year = cldAttendance.SelectedDate.Year;
                int month = cldAttendance.SelectedDate.Month;
                BindCalender(year, month);

                // 设置各月考勤的状态标题
                SetTitleString();
                SetMonthStat();
            }
        }

        /// <summary>
        /// 绑定考勤时间事由信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        public void BindCalender(int year, int month)
        {
            clockInTimes = clockInManager.GetClockInTimesOfMonth(year, month, this.UserID);
            holidays = new HolidaysInfoManager().GetHolidayListByMonth(year, month);
            matters = mattersManager.GetModelListByMonth(this.UserID, year, month);
            overtimes = new SingleOvertimeManager().GetModelListByMonth(this.UserID, year, month);
            userBasicModel = userBasicManager.GetModelByUserid(UserID);
            employeeJobModel = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(UserID);
            dimissionModel = ESP.HumanResource.BusinessLogic.DimissionManager.GetModelByUserID(UserID);
            commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserID);

            int days = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= days; i++)
            {
                ComponentArt.Web.UI.CalendarDay day = new ComponentArt.Web.UI.CalendarDay();
                day.Date = new DateTime(year, month, i);
                day.TemplateId = "DefaultTemplate";
                if (holidays.Contains(i))
                    day.CssClass = "holidayday";
                else
                    day.CssClass = "day";
                cldAttendance.CustomDays.Add(day);
            }
        }

        /// <summary>
        /// 绑定考勤日内容
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetDay(ComponentArt.Web.UI.CalendarDay day)
        {
            return day.Date.Day.ToString();
        }

        /// <summary>
        /// 绑定上班时间
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetClockIn(ComponentArt.Web.UI.CalendarDay day)
        {
            StringBuilder builder = new StringBuilder();
            if (CurrentUserID != int.Parse(System.Configuration.ConfigurationManager.AppSettings["DavidZhangID"]))
            {
                if (employeeJobModel != null && employeeJobModel.joinDate.Date <= day.Date)
                {
                    object obj = clockInTimes[(long)day.Date.Day];
                    if (obj == null)
                        return "";
                    string color = Status.normal;
                    ArrayList list = (ArrayList)obj;
                    if (list != null && list.Count > 0)
                    {
                        DateTime clockIn = ((DateTime)list[0]);
                        string titleStr = "";
                        if (holidays.Contains(day.Date.Day))
                        {
                            color = Status.weekend;
                        }
                        else
                        {
                            CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, day.Date);
                            if (clockIn.Date == day.Date
                                && clockIn.TimeOfDay >= commuterTimeModel.GoWorkTime.TimeOfDay.Add(new TimeSpan(0, Status.GoWorkTime_BufferMinute, 0)))
                            {
                                color = Status.improper;
                            }
                        }
                        if (list.Count > 1 && list[1] != null && !string.IsNullOrEmpty(list[1].ToString()))
                        {
                            color = Status.operatorTime;
                            if (list.Count > 2 && list[2] != null && !string.IsNullOrEmpty(list[2].ToString()))
                            {
                                titleStr = list[1].ToString() + "录入，备注：" + list[2].ToString();
                            }
                        }
                        builder.Append("<span style=\"color:").Append(color).Append("\" title='" + titleStr + "'>").Append(clockIn.ToString("HH:mm")).Append("</span>");
                    }
                }
            }
            return builder.ToString();
        }


        /// <summary>
        /// 绑定下班时间，如果是当天则不显示下班时间
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetClockOut(ComponentArt.Web.UI.CalendarDay day)
        {
            if (CurrentUserID != int.Parse(System.Configuration.ConfigurationManager.AppSettings["DavidZhangID"]))
            {
                CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, day.Date);
                if (employeeJobModel != null && employeeJobModel.joinDate.Date <= day.Date)
                {
                    // 如果绑定的是当前日期的考勤信息，不显示下班时间
                    if (day.Date == DateTime.Now.Date)
                    {
                        return "";
                    }
                    else
                    {
                        StringBuilder builder = new StringBuilder();
                        object obj = clockInTimes[(long)-day.Date.Day];
                        if (obj == null)
                            return "";

                        string color = Status.normal;
                        ArrayList list = (ArrayList)obj;
                        if (list != null && list.Count > 0)
                        {
                            DateTime clockOut = ((DateTime)list[0]);
                            string titleStr = "";
                            if (holidays.Contains(day.Date.Day))
                            {
                                color = Status.weekend;
                            }
                            else
                            {
                                // 判断时间是否正常，如果不正常则显示相对应的颜色
                                if (clockOut.Date == day.Date &&
                                    clockOut.TimeOfDay < commuterTimeModel.OffWorkTime.TimeOfDay)
                                {
                                    color = Status.improper;
                                }
                                // 计算工作日OT超过几点后有效
                                //TimeSpan span = commuterTimeModel.OffWorkTime.TimeOfDay.Add(new TimeSpan(Status.WorkingDays_OverTime1, 0, 0));
                                TimeSpan span = commuterTimeModel.OffWorkTime.AddHours(commuterTimeModel.WorkingDays_OverTime1).TimeOfDay;
                                if (clockOut.Date > day.Date
                                    || (clockOut.Date == day.Date
                                    && clockOut.TimeOfDay > span))
                                {
                                    color = Status.overNine;
                                }
                            }
                            if (list.Count > 1 && list[1] != null && !string.IsNullOrEmpty(list[1].ToString()))
                            {
                                color = Status.operatorTime;
                                if (list.Count > 2 && list[2] != null && !string.IsNullOrEmpty(list[2].ToString()))
                                {
                                    titleStr = list[1].ToString() + "录入，备注：" + list[2].ToString();
                                }
                            }
                            builder.Append("<span style=\"color:").Append(color).Append("\" title='" + titleStr + "'>").Append(clockOut.ToString("HH:mm")).Append("</span>");
                            return builder.ToString();
                        }
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 判断显示考勤统计图表内容
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetIconsHtml(ComponentArt.Web.UI.CalendarDay day)
        {
            CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, day.Date);
            StringBuilder builder = new StringBuilder();
            DateTime today = day.Date;
            DateTime tomorrow = today.AddDays(1);
            if (employeeJobModel != null && employeeJobModel.joinDate.Date <= today
                && (dimissionModel == null || (dimissionModel != null && today <= dimissionModel.dimissionDate.Date)))
            {
                ArrayList clockinList = (ArrayList)clockInTimes[(long)day.Date.Day];
                ArrayList clockoutList = (ArrayList)clockInTimes[(long)-day.Date.Day];
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
                if (commuterTimeModel.AttendanceType == Status.UserBasicAttendanceType_Normal)
                {
                    #region 判断显示当天的OT信息
                    //foreach (SingleOvertimeInfo info in overtimes)
                    //{
                    //    if ((info.BeginTime >= today && info.BeginTime < tomorrow))
                    //    {
                    //        int imgType = 0;
                    //        switch (info.Approvestate)
                    //        {
                    //            case Status.OverTimeState_NotSubmit:
                    //            case Status.OverTimeState_Cancel:
                    //            case Status.OverTimeState_Overrule:
                    //                imgType = 1;
                    //                break;
                    //            case Status.OverTimeState_Passed:
                    //                imgType = 3;
                    //                break;
                    //            case Status.OverTimeState_WaitDirector:
                    //            case Status.OverTimeState_WaitHR:
                    //                imgType = 2;
                    //                break;
                    //            default:
                    //                imgType = 1;
                    //                break;
                    //        }

                    //        if (imgType == 1)
                    //        {
                    //            string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                    //            string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, 1);
                    //            builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                    //        }
                    //        string imgUrl = "/images/calendar/" + imgType + "jiaban.jpg";
                    //        string title;
                    //        switch (imgType)
                    //        {
                    //            case 2:
                    //                title = "OT 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 3:
                    //                title = "OT 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 1:
                    //            default:
                    //                title = "OT 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //        }

                    //        builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                    //        if (imgType == 1)
                    //        {
                    //            builder.Append("</a>");
                    //        }
                    //    }
                    //}
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
                    attMan.CalDefaultMatters(UserID, today, clockIn, clockOut,
                        out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterTimeModel);
                    long abnormityTicks = abnormityTime.Ticks;
                    #endregion

                    #region 计算前一天的OT信息，判断是否去除当天的迟到和矿工
                    // 获得前一天的OT单信息，并且是审批通过的OT单
                    //List<SingleOvertimeInfo> beforeDaySingleList = singleOverTime.GetSingleOvertimeList(UserID, today.AddDays(-1), true);
                    //if (beforeDaySingleList != null && beforeDaySingleList.Count > 0)
                    //{
                    //    foreach (SingleOvertimeInfo single in beforeDaySingleList)
                    //    {
                    //        if (single != null)
                    //        {
                    //            HolidaysInfo holidayModel = new HolidaysInfoManager().GetHolideysInfoByDatetime(today.AddDays(-1));
                    //            // 判断用户的OT开始时间是否大于用户的下班时间
                    //            if (single.BeginTime >= single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)
                    //                || ((holidays.Contains(today.AddDays(-1).Day) || holidayModel != null)
                    //                    && single.EndTime > single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)))
                    //            {
                    //                decimal overTimeHours = 0;
                    //                if (!holidays.Contains(today.AddDays(-1).Day) && holidayModel == null)
                    //                {
                    //                    overTimeHours = single.OverTimeHours;
                    //                }
                    //                else
                    //                {
                    //                    overTimeHours = (decimal)(single.EndTime - single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)).TotalHours;
                    //                }
                    //                // 用户OT小时数大于6小时
                    //                if (overTimeHours >= (decimal)commuterTimeModel.WorkingDays_OverTime2)
                    //                {
                    //                    TimeSpan tempSpan = new TimeSpan(Status.AMWorkingHours, 0, 0);
                    //                    abnormityTicks -= tempSpan.Ticks;
                    //                    if (isAMAbsent)
                    //                    {
                    //                        isAMAbsent = false;
                    //                    }
                    //                    else if (isLate)
                    //                    {
                    //                        isLate = false;
                    //                    }
                    //                    else if (isPMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }

                    //                    }
                    //                }
                    //                else if (overTimeHours >= (decimal)commuterTimeModel.WorkingDays_OverTime1 && overTimeHours < (decimal)commuterTimeModel.WorkingDays_OverTime2)
                    //                {
                    //                    //TimeSpan tempSpan = new TimeSpan(Status.LateGoWorkTime_OverTime1, 0, 0);
                    //                    DateTime tempSpan = new DateTime().AddHours(commuterTimeModel.LateGoWorkTime_OverTime1);
                    //                    abnormityTicks -= tempSpan.Ticks;
                    //                    if (isLate)
                    //                    {
                    //                        isLate = false;
                    //                    }
                    //                    else if (isAMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.NumberOfHours_Late);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (span.TotalHours < Status.NumberOfHours_Late)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                    else if (isPMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (span.TotalHours < Status.NumberOfHours_Late)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 判断显示当天的考勤事由信息
                    foreach (MattersInfo info in matters)
                    {
                        if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                            || (info.EndTime > today && info.EndTime <= tomorrow)
                            || (info.BeginTime <= today && info.EndTime >= tomorrow))
                        {
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterTimeModel, clockIn, clockOut, today, ref abnormityTicks);
                            int imgType = 0;
                            switch (info.MatterState)
                            {
                                case 1:
                                case 5:
                                case 6:
                                    imgType = 1;
                                    break;
                                case 2:
                                    imgType = 3;
                                    break;
                                case 3:
                                case 4:
                                    imgType = 2;
                                    break;
                                default:
                                    imgType = 1;
                                    break;
                            }

                            string matterType;
                            switch (info.MatterType)
                            {
                                case 1:
                                    matterType = "病假";
                                    break;

                                case 2:
                                    matterType = "事假";
                                    break;

                                case 3:
                                    matterType = "年假";
                                    break;

                                case 4:
                                    matterType = "婚假";
                                    break;

                                case 5:
                                    matterType = "产假";
                                    break;

                                case 6:
                                    matterType = "丧假";
                                    break;

                                case 7:
                                    matterType = "出差";
                                    break;

                                case 8:
                                    matterType = "外出";
                                    break;

                                case 9:
                                    matterType = "调休";
                                    break;
                                case 10:
                                    matterType = "其它";
                                    break;
                                case 11:
                                    matterType = "产检";
                                    break;
                                   case 13:
                                    matterType = "晚到申请";
                                    break;
                                   case 14:
                                    matterType = "陪产假";
                                    break;
                                   case 15:
                                    matterType = "年假(补)";
                                    break;
                                default:
                                    continue;
                            }

                            string imagename;
                            int t = 0;
                            imagename = "qingjia";

                            if (info.MatterType == 8)
                            {
                                t = 2;
                                imagename = "waichu";
                            }
                            if (info.MatterType == 7)
                            {
                                t = 3;
                                imagename = "chuchai";
                            }
                            if (info.MatterType == 9 || info.MatterType == 13)
                            {
                                t = 4;
                                imagename = "tiaoxiu";
                            }
                            if (info.MatterType == 10)
                            {
                                t = 5;
                                imagename = "qita";
                            }

                            if (!holidays.Contains(today.Day))
                            {
                                if (imgType == 1)
                                {
                                    string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                                    string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, t);
                                    builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                    builder.Append("</a>");
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7 || info.MatterType == 14)
                            {
                                if (imgType == 1)
                                {
                                    string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                                    string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, t);
                                    builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                    builder.Append("</a>");
                                }
                            }
                        }
                    }
                    #endregion

                    #region 判断用户是否是第一天入职
                    if (employeeJobModel.joinDate.Date == today && clockOut.ToString("yyyy-MM-dd") != Status.EmptyTime)
                    {
                        isLate = false;
                        isAMAbsent = false;
                        isPMAbsent = false;
                        abnormityTicks = 0;
                    }
                    #endregion

                    #region 判断显示考勤默认事由信息
                    TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                    if (!holidays.Contains(today.Day))
                    {
                        if (isLate && remainTime.TotalMinutes >= Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            string title = "迟到";
                            string imgUrl = "/images/calendar/3chidao.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                            if (isLeaveEarly)
                            {
                                string title2 = "早退";
                                string imgUrl2 = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");

                            }
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Late && remainTime.TotalHours <= Status.NumberOfHours_Absent)
                        {
                            if (isLate && isLeaveEarly)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                string title2 = "早退";
                                string imgUrl2 = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");
                            }
                            else if (isLate)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                            else if (isLeaveEarly)
                            {
                                string title2 = "早退";
                                string imgUrl2 = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");
                            }
                            else
                            {
                                string title = "旷工半天";
                                string imgUrl = "/images/calendar/3kuanggong.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Absent)
                        {
                            if (isLate)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                string title2 = "旷工半天";
                                string imgUrl2 = "/images/calendar/3kuanggong.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");
                            }
                            else if (isLeaveEarly)
                            {
                                string title2 = "早退";
                                string imgUrl2 = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");

                                string title = "旷工半天";
                                string imgUrl = "/images/calendar/3kuanggong.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                            else
                            {
                                string title = "旷工一天";
                                string imgUrl = "/images/calendar/3kuanggong.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                        }
                        else if (isLeaveEarly && remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            string title = "早退";
                            string imgUrl = "/images/calendar/3zaotui.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            if (clockIn > today.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay) && (clockIn - today.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay)).TotalMinutes >= Status.GoWorkTime_BufferMinute)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                            else if (clockOut < today.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay) && today.Date < DateTime.Now.Date && (clockOut - today.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)).TotalMinutes >= 1)
                            {
                                string title = "早退";
                                string imgUrl = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                        }

                        //if (isLate)
                        //{
                        //    string title = "迟到";
                        //    string imgUrl = "/images/calendar/3chidao.jpg";
                        //    builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        //}
                        //else if (isAMAbsent)
                        //{
                        //    string title = "旷工半天";
                        //    string imgUrl = "/images/calendar/3kuanggong.jpg";
                        //    builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        //}
                        //else if (isPMAbsent)
                        //{
                        //    string title = "旷工一天";
                        //    string imgUrl = "/images/calendar/3kuanggong.jpg";
                        //    builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        //}
                        //if (isLeaveEarly)
                        //{
                        //    string title = "早退";
                        //    string imgUrl = "/images/calendar/3zaotui.jpg";
                        //    builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        //}
                    }
                    #endregion
                }
                #endregion
                else if (commuterTimeModel.AttendanceType == Status.UserBasicAttendanceType_Special)
                {
                    #region 判断显示当天的OT信息
                    //foreach (SingleOvertimeInfo info in overtimes)
                    //{
                    //    if ((info.BeginTime >= today && info.BeginTime < tomorrow))
                    //    {
                    //        int imgType = 0;
                    //        switch (info.Approvestate)
                    //        {
                    //            case Status.OverTimeState_NotSubmit:
                    //            case Status.OverTimeState_Cancel:
                    //            case Status.OverTimeState_Overrule:
                    //                imgType = 1;
                    //                break;
                    //            case Status.OverTimeState_Passed:
                    //                imgType = 3;
                    //                break;
                    //            case Status.OverTimeState_WaitDirector:
                    //            case Status.OverTimeState_WaitHR:
                    //                imgType = 2;
                    //                break;
                    //            default:
                    //                imgType = 1;
                    //                break;
                    //        }

                    //        if (imgType == 1)
                    //        {
                    //            string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                    //            string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, 1);
                    //            builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                    //        }
                    //        string imgUrl = "/images/calendar/" + imgType + "jiaban.jpg";
                    //        string title;
                    //        switch (imgType)
                    //        {
                    //            case 2:
                    //                title = "OT 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 3:
                    //                title = "OT 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 1:
                    //            default:
                    //                title = "OT 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //        }

                    //        builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                    //        if (imgType == 1)
                    //        {
                    //            builder.Append("</a>");
                    //        }
                    //    }
                    //}
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

                    if (clockIn >= new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, commuterTimeModel.OffWorkTime.Hour, commuterTimeModel.OffWorkTime.Minute, commuterTimeModel.OffWorkTime.Second))
                    {
                        clockIn = DateTime.Parse(Status.EmptyTime);
                        clockOut = DateTime.Parse(Status.EmptyTime);
                    }


                    if (clockIn.ToString("yyyy-MM-dd") == Status.EmptyTime || clockOut.ToString("yyyy-MM-dd") == Status.EmptyTime || clockIn == clockOut)
                    {
                        if (day.Date < DateTime.Now.Date)
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

                            span = span.Add(new TimeSpan(hours, minutes, seconds));
                        }
                    }
                    else
                    {
                        if (day.Date < DateTime.Now.Date)
                        {
                            TimeSpan workingHours = commuterTimeModel.OffWorkTime - commuterTimeModel.GoWorkTime;
                            int hours = workingHours.Hours;
                            int minutes = workingHours.Minutes;
                            int seconds = workingHours.Seconds;
                            DateTime currentOffTime = new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, commuterTimeModel.OffWorkTime.Hour, commuterTimeModel.OffWorkTime.Minute, commuterTimeModel.OffWorkTime.Second);
                            DateTime currentGoTime = new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, commuterTimeModel.GoWorkTime.Hour, commuterTimeModel.GoWorkTime.Minute, commuterTimeModel.GoWorkTime.Second);
                           

                            if (clockIn > currentGoTime)
                                currentGoTime = clockIn;

                            if (clockOut < currentOffTime)
                            {
                                currentOffTime = clockOut;
                            }

                            TimeSpan workingHours2 = currentOffTime - currentGoTime;


                            int hours2 = workingHours2.Hours;
                            int minutes2 = workingHours2.Minutes;
                            int seconds2 = workingHours2.Seconds;
                            span = span.Add(new TimeSpan(hours - hours2, minutes - minutes2, seconds - seconds2));
                        }
                    }

                    TimeSpan abnormityTime = new TimeSpan(0);

                    attMan.CalDefaultMatters(UserID, today, clockIn, clockOut,
                       out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterTimeModel);

                    long abnormityTicks = span.Ticks;
                    #endregion

                    #region 计算前一天的OT信息，判断是否去除当天的迟到和矿工
                    // 获得前一天的OT单信息，并且是审批通过的OT单
                    //  List<SingleOvertimeInfo> beforeDaySingleList = singleOverTime.GetSingleOvertimeList(UserID, today.AddDays(-1), true);
                    // if (beforeDaySingleList != null && beforeDaySingleList.Count > 0)
                    //{
                    //    foreach (SingleOvertimeInfo single in beforeDaySingleList)
                    //    {
                    //        if (single != null)
                    //        {
                    //            // 判断用户的OT开始时间是否大于用户的下班时间
                    //            if (single.BeginTime >= single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)
                    //                || ((holidays.Contains(today.AddDays(-1).Day) || new HolidaysInfoManager().GetHolideysInfoByDatetime(today.AddDays(-1)) != null)
                    //                    && single.EndTime > single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)))
                    //            {
                    //                decimal overTimeHours = 0;
                    //                if (!holidays.Contains(today.AddDays(-1).Day))
                    //                {
                    //                    overTimeHours = single.OverTimeHours;
                    //                }
                    //                else
                    //                {
                    //                    overTimeHours = (decimal)(single.EndTime - single.BeginTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay)).TotalHours;
                    //                }
                    //                // 用户OT小时数大于6小时
                    //                if (overTimeHours >= (decimal)commuterTimeModel.WorkingDays_OverTime2)
                    //                {
                    //                    TimeSpan tempSpan = new TimeSpan(Status.AMWorkingHours, 0, 0);
                    //                    abnormityTicks -= tempSpan.Ticks;
                    //                    if (isAMAbsent)
                    //                    {
                    //                        isAMAbsent = false;
                    //                    }
                    //                    else if (isLate)
                    //                    {
                    //                        isLate = false;
                    //                    }
                    //                    else if (isPMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (TimeSpan.FromTicks(abnormityTicks).TotalMinutes > 0)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                }
                    //                else if (overTimeHours >= (decimal)commuterTimeModel.WorkingDays_OverTime1 && overTimeHours < (decimal)commuterTimeModel.WorkingDays_OverTime2)
                    //                {
                    //                    //TimeSpan tempSpan = new TimeSpan(Status.LateGoWorkTime_OverTime1, 0, 0);
                    //                    DateTime tempSpan = new DateTime().AddHours(commuterTimeModel.LateGoWorkTime_OverTime1);
                    //                    abnormityTicks -= tempSpan.Ticks;
                    //                    if (isLate)
                    //                    {
                    //                        isLate = false;
                    //                    }
                    //                    else if (isAMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.NumberOfHours_Late);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (span.TotalHours < Status.NumberOfHours_Late)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                    else if (isPMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (span.TotalHours < Status.NumberOfHours_Late)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 判断显示当天的考勤事由信息
                    foreach (MattersInfo info in matters)
                    {
                        if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                            || (info.EndTime > today && info.EndTime <= tomorrow)
                            || (info.BeginTime <= today && info.EndTime >= tomorrow))
                        {
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterTimeModel, clockIn, clockOut, today, ref abnormityTicks);
                            int imgType = 0;
                            switch (info.MatterState)
                            {
                                case 1:
                                case 5:
                                case 6:
                                    imgType = 1;
                                    break;
                                case 2:
                                    imgType = 3;
                                    break;
                                case 3:
                                case 4:
                                    imgType = 2;
                                    break;
                                default:
                                    imgType = 1;
                                    break;
                            }

                            string matterType;
                            switch (info.MatterType)
                            {
                                case 1:
                                    matterType = "病假";
                                    break;

                                case 2:
                                    matterType = "事假";
                                    break;

                                case 3:
                                    matterType = "年假";
                                    break;

                                case 4:
                                    matterType = "婚假";
                                    break;

                                case 5:
                                    matterType = "产假";
                                    break;

                                case 6:
                                    matterType = "丧假";
                                    break;

                                case 7:
                                    matterType = "出差";
                                    break;

                                case 8:
                                    matterType = "外出";
                                    break;

                                case 9:
                                    matterType = "调休";
                                    break;
                                case 10:
                                    matterType = "其它";
                                    break;
                                case 11:
                                    matterType = "产检";
                                    break;
                                case 13:
                                    matterType = "晚到申请";
                                    break;
                                case 14:
                                    matterType = "陪产假";
                                    break;
                                case 15:
                                    matterType = "年假(补)";
                                    break;
                                default:
                                    continue;
                            }

                            string imagename;
                            int t = 0;
                            imagename = "qingjia";

                            if (info.MatterType == 8)
                            {
                                t = 2;
                                imagename = "waichu";
                            }
                            if (info.MatterType == 7)
                            {
                                t = 3;
                                imagename = "chuchai";
                            }
                            if (info.MatterType == 9 || info.MatterType == 13)
                            {
                                t = 4;
                                imagename = "tiaoxiu";
                            }
                            if (info.MatterType == 10)
                            {
                                t = 5;
                                imagename = "qita";
                            }

                            if (!holidays.Contains(today.Day))
                            {
                                if (imgType == 1)
                                {
                                    string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                                    string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, t);
                                    builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                    builder.Append("</a>");
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7 || info.MatterType == 14)
                            {
                                if (imgType == 1)
                                {
                                    string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                                    string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, t);
                                    builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                    builder.Append("</a>");
                                }
                            }
                        }
                    }
                    #endregion

                    #region 判断考勤默认事由情况
                    TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                    if (!holidays.Contains(today.Day))
                    {
                        if (isLate && remainTime.TotalMinutes >= Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            string title = "迟到";
                            string imgUrl = "/images/calendar/3chidao.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Late && remainTime.TotalHours <= Status.NumberOfHours_Absent)
                        {
                            string title = "旷工半天";
                            string imgUrl = "/images/calendar/3kuanggong.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Absent)
                        {
                            string title = "旷工一天";
                            string imgUrl = "/images/calendar/3kuanggong.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (isLeaveEarly && remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            string title = "早退";
                            string imgUrl = "/images/calendar/3zaotui.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                    }
                    #endregion
                }
                else if (commuterTimeModel.AttendanceType == Status.UserBasicAttendanceType_NotStat)
                {
                    #region 判断显示当天的OT信息
                    //foreach (SingleOvertimeInfo info in overtimes)
                    //{
                    //    if ((info.BeginTime >= today && info.BeginTime < tomorrow))
                    //    {
                    //        int imgType = 0;
                    //        switch (info.Approvestate)
                    //        {
                    //            case Status.OverTimeState_NotSubmit:
                    //            case Status.OverTimeState_Cancel:
                    //            case Status.OverTimeState_Overrule:
                    //                imgType = 1;
                    //                break;
                    //            case Status.OverTimeState_Passed:
                    //                imgType = 3;
                    //                break;
                    //            case Status.OverTimeState_WaitDirector:
                    //            case Status.OverTimeState_WaitHR:
                    //                imgType = 2;
                    //                break;
                    //            default:
                    //                imgType = 1;
                    //                break;
                    //        }

                    //        if (imgType == 1)
                    //        {
                    //            string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                    //            string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, 1);
                    //            builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                    //        }
                    //        string imgUrl = "/images/calendar/" + imgType + "jiaban.jpg";
                    //        string title;
                    //        switch (imgType)
                    //        {
                    //            case 2:
                    //                title = "OT 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 3:
                    //                title = "OT 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 1:
                    //            default:
                    //                title = "OT 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //        }

                    //        builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                    //        if (imgType == 1)
                    //        {
                    //            builder.Append("</a>");
                    //        }
                    //    }
                    //}
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
                    attMan.CalDefaultMatters(UserID, today, clockIn, clockOut,
                        out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterTimeModel);
                    long abnormityTicks = abnormityTime.Ticks;
                    #endregion

                    #region 判断显示当天的考勤事由信息
                    foreach (MattersInfo info in matters)
                    {
                        if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                            || (info.EndTime > today && info.EndTime <= tomorrow)
                            || (info.BeginTime <= today && info.EndTime >= tomorrow))
                        {
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterTimeModel, clockIn, clockOut, today, ref abnormityTicks);
                            int imgType = 0;
                            switch (info.MatterState)
                            {
                                case 1:
                                case 5:
                                case 6:
                                    imgType = 1;
                                    break;
                                case 2:
                                    imgType = 3;
                                    break;
                                case 3:
                                case 4:
                                    imgType = 2;
                                    break;
                                default:
                                    imgType = 1;
                                    break;
                            }

                            string matterType;
                            switch (info.MatterType)
                            {
                                case 1:
                                    matterType = "病假";
                                    break;

                                case 2:
                                    matterType = "事假";
                                    break;

                                case 3:
                                    matterType = "年假";
                                    break;

                                case 4:
                                    matterType = "婚假";
                                    break;

                                case 5:
                                    matterType = "产假";
                                    break;

                                case 6:
                                    matterType = "丧假";
                                    break;

                                case 7:
                                    matterType = "出差";
                                    break;

                                case 8:
                                    matterType = "外出";
                                    break;

                                case 9:
                                    matterType = "调休";
                                    break;
                                case 10:
                                    matterType = "其它";
                                    break;
                                case 11:
                                    matterType = "产检";
                                    break;
                                case 13:
                                    matterType = "晚到申请";
                                    break;
                                case 14:
                                    matterType = "陪产假";
                                    break;
                                case 15:
                                    matterType = "年假(补)";
                                    break;
                                default:
                                    continue;
                            }

                            string imagename;
                            int t = 0;
                            imagename = "qingjia";

                            if (info.MatterType == 8)
                            {
                                t = 2;
                                imagename = "waichu";
                            }
                            if (info.MatterType == 7)
                            {
                                t = 3;
                                imagename = "chuchai";
                            }
                            if (info.MatterType == 9 || info.MatterType == 13)
                            {
                                t = 4;
                                imagename = "tiaoxiu";
                            }
                            if (info.MatterType == 10)
                            {
                                t = 5;
                                imagename = "qita";
                            }

                            if (!holidays.Contains(today.Day))
                            {
                                if (imgType == 1)
                                {
                                    string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                                    string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, t);
                                    builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                    builder.Append("</a>");
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7 || info.MatterType == 14)
                            {
                                if (imgType == 1)
                                {
                                    string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"] + "/Default.aspx?contentUrl=";
                                    string url = "/Attendance/" + string.Format(Status.defaultUrl, "", info.ID, t);
                                    builder.Append("<a onclick=\"window.open('").Append(attendanceHeader).Append(HttpUtility.UrlEncode(url)).Append("')\">");
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                    builder.Append("</a>");
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 提交考勤记录，等待审批，提交之前先保存一下当前选择日期的考情记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // 保存当前选择日期的考勤记录
                string clew = "";
                int year = cldAttendance.SelectedDate.Year;
                int month = cldAttendance.SelectedDate.Month;
                BindCalender(year, month);

                int flagtype = 0;
                if (matters != null && matters.Count > 0)
                {
                    foreach (MattersInfo model in matters)
                    {
                        if (model.MatterState != Status.MattersState_Passed)
                        {
                            flagtype = 1;
                        }
                    }
                }
                //if (overtimes != null && overtimes.Count > 0)
                //{
                //    foreach (SingleOvertimeInfo model in overtimes)
                //    {
                //        if (model.Approvestate != Status.OverTimeState_Passed)
                //        {
                //            flagtype = 1;
                //        }
                //    }
                //}
                if (flagtype == 1)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('您尚有考勤事由未审批，无法提交整月考勤！');", true);
                }
                else
                {
                    if (string.IsNullOrEmpty(clew))
                    {
                        bool b = attMan.SubmintAttendance(UserID, year, month, chkDimission.Checked);
                        if (b)
                        {
                            // 驳回自动关闭七天的提交限制
                            AttGracePeriodManager manager = new AttGracePeriodManager();
                            manager.DeleteByUserId(UserID);

                            ClientScript.RegisterStartupScript(typeof(string), "", "alert('考勤记录提交成功，等待审批。');window.location='AttendanceSelect.aspx';", true);
                        }
                        else
                            ClientScript.RegisterStartupScript(typeof(string), "", "alert('考勤记录提交失败。');window.location='AttendanceSelect.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + clew + "');window.location='AttendanceSelect.aspx';", true);
                    }
                    // 设置各月考勤的状态标题
                    SetTitleString();
                    SetMonthStat();
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "提交考勤记录", ESP.Logging.LogLevel.Error, ex);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('考勤记录提交失败。');", true);
            }
        }

        /// <summary>
        /// 可见月份改变的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cldAttendance_VisibleDateChanged(object sender, EventArgs e)
        {

            int year = cldAttendance.VisibleDate.Year;
            int month = cldAttendance.VisibleDate.Month;

            int day = cldAttendance.SelectedDate.Day;
            int total = DateTime.DaysInMonth(year, month);
            if (day > total)
                day = total;
            cldAttendance.SelectedDate = new DateTime(year, month, day);

            BindCalender(year, month);

            // 设置各月考勤的状态标题
            SetTitleString();
            SetMonthStat();


        }

        /// <summary>
        /// 设置各月考勤考勤记录的标题和审批状态
        /// </summary>
        public void SetTitleString()
        {
            labRemark.Text = "";
            // 判断用户提示信息是否为空
            if (!string.IsNullOrEmpty(Tips))
            {
                cldAttendance.TitleDateFormat = Tips;
            }
            else
            {
                MonthStatInfo monthStatInfo =
                monthManager.GetMonthStatInfoApprove(UserID, cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month);
                string title = cldAttendance.MonthNames[cldAttendance.SelectedDate.Month - 1]
                        + " " + cldAttendance.SelectedDate.Year + "    {0}";
                if (monthStatInfo != null)
                {
                    if (monthStatInfo.State == Status.MonthStatAppState_NoSubmit)
                    {
                        title = string.Format(title, "未提交");
                        ImageButton2.Visible = true;
                        btnAddMatters.Visible = true;
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitDirector)
                    {
                        title = string.Format(title, "等待总监审批");
                        ImageButton2.Visible = false;
                        btnAddMatters.Visible = false;
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitStatisticians)
                    {
                        title = string.Format(title, "等待考勤统计员审批");
                        ImageButton2.Visible = false;
                        btnAddMatters.Visible = false;
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_Passed)
                    {
                        title = string.Format(title, "审批通过");
                        ImageButton2.Visible = false;
                        btnAddMatters.Visible = false;
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitADAdmin)
                    {
                        title = string.Format(title, "等待考勤管理员确认");
                        ImageButton2.Visible = false;
                        btnAddMatters.Visible = false;
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitHRAdmin)
                    {
                        title = string.Format(title, "等待人力审批");
                        ImageButton2.Visible = false;
                        btnAddMatters.Visible = false;
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitManager)
                    {
                        title = string.Format(title, "等待团队总经理审批");
                        ImageButton2.Visible = false;
                        btnAddMatters.Visible = false;
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_Overrule)
                    {
                        title = string.Format(title, "审批驳回");
                        ImageButton2.Visible = true;
                        btnAddMatters.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(monthStatInfo.ApproveRemark))
                    {
                        labRemark.Text = "审批记录：<br/>" + monthStatInfo.ApproveRemark.Replace("\r\n", "<br/>");
                    }
                }
                else
                {
                    title = string.Format(title, "未提交");
                    ImageButton2.Visible = true;
                    btnAddMatters.Visible = true;
                }
                cldAttendance.TitleDateFormat = title;
            }
        }

        /// <summary>
        /// 设置当前用户所选择的月份的考情统计信息
        /// </summary>
        protected void SetMonthStat()
        {
            UserAttBasicInfo userBasicModel = userBasicManager.GetModelByUserid(UserID);
            if (userBasicModel != null)
            {
                AttendanceDataInfo attdatainfo = attMan.GetMonthStat(UserID,
                           cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month, clockInTimes, employeeJobModel, dimissionModel);
                if (attdatainfo != null)
                {
                    // 判断人员考勤的类型，如果是考勤是特殊的就不计算考勤情况
                    if (attdatainfo.LateCount > 0)
                        labLate.Text = attdatainfo.LateCount + "";    // 迟到
                    else
                        labLate.Text = "";    // 迟到

                    if (attdatainfo.LeaveEarlyCount > 0)
                        labLeaveEarly.Text = attdatainfo.LeaveEarlyCount + "";   // 早退
                    else
                        labLeaveEarly.Text = "";

                    if (attdatainfo.AbsentHours > 0)
                        labAbsent.Text = string.Format("{0:F1}", attdatainfo.AbsentHours / Status.WorkingHours) + "D";    // 旷工
                    else
                        labAbsent.Text = "";

                    if (attdatainfo.EvectionHours > 0)
                        labEvection.Text = string.Format("{0:F1}", attdatainfo.EvectionHours / Status.WorkingHours) + "D";  // 出差
                    else
                        labEvection.Text = "";

                    if (attdatainfo.EgressHours > 0)
                        labEgress.Text = string.Format("{0:F3}", attdatainfo.EgressHours) + "H";   // 外出
                    else
                        labEgress.Text = "";

                    if (attdatainfo.SickLeaveHours > 0)
                        labSickLeave.Text = string.Format("{0:F3}", attdatainfo.SickLeaveHours) + "H"; // 病假
                    else
                        labSickLeave.Text = "";

                    if (attdatainfo.SickByYear > 0)
                        lblSickTotal.Text = string.Format("{0:F3}", attdatainfo.SickByYear) + "H"; // 病假Total
                    else
                        lblSickTotal.Text = "";

                    if (attdatainfo.AffiairLeaveHours > 0)
                        labAffiairLeave.Text = string.Format("{0:F3}", attdatainfo.AffiairLeaveHours) + "H";  // 事假
                    else
                        labAffiairLeave.Text = "";

                    if (attdatainfo.AffairByYear > 0)
                        labAffairTotal.Text = string.Format("{0:F3}", attdatainfo.AffairByYear) + "H";  // 事假Total
                    else
                        labAffairTotal.Text = "";

                    if (attdatainfo.AnnualLeaveHours > 0)
                        labAnnualLeave.Text = string.Format("{0:F3}", attdatainfo.AnnualLeaveHours / Status.WorkingHours) + "D";  // 法定假
                    else
                        labAnnualLeave.Text = "";

                    if (attdatainfo.AnnualLeaveByYear > 0)
                        lblAnnualTotal.Text = string.Format("{0:F3}", attdatainfo.AnnualLeaveByYear / Status.WorkingHours) + "D";  // 法定假
                    else
                        lblAnnualTotal.Text = "";

                    if (attdatainfo.LastAnnualHours > 0)
                        labAnnualLast.Text = string.Format("{0:F3}", attdatainfo.LastAnnualHours / Status.WorkingHours) + "D";  // 补年假
                    else
                        labAnnualLast.Text = "";

                    if (attdatainfo.FuneralLeaveHours > 0)
                        labFuneralLeave.Text = string.Format("{0:F3}", attdatainfo.FuneralLeaveHours / Status.WorkingHours) + "D";   // 丧假
                    else
                        labFuneralLeave.Text = "";

                    if (attdatainfo.MarriageLeaveHours > 0)
                        labMarriageLeave.Text = string.Format("{0:F1}", attdatainfo.MarriageLeaveHours / Status.WorkingHours) + "D";  // 婚假
                    else
                        labMarriageLeave.Text = "";

                    if (attdatainfo.MaternityLeaveHours > 0)
                        labMaternityLeave.Text = string.Format("{0:F1}", attdatainfo.MaternityLeaveHours / Status.WorkingHours) + "D";  // 产假
                    else
                        labMaternityLeave.Text = "";

                    if (attdatainfo.PrenatalCheckHours > 0)
                        labPrenatalCheck.Text = string.Format("{0:F3}", attdatainfo.PrenatalCheckHours / Status.WorkingHours) + "D";   // 产前检查
                    else
                        labPrenatalCheck.Text = "";

                    //if (attdatainfo.IncentiveHours > 0)
                    //    labIncentive.Text = string.Format("{0:F1}", attdatainfo.IncentiveHours / Status.WorkingHours) + "D";      // 福利假
                    //else
                    //    labIncentive.Text = "";

                    //if (attdatainfo.IncentiveByYear > 0)
                    //    lblIncentiveTotal.Text = string.Format("{0:F1}", attdatainfo.IncentiveByYear / Status.WorkingHours) + "D";      // 福利假
                    //else
                    //    lblIncentiveTotal.Text = "";
                }
            }
        }

        /// <summary>
        /// 网站系统信息
        /// </summary>
        protected string WebSiteInfoUrl
        {
            get
            {
                string url = "";
                IList<WebSiteInfo> list = ESP.Framework.BusinessLogic.WebSiteManager.GetByUser(ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID());
                if (list != null)
                {
                    foreach (WebSiteInfo web in list)
                    {
                        if (web.WebSiteID == 30)
                        {
                            url = web.HttpRootUrl;
                        }
                    }
                }
                return url;
            }
        }

        /// <summary>
        /// 添加考勤事由
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddMatters_Click(object sender, EventArgs e)
        {
            string attendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"];
            // 添加考勤事由系统自动将用户选择的日期带到要添加的用户信息页面中
            DateTime selectDateTime = cldAttendance.SelectedDate;
            string strselectDateTime = selectDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (!attMan.CheckIsOpenedUser(UserID))
            {
                if (selectDateTime.Date < Status.ExecuteRestrictTime.Date)
                {
                    if (DateTime.Now.Date > Status.ExecuteRestrictTime.AddDays(Status.SubmitTerm))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('您不能为当天添加事由，已经超过了提交的期限（提交期限自考勤异常当日起7天内有效）。');", true);
                        BindCalender(selectDateTime.Year, selectDateTime.Month);
                        // 设置各月考勤的状态标题
                        SetTitleString();
                        SetMonthStat();
                        return;
                    }
                }
                else
                {
                    selectDateTime = selectDateTime.AddDays(Status.SubmitTerm);
                    if (DateTime.Now.Date > selectDateTime.Date)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('您不能为当天添加事由，已经超过了提交的期限（提交期限自考勤异常当日起7天内有效）。');", true);
                        BindCalender(selectDateTime.Year, selectDateTime.Month);
                        // 设置各月考勤的状态标题
                        SetTitleString();
                        SetMonthStat();
                        return;
                    }
                }
            }
            Response.Redirect(attendanceHeader + "/Default.aspx?contentUrl="
                + HttpUtility.UrlEncode("/Attendance/MattersEdit.aspx?selectdatetime=" + strselectDateTime
                + "&backurl=AttendanceSelect.aspx"));
        }

        /// <summary>
        /// 调休日期信息
        /// </summary>
        public string TipsInfo
        {
            get
            {
                ESP.HumanResource.Entity.DimissionFormInfo dimission = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(UserID);

                // 剩余调休小时总数
               // int remaining = singleOverTime.GetRemainingHours(UserID, DateTime.Now);
                // 最近一周即将过期的调休时间总数
               // int invalid = singleOverTime.InvalidWithinAWeek(UserID, DateTime.Now);
                // 剩余法定假总数
                double remainingAnnualDays = 0;
                // 个人法定假总数
                double totalAnnualLeaveDays = attMan.CalAnnualLeave(UserID, cldAttendance.SelectedDate.Year, out remainingAnnualDays);
              
                // 奖励年假
                double remainingAwardDays = 0;
                double awardAnnualDays = attMan.GetAwardAnnualDays(UserID, cldAttendance.SelectedDate.Year, out remainingAwardDays);

                if (dimission != null && dimission.Status > (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector)
                {
                    double remainAnnual = 0;
                    double canUseAnnual = 0;
                    double usedAnnual = 0;
                    double annualBase = 0;
                    try
                    {
                        remainAnnual = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetAnnualLeaveInfo(dimission.UserId, dimission.LastDay.Value, out canUseAnnual, out usedAnnual, out annualBase);
                    }
                    catch
                    {
                        remainAnnual = 0;

                    }



                    return "年假（剩余/总数）：" + ((int)remainAnnual).ToString() + "/" + ((int)canUseAnnual).ToString();
                }
                else
                {
                    return "年假（剩余/总数）：" + (remainingAnnualDays + remainingAwardDays) + "/" + (totalAnnualLeaveDays + awardAnnualDays);
                }
            }
        }

        /// <summary>
        /// 导出考勤统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            BindCalender(cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month);
            attdatainfo = attMan.GetMonthStat(UserID,
                           cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month, clockInTimes, employeeJobModel, dimissionModel);
            //attdatainfo = attMan.GetApprovedMonthStat(UserID,
            //               cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month, clockInTimes, employeeJobModel);
             
            FileHelper.ExportStatistics(null, Server.MapPath("~"), cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month, UserID, attdatainfo,
                clockInTimes, holidays, matters, overtimes, employeeJobModel, commuterTimeList, Response, dimissionModel);
        }
    }
}