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
using System.IO;

namespace FrameSite.Web.Download
{
	/// <summary>
	/// DownloadFile ��ժҪ˵����
	/// </summary>
	public partial class DownloadFile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
				string fn=Server.UrlDecode(Request["FileName"] as String);

				/*
				Response.Clear(); 
				Response.Buffer= true; 
				Response.Charset="GB2312";    
				Response.AppendHeader("Content-Disposition","attachment;filename=menu.xml"); 
				Response.ContentEncoding=System.Text.Encoding.GetEncoding("utf-8");
				//Response.ContentType = "application/ms-xml";
				this.EnableViewState = false;
				Response.WriteFile( fn );
				Response.Flush();
				Response.End();
				*/

				FileStream fileStream=new FileStream(fn,FileMode.Open); 
				long fileSize = fileStream.Length; 
				Context.Response.ContentType="application/octet-stream"; 
				Context.Response.AddHeader("Content-Disposition","attachment; filename=\"menu.xml\""); 
				Context.Response.AddHeader("Content-Length",fileSize.ToString()); 
				byte[] fileBuffer=new byte[fileSize]; 
				fileStream.Read(fileBuffer, 0, (int)fileSize); 
				fileStream.Close(); 
				Context.Response.BinaryWrite(fileBuffer); 
				Context.Response.End(); 


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
