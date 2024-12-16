using System;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;

namespace ESP.ShunYaMyPhoto.Utility
{
    /// <summary>
    /// SendMail 的摘要说明
    /// </summary>
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
        public static void Send1(string subject, string recipient, string body, bool showPortalSite, string url, params string[] attachment)
        {
            if (string.IsNullOrEmpty(recipient))
                return;
            string[] mails = recipient.TrimEnd(',').Split(',');
            MailAddress[] recipientAddress = new MailAddress[mails.Length];
            for (int i = 0; i < recipientAddress.Length; i++)
            {
                recipientAddress[i] = new MailAddress(mails[i]);
            }
         
            ESP.Mail.MailManager.Send(subject, body, true, null, recipientAddress, null, null, null);
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