using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;
using System.Web.Security;
using ESP.Configuration;
using ESP.Security;
using System.Text.RegularExpressions;

namespace PassportWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isFromInternet = !IsFromIntranet();
            //this.ctlCaptcha.Enabled = isFromInternet;
            //pnlCaptcha.Visible = isFromInternet;
            //Panel1.Visible = !isFromInternet;

            if (Request.QueryString["noauto"] == null && Request.IsAuthenticated)
            {
                UserInfo user = UserManager.Get();
                string returnUrl = GetReturnUrl(user.UserID);
                if (returnUrl != null)
                    Response.Redirect(returnUrl);
                else
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(user.Username, false));
            }
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


        protected void LoginButton_Click(object sender, ImageClickEventArgs e)
        {
            //bool isRememberMe = RememberMe.Checked;
            //bool isRememberMe = false;

            //if (!this.ctlCaptcha.IsValid)
            //{
            //    // ESP.Logging.Logger.Add("用户名 " + UserCode.Text + " 登录失败，验证码无效。", "login");
            //    ClientScript.RegisterStartupScript(this.GetType(), "login_failed", "alert('登录失败，验证码无效或已过期。');", true);
            //    return;
            //}
            string password = Password.Text;
            string username = UserCode.Text;

            UserInfo user = UserManager.Get(UserCode.Text);
            if (user == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login_failed", "alert('登录失败，用户名无效。');", true);
                return;
            }
            //IList<ESP.Purchase.Entity.sepArticleInfo> list = new ESP.Purchase.BusinessLogic.sepArticleManager().GetList(" sysuserid =" + user.UserID);
            ESP.HumanResource.Entity.DimissionFormInfo dimissonModel = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(user.UserID);

            if (dimissonModel != null && dimissonModel.Status != (int)ESP.HumanResource.Common.DimissionFormStatus.NotSubmit && dimissonModel.LastDay.Value.AddDays(1) < DateTime.Now)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login_failed", "alert('验证通过，请检查账号状态是否有效。');", true);
                return;
            }

            bool b = UserManager.ValidateUser(UserCode.Text, Password.Text);
            if (!b)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login_failed", "alert('登录失败，请检查用户名密码是否匹配。');", true);

                return;
            }


            if (password == "password" || password.Length < 8 || user.LastPasswordChangedDate < DateTime.Now.AddDays(-90))
            {
                string appid = Request["appid"];
                string reqpath = Request["reqpath"];

                string url;
                if (appid != null && reqpath != null)
                {
                    url = "ChangePassword.aspx?u=" + Server.UrlEncode(username) + "&appid=" + Server.UrlEncode(appid) + "&reqpath=" + Server.UrlEncode(reqpath);
                }
                else
                {
                    url = "ChangePassword.aspx?u=" + Server.UrlEncode(username);
                }

                Response.Redirect(url);
                return;
            }

            string returnUrl = GetReturnUrl(user.UserID);
            string rurl = string.Empty;
            if (returnUrl != null)
                rurl = returnUrl;
            else
                rurl = FormsAuthentication.GetRedirectUrl(UserCode.Text, false);

            //弹出协议

            //if (list == null || list.Count == 0)
            //{
            //    ClientScript.RegisterStartupScript(ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>ArticleIsRead('" + user.UserID + "','" + user.Username + "','" + UserCode.Text + "','" + this.Server.UrlEncode(rurl) + "');</script>");
            //}
            //else
            //{
                ESP.Security.PassportAuthentication.SetAuthCookie(user.UserID, user.Username);
                Response.Redirect(rurl);
           // }


        }
    }
}
