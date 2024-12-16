using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

using ESP.Compatible;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace FrameSite.Include.Page
{
	/// <summary>
	/// DefaultWorkSpace 的摘要说明。
	/// </summary>
	public partial class DefaultWorkSpace : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// 访问历史
				//divHistory.InnerHtml = LoggerManager.GetVisitHistorySeqHtml(CurrentUserCode);

				//单点登录
				//divApp.InnerHtml = SSOHelper.GetUserRegistrySSOHtml(SessionManager.GetUser(this.Session).ITCode);

				//通知
				//divNotify.InnerHtml = NotifyManager.GetNotifyHtml(SessionManager.GetUser(this.Session).ITCode,Server.MapPath("/include/xslt/NotifyBrow.xslt"));

                UserInfo user = ESP.Framework.BusinessLogic.UserManager.Get();


                lbDate.Text = "今天是&nbsp; &nbsp;" + DateTime.Now.ToLongDateString() + "&nbsp; &nbsp; &nbsp; &nbsp;" + DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn")); ;
                if (user != null)
                {
                    lblCaption.Text = "欢迎您，" + user.FullNameCN;
                }

			}

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