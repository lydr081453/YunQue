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
using ESP.Finance.Entity;
using System.Configuration;

public partial class Dialogs_CustomerFiles : System.Web.UI.Page
{
    private static string term = string.Empty;
    //private static List<SqlParameter> paramlist = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCustomerFiles();
        }
    }

    protected void gvCustomerFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CustomerAttachInfo item = (CustomerAttachInfo)e.Row.DataItem;

            HyperLink hypName = (HyperLink)e.Row.FindControl("hypName");
            if (hypName != null)
            {
                int key = hypName.Text.LastIndexOf("__");
                if (key > -1)
                {
                    hypName.Text = hypName.Text.Substring(key + 2);
                    hypName.ToolTip = hypName.Text;
                }
                hypName.NavigateUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["CustomerAttachPath"] + "\\" + item.Attachment;
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
        HiddenField hidId = (HiddenField)rw.FindControl("hidId");

        if (hidId != null && !string.IsNullOrEmpty(hidId.Value))
        {
            ESP.Finance.BusinessLogic.CustomerAttachManager.Delete(Convert.ToInt32(hidId.Value));
        }
        BindCustomerFiles();
    }

    private void BindCustomerFiles()
    {
        int customerID = 0;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.CustomerID]))
            customerID = int.Parse(Request[ESP.Finance.Utility.RequestName.CustomerID]);
        string condition = "customerID = " + customerID.ToString();
        IList<CustomerAttachInfo> list = ESP.Finance.BusinessLogic.CustomerAttachManager.GetList(condition);
        this.gvCustomerFiles.DataSource = list;
        this.gvCustomerFiles.DataBind();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "window.close(); ", true);
    }

    protected void btnSaveContract_Click(object sender, EventArgs e)
    {
        SaveCustomerFile();
    }

    private void CreateCustomerFile(string fileName)
    {
        CustomerAttachInfo attach = new CustomerAttachInfo();
        attach.CustomerID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.CustomerID]);
        attach.Description = txtContractDescription.Text.Trim();
        attach.Attachment = fileName;

        ESP.Finance.BusinessLogic.CustomerAttachManager.Add(attach);
    }

    private void SaveCustomerFile()
    {
        if (this.fileupContract.FileName != string.Empty)
        {
            string fileName = "Customer__" + Guid.NewGuid().ToString() + "__" + this.fileupContract.FileName;
            this.fileupContract.SaveAs(ESP.Configuration.ConfigurationManager.SafeAppSettings["CustomerAttachPath"] + "\\" + fileName);
            CreateCustomerFile(this.fileupContract.FileName);
            BindCustomerFiles();
        }
    }

}