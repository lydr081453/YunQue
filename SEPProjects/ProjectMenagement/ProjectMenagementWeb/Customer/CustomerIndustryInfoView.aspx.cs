using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
public partial class Customer_CustomerIndustryInfoView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["industryid"]))
                BindInfo();
        }
    }

    private void BindInfo()
    {
        CustomerIndustryInfo info = ESP.Finance.BusinessLogic.CustomerIndustryManager.GetModel(Convert.ToInt32(Request["industryid"]));
        if (info != null)
        {
            this.txtCode.Text = info.IndustryCode;
            this.txtName.Text = info.CategoryName;
            this.txtNote.Text = info.Description;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerIndustryInfosList.aspx");
    }
}