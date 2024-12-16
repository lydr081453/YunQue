﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;

public partial class Dialogs_ProductTypeMapDlg : System.Web.UI.Page
{
    //int generalid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.GeneralID]))
        //    generalid = int.Parse(Request[ESP.Finance.Utility.RequestName.GeneralID]);
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
        rep1.DataSource = ESP.Finance.BusinessLogic.CostTypeViewManager.GetLevel1List("", null);
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
            CostTypeViewInfo model = (CostTypeViewInfo)e.Item.DataItem;
            Label lab = (Label)e.Item.FindControl("lab");
            lab.Text = model.TypeName;
            DataList rep2 = (DataList)e.Item.FindControl("rep2");

            rep2.DataSource = ESP.Finance.BusinessLogic.CostTypeViewManager.GetList("a.ParentID=" + model.TypeID.ToString());
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
            CostTypeViewInfo model = (CostTypeViewInfo)e.Item.DataItem;
            Label lab = (Label)e.Item.FindControl("lab");
            lab.Text = model.TypeName;
            DataList dg3 = (DataList)e.Item.FindControl("dg3");

            dg3.DataSource = ESP.Finance.BusinessLogic.CostTypeViewManager.GetList("a.ParentID=" + model.TypeID.ToString());
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
            CostTypeViewInfo model = (CostTypeViewInfo)e.Item.DataItem;
            LinkButton lnk = (LinkButton)e.Item.FindControl("lnk");
            lnk.Text = model.TypeName;
        }
    }

    /// <summary>
    /// 物料类别列表 ItemCommand事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dg3_ItemCommand(object sender, DataListCommandEventArgs e)
    {
        
    }

    /// <summary>
    /// 返回按钮(返回编辑页)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + generalid);
    }

}