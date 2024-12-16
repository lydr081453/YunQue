using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SEPAdmin
{
    public partial class PSWChange : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            if (NewPSW.Text.Trim() == "")
            {
                lblMsg.Text = "新密码不允许为空！";
                return;
            }

            if (NewPSW.Text.Trim() != ConfirmPSW.Text.Trim())
            {
                lblMsg.Text = "新密码与确认密码不同，请重新输入！";
                return;
            }

            if (!ESP.Framework.BusinessLogic.UserManager.ChangePassword(UserInfo.Username, OldPSW.Text.Trim(), NewPSW.Text.Trim()))
            {
                lblMsg.Text = "旧密码输入错误！";
                return;
            }
            else
            {
                ShowCompleteMessage("您已经成功得更新了密码！", ESP.Security.PassportAuthentication.GetLogoutUrl("/default.aspx"));
            }

        }
    }
}
