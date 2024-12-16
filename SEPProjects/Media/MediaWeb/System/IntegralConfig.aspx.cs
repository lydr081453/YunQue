using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Media.BusinessLogic;
using ESP.Compatible;
using System.Collections.Generic;

public partial class System_IntegralConfig : ESP.Web.UI.PageBase
{
    DataTable dtList;

    protected override void OnInit(EventArgs e)
    {
        DrawData();
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDataSource();
        }
    }

    void DrawData()
    {
        //gvList.AutoGenerateColumns = false;
        //gvList.AllowSorting = true;
        //string[] fileds = { "id", "altname", "Integral" };
        //string[] heads = { "id", "操作名称", "分数" };
        //string[] sorts = { "id", "", "" };
        //gvList.HeaderStyle.CssClass = "heading";
        //for (int i = 0; i < fileds.Length; i++)
        //{
        //    BoundField bc = new BoundField();
        //    bc.ItemStyle.Width = Unit.Point(150);
        //    bc.DataField = fileds[i];
        //    bc.HeaderText = heads[i];
        //    bc.ReadOnly = true;
        //    if (gvList.AllowSorting)
        //    {
        //        if (sorts != null && sorts.Length > 0)
        //        {
        //            bc.SortExpression = sorts[i];
        //        }
        //        else
        //        {
        //            bc.SortExpression = bc.DataField;
        //        }
        //    }
        //    gvList.Columns.Add(bc);
        //}
        //gvList.DataKeyNames = new string[]{"id"};
        //gvList.Columns[0].ItemStyle.Width = Unit.Point(20);
    }

    void GetDataSource()
    {
        dtList = IntegralruleManager.GetAll();
        if (dtList != null)
        {
            
            gvList.DataSource = dtList.DefaultView;
            gvList.DataBind();
            //gvList.Columns[0].Visible = false;
        }
    }


    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Attributes["class"] = "Gheading";
            e.Row.Cells[1].Attributes["class"] = "Gheading";
            
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    TextBox txtIntegral = new TextBox();
        //    txtIntegral.Text = e.Row.Cells[2].Text;
        //    e.Row.Cells[2].Text = string.Format("<input type='text' value='{0}'  id='txtIntegral' onchange='check()' />", e.Row.Cells[2].Text);
        //}
    }
    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortDirection == SortDirection.Ascending)
        {
            e.SortDirection = SortDirection.Descending;
        }
        else if (e.SortDirection == SortDirection.Descending)
        { 
            e.SortDirection = SortDirection.Ascending;
        }

        GetDataSource();
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("IntegralRuleAdd.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<ESP.Media.Entity.IntegralruleInfo> rules = new List<ESP.Media.Entity.IntegralruleInfo>();
        for (int i=0;i<gvList.Rows.Count;i++)
        {
            if (gvList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                ESP.Media.Entity.IntegralruleInfo rule = ESP.Media.BusinessLogic.IntegralruleManager.GetModel(Convert.ToInt32(gvList.DataKeys[i].Value));
                rule.Integral = Convert.ToInt32((gvList.Rows[i].Cells[1].FindControl("txtIntegral") as TextBox).Text);
                rules.Add(rule);
            }
        }
        int ret = ESP.Media.BusinessLogic.IntegralruleManager.Modify(rules, CurrentUserID);
        string strret = string.Empty;
        if (ret > 0)
        { 
            strret = "保存成功!";
        }
        else
        {
         strret = "保存失败!";
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(),Guid.NewGuid().ToString(),string.Format("alert('{0}');",strret),true);

    }
}
