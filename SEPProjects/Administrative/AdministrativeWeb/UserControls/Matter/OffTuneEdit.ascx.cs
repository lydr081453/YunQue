using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using System.Net.Mail;

namespace AdministrativeWeb.UserControls.Matter
{
    /// <summary>
    /// 调休用户控件
    /// </summary>
    public partial class OffTuneEdit : MatterUserControl
    {
        #region 成员变量
        /// <summary>
        /// 考勤事由业务实现类
        /// </summary>
        MattersManager matterManager = new MattersManager();
        /// <summary>
        /// 考勤时间记录信息业务对象
        /// </summary>
        private ClockInManager clockInManager = new ClockInManager();
        /// <summary>
        /// 考勤业务对象类
        /// </summary>
        private ESP.Administrative.BusinessLogic.AttendanceManager attMan =
            new ESP.Administrative.BusinessLogic.AttendanceManager();
        /// <summary>
        /// OT业务信息类
        /// </summary>
        private SingleOvertimeManager singleManager = new SingleOvertimeManager();
        /// <summary>
        /// 用户上下班时间信息业务信息类
        /// </summary>
        private CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitPage()
        {
            // 判断调休单ID是否存在
            if (!string.IsNullOrEmpty(Request["matterid"]))
            {
                MattersInfo model = matterManager.GetModel(int.Parse(Request["matterid"]));
                if (model != null)
                {
                    if (model.MatterType == Status.MattersType_OffTune)
                    {
                        ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);

                        hidOffUserid.Value = model.UserID.ToString();
                        txtOffUserName.Text = userinfoModel.FullNameCN;
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                        txtOffUserCode.Text = emp.Code;

                        // 获得剩余的倒休小时数
                        //int remaining = singleManager.GetRemainingHours(UserID, DateTime.Parse(_selectDateTime));
                        //int invalid = singleManager.InvalidWithinAWeek(UserID, DateTime.Parse(_selectDateTime));

                        //double totalRemaining = (((double)remaining) / Status.WorkingHours);
                        //totalRemaining = Math.Floor(totalRemaining * 2) / 2;
                        //double totalInvalid = (((double)invalid) / Status.WorkingHours);
                        //totalInvalid = Math.Floor(totalInvalid * 2) / 2;

                        //labRemainingHours.Text = "可调休天数：" + totalRemaining + "天，一个星期内将要作废的调休天数：" + totalInvalid + "天";

                        IList<ESP.Framework.Entity.EmployeePositionInfo> list = 
                            ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                        if (list != null && list.Count > 0)
                        {
                            ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                            txtOffGroup.Text = emppos.DepartmentName;
                            int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                            txtOffTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                        }
                        labOffAppTime.Text = model.CreateTime.ToString("yyyy年MM月dd日 HH时");

                        if (model.EndTime.Hour == 12)
                        {
                            model.EndTime = model.EndTime.AddHours(-1);
                        }
                        OffPickerFrom1.SelectedDate = model.BeginTime;
                        OffPickerTo1.SelectedDate = model.EndTime;
                        txtOffTuneCause.Text = model.MatterContent;
                    }
                }
            }
            else
            {
                DateTime selectDateTime = DateTime.Parse(_selectDateTime);
                // 获得用户的上下班时间信息集合
                List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserID);
                // 用户的上下班时间信息
                CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, selectDateTime);

                hidOffUserid.Value = UserID.ToString();
                txtOffUserName.Text = UserInfo.FullNameCN;
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                txtOffUserCode.Text = emp.Code;
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                    txtOffGroup.Text = emppos.DepartmentName;
                    int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                    txtOffTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                }
                labOffAppTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时");

                // 获得剩余的倒休小时数
                //int remaining = singleManager.GetRemainingHours(UserID, selectDateTime);
                //int invalid = singleManager.InvalidWithinAWeek(UserID, selectDateTime);

                //double totalRemaining = (((double)remaining) / Status.WorkingHours);
                //totalRemaining = Math.Floor(totalRemaining * 2) / 2;
                //double totalInvalid = (((double)invalid) / Status.WorkingHours);
                //totalInvalid = Math.Floor(totalInvalid * 2) / 2;

                //labRemainingHours.Text = "可调休天数：" + totalRemaining + "天，一个星期内将要作废的调休天数：" + totalInvalid + "天";

                // 获取上下班时间
                DateTime goWorkTime = DateTime.Parse(Status.EmptyTime);
                DateTime offWorkTime = DateTime.Parse(Status.EmptyTime);
                clockInManager.GetAttendanceTime(UserID, selectDateTime, out goWorkTime, out offWorkTime);
                // 是否迟到
                bool isLate = false;
                // 是否上午旷工
                bool isAMAbsent = false;
                // 是否下午旷工
                bool isPMAbsent = false;
                // 是否早退
                bool isLeaveEarly = false;
                TimeSpan span = new TimeSpan();
                attMan.CalDefaultMatters(UserID, selectDateTime,
                    goWorkTime, offWorkTime, out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out span, commuterTimeModel);
                if (isLate || isAMAbsent || isPMAbsent)
                {
                    OffPickerFrom1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);

                    DateTime beginSelDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    while (beginSelDate < goWorkTime)
                    {
                        beginSelDate = beginSelDate.AddHours(1);
                    }
                    OffPickerTo1.SelectedDate = beginSelDate;
                }
                else if (isLeaveEarly)
                {
                    OffPickerTo1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    DateTime endSelDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    while (endSelDate > offWorkTime)
                    {
                        endSelDate = endSelDate.AddHours(-1);
                    }
                    OffPickerFrom1.SelectedDate = endSelDate;
                }
                else
                {
                    OffPickerFrom1.SelectedDate = selectDateTime;
                    OffPickerTo1.SelectedDate = selectDateTime;
                }
            }
        }

        /// <summary>
        /// 保存休假单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOffSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["matterid"]))
            {
                DateTime selectDateTime = OffPickerFrom1.SelectedDate;

                // 判断时间段，不允许事后申请
                //if (selectDateTime <= DateTime.Now)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('目前系统暂不支持事后申请，请联系HR。');", true);
                //    return;
                //}

                MonthStatManager monthStatManager = new MonthStatManager();
                // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                if (!monthStatManager.TryOperateData(UserID, selectDateTime))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                    return;
                }
                if (!attMan.CheckIsOpenedUser(UserID))
                {
                    if (selectDateTime.Date < Status.ExecuteRestrictTime.Date)
                    {
                        if (DateTime.Now.Date > Status.ExecuteRestrictTime.AddDays(Status.SubmitTerm))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您不能为当天添加事由，已经超过了提交的期限（提交期限自考勤异常当日起7天内有效）。');", true);
                            return;
                        }
                    }
                    else
                    {
                        selectDateTime = selectDateTime.AddDays(Status.SubmitTerm);
                        if (DateTime.Now.Date > selectDateTime.Date)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您不能为当天添加事由，已经超过了提交的期限（提交期限自考勤异常当日起7天内有效）。');", true);
                            return;
                        }
                    }
                }
            }

            if (CheckIsOverLap(Request["matterid"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('此事由和之前申请的事由存在时间上的重叠，请确认后重新申请！');", true);
                return;
            }
            // 获得剩余的倒休小时数
           // int remaining = singleManager.GetRemainingHours(UserID, OffPickerFrom1.SelectedDate);
            MattersInfo model = GetMatterModel(Request["matterid"]);

            // 判断时间段，不允许事后申请
            //if (model.BeginTime <= DateTime.Now)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('目前系统暂不支持事后申请，请联系HR。');", true);
            //    return;
            //}

            model.MatterState = Status.MattersState_NoSubmit;
            try
            {
                if (model.ID > 0)
                {
                    // 用户已经占用的调休小时数加上剩余的调休小时数是否大于或者等于用户现在申请的调休小时数
                    //if (model.TotalHours <= remaining + matterManager.GetModel(model.ID).TotalHours)
                    //{
                        matterManager.UpdateOffTune(model);
                        ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")保存了一条调休单信息编号(" + model.ID + ")",
                            "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                            Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('调休单保存成功。');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                    //        Guid.NewGuid().ToString(), "alert('您剩余的倒休小时数不足，请确认。');", true);
                    //}
                }
                else
                {
                    // 判断用户申请的调休小时数是否大于后者等于剩余的调休小时数
                    //if (model.TotalHours <= remaining)
                    //{
                        matterManager.AddOffTune(model);

                        ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")保存了一条调休单信息编号(" + model.ID + ")",
                            "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                            Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('调休单保存成功。');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                    //        Guid.NewGuid().ToString(), "alert('您剩余的倒休小时数不足，请确认。');", true);
                    //}
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                    Guid.NewGuid().ToString(), "alert('调休单保存失败。');", true);
            }
        }

        /// <summary>
        /// 提交调休单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOffSubmit_Click(object sender, EventArgs e)
        {
            // 判断该考勤事由是否允许直接提交，不允许就直接提交
            if (!IsCanSubmitMatters(Request["matterid"]))
            {
                DateTime selectDateTime = OffPickerFrom1.SelectedDate;
                // 判断时间段，不允许事后申请
                //if (selectDateTime <= DateTime.Now)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('目前系统暂不支持事后申请，请联系HR。');", true);
                //    return;
                //}
                MonthStatManager monthStatManager = new MonthStatManager();
                // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                if (!monthStatManager.TryOperateData(UserID, selectDateTime))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                    return;
                }
                if (!attMan.CheckIsOpenedUser(UserID))
                {
                    if (selectDateTime.Date < Status.ExecuteRestrictTime.Date)
                    {
                        if (DateTime.Now.Date > Status.ExecuteRestrictTime.AddDays(Status.SubmitTerm))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您不能为当天添加事由，已经超过了提交的期限（提交期限自考勤异常当日起7天内有效）。');", true);
                            return;
                        }
                    }
                    else
                    {
                        selectDateTime = selectDateTime.AddDays(Status.SubmitTerm);
                        if (DateTime.Now.Date > selectDateTime.Date)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您不能为当天添加事由，已经超过了提交的期限（提交期限自考勤异常当日起7天内有效）。');", true);
                            return;
                        }
                    }
                }
            }

            if (CheckIsOverLap(Request["matterid"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('此事由和之前申请的事由存在时间上的重叠，请确认后重新申请！');", true);
                return;
            }
            // 获得剩余的倒休小时数
           // int remaining = singleManager.GetRemainingHours(UserID, OffPickerFrom1.SelectedDate);
            MattersInfo model = GetMatterModel(Request["matterid"]);

            // 判断时间段，不允许事后申请
            //if (model.BeginTime <= DateTime.Now)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('目前系统暂不支持事后申请，请联系HR。');", true);
            //    return;
            //}

            try
            {
                model.MatterState = Status.MattersState_WaitHR;
                if (model.ID > 0)
                {
                    // 用户已经占用的调休小时数加上剩余的调休小时数是否大于或者等于用户现在申请的调休小时数
                    //if (remaining + matterManager.GetModel(model.ID).TotalHours >= model.TotalHours)
                    //{
                        ApproveLogInfo approve = GetApproveLogModel("", (int)Status.MattersSingle.MattersSingle_OffTune);

                        matterManager.Update(model, approve);

                        ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条调休单信息编号(" + model.ID + "),审批人是(" + approve.ApproveID + ", " + approve.ApproveName + ")",
                            "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                        //发邮件
                        try
                        {
                            string email = new ESP.Compatible.Employee(approve.ApproveID).EMail;
                            if (!string.IsNullOrEmpty(email))
                            {
                                string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + model.ID + "&flag=1";
                                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                body += "<br><br>" + UserInfo.FullNameCN + "提交的调休单等待您的审批";
                                string pageurl = "Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "/" + string.Format(pageurl, approve.ApproveType, approve.ID) + "'>"
                                    + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                MailAddress[] recipientAddress = { new MailAddress(email) };
                                ESP.Mail.MailManager.Send("考勤事由等待审批", body, true, null, recipientAddress, null, null, null);
                            }
                        }
                        catch
                        {
                            ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条调休单信息编号(" + model.ID + "),审批人是(" + approve.ApproveID + ", " + approve.ApproveName + ")，发送邮件失败",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('调休单提交成功，等待“" + approve.ApproveName + "”的审批');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                    //        Guid.NewGuid().ToString(), "alert('您剩余的倒休小时数不足，请确认。');", true);
                    //}
                }
                else
                {
                    // 判断用户申请的调休小时数是否大于后者等于剩余的调休小时数
                    //if (model.TotalHours <= remaining)
                    //{
                        ApproveLogInfo approve = GetApproveLogModel("", (int)Status.MattersSingle.MattersSingle_OffTune);
                        int returnvalue = matterManager.Add(model, approve);

                        ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条调休单信息编号(" + returnvalue + "),审批人是(" + approve.ApproveID + ", " + approve.ApproveName + ")",
                            "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                        //发邮件
                        try
                        {
                            string email = new ESP.Compatible.Employee(approve.ApproveID).EMail;
                            if (!string.IsNullOrEmpty(email))
                            {
                                string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + model.ID + "&flag=1";
                                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                body += "<br><br>" + UserInfo.FullNameCN + "提交的调休单等待您的审批";
                                string pageurl = "Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "/" + string.Format(pageurl, approve.ApproveType, approve.ID) + "'>"
                                    + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                MailAddress[] recipientAddress = { new MailAddress(email) };
                                ESP.Mail.MailManager.Send("考勤事由等待审批", body, true, null, recipientAddress, null, null, null);
                            }
                        }
                        catch
                        {
                            ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条调休单信息编号(" + returnvalue + "),审批人是(" + approve.ApproveID + ", " + approve.ApproveName + ")，发送邮件失败",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('调休单提交成功，等待“" + approve.ApproveName + "”的审批');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                    //        Guid.NewGuid().ToString(), "alert('您剩余的倒休小时数不足，请确认。');", true);
                    //}
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('调休单提交失败！');", true);

            }
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOffBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl);
        }

        /// <summary>
        /// 获得一个考勤事由的实例对象
        /// </summary>
        /// <param name="leaveID">考勤事由对象编号</param>
        /// <returns>返回一个考勤事由的实例对象</returns>
        private MattersInfo GetMatterModel(string leaveID)
        {
            // 获得用户的上下班时间信息集合
            List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserID);
            // 获得一个事由对象
            MattersInfo model = null;
            // 如果事由ID不为空，就通过ID值获得一个事由对象；否则就创建一个事由对象
            if (!string.IsNullOrEmpty(leaveID))
            {
                model = new MattersManager().GetModel(int.Parse(leaveID.Trim()));
            }
            else
            {
                model = new MattersInfo();
                model.CreateTime = DateTime.Now;
                model.UserID = UserInfo.UserID;
            }
            DateTime beginTime = OffPickerFrom1.SelectedDate;
            DateTime endTime = OffPickerTo1.SelectedDate;
            // 计算用户的开始和结束时间，判断是半天还是全天，并返回用户的开始时间和结束时间
            double timeRange = attMan.TimeRange(ref beginTime, ref endTime, commuterTimeList);

            model.BeginTime = beginTime;
            model.EndTime = endTime;
            model.MatterType = Status.MattersType_OffTune;
            model.TotalHours = (decimal)timeRange;
            model.OperateorID = UserInfo.UserID;
            model.Deleted = false;
            model.UpdateTime = DateTime.Now;
            model.MatterContent = txtOffTuneCause.Text.Trim();
            return model;
        }

        /// <summary>
        /// 判断事由的时间时候出现了重叠的情况
        /// </summary>
        /// <returns>如果出现了重叠的情况返回true，否则返回false</returns>
        public bool CheckIsOverLap(string matterid)
        {
            int modelid = 0;
            if (!string.IsNullOrEmpty(matterid))
                modelid = int.Parse(matterid);
            return matterManager.CheckIsOverLap(UserID, OffPickerFrom1.SelectedDate, OffPickerTo1.SelectedDate, modelid);
        }
    }
}