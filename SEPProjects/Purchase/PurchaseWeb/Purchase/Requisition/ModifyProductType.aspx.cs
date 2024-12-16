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

public partial class Purchase_Requisition_ModifyProductType : ESP.Web.UI.PageBase
{
    int generalid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            generalid = int.Parse(Request[RequestName.GeneralID]);
        if (!IsPostBack)
        {
            level1Types.Clear();
            level2Types.Clear();

            InitProductTypeItems();
            ESP.Logging.Logger.Add(string.Format("变更流水号为{0}申请单的物料类别", generalid));
        }
    }

    static List<string> level1Types = new List<string>();
    static List<string> level2Types = new List<string>();

    /// <summary>
    /// 设置显示类型
    /// </summary>
    private void InitProductTypeItems()
    {
        //设置项目号下所包含的一级物料和二级物料
        GeneralInfo generalInfo = GeneralInfoManager.GetModel(generalid);
        if (generalInfo.Project_id > 0)
        {
            string strTerms = " and a.projectId=@projectId and a.groupId = @groupId";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@projectId", SqlDbType.Int, 4));
            parms.Add(new SqlParameter("@groupId", SqlDbType.Int, 4));
            parms[0].Value = generalInfo.Project_id;
            parms[1].Value = generalInfo.Departmentid;

            //改申请单可以选择的一级物料和二级物料
            DataTable typeDt = TypeManager.GetList(" and typeid in (" + generalInfo.thirdParty_materielID + ")").Tables[0];
            foreach (DataRow dr in typeDt.Rows)
            {
                if (!level1Types.Contains(dr["parentId"].ToString()))
                    level1Types.Add(dr["parentId"].ToString());
                if (!level2Types.Contains(dr["typeid"].ToString()))
                    level2Types.Add(dr["typeid"].ToString());
            }
        }
        rep1.DataSource = TypeManager.GetListByParentId(0);
        rep1.DataBind();
        //dgNav.DataBind();
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
            if (level2Types.Count > 0 && !level2Types.Contains(model.parentId.ToString()))
                lnk.Enabled = false;
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

            TypeInfo typeModel2 = TypeManager.GetModel(model.parentId);
            TypeInfo typeModel1 = TypeManager.GetModel(typeModel2.parentId);
            string typeNames = typeModel1.typename + " - " + typeModel2.typename + " - " + model.typename;

            Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_hidProductTypeId.value='" + id + "'</script>");
            Response.Write("<script>if(document.all)opener.document.all.ctl00_ContentPlaceHolder1_labProductType.innerText='" + typeNames + "';else opener.document.all.ctl00_ContentPlaceHolder1_labProductType.textContent='" + typeNames + "';</script>");
            Response.Write("<script>window.close();</script>");
        }
    }
}
