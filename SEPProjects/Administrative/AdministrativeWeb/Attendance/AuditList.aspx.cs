using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using ESP.Administrative.BusinessLogic;
using System.Data;
using System.Net.Mail;

namespace AdministrativeWeb.Attendance
{
    public partial class AuditList : ESP.Web.UI.PageBase
    {
        /// <summary>
        /// 审批记录业务类
        /// </summary>
        private readonly ApproveLogManager approveLogManager = new ApproveLogManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        /// <summary>
        /// 绑定查询数据
        /// </summary>
        protected void BindInfo()
        {
            // 审批人用户ID
            string approveuserid = UserID.ToString(); 
            // 获得用户所代理的审批人
            IList<ESP.Framework.Entity.AuditBackUpInfo> Delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(UserID);
            if (Delegates != null && Delegates.Count > 0)
            {
                foreach (ESP.Framework.Entity.AuditBackUpInfo auditbackup in Delegates)
                {
                    approveuserid += "," + auditbackup.UserID;
                }
            }

            string sqlStr = "";
            sqlStr += " and approveID in (" + approveuserid + ")";

            if (!string.IsNullOrEmpty(txtApp.Text.Trim()))
            {
                sqlStr += string.Format(" and userName like '%{0}%' ", txtApp.Text.Trim());
            }
            if (!string.IsNullOrEmpty(PickerFrom.SelectedDate.ToString()) && PickerFrom.SelectedDate.Year != 1)
            {
                sqlStr += string.Format(" and datediff(dd, cast('{0}' as smalldatetime), CreateTime ) >= 0  ", PickerFrom.SelectedDate);
            }
            if (!string.IsNullOrEmpty(PickerTo.SelectedDate.ToString()) && PickerTo.SelectedDate.Year != 1)
            {
                sqlStr += string.Format(" and datediff(dd, cast('{0}' as smalldatetime), CreateTime ) <= 0  ", PickerTo.SelectedDate);
            }

            DataSet ds = approveLogManager.GetWaitApproveList(sqlStr);
            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        /// <summary>
        /// 查询待审批的事由信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }

        /// <summary>
        /// 审批驳回考勤事由信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOverrule_Click(object sender, EventArgs e)
        {
            // 获得选择考勤事由集合
            GridItemCollection checkedItems = Grid1.GetCheckedItems(Grid1.Levels[0].Columns["Deleted"]);
            List<int> approveIdsList = new List<int>();
            if (checkedItems != null && checkedItems.Count > 0)
            {
                foreach (GridItem item in checkedItems)
                {
                    // 审批记录ID
                    int id = int.Parse(item["ID"].ToString());
                    approveIdsList.Add(id);
                }
                MattersManager mattersManager = new MattersManager();
                // 发邮件内容信息，key表示事由的ID值，value内容格式(matter/single,1/2,AcceptUserId,matterid/singleid)(考勤事由/OT事由,审批通过/等待下级审批,接受人id,事由申请人id)
                Dictionary<int, string> sendMailDic = new Dictionary<int, string>();
                bool b = mattersManager.BatchOverruleMatter(UserID, approveIdsList, Request.Url.Authority, out sendMailDic);
                if (b)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量驳回考勤事由成功。');window.location='AuditList.aspx';", true);
                    if (sendMailDic != null && sendMailDic.Count > 0)
                    {
                        foreach (KeyValuePair<int, string> pair in sendMailDic)
                        {
                            string[] value = pair.Value.Split(new char[] { ',' });
                            int key = pair.Key;
                            // 考勤事由
                            if (value[0] == "matter")
                            {
                                //发邮件
                                try
                                {
                                    string email = new ESP.Compatible.Employee(int.Parse(value[2])).EMail;
                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + key + "&flag=1";
                                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                        body += "<br><br>您的事由申请单，审批已被驳回";
                                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                        MailAddress[] recipientAddress = { new MailAddress(email) };

                                        ESP.Mail.MailManager.Send("考勤事由被驳回", body, true, null, recipientAddress, null, null, null);
                                    }
                                }
                                catch { }
                            }
                            // OT事由
                            else
                            {
                                //发邮件
                                try
                                {
                                    string email = new ESP.Compatible.Employee(int.Parse(value[2])).EMail;
                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + key + "&flag=1";
                                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                        body += "<br><br>您的事由申请单，审批已被驳回";
                                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                        MailAddress[] recipientAddress = { new MailAddress(email) };

                                        ESP.Mail.MailManager.Send("考勤事由被驳回", body, true, null, recipientAddress, null, null, null);
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量驳回考勤事由失败，请与系统管理员联系。');window.location='AuditList.aspx';", true);
                    return;
                }
            }
        }

        /// <summary>
        /// 审批通过考勤统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            // 获得选择考勤事由集合
            GridItemCollection checkedItems = Grid1.GetCheckedItems(Grid1.Levels[0].Columns["Deleted"]);
            List<int> approveIdsList = new List<int>();
            if (checkedItems != null && checkedItems.Count > 0)
            {
                foreach (GridItem item in checkedItems)
                {
                    // 审批记录ID
                    int id = int.Parse(item["ID"].ToString());
                    approveIdsList.Add(id);
                }
                MattersManager mattersManager = new MattersManager();
                // 发邮件内容信息，key表示事由的ID值，value内容格式(matter/single,1/2,AcceptUserId,matterid/singleid)(考勤事由/OT事由,审批通过/等待下级审批,接受人id,事由申请人id)
                Dictionary<int, string> sendMailDic = new Dictionary<int, string>();
                bool b = mattersManager.BatchAuditMatter(UserID, approveIdsList, out sendMailDic);
                if (b)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量审批考勤事由成功。');window.location='AuditList.aspx';", true);
                    if (sendMailDic != null && sendMailDic.Count > 0)
                    {
                        foreach (KeyValuePair<int, string> pair in sendMailDic)
                        {
                            string[] value = pair.Value.Split(new char[] { ',' });
                            int key = pair.Key;
                            // 考勤事由
                            if (value[0] == "matter")
                            {
                                if (value[1] == "1")
                                {
                                    //发邮件
                                    try
                                    {
                                        string email = new ESP.Compatible.Employee(int.Parse(value[2])).EMail;
                                        if (!string.IsNullOrEmpty(email))
                                        {

                                            string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + key + "&flag=1";
                                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                            body += "<br><br>您的考勤事由审批已通过";
                                            body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                            MailAddress[] recipientAddress = { new MailAddress(email) };

                                            ESP.Mail.MailManager.Send("考勤事由审批通过", body, true, null, recipientAddress, null, null, null);
                                        }
                                    }
                                    catch { }
                                }
                                else
                                {
                                    //发邮件
                                    try
                                    {
                                        string email = new ESP.Compatible.Employee(int.Parse(value[2])).EMail;
                                        if (!string.IsNullOrEmpty(email))
                                        {
                                            string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + key + "&flag=1";
                                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                            body += "<br><br>" + ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(value[3])).FullNameCN
                                                + "提交的考勤事由等待您的审批";
                                            body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                            MailAddress[] recipientAddress = { new MailAddress(email) };

                                            ESP.Mail.MailManager.Send("考勤事由等待审批", body, true, null, recipientAddress, null, null, null);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            // OT事由
                            else
                            {
                                //发邮件
                                try
                                {
                                    string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?singleid=" + key + "&flag=1";
                                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                    body += "<br><br>您的OT单审批已通过";
                                    body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                    MailAddress[] recipientAddress = { new MailAddress(new ESP.Compatible.Employee(int.Parse(value[2])).EMail) };

                                    ESP.Mail.MailManager.Send("考勤事由审批通过", body, true, null, recipientAddress, null, null, null);
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量审批考勤事由失败，请与系统管理员联系。');window.location='AuditList.aspx';", true);
                    return;
                }
            }
        }
    }
}
