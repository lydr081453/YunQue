using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ESP.Media.Entity;

public partial class UserControl_ClientControl_ClientView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 设置页面信息
    /// </summary>
    /// <param name="mlClient"></param>
    public void InitPage(ClientsInfo mlClient)
    {
        if (mlClient != null)
        {
            if (mlClient.Lastmodifiedbyuserid > 0)
            {
                labLastModifyUser.Text = new ESP.Compatible.Employee(mlClient.Lastmodifiedbyuserid).Name;
            }
            if (mlClient.Lastmodifieddate != null && mlClient.Lastmodifieddate.Length > 0)
            {
                labLastModifyDate.Text = mlClient.Lastmodifieddate;
            }

            this.labClientName.Text = "公司名称：" + mlClient.Clientcfullname;
            this.labChFullName.Text = mlClient.Clientcfullname;
            this.labChShortName.Text = mlClient.Clientcshortname;
            this.labEnFullName.Text = mlClient.Clientefullname;
            this.labEnShortName.Text = mlClient.Clienteshortname;
            this.labBrief.Text = mlClient.Clientdescription;

            getFullImageUrl(mlClient.Clientlogo);
            //if (mlClient.Clientlogo != string.Empty)
            //{
            //    this.imgLogo.ImageUrl = mlClient.Clientlogo.Replace(".jpg", "_225.jpg");
            //    imgTitleFull.ImageUrl = mlClient.Clientlogo.Replace(".jpg", "_full.jpg");
            //}
            //else
            //{
            //    this.imgLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
            //    this.imgLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath.Replace(".jpg", "_full.jpg");
            //}
        }
    }

    private void getFullImageUrl(string imgurl)
    {
        if (string.IsNullOrEmpty(imgurl))
        {
            this.imgLogo.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["ImageUrl"].ToString() + ESP.Configuration.ConfigurationManager.SafeAppSettings["DefauleImgPath"].ToString().Replace(".jpg", "_full.jpg");
        }
        else
            this.imgLogo.ImageUrl = imgurl.Replace(".jpg", "_full.jpg");
    }
}
