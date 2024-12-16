using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace PurchaseWeb.Purchase.Requisition.Advertisement
{
    public partial class EditProducts : ESP.Purchase.WebPage.EditPageForPR
    {
        int generalid = 0;
        static int SupplierId = 0;
        string query = string.Empty;
        static bool isFPrduct = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            {
                generalid = int.Parse(Request[RequestName.GeneralID]);
            }
            query = Request.Url.Query;

            if (!IsPostBack)
            {
                rep1Bind();
            }
        }
        static List<string> level1Types = new List<string>();
        static List<string> level2Types = new List<string>();
        static int projectId = 0;
        #region Panel1

        /// <summary>
        /// 获得一级物料类别
        /// </summary>
        /// <param name="orderInfoList"></param>
        /// <returns></returns>

        private void rep1Bind()
        {
            rep1.DataSource = TypeManager.GetListByParentId(0);
            rep1.DataBind();
        }

        /// <summary>
        /// 获得二级物料类别
        /// </summary>
        /// <param name="type1Id">一级物料类别ID</param>
        /// <returns></returns>
        private List<TypeInfo> getType2List(int type1Id)
        {
            List<TypeInfo> type2List = null;
            //获得一级物料类别下的二级类别
            type2List = TypeManager.GetListByParentId(type1Id);
            return type2List;
        }

        /// <summary>
        /// 一级物料类别列表 ItemDataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TypeInfo model = (TypeInfo)e.Item.DataItem;
                Label lab = (Label)e.Item.FindControl("lab");
                lab.Text = model.typename;
                DataList rep2 = (DataList)e.Item.FindControl("rep2");
                rep2.DataSource = getType2List(model.typeid);
                rep2.DataBind();
            }
        }

        /// <summary>
        /// 二级物料类别 ItemDataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rep2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TypeInfo model = (TypeInfo)e.Item.DataItem;
                Label lab = (Label)e.Item.FindControl("lab");
                lab.Text = model.typename;
                DataList dg3 = (DataList)e.Item.FindControl("dg3");
                //根据二级物料的ID，获得三级物料
                dg3.DataSource = TypeManager.GetListByParentId(model.typeid);
                dg3.DataBind();
            }
        }

        /// <summary>
        /// 物料类别列表 ItemDataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dg3_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TypeInfo model = (TypeInfo)e.Item.DataItem;
                LinkButton lnk = (LinkButton)e.Item.FindControl("lnk");
                if (projectId > 0 && !level2Types.Contains(model.parentId.ToString()))
                    lnk.Enabled = false;//选择的项目号，物料类别如不存在项目号中，则屏蔽掉，不能选择。
                if (model.status == State.typestatus_mediaUp3000)
                {
                    //GeneralInfo general = GeneralInfoManager.GetModel(generalid);
                    //if (general.PRType != (int)PRTYpe.PR_MediaFA && general.PRType != (int)PRTYpe.PR_PriFA)
                    //    lnk.Visible = false; //只有由3000以上媒介生成的pr单，才显示状态为typestatus_mediaUp3000的物料类别
                    //else
                    //{
                    //    lnk.Enabled = true;
                    //    lnk.Visible = true;
                    //}
                }
                lnk.Text = model.typename;
            }
        }

        /// <summary>
        /// 物料类别列表 ItemCommand事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dg3_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName == "SET")
            {
                string id = e.CommandArgument.ToString();
                TypeInfo model = TypeManager.GetModel(int.Parse(id));
                if (null != model)
                {
                    string clientId = "ctl00$ContentPlaceHolder1$";
                    Response.Write("<script>opener.document.all." + clientId + "hidtypeIds.value= '" + model.typeid + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtThirdParty.value= '" + model.typename + "'</script>");
                    Response.Write(@"<script>window.close();</script>");
                }
            }
        }

        /// <summary>
        /// 目录物品列表 RowDataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProductInfo model = (ProductInfo)e.Row.DataItem;
                Button btnS = (Button)e.Row.FindControl("btnS");
                if ((SupplierId != 0 && model.supplierId != 0 && model.supplierId != SupplierId) || isFPrduct)
                {
                    btnS.Enabled = false;
                }
                TypeInfo type = TypeManager.GetModel(model.productType);
                if (projectId > 0 && !level2Types.Contains(type.parentId.ToString()))
                    btnS.Enabled = false;
                if (type.status == (int)State.typestatus_mediaUp3000)
                {
                    btnS.Enabled = true;
                }
            }
        }

        #endregion
    }
}