using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Project_TopMessage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private bool isEditPage = false;
    public bool IsEditPage { get { return isEditPage; } set { isEditPage = value; } }
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
                }else
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