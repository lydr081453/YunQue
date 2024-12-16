using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin
{
    public partial class Header : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            if (!IsPostBack)
            {
                if (UserInfo == null)
                {
                    lblCaption.Text = "匿名用户";
                  //  lnkSignOut.NavigateUrl = SEP.Security.PassportAuthentication.GetLoginUrl();
                  //  lnkSignOut.Text = "登录";
                }
                else
                {
                    lblCaption.Text = UserInfo.FullNameCN;
                 //   lnkSignOut.NavigateUrl = SEP.Security.PassportAuthentication.GetLogoutUrl();
                  //  lnkSignOut.Text = "注销";
                }
            }
        }

        protected void ImageButton_Click(object sender, EventArgs e)
        {
           string logout =  ESP.Security.PassportAuthentication.GetLogoutUrl("/default.aspx");
           Response.Redirect(logout);
            
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
    }
}
