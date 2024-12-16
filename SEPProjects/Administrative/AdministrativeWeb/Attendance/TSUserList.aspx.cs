using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Administrative.BusinessLogic;
using System.Net.Mail;

namespace AdministrativeWeb.Attendance
{
    public partial class TSUserList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        private void BindInfo()
        {
            PickerFrom.SelectedDate = GetMondayByNow((int)DateTime.Now.DayOfWeek, DateTime.Now);
            PickerTo.SelectedDate = PickerFrom.SelectedDate.AddDays(6);

            ListBind();
        }

        private DateTime GetMondayByNow(int dayOfWeek, DateTime now)
        {
            DateTime monday = DateTime.Now;
            switch (dayOfWeek)
            {
                case 1:
                    monday = now;
                    break;
                case 2:
                    monday = now.AddDays(-1);
                    break;
                case 3:
                    monday = now.AddDays(-2);
                    break;
                case 4:
                    monday = now.AddDays(-3);
                    break;
                case 5:
                    monday = now.AddDays(-4);
                    break;
                case 6:
                    monday = now.AddDays(-5);
                    break;
                case 0:
                    monday = now.AddDays(-6);
                    break;
            }
            return monday;
        }

        private void ListBind()
        {
            DataTable dt = null;
            string hrusers = System.Configuration.ConfigurationManager.AppSettings["TimeSheetHRAdmin"];
            if (hrusers.IndexOf(CurrentUser.SysID) >= 0)
            {
                dt = TimeSheetCommitManager.GetUserListByHrAdmin(PickerFrom.SelectedDate.ToString(), PickerTo.SelectedDate.ToString(), txtKey.Text.Trim());
            }
            else
            {
                dt = TimeSheetCommitManager.GetUserListByManagerId(int.Parse(CurrentUser.SysID), PickerFrom.SelectedDate.ToString(), PickerTo.SelectedDate.ToString(), txtKey.Text.Trim());
            }
            gvList.DataSource = dt;
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hidUserId = (HiddenField)e.Row.FindControl("hidUserId");
                HyperLink hlyLink = (HyperLink)e.Row.FindControl("hlyLink");
                hlyLink.NavigateUrl = "TiemSheetViewList.aspx?bdate=" + PickerFrom.SelectedDate.ToString("yyyy-MM-dd") + "&edate=" + PickerTo.SelectedDate.ToString("yyyy-MM-dd") + "&userid=" + hidUserId.Value;

                HyperLink hylMLink = (HyperLink)e.Row.FindControl("hylMLink");
                hylMLink.NavigateUrl = "TimeSheetMonthView.aspx?sYM=" + PickerFrom.SelectedDate.ToString("yyyy-MM") + "&userid=" + hidUserId.Value;
            }
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {

            string[] useridList = Request["chkItem"].Split(',');
            if (useridList != null && useridList.Length > 0)
            {
                foreach (string userid in useridList)
                {
                    //发邮件
                    try
                    {
                        string email = new ESP.Compatible.Employee(int.Parse(userid)).EMail;
                        if (!string.IsNullOrEmpty(email))
                        {

                            string year = PickerFrom.SelectedDate.Year.ToString();
                            string month = PickerFrom.SelectedDate.Month.ToString();
                            string url = "http://" + Request.Url.Authority + "/MailTemplate/RemindMail2.aspx?userid=" + userid + "&senderid=" + UserID + "&year=" + year + "&month=" + month;
                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                            body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                            MailAddress[] recipientAddress = { new MailAddress(email) };

                            ESP.Mail.MailManager.Send("Time Sheet填报提醒", body, true, null, recipientAddress, null, null, null);
                            ClientScript.RegisterStartupScript(typeof(string), "", "alert('邮件发送成功！');", true);
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
