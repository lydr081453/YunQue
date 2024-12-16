using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Purchase_TopMessage : System.Web.UI.UserControl
{
    private bool isEditPage = false;
    public bool IsEditPage { get { return isEditPage; } set { isEditPage = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private ESP.Purchase.Entity.GeneralInfo model;
    public ESP.Purchase.Entity.GeneralInfo Model
    {
        get { return model; }
        set
        {
            model = value;
            if (model != null)
            {
                if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.Disable)
                {
                    labMessage.Text = "该付款申请所属申请单已被暂停。";
                }
                else if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.DisableProject)
                {
                    labMessage.Text = "该付款申请所属项目已被暂停。";
                }
                else
                {
                    labMessage.Text = "";
                }
                if (!IsPostBack)
                {
                    if (isEditPage && model.InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + labMessage.Text.Trim() + "您不能进行任何操作！');window.location.href='/project/Default.aspx';", true);
                    }
                }
            }
        }
    }

    private ESP.Finance.Entity.ProjectInfo projectModel;
    public ESP.Finance.Entity.ProjectInfo ProjectModel
    {
        get { return projectModel; }
        set
        {
            projectModel = value;
            if (projectModel != null)
            {
                if (projectModel.InUse == (int)ESP.Finance.Utility.ProjectInUse.Disable)
                {
                    labMessage.Text = "该项目已被暂停。";
                }
                else
                {
                    labMessage.Text = "";
                }
                if (!IsPostBack)
                {
                    if (isEditPage && projectModel.InUse == (int)ESP.Finance.Utility.ProjectInUse.Disable)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + labMessage.Text.Trim() + "您不能进行任何操作！');window.location.href='/project/Default.aspx';", true);
                    }
                }
            }
        }
    }
}