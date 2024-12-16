using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using System.Net.Mail;

namespace AdministrativeWeb.UserControls.Matter
{
    public partial class OtherEdit : MatterUserControl
    {
        #region 成员变量
        /// <summary>
        /// 考勤事由业务类对象
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
                    if (model.MatterType == Status.MattersType_Other)
                    {
                        ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);

                        hidOtherUserid.Value = model.UserID.ToString();
                        txtOtherUserName.Text = userinfoModel.FullNameCN;
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                        txtOtherUserCode.Text = emp.Code;
                        IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                            ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                        if (list != null && list.Count > 0)
                        {
                            ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                            txtOtherGroup.Text = emppos.DepartmentName;
                            int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                            txtOtherTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                        }
                        labOtherAppTime.Text = model.CreateTime.ToString("yyyy年MM月dd日 HH时");
                        OtherPickerFrom1.SelectedDate = model.BeginTime;
                        OtherPickerTo1.SelectedDate = model.EndTime;
                        txtOtherCause.Text = model.MatterContent;
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

                hidOtherUserid.Value = UserID.ToString();
                txtOtherUserName.Text = UserInfo.FullNameCN;
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                txtOtherUserCode.Text = emp.Code;
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                    txtOtherGroup.Text = emppos.DepartmentName;
                    int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                    txtOtherTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                }
                labOtherAppTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时");

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
                    OtherPickerFrom1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    DateTime beginSelDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    while (beginSelDate < goWorkTime)
                    {
                        beginSelDate = beginSelDate.AddHours(1);
                    }
                    OtherPickerTo1.SelectedDate = beginSelDate;
                }
                else if (isLeaveEarly)
                {
                    OtherPickerTo1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    DateTime endSelDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    while (endSelDate > offWorkTime)
                    {
                        endSelDate = endSelDate.AddHours(-1);
                    }
                    OtherPickerFrom1.SelectedDate = endSelDate;
                }
                else
                {
                    OtherPickerFrom1.SelectedDate = selectDateTime;
                    OtherPickerTo1.SelectedDate = selectDateTime;
                }
            }
        }

        /// <summary>
        /// 保存外出内容信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOtherSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["matterid"]))
            {
                DateTime selectDateTime = OtherPickerFrom1.SelectedDate;

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
            model.MatterState = Status.MattersState_NoSubmit;
            try
            {
                if (model.ID > 0)
                {
                    matterManager.Update(model);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")保存了一条其他事由信息编号(" + model.ID + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('其他事由修改成功。');", true);
                }
                else
                {
                    int modelID = matterManager.Add(model);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")保存了一条其他事由信息编号(" + modelID + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('其他事由保存成功。');", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('其他事由申请单保存失败。');", true);
            }
        }

        /// <summary>
        /// 提交出差单记录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOtherSubmit_Click(object sender, EventArgs e)
        {
            // 判断该考勤事由是否允许直接提交，不允许就直接提交
            if (!IsCanSubmitMatters(Request["matterid"]))
            {
                DateTime selectDateTime = OtherPickerFrom1.SelectedDate;
                
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
            model.MatterState = Status.MattersState_WaitDirector;
            try
            {
                if (model.ID > 0)
                {
                    ApproveLogInfo approve = GetApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Other);
                    matterManager.Update(model, approve);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条其他事由申请单编号(" + model.ID + "),审批人是(" + approve.ApproveID + ", " + approve.ApproveName + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('其他事由申请单提交成功，等待“" + approve.ApproveName + "”审批！');", true);
                }
                else
                {
                    ApproveLogInfo approve = GetApproveLogModel("", (int)Status.MattersSingle.MattersSingle_Other);
                    matterManager.Add(model, approve);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")提交了一条其他事由申请单编号(" + model.ID + "),审批人是(" + approve.ApproveID + ", " + approve.ApproveName + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('其他事由申请单提交成功，等待“" + approve.ApproveName + "”审批！');", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('其他事由申请单提交失败！');", true);
            }
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOtherBack_Click(object sender, EventArgs e)
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
            model.BeginTime = OtherPickerFrom1.SelectedDate;
            model.EndTime = OtherPickerTo1.SelectedDate;
            model.MatterType = Status.MattersType_Other;
            model.MatterContent = txtOtherCause.Text.Trim();
            TimeSpan ts = model.EndTime - model.BeginTime;
            model.TotalHours = decimal.Parse(ts.TotalHours.ToString());
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
            return matterManager.CheckIsOverLap(UserID, OtherPickerFrom1.SelectedDate, OtherPickerTo1.SelectedDate, modelid);
        }
    }
}