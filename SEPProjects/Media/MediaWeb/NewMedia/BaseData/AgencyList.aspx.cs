using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.MediaLinq.Entity;
using System.Data;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class AgencyList : ESP.Web.UI.PageBase
    {
        override protected void OnInit(EventArgs e)
        {
            InitDataGridColumn();
            InitializeComponent();
            base.OnInit(e);          
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
            string strColumn = "AgencyCName#AgencyEName#MediaID#AgencyID#AgencyID";
            string strHeader = "机构中文名#机构英文名#所属媒体#编辑#删除";
            string strSort = "AgencyCName##MediaID##";
            string strH = "center#center#center#center#center";
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

            DataSet ds = ESP.MediaLinq.BusinessLogic.AgencyManager.GetDataSet();
            this.glist.DataSource = ds.Tables[0].DefaultView;
            


        }
        #endregion

        protected void gList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string mediaID = e.Row.Cells[2].Text;
                if (e.Row.Cells[2].Text != string.Empty)
                {
                    ESP.MediaLinq.Entity.media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(e.Row.Cells[2].Text));
                    if (media != null)
                    {
                        e.Row.Cells[2].Text = media.MediaCName;
                    }
                    else
                    {
                        e.Row.Cells[2].Text = "无关联媒体";
                    }
                }
                e.Row.Cells[3].Text = string.Format("<a href='AgencyAddAndEdit.aspx?aid={0}' ><img src='{1}' /></a>", e.Row.Cells[3].Text, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);

                e.Row.Cells[4].Text = string.Format("<a href='AgencyAddAndEdit.aspx?Operate=Delaid={0}' ><img src='{1}' /></a>", e.Row.Cells[4].Text, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);
            }
        }

        protected void gList_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}
