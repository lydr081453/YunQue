using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ExtExtenders;
using ESP.Finance.Utility;

public partial class Dialogs_AreaDlg : System.Web.UI.Page
{
    private string clientId = "ctl00_ContentPlaceHolder1_CustomerInfo_";
    private static string term = string.Empty;
    private static List<SqlParameter> paramlist = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<ESP.Finance.Entity.AreaInfo> cms = ESP.Finance.BusinessLogic.AreaManager.GetList(term, paramlist);
            gvArea.DataSource = cms;
            this.gvArea.DataBind();
        }
         
    }

    protected void gvArea_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string areaid = gvArea.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(areaid);
        }
    }

    private void add(string areaid)
    {
        ESP.Finance.Entity.AreaInfo area = ESP.Finance.BusinessLogic.AreaManager.GetModel(Convert.ToInt32(areaid));

        string AreaID = area.AreaID.ToString();
        string AreaCode = area.AreaCode;
        string AreaName = area.AreaName;
        if (!string.IsNullOrEmpty(Request["clientid"]))
        {
            clientId = Request["clientid"];
        }
        string strscript = "opener.document.getElementById('" + clientId + "hidAreaID').value= '" + AreaID + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtArea').value= '" + AreaName + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidAreaCode').value= '" + AreaCode + "';";
        strscript += "window.close(); ";
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), strscript, true);
    }

    protected void gvArea_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void gvArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvArea.PageIndex = e.NewPageIndex;
        search();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        search();
    }

    private void search()
    {
        term = "   AreaCode  like '%'+@AreaCode+'%' or AreaName like '%'+@AreaName+'%' or SearchCode  like '%'+@SearchCode+'%' ";
        paramlist = new List<SqlParameter>();
        paramlist.Add(new SqlParameter("@AreaCode", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@AreaName", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@SearchCode", this.txtCode.Text.Trim()));

        IList<ESP.Finance.Entity.AreaInfo> cms = ESP.Finance.BusinessLogic.AreaManager.GetList(term, paramlist);
        gvArea.DataSource = cms;
        this.gvArea.DataBind();
    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        term = string.Empty;
        paramlist = null;
        IList<ESP.Finance.Entity.AreaInfo> cms = ESP.Finance.BusinessLogic.AreaManager.GetList(term, paramlist);
        gvArea.DataSource = cms;
        this.gvArea.DataBind();
    }
}
