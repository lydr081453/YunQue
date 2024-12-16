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

public partial class Purchase_Requisition_selectProductTypes : ESP.Web.UI.PageBase
{
    static string level1Types = "";
    static string level2Types = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            level1Types = "";
            level2Types = "";
            InitProductTypeItems();
        }
    }

    /// <summary>
    /// 设置显示类型
    /// </summary>
    private void InitProductTypeItems()
    {
        List<ESP.Purchase.Entity.V_GetProjectTypeList> typelist = getTypeList();
        foreach (ESP.Purchase.Entity.V_GetProjectTypeList type in typelist)
        {
            if (!level1Types.Contains(type.parentId.ToString()))
                level1Types += type.parentId + ",";
            if (!level2Types.Contains(type.TypeID.ToString()))
                level2Types += type.TypeID + ",";
        }
        DataSet ds;
        if (level1Types.Length > 0)
            ds = TypeManager.GetList(" and typeid in (" + level1Types.TrimEnd(',') + ")");
        else
            ds = null;
        rep1.DataSource = ds;
        rep1.DataBind();
    }

    private List<ESP.Purchase.Entity.V_GetProjectTypeList> getTypeList()
    {
        //设置项目号下所包含的一级物料和二级物料
        string strTerms = " and a.projectId=@projectId and a.groupId = @groupId";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@projectId", SqlDbType.Int, 4));
        parms.Add(new SqlParameter("@groupId", SqlDbType.Int, 4));
        parms[0].Value = Request["projectId"];
        parms[1].Value = Request["deptId"];
        List<ESP.Purchase.Entity.V_GetProjectTypeList> typelist = ESP.Purchase.BusinessLogic.V_GetProjectTypeList.GetList(strTerms, parms);

        return typelist;
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
            DataRowView dv = (DataRowView)e.Item.DataItem;
            Label lab = (Label)e.Item.FindControl("lab");
            lab.Text = dv["typename"].ToString();
            DataList rep2 = (DataList)e.Item.FindControl("rep2");
            List<ESP.Purchase.Entity.V_GetProjectTypeList> typelist = getTypeList();
            for (int i = (typelist.Count-1); i >= 0; i--)
            {
                if(typelist[i].parentId != int.Parse(dv["typeid"].ToString()))
                {
                    typelist.RemoveAt(i);
                }
            }
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
            ESP.Purchase.Entity.V_GetProjectTypeList model = (ESP.Purchase.Entity.V_GetProjectTypeList)e.Item.DataItem;
            TypeInfo typeModel = TypeManager.GetModel(model.TypeID);
            Literal lab = (Literal)e.Item.FindControl("lab");
            lab.Text = typeModel.typename + " (剩余:" + model.Amounts.ToString("#,##0.00")+")";
            DataList dg3 = (DataList)e.Item.FindControl("dg3");
            dg3.DataSource = TypeManager.GetListByParentId(model.TypeID);
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
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        string selected = Request["chk"];
        string ids = "";
        decimal price = 0;
        for (int i=0;i<selected.Split(',').Length;i++)
        {
            if (selected.Split(',')[i] != "")
            {
                ids += selected.Split(',')[i].Split('-')[0] + ",";
                price += decimal.Parse(selected.Split(',')[i].Split('-')[1]);
            }
        }
        ids = ids.TrimEnd(',');
        DataSet ds = TypeManager.GetList(" and typeid in (" + ids + ")");
        string typenames = "";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            typenames += dr["typename"].ToString() + "  ";
        }

        GeneralInfo generalInfo = GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));
        generalInfo.thirdParty_materielDesc = typenames;
        generalInfo.thirdParty_materielID = ids;
        generalInfo.buggeted = price;
        generalInfo.PRType = 0;
        GeneralInfoManager.Update(generalInfo);

        Response.Write("<script>window.opener.setProductTypes('"+ids+"','"+typenames+"','"+price.ToString("#,##0.00")+"');</script>");
        Response.Write(@"<script>window.close();</script>");
    }
}
