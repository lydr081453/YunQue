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


public partial class Dialogs_BranchDlg : System.Web.UI.Page
{
    private string clientId = "ctl00_ContentPlaceHolder1_ProjectInfo_";
    private string uniqueId = "ctl00$ContentPlaceHolder1$ProjectInfo$";
    bool notPostBack = false;
    //int page_size = 20; //default page size
    //int offset = 0; //default page
    //string sortCol, sortDir;

    protected void Page_Load(object sender, EventArgs e)
    {
        notPostBack = false;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.NotPostBack]))
        {
            if (Request[ESP.Finance.Utility.RequestName.NotPostBack].Trim().ToLower() == "true")
            {
                notPostBack = true;
            }
        }
        if (!IsPostBack)
        {
            BindGrid();
        }

    }

    void gvBranch_SelectedIndexChanged(object sender, SelectedRowArgs e)
    {
        string BranchCode = e.SelectedRow["BranchCode"] == null ? "" : e.SelectedRow["BranchCode"].ToString();
        string BranchName = e.SelectedRow["BranchName"] == null ? "" : e.SelectedRow["BranchName"].ToString();
        string BranchID = e.SelectedRow["BranchID"] == null ? "" : e.SelectedRow["BranchID"].ToString();

        string script = @"
var clientId = '" + clientId + @"';
var uniqueId = '" + uniqueId + @"';
var BranchCode = '" + BranchCode + @"';
var BranchName = '" + BranchName + @"';
var BranchID = '" + BranchID + @"';

opener.document.getElementById(clientId + 'hidBranchCode').value = BranchCode;
opener.document.getElementById(clientId + 'txtBranchName').value = BranchName;
opener.document.getElementById(clientId + 'hidBranchID').value = BranchID;
";
        if (!notPostBack)
        {

            script += "opener.retClick();";
        }
script+=@"window.close(); 
";

        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);

    }

    private void add(string branchid)
    {
        ESP.Finance.Entity.BranchInfo branch = ESP.Finance.BusinessLogic.BranchManager.GetModel(int.Parse(branchid));

        string BranchCode = branch.BranchCode;
        string BranchName = branch.BranchName;
        string BranchID =branch.BranchID.ToString();
        string Des = branch.Des;
        string script = "";
        if (string.IsNullOrEmpty(Request["type"]))
        {
            script = @"
var clientId = '" + clientId + @"';
var uniqueId = '" + uniqueId + @"';
var BranchCode = '" + BranchCode + @"';
var BranchName = '" + BranchName + @"';
var BranchID = '" + BranchID + @"';

opener.document.getElementById(clientId + 'hidBranchCode').value = BranchCode;
opener.document.getElementById(clientId + 'txtBranchName').value = BranchName;
opener.document.getElementById(clientId + 'hidBranchID').value = BranchID;
";
            if (!notPostBack)
            {
                script += "opener.__doPostBack(uniqueId + 'btnRet', '');";
            }

            script +=@"window.close(); 
            ";
        }
        else if (Request["type"] == "branch")
        {
            script = "opener.setBranchInfo('" + BranchID + "','" + BranchName + "','" + BranchCode + "','" + Des + "');window.close();";
        }
        else if (Request["type"] == "customer")
        {
            script = "opener.document.all.ctl00$ContentPlaceHolder1$txtAppCompany.value= '" + BranchName + "';";
            script += "opener.document.all.ctl00$ContentPlaceHolder1$hidCompanyID.value= '" + BranchID + "';";
            script += "window.close(); ";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true); 
        }

        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);

    }
    protected void gvBranch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string branchid = gvBranch.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(branchid);
        }
    }

    protected void gvBranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void gvBranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvBranch.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        IList<ESP.Finance.Entity.BranchInfo> cms = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
        gvBranch.DataSource = cms;
        this.gvBranch.DataBind();
    }
}