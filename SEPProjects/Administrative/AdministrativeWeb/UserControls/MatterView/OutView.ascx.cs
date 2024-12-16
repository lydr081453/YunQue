using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using AdministrativeWeb.UserControls.Matter;

namespace AdministrativeWeb.UserControls.MatterView
{
    public partial class OutView : MatterUserControl
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
        /// 上下班时间业务对象类
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
                        OutPickerFrom1.SelectedDate = model.BeginTime;
                        OutPickerTo1.SelectedDate = model.EndTime;
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
                    OutPickerFrom1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    DateTime beginSelDate = selectDateTime.Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay);
                    while (beginSelDate < goWorkTime)
                    {
                        beginSelDate = beginSelDate.AddHours(1);
                    }
                    OutPickerTo1.SelectedDate = beginSelDate;
                }
                else if (isLeaveEarly)
                {
                    OutPickerTo1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    DateTime endSelDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    while (endSelDate > offWorkTime)
                    {
                        endSelDate = endSelDate.AddHours(-1);
                    }
                    OutPickerFrom1.SelectedDate = endSelDate;
                }
                else
                {
                    OutPickerFrom1.SelectedDate = selectDateTime;
                    OutPickerTo1.SelectedDate = selectDateTime;
                }
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
    }
}