using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;

namespace AdministrativeWeb.Attendance
{
    public partial class TiemSheetViewList : ESP.Web.UI.PageBase
    {
        string userid = "";
        DateTime bDate = new DateTime();
        DateTime eDate = new DateTime();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
                userid = Request["userid"];
            if (!string.IsNullOrEmpty(Request["bdate"]))
            {
                bDate = DateTime.Parse(Request["bdate"]);
                eDate = bDate.AddDays(6);
            }
            if (!string.IsNullOrEmpty(Request["edate"]))
                eDate = DateTime.Parse(Request["edate"]);

            if (!IsPostBack)
            {
                litDate.Text = bDate.ToString("yyyy-MM-dd") + " 至 " + eDate.ToString("yyyy-MM-dd");
                litUserName.Text = ESP.Compatible.UserManager.GetUserName(int.Parse(userid));
                hylMonth.NavigateUrl = "TimeSheetMonthView.aspx?sYM=" + bDate.ToString("yyyy-MM") + "&userid="+userid;

                DateTime preB = bDate.AddDays(-((int)bDate.DayOfWeek == 0 ? 7 : (int)bDate.DayOfWeek) - 6);
                DateTime nexB = bDate.AddDays( 8 -((int)bDate.DayOfWeek == 0 ? 7 : (int)bDate.DayOfWeek));

                hylPre.NavigateUrl = "TiemSheetViewList.aspx?userid=" + userid + "&bdate=" + preB.ToString("yyyy-MM-dd") + "&edate=" + preB.AddDays(6).ToString("yyyy-MM-dd");
                hylNext.NavigateUrl = "TiemSheetViewList.aspx?userid=" + userid + "&bdate=" + nexB.ToString("yyyy-MM-dd") + "&edate=" + nexB.AddDays(6).ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 获得一周的TimeSheet信息
        /// </summary>
        /// <returns></returns>
        public WeekInfoList GetWeekInfoList()
        {
            if (Request["userid"] != null)
            {
                userid = Request["userid"];
            }

            WeekInfoList list = new WeekInfoList();
            DayInfo info = null;
            for (DateTime date = bDate; date <= eDate;)
            {
                info = new DayInfo();
                info.WeekCn = ESP.Administrative.BusinessLogic.WeekSettingManager.GetWeekCn((int)date.DayOfWeek);
                info.Year = date.Year;
                info.Month = date.Month;
                info.Week = (int)date.DayOfWeek;
                info.Day = date.Day;
                info.TimeSheetCommitInfo = TimeSheetCommitManager.GetModel(int.Parse(userid), date.ToString("yyyy-MM-dd"));
                if (info.TimeSheetCommitInfo != null )// && info.TimeSheetCommitInfo.Status == (int)ESP.Administrative.Common.TimeSheetCommitStatus.Passed)
                {
                    string strWhere = " userid = " + userid + " and commitid = " + info.TimeSheetCommitInfo.Id;
                    info.TimeSheets = ESP.Administrative.BusinessLogic.TimeSheetManager.GetList(500, strWhere, "id");
                    info.TotalHours = info.TimeSheets.Sum(x => x.Hours);
                }
                else
                {
                    info.TimeSheets = new List<TimeSheetInfo>();
                }

                if (info.TimeSheetCommitInfo != null && info.TimeSheetCommitInfo.CurrentGoWorkTime != null && info.TimeSheetCommitInfo.CurrentOffWorkTime != null)
                {
                    double time = (info.TimeSheetCommitInfo.CurrentOffWorkTime.Value - info.TimeSheetCommitInfo.CurrentGoWorkTime.Value).TotalMinutes;
                    info.WorkHours = (time / 60).ToString("0.00");
                }
                else
                {
                    info.WorkHours = "0";
                }
                list.DayInfos.Add(info);
                date = date.AddDays(1); 
            }
            return list;
        }

        /// <summary>
        /// 获得TimeSheet类型
        /// </summary>
        /// <returns></returns>
        public List<TimeSheetCategoryInfo> GetTimeSheetTypeList()
        {
            return ESP.Administrative.BusinessLogic.TimeSheetCategoryManager.GetList("");
        }
    }
}
