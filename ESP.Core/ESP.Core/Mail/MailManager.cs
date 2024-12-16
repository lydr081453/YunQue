using System;
using System.Collections.Generic;
using System.Text;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using System.Net.Mail;
using System.Web.Compilation;
using System.Web.UI;
using System.IO;
using System.Reflection;
using System.Collections.Specialized;
using System.Collections;

namespace ESP.Mail
{
    public static class MailManager
    {
        /// <summary>
        /// 根据模板和数据源生成邮件并发送
        /// </summary>
        /// <param name="template">邮件模板的虚拟路径。</param>
        /// <param name="dataSource">单数据源对象，使用默认键值 DataSource。</param>
        /// <param name="recipients">收件人地址。</param>
        public static void Send(string template, object dataSource, params MailAddress[] recipients)
        {
            Send(template, dataSource, null, recipients, null, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template">邮件模板的虚拟路径。</param>
        /// <param name="dataSource">单数据源对象，使用默认键值 DataSource。</param>
        /// <param name="attachment">要附加到附件中的文件的物理路径。</param>
        /// <param name="recipients">收件人地址。</param>
        public static void Send(string template, object dataSource, string attachment, params MailAddress[] recipients)
        {
            Send(template, dataSource, null, recipients, null, null, new Attachment[] { new Attachment(attachment) });
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件标题。</param>
        /// <param name="body">邮件内容。</param>
        /// <param name="isHtml">邮件内容是否为 Html 格式。</param>
        /// <param name="recipients">邮件收件人列表。</param>
        public static void Send(string subject, string body, bool isHtml, params MailAddress[] recipients)
        {
            Send(subject, body, isHtml, null, recipients, null, null, null);
        }


        /// <summary>
        /// 根据模板和数据源生成邮件并发送
        /// </summary>
        /// <param name="template">邮件模板的虚拟路径。</param>
        /// <param name="dataSource">数据源对象。</param>
        /// <param name="replyTo">邮件回复地址。</param>
        /// <param name="recipients">邮件收件人列表。</param>
        /// <param name="carbonCopes">邮件抄送列表。</param>
        /// <param name="blindCarbonCopes">邮件暗送列表。</param>
        /// <param name="attachments">附件列表</param>
        public static void Send(string template, object dataSource, MailAddress replyTo, MailAddress[] recipients, MailAddress[] carbonCopes, MailAddress[] blindCarbonCopes, Attachment[] attachments)
        {
            Send(template, dataSource, null, replyTo, recipients, carbonCopes, blindCarbonCopes, attachments);
        }

        /// <summary>
        /// 根据模板和数据源生成邮件并发送
        /// </summary>
        /// <param name="template">邮件模板的虚拟路径。</param>
        /// <param name="dataSource">数据源对象。</param>
        /// <param name="from">发件人地址。</param>
        /// <param name="replyTo">邮件回复地址。</param>
        /// <param name="recipients">邮件收件人列表。</param>
        /// <param name="carbonCopes">邮件抄送列表。</param>
        /// <param name="blindCarbonCopes">邮件暗送列表。</param>
        /// <param name="attachments">附件列表</param>
        public static void Send(string template, object dataSource, MailAddress from, MailAddress replyTo, MailAddress[] recipients, MailAddress[] carbonCopes, MailAddress[] blindCarbonCopes, Attachment[] attachments)
        {
            SettingsInfo settings = SettingManager.Get();

            MailMessage msg = new MailMessage();
            NameValueCollection attachmentContentIdMappings = new NameValueCollection();

            if (attachments != null && attachments.Length > 0)
            {
                foreach (Attachment attachment in attachments)
                {
                    msg.Attachments.Add(attachment);
                }
            }

            foreach (MailAddress mailAddress in recipients)
            {
                msg.To.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName, System.Text.Encoding.UTF8));
            }

            if (carbonCopes != null && carbonCopes.Length > 0)
            {
                foreach (MailAddress mailAddress in carbonCopes)
                {
                    msg.CC.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName, System.Text.Encoding.UTF8));
                }
            }

            if (blindCarbonCopes != null && blindCarbonCopes.Length > 0)
            {
                foreach (MailAddress mailAddress in blindCarbonCopes)
                {
                    msg.Bcc.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName, System.Text.Encoding.UTF8));
                }
            }

            if (replyTo != null)
            {
                msg.ReplyTo = replyTo;
            }

            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;

            if (from == null)
                msg.From = new MailAddress(settings.MailFrom, WebSiteManager.Get().WebSiteName, System.Text.Encoding.UTF8);
            else
                msg.From = new MailAddress(from.Address, from.DisplayName, System.Text.Encoding.UTF8);

            string subject;
            string body = RenderTemplate(template, dataSource, msg, out subject);

            msg.Subject = subject;
            msg.Body = body;


            SmtpClient client = new System.Net.Mail.SmtpClient(settings.SmtpServer, settings.SmtpServerPort);
            if (settings.SmtpUsername != null && settings.SmtpUsername.Length > 0)
                client.Credentials = new System.Net.NetworkCredential(settings.SmtpUsername, settings.SmtpPassword);


            client.Send(msg);
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件标题。</param>
        /// <param name="body">邮件内容。</param>
        /// <param name="isHtml">邮件内容是否为 Html 格式。</param>
        /// <param name="replyTo">邮件回复地址。</param>
        /// <param name="recipients">邮件收件人列表。</param>
        /// <param name="carbonCopes">邮件抄送列表。</param>
        /// <param name="blindCarbonCopes">邮件暗送列表。</param>
        /// <param name="attachments">附件列表</param>
        public static void Send(string subject, string body, bool isHtml, MailAddress replyTo, MailAddress[] recipients, MailAddress[] carbonCopes, MailAddress[] blindCarbonCopes, Attachment[] attachments)
        {
            Send(subject, body, isHtml, null, replyTo, recipients, carbonCopes, blindCarbonCopes, attachments);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件标题。</param>
        /// <param name="body">邮件内容。</param>
        /// <param name="isHtml">邮件内容是否为 Html 格式。</param>
        /// <param name="from">发件人地址。。</param>
        /// <param name="replyTo">邮件回复地址。</param>
        /// <param name="recipients">邮件收件人列表。</param>
        /// <param name="carbonCopes">邮件抄送列表。</param>
        /// <param name="blindCarbonCopes">邮件暗送列表。</param>
        /// <param name="attachments">附件列表</param>
        public static void Send(string subject, string body, bool isHtml, MailAddress from, MailAddress replyTo, MailAddress[] recipients, MailAddress[] carbonCopes, MailAddress[] blindCarbonCopes, Attachment[] attachments)

        {
            SettingsInfo settings = SettingManager.Get();

            MailMessage msg = new MailMessage();
            NameValueCollection attachmentContentIdMappings = new NameValueCollection();

            if (attachments != null && attachments.Length > 0)
            {
                foreach (Attachment attachment in attachments)
                {
                    msg.Attachments.Add(attachment);
                }
            }

            foreach (MailAddress mailAddress in recipients)
            {
                msg.To.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName, System.Text.Encoding.UTF8));
            }

            if (carbonCopes != null && carbonCopes.Length > 0)
            {
                foreach (MailAddress mailAddress in carbonCopes)
                {
                    msg.To.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName, System.Text.Encoding.UTF8));
                }
            }

            if (blindCarbonCopes != null && blindCarbonCopes.Length > 0)
            {
                foreach (MailAddress mailAddress in blindCarbonCopes)
                {
                    msg.To.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName, System.Text.Encoding.UTF8));
                }
            }

            if (replyTo != null)
            {
                msg.ReplyTo = replyTo;
            }

            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;



            if (from == null)
                msg.From = new MailAddress(settings.MailFrom, WebSiteManager.Get().WebSiteName, System.Text.Encoding.UTF8);
            else
                msg.From = new MailAddress(from.Address, from.DisplayName, System.Text.Encoding.UTF8); 

            msg.Subject = subject;
            msg.Body = body;


            SmtpClient client = new System.Net.Mail.SmtpClient(settings.SmtpServer, settings.SmtpServerPort);
            if (settings.SmtpUsername != null && settings.SmtpUsername.Length > 0)
                client.Credentials = new System.Net.NetworkCredential(settings.SmtpUsername, settings.SmtpPassword);

            client.Send(msg);
        }

        /// <summary>
        /// 根据模板和数据源生成 HTML 代码。
        /// </summary>
        /// <param name="template">邮件模板的虚拟路径。</param>
        /// <param name="dataSource">单数据源对象，使用默认键值 DataSource。</param>
        /// <param name="mailMessage">邮件消息对象。</param>
        /// <param name="subject">邮件的标题。</param>
        /// <returns>生成的 HTML 代码。</returns>
        private static string RenderTemplate(string template, object dataSource, MailMessage mailMessage, out string subject)
        {
            AspxMail inst = null;
            inst = (AspxMail)BuildManager.CreateInstanceFromVirtualPath(template, typeof(AspxMail));



            using (StringWriter writer = new StringWriter())
            {
                inst.Render(writer, dataSource, mailMessage, out subject);
                writer.Flush();
                return writer.GetStringBuilder().ToString();
            }


        }
    }
}
