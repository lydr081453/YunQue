using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using System.Net.Mail;

namespace AdministrativeWeb.UserControls.Matter
{
    public partial class OverTimeEdit : MatterUserControl
    {
        #region 成员变量
        /// <summary>
        /// OT单业务类
        /// </summary>
        private SingleOvertimeManager overtimeManager = new SingleOvertimeManager();
        /// <summary>
        /// 审批日志业务类
        /// </summary>
        private ApproveLogManager approvelogManager = new ApproveLogManager();
        /// <summary>
        /// 考勤信息管理
        /// </summary>
        private AttendanceManager attendanceManager = new AttendanceManager();
        /// <summary>
        /// 用户考勤基本信息业务对象
        /// </summary>
        private UserAttBasicInfoManager userBasicManager = new UserAttBasicInfoManager();
        /// <summary>
        /// 上下班时间信息业务对象类
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
        /// 初始化页面内容
        /// </summary>
        protected void InitPage()
        {
            // 判断OT单的ID是否存在
            if (Request["matterid"] != null)
            {
                int overTimeId = int.Parse(Request["matterid"]);
                // 获得OT单信息
                SingleOvertimeInfo overtimeInfo = overtimeManager.GetModel(overTimeId);
                if (overtimeInfo != null)
                {
                    singlOverTimeId.Value = overtimeInfo.ID.ToString();
                    hidUserid.Value = overtimeInfo.UserID.ToString();
                    txtUserName.Text = overtimeInfo.EmployeeName;
                    txtUserCode.Text = overtimeInfo.UserCode;
                    IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(overtimeInfo.UserID);
                    if (list != null && list.Count > 0)
                    {
                        ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                        txtGroup.Text = emppos.DepartmentName;
                        int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                        txtTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                    }
                    labAppTime.Text = overtimeInfo.AppTime.ToString("yyyy年MM月dd日 HH时");
                    //radType.SelectedValue = overtimeInfo.OverTimeType.ToString();
                    hidOverTimeProjectId.Value = overtimeInfo.ProjectID.ToString();
                    txtOverTimeProjectNo.Text = overtimeInfo.ProjectNo;
                    PickerFrom1.SelectedDate = overtimeInfo.BeginTime;
                    PickerTo1.SelectedDate = overtimeInfo.EndTime;
                    txtDes.Text = overtimeInfo.OverTimeCause;

                    this.grdMatterDetails.DataSource = new MatterReasonManager().GetList(" SingleOverTimeID=" + overtimeInfo.ID.ToString());
                    this.grdMatterDetails.DataBind();
                }
            }
            else
            {
                DateTime selectDateTime = DateTime.Parse(_selectDateTime);
                // 获得用户的上下班时间信息集合
                List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserID);
                // 用户的上下班时间信息
                CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, selectDateTime);

                hidUserid.Value = UserID.ToString();
                txtUserName.Text = UserInfo.FullNameCN;
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                txtUserCode.Text = emp.Code;
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                    txtGroup.Text = emppos.DepartmentName;
                    int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                    txtTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                }
                if (attendanceManager.CheckIsHoliday(selectDateTime))
                {
                    //radType.SelectedValue = Status.OverTimeType_Holiday.ToString();
                    PickerFrom1.SelectedDate = selectDateTime;
                    PickerTo1.SelectedDate = selectDateTime;
                }
                else
                {
                    //radType.SelectedValue = Status.OverTimeType_Working.ToString();
                    PickerFrom1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    PickerTo1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                }
                labAppTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时");
            }
        }

        /// <summary>
        /// 提交OT单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validTimeSheet())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('OT单中的Time Sheet是必填项，请填写完整。');", true);
                return;
            }
            // 判断该考勤事由是否允许直接提交，不允许就直接提交
            if (!IsCanSubmitOvertime(singlOverTimeId.Value))
            {
                DateTime selectDateTime = PickerFrom1.SelectedDate;
                MonthStatManager monthStatManager = new MonthStatManager();
                // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                if (!monthStatManager.TryOperateData(UserID, selectDateTime))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                    return;
                }
                if (!new AttendanceManager().CheckIsOpenedUser(UserID))
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

            /* 计算OT的总小时数，并判断OT总小时数是否超过3小时，如果超过算有效OT可以提交，否则不可以提交（2011年2月之前有效。）
             * 自2011年2月开始不再做OT小时数的判断，晚上OT只要超过9点第二天可以晚来一小时，超过晚上12点第二天上午可以不来。
             */
            /*
            double overTotalHours = (PickerTo1.SelectedDate - PickerFrom1.SelectedDate).TotalHours;
            // 判断是否是节假日
            if (new HolidaysInfoManager().GetHolideysInfoByDatetime(PickerFrom1.SelectedDate) != null)
            {
                if (overTotalHours < Status.Holiday_MinOverTime)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您此次OT不足4小时，OT单无效。');", true);
                    return;
                }
            }
            else
            {
                if (overTotalHours < Status.WorkingDays_OverTime1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您此次OT不足3小时，OT单无效。');", true);
                    return;
                }
            }
             */

            if (!string.IsNullOrEmpty(singlOverTimeId.Value))
            {
                int overtimeid = int.Parse(singlOverTimeId.Value);
                SingleOvertimeInfo model = overtimeManager.GetModel(overtimeid);
                model.Approvestate = Status.OverTimeState_WaitDirector;   // OT单状态修改为已提交，等待总监审批
                model = this.GetModelInfo(model);
                ApproveLogInfo appinfo = this.GetApproveLogModel(null, (int)Status.MattersSingle.MattersSingle_OverTime);
                overtimeManager.Update(model);
                appinfo.ApproveDateID = overtimeid;
                approvelogManager.Add(appinfo);

                IList<MatterReasonInfo> listReason = new MatterReasonManager().GetList(" SingleOverTimeID=" + overtimeid.ToString());
                if (listReason != null && listReason.Count > 0)
                {
                    foreach (MatterReasonInfo reason in listReason)
                        new MatterReasonManager().Delete(reason.ID);
                }

                foreach (GridViewRow rw in this.grdMatterDetails.Rows)
                {
                    Label lblTimeStart = (Label)rw.FindControl("lblTimeStart");
                    Label lblTimeEnd = (Label)rw.FindControl("lblTimeEnd");
                    TextBox txtMatterDetails = (TextBox)rw.FindControl("txtMatterDetails");

                    MatterReasonInfo info = new MatterReasonInfo();
                    info.StartDate = Convert.ToDateTime(lblTimeStart.Text);
                    info.EndDate = Convert.ToDateTime(lblTimeEnd.Text);
                    info.Details = txtMatterDetails.Text.Trim();
                    info.SingleOverTimeID = overtimeid;

                    new MatterReasonManager().Add(info);
                }

                //ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条OT单信息编号(" + model.ID + "),审批人是(" + appinfo.ApproveID + ", " + appinfo.ApproveName + ")",
                //                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                //发邮件
                try
                {
                    string email = new ESP.Compatible.Employee(appinfo.ApproveID).EMail;
                    if (!string.IsNullOrEmpty(email))
                    {
                        string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?singleid=" + model.ID + "&flag=1";
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        body += "<br><br>" + UserInfo.FullNameCN + "提交的OT单等待您的审批";
                        string pageurl = "Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "/" + string.Format(pageurl, appinfo.ApproveType, appinfo.ID) + "'>"
                            + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                        MailAddress[] recipientAddress = { new MailAddress(email) };

                        ESP.Mail.MailManager.Send("考勤事由等待审批", body, true, null, recipientAddress, null, null, null);
                    }
                }
                catch
                {
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条OT单信息编号(" + model.ID + "),审批人是(" + appinfo.ApproveID + ", " + appinfo.ApproveName + ")，发送邮件失败",
                               "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('OT单提交成功，等待“" + appinfo.ApproveName + "”的审批。');", true);
            }
            else
            {
                SingleOvertimeInfo overtimeinfo = new SingleOvertimeInfo();
                overtimeinfo.Approvestate = Status.OverTimeState_WaitDirector;
                overtimeinfo = this.GetModelInfo(overtimeinfo);
                int id = overtimeManager.Add(overtimeinfo);
                ApproveLogInfo appinfo = this.GetApproveLogModel(null, (int)Status.MattersSingle.MattersSingle_OverTime);
                appinfo.ApproveDateID = id;
                approvelogManager.Add(appinfo);

                foreach (GridViewRow rw in this.grdMatterDetails.Rows)
                {
                    Label lblTimeStart = (Label)rw.FindControl("lblTimeStart");
                    Label lblTimeEnd = (Label)rw.FindControl("lblTimeEnd");
                    TextBox txtMatterDetails = (TextBox)rw.FindControl("txtMatterDetails");

                    MatterReasonInfo info = new MatterReasonInfo();
                    info.StartDate = Convert.ToDateTime(lblTimeStart.Text);
                    info.EndDate = Convert.ToDateTime(lblTimeEnd.Text);
                    info.Details = txtMatterDetails.Text.Trim();
                    info.SingleOverTimeID = id;

                    new MatterReasonManager().Add(info);
                }

                //ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条OT单信息编号(" + id + "),审批人是(" + appinfo.ApproveID + ", " + appinfo.ApproveName + ")",
                //                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                //发邮件
                try
                {
                    string email = new ESP.Compatible.Employee(appinfo.ApproveID).EMail;
                    if (!string.IsNullOrEmpty(email))
                    {
                        string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?singleid=" + id + "&flag=1";
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        body += "<br><br>" + UserInfo.FullNameCN + "提交的OT单等待您的审批";
                        string pageurl = "Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "/" + string.Format(pageurl, appinfo.ApproveType, appinfo.ID) + "'>"
                            + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                        MailAddress[] recipientAddress = { new MailAddress(email) };

                        ESP.Mail.MailManager.Send("考勤事由等待审批", body, true, null, recipientAddress, null, null, null);
                    }
                }
                catch
                {
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条OT单信息编号(" + id + "),审批人是(" + appinfo.ApproveID + ", " + appinfo.ApproveName + ")，发送邮件失败",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('OT单提交成功，等待“" + appinfo.ApproveName + "”的审批。');", true);
            }
        }

        private bool validTimeSheet()
        {
            bool ret = false;
            if (grdMatterDetails.Rows.Count == 0)
                ret = true;
            for (int i = 0; i < grdMatterDetails.Rows.Count; i++)
            {
                GridViewRow row = grdMatterDetails.Rows[i];
                TextBox txtMatterDetails = (TextBox)row.FindControl("txtMatterDetails");
                if (string.IsNullOrEmpty(txtMatterDetails.Text))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 保存OT单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(singlOverTimeId.Value))
            {
                int overtimeid = int.Parse(singlOverTimeId.Value);
                SingleOvertimeInfo model = overtimeManager.GetModel(overtimeid);
                model = this.GetModelInfo(model);
                model.Approvestate = Status.OverTimeState_NotSubmit;
                overtimeManager.Update(model);

                IList<MatterReasonInfo> listReason =new MatterReasonManager().GetList(" SingleOverTimeID=" + overtimeid.ToString());
                if(listReason != null && listReason.Count > 0)
                {
                    foreach (MatterReasonInfo reason in listReason)
                        new MatterReasonManager().Delete(reason.ID);
                }

                foreach (GridViewRow rw in this.grdMatterDetails.Rows)
                {
                    Label lblTimeStart = (Label)rw.FindControl("lblTimeStart");
                    Label lblTimeEnd = (Label)rw.FindControl("lblTimeEnd");
                    TextBox txtMatterDetails = (TextBox)rw.FindControl("txtMatterDetails");

                    MatterReasonInfo info = new MatterReasonInfo();
                    info.StartDate = Convert.ToDateTime(lblTimeStart.Text);
                    info.EndDate = Convert.ToDateTime(lblTimeEnd.Text);
                    info.Details = txtMatterDetails.Text.Trim();
                    info.SingleOverTimeID = overtimeid;

                    new MatterReasonManager().Add(info);
                }
                ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")保存了一条OT单信息编号(" + model.ID + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);

            }
            else
            {
                DateTime selectDateTime = PickerFrom1.SelectedDate;

                MonthStatManager monthStatManager = new MonthStatManager();
                // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                if (!monthStatManager.TryOperateData(UserID, selectDateTime))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                    return;
                }
                if (!new AttendanceManager().CheckIsOpenedUser(UserID))
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

                /*
                // 计算OT的总小时数，并全段OT总小时数是否超过3小时，如果超过算有效OT可以提交，否则不可以提交
                double overTotalHours = (PickerTo1.SelectedDate - PickerFrom1.SelectedDate).TotalHours;
                // 判断是否是节假日
                if (new HolidaysInfoManager().GetHolideysInfoByDatetime(PickerFrom1.SelectedDate) != null)
                {
                    if (overTotalHours < Status.Holiday_MinOverTime)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您此次OT不足4小时，OT单无效。');", true);
                        return;
                    }
                }
                else
                {
                    if (overTotalHours < Status.WorkingDays_OverTime1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您此次OT不足3小时，OT单无效。');", true);
                        return;
                    }
                }
                */

                SingleOvertimeInfo overtimeinfo = new SingleOvertimeInfo();
                overtimeinfo.Approvestate = Status.OverTimeState_NotSubmit;
                overtimeinfo = this.GetModelInfo(overtimeinfo);
                int id = overtimeManager.Add(overtimeinfo);

                foreach (GridViewRow rw in this.grdMatterDetails.Rows)
                {
                    Label lblTimeStart = (Label)rw.FindControl("lblTimeStart");
                    Label lblTimeEnd = (Label)rw.FindControl("lblTimeEnd");
                    TextBox txtMatterDetails = (TextBox)rw.FindControl("txtMatterDetails");

                    MatterReasonInfo info = new MatterReasonInfo();
                    info.StartDate = Convert.ToDateTime(lblTimeStart.Text);
                    info.EndDate = Convert.ToDateTime(lblTimeEnd.Text);
                    info.Details = txtMatterDetails.Text.Trim();
                    info.SingleOverTimeID = id;

                    new MatterReasonManager().Add(info);
                }

                ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")保存了一条OT单信息编号(" + id + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('OT单保存成功。');", true);
        }

        /// <summary>
        /// 获得一个OT单对象
        /// </summary>
        /// <param name="state">1表示保存，2表示提交</param>
        public SingleOvertimeInfo GetModelInfo(SingleOvertimeInfo overtimeinfo)
        {
            overtimeinfo.UserID = int.Parse(hidUserid.Value);  // 申请人
            overtimeinfo.EmployeeName = txtUserName.Text;
            overtimeinfo.UserCode = txtUserCode.Text;
            overtimeinfo.AppTime = DateTime.Now;
            //overtimeinfo.OverTimeType = int.Parse(radType.SelectedValue);
            if (!string.IsNullOrEmpty(hidOverTimeProjectId.Value))
            {
                overtimeinfo.ProjectID = int.Parse(hidOverTimeProjectId.Value);
            }
            overtimeinfo.ProjectNo = txtOverTimeProjectNo.Text;
            overtimeinfo.OverTimeCause = txtDes.Text.Trim();
            overtimeinfo.BeginTime = PickerFrom1.SelectedDate;
            overtimeinfo.EndTime = PickerTo1.SelectedDate;
            overtimeinfo.OverTimeHours = decimal.Parse((overtimeinfo.EndTime - overtimeinfo.BeginTime).TotalHours.ToString());

            #region 计算OT
            UserAttBasicInfo userAttBasicInfo = userBasicManager.GetModelByUserid(UserID);
            // 总监级别（含以上）的人员不享受节假日OT调休制度
            if (userAttBasicInfo.AttendanceType == Status.UserBasicAttendanceType_Special)
            {
                overtimeinfo.Remaininghours = 0;
            }
            else
            {
                // 总监级别一下的人员节假日OT，可以享受节假日OT调休制度，节假日OT超过8个小时的按8小时计算调休小时数
                // 开始日期
                DateTime beginTime = overtimeinfo.BeginTime.Date;
                // 结束日期
                DateTime endTime = overtimeinfo.EndTime.Date;
                int remaininghours = 0;
                if (beginTime == endTime)
                {
                    if (attendanceManager.CheckIsHoliday(beginTime))
                    {
                        remaininghours = (int)(overtimeinfo.EndTime - overtimeinfo.BeginTime).TotalHours;
                        if (remaininghours > Status.Holiday_OverTime1)  // OT小时数大于8小时，调休按8小时计算。
                        {
                            remaininghours = Status.Holiday_OverTime1;
                        }
                        else if (remaininghours < Status.Holiday_MinOverTime) // OT小时数小于4小时，没有调休小时数。
                        {
                            remaininghours = 0;
                        }

                        if (beginTime.Date == new DateTime(2010, 9, 22)
                            || beginTime.Date == new DateTime(2010, 10, 1)
                            || beginTime.Date == new DateTime(2010, 10, 2)
                            || beginTime.Date == new DateTime(2010, 10, 3))
                        {
                            remaininghours = 0;
                        }
                    }
                }
                else
                {
                    while (beginTime <= endTime)
                    {
                        if (attendanceManager.CheckIsHoliday(beginTime))
                        {
                            DateTime calBeginTime = overtimeinfo.BeginTime;
                            DateTime calEndTime = overtimeinfo.EndTime;
                            if (beginTime == overtimeinfo.BeginTime.Date)
                            {
                                calBeginTime = overtimeinfo.BeginTime;
                            }
                            else if (beginTime > overtimeinfo.BeginTime.Date)
                            {
                                calBeginTime = DateTime.Parse(beginTime.ToString("yyyy-MM-dd ") + "00:00:00");
                            }

                            if (beginTime < endTime)
                            {
                                calEndTime = DateTime.Parse(beginTime.ToString("yyyy-MM-dd ") + "23:59:00");
                            }
                            else if (beginTime == endTime)
                            {
                                calEndTime = overtimeinfo.EndTime;
                            }
                            // 判断OT是否超过8小时，如果超过8小时按8小时计算
                            if (decimal.Parse((calEndTime - calBeginTime).TotalHours.ToString()) > Status.Holiday_OverTime1)
                            {
                                if (calBeginTime.Date == new DateTime(2010, 9, 22)
                                    || calBeginTime.Date == new DateTime(2010, 10, 1)
                                    || calBeginTime.Date == new DateTime(2010, 10, 2)
                                    || calBeginTime.Date == new DateTime(2010, 10, 3))
                                {
                                    remaininghours += 0;
                                }
                                else
                                {
                                    remaininghours += Status.Holiday_OverTime1;
                                }
                            }
                            else if (decimal.Parse((calEndTime - calBeginTime).TotalHours.ToString()) < Status.Holiday_MinOverTime)
                            {
                                remaininghours += 0;
                            }
                            else
                            {
                                if (calBeginTime.Date == new DateTime(2010, 9, 22)
                                    || calBeginTime.Date == new DateTime(2010, 10, 1)
                                    || calBeginTime.Date == new DateTime(2010, 10, 2)
                                    || calBeginTime.Date == new DateTime(2010, 10, 3))
                                {
                                    remaininghours += 0;
                                }
                                else
                                {
                                    remaininghours += (int)(calEndTime - calBeginTime).TotalHours;
                                }
                            }


                        }
                        beginTime = beginTime.AddDays(1);
                    }
                }
                // 计算OT的有效倒休时间
                overtimeinfo.Remaininghours = remaininghours;
            }
            #endregion

            ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
            ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(UserInfo.UserID);

            if (opearmodel != null)
            {
                overtimeinfo.ApproveID = opearmodel.TeamLeaderID;
                overtimeinfo.ApproveName = opearmodel.TeamLeaderName;
            }
            overtimeinfo.Deleted = false;
            overtimeinfo.CreateTime = DateTime.Now;
            overtimeinfo.UpdateTime = DateTime.Now;
            overtimeinfo.OperateorID = UserID;
            overtimeinfo.Sort = 0;
            return overtimeinfo;
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl);
        }

        //protected void PickerTo1_SelectionChanged(object sender, EventArgs e)
        //{
        //    AutoBindMatterDetailsItems();
        //}

        protected void btnTimeSheet_OnClick(object sender, EventArgs e)
        {
            AutoBindMatterDetailsItems();
        }


        private void AutoBindMatterDetailsItems()
        {
            if (this.PickerFrom1.SelectedDate != null && this.PickerTo1.SelectedDate != null && this.PickerTo1.SelectedDate > this.PickerFrom1.SelectedDate)
            {
                DateTime startDate = this.PickerFrom1.SelectedDate;
                DateTime toDate = this.PickerTo1.SelectedDate;

                IList<MatterReasonInfo> list = new List<MatterReasonInfo>();

                for (int i = 0; startDate.AddHours(i) < toDate; i++)
                {
                    MatterReasonInfo info = new MatterReasonInfo();
                    info.StartDate = startDate.AddHours(i);
                    if (startDate.AddHours(i + 1) > toDate)
                        info.EndDate = toDate;
                    else
                        info.EndDate = startDate.AddHours(i + 1);
                    list.Add(info);
                }
                this.grdMatterDetails.DataSource = list;
                this.grdMatterDetails.DataBind();
            }
        }

        protected void grdMatterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MatterReasonInfo info = (MatterReasonInfo)e.Row.DataItem;
            if (info != null)
            {
                Label lblTimeStart = (Label)e.Row.FindControl("lblTimeStart");
                Label lblTimeEnd = (Label)e.Row.FindControl("lblTimeEnd");
                TextBox txtMatterDetails = (TextBox)e.Row.FindControl("txtMatterDetails");

                lblTimeStart.Text = info.StartDate.ToString();
                lblTimeEnd.Text = info.EndDate.ToString();

                txtMatterDetails.Text = info.Details;
            }
        }
    }
}