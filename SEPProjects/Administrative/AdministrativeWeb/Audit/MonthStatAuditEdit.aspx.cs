using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using System.Text;
using System.Collections;
using System.Net.Mail;

namespace AdministrativeWeb.Audit
{
    public partial class MonthStatAuditEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private string SelectUserID
        {
            get
            {
                return this.ViewState["SelectUserID"] as string;
            }
            set
            {
                this.ViewState["SelectUserID"] = value;
            }
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitPage()
        {
            labApproveDesc.Text = "";
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                ESP.Administrative.Entity.MonthStatInfo model = new MonthStatManager().GetModel(int.Parse(Request["id"]));
                if (model != null)
                {
                    SelectUserID = model.UserID.ToString();
                    // 获得用户基本信息和用户部门组别信息
                    ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);
                    hidLeaveUserid.Value = model.UserID.ToString();
                    txtLeaveUserName.Text = userinfoModel.FullNameCN;
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                    txtLeaveUserCode.Text = emp.Code;
                    IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                        ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                    if (list != null && list.Count > 0)
                    {
                        ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                        txtLeaveGroup.Text = emppos.DepartmentName;
                        int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                        txtLeaveTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                    }
                    if (model.AttendanceSubType == (int)AttendanceSubType.Dimission)
                    {
                        txtLeaveGroup.Text += "，";
                        lblAttendanceType.Text = "离职提交考勤";
                    }

                    hidID.Value = model.ID.ToString();
                    // 迟到
                    txtLate.Text = model.LateCount == 0 ? "" : model.LateCount.ToString();
                    // 早退
                    txtLeaveEarly.Text = model.LeaveEarlyCount == 0 ? "" : model.LeaveEarlyCount.ToString();
                    // 旷工
                    txtAbsent.Text = model.AbsentDays == 0 ? "" : string.Format("{0:F1}", model.AbsentDays / Status.WorkingHours);
                    // OT
                    //txtOverTime.Text = model.OverTimeHours == 0 ? "" : string.Format("{0:F1}", model.OverTimeHours);
                    // 节假日OT
                    //txtHolidayOverTime.Text = model.HolidayOverTimeHours == 0 ? "" : string.Format("{0:F1}", model.HolidayOverTimeHours);
                    // 出差
                    txtEvection.Text = model.EvectionDays == 0 ? "" : string.Format("{0:F1}", model.EvectionDays / Status.WorkingHours);
                    // 外出
                    txtEgress.Text = model.EgressHours == 0 ? "" : string.Format("{0:F3}", model.EgressHours);
                    // 病假
                    txtSickLeave.Text = model.SickLeaveHours == 0 ? "" : string.Format("{0:F3}", model.SickLeaveHours);
                    // 事假
                    txtAffiairLeave.Text = model.AffairLeaveHours == 0 ? "" : string.Format("{0:F3}", model.AffairLeaveHours);
                    // 年假
                    txtAnnualLeave.Text = model.AnnualLeaveDays == 0 ? "" : string.Format("{0:F3}", model.AnnualLeaveDays / Status.WorkingHours);
                    // 年假补去年
                    txtAnnualLast.Text = model.LastAnnualDays == 0 ? "" : string.Format("{0:F3}", model.LastAnnualDays / Status.WorkingHours);
                   
                    // 婚假
                    txtMarriageLeave.Text = model.MarriageLeaveHours == 0 ? "" : string.Format("{0:F1}", model.MarriageLeaveHours / Status.WorkingHours);
                    // 丧假
                    txtFuneralLeave.Text = model.FuneralLeaveHours == 0 ? "" : string.Format("{0:F1}", model.FuneralLeaveHours / Status.WorkingHours);
                    // 产假
                    txtMaternityLeave.Text = model.MaternityLeaveHours == 0 ? "" : string.Format("{0:F1}", model.MaternityLeaveHours / Status.WorkingHours);
                    // 产前检查
                    txtPrenatalCheck.Text = model.PrenatalCheckHours == 0 ? "" : string.Format("{0:F3}", model.PrenatalCheckHours);
 
                    DateTime minDate = new DateTime(model.Year, model.Month, 1);
                    DateTime maxDate = new DateTime(model.Year, model.Month, DateTime.DaysInMonth(model.Year, model.Month));
                    cldAttendance.MinDate = minDate;
                    cldAttendance.MaxDate = minDate;

                    if (!string.IsNullOrEmpty(model.ApproveRemark))
                    {
                        labApproveDesc.Text = "审批记录：<br/>" + model.ApproveRemark.Replace("\r\n", "<br />");
                    }

                    // 绑定用户所提交的考勤信息
                    BindCalender(model.Year, model.Month);
                }
            }
        }

        /// <summary>
        /// 保存月审核信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ESP.Administrative.Entity.MonthStatInfo model = new MonthStatManager().GetModel(int.Parse(hidID.Value));
                this.SaveMonthStat(model);
                new MonthStatManager().Update(model);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + Request["type"] + "';alert('月审核单修改成功。');", true);
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString(), "AdiministrativeWeb", ESP.Logging.LogLevel.Error);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + Request["type"] + "';alert('月审核单修改失败。');", true);
            }
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MonthStatAuditList.aspx?type=" + Request["type"]);
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAppPass_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidID.Value))
            {
                ESP.Administrative.Entity.MonthStatInfo model = new MonthStatManager().GetModel(int.Parse(hidID.Value));
                if (model != null)
                {
                    ApproveLogInfo approve = new ApproveLogInfo();
                    this.SaveMonthStat(model);
                    model.State = Status.MonthStatAppState_NoSubmit;
                    string type = Request["type"];
                    ESP.Administrative.Entity.OperationAuditManageInfo manageModel
                            = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetOperationAuditModelByUserID(model.UserID);
                    switch (type)
                    {
                        case "2":  // 等待总监审批。
                            {
                                model.State = Status.MonthStatAppState_WaitHRAdmin;
                                model.ApproveID = UserInfo.UserID;
                                model.ApproveTime = DateTime.Now;

                                approve.ApproveID = manageModel.HRAdminID;
                                approve.ApproveName = manageModel.HRAdminName;
                                break;
                            }
                        case "4":
                            {
                                model.State = Status.MonthStatAppState_Passed;
                                model.ADAdminID = UserInfo.UserID;
                                model.ADAdminTime = DateTime.Now;
                                approve = null;

                                break;
                            }
                        case "5":
                            {
                                model.State = Status.MonthStatAppState_WaitADAdmin;
                                model.ManagerID = UserInfo.UserID;
                                model.ManagerTime = DateTime.Now;
                                int departmentId = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(model.UserID).DepartmentID;
                                if (departmentId == (int)AreaID.HeadOffic)
                                {
                                    ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("BeijingAttendanceAdmin")[0];
                                    string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                    approve.ApproveID = int.Parse(datecode[0]);
                                    approve.ApproveName = datecode[1];
                                }
                                else if (departmentId == (int)AreaID.ChongqingOffic)
                                {
                                    ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("ChongQingAttendanceAdmin")[0];
                                    string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                    approve.ApproveID = int.Parse(datecode[0]);
                                    approve.ApproveName = datecode[1];
                                }
                                
                                else
                                {
                                    ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("BeijingAttendanceAdmin")[0];
                                    string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                    approve.ApproveID = int.Parse(datecode[0]);
                                    approve.ApproveName = datecode[1];
                                }
                                break;
                            }
                        case "6":
                            {
                                model.State = Status.MonthStatAppState_Passed;
                                model.ADAdminID = UserInfo.UserID;
                                model.ADAdminTime = DateTime.Now;
                                approve = null;
                                break;
                            }
                    }
                    if (type != "4")
                    {
                        approve.ApproveType = (int)Status.MattersSingle.MattersSingle_Attendance;
                        approve.ApproveDateID = model.ID;
                        approve.ApproveState = 0;
                        approve.ApproveUpUserID = UserInfo.UserID;
                        approve.IsLastApprove = 0;
                        approve.Deleted = false;
                        approve.CreateTime = approve.UpdateTime = DateTime.Now;
                        approve.OperateorID = UserInfo.UserID;
                    }

                    ESP.Administrative.Entity.ApproveLogInfo appmodel = new ApproveLogManager().GetModel(UserInfo.UserID, model.ID);
                    if (type == "4")
                        appmodel.IsLastApprove = 1;
                    if (appmodel != null)
                    {
                        appmodel.ApproveState = Status.ApproveState_Passed;
                        appmodel.UpdateTime = DateTime.Now;
                        appmodel.OperateorID = UserID;
                    }
                    model.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：" + txtAppRemark.Text + "；\r\n";

                    if (0 < new MonthStatManager().Update(model, appmodel, approve))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + Request["type"] + "';alert('审批成功。');", true);

                        try
                        {
                            string email = new ESP.Compatible.Employee(model.UserID).EMail;
                            if (!string.IsNullOrEmpty(email))
                            {
                                string body = "<br><br>" + UserInfo.FullNameCN + "审批通过了您的" + model.Year + "年" + model.Month + "月的考勤，备注：" + txtAppRemark.Text + "；";
                                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                MailAddress[] recipientAddress = { new MailAddress(email) };

                                ESP.Mail.MailManager.Send("考勤事由审批", body, true, null, recipientAddress, null, null, null);
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + Request["type"] + "';alert('审批失败，请与系统管理员联系！。');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + Request["type"] + "';alert('审批失败，请与系统管理员联系！。');", true);
                }
            }
        }

        /// <summary>
        /// 审批驳回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOverrule_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidID.Value))
            {
                ESP.Administrative.Entity.MonthStatInfo model = new MonthStatManager().GetModel(int.Parse(hidID.Value));
                if (model != null)
                {
                    this.SaveMonthStat(model);
                    model.State = Status.MonthStatAppState_Overrule;
                    ESP.Administrative.Entity.ApproveLogInfo appmodel = new ApproveLogManager().GetModel(UserInfo.UserID, model.ID);
                    if (appmodel != null)
                    {
                        appmodel.ApproveState = Status.ApproveState_Overrule;
                        appmodel.UpdateTime = DateTime.Now;
                        appmodel.OperateorID = UserID;
                        appmodel.Approveremark += UserInfo.FullNameCN + "驳回了您的" + model.Year + "年" + model.Month + "月的考勤，备注：" + txtAppRemark.Text + "；";
                    }
                    model.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批驳回：" + txtAppRemark.Text + "；\r\n";
                    
                    if (0 < new MonthStatManager().Update(model, appmodel))
                    {
                        AttGracePeriodManager manager = new AttGracePeriodManager();
                        AttGracePeriodInfo attGracePeriodModel = new AttGracePeriodInfo();
                        attGracePeriodModel.UserID = model.UserID;
                        attGracePeriodModel.UserCode = model.UserCode;
                        attGracePeriodModel.EmployeeName = model.EmployeeName;
                        attGracePeriodModel.BeginTime = DateTime.Now;
                        attGracePeriodModel.EndTime = DateTime.Now.AddDays(Status.ApproveGracePeriodDays);
                        attGracePeriodModel.Remark = UserInfo.FullNameCN + "审批驳回" + model.EmployeeName + "的月度考勤系统自动开通提交限制";
                        attGracePeriodModel.CreateTime = DateTime.Now;
                        attGracePeriodModel.OperatorID = UserID;
                        attGracePeriodModel.OperatorName = UserInfo.FullNameCN;
                        attGracePeriodModel.UpdateTime = DateTime.Now;
                        manager.Add(attGracePeriodModel);

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + Request["type"] + "';alert('驳回成功。');", true);

                        try
                        {
                            string email = new ESP.Compatible.Employee(model.UserID).EMail;
                            if (!string.IsNullOrEmpty(email))
                            {
                                string body = "<br><br>" + UserInfo.FullNameCN + "驳回了您的" + model.Year + "年" + model.Month + "月的考勤，备注：" + txtAppRemark.Text + "；";
                                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                MailAddress[] recipientAddress = { new MailAddress(email) };

                                ESP.Mail.MailManager.Send("考勤事由审批", body, true, null, recipientAddress, null, null, null);
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + Request["type"] + "';alert('驳回失败，请与系统管理员联系！。');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + Request["type"] + "';alert('驳回失败，请与系统管理员联系！。');", true);
                }
            }
        }

        #region 局部变量定义
        /// <summary>
        /// 考勤业务对象类
        /// </summary>
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
        /// 事由业务对象
        /// </summary>
        private MattersManager mattersManager = new MattersManager();
        /// <summary>
        /// 上下班时间信息业务对象
        /// </summary>
        private CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
        #endregion

        #region 考勤事由变量定义
        /// <summary>
        /// 打卡记录集合
        /// </summary>
        Hashtable clockInTimes = null;
        /// <summary>
        /// 考勤事由集合
        /// </summary>
        IList<MattersInfo> matters = null;
        /// <summary>
        /// OT单信息集合
        /// </summary>
        IList<SingleOvertimeInfo> overtimes = null;
        /// <summary>
        /// 节假日信息集合
        /// </summary>
        HashSet<int> holidays = null;
        /// <summary>
        /// 当前用户基本信息
        /// </summary>
        UserAttBasicInfo userBasicModel = null;
        /// <summary>
        /// 员工入职信息
        /// </summary>
        ESP.HumanResource.Entity.EmployeeJobInfo employeeJobModel = null;
        /// <summary>
        /// 员工离职信息
        /// </summary>
        ESP.HumanResource.Entity.DimissionInfo dimissionModel = null;
        /// <summary>
        /// 上下班时间信息集合
        /// </summary>
        List<CommuterTimeInfo> commuterList = null;
        #endregion

        /// <summary>
        /// 绑定考勤信息日历
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        public void BindCalender(int year, int month)
        {
            clockInTimes = clockInManager.GetClockInTimesOfMonth(year, month, int.Parse(SelectUserID));
            holidays = new HolidaysInfoManager().GetHolidayListByMonth(year, month);
            matters = mattersManager.GetModelListByMonth(int.Parse(SelectUserID), year, month);
            overtimes = new SingleOvertimeManager().GetModelListByMonth(int.Parse(SelectUserID), year, month);
            userBasicModel = userBasicManager.GetModelByUserid(int.Parse(SelectUserID));
            employeeJobModel = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(int.Parse(SelectUserID));
            dimissionModel = ESP.HumanResource.BusinessLogic.DimissionManager.GetModelByUserID(int.Parse(SelectUserID));
            commuterList = commuterTimeManager.GetCommuterTimeByUserId(int.Parse(SelectUserID));

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
        /// 绑定考勤统计日期值
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
                        CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterList, day.Date);
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
            return builder.ToString();
        }

        /// <summary>
        /// 绑定下班时间，如果是当天则不显示下班时间
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetClockOut(ComponentArt.Web.UI.CalendarDay day)
        {
            CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterList, day.Date);
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
            return "";
        }

        /// <summary>
        /// 判断显示考勤统计图表内容
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetIconsHtml(ComponentArt.Web.UI.CalendarDay day)
        {
            // 获得上下班时间信息
            CommuterTimeInfo commuterModel = commuterTimeManager.GetCommuterTimes(commuterList, day.Date);

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
                if (commuterModel.AttendanceType == Status.UserBasicAttendanceType_Normal)
                {
                    #region 判断显示当天的OT信息
                    foreach (SingleOvertimeInfo info in overtimes)
                    {
                        if ((info.BeginTime >= today && info.BeginTime < tomorrow))
                        {
                            int imgType = 0;
                            switch (info.Approvestate)
                            {
                                case Status.OverTimeState_NotSubmit:
                                case Status.OverTimeState_Cancel:
                                case Status.OverTimeState_Overrule:
                                    imgType = 1;
                                    break;
                                case Status.OverTimeState_Passed:
                                    imgType = 3;
                                    break;
                                case Status.OverTimeState_WaitDirector:
                                case Status.OverTimeState_WaitHR:
                                    imgType = 2;
                                    break;
                                default:
                                    imgType = 1;
                                    break;
                            }

                            if (imgType == 1)
                            {
                                string url = string.Format(Status.defaultUrl, "", info.ID, 1);
                            }
                            string imgUrl = "/images/calendar/" + imgType + "jiaban.jpg";
                            string title;
                            switch (imgType)
                            {
                                case 2:
                                    title = "OT 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.OverTimeCause;
                                    break;
                                case 3:
                                    title = "OT 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.OverTimeCause;
                                    break;
                                case 1:
                                default:
                                    title = "OT 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.OverTimeCause;
                                    break;
                            }

                            builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                            if (imgType == 1)
                            {
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
                    attMan.CalDefaultMatters(int.Parse(SelectUserID), today, clockIn, clockOut,
                        out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterModel);
                    long abnormityTicks = abnormityTime.Ticks;
                    #endregion

                    #region 计算前一天的OT信息，判断是否去除当天的迟到和矿工
                    // 获得前一天的OT单信息，并且是审批通过的OT单
                    List<SingleOvertimeInfo> beforeDaySingleList = singleOverTime.GetSingleOvertimeList(int.Parse(SelectUserID), today.AddDays(-1), true);
                    if (beforeDaySingleList != null && beforeDaySingleList.Count > 0)
                    {
                        foreach (SingleOvertimeInfo single in beforeDaySingleList)
                        {
                            if (single != null)
                            {
                                HolidaysInfo holidayModel = new HolidaysInfoManager().GetHolideysInfoByDatetime(today.AddDays(-1));
                                // 判断用户的OT开始时间是否大于用户的下班时间
                                if (single.BeginTime >= single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)
                                    || ((holidays.Contains(today.AddDays(-1).Day) || holidayModel != null)
                                        && single.EndTime > single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)))
                                {
                                    decimal overTimeHours = 0;
                                    if (!holidays.Contains(today.AddDays(-1).Day) && holidayModel == null)
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
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterModel, clockIn, clockOut, today, ref abnormityTicks);
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
                            if (info.MatterType == 9)
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
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
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
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7)
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
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

                    TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                    #region 判断显示考勤默认事由信息
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
                            if (clockIn > today.Date.Add(commuterModel.GoWorkTime.TimeOfDay) && (clockIn - today.Date.Add(commuterModel.GoWorkTime.TimeOfDay)).TotalMinutes >= 1)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                            else if (clockOut < today.Date.Add(commuterModel.OffWorkTime.TimeOfDay) && today.Date < DateTime.Now.Date && (clockOut - today.Date.Add(commuterModel.OffWorkTime.TimeOfDay)).TotalMinutes >= Status.GoWorkTime_BufferMinute)
                            {
                                string title = "早退";
                                string imgUrl = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                        }
                    }
                    #endregion
                }
                #endregion
                else if (commuterModel.AttendanceType == Status.UserBasicAttendanceType_Special)
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
                    //            string url = string.Format(Status.defaultUrl, "", info.ID, 1);
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

                    if (clockIn >= new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, commuterModel.OffWorkTime.Hour, commuterModel.OffWorkTime.Minute, commuterModel.OffWorkTime.Second))
                                       {
                        clockIn = DateTime.Parse(Status.EmptyTime);
                        clockOut = DateTime.Parse(Status.EmptyTime);
                    }

                    if (clockIn.ToString("yyyy-MM-dd") == Status.EmptyTime || clockOut.ToString("yyyy-MM-dd") == Status.EmptyTime || clockIn == clockOut)
                    {
                        if (day.Date < DateTime.Now.Date)
                        {
                            isPMAbsent = true;
                            TimeSpan workingHours = commuterModel.OffWorkTime - commuterModel.GoWorkTime;
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
                            TimeSpan workingHours = commuterModel.OffWorkTime - commuterModel.GoWorkTime;
                            int hours = workingHours.Hours;
                            int minutes = workingHours.Minutes;
                            int seconds = workingHours.Seconds;

                            DateTime currentOffTime = new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, commuterModel.OffWorkTime.Hour, commuterModel.OffWorkTime.Minute, commuterModel.OffWorkTime.Second);
                            TimeSpan workingHours2 = currentOffTime - clockIn;
                            if (clockOut < currentOffTime)
                            {
                                workingHours2 = clockOut - clockIn;
                            }
                            int hours2 = workingHours2.Hours;
                            int minutes2 = workingHours2.Minutes;
                            int seconds2 = workingHours2.Seconds;
                            span = span.Add(new TimeSpan(hours - hours2, minutes - minutes2, seconds - seconds2));
                        }
                    }
                    long abnormityTicks = span.Ticks;
                    #endregion

                    #region 计算前一天的OT信息，判断是否去除当天的迟到和矿工
                    // 获得前一天的OT单信息，并且是审批通过的OT单
                    //List<SingleOvertimeInfo> beforeDaySingleList = singleOverTime.GetSingleOvertimeList(int.Parse(SelectUserID), today.AddDays(-1), true);
                    //if (beforeDaySingleList != null && beforeDaySingleList.Count > 0)
                    //{
                    //    foreach (SingleOvertimeInfo single in beforeDaySingleList)
                    //    {
                    //        if (single != null)
                    //        {
                    //            // 判断用户的OT开始时间是否大于用户的下班时间
                    //            if (single.BeginTime >= single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)
                    //                || ((holidays.Contains(today.AddDays(-1).Day) || new HolidaysInfoManager().GetHolideysInfoByDatetime(today.AddDays(-1)) != null)
                    //                    && single.EndTime > single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)))
                    //            {
                    //                decimal overTimeHours = 0;
                    //                if (!holidays.Contains(today.AddDays(-1).Day))
                    //                {
                    //                    overTimeHours = single.OverTimeHours;
                    //                }
                    //                else
                    //                {
                    //                    overTimeHours = (decimal)(single.EndTime - single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)).TotalHours;
                    //                }
                    //                // 用户OT小时数大于6小时
                    //                if (overTimeHours >= (decimal)commuterModel.WorkingDays_OverTime2)
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
                    //                else if (overTimeHours >= (decimal)commuterModel.WorkingDays_OverTime1)
                    //                {
                    //                    //TimeSpan tempSpan = new TimeSpan(Status.LateGoWorkTime_OverTime1, 0, 0);
                    //                    DateTime tempSpan = new DateTime().AddHours(commuterModel.LateGoWorkTime_OverTime1);
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
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterModel, clockIn, clockOut, today, ref abnormityTicks);
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
                            if (info.MatterType == 9)
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
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
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
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7)
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
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
                                }
                            }
                        }
                    }
                    #endregion

                    #region 判断考勤默认事由情况
                    TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                    if (!holidays.Contains(today.Day))
                    {
                        if (remainTime.TotalMinutes >= Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
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
                else if (commuterModel.AttendanceType == Status.UserBasicAttendanceType_NotStat)
                {
                    #region 判断显示当天的OT信息
                    foreach (SingleOvertimeInfo info in overtimes)
                    {
                        if ((info.BeginTime >= today && info.BeginTime < tomorrow))
                        {
                            int imgType = 0;
                            switch (info.Approvestate)
                            {
                                case Status.OverTimeState_NotSubmit:
                                case Status.OverTimeState_Cancel:
                                case Status.OverTimeState_Overrule:
                                    imgType = 1;
                                    break;
                                case Status.OverTimeState_Passed:
                                    imgType = 3;
                                    break;
                                case Status.OverTimeState_WaitDirector:
                                case Status.OverTimeState_WaitHR:
                                    imgType = 2;
                                    break;
                                default:
                                    imgType = 1;
                                    break;
                            }

                            if (imgType == 1)
                            {
                                string url = string.Format(Status.defaultUrl, "", info.ID, 1);
                            }
                            string imgUrl = "/images/calendar/" + imgType + "jiaban.jpg";
                            string title;
                            switch (imgType)
                            {
                                case 2:
                                    title = "OT 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.OverTimeCause;
                                    break;
                                case 3:
                                    title = "OT 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.OverTimeCause;
                                    break;
                                case 1:
                                default:
                                    title = "OT 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.OverTimeCause;
                                    break;
                            }

                            builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                            if (imgType == 1)
                            {
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
                    if (clockIn.ToString("yyyy-MM-dd") == Status.EmptyTime && clockOut.ToString("yyyy-MM-dd") == Status.EmptyTime && day.Date < DateTime.Now.Date)
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
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterModel, clockIn, clockOut, today, ref abnormityTicks);
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
                            if (info.MatterType == 9)
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
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
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
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7)
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
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
        /// 更新统计信息
        /// </summary>
        public void SaveMonthStat(MonthStatInfo model)
        {
            if (model != null)
            {
                StringBuilder strinfo = new StringBuilder();
                // 迟到
                if (!string.IsNullOrEmpty(txtLate.Text))
                {
                    if (int.Parse(txtLate.Text.Trim()) != model.LateCount)
                        strinfo.Append("将迟到次数从" + model.LateCount + "次修改为" + int.Parse(txtLate.Text.Trim()) + "次，");
                    model.LateCount = int.Parse(txtLate.Text.Trim());
                }
                else
                    model.LateCount = 0;

                // 早退
                if (!string.IsNullOrEmpty(txtLeaveEarly.Text))
                {
                    if (int.Parse(txtLeaveEarly.Text.Trim()) != model.LeaveEarlyCount)
                        strinfo.Append("将早退次数从" + model.LeaveEarlyCount + "次修改为" + int.Parse(txtLeaveEarly.Text.Trim()) + "次，");
                    model.LeaveEarlyCount = int.Parse(txtLeaveEarly.Text.Trim());
                }
                else
                    model.LeaveEarlyCount = 0;

                // 旷工
                if (!string.IsNullOrEmpty(txtAbsent.Text))
                {
                    if (decimal.Parse(txtAbsent.Text.Trim()) != (model.AbsentDays / Status.WorkingHours))
                        strinfo.Append("将旷工天数从" + (model.AbsentDays / Status.WorkingHours) + "天修改为" + decimal.Parse(txtAbsent.Text.Trim()) + "天，");
                    model.AbsentDays = decimal.Parse(txtAbsent.Text.Trim()) * Status.WorkingHours;
                }
                else
                    model.AbsentDays = 0;

               

                // 出差
                if (!string.IsNullOrEmpty(txtEvection.Text))
                {
                    if (decimal.Parse(txtEvection.Text.Trim()) != (model.EvectionDays / Status.WorkingHours))
                        strinfo.Append("将出差天数从" + (model.EvectionDays / Status.WorkingHours) + "天修改为" + decimal.Parse(txtEvection.Text.Trim()) + "天，");
                    model.EvectionDays = decimal.Parse(txtEvection.Text.Trim()) * Status.WorkingHours;
                }
                else
                    model.EvectionDays = 0;

                // 外出
                if (!string.IsNullOrEmpty(txtEgress.Text))
                {
                    if (decimal.Parse(txtEgress.Text.Trim()) != model.EgressHours)
                        strinfo.Append("将外出小时数从" + model.EgressHours + "小时修改为" + decimal.Parse(txtEgress.Text.Trim()) + "小时，");
                    model.EgressHours = decimal.Parse(txtEgress.Text.Trim());
                }
                else
                    model.EgressHours = 0;

                // 病假
                if (!string.IsNullOrEmpty(txtSickLeave.Text))
                {
                    if (decimal.Parse(txtSickLeave.Text.Trim()) != model.SickLeaveHours)
                        strinfo.Append("将病假小时数从" + model.SickLeaveHours + "小时修改为" + decimal.Parse(txtSickLeave.Text.Trim()) + "小时，");
                    model.SickLeaveHours = decimal.Parse(txtSickLeave.Text.Trim());
                }
                else
                    model.SickLeaveHours = 0;

                // 事假
                if (!string.IsNullOrEmpty(txtAffiairLeave.Text))
                {
                    if (decimal.Parse(txtAffiairLeave.Text.Trim()) != model.AffairLeaveHours)
                        strinfo.Append("将事假小时数从" + model.AffairLeaveHours + "小时修改为" + decimal.Parse(txtAffiairLeave.Text.Trim()) + "小时，");
                    model.AffairLeaveHours = decimal.Parse(txtAffiairLeave.Text.Trim());
                }
                else
                    model.AffairLeaveHours = 0;

                // 年假
                if (!string.IsNullOrEmpty(txtAnnualLeave.Text))
                {
                    if (decimal.Parse(txtAnnualLeave.Text.Trim()) != (model.AnnualLeaveDays / Status.WorkingHours))
                        strinfo.Append("将年假天数从" + (model.AnnualLeaveDays / Status.WorkingHours) + "天修改为" + decimal.Parse(txtAnnualLeave.Text.Trim()) + "天，");
                    model.AnnualLeaveDays = decimal.Parse(txtAnnualLeave.Text.Trim()) * Status.WorkingHours;
                }
                else
                    model.AnnualLeaveDays = 0;

                // 婚假
                if (!string.IsNullOrEmpty(txtMarriageLeave.Text))
                {
                    if (decimal.Parse(txtMarriageLeave.Text.Trim()) != (model.MarriageLeaveHours / Status.WorkingHours))
                        strinfo.Append("将婚假天数从" + (model.MarriageLeaveHours / Status.WorkingHours) + "天修改为" + decimal.Parse(txtMarriageLeave.Text.Trim()) + "天，");
                    model.MarriageLeaveHours = decimal.Parse(txtMarriageLeave.Text.Trim()) * Status.WorkingHours;
                }
                else
                    model.MarriageLeaveHours = 0;

                // 丧假
                if (!string.IsNullOrEmpty(txtFuneralLeave.Text))
                {
                    if (decimal.Parse(txtFuneralLeave.Text.Trim()) != (model.FuneralLeaveHours / Status.WorkingHours))
                        strinfo.Append("将丧假天数从" + (model.FuneralLeaveHours / Status.WorkingHours) + "天修改为" + decimal.Parse(txtFuneralLeave.Text.Trim()) + "天，");
                    model.FuneralLeaveHours = decimal.Parse(txtFuneralLeave.Text.Trim()) * Status.WorkingHours;
                }
                else
                    model.FuneralLeaveHours = 0;

                // 产假
                if (!string.IsNullOrEmpty(txtMaternityLeave.Text))
                {
                    if (decimal.Parse(txtMaternityLeave.Text.Trim()) != (model.MaternityLeaveHours / Status.WorkingHours))
                        strinfo.Append("将产假天数从" + (model.MaternityLeaveHours / Status.WorkingHours) + "天修改为" + decimal.Parse(txtMaternityLeave.Text.Trim()) + "天，");
                    model.MaternityLeaveHours = decimal.Parse(txtMaternityLeave.Text.Trim()) * Status.WorkingHours;
                }
                else
                    model.MaternityLeaveHours = 0;

                model.OperateorID = UserID;
                model.ApproveID = UserID;
                ESP.Logging.Logger.Add(DateTime.Now.ToString("yyyy-MM-dd HH") + "："
                    + UserInfo.FullNameCN + "修改了" + model.EmployeeName + "(" + model.UserID + ")考勤统计信息，" + strinfo.ToString() + "\r\n", "考勤统计信息修改", ESP.Logging.LogLevel.Information);
                model.UpdateTime = DateTime.Now;
            }
        }
    }
}
