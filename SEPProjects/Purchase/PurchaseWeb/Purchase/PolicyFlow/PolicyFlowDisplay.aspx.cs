using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;

public partial class Purchase_PolicyFlow_PolicyFlowDisplay : ESP.Web.UI.PageBase
{
    int policyFlowId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["policyFlowId"]))
        {
            policyFlowId = int.Parse(Request["policyFlowId"]);
        }
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    private void InitPage()
    {
        if (policyFlowId > 0)
        {
            PolicyFlowInfo model = PolicyFlowManager.GetModel(policyFlowId);
            txtTitle.Text = model.title;
            txtSynopsis.Text = model.synopsis;
            txtContents.Text = string.IsNullOrEmpty(model.contents) ? "" : ("<a target='_blank' href='../../" + model.contents + "'><img src='/images/ico_04.gif' border='0' /></a>");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PolicyFlowEditList.aspx");
    }
}
