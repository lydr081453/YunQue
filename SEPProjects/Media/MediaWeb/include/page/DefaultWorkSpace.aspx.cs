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
	/// DefaultWorkSpace ��ժҪ˵����
	/// </summary>
	public partial class DefaultWorkSpace : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// ������ʷ
				//divHistory.InnerHtml = LoggerManager.GetVisitHistorySeqHtml(CurrentUserCode);

				//�����¼
				//divApp.InnerHtml = SSOHelper.GetUserRegistrySSOHtml(SessionManager.GetUser(this.Session).ITCode);

				//֪ͨ
				//divNotify.InnerHtml = NotifyManager.GetNotifyHtml(SessionManager.GetUser(this.Session).ITCode,Server.MapPath("/include/xslt/NotifyBrow.xslt"));

                UserInfo user = ESP.Framework.BusinessLogic.UserManager.Get();


                lbDate.Text = "������&nbsp; &nbsp;" + DateTime.Now.ToLongDateString() + "&nbsp; &nbsp; &nbsp; &nbsp;" + DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn")); ;
                if (user != null)
                {
                    lblCaption.Text = "��ӭ����" + user.FullNameCN;
                }

			}

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