using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Management.UserManagement
{
    public partial class UserChangePwd : ESP.Web.UI.PageBase
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

            if (txtUsername.Text.Trim() == "")
            {
                lblMsg.Text = "用户名不允许为空！";
                return;
            }
            else
            {
                ESP.Framework.Entity.UserInfo users = ESP.Framework.BusinessLogic.UserManager.Get(txtUsername.Text.Trim());
                if (null != users)
                {
                    try
                    {
                        ESP.Framework.BusinessLogic.UserManager.ResetPassword(txtUsername.Text.Trim(), NewPSW.Text.Trim());
                        NewPSW.Text = "";
                        ConfirmPSW.Text = "";
                        txtUsername.Text = "";
                        lblMsg.Text = "您已经成功得更新了密码！";

                    }
                    catch
                    {
                        lblMsg.Text = "旧密码输入错误！";
                    }
                }
                else
                {
                    lblMsg.Text = "用户名错误！";
                }
            }
            //if (!SEP.BusinessControlling.UserController.ResetPassword(UserInfo.Username, NewPSW.Text.Trim()))
            //{
            //    lblMsg.Text = "旧密码输入错误！";
            //    return;
            //}
            //else
            //{
            //    ShowCompleteMessage("您已经成功得更新了密码！", SEP.Security.PassportAuthentication.GetLogoutUrl("/default.aspx"));
            ////}

        }
    }
}
