using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;

namespace Portal.WebSite.Account
{
    public partial class Password : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.UserInfo == null)
            {
                _SavePasswordError = "你尚末登录，无法修改密码。";
                return;
            }

            if (IsPostBack)
            {
                if (Request["SavePassword"] != null)
                {
                    SavePassword();
                }
            }
        }

        protected bool SavePassword()
        {
            string oldPwd = Request["OldPassword"];
            string newPwd = Request["NewPassword"];
            string newPwdCfm = Request["NewPasswordConfirm"];

            _SavePasswordError = string.Empty;

            if (newPwd != newPwdCfm)
            {
                _SavePasswordError = "两次新密码输入不一致。";
                return false;
            }

            //if (!UserController.ValidateUser(this.UserInfo.Username, oldPwd))
            //{
            //    _SavePasswordError = "旧密码不正确，请检查是否输入有误。";
            //    return false;
            //}

            if (!UserManager.ChangePassword(this.UserInfo.Username, oldPwd, newPwd))
            {
                _SavePasswordError = "当前密码不正确，请检查是否输入有误。";
                return false;
            }

            _SavePasswordError = "密码修改成功。";

            return true;
        }


        string _SavePasswordError = string.Empty;
        protected string SavePasswordError
        {
            get
            {
                return _SavePasswordError;
            }
        }
    }
}
