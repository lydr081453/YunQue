using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.Entity;

public partial class UserControl_ClientControl_ClientEdit : System.Web.UI.UserControl
{
    ESP.Compatible.Employee currentUser = new ESP.Compatible.Employee();
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["Operate"]) && Request["Operate"] == "ADD")
            {
                this.imgLogo.Visible = true;
                this.imgLogo.Visible = false;
                this.imgLogo.Height = 36;
                this.imgLogo.Width = 36;
            }
            else if(!string.IsNullOrEmpty(Request["Operate"]) && Request["Operate"] == "EDIT")
            {
                labHeading.Text = "编辑客户";
            }
        }
    }

    /// <summary>
    /// 获得对象
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ClientsInfo getModel(ClientsInfo model)
    {
        if(model == null)
            model = new ClientsInfo();
        model.Clientcfullname = txtChFullName.Text.Trim();
        model.Clientcshortname = txtChShortName.Text.Trim();
        model.Clientefullname = txtEnFullName.Text.Trim();
        model.Clienteshortname = txtEnShortName.Text.Trim();
        if (txtDes.Text.Trim().Length > 1024)
            model.Clientdescription = txtDes.Text.Trim().Substring(0, 1024);
        else
            model.Clientdescription = txtDes.Text.Trim();
        if (imgLogo.ImageUrl != null && imgLogo.ImageUrl.Length > 0)
        {
            model.Clientlogo = imgLogo.ImageUrl;
        }
        return model;
    }

    /// <summary>
    /// 设置页面信息
    /// </summary>
    /// <param name="model"></param>
    public void InitPage(ClientsInfo model)
    {
        if (model != null)
        {
            this.txtChFullName.Text = model.Clientcfullname;
            this.txtChShortName.Text = model.Clientcshortname;
            this.txtEnFullName.Text = model.Clientefullname;
            this.txtEnShortName.Text = model.Clienteshortname;
            this.txtDes.Text = model.Clientdescription;
            if (model.Clientlogo != string.Empty)
            {
                this.imgLogo.Visible = true;
                this.imgLogo.ImageUrl = model.Clientlogo;
                this.imgLogo.Height = 36;
                this.imgLogo.Width = 36;

            }
            else
            {

                this.imgLogo.Visible = false;
                this.imgLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
                this.imgLogo.Height = 20;
                this.imgLogo.Width = 20;
            }
        }
    }
}
