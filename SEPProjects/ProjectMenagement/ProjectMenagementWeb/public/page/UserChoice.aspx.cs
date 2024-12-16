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

using ESP.Compatible;
using ESP.Finance.BusinessLogic;
using System.Collections.Generic;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.BusinessLogic;
namespace FrameWork.Web.Public
{
	/// <summary>
	/// UserChoice ��ժҪ˵����
	/// </summary>
	public partial class UserChoice : ESP.Web.UI.PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//��ҳ�治���밲ȫ���
			//SecurityMode = PageSecurityMode.None;
			if(!IsPostBack)
			{
				string script = "javascript:" +
					" event.returnValue = CheckInput(" + ConfigManager.MaxRecordCount + "); " ;

				btnQuery.Attributes.Add("onclick",script);
			}

			if(QueryStr.Text.Trim()!="")
			{
				FillDg();
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

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			FillDg();
		}

		private void FillDg()
		{
            string where;
            ESP.Framework.DataAccess.Utilities.DbDataParameter[] paras;
            if (QueryStr.Text.Trim() != "")
            {
                string keyword = "%" + QueryStr.Text.Trim() + "%";
                where = "u.Username LIKE @Keyword OR (u.FirstNameCN + u.LastNameCN) LIKE @Keyword";
                DbDataParameter p = new DbDataParameter(DbType.String, "Keyword", keyword);
                paras = new DbDataParameter[] { p };
            }
            else
            {
                where = null;
                paras = null;
            }

            IList<EmployeeInfo> list = EmployeeManager.Search(ConfigManager.MaxRecordCount, 0, null, where, paras);
            IList<Employee> el = new List<Employee>(list.Count);
            DgBrow.DataSource = el;
            DgBrow.DataBind();



            //string sql;
            //if(QueryStr.Text.Trim()!="")
            //{
            //    sql = "select top " + ConfigManager.MaxRecordCount + " * from EmployeeInfo where UserCode like '%" + Utils.DoubleQuote(QueryStr.Text.Trim()) + "%' " +
            //        " or UserName like '%" + QueryStr.Text.Trim() + "%'";
            //}
            //else
            //{
            //    sql = "select top " + ConfigManager.MaxRecordCount + " * from EmployeeInfo";
            //}

            //using(SqlDataReader dr = SqlExecuteReader(sql))
            //{
            //    DgBrow.DataSource = dr;
            //    DgBrow.DataBind();
            //}

		}
	}
}
