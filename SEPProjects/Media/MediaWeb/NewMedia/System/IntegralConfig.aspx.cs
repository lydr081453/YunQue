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

public partial class NewMedia_System_IntegralConfig : ESP.Web.UI.PageBase
{
    DataTable dtList;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDataSource();
        }
    }

    void GetDataSource()
    {
        dtList = ESP.MediaLinq.BusinessLogic.IntegralManager.GetAll();
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
            string strret = string.Empty;
        try
        {
            //List<ESP.MediaLinq.Entity.media_IntegralRule> rules = new List<ESP.MediaLinq.Entity.media_IntegralRule>();
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                if (gvList.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    ESP.MediaLinq.Entity.media_IntegralRule rule = ESP.MediaLinq.BusinessLogic.IntegralManager.GetModel(Convert.ToInt32(gvList.DataKeys[i].Value));
                    rule.Integral = Convert.ToInt32((gvList.Rows[i].Cells[1].FindControl("txtIntegral") as TextBox).Text);
                    ESP.MediaLinq.BusinessLogic.IntegralManager.Update(rule);
                    //rules.Add(rule);
                }
            }
            strret = "保存成功!";
        }
        catch
        {
            strret = "保存失败!";
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(),Guid.NewGuid().ToString(),string.Format("alert('{0}');",strret),true);

    }
}