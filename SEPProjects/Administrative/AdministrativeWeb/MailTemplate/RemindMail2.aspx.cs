using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;

namespace AdministrativeWeb.MailTemplate
{
    public partial class RemindMail2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgs.ImageUrl = "http://" + Request.Url.Authority + "/images/mail_03.jpg";
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);
            int senderid = string.IsNullOrEmpty(Request["senderid"]) ? 0 : int.Parse(Request["senderid"]);
            int year = string.IsNullOrEmpty(Request["year"]) ? 0 : int.Parse(Request["year"]);
            int month = string.IsNullOrEmpty(Request["month"]) ? 0 : int.Parse(Request["month"]);
            this.SetMonthStat(userid, year, month);

            Sender = ESP.Framework.BusinessLogic.UserManager.Get(senderid).FullNameCN;
            RemindText = "该做日常考勤了。";

            labTitle.Text = "考勤统计（" + year + "年" + month + "月）";
        }


        protected void SetMonthStat(int userid, int year, int month)
        {
            UserAttBasicInfoManager userBasicManager = new UserAttBasicInfoManager();

            ESP.Administrative.BusinessLogic.AttendanceManager attMan = new ESP.Administrative.BusinessLogic.AttendanceManager();
           
            UserAttBasicInfo userBasicModel = userBasicManager.GetModelByUserid(userid);
            if (userBasicModel != null)
            {
                AttendanceDataInfo attdatainfo = attMan.GetMonthStat(userid, year, month, null,null,null);
                if (attdatainfo != null)
                {
                    // 判断人员考勤的类型，如果是考勤是特殊的就不计算考勤情况
                    if (attdatainfo.LateCount > 0)
                        labLate.Text = attdatainfo.LateCount + "";    // 迟到
                    else
                        labLate.Text = "";    // 迟到

                    if (attdatainfo.LeaveEarlyCount > 0)
                        labLeaveEarly.Text = attdatainfo.LeaveEarlyCount + "";   // 早退
                    else
                        labLeaveEarly.Text = "";

                    if (attdatainfo.AbsentHours > 0)
                        labAbsent.Text = string.Format("{0:F1}", attdatainfo.AbsentHours / Status.WorkingHours) + "D";    // 旷工
                    else
                        labAbsent.Text = "";

                    if (attdatainfo.EvectionHours > 0)
                        labEvection.Text = string.Format("{0:F1}", attdatainfo.EvectionHours / Status.WorkingHours) + "D";  // 出差
                    else
                        labEvection.Text = "";

                    if (attdatainfo.EgressHours > 0)
                        labEgress.Text = string.Format("{0:F3}", attdatainfo.EgressHours) + "H";   // 外出
                    else
                        labEgress.Text = "";

                    if (attdatainfo.SickLeaveHours > 0)
                        labSickLeave.Text = string.Format("{0:F3}", attdatainfo.SickLeaveHours) + "H"; // 病假
                    else
                        labSickLeave.Text = "";


                    if (attdatainfo.AffiairLeaveHours > 0)
                        labAffiairLeave.Text = string.Format("{0:F3}", attdatainfo.AffiairLeaveHours) + "H";  // 事假
                    else
                        labAffiairLeave.Text = "";


                    if (attdatainfo.AnnualLeaveHours > 0)
                        labAnnualLeave.Text = string.Format("{0:F3}", attdatainfo.AnnualLeaveHours / Status.WorkingHours) + "D";  // 法定假
                    else
                        labAnnualLeave.Text = "";

                    if (attdatainfo.FuneralLeaveHours > 0)
                        labFuneralLeave.Text = string.Format("{0:F3}", attdatainfo.FuneralLeaveHours / Status.WorkingHours) + "D";   // 丧假
                    else
                        labFuneralLeave.Text = "";

                    if (attdatainfo.MarriageLeaveHours > 0)
                        labMarriageLeave.Text = string.Format("{0:F1}", attdatainfo.MarriageLeaveHours / Status.WorkingHours) + "D";  // 婚假
                    else
                        labMarriageLeave.Text = "";

                    if (attdatainfo.MaternityLeaveHours > 0)
                        labMaternityLeave.Text = string.Format("{0:F1}", attdatainfo.MaternityLeaveHours / Status.WorkingHours) + "D";  // 产假
                    else
                        labMaternityLeave.Text = "";

                    if (attdatainfo.PrenatalCheckHours > 0)
                        labPrenatalCheck.Text = string.Format("{0:F3}", attdatainfo.PrenatalCheckHours / Status.WorkingHours) + "D";   // 产前检查
                    else
                        labPrenatalCheck.Text = "";

                }
            }
        }

        /// <summary>
        /// 发送人姓名
        /// </summary>
        public string Sender
        {
            get { return this.ViewState["Sender"] as string; }
            set { this.ViewState["Sender"] = value; }
        }

        /// <summary>
        /// 提示内容
        /// </summary>
        public string RemindText
        {
            get { return this.ViewState["Content"] as string; }
            set { this.ViewState["Content"] = value; }
        }
    }
}