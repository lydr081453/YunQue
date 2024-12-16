using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;

namespace AdministrativeWeb.UserControls.Matter
{
    public partial class OutEdit : MatterUserControl
    {
        #region private 成员变量
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
        /// 上下班时间信息业务对象类
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
                    if (model.MatterType == Status.MattersType_Out)
                    {
                        ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);

                        hidOutUserid.Value = model.UserID.ToString();
                        txtOutUserName.Text = userinfoModel.FullNameCN;
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                        txtOutUserCode.Text = emp.Code;
                        IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                            ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                        if (list != null && list.Count > 0)
                        {
                            ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                            txtOutGroup.Text = emppos.DepartmentName;
                            int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                            txtOutTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                        }
                        labOutAppTime.Text = model.CreateTime.ToString("yyyy年MM月dd日 HH时");
                        txtOutPickerFrom1.Text = model.BeginTime.ToString("yyyy-MM-dd");
                        ddlHourFrom.SelectedValue = model.BeginTime.Hour.ToString();
                        ddlMinuteFrom.SelectedValue = model.BeginTime.Minute.ToString();

                        txtOutPickerTo1.Text = model.EndTime.ToString("yyyy-MM-dd");
                        ddlHourTo.SelectedValue = model.EndTime.Hour.ToString();
                        ddlMinuteTo.SelectedValue = model.EndTime.Minute.ToString();

                        txtOutCause.Text = model.MatterContent;
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

                hidOutUserid.Value = UserID.ToString();
                txtOutUserName.Text = UserInfo.FullNameCN;
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                txtOutUserCode.Text = emp.Code;
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                    txtOutGroup.Text = emppos.DepartmentName;
                    int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                    txtOutTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                }
                labOutAppTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时");

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
                    txtOutPickerFrom1.Text = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).ToString("yyyy-MM-dd");
                    ddlHourFrom.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).Hour.ToString();
                    ddlMinuteFrom.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).Minute.ToString();

                    DateTime beginSelDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    while (beginSelDate < goWorkTime)
                    {
                        beginSelDate = beginSelDate.AddHours(1);
                    }
                    txtOutPickerTo1.Text = beginSelDate.ToString("yyyy-MM-dd");
                    ddlHourTo.SelectedValue = beginSelDate.Hour.ToString();
                    ddlMinuteTo.SelectedValue = beginSelDate.Minute.ToString();
                }
                else if (isLeaveEarly)
                {
                    txtOutPickerTo1.Text = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay).ToString("yyyy-MM-dd");
                    ddlHourTo.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay).Hour.ToString();
                    ddlMinuteTo.SelectedValue = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay).Minute.ToString();

                    DateTime endSelDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    while (endSelDate > offWorkTime)
                    {
                        endSelDate = endSelDate.AddHours(-1);
                    }
                    txtOutPickerFrom1.Text = endSelDate.ToString("yyyy-MM-dd");
                    ddlHourFrom.SelectedValue = endSelDate.Hour.ToString();
                    ddlMinuteFrom.SelectedValue = endSelDate.Minute.ToString();
                }
                else
                {
                    txtOutPickerFrom1.Text = selectDateTime.ToString("yyyy-MM-dd");
                    txtOutPickerTo1.Text = selectDateTime.ToString("yyyy-MM-dd");
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
        /// 提交出差单记录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOutSubmit_Click(object sender, EventArgs e)
        {
            // 判断该考勤事由是否允许直接提交，不允许就直接提交
            if (!IsCanSubmitMatters(Request["matterid"]))
            {
                DateTime selectDateTime = DateTime.Parse(txtOutPickerFrom1.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);

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
            MattersInfo model = GetMatterModel(Request["matterid"]);

            // 判断时间段，不允许事后申请
            //if (model.BeginTime <= DateTime.Now)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('目前系统暂不支持事后申请，请联系HR。');", true);
            //    return;
            //}

            model.MatterState = Status.MattersState_WaitDirector;

            ApproveLogInfo approve = GetApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Out);

            //团队总经理不需提交考勤
            string managerids = System.Configuration.ConfigurationManager.AppSettings["MatterManagers"];

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
                if (approve != null)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('外出单提交成功，等待“" + approve.ApproveName + "”的审批！');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('外出单提交成功！');", true);

            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('外出单提交失败，请与系统管理员联系！');", true);
            }
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOutBack_Click(object sender, EventArgs e)
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
            List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(UserInfo.UserID);
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
            DateTime beginTime = DateTime.Parse(txtOutPickerFrom1.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue);
            DateTime endTime = DateTime.Parse(txtOutPickerTo1.Text + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue);
            model.MatterType = Status.MattersType_Out;
            model.MatterContent = txtOutCause.Text.Trim();

            double timeRange = attMan.TimeRangeAnnual(ref beginTime, ref endTime, commuterTimeList);
            model.TotalHours = (decimal)timeRange;
            model.BeginTime = beginTime;
            model.EndTime = endTime;
            model.OperateorID = UserInfo.UserID;
            model.Deleted = false;
            model.UpdateTime = DateTime.Now;


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
            return matterManager.CheckIsOverLap(UserID, DateTime.Parse(txtOutPickerFrom1.Text + " " + ddlHourFrom.SelectedValue + ":" + ddlMinuteFrom.SelectedValue), DateTime.Parse(txtOutPickerTo1.Text + " " + ddlHourTo.SelectedValue + ":" + ddlMinuteTo.SelectedValue), modelid);
        }
    }
}