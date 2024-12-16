using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;

namespace ESP.Web.UI
{
    /// <summary>
    /// 响应验证码图片请求的 HttpHandler。
    /// </summary>
    public class CaptchaImageHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        /// <summary>
        /// 己重载。是否可重用。
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// 己重载。处理请求。
        /// </summary>
        /// <param name="context">当前请求的上下文。</param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";
            using (Bitmap img = CaptchaManager.Regenerate())
            {
                img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        #endregion
    }
}
