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
using System.Collections.Specialized;

namespace FrameSite.Web.Public
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public partial class CalendarSel : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label m_lblYear;
		private DateTime dtTodaysDate;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			dtTodaysDate = DateTime.Now;
			//返回页面则显示已有数据
			NameValueCollection income = Request.QueryString;
			if(income.Keys.Count != 0)
			{
				string sTodaysDate= income.GetValues("sTodaysDate")[0];
				if(sTodaysDate != "" && sTodaysDate != "undefined")
				{
					dtTodaysDate = DateTime.Parse(sTodaysDate);
					//m_cldSelDate.SelectedDate = dtTodaysDate;
				}
				else
				{
					//m_cldSelDate.SelectedDate = Convert.ToDateTime("1-1-1");
				}
				if(!IsPostBack)
				{
					m_cldSelDate.VisibleDate = dtTodaysDate;
				}

			}
			else
			{
				if(!IsPostBack)
				{
					//m_cldSelDate.SelectedDate = Convert.ToDateTime("1-1-1");
					m_cldSelDate.VisibleDate = dtTodaysDate;
				}
			}
			if(!IsPostBack)
			{
				int nYear = int.Parse(dtTodaysDate.ToString("yyyy"));
				int i=10;
				while(i > 0)
				{
					m_ddlYear.Items.Add(new ListItem(((int)(nYear-i)).ToString(),((int)(nYear-i)).ToString()));
					i--;
				}
				m_ddlYear.Items.Add(new ListItem(nYear.ToString(),nYear.ToString()));
				i=1;
				while(i <= 10)
				{
					m_ddlYear.Items.Add(new ListItem(((int)(nYear+i)).ToString(),((int)(nYear+i)).ToString()));
					i++;
				}
				m_ddlYear.SelectedIndex = m_ddlYear.Items.IndexOf(new ListItem(dtTodaysDate.ToString("yyyy"),dtTodaysDate.ToString("yyyy")));
				m_ddlMonth.SelectedIndex = (int.Parse(dtTodaysDate.ToString("MM")) - 1);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN：该调用是 ASP.NET Web 窗体设计器所必需的。
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
			this.m_cldSelDate.VisibleMonthChanged += new System.Web.UI.WebControls.MonthChangedEventHandler(this.m_cldSelDate_VisibleMonthChanged);

		}
		#endregion

		protected void m_cldSelDate_SelectionChanged(object sender, System.EventArgs e)
		{
			string sDate = m_cldSelDate.SelectedDate.ToString("yyyy-MM-dd");
			string strURL = "<script language=javascript>window.returnValue = '" +sDate + "';window.close();</script>";
			this.Response.Write(strURL);
		}
		protected void m_ddlMonth_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string sYear = m_ddlYear.SelectedItem.Value;
			string sMonth = m_ddlMonth.SelectedItem.Value;
			string sFullDate=sYear+"-"+sMonth;
			string sOldDay = dtTodaysDate.ToString("dd");
			int iYear = int.Parse(sYear);
			int iMonth = int.Parse(sMonth);
			int iOldDay = int.Parse(sOldDay);
			int iDay = DateTime.DaysInMonth(iYear,iMonth);
			if(iDay >= iOldDay)
			{
				sFullDate+= "-" + sOldDay;
			}
			else
			{
				sFullDate+= "-" + iDay.ToString();
			}
			m_cldSelDate.VisibleDate = DateTime.Parse(sFullDate);
		}

		protected void m_ddlYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string sYear = m_ddlYear.SelectedItem.Value;
			string sMonth = m_ddlMonth.SelectedItem.Value;
			string sFullDate=sYear+"-"+sMonth;
			sFullDate+= "-" + dtTodaysDate.ToString("dd");
			m_cldSelDate.VisibleDate = DateTime.Parse(sFullDate);
		}

		private void m_cldSelDate_VisibleMonthChanged(object sender, System.Web.UI.WebControls.MonthChangedEventArgs e)
		{
			int nSel=  m_ddlYear.Items.IndexOf(new ListItem(e.NewDate.Year.ToString(),e.NewDate.Year.ToString()));
			if(nSel < 0)
			{
				int minYear=int.Parse(m_ddlYear.Items[0].Value);
				if(int.Parse(e.NewDate.Year.ToString()) > minYear)
				{
					m_ddlYear.Items.Add(new ListItem(e.NewDate.Year.ToString(),e.NewDate.Year.ToString()));
					nSel = m_ddlYear.Items.Count-1;
				}
				else
				{
					m_ddlYear.Items.Insert(0,new ListItem(e.NewDate.Year.ToString(),e.NewDate.Year.ToString()));
					nSel = 0;
				}
			}
			m_ddlYear.SelectedIndex = nSel;
			m_ddlMonth.SelectedIndex = (int.Parse(e.NewDate.Month.ToString()) - 1);
		}

		protected void m_btnAdd_Click(object sender, System.EventArgs e)
		{
			DateTime dtSelDate = m_cldSelDate.VisibleDate.AddYears(1);
			m_cldSelDate.VisibleDate=dtSelDate;
			int nSel=  m_ddlYear.Items.IndexOf(new ListItem(dtSelDate.Year.ToString(),dtSelDate.Year.ToString()));
			if(nSel < 0)
			{
				m_ddlYear.Items.Add(new ListItem(dtSelDate.Year.ToString(),dtSelDate.Year.ToString()));
				nSel = m_ddlYear.Items.Count-1;
			}
			m_ddlYear.SelectedIndex = nSel;
		}

		protected void m_btnDeduct_Click(object sender, System.EventArgs e)
		{
			DateTime dtSelDate = m_cldSelDate.VisibleDate.AddYears(-1);
			m_cldSelDate.VisibleDate=dtSelDate;
			int nSel=  m_ddlYear.Items.IndexOf(new ListItem(dtSelDate.Year.ToString(),dtSelDate.Year.ToString()));
			if(nSel < 0)
			{
				m_ddlYear.Items.Insert(0,new ListItem(dtSelDate.Year.ToString(),dtSelDate.Year.ToString()));
				nSel = 0;
			}
			m_ddlYear.SelectedIndex = nSel;
		}
	}
}
