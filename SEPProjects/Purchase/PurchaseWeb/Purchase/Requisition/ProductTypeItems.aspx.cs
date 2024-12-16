using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ProductTypeItems : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitProductTypeItems();
        }
    }

    /// <summary>
    /// 设置显示类型
    /// </summary>
    private void InitProductTypeItems()
    {
        rep1.DataSource = TypeManager.GetListByParentId(0);
        rep1.DataBind();
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
            rep2.DataSource = TypeManager.GetListByParentId(model.typeid);
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
            dg3.DataSource = TypeManager.GetListByParentId(model.typeid);
            dg3.DataBind();
        }
    }

    /// <summary>
    /// 二级物料类别 ItemCommand事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rep2_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "SET")
        {
            string id = e.CommandArgument.ToString();
            //setItemList(int.Parse(id), false);
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
            lnk.Text = model.typename;
            lnk.Attributes["onclick"] = "javascript:openSuppliers(" + model.typeid + ");return false;";
        }
    }
}
