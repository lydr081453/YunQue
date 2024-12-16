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
    public partial class TimeSheetAudit : ESP.Web.UI.PageBase
    {
        string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            /* 暂定取消该功能
            if (!string.IsNullOrEmpty(Request["updateChecked"]) && !string.IsNullOrEmpty(Request["tid"]))
            {
                //
                TimeSheetInfo tModel = TimeSheetManager.GetModel(int.Parse(Request["tid"]));
                tModel.IsChecked = bool.Parse(Request["updateChecked"]);
                if (TimeSheetManager.UpdateChecked(tModel))
                    Response.Write("1");
                else
                    Response.Write("0");
                
                Response.Flush();
                Response.End();
                return;
            }*/
            if (!string.IsNullOrEmpty(Request["userid"]))
                userid = Request["userid"];

            if (!IsPostBack)
            {
                litUserName.Text = ESP.Compatible.UserManager.GetUserName(int.Parse(userid));

                ListBind();
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
            string terms = " userid = " + userid + " and commitType <> '" + ESP.Administrative.Common.TimeSheetCommitType.Holiday.ToString() + "' and status=" + (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit; 
            List<TimeSheetCommitInfo> commitList = TimeSheetCommitManager.GetList(terms);

            WeekInfoList list = new WeekInfoList();
            DayInfo info = null;
            for (int i = 0; i < commitList.Count;i++ )
            {
                DateTime date = commitList[i].CurrentDate.Value;
                info = new DayInfo();
                info.WeekCn = ESP.Administrative.BusinessLogic.WeekSettingManager.GetWeekCn((int)date.DayOfWeek);
                info.Year = date.Year;
                info.Month = date.Month;
                info.Week = (int)date.DayOfWeek;
                info.Day = date.Day;
                info.TimeSheetCommitInfo = TimeSheetCommitManager.GetModel(int.Parse(userid), date.ToString("yyyy-MM-dd"));

                if (info.TimeSheetCommitInfo.CurrentGoWorkTime != null && info.TimeSheetCommitInfo.CurrentOffWorkTime != null)
                {
                    double time1 = ((new DateTime(info.TimeSheetCommitInfo.CurrentGoWorkTime.Value.Year, info.TimeSheetCommitInfo.CurrentGoWorkTime.Value.Month, info.TimeSheetCommitInfo.CurrentGoWorkTime.Value.Day, 12, 30, 0)) - info.TimeSheetCommitInfo.CurrentGoWorkTime.Value).TotalMinutes;
                    double time2 = (info.TimeSheetCommitInfo.CurrentOffWorkTime.Value - (new DateTime(info.TimeSheetCommitInfo.CurrentGoWorkTime.Value.Year, info.TimeSheetCommitInfo.CurrentGoWorkTime.Value.Month, info.TimeSheetCommitInfo.CurrentGoWorkTime.Value.Day, 13, 30, 0))).TotalMinutes;

                    double time = time1 + time2;
                    if (time < 0)
                        time = 0;
                    info.WorkHours = (double.Parse((time / 60).ToString("0.00"))).ToString("0.00");
                }
                else
                {
                    info.WorkHours = "0";
                }

                string strWhere = " userid = " + userid + " and commitid = " + info.TimeSheetCommitInfo.Id;
                info.TimeSheets = ESP.Administrative.BusinessLogic.TimeSheetManager.GetList(500, strWhere, "id");
                info.TotalHours = info.TimeSheets.Sum(x => x.Hours);
                list.DayInfos.Add(info);
            }
            return list;
        }

        private void ListBind()
        {
            if (Request["userid"] != null)
            {
                userid = Request["userid"];
            }

            string terms = " and a.userId = " + userid + " and a.commitType='" + ESP.Administrative.Common.TimeSheetCommitType.Holiday.ToString() + "' and a.status =" + (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
            List<TimeSheetCommitInfo> rollbackList = TimeSheetCommitManager.GetLongHolidayList(terms);
            if (rollbackList == null || rollbackList.Count == 0)
                palLong.Visible = false;
            else
            {
                palLong.Visible = true;
                gvList.DataSource = rollbackList;
                gvList.DataBind();
            }
        }

        public void setCheckType()
        {

        }

        /// <summary>
        /// 获得TimeSheet类型
        /// </summary>
        /// <returns></returns>
        public List<TimeSheetCategoryInfo> GetTimeSheetTypeList()
        {
            return ESP.Administrative.BusinessLogic.TimeSheetCategoryManager.GetList("");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            if (audit((int)ESP.Administrative.Common.TimeSheetCommitStatus.Passed))
            {
                Response.Redirect("TimeSheetAuditList.aspx");
            }
            
        }

        private bool audit(int status)
        {
            string commitIds = Request["commitIds"];
            string serialNos = Request["serialNos"];
            TimeSheetLogInfo log = new TimeSheetLogInfo();
            log.AuditorId = int.Parse(CurrentUser.SysID);
            log.AuditorName = CurrentUser.Name;
            log.AuditDate = DateTime.Now;
            log.Suggestion = txtDesc.Text.Trim();
            log.IP = Request.UserHostAddress;

            return TimeSheetCommitManager.SubmitAudit(commitIds,serialNos, status, log);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (audit((int)ESP.Administrative.Common.TimeSheetCommitStatus.Reject))
            {
                Response.Redirect("TimeSheetAuditList.aspx");
            }
        }
    }
}
