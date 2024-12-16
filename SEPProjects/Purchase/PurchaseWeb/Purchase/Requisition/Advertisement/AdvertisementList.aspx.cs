using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using ESP.ConfigCommon;

namespace PurchaseWeb.Purchase.Requisition.Advertisement
{
    public partial class AdvertisementList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            string condition = string.Empty;
            if (this.txtMediaName.Text != string.Empty)
            {
                condition = " MediaName Like('%" + this.txtMediaName.Text + "%')";
            }
            IList<AdvertisementInfo> list = AdvertisementManager.GetInfoList(condition);
            this.gvG.DataSource = list;
            this.gvG.DataBind();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AdvertisementInfo info = (AdvertisementInfo)e.Row.DataItem;
                Label lblIsDel = (Label)e.Row.FindControl("lblIsDel");
                if (lblIsDel != null)
                {
                    lblIsDel.Text = "有效";
                    if (info.IsDel)
                    {
                        lblIsDel.Text = "无效";
                    }
                }
                Label lblMediaType = (Label)e.Row.FindControl("lblMediaType");
                lblMediaType.Text = AdvertisementTypeManager.GetModel(info.MediaTypeID).TypeName;

                Label lblProductType = (Label)e.Row.FindControl("lblProductType");
                if (lblProductType != null)
                {
                    TypeInfo type = TypeManager.GetModel(info.ProductTypeID);
                    if(type != null)
                        lblProductType.Text = type.typename;
                }
            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteItem")
            {
                AdvertisementManager.Delete(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        #region 分页
        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvG.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvG.PageIndex + 1) > gvG.PageCount ? gvG.PageCount : (gvG.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvG.PageIndex - 1) < 0 ? 0 : (gvG.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvG_PageIndexChanging(new object(), e);
        }

        private void disButton(string page)
        {
            switch (page)
            {
                case "first":
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = false;
                    btnPrevious2.Enabled = false;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
                case "last":
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = false;
                    btnLast2.Enabled = false;
                    break;
                default:
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
            }
        }

        #endregion
    }
}