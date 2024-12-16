using System;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Data;
using System.Collections.Generic;

namespace ESP.HumanResource.Common
{
    public class SendMailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="recipient">收件人</param>
        /// <param name="body">内容</param>
        /// <param name="showPortalSite">是否显示站点链接</param>
        /// <param name="atts">附件</param>
        public static void Send1(string subject, string recipient, string body, bool showPortalSite, params string[] attachment)
        {
            if (string.IsNullOrEmpty(recipient))
                return;
            MailAddress[] recipientAddress = { new MailAddress(recipient.Trim()) };
            Attachment[] attachments = null;

            if (attachment != null && attachment.Length > 0)
            {
                attachments = new Attachment[attachment.Length];
                for (int i = 0; i < attachment.Length; i++)
                {
                    attachments[i] = new Attachment(attachment[i]);
                }
            }
            if (showPortalSite)
                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "</a>";
            ESP.Mail.MailManager.Send(subject, body, true, null, recipientAddress, null, null, attachments);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="recipient">收件人</param>
        /// <param name="body">内容</param>
        /// <param name="showPortalSite">是否显示站点链接</param>
        /// <param name="atts">附件</param>
        public static void Send1(string subject, string recipient, string body, bool showPortalSite, Hashtable atts)
        {
            if (string.IsNullOrEmpty(recipient))
                return;

            MailAddress[] recipientAddress = { new MailAddress(recipient.Trim()) };
            Attachment[] attachments = null;
            if (atts != null)
            {
                attachments = new Attachment[atts.Count];
                int index = 0;
                foreach (DictionaryEntry ic in atts)
                {
                    Attachment attach = new Attachment(ic.Value.ToString());
                    if (ic.Key.ToString().Trim() != "")
                        attach.Name = ic.Key.ToString();
                    attachments[index] = attach;
                    index++;
                }
            }
            if (showPortalSite)
                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "</a>";
            ESP.Mail.MailManager.Send(subject, body, true, null, recipientAddress, null, null, attachments);
        }

        /// <summary>
        /// 发送密送邮件
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="recipientAddress">密送收件人</param>
        /// <param name="body">内容</param>       
        /// <param name="atts">附件</param>
        public static void SendMail(string subject, string recipient, string body, Hashtable atts)
        {
            if (recipient == null)
                return;

            string[] rec = recipient.Split(',');
            List<MailAddress> recipientAddress = new List<MailAddress>();

            for (int i = 0; i < rec.Length; i++)
            {
                if (!string.IsNullOrEmpty(rec[i].Trim()))
                {
                    recipientAddress.Add(new MailAddress(rec[i].Trim()));
                }
            }

            MailAddress[] nullAddress = { new MailAddress("hr@shunyagroup.com") };
            Attachment[] attachments = null;
            if (atts != null)
            {
                attachments = new Attachment[atts.Count];
                int index = 0;
                foreach (DictionaryEntry ic in atts)
                {
                    Attachment attach = new Attachment(ic.Value.ToString());
                    if (ic.Key.ToString().Trim() != "")
                        attach.Name = ic.Key.ToString();
                    attachments[index] = attach;
                    
                    index++;
                }
            }
            ESP.Mail.MailManager.Send(subject, body, true, null, nullAddress, null, recipientAddress.ToArray(), attachments);
        }

        public static string ScreenScrapeHtml(string url)
        {
            WebRequest objRequest = System.Net.HttpWebRequest.Create(url + "&InternalPassword=f67u7b6i8asdf");
            WebResponse objResponse = objRequest.GetResponse();
            try
            {
                using (Stream stream = objResponse.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                    {
                        string result = sr.ReadToEnd();
                        return result;
                    }
                }
            }
            finally
            {
                objResponse.Close();
            }
        }

        //public static void SendMailToUser(int userid, string pwd)
        //{
        //    if (!string.IsNullOrEmpty(pwd) && userid > 0)
        //    {
        //        string msgBody = "您新的社保验证密码为：" + pwd;

        //        MailAddress recipientAddress = new MailAddress(new ESP.Compatible.Employee(userid).EMail);

        //        ESP.Mail.MailManager.Send("社保验证密码（极为重要）", msgBody, false, recipientAddress);
        //    }
        //}

        public static void SendDimissionMail(string year, string month)
        {
            try
            {
                string date = year + "-" + month;
                string recipientAddress = "";
                string str1 = @"
<html xmlns='http://www.w3.org/1999/xhtml'>
<head id='Head1'>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
<style  type='text/css'>
body{	
	background-color: #ececec;
	font-family:Arial, Helvetica, sans-serif;
	font-size:12px;
	color:#585858;
	line-height:170%;
}

</style>

</head>
<body>

<table width='900' border='0' align='center' cellpadding='0' cellspacing='10' bgcolor='#FFFFFF'>
  <tr>
    <td></td>
  </tr>
  <tr>
    <td style='background-repeat:repeat-x; padding:0 10px 20px 10px;'>各位同事,<br />
截止到 {0}年{1}月，以下人员已确认离职：<br />
      <br />
      <table width='100%' border='0' cellpadding='0' cellspacing='1' bgcolor='#d6d6d6'>
        <tr>
          <td height='28' align='center' bgcolor='#F4F4F4'><strong>姓名</strong></td>
          <td align='center' bgcolor='#F4F4F4'><strong>组别</strong></td>
          <td align='center' bgcolor='#F4F4F4'><strong>职位</strong></td>
          <td align='center' bgcolor='#F4F4F4'><strong>离职日期（考勤日）</strong></td>
          <td align='center' bgcolor='#F4F4F4'><strong>医疗缴存至</strong></td>
          <td align='center' bgcolor='#F4F4F4'><strong>养老、失业、工伤、生育缴存至</strong></td>          
        </tr>
        {2}  
      </table>
    </td>
    </tr>
    <tr><td style='height:20px'></td></tr> 
    </table>
    </form>
</body>
</html>";
                string str2 = "";

                List<ESP.HumanResource.Entity.UsersInfo> list = ESP.HumanResource.BusinessLogic.UsersManager.GetUserList(ESP.HumanResource.Common.Status.DimissionSendMail);
                foreach (ESP.HumanResource.Entity.UsersInfo info in list)
                {
                    if (!string.IsNullOrEmpty(info.Email))
                    {
                        recipientAddress += info.Email.Trim() + " ,";
                    }
                }
                if (recipientAddress.Trim().Length > 1)
                {
                    recipientAddress = recipientAddress.Substring(0, recipientAddress.Length - 1);
                }

                List<ESP.HumanResource.Entity.DimissionInfo> dimlist = ESP.HumanResource.BusinessLogic.DimissionManager.GetModelList(string.Format(" and dimissionDate >='{0}' and dimissionDate <='{1}'", date + "-01", date + "-" + DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));
                foreach (ESP.HumanResource.Entity.DimissionInfo info in dimlist)
                {
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eip = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + info.userId);
                    ESP.HumanResource.Entity.SnapshotsInfo snap = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetModel(info.snapshotsId);
                    str2 += string.Format(@" <tr>
                        <td height='28' align='center' bgcolor='#FFFFFF'>
                            {0}
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            {1}
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                             {2}
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            {3}
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            {4}
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            {5}
                        </td>                        
                    </tr>   ",
                             info.userName,
                             eip.Count > 0 ? eip[0].CompanyName + "-" + eip[0].DepartmentName + "-" + eip[0].GroupName : "",
                             eip.Count > 0 ? eip[0].DepartmentPositionName : "",
                             info.dimissionDate.ToString("yyyy-MM-dd"),
                             snap != null ? snap.medicalInsuranceEndTime.Month.ToString() : "",
                             snap != null ? snap.endowmentInsuranceEndTime.Month.ToString() : "");
                }
                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(string.Format(str1, year, month, str2));

                SendMailHelper.SendMail(date + "离职员工信息", recipientAddress, body, null);
                ESP.Logging.Logger.Add("每月底离职人员邮件发送成功", "Human", ESP.Logging.LogLevel.Information);
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("每月底离职人员邮件发送失败", "Human", ESP.Logging.LogLevel.Error, ex);
            }
        }
    }

    public class MailException : Exception
    {
        private string msg;
        public MailException(string Msg)
            : base()
        {
            msg = Msg;
        }
        public override string ToString()
        {
            return msg;
        }
    }
}
