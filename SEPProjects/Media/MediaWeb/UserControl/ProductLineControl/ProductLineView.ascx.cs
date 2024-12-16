using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.Entity;

public partial class UserControl_ProductLineControl_ProductLineView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPage(ProductlinesInfo mlProductLine)
    {
        if (mlProductLine != null)
        {
            if (mlProductLine.Lastmodifiedbyuserid > 0)
            {
                labLastModifyUser.Text = new ESP.Compatible.Employee(mlProductLine.Lastmodifiedbyuserid).Name;
            }
            if (mlProductLine.Lastmodifieddate != null && mlProductLine.Lastmodifieddate.Length > 0)
            {
                labLastModifyDate.Text = mlProductLine.Lastmodifieddate;
            }
            this.labProductLineName.Text = mlProductLine.Productlinename;
            this.labDes.Text = mlProductLine.Productlinedescription;
            this.imgTitle.Visible = true;
            if (mlProductLine.Productlinetitle != string.Empty)
            {

                this.imgTitle.ImageUrl = mlProductLine.Productlinetitle;
                imgTitleFull.ImageUrl = mlProductLine.Productlinetitle.Replace(".jpg", "_full.jpg");
            }
            else
            {
                this.imgTitle.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
                this.imgTitle.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath.Replace(".jpg", "_full.jpg");
            }
            this.imgTitle.Width = 80;
            this.imgTitle.Height = 80;

            if (mlProductLine.Clientid > 0)
            {
                ClientsInfo client = ESP.Media.BusinessLogic.ClientsManager.GetModel(mlProductLine.Clientid);
                if (client != null)
                {
                    this.lnkClientName.Text = client.Clientcfullname;
                }
            }
        }
    }
}
