using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using ESP.Framework.Entity;

namespace AdministrativeWeb.UserControls.Matter
{
    public partial class LeaveEdit : MatterUserControl
    {
        #region 变量定义
        /// <summary>
        /// 事由业务实现类
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
        /// 上下班时间信息业务对象
        /// </summary>
        private CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
                initTime();
            }
        }

        /// <summary>
        /// 初始化请假单页面
        /// </summary>
        private void initPage()
        {
            // 判断请假单ID是否存在
            if (!string.IsNullOrEmpty(Request["matterid"]))
            {
                MattersInfo model = matterManager.GetModel(int.Parse(Request["matterid"]));
                if (model != null)
                {
                    hidLeaveID.Value = model.ID.ToString();
                    if (model.MatterType == Status.MattersType_Leave || model.MatterType == Status.MattersType_Sick)
                    {
                        if (model.MatterType == Status.MattersType_Leave)
                            chkThing.Checked = true;
                        else
                            chkSick.Checked = true;
                        txtLeavePickerFrom2.Text = model.BeginTime.ToString("yyyy-MM-dd");
                        ddlHourFrom.SelectedValue = model.BeginTime.Hour.ToString();
                        ddlMinuteFrom.SelectedValue = model.BeginTime.Minute.ToString();

                        txtLeavePickerTo2.Text = model.EndTime.ToString("yyyy-MM-dd");
                        ddlHourTo.SelectedValue = model.EndTime.Hour.ToString();
                        ddlMinuteTo.SelectedValue = model.EndTime.Minute.ToString();

                        txtLeavePickerFrom3.Text = model.BeginTime.ToString("yyyy-MM-dd"); 
                        txtLeavePickerTo3.Text = model.EndTime.ToString("yyyy-MM-dd"); 
                    }
                    else if (model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last || model.MatterType == Status.MattersType_PrenatalCheck || model.MatterType == Status.MattersType_Incentive)
                    {
                        if (model.MatterType == Status.MattersType_Annual)
                            chkAnnual.Checked = true;
                        else if (model.MatterType == Status.MattersType_Annual_Last)
                            chkAnnualLast.Checked = true;
                        else if (model.MatterType == Status.MattersType_PrenatalCheck)
                            chkPrenatalCheck.Checked = true;
                        //if (model.MatterType == Status.MattersType_Incentive)
                        //    chkIncentive.Checked = true;

                        if (model.EndTime.Hour == 12)
                        {
                            model.EndTime = model.EndTime.AddHours(-1);
                        }
                        txtLeavePickerFrom2.Text = model.BeginTime.ToString("yyyy-MM-dd");
                        ddlHourFrom.SelectedValue = model.BeginTime.Hour.ToString();
                        ddlMinuteFrom.SelectedValue = model.BeginTime.Minute.ToString();

                        txtLeavePickerTo2.Text = model.EndTime.ToString("yyyy-MM-dd");
                        ddlHourTo.SelectedValue = model.EndTime.Hour.ToString();
                        ddlMinuteTo.SelectedValue = model.EndTime.Minute.ToString();

                        txtLeavePickerFrom3.Text = model.BeginTime.ToString("yyyy-MM-dd");
                        txtLeavePickerTo3.Text = model.EndTime.ToString("yyyy-MM-dd"); 
                    }
                    else if (model.MatterType == Status.MattersType_Bereavement
                        || model.MatterType == Status.MattersType_Marriage
                        || model.MatterType == Status.MattersType_Maternity
                        || model.MatterType == Status.MattersType_PeiChanJia
                        )
                    {
                        if (model.MatterType == Status.MattersType_Bereavement)
                            chkBereavement.Checked = true;
                        else if (model.MatterType == Status.MattersType_Marriage)
                            chkMarriage.Checked = true;
                        else if (model.MatterType == Status.MattersType_Maternity)
                            chkMaternity.Checked = true;
                        else if (model.MatterType == Status.MattersType_PeiChanJia)
                            chkPeiChanJia.Checked = true;

                        txtLeavePickerFrom2.Text = model.BeginTime.ToString("yyyy-MM-dd");
                        ddlHourFrom.SelectedValue = model.BeginTime.Hour.ToString();
                        ddlMinuteFrom.SelectedValue = model.BeginTime.Minute.ToString();

                        txtLeavePickerTo2.Text = model.EndTime.ToString("yyyy-MM-dd");
                        ddlHourTo.SelectedValue = model.EndTime.Hour.ToString();
                        ddlMinuteTo.SelectedValue = model.EndTime.Minute.ToString();

                        txtLeavePickerFrom3.Text = model.BeginTime.ToString("yyyy-MM-dd"); 
                        txtLeavePickerTo3.Text = model.EndTime.ToString("yyyy-MM-dd"); 
                    }
                    else
                    {
                        chkSick.Checked = true;

                        txtLeavePickerFrom2.Text = DateTime.Parse(_selectDateTime).ToString("yyyy-MM-dd");

                        txtLeavePickerTo2.Text = DateTime.Parse(_selectDateTime).ToString("yyyy-MM-dd");

                        txtLeavePickerFrom3.Text = DateTime.Parse(_selectDateTime).ToString("yyyy-MM-dd");
                        txtLeavePickerTo3.Text = DateTime.Parse(_selectDateTime).ToString("yyyy-MM-dd");
                        return;
                    }
                    txtLeaveCause.Text = model.MatterContent;
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
                }
            }
            else   // 如果请假单ID不存在就显示默认的页面信息
            {
                DateTime selectDateTime = DateTime.Parse(_selectDateTime);
                // 获得用户的上下班时间信息集合
                List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserID);
                // 用户的上下班时间信息
                CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, selectDateTime);

                hidLeaveUserid.Value = UserID.ToString();
                txtLeaveUserName.Text = UserInfo.FullNameCN;
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                txtLeaveUserCode.Text = emp.Code;
                IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                    ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                    txtLeaveGroup.Text = emppos.DepartmentName;
                    int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                    txtLeaveTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                }

                chkSick.Checked = true;

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

                    txtLeavePickerFrom2.Text = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).ToString("yyyy-MM-dd");
                    ddlHourFrom.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).Hour.ToString();
                    ddlMinuteFrom.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).Minute.ToString();

                    DateTime beginSelDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    while (beginSelDate < goWorkTime)
                    {
                        beginSelDate = beginSelDate.AddHours(1);
                    }
                    txtLeavePickerTo2.Text = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).ToString("yyyy-MM-dd");
                    ddlHourTo.SelectedValue = beginSelDate.Hour.ToString();
                    ddlMinuteTo.SelectedValue = beginSelDate.Minute.ToString();

                    txtLeavePickerFrom3.Text = selectDateTime.ToString("yyyy-MM-dd");
                    txtLeavePickerTo3.Text = selectDateTime.ToString("yyyy-MM-dd");
                }
                else if (isLeaveEarly)
                {
                    txtLeavePickerTo2.Text = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay).ToString("yyyy-MM-dd"); ;
                    ddlHourTo.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).Hour.ToString();
                    ddlMinuteTo.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).Minute.ToString();

                    DateTime endSelDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    while (endSelDate > offWorkTime)
                    {
                        endSelDate = endSelDate.AddHours(-1);
                    }
                    txtLeavePickerFrom2.Text = endSelDate.ToString("yyyy-MM-dd");
                    ddlHourFrom.SelectedValue = endSelDate.Hour.ToString();
                    ddlMinuteFrom.SelectedValue = endSelDate.Minute.ToString();

                    txtLeavePickerFrom3.Text = selectDateTime.ToString("yyyy-MM-dd");
                    txtLeavePickerTo3.Text = selectDateTime.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtLeavePickerFrom2.Text = selectDateTime.ToString("yyyy-MM-dd");
                    ddlHourFrom.SelectedValue = selectDateTime.Hour.ToString();
                    ddlMinuteFrom.SelectedValue = selectDateTime.Minute.ToString();

                    txtLeavePickerTo2.Text = selectDateTime.ToString("yyyy-MM-dd");
                    ddlHourTo.SelectedValue = selectDateTime.Hour.ToString();
                    ddlMinuteTo.SelectedValue = selectDateTime.Minute.ToString();

                    txtLeavePickerFrom3.Text = selectDateTime.ToString("yyyy-MM-dd");
                    txtLeavePickerTo3.Text = selectDateTime.ToString("yyyy-MM-dd");
                }
            }
        }

        private void initTime()
        { 
            ddlHourFrom.Items.Add(new ListItem("09","09"));
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
        /// 保存请假单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLeaveSave_Click(object sender, EventArgs e)
        {
            int leaveid = 0;
            if (!string.IsNullOrEmpty(hidLeaveID.Value))
            {
                leaveid = int.Parse(hidLeaveID.Value);
            }

            if (string.IsNullOrEmpty(leaveid.ToString()))
            {
                // 添加考勤事由系统自动将用户选择的日期带到要添加的用户信息页面中
                DateTime selectDateTime = DateTime.Now;
                if (chkSick.Checked || chkThing.Checked || chkAnnual.Checked || chkAnnualLast.Checked || chkPrenatalCheck.Checked)
                {
                    selectDateTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                }
                else if (chkMarriage.Checked || chkMaternity.Checked || chkBereavement.Checked || chkPeiChanJia.Checked)
                {
                    selectDateTime = DateTime.Parse( txtLeavePickerFrom3.Text);
                }

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

            MattersInfo model = GetLeaveModel(leaveid.ToString());

            // 判断时间段，不允许事后申请
            //if (model.BeginTime <= DateTime.Now)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('目前系统暂不支持事后申请，请联系HR。');", true);
            //    return;
            //}

            if (model != null)
            {
                if (CheckIsOverLap(model))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('此事由和之前申请的事由存在时间上的重叠，请确认后重新申请！');", true);
                    return;
                }
                model.MatterState = Status.MattersState_NoSubmit;
                SaveModel(model);
            }
            else
                return;
        }

        /// <summary>
        /// 提交请假单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLeaveSubmit_Click(object sender, EventArgs e)
        {
            int leaveid = 0;
            if (!string.IsNullOrEmpty(hidLeaveID.Value))
            {
                leaveid = int.Parse(hidLeaveID.Value);
            }

            MattersInfo model = GetLeaveModel(leaveid.ToString());

            if (model != null)
            {
               
                // 添加考勤事由系统自动将用户选择的日期带到要添加的用户信息页面中
                DateTime selectDateTime = DateTime.Now;
                if (chkSick.Checked)
                {
                    selectDateTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                }
                else if (chkThing.Checked )
                {
                    ESP.HumanResource.Entity.EmployeeBaseInfo employeeBaseModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(UserID);
                    ESP.Administrative.Entity.ALAndRLInfo annual = (new ESP.Administrative.BusinessLogic.ALAndRLManager()).GetALAndRLModel(UserID, model.BeginTime.Year, 1);
                    ESP.Administrative.Entity.ALAndRLInfo reward = (new ESP.Administrative.BusinessLogic.ALAndRLManager()).GetALAndRLModel(UserID, model.BeginTime.Year, 2);

                    if ((annual != null && annual.RemainingNumber != 0) || (reward != null && reward.RemainingNumber != 0))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('请先将年假使用完毕，再申请事假。');", true);
                        return;
                    }

                    selectDateTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                }
                else if (chkAnnual.Checked || chkAnnualLast.Checked)
                {
                    selectDateTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                }
                else if (chkPrenatalCheck.Checked)
                {
                    selectDateTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                }
                else if (chkMarriage.Checked || chkMaternity.Checked || chkBereavement.Checked || chkPeiChanJia.Checked)
                {
                    selectDateTime = DateTime.Parse( txtLeavePickerFrom3.Text);
                }

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
                // 判断该考勤事由是否允许直接提交，不允许就直接提交
                if (!IsCanSubmitMatters(leaveid.ToString()))
                {
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



                if (CheckIsOverLap(model))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('此事由和之前申请的事由存在时间上的重叠，请确认后重新申请！');", true);
                    return;
                }
                SubmitModel(model);
            }
            else
                return;
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLeaveBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl);
        }

        /// <summary>
        /// 保存请假单信息
        /// </summary>
        /// <param name="model">请假单实例类</param>
        private void SaveModel(MattersInfo model)
        {
            try
            {
                if (model.ID > 0)
                {
                    matterManager.Update(model);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")更新了一条请假单信息，编号(" + model.ID + ")",
                        "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('请假单保存成功');", true);
                }
                else
                {
                    int modelId = matterManager.Add(model);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")新增了一条请假单信息，编号(" + modelId + ")",
                        "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('请假单保存成功');", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('请假单保存失败！');", true);
            }
        }

        /// <summary>
        /// 提交请假单信息，并等待审批
        /// </summary>
        /// <param name="model"></param>
        private void SubmitModel(MattersInfo model)
        {
            try
            {

                ApproveLogInfo approve = new ApproveLogInfo();

                var monthlist = new MattersManager().GetModelListByMonth(model.UserID, model.BeginTime.Year, model.BeginTime.Month);
                var anuallist = monthlist.Where(x => x.MatterType == model.MatterType && (x.MatterState == Status.MattersState_Passed || x.MatterState == Status.MattersState_WaitHR || x.MatterState == Status.MattersState_WaitDirector));

                if (model.MatterType == Status.MattersType_Bereavement
                    || model.MatterType == Status.MattersType_Marriage
                    || model.MatterType == Status.MattersType_Maternity
                    || model.MatterType == Status.MattersType_PrenatalCheck
                    || model.MatterType == Status.MattersType_PeiChanJia)
                {
                    approve = GetLeaveApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Leave);
                    model.MatterState = Status.MattersState_WaitHR;
                }
                else if (model.MatterType == Status.MattersType_Sick && (anuallist.Sum(x => x.TotalHours)+model.TotalHours) >= 16)
                {
                    approve = GetLeaveApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Leave);
                    model.MatterState = Status.MattersState_WaitHR;
                }
                else if (model.MatterType == Status.MattersType_Leave && (anuallist.Sum(x => x.TotalHours) + model.TotalHours) >= 16)
                {
                    approve = GetLeaveApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Leave);
                    model.MatterState = Status.MattersState_WaitHR;
                }
                //else if ((model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last
                //    || model.MatterType == Status.MattersType_Leave) && (anuallist.Sum(x => x.TotalHours) + model.TotalHours) >= 16)
                //{
                //    approve = GetApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Leave);
                //    model.MatterState = Status.MattersState_WaitDirector;
                //}
                else
                {
                    approve = GetApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Leave);
                    model.MatterState = Status.MattersState_WaitDirector;
                }

                //团队总经理不需提交考勤
                string managerids = System.Configuration.ConfigurationManager.AppSettings["MatterManagers"];

                if (managerids.IndexOf("," + model.UserID + ",") >= 0)
                {
                    model.MatterState = Status.MattersState_Passed;
                    approve = null;
                }
                //年假计算
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
                    if (model.MatterState != Status.MattersState_Passed)
                    {
                        if (approve != null && approve.ApproveID != 0)
                        {
                            string email = new ESP.Compatible.Employee(approve.ApproveID).EMail;
                            if (!string.IsNullOrEmpty(email))
                            {
                                string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + model.ID + "&flag=1";
                                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                body += "<br><br>" + UserInfo.FullNameCN + "提交的请假单等待您的审批";
                                string pageurl = "Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "/" + string.Format(pageurl, approve.ApproveType, approve.ID) + "'>"
                                    + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                MailAddress[] recipientAddress = { new MailAddress(email) };
                                ESP.Mail.MailManager.Send("考勤事由审批", body, true, null, recipientAddress, null, null, null);
                            }
                            if (model.MatterType == Status.MattersType_Sick && model.TotalHours >= Status.WorkingHours * 2)
                            {
                                OperationAuditManageManager operationAuditManager = new OperationAuditManageManager();
                                ESP.Administrative.Entity.OperationAuditManageInfo operationauditModel = operationAuditManager.GetOperationAuditModelByUserID(UserID);
                                string hrEmail = new ESP.Compatible.Employee(operationauditModel.HRAdminID).EMail;
                                if (!string.IsNullOrEmpty(hrEmail))
                                {
                                    string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + model.ID + "&flag=1";
                                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                    body += "<br><br>" + UserInfo.FullNameCN + "申请了两天以上的病假请确认";

                                    MailAddress[] recipientAddress = { new MailAddress(email) };
                                    ESP.Mail.MailManager.Send(UserInfo.FullNameCN + "申请了两天以上的病假请确认", body, true, null, recipientAddress, null, null, null);
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
                if (model.MatterState != Status.MattersState_Passed)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('请假单重新申请成功，等待“" + approve.ApproveName + "”的审批');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('请假单重新申请成功!');", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('请假单提交失败！"+ex.ToString()+"');", true);
            }
        }

        /// <summary>
        /// 获得时间信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private DateTime GetTime(DateTime dt, string str)
        {
            DateTime date = new DateTime();
            if ("star" == str)
            {
                if (dt.Hour < 13)
                {
                    date = DateTime.Parse(dt.ToString("yyyy-MM-dd ") + "09:00:00");
                }
                else
                {
                    date = DateTime.Parse(dt.ToString("yyyy-MM-dd ") + "13:00:00");
                }
            }
            else if ("end" == str)
            {
                if (dt.Hour < 13)
                {
                    date = DateTime.Parse(dt.ToString("yyyy-MM-dd ") + "12:00:00");
                }
                else
                {
                    date = DateTime.Parse(dt.ToString("yyyy-MM-dd ") + "18:00:00");
                }
            }
            return date;
        }

        /// <summary>
        /// 获得一个请假单的实例对象
        /// </summary>
        /// <param name="leaveID">请假单编号</param>
        /// <returns>返回一个请假单的实例对象</returns>
        private MattersInfo GetLeaveModel(string leaveID)
        {
            // 获得用户的上下班时间信息集合
            List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserID);

            // 获得一个事由对象
            MattersInfo model = new MattersInfo();
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
            if (model == null)
            {
                model = new MattersInfo();
                model.CreateTime = DateTime.Now;
                model.UserID = UserInfo.UserID;
            }
            if (chkSick.Checked)
            {
                model.MatterType = Status.MattersType_Sick;
                DateTime beginTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                DateTime endTime = DateTime.Parse(txtLeavePickerTo2.Text + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue);
                model.BeginTime = beginTime;
                model.EndTime = endTime;
                double timeRange = attMan.TimeRangeAnnual(ref beginTime, ref endTime, commuterTimeList);
                model.TotalHours = (decimal)timeRange;
            }
            else if (chkThing.Checked)
            {
                model.MatterType = Status.MattersType_Leave;
                DateTime beginTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                DateTime endTime = DateTime.Parse(txtLeavePickerTo2.Text + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue);

                // 计算申请事假的小时数，判断大于半天的时间请申请年假
                double thingHours = (model.EndTime - model.BeginTime).TotalHours;
                double remainingAnnDays = attMan.GetRemainingAnnualDays(UserID, model.BeginTime.Year);
                if (thingHours != 0 && thingHours > 4 && (remainingAnnDays * 8) > thingHours)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您还有剩余年假，请先申请年假。');", true);
                    return null;
                }
                double timeRange = attMan.TimeRangeAnnual(ref beginTime, ref  endTime, commuterTimeList);
                model.BeginTime = beginTime;
                model.EndTime = endTime;
                model.TotalHours = (decimal)timeRange;
            }
            else if (chkAnnual.Checked)
            {
                // 获得开始和结束时间
                DateTime beginTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                DateTime endTime = DateTime.Parse(txtLeavePickerTo2.Text + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue);
                // 计算用户的开始和结束时间，判断是半天还是全天，并返回用户的开始时间和结束时间
                double timeRange = attMan.TimeRangeAnnual(ref beginTime, ref endTime, commuterTimeList);
                double remainAnnual = attMan.GetRemainingAnnualDays(UserID, beginTime.Year);
                double remainAward = 0;
                double totalRemain = 0;

                attMan.GetAwardAnnualDays(UserID, beginTime.Year, out remainAward);

                totalRemain = remainAnnual + remainAward;

                if (model.ID != 0)
                {
                    totalRemain += (double)(model.TotalHours / Status.WorkingHours);
                }
                if ((timeRange / Status.WorkingHours) > totalRemain)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您剩余的年假不足，请确认后在申请！');", true);
                    return null;
                }
                model.MatterType = Status.MattersType_Annual;
                model.BeginTime = beginTime;
                model.EndTime = endTime;
                model.TotalHours = (decimal)timeRange;
                model.AnnualHours = (decimal)timeRange;
                model.AwardHours = 0;

                if (remainAnnual * Status.WorkingHours < timeRange)
                {
                    model.AnnualHours = ((decimal)remainAnnual) * Status.WorkingHours;
                    model.AwardHours = (decimal)timeRange - ((decimal)remainAnnual) * Status.WorkingHours;
                }
            }
            else if (chkAnnualLast.Checked)
            {
                // 获得开始和结束时间
                DateTime beginTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                DateTime endTime = DateTime.Parse(txtLeavePickerTo2.Text + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue);
                // 计算用户的开始和结束时间，判断是半天还是全天，并返回用户的开始时间和结束时间
                double timeRange = attMan.TimeRangeAnnual(ref beginTime, ref endTime, commuterTimeList);
                double remainAnnual = attMan.GetRemainingAnnualDays(UserID, beginTime.Year-1);
                double remainAward = 0;
                double totalRemain = 0;

                attMan.GetAwardAnnualDays(UserID, beginTime.Year-1, out remainAward);

                totalRemain = remainAnnual + remainAward;

                if (model.ID != 0)
                {
                    totalRemain += (double)(model.TotalHours / Status.WorkingHours);
                }
                if ((timeRange / Status.WorkingHours) > totalRemain)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('您去年剩余的年假不足，请确认后在申请！');", true);
                    return null;
                }
                model.MatterType = Status.MattersType_Annual_Last;
                model.BeginTime = beginTime;
                model.EndTime = endTime;
                model.TotalHours = (decimal)timeRange;
                model.AnnualHours = (decimal)timeRange;
                model.AwardHours = 0;

                if (remainAnnual * Status.WorkingHours < timeRange)
                {
                    model.AnnualHours = ((decimal)remainAnnual) * Status.WorkingHours;
                    model.AwardHours = (decimal)timeRange - ((decimal)remainAnnual) * Status.WorkingHours;
                }
            }
            else if (chkPrenatalCheck.Checked)
            {
                // 获得开始和结束时间
                DateTime beginTime = DateTime.Parse(txtLeavePickerFrom2.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
                DateTime endTime = DateTime.Parse(txtLeavePickerTo2.Text + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue);
                // 计算用户的开始和结束时间，判断是半天还是全天，并返回用户的开始时间和结束时间
                double timeRange = attMan.TimeRangeAnnual(ref beginTime, ref endTime, commuterTimeList);
                model.MatterType = Status.MattersType_PrenatalCheck;
                model.BeginTime = beginTime;
                model.EndTime = endTime;
                model.TotalHours = (decimal)timeRange;
            }
            else if (chkMarriage.Checked || chkMaternity.Checked || chkBereavement.Checked || chkPeiChanJia.Checked)
            {
                if (chkMarriage.Checked)
                {
                    model.MatterType = Status.MattersType_Marriage;
                }
                else if (chkMaternity.Checked)
                    model.MatterType = Status.MattersType_Maternity;
                else if (chkBereavement.Checked)
                    model.MatterType = Status.MattersType_Bereavement;
                else if (chkPeiChanJia.Checked)
                    model.MatterType = Status.MattersType_PeiChanJia;

                CommuterTimeInfo commuterTimeBeginTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, DateTime.Parse( txtLeavePickerFrom3.Text));
                CommuterTimeInfo commuterTimeEndTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, DateTime.Parse( txtLeavePickerTo3.Text));
                // 获得开始和结束时间
                DateTime beginTime = DateTime.Parse( txtLeavePickerFrom3.Text).Date.Add(commuterTimeBeginTimeModel.GoWorkTime.TimeOfDay);
                DateTime endTime = DateTime.Parse( txtLeavePickerTo3.Text).Date.Add(commuterTimeEndTimeModel.OffWorkTime.TimeOfDay);
                decimal totalHours = ((endTime - beginTime).Days + 1) * Status.WorkingHours;

                model.BeginTime = beginTime;
                model.EndTime = endTime;
                model.TotalHours = totalHours;

                if (chkMarriage.Checked)
                {
                    var marriageList = matterManager.GetMattersList(UserInfo.UserID, Status.MattersType_Marriage);
                    var totalMarriageHours = marriageList.Sum(x=>x.TotalHours);
                    if (int.Parse(leaveID) == 0)
                        totalMarriageHours = totalMarriageHours + model.TotalHours;

                    if (totalMarriageHours>120)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('婚假最多可申请15天！');", true);
                        return null;
                    }
                }
            }

            model.MatterContent = txtLeaveCause.Text.Trim();
            model.OperateorID = UserInfo.UserID;
            model.Deleted = false;
            model.UpdateTime = DateTime.Now;
            return model;
        }

        /// <summary>
        /// 判断事由的时间时候出现了重叠的情况
        /// </summary>
        /// <returns>如果出现了重叠的情况返回true，否则返回false</returns>
        public bool CheckIsOverLap(MattersInfo mattersModel)
        {
            if (chkSick.Checked || chkThing.Checked)
            {
                bool b = matterManager.CheckIsOverLap(UserID, mattersModel.BeginTime, mattersModel.EndTime, mattersModel.ID);
                return b;
            }
            else if (chkAnnual.Checked || chkAnnualLast.Checked || chkMarriage.Checked || chkMaternity.Checked || chkBereavement.Checked || chkPrenatalCheck.Checked || chkPeiChanJia.Checked)
            {
                bool b = matterManager.CheckIsOverLap(UserID, mattersModel.BeginTime, mattersModel.EndTime, mattersModel.ID);
                return b;
            }
            return false;
        }

        /// <summary>
        /// 获得一个审批记录对象
        /// </summary>
        /// <param name="id">审批记录ID</param>
        /// <param name="singelType">单据类型</param>
        /// <returns>审批记录对象</returns>
        protected ApproveLogInfo GetLeaveApproveLogModel(string id, int singelType)
        {
            ApproveLogInfo model = new ApproveLogInfo();
            if (!string.IsNullOrEmpty(id))
            {
                model = new ApproveLogManager().GetModel(int.Parse(id));
            }
            else
            {
                ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(UserInfo.UserID);
                if (opearmodel != null)
                {
                    model.ApproveID = opearmodel.HRAdminID;
                    model.ApproveName = opearmodel.HRAdminName;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('没有发现设置的审批人，请与IT部联系');", true);
                    return null;
                }
            }
            model.ApproveState = 0;
            model.ApproveType = singelType;
            model.ApproveUpUserID = 0;
            model.CreateTime = DateTime.Now;
            model.Deleted = false;
            model.IsLastApprove = 0;
            model.OperateorID = UserInfo.UserID;
            model.Sort = 0;
            model.UpdateTime = DateTime.Now;
            return model;
        }
    }
}
