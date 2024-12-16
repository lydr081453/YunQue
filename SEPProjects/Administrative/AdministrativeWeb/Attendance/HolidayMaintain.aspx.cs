using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.Attendance
{
    public partial class HolidayMaintain : ESP.Web.UI.PageBase
    {
        public static int yearvalue = DateTime.Now.Year;

        /// <summary>
        /// 选择时间保存集合
        /// </summary>
        public static List<DateTime> dateTimeList = new List<DateTime>();

        /// <summary>
        /// 假日信息业务维护对象
        /// </summary>
        public HolidaysInfoManager holidayManager = new HolidaysInfoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SelectDefaultDate(yearvalue);
            }
        }

        /// <summary>
        /// 选择一个日期的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            // 判断当前选择的日期是否存在于选择日期集合中
            DateTime selDateTime = Calendar1.SelectedDate;
            // 如果存在将该日期从选择日期集合中删除掉,否则将添加到选择的日期集合中
            if (dateTimeList.Contains(selDateTime))
            {
                dateTimeList.Remove(selDateTime);
                Calendar1.SelectedDates.Remove(selDateTime);
            }
            else
            {
                // 往选择日期集合中添加选择日期
                dateTimeList.Add(selDateTime);
            }
            
            foreach (DateTime dt in dateTimeList)
            {
                Calendar1.SelectedDates.Add(dt);
            }
        }

        /// <summary>
        /// 回复默认假日
        /// </summary>
        /// <param name="year"></param>
        protected void SelectDefaultDate(int year)
        {
            Calendar1.VisibleDate = new DateTime(year, 1, 1);
            dateTimeList.Clear();
            List<DateTime> list = holidayManager.GetOneYearHolidays(year);
            // 清空原有日历选择的日期集合
            Calendar1.SelectedDates.Clear();
            if (list != null && list.Count > 0)
            {
                foreach (DateTime dt in list)
                {
                    dateTimeList.Add(dt);
                    Calendar1.SelectedDates.Add(dt);
                }
            }
            else
            {
                for (DateTime dt = new DateTime(year, 1, 1); dt <= new DateTime(year, 12, 31); dt = dt.AddDays(1))
                {
                    if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dateTimeList.Add(dt);
                        Calendar1.SelectedDates.Add(dt);
                    }
                }
            }
        }

        /// <summary>
        /// 回复当月默认的节假日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDefaultHoliday_Click(object sender, EventArgs e)
        {
            Calendar1.VisibleDate = new DateTime(yearvalue, 1, 1);
            dateTimeList.Clear();
            // 清空原有日历选择的日期集合
            Calendar1.SelectedDates.Clear();
            for (DateTime dt = new DateTime(yearvalue, 1, 1); dt <= new DateTime(yearvalue, 12, 31); dt = dt.AddDays(1))
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    dateTimeList.Add(dt);
                    Calendar1.SelectedDates.Add(dt);
                }
            }
        }

        /// <summary>
        /// 年份减一
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void aPrev_Click(object sender, EventArgs e)
        {
            --yearvalue;
            SelectDefaultDate(yearvalue);
        }

        /// <summary>
        /// 年份加一
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void aNext_Click(object sender, EventArgs e)
        {
            ++yearvalue;
            SelectDefaultDate(yearvalue);
        }

        /// <summary>
        /// 保存当年的假日信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (dateTimeList != null && dateTimeList.Count > 0)
            {
                // 更新一年的假日信息
                bool b = holidayManager.AddOneYearHolidays(yearvalue, dateTimeList, UserID);
                if (b)
                {
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")更新了"+yearvalue+"年的节假日信息", 
                    "考勤系统基本信息", ESP.Logging.LogLevel.Information);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + yearvalue + "年的假日信息保存成功！');", true);
                }
                else
                {
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")更新" + yearvalue + "年的节假日信息失败！",
                    "考勤系统基本信息", ESP.Logging.LogLevel.Information);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + yearvalue + "年的假日信息保存失败！');", true);
                }
            }
        }
    }
}