using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace FinanceWeb.ITAsset
{
    public partial class AssetList : ESP.Web.UI.PageBase
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
            string terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (txtKey.Text.Trim() != "")
            {
                terms += " and (SerialCode+CategoryName+AssetName+Brand+Model+Configuration+cast(Price as varchar(50))  like '%'+@keyword+'%' or ( id in(select AssetId  from F_ITAssetReceiving where UserName+UserCode +Email like '%'+@keyword+'%')))";
                parms.Add(new SqlParameter("@keyword", txtKey.Text.Trim()));
            }

            if (ddlStatus.SelectedValue != "-1")
            {
                terms += " and status=@status";
            }
            parms.Add(new SqlParameter("@status", ddlStatus.SelectedValue.ToString()));
           
            IList<ESP.Finance.Entity.ITAssetsInfo> list = ESP.Finance.BusinessLogic.ITAssetsManager.GetList(terms, parms);
            gvList.DataSource = list;
            gvList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                if (ESP.Finance.BusinessLogic.ITAssetsManager.Delete(id) == 1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('损坏操作成功！');", true);
                    ListBind();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('损坏操作失败！');", true);
                }
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.ITAssetsInfo model = (ESP.Finance.Entity.ITAssetsInfo)e.Row.DataItem;
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Bad || model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Receiving || model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Received || model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Scrapping || model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Scrapping2 || model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Scrapped)
                 {
                    // e.Row.Cells[7].Text = "";//编辑
                     e.Row.Cells[9].Text = "";//领用
                     e.Row.Cells[10].Text = "";//报废
                     e.Row.Cells[12].Text = "";//删除
                 }

                 if (model.Status != (int)ESP.Finance.Utility.FixedAssetStatus.Received)
                 {
                     e.Row.Cells[11].Text = "";//还回
                 }
             }
        }


    }
}