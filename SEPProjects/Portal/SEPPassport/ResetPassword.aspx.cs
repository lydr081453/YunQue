using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace PassportWeb
{
    public partial class PasswordReset : System.Web.UI.Page
    {
        private UserInfo _user = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                pnl1.Visible = true;
                pnl2.Visible = false;
                pnl3.Visible = false;
            }
            if (this.ViewState["Finish"] == null)
            {
                UserInfo user;
                if (CheckCode(out user))
                {
                    _user = user;
                }
                else
                {
                    pnl1.Visible = false;
                    pnl2.Visible = true;
                    pnl3.Visible = false;
                }
            }
        }

        private bool CheckCode(out UserInfo user)
        {
            user = null;
            string code = Request["c"];
            if (string.IsNullOrEmpty(code))
                return false;

            try
            {
                SettingsInfo settings = SettingManager.Get();

                //string key = ESP.Configuration.ConfigurationManager.WebSiteKey;

                byte[] encBuf = Convert.FromBase64String(code);
                byte[] buf = ESP.Utilities.CryptoUtility.DecryptData(encBuf, settings.AESKey);
                if (buf == null || buf.Length == 0)
                    return false;

                //SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                //byte[] hash = sha1.ComputeHash(System.Text.Encoding.Unicode.GetBytes(key));
                //if (buf.Length < hash.Length)
                //    return false;

                int position = 0;

                int validCodeLength = buf[position++];
                if (validCodeLength > buf.Length || validCodeLength == 0)
                    return false;

                byte[] validCode = new byte[validCodeLength];
                Array.Copy(buf, position, validCode, 0, validCodeLength);
                position += validCodeLength;

                long expired = ESP.Utilities.CryptoUtility.Int64FromBytes(buf, ref position);

                DateTime expiredTime = new DateTime(expired);
                double days = (expiredTime - DateTime.Now).TotalDays;
                if (days < 0)
                    return false;

                int uid = ESP.Utilities.CryptoUtility.Int32FromBytes(buf, ref position);

                if (uid <= 0)
                    return false;

                UserInfo u = UserManager.Get(uid);

                if (u == null)
                    return false;

                string validCodeB64 = UserManager.GetResetPasswordCode(u.Username);
                if (validCodeB64 == null || validCodeB64.Length == 0)
                    return false;

                byte[] validCode2 = Convert.FromBase64String(validCodeB64);
                if (validCode2 == null || validCode2.Length == 0)
                    return false;

                if (validCode.Length != validCode2.Length)
                    return false;

                for (int i = 0; i < validCode.Length; i++)
                {
                    if (validCode[i] != validCode2[i])
                        return false;
                }

                user = u;
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            if (_user == null)
                return;

            if (!Page.IsValid)
                return;

            string password = NewPassword.Text;

            UserManager.ResetPassword(_user.Username, password);

            this.ViewState["Finish"] = true;
            pnl1.Visible = false;
            pnl2.Visible = false;
            pnl3.Visible = true;
        }
    }
}
