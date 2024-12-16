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

namespace FrameSite.Web.ErrorMsg
{
	/// <summary>
	/// ErrorMsg ��ժҪ˵����
	/// </summary>
	public partial class ErrorMsg : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
				if(Request.Params["backurl"]!=null)
				{	
					BackUrl.HRef = Request.Params["backurl"];
				}
				if(Request.Params["back"]!=null)
				{
					BackUrl.HRef = "javascript:history.back(" + Request.Params["back"] + ")";
				}
				if(Request.Params["title"]!=null)
				{
					BackUrl.Title = Request.Params["title"];
				}

				MsgContent.InnerHtml = Request.Params["message"];
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
