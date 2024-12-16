using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;
using System.Web.Security;
using ESP.Security;

namespace PassportWeb
{
    public partial class LoginAs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Request.UserHostAddress);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userIp = Request.UserHostAddress;
            //if (userIp.IndexOf("172.16.") < 0 && userIp != "111.205.216.71")
            //{
            //    Response.Redirect("Login.aspx");
            //}
            //bool isRememberMe = false;

            string adminUsername = this.txtAdminUsername.Text.Trim();
            string adminPassword = this.txtAdminPassword.Text;
            string loginAs = this.txtLoginAs.Text.Trim();

            UserInfo user = UserManager.Get(loginAs);
            if (user == null)
            {
                this.lblFailureText.Text = "登录失败， 用户\"" + loginAs + "\"不存在。";
                ESP.Logging.Logger.Add("用户名 " + adminUsername + " 登录为 " + loginAs + " 失败， 用户 " + loginAs + " 不存在。", "login");
                return;
            }

            UserInfo adminUser = UserManager.Get(adminUsername);
            if (adminUser == null)
            {
                this.lblFailureText.Text = "登录失败，用户\"" + adminUsername + "\"不存在。";
                ESP.Logging.Logger.Add("用户名 " + adminUsername + " 登录为 " + loginAs + " 失败， 用户 " + adminUsername + " 不存在。", "login");
                return;

            }

            int[] adminRoles = RoleManager.GetUserRoleIDs(adminUser.UserID);
            if (adminRoles == null || adminRoles.Length == 0 || Array.IndexOf<int>(adminRoles, 1) < 0)
            {
                this.lblFailureText.Text = "登录失败，用户\"" + adminUsername + "\"不是超级管理员用户。";
                ESP.Logging.Logger.Add("用户名 " + adminUsername + " 登录为 " + loginAs + " 失败， 用户 " + adminUsername + " 不是超级管理员用户。", "login");
                return;
            }

            bool b = UserManager.ValidateUser(adminUsername, adminPassword);
            if (!b)
            {
                this.lblFailureText.Text = "登录失败，请检查用户名密码是否匹配。";

                //ClientScript.RegisterStartupScript(this.GetType(), "login_failed", "alert('登录失败，请检查用户名密码是否匹配。');", true);

                ESP.Logging.Logger.Add("用户名 " + adminUsername + " 登录为 " + loginAs + " 失败， 用户名密码不匹配。", "login");

                return;
            }

            

            SettingsInfo s = SettingManager.Get();
            int portalId = s == null ? 0 : s.PortalWebSite;
            WebSiteInfo portal = WebSiteManager.Get(portalId);

            PassportAuthenticationTicket ticket = new PassportAuthenticationTicket();
            ticket.UserID = user.UserID;
            //ticket.UserName = user.Username;
            ticket.UserName = string.Empty;
            ticket.Expired = DateTime.Now.AddSeconds(PassportAuthentication.AuthTokenExpiredInSeconds);
            string token = PassportAuthentication.EncryptTicket(ticket, s.AESKey);
            token = HttpUtility.UrlEncode(token, Request.ContentEncoding);

            string url = ESP.Utilities.UrlUtility.AddQuery(portal.HttpRootUrl, PassportAuthentication.AuthTokenName, token);

            ESP.Security.PassportAuthentication.SetAuthCookie(user.UserID, user.Username);

            Response.Redirect(url);

        }
    }
}
