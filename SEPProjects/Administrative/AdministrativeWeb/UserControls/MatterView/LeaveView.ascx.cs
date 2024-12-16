using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using ESP.Framework.Entity;
using AdministrativeWeb.UserControls.Matter;

namespace AdministrativeWeb.UserControls.MatterView
{
    public partial class LeaveView : MatterUserControl
    {
        #region 成员变量
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
        /// 上下班时间业务对象类
        /// </summary>
        private CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
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

                        LeavePickerFrom1.SelectedDate = model.BeginTime;
                        LeavePickerTo1.SelectedDate = model.EndTime;
                        LeavePickerFrom2.SelectedDate = model.BeginTime;
                        LeavePickerTo2.SelectedDate = model.EndTime;
                    }
                    else if (model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last
                        || model.MatterType == Status.MattersType_Bereavement
                        || model.MatterType == Status.MattersType_Marriage
                        || model.MatterType == Status.MattersType_Maternity
                        || model.MatterType == Status.MattersType_Incentive
                         || model.MatterType == Status.MattersType_PeiChanJia)
                    {
                        if (model.MatterType == Status.MattersType_Annual)
                            chkAnnual.Checked = true;
                        else if (model.MatterType == Status.MattersType_Annual_Last)
                            chkAnnualLast.Checked = true;
                        else if (model.MatterType == Status.MattersType_Bereavement)
                            chkBereavement.Checked = true;
                        else if (model.MatterType == Status.MattersType_Marriage)
                            chkMarriage.Checked = true;
                        else if (model.MatterType == Status.MattersType_Maternity)
                            chkMaternity.Checked = true;
                        else if (model.MatterType == Status.MattersType_PeiChanJia)
                            chkPeiChanJia.Checked = true;

                        LeavePickerFrom1.SelectedDate = model.BeginTime;
                        LeavePickerTo1.SelectedDate = model.EndTime;
                        LeavePickerFrom2.SelectedDate = model.BeginTime;
                        LeavePickerTo2.SelectedDate = model.EndTime;
                    }
                    else
                    {
                        chkSick.Checked = true;
                        LeavePickerFrom1.SelectedDate = DateTime.Parse(_selectDateTime);
                        LeavePickerTo1.SelectedDate = DateTime.Parse(_selectDateTime);
                        LeavePickerFrom2.SelectedDate = DateTime.Parse(_selectDateTime);
                        LeavePickerTo2.SelectedDate = DateTime.Parse(_selectDateTime);
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
                    LeavePickerFrom1.SelectedDate = selectDateTime;
                    LeavePickerTo1.SelectedDate = selectDateTime;
                    LeavePickerFrom2.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    DateTime beginSelDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    while (beginSelDate < goWorkTime)
                    {
                        beginSelDate = beginSelDate.AddHours(1);
                    }
                    LeavePickerTo2.SelectedDate = beginSelDate;
                }
                else if (isLeaveEarly)
                {
                    LeavePickerFrom1.SelectedDate = selectDateTime;
                    LeavePickerTo1.SelectedDate = selectDateTime;
                    LeavePickerTo2.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    DateTime endSelDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    while (endSelDate > offWorkTime)
                    {
                        endSelDate = endSelDate.AddHours(-1);
                    }
                    LeavePickerFrom2.SelectedDate = endSelDate;
                }
                else 
                {
                    LeavePickerFrom1.SelectedDate = selectDateTime;
                    LeavePickerTo1.SelectedDate = selectDateTime;
                    LeavePickerFrom2.SelectedDate = selectDateTime;
                    LeavePickerTo2.SelectedDate = selectDateTime;
                }
            }
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
    }
}
