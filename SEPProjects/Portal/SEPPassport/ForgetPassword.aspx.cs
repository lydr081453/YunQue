using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Mail;
using System.Net.Mail;

namespace PassportWeb
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            UserInfo userinfo = UserManager.Get(UserName.Text.Trim());

            if (userinfo == null || userinfo.UserID <= 0)
            {
                FailureText.Text = "用户不存在。";
                return;
            }

            try
            {
                //string key = ESP.Configuration.ConfigurationManager.WebSiteKey;

                byte[] code = ESP.Utilities.CryptoUtility.GetRandomSequence();
                byte codeLength = (byte)code.Length;

                UserManager.SetResetPasswordCode(userinfo.Username, Convert.ToBase64String(code));

                //System.Security.Cryptography.SHA1CryptoServiceProvider sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                //byte[] hash = sha1.ComputeHash(System.Text.Encoding.Unicode.GetBytes(key));

                byte[] expired = ESP.Utilities.CryptoUtility.Int64ToBytes(DateTime.Now.AddDays(1).Ticks);
                byte[] userId = ESP.Utilities.CryptoUtility.Int32ToBytes(userinfo.UserID);

                byte[] buf = new byte[1 + code.Length + expired.Length + userId.Length];

                int position = 0;
                buf[position++] = codeLength;

                Array.Copy(code, 0, buf, position, code.Length);
                position += code.Length;

                Array.Copy(expired, 0, buf, position, expired.Length);
                position += expired.Length;

                Array.Copy(userId, 0, buf, position, userId.Length);
                position += userId.Length;

                SettingsInfo settings = SettingManager.Get();

                byte[] encBuf = ESP.Utilities.CryptoUtility.EncryptData(buf, settings.AESKey);
                string encEncBuf = Convert.ToBase64String(encBuf);

                WebSiteInfo webSite = WebSiteManager.Get();

                string url = ESP.Utilities.UrlUtility.ConcatUrl("http://" + webSite.UrlPrefix, "ResetPassword.aspx");
                url = ESP.Utilities.UrlUtility.AddQuery(url, "c", Server.UrlEncode(encEncBuf));

                MailAddress receipt = new MailAddress(userinfo.Email, userinfo.Username);
                IDictionary<string, object> dataSource = new Dictionary<string, object>()
                {
                    {"PasswordResetUrl", url},
                    {"UserInfo", userinfo}
                };
                MailManager.Send("~/MailTemplates/PasswordReset.aspx", dataSource, receipt);
            }
            catch
            {
                FailureText.Text = "发生未知错误，请稍后重试，由此带来的不便敬请谅解。";
            }
            pnl1.Visible = false;
            pnl2.Visible = true;
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            SubmitButton_Click(sender, e);
        }
    }
}
