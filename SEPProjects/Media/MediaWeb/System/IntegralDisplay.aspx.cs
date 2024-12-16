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

public partial class System_IntegralDisplay : ESP.Web.UI.PageBase
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
        dtList = IntegralruleManager.GetAll();
        if (dtList != null)
        {

            gvList.DataSource = dtList.DefaultView;
            gvList.DataBind();
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


}
