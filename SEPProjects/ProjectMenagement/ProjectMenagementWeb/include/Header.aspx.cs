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

namespace FrameSite.Web.include.page
{
	/// <summary>
	/// Header 的摘要说明。
	/// </summary>
	public partial class Header : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{
                ESP.Framework.Entity.UserInfo currentuser = ESP.Framework.BusinessLogic.UserManager.Get();
                lblCaption.Text = "当前用户：";
                if (currentuser != null)
                    lblCaption.Text += currentuser.FullNameCN;
			}
		}

        protected void signout(object sender, EventArgs e)
        {
            string url = ESP.Security.PassportAuthentication.GetLogoutUrl(ConfigManager.SiteLogonUrl);
            Response.Redirect(url);

            //SessionManager.SetSignOut(this.Session);
            //Response.Redirect(ConfigManager.SiteLogonUrl);
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
