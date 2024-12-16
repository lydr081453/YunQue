using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Media.Entity;
using ESP.Compatible;
public partial class Client_ProductLineContentsDisplay : ESP.Web.UI.PageBase
{
    int alertvalue = 0;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Request.Url.Query;
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertvalue = Convert.ToInt32(Request["alert"]);
            param = "?" + ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue - 1).ToString());
        }
        
        this.hidUrl.Value = ESP.Media.Access.Utilities.Global.Url.ProductLineContentsList+param;
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    #region 绑定页面信息
    /// <summary>
    /// Inits the page.
    /// </summary>
    private void InitPage()
    {
        int id;
        this.Title = "编辑产品线";
        if (Request["Plcid"] != null && int.TryParse(Request["Plcid"], out id))
        {
            ProductlineshistInfo mlProductLine = ESP.Media.BusinessLogic.ProductlinesManager.GetHistModel(id);
            if (mlProductLine != null)
            {
                if (mlProductLine.Lastmodifiedbyuserid > 0)
                {
                    labLastModifyUser.Text = new Employee(mlProductLine.Lastmodifiedbyuserid).Name;
                }
                if (mlProductLine.Lastmodifieddate != null && mlProductLine.Lastmodifieddate.Length > 0)
                {
                    labLastModifyDate.Text = mlProductLine.Lastmodifieddate;
                }

                this.labVersion.Text = mlProductLine.Version + "";
                this.labName.Text = "产品线：" + mlProductLine.Productlinename;
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
            }
        }
    }
    #endregion
}
