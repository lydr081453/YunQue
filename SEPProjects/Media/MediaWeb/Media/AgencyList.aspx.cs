using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using ESP.Compatible;

/*
 * 
 * 
 * 机构列表页
 * 
 * 
 */
namespace MediaWeb.Media
{
    public partial class AgencyList : ESP.Web.UI.PageBase
    {
        override protected void OnInit(EventArgs e)
        {
            InitDataGridColumn();
            InitializeComponent();
            base.OnInit(e);
           // int userid = CurrentUserID;
        }

        private void InitializeComponent()
        {

        }

        #region 绑定列头
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDataGridColumn()
        {
            string strColumn = "AgencyCName#AgencyEName#AgencyID#AgencyID";
            string strHeader = "机构中文名#机构英文名#编辑#删除";
            string strSort = "AgencyCName####";
            string strH = "center#center#center#center";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, strH, this.glist);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ListBind();

        }

        #region 绑定列表
        /// <summary>
        /// Lists the bind.
        /// </summary>       
        private void ListBind()
        {
            DataSet ds = new ESP.Media.BusinessLogic.AgencyManager().GetAllList();
          //  this.dgList.DataSource = ds.Tables[0].DefaultView;
            this.glist.DataSource = ds.Tables[0].DefaultView;
            

        }
        #endregion

        protected void gList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].Text = string.Format("<a href='AgencyAddAndEdit.aspx?aid={0}' ><img src='{1}' /></a>", e.Row.Cells[2].Text, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);

                e.Row.Cells[3].Text = string.Format("<a href='AgencyAddAndEdit.aspx?Operate=Delaid={0}' ><img src='{1}' /></a>", e.Row.Cells[3].Text, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);
            }
        }

        protected void gList_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}
