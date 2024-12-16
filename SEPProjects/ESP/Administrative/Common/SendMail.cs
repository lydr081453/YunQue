using System;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;

namespace ESP.Administrative.Common
{
    /// <summary>
    /// SendMail 的摘要说明
    /// </summary>
    public class SendMail
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
            int count = 0;
            for (int k = 0; k < attachment.Length; k++)
            {
                if (!string.IsNullOrEmpty(attachment[k]))
                {
                    count++;
                }
            }
            if (attachment != null && count > 0)
            {
                attachments = new Attachment[count];
                for (int i = 0; i < attachment.Length; i++)
                {
                    if (string.IsNullOrEmpty(attachment[i]))
                        continue;
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

        public static string ScreenScrapeHtml(string url)
        {
            WebRequest objRequest = System.Net.HttpWebRequest.Create(url + "&InternalPassword=f67u7b6i8asdf");
            StreamReader sr = new StreamReader(objRequest.GetResponse().GetResponseStream(), Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            return result;
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