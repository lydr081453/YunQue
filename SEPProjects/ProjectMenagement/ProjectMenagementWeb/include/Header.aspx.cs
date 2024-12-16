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
	/// Header ��ժҪ˵����
	/// </summary>
	public partial class Header : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
                ESP.Framework.Entity.UserInfo currentuser = ESP.Framework.BusinessLogic.UserManager.Get();
                lblCaption.Text = "��ǰ�û���";
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

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
