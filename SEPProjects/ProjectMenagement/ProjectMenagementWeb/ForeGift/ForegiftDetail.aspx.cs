using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForeGift_ForegiftDetail : ESP.Web.UI.PageBase
{
    int returnId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        }
        if (!IsPostBack)
        {
            ViewForeGift.BindInfo(ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId));
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForegiftList.aspx");
    }
}
