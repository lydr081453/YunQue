using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

public partial class Customer_AreaInfoView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["areaid"]))
                BindInfo();
        }
    }

    private void BindInfo()
    {
        AreaInfo info = ESP.Finance.BusinessLogic.AreaManager.GetModel(Convert.ToInt32(Request["areaid"]));
        if (info != null)
        {
            this.txtCode.Text = info.AreaCode;
            this.txtENDescription.Text = info.Description;
            this.txtName.Text = info.AreaName;
            this.txtSearchCode.Text = info.SearchCode;
            this.txtNote.Text = info.Others;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AreaInfosList.aspx");
    }
}