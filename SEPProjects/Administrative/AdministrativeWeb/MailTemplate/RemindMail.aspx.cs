using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.MailTemplate
{
    public partial class RemindMail : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgs.ImageUrl = "http://" + Request.Url.Authority + "/images/mail_03.jpg";
                int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);
                int senderid = string.IsNullOrEmpty(Request["senderid"]) ? 0 : int.Parse(Request["senderid"]);
                int year = string.IsNullOrEmpty(Request["year"]) ? 0 : int.Parse(Request["year"]);
                int month = string.IsNullOrEmpty(Request["month"]) ? 0 : int.Parse(Request["month"]);
                Sender = ESP.Framework.BusinessLogic.UserManager.Get(senderid).FullNameCN;
                RemindText = "您该做日常考勤了。";
                AttendanceStatisticManager manager = new AttendanceStatisticManager();
                AttendanceStatisticInfo model = manager.GetAttendanceStatisticModel(userid, year, month);
                if (model != null)
                {
                    labMonth.Text = model.AttendanceYear + "年" + model.AttendanceMonth + "月";
                    labLateCount1.Text = model.LateCount1.ToString();
                    labLateCount2.Text = model.LateCount2.ToString();
                    labAbsentCount1.Text = model.AbsentCount1.ToString();
                    labAbsentCount2.Text = model.AbsentCount2.ToString(); 
                    labAbsentCount3.Text = model.AbsentCount3.ToString();
                    labLeaveEarly.Text = model.LeaveEarly.ToString();
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
