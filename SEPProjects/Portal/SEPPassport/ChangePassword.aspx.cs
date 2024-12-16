using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Security;
using ESP.Framework.Entity;
using ESP.Configuration;
using ESP.Framework.BusinessLogic;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace PassportWeb
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isFromIntranet = !IsFromIntranet();
            this.ctlCaptcha.Enabled = isFromIntranet;
            pnlCaptcha.Visible = isFromIntranet;

            //if (Request.QueryString["noauto"] == null && Request.IsAuthenticated)
            //{
            //    UserInfo user = UserManager.Get();
            //    string returnUrl = GetReturnUrl(user.UserID);
            //    if (returnUrl != null)
            //        Response.Redirect(returnUrl);
            //    else
            //        Response.Redirect(FormsAuthentication.GetRedirectUrl(user.Username, false));
            //}
        }

        private bool IsFromIntranet()
        {
            string ip = this.Request.UserHostAddress;
            string pattern = ESP.Configuration.ConfigurationManager.SafeAppSettings["intranetIPPattern"];
            if (pattern == null || pattern.Length == 0)
                return false;

            pattern = pattern.Replace(".", "\\.");
            pattern = pattern.Replace("*", "\\d+");
            pattern = "^" + pattern + "$";
            return Regex.IsMatch(ip, pattern);
        }

        protected string GetReturnUrlPath(out string webSiteKeyToken)
        {
            int appid;
            string reqpath;
            webSiteKeyToken = null;

            if (!int.TryParse(Request["appid"], out appid))
                return null;

            if (appid <= 0 || appid == ConfigurationManager.WebSiteID)
                return null;


            WebSiteInfo webSite = WebSiteManager.Get(appid);
            if (webSite == null)
                return null;

            //webSiteKeyToken = webSite.Token;

            string urlPrefix = webSite.UrlPrefix;
            reqpath = Request["reqpath"];
            //reqpath = "/Default.aspx";


            return ESP.Utilities.UrlUtility.ConcatUrl("http://" + urlPrefix, reqpath);
        }

        protected string GetReturnUrl(int userId)
        {
            string webSiteKeyToken;
            string url = GetReturnUrlPath(out webSiteKeyToken);

            PassportAuthenticationTicket ticket = new PassportAuthenticationTicket();
            ticket.UserID = userId;
            ticket.Expired = DateTime.Now.AddSeconds(PassportAuthentication.AuthTokenExpiredInSeconds);
            string token = PassportAuthentication.EncryptTicket(ticket, webSiteKeyToken);
            token = HttpUtility.UrlEncode(token, Request.ContentEncoding);

            return ESP.Utilities.UrlUtility.AddQuery(url, PassportAuthentication.AuthTokenName, token);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string str = Request["u"];
            if (string.IsNullOrEmpty(str))
                return;

            this.Validate();

            if(!this.IsValid)
                return;

            if (!this.ctlCaptcha.IsValid)
            {
                ESP.Logging.Logger.Add("用户名 " + str + " 修改密码(F)失败，验证码无效。", "login");
                ClientScript.RegisterStartupScript(this.GetType(), "login_failed", "alert('操作失败，验证码无效或已过期。');", true);
                return;
            }

            string oldPass = OldPassword.Text;
            string newPass = Password.Text;

            bool b = UserManager.ChangePassword(str, oldPass, newPass);
            if (!b)
            {
                //this.FailureText.Text = "登录失败，请检查用户名密码是否匹配。";

                ClientScript.RegisterStartupScript(this.GetType(), "login_failed", "alert('操作失败，请检查旧密码输入是否正确。');", true);

                ESP.Logging.Logger.Add("用户名 " + str + " 修改密码(F)失败，用户名密码不匹配。", "login");

                return;
            }

            UserInfo user = UserManager.Get(str);





            
            string returnUrl = GetReturnUrl(user.UserID);
            string rurl = string.Empty;
            if (returnUrl != null)
                rurl = returnUrl;
            else
                rurl = FormsAuthentication.GetRedirectUrl(str, false);

            //弹出协议
            //IList<ESP.Purchase.Entity.sepArticleInfo> list = new ESP.Purchase.BusinessLogic.sepArticleManager().GetList(" sysuserid =" + user.UserID);
            //if (list == null || list.Count == 0)
            //{
            //    ClientScript.RegisterStartupScript(ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>ArticleIsRead('" + user.UserID + "','" + user.Username + "','" + str + "','" + this.Server.UrlEncode(rurl) + "');</script>");
            //}
            //else
            //{
                ESP.Security.PassportAuthentication.SetAuthCookie(user.UserID, user.Username);

                ESP.Logging.Logger.Add("用户名 " + str + " 修改密码(F)成功。", "login");
                Response.Redirect(rurl);
            //}

        }

    }
}
