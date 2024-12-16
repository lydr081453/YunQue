using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using ESP.Compatible;
using ESP.Framework.BusinessLogic;

public partial class PSWChange_PSWChange : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
            //SecurityMode = PageSecurityMode.None;
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
            string oldPwd = OldPSW.Text;
            string newPwd = NewPSW.Text;
            string cfmPwd = ConfirmPSW.Text;

            if (newPwd.Length == 0)
            {
                lblMsg.Text = "新密码不允许为空！";
                return;
            }

            if (newPwd != cfmPwd)
            {
                lblMsg.Text = "新密码与确认密码不同，请重新输入！";
                return;
            }

            //if(!UserManager.VerifyPassword(CurrentUserCode,OldPSW.Text.Trim()))
            if (!ESP.Framework.BusinessLogic.UserManager.ChangePassword(this.UserInfo.Username, oldPwd, newPwd))
            {
                lblMsg.Text = "旧密码输入错误！";
                return;
            }

            ShowMessage("主页", "您已经成功得更新了密码！", "/include/page/DefaultWorkSpace.aspx");

            //if (NewPSW.Text.Trim() != ConfirmPSW.Text.Trim())
            //{
            //    lblMsg.Text = "新密码与确认密码不同，请重新输入！";
            //    return;
            //}

            //if(!UserController.VerifyPassword(CurrentUserCode,OldPSW.Text.Trim()))
            //{
            //    lblMsg.Text = "旧密码输入错误！";
            //    return;
            //}

            //if(NewPSW.Text.Trim()=="")
            //{
            //    lblMsg.Text = "新密码不允许为空！";
            //    return;
            //}

            //string sql = "update F_Employee set Password = '" + Utils.DoubleQuote(NewPSW.Text.Trim()) + "' " +
            //    " where UserCode = '" + CurrentUserCode + "'";

            //if(1!=SqlHelper.ExecuteNonQuery(ConfigManager.ConfigSqlConnectionString,sql))
            //{
            //    lblMsg.Text = "更新密码出错！";
            //}
            //else
            //{
            //    ShowMessage("主页","您已经成功得更新了密码！","/include/page/DefaultWorkSpace.aspx");
            //}
		}
	
}
