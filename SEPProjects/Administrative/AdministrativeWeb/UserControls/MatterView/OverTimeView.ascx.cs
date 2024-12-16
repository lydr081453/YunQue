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
using AdministrativeWeb.UserControls.Matter;

namespace AdministrativeWeb.UserControls.MatterView
{
    public partial class OverTimeView : MatterUserControl
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
                    radType.SelectedValue = overtimeInfo.OverTimeType.ToString();
                    hidOverTimeProjectId.Value = overtimeInfo.ProjectID.ToString();
                    txtOverTimeProjectNo.Text = overtimeInfo.ProjectNo;
                    PickerFrom1.SelectedDate = overtimeInfo.BeginTime;
                    PickerTo1.SelectedDate = overtimeInfo.EndTime;
                    txtDes.Text = overtimeInfo.OverTimeCause;
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
                    radType.SelectedValue = Status.OverTimeType_Holiday.ToString();
                    PickerFrom1.SelectedDate = selectDateTime;
                    PickerTo1.SelectedDate = selectDateTime;
                }
                else
                {
                    radType.SelectedValue = Status.OverTimeType_Working.ToString();
                    PickerFrom1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                    PickerTo1.SelectedDate = selectDateTime.Date.Add(commuterTimeModel.OffWorkTime.TimeOfDay);
                }
                labAppTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时");
            }
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
    }
}