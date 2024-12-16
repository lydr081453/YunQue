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
    public partial class OffTuneView : MatterUserControl
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
                    if (model.MatterType == Status.MattersType_OffTune)
                    {
                        ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);

                        hidOffUserid.Value = model.UserID.ToString();
                        txtOffUserName.Text = userinfoModel.FullNameCN;
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                        txtOffUserCode.Text = emp.Code;

                        // 获得剩余的倒休小时数
                        int remaining = singleManager.GetRemainingHours(UserID, DateTime.Parse(_selectDateTime));
                        int invalid = singleManager.InvalidWithinAWeek(UserID, DateTime.Parse(_selectDateTime));
                        labRemainingHours.Text = "剩余调休小时数：" + remaining + "小时，一个星期内要失效的倒休小时数：" + invalid + "小时";

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
                int remaining = singleManager.GetRemainingHours(UserID, selectDateTime);
                int invalid = singleManager.InvalidWithinAWeek(UserID, selectDateTime);
                labRemainingHours.Text = "剩余调休小时数：" + remaining + "小时，一个星期内要失效的倒休小时数：" + invalid + "小时";
                
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
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOffBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl);
        }
    }
}