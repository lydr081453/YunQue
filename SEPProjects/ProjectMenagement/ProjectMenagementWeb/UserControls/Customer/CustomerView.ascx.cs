using System;
using System.Collections.Generic;
using ESP.Finance.Utility;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Customer_CustomerView : System.Web.UI.UserControl
{
    int customerId = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.CustomerID]))
        {
            customerId = int.Parse(Request[ESP.Finance.Utility.RequestName.CustomerID]);
        }
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        
    }
}
