using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_View_PRTopMessage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private bool isEditPage = false;
    public bool IsEditPage { get { return isEditPage; } set { isEditPage = value; } }

    private ESP.Purchase.Entity.GeneralInfo model;
    public ESP.Purchase.Entity.GeneralInfo Model {
        get { return model; }
        set 
        { 
            model = value;
            if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.Disable)
            {
                labMessage.Text = ESP.Purchase.Common.State.DisabledMessageForPRView;
            }
            else if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.DisableProject)
            {
                labMessage.Text = ESP.Purchase.Common.State.DisabledMessageForProjectView;
            }
            else
            {
                labMessage.Text = "";
            }
            if (model.status == ESP.Purchase.Common.State.requisition_Stop)
            {
                labMessage.Text = "[" + ESP.Purchase.Common.State.StopMessageForPRView + "]";
            }
            if (!IsPostBack)
            {
                if (isEditPage && labMessage.Text != "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('"+labMessage.Text.Trim()+"您不能进行任何操作！');window.location.href='/Purchase/Default.aspx';", true);
                }
            }
        }
    }

}