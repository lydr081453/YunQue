using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.Entity;

public partial class UserControl_ProductLineControl_ProductLineEdit : System.Web.UI.UserControl
{
    
    ESP.Compatible.Employee currentUser = new ESP.Compatible.Employee();
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 获得对象
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ProductlinesInfo getModel(ProductlinesInfo mlProductline)
    {
        if (mlProductline == null)
            mlProductline = new ProductlinesInfo();
        mlProductline.Productlinename = this.txtProductLineName.Text;
        if (this.txtDes.Text.Trim().Length > 200)
            mlProductline.Productlinedescription = this.txtDes.Text.Trim().Substring(0, 200);
        else
            mlProductline.Productlinedescription = this.txtDes.Text.Trim();

        mlProductline.Clientid = int.Parse(hidCustom.Value == "" ? "0" : hidCustom.Value);

        return mlProductline;
    }
    public HttpPostedFile GetFileName()
    {
        string imgname = string.Empty;
        if (fplTitle.HasFile)
        {
            HttpPostedFile myFile = fplTitle.PostedFile;
            return myFile;
        }
        else
            return null;
    }

    /// <summary>
    /// 设置页面信息
    /// </summary>
    /// <param name="model"></param>
    public void InitPage(ProductlinesInfo mlProductLine)
    {
        if (mlProductLine != null)
        {
            ClientsInfo clients = ESP.Media.BusinessLogic.ClientsManager.GetModel(mlProductLine.Clientid);
            this.labClient.Text = clients == null ? "" : clients.Clientcfullname;
            this.txtProductLineName.Text = mlProductLine.Productlinename;
            this.txtDes.Text = mlProductLine.Productlinedescription;
            hidCustom.Value = mlProductLine.Clientid.ToString();
            txtCustom.Text = clients.Clientcfullname;
        }
    }
}
