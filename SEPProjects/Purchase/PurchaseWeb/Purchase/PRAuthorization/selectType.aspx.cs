using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using ESP.Purchase.Common;

public partial class Purchase_PRAuthorization_selectType : ESP.Web.UI.PageBase
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
        List<ESP.Purchase.Entity.TypeInfo> typelist = ESP.Purchase.BusinessLogic.TypeManager.GetModelList(" and typelevel=1");

        rep1.DataSource = typelist;
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
            ESP.Purchase.Entity.TypeInfo model = (ESP.Purchase.Entity.TypeInfo)e.Item.DataItem;
            Label lab = (Label)e.Item.FindControl("lab");
            lab.Text = model.typename;
            DataList rep2 = (DataList)e.Item.FindControl("rep2");
            List<ESP.Purchase.Entity.TypeInfo> typelist = ESP.Purchase.BusinessLogic.TypeManager.GetModelList("  and parentid="+model.typeid.ToString());
          
            rep2.DataSource = typelist;
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
            ESP.Purchase.Entity.TypeInfo model = (ESP.Purchase.Entity.TypeInfo)e.Item.DataItem;
            TypeInfo typeModel = TypeManager.GetModel(model.typeid);
            Literal lab = (Literal)e.Item.FindControl("lab");
            lab.Text = typeModel.typename;
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
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        string selected = Request["chk"];
        string ids = "";
        for (int i=0;i<selected.Split(',').Length;i++)
        {
            if (selected.Split(',')[i] != "")
            {
                ids += selected.Split(',')[i].Split('-')[0] + ",";
            }
        }
        ids = ids.TrimEnd(',');
        List<TypeInfo> typelist = TypeManager.GetModelList(" and typeid in (" + ids + ")");
        string typenames = "";
        foreach (TypeInfo model  in typelist)
        {
            typenames +=model.typename + "  ";
        }
        ids = "," + ids + ",";
        Response.Write("<script>window.opener.setTypes('"+ids+"','"+typenames+"');</script>");
        Response.Write(@"<script>window.close();</script>");
    }
}
