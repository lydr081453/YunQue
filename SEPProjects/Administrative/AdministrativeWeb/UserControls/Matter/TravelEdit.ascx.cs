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
    public partial class TravelEdit : MatterUserControl
    {
        #region 成员变量
        /// <summary>
        /// 考勤事由业务类对象
        /// </summary>
        private MattersManager matterManager = new MattersManager();
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
        /// 上下班时间业务对象类
        /// </summary>
        private CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
                initTime();
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
                    if (model.MatterType == Status.MattersType_Travel)
                    {
                        ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);

                        hidTravelUserid.Value = model.UserID.ToString();
                        txtTravelUserName.Text = userinfoModel.FullNameCN;
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                        txtTravelUserCode.Text = emp.Code;
                        IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                            ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                        if (list != null && list.Count > 0)
                        {
                            ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                            txtTravelGroup.Text = emppos.DepartmentName;
                            int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                            txtTravelTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                        }
                        labTravelAppTime.Text = model.CreateTime.ToString("yyyy年MM月dd日 HH时");

                        txtTravelPickerFrom1.Text = model.BeginTime.ToString("yyyy-MM-dd");
                        ddlHourFrom.SelectedValue = model.BeginTime.Hour.ToString();
                        ddlMinuteFrom.SelectedValue = model.BeginTime.Minute.ToString();

                        txtTravelPickerTo1.Text = model.EndTime.ToString("yyyy-MM-dd");
                        ddlHourTo.SelectedValue = model.EndTime.Hour.ToString();
                        ddlMinuteTo.SelectedValue = model.EndTime.Minute.ToString();

                        txtTravelDes.Text = model.MatterContent;
                        txtTravelProjectNo.Text = model.ProjectNo;
                        hidTravelProjectId.Value = model.ProjectID.ToString();
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

                hidTravelUserid.Value = UserID.ToString();
                txtTravelUserName.Text = UserInfo.FullNameCN;
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                txtTravelUserCode.Text = emp.Code;
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                    txtTravelGroup.Text = emppos.DepartmentName;
                    int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                    txtTravelTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                }
                labTravelAppTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时");

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
                    txtTravelPickerFrom1.Text = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).ToString("yyyy-MM-dd");
                    ddlHourFrom.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).Hour.ToString();
                    ddlMinuteFrom.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).Minute.ToString();

                    DateTime beginSelDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    while (beginSelDate < goWorkTime)
                    {
                        beginSelDate = beginSelDate.AddHours(1);
                    }
                    txtTravelPickerTo1.Text = beginSelDate.ToString("yyyy-MM-dd");
                    ddlHourTo.SelectedValue = beginSelDate.Hour.ToString();
                    ddlMinuteTo.SelectedValue = beginSelDate.Minute.ToString();
                }
                else if (isLeaveEarly)
                {
                    txtTravelPickerTo1.Text = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay).ToString("yyyy-MM-dd");
                    DateTime endSelDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    while (endSelDate > offWorkTime)
                    {
                        endSelDate = endSelDate.AddHours(-1);
                    }
                    txtTravelPickerFrom1.Text = endSelDate.ToString("yyyy-MM-dd");
                    ddlHourFrom.SelectedValue = endSelDate.Hour.ToString();
                    ddlMinuteFrom.SelectedValue = endSelDate.Minute.ToString();
                }
                else
                {
                    txtTravelPickerFrom1.Text = selectDateTime.ToString("yyyy-MM-dd");
                    ddlHourFrom.SelectedValue = selectDateTime.Hour.ToString();
                    ddlMinuteFrom.SelectedValue = selectDateTime.Minute.ToString();

                    txtTravelPickerTo1.Text = selectDateTime.ToString("yyyy-MM-dd");
                    ddlHourTo.SelectedValue = selectDateTime.Hour.ToString();
                    ddlMinuteTo.SelectedValue = selectDateTime.Minute.ToString();
                }
            }
        }


        private void initTime()
        {
            ddlHourFrom.Items.Add(new ListItem("09", "09"));
            ddlHourFrom.Items.Add(new ListItem("10", "10"));
            ddlHourFrom.Items.Add(new ListItem("11", "11"));
            ddlHourFrom.Items.Add(new ListItem("12", "12"));
            ddlHourFrom.Items.Add(new ListItem("13", "13"));
            ddlHourFrom.Items.Add(new ListItem("14", "14"));
            ddlHourFrom.Items.Add(new ListItem("15", "15"));
            ddlHourFrom.Items.Add(new ListItem("16", "16"));
            ddlHourFrom.Items.Add(new ListItem("17", "17"));
            ddlHourFrom.Items.Add(new ListItem("18", "18"));
            ddlHourFrom.Items.Add(new ListItem("19", "19"));
            ddlHourFrom.SelectedValue = "09";

            ddlMinuteFrom.Items.Add(new ListItem("00", "00"));
            ddlMinuteFrom.Items.Add(new ListItem("30", "30"));
            ddlMinuteFrom.SelectedValue = "30";

            ddlHourTo.Items.Add(new ListItem("09", "09"));
            ddlHourTo.Items.Add(new ListItem("10", "10"));
            ddlHourTo.Items.Add(new ListItem("11", "11"));
            ddlHourTo.Items.Add(new ListItem("12", "12"));
            ddlHourTo.Items.Add(new ListItem("13", "13"));
            ddlHourTo.Items.Add(new ListItem("14", "14"));
            ddlHourTo.Items.Add(new ListItem("15", "15"));
            ddlHourTo.Items.Add(new ListItem("16", "16"));
            ddlHourTo.Items.Add(new ListItem("17", "17"));
            ddlHourTo.Items.Add(new ListItem("18", "18"));
            ddlHourTo.Items.Add(new ListItem("19", "19"));
            ddlHourTo.SelectedValue = "18";

            ddlMinuteTo.Items.Add(new ListItem("00", "00"));
            ddlMinuteTo.Items.Add(new ListItem("30", "30"));
            ddlMinuteTo.SelectedValue = "30";

        }


        /// <summary>
        /// 保存出差记录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTravelSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["matterid"]))
            {
                DateTime selectDateTime = DateTime.Parse(txtTravelPickerFrom1.Text);

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
            //if (CheckIsOverLap(Request["matterid"]))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('此事由和之前申请的事由存在时间上的重叠，请确认后重新申请！');", true);
            //    return;
            //}
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
                    matterManager.Update(model);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")保存了一条出差单信息编号(" + model.ID + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('出差单保存成功。');", true);
                }
                else
                {
                    int id = matterManager.Add(model);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")保存了一条出差单信息编号(" + id + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('出差单保存成功。');", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('出差单保存失败。');", true);
            }
        }

        /// <summary>
        /// 提交出差单记录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTravelSubmit_Click(object sender, EventArgs e)
        {
            // 判断该考勤事由是否允许直接提交，不允许就直接提交
            if (!IsCanSubmitMatters(Request["matterid"]))
            {
                DateTime selectDateTime =DateTime.Parse( txtTravelPickerFrom1.Text);

                  // 判断时间段，不允许事后申请
                //if (selectDateTime < new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day))
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
                //holiday 

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

            //if (CheckIsOverLap(Request["matterid"]))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('此事由和之前申请的事由存在时间上的重叠，请确认后重新申请！');", true);
            //    return;
            //}
            MattersInfo model = GetMatterModel(Request["matterid"]);

            // 判断时间段，不允许事后申请
            //if (model.BeginTime <= DateTime.Now)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('目前系统暂不支持事后申请，请联系HR。');", true);
            //    return;
            //}

            //DateTime checkdate = model.BeginTime;
            //while (checkdate <= model.EndTime)
            //{
            //    HolidaysInfo holi = (new HolidaysInfoManager()).GetHolideysInfoByDatetime(checkdate);
            //    if (holi != null)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('区间内含节假日,不能填写出差申请单.');", true);
            //        return;
            //    }
            //    else
            //       checkdate= checkdate.AddDays(1);
            //}
            model.MatterState = Status.MattersState_WaitDirector;

            //团队总经理不需提交考勤
            string managerids = System.Configuration.ConfigurationManager.AppSettings["MatterManagers"];
            ApproveLogInfo approve = GetApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Travel);

            if (managerids.IndexOf("," + model.UserID + ",") >= 0)
            {
                model.MatterState = Status.MattersState_Passed;
                approve = null;
            }

            try
            {
                if (model.ID > 0)
                {

                    matterManager.Update(model, approve);
                }
                else
                {
                    matterManager.Add(model, approve);
                }

                //发邮件
                try
                {
                    if (approve != null)
                    {
                        string email = new ESP.Compatible.Employee(approve.ApproveID).EMail;
                        if (!string.IsNullOrEmpty(email))
                        {
                            string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + model.ID + "&flag=1";
                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                            body += "<br><br>" + UserInfo.FullNameCN + "提交的出差单等待您的审批";
                            string pageurl = "Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                            body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "/" + string.Format(pageurl, approve.ApproveType, approve.ID) + "'>"
                                + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                            MailAddress[] recipientAddress = { new MailAddress(email) };
                            ESP.Mail.MailManager.Send("考勤事由等待审批", body, true, null, recipientAddress, null, null, null);
                        }
                    }
                }
                catch
                {
                }

                if (approve != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('出差单提交成功，等待“" + approve.ApproveName + "”的审批');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('出差单提交成功!');", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('出差单提交失败！');", true);
            }
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTravelBack_Click(object sender, EventArgs e)
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
            DateTime beginTime = DateTime.Parse(txtTravelPickerFrom1.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
            DateTime endTime = DateTime.Parse(txtTravelPickerTo1.Text + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue);

            model.MatterType = Status.MattersType_Travel;
            model.OperateorID = UserInfo.UserID;
            model.Deleted = false;
            model.UpdateTime = DateTime.Now;
            model.ProjectID = int.Parse(hidTravelProjectId.Value);
            model.ProjectNo = txtTravelProjectNo.Text;
            model.MatterContent = txtTravelDes.Text.Trim();

            double timeRange = attMan.TimeRangeAnnual(ref beginTime, ref endTime, commuterTimeList);
            model.TotalHours = (decimal)timeRange;
            model.BeginTime = beginTime;
            model.EndTime = endTime;

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

            return matterManager.CheckIsOverLap(UserID, DateTime.Parse( txtTravelPickerFrom1.Text), DateTime.Parse( txtTravelPickerTo1.Text), modelid);
        }
    }
}