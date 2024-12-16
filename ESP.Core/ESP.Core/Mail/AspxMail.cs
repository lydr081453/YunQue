using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Reflection;
using System.Web.UI;

namespace ESP.Mail
{
    /// <summary>
    /// Asp.Net 页面格式的邮件模板基类。
    /// </summary>
    public class AspxMail : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            Response.End();

            this.Theme = null;
        }
        /// <summary>
        /// 初始化模板实例
        /// </summary>
        /// <param name="output">用于输出的文本流。</param>
        /// <param name="dataSource">数据源。</param>
        /// <param name="mailMessage">要发送的邮件。</param>
        /// <param name="subject">返回邮件的主题。</param>
        public void Render(System.IO.TextWriter output, object dataSource, MailMessage mailMessage, out string subject)
        {
            this.Theme = null;
            this.Title = "";

            this.DataSource = dataSource;
            this.MailMessage = mailMessage;

            Type pageType = this.GetType();

            FieldInfo stringResource = pageType.GetField("__stringResource", BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo buildControlTree = pageType.GetMethod("__BuildControlTree", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo renderChildren = pageType.GetMethod("RenderChildren", BindingFlags.NonPublic | BindingFlags.Instance);

            if (stringResource != null)
            {
                MethodInfo setStringResourcePointer = pageType.GetMethod("SetStringResourcePointer", BindingFlags.NonPublic | BindingFlags.Instance);
                object stringResourceValue = stringResource.GetValue(null);
                setStringResourcePointer.Invoke(this, new object[] { stringResourceValue, 0 });
            }
            buildControlTree.Invoke(this, new object[] { this });

            MasterPage master = this.Master;

            Html32TextWriter htmlWriter = new Html32TextWriter(output);
            this.RenderChildren(htmlWriter);
            try
            {
                subject = this.Title;
            }
            catch (System.NullReferenceException)
            {
                subject = string.Empty;
            }

            htmlWriter.Flush();
        }

        /// <summary>
        /// 要绑定到邮件内容中的数据源。
        /// </summary>
        public object DataSource { get; set; }

        /// <summary>
        /// 要发送的邮件，其中主题和内容尚未设置。
        /// </summary>
        public MailMessage MailMessage { get; private set; }
    }
}
