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
    public partial class TimeSheetMonthView : ESP.Web.UI.PageBase
    {
        private string sYM = "";
        int userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["sYM"]))
            {
                sYM = Request["sYM"];
            }
            else
            {
                sYM = DateTime.Now.ToString("yyyy-MM");
            }

            if(!string.IsNullOrEmpty(Request["userid"])){
                userId = int.Parse(Request["userid"]); 
            }

            if (!IsPostBack)
            {
                litUserName.Text = ESP.Compatible.UserManager.GetUserName(userId);
                litDate.Text = DateTime.Parse(sYM + "-01").ToString("yyyy.MM");
                hylPre.NavigateUrl = "TimeSheetMonthView.aspx?sYM=" + DateTime.Parse(sYM + "-01").AddMonths(-1).ToString("yyyy-MM") + "&userid=" + userId;
                hylNext.NavigateUrl = "TimeSheetMonthView.aspx?sYM=" + DateTime.Parse(sYM + "-01").AddMonths(1).ToString("yyyy-MM") + "&userid=" + userId;
                hylWeek.NavigateUrl = "TiemSheetViewList.aspx?userid=" + userId + "&bdate=" + DateTime.Parse(sYM + "-01").ToString("yyyy-MM-dd") + "&edate=" + DateTime.Parse(sYM + "-07").ToString("yyyy-MM-dd");

                //剩余调休天数
                //判断如果总监级以上,没有调休
                //ESP.Administrative.Entity.UserAttBasicInfo basicModel = (new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager()).GetModelByUserid(userId);
                //if (basicModel.AttendanceType == 3)
                //{
                //    litOverWorkRemain.Text = "0";
                //}
                //else
                //{
                //    string[] tmp = TimeSheetCommitManager.GetUserOverWorkRemain(userId).ToString("0.0").Split('.');
                //    litOverWorkRemain.Text = decimal.Parse(tmp[0] + "." + (int.Parse(tmp[1]) >= 5 ? 5 : 0).ToString()).ToString("0.#");
                //}

                //剩余年假数
                ALAndRLManager alManager = new ALAndRLManager();
                ALAndRLInfo al1Info = alManager.GetALAndRLModel(userId, DateTime.Now, 1);
                litYear.Text = al1Info == null ? "0" : al1Info.RemainingNumber.ToString("0.#");
                //奖励假数
                //ALAndRLInfo al2Info = alManager.GetALAndRLModel(userId, DateTime.Now, 2);
                //litJL.Text = al2Info == null ? "0" : al2Info.RemainingNumber.ToString("0.#");

                SetMonthStat();
            }

        }

        public List<MonthInfo> GetMonthList()
        {

            DateTime firstDay = DateTime.Parse(sYM + "-01");
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
            List<MonthInfo> monthList = new List<MonthInfo>();

            for (int i = ((int)firstDay.DayOfWeek); i > 0; i--)
            {
                MonthInfo m = new MonthInfo();
                DateTime day = firstDay.AddDays(-i);
                m.Year = day.Year;
                m.Month = day.Month;
                m.Day = day.Day;
                m.Week = (int)day.DayOfWeek;

                if (m.Week != 6 && m.Week != 0)
                    m.ClassName = "month-notthis";
                else
                    m.ClassName = "month-notthis-weekend";
                setTimeSheetInfo(m, day);
                monthList.Add(m);
            }

            int cbNum = 1;
            int ceNum = lastDay.Day;

            for (int i = cbNum; i <= ceNum; i++)
            {
                MonthInfo m = new MonthInfo();
                DateTime day = firstDay.AddDays(i - 1);
                m.Year = day.Year;
                m.Month = day.Month;
                m.Day = day.Day;
                m.Week = (int)day.DayOfWeek;

                if (m.Week != 6 && m.Week != 0)
                    m.ClassName = "month-this";
                else
                    m.ClassName = "month-this-weekend";

                if (DateTime.Now.ToString("yyyy-MM-dd") == day.ToString("yyyy-MM-dd"))
                    m.ClassName = "month-today";
                setTimeSheetInfo(m, day);
                monthList.Add(m);
            }

            int endNum = 42 - monthList.Count;
            for (int i = 1; i <= endNum; i++)
            {
                MonthInfo m = new MonthInfo();
                DateTime day = lastDay.AddDays(i);
                m.Year = day.Year;
                m.Month = day.Month;
                m.Day = day.Day;
                m.Week = (int)day.DayOfWeek;
                if (m.Week != 6 && m.Week != 0)
                    m.ClassName = "month-notthis";
                else
                    m.ClassName = "month-notthis-weekend";
                setTimeSheetInfo(m, day);
                monthList.Add(m);
            }
            return monthList;
        }

        private void setTimeSheetInfo(MonthInfo m, DateTime day)
        {
            TimeSheetCommitInfo timeSheetInfo = new TimeSheetCommitInfo();
            timeSheetInfo = TimeSheetCommitManager.GetTimeSheetInfoForDay(userId, day);
            m.TimeSheetCount = timeSheetInfo == null ? 0 : timeSheetInfo.TimeSheetCount;
            m.TimeSheetHours = timeSheetInfo == null ? 0 : timeSheetInfo.TimeSheetHours;
            m.UserId = userId;

            m.ChiDao = timeSheetInfo == null ? 0 : timeSheetInfo.ChiDao;
            m.ZaoTui = timeSheetInfo == null ? 0 : timeSheetInfo.ZaoTui;
            m.KuangGong = timeSheetInfo == null ? 0 : timeSheetInfo.KuangGong;
            m.QingJia = timeSheetInfo == null ? 0 : timeSheetInfo.QingJia;
            m.ChuChai = timeSheetInfo == null ? 0 : timeSheetInfo.ChuChai;
            m.WaiChu = timeSheetInfo == null ? 0 : timeSheetInfo.WaiChu;
            m.JiaBan = timeSheetInfo == null ? 0 : timeSheetInfo.JiaBan;
            m.TiaoXiu = timeSheetInfo == null ? 0 : timeSheetInfo.TiaoXiu;
            m.TimeSheetCommitModel = timeSheetInfo;
        }

        /// <summary>
        /// 设置当前用户所选择的月份的考情统计信息
        /// </summary>
        protected void SetMonthStat()
        {
            int year = int.Parse(sYM.Split('-')[0]);
            int month = int.Parse(sYM.Split('-')[1]);
            MonthStatManager statManager = new MonthStatManager();
            MonthStatInfo statInfo = statManager.GetMonthStatInfoApprove(userId, year, month);

            HashSet<int> holidays = new HolidaysInfoManager().GetHolidayListByMonth(year,month);

            decimal jKG = 0;

            DateTime begin = new DateTime(year,month,1);
            for (DateTime dt = begin; dt < begin.AddMonths(1); )
            {
                TimeSheetCommitInfo tm = TimeSheetCommitManager.GetModel(userId, dt.ToString("yyyy-MM-dd"));
                if (tm == null || (tm.Status != (int)ESP.Administrative.Common.TimeSheetCommitStatus.Passed  && tm.Status != (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit))
                {
                    if (!holidays.Contains(dt.Day))
                    {
                        jKG += 8;
                    }
                }
               dt = dt.AddDays(1);
            }


            if (statInfo != null)
            {
                labLate.Text = statInfo.LateCount.ToString("#.##");    // 迟到

                labLeaveEarly.Text = statInfo.LeaveEarlyCount.ToString("#.##");   // 早退

                labAbsent.Text = (statInfo.AbsentDays  + jKG).ToString("#.##");    // 旷工

                labOverTime.Text = statInfo.OverTimeHours.ToString("#.##");   // OT

                //labHolidayOverTime.Text = statInfo.HolidayOverTimeHours.ToString();   // 节假日OT

                labEvection.Text = statInfo.EvectionDays.ToString("#.##");  // 出差

                labEgress.Text = statInfo.EgressHours.ToString("#.##");   // 外出

                labSickLeave.Text = statInfo.SickLeaveHours.ToString("#.##"); // 病假

                labAffiairLeave.Text = statInfo.AffairLeaveHours.ToString("#.##");  // 事假

                labAnnualLeave.Text = statInfo.AnnualLeaveDays.ToString("#.##");  // 年假

                labFuneralLeave.Text = statInfo.FuneralLeaveHours.ToString("#.##");   // 丧假

                labMarriageLeave.Text = statInfo.MarriageLeaveHours.ToString("#.##");  // 婚假

                labMaternityLeave.Text = statInfo.MaternityLeaveHours.ToString("#.##");  // 产假

                labPrenatalCheck.Text = statInfo.PrenatalCheckHours.ToString("#.##");   // 产前检查

                //labIncentive.Text = statInfo.IncentiveHours.ToString("#.##");      // 奖励假
            }
        }
    }
}
