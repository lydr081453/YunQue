using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESP.Web.UI
{
    /// <summary>
    /// 控制验证码显示的扩展控件。
    /// </summary>
    public class CaptchaExtender : System.Web.UI.Control
    {
        private bool _hasChecked = false;
        private bool _isValid;

        /// <summary>
        /// 输入框的ID
        /// </summary>
        public string InputControl { get; set; }

        /// <summary>
        /// 图片控件的ID
        /// </summary>
        public string ImageControl { get; set; }

        /// <summary>
        /// 刷新图片的按钮的ID
        /// </summary>
        public string ChangeImageButton { get; set; }

        /// <summary>
        /// 是否进行校验。
        /// </summary>
        public bool Enabled{ get; set; }

        /// <summary>
        /// 已重载。引发 System.Web.UI.Control.PreRender 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (!this.Enabled)
                return;

            base.OnPreRender(e);

            string script = @"
function refreshCaptchaImageButtonClick(imgId, e) {
    document.getElementById(imgId).src = ""captcha.aspx?+"" + escape(new Date()) + ""+"" + escape(Math.random());
    if (!e) e = window.event;

    if (!e) return false;

    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();

    return false;
}
function initializeCaptchaImage(buttonId, imgId) {
    var button = document.getElementById(buttonId);
    if (button.attachEvent) {
        button.attachEvent(""onclick"", function() { refreshCaptchaImageButtonClick(imgId, arguments[0]); });
    }
    else if (button.addEventListener) {
        button.addEventListener(""click"", function() { refreshCaptchaImageButtonClick(imgId, arguments[0]); }, false);
    }
    else {
        button.onclick = function() { refreshCaptchaImageButtonClick(imgId, arguments[0]); }
    }
}
";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CaptchaImageScript", script, true);
            ITextControl input = this.NamingContainer.FindControl(InputControl) as ITextControl;
            input.Text = string.Empty;

            Image image = this.NamingContainer.FindControl(ImageControl) as Image;
            image.ImageUrl = "captcha.aspx";
            image.Style[HtmlTextWriterStyle.Cursor] = "pointer";

            Control ctrl = this.NamingContainer.FindControl(ChangeImageButton);
            string script2 = "initializeCaptchaImage('" + ctrl.ClientID + "','" + image.ClientID + "');"
                + "initializeCaptchaImage('" + image.ClientID + "','" + image.ClientID + "');";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CaptchaImageScript" + this.UniqueID, script2, true);
        }

        /// <summary>
        /// 验证码是否有效。
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (!this.Enabled)
                    return true;

                if (!_hasChecked)
                {
                    ITextControl input = this.NamingContainer.FindControl(InputControl) as ITextControl;
                    _isValid = CaptchaManager.Check(input.Text);
                    _hasChecked = true;
                }

                return _isValid;
            }
        }
    }
}
