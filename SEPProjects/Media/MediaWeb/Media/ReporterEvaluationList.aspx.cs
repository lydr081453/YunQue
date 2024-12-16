using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.BusinessLogic;

namespace MediaWeb.Media
{
    public partial class ReporterEvaluationList : ESP.Web.UI.PageBase
    {

        override protected void OnInit(EventArgs e)
        {
            InitDataGridColumn();
            base.OnInit(e);
        }

        #region 绑定列头
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDataGridColumn()
        {
            string strColumn = "CreateDate#UserName#Reason#ID";
            string strHeader = "修订时间#修订人#原因#查看";
            string sort = "CreateDate###";
            string strH = "center#center##center";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, strH, this.dgList);
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            dgList.DataSource = ReporterEvaluationManager.GetReporterEvaluation(int.Parse(Request[RequestName.ReporterID]),txtUser.Text.Trim()).Tables[0].DefaultView;

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].Text = "<a href='ReporterEvaluationLog.aspx?Rid="+Request[RequestName.ReporterID]+"&id="+e.Row.Cells[3].Text+"'>查看</a>";
            }
        }

        protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortDirection == SortDirection.Ascending)
            {
                e.SortDirection = SortDirection.Descending;
            }
            else if (e.SortDirection == SortDirection.Descending)
            {
                e.SortDirection = SortDirection.Ascending;
            }
        }
    }
}
