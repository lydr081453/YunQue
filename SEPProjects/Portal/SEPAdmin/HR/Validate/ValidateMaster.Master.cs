using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Validate
{
    public partial class ValidateMaster : System.Web.UI.MasterPage
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.IsPostBack && this.IsLoggedIn == false)
            {

                if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("011", (((ESP.Web.UI.PageBase)Page).ModuleInfo).ModuleID, (((ESP.Web.UI.PageBase)Page).UserInfo).UserID))
                {
                    this.extLoginForm.Show();
                }
            }
        }

        private bool IsLoggedIn
        {
            get
            {
                return true.Equals(this.Session["IsLoggedIn"]);
            }
            set
            {
                this.Session["IsLoggedIn"] = value;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {            
            extLoginForm.Show();
            int id = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
            if (id > 0)
            {
                if (new ESP.HumanResource.BusinessLogic.UserValidateManager().IsUserValidate(id, this.txtPassword.Text))
                {
                    this.IsLoggedIn = true;
                    this.extLoginForm.Hide();
                }
                else
                {
                    this.IsLoggedIn = false;
                    lblMsg.Text = "您所输入的社保验证密码不正确！";
                    this.extLoginForm.Show();
                }
            }
        }

        protected void btnReCreatePwd_Click(object sender, EventArgs e)
        {
            int id = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
            if (id > 0)
            {
                new ESP.HumanResource.BusinessLogic.UserValidateManager().AddOrUpdate(id);

                lblMsg.Text = "您的社保验证密码已发送至您的帐户邮箱，请查收并妥善保管！";                
                this.extLoginForm.Show();
            }
        }
    }    
}
