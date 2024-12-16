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
using System.IO;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Common;
using ESP.Media.BusinessLogic;

public partial class Client_ProductLineAddAndEdit : ESP.Web.UI.PageBase
{
    int alertvalue = 0;
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", Request["Cid"]);
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", Request["Operate"]);
        if (Request["backurl"] != null)
        {
            this.hidUrl.Value = Request["backurl"] + string.Format("?{0}", param);
        }
        else
        {
            if (Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] != null)
                this.hidUrl.Value = Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage].ToString();
            else
                this.hidUrl.Value = "../Client/ProductLineList.aspx";

        }
        if (Request["alert"] != null)
        {
            this.backpage.Visible = true;
            this.btnBack.Visible = false;
        }
        else
        {
            this.backpage.Visible = false;
            this.btnBack.Visible = true;
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["Cid"]))
            {
                ClientsInfo client = ClientsManager.GetModel(int.Parse(Request["Cid"]));
                txtCustom.Text = client == null ? "" : client.Clientcfullname;
                hidCustom.Value = client == null ? "" : client.Clientid.ToString();
            }
            string operate = "null";

            if (Request["Operate"] != null)
            {
                operate = Request["Operate"];
            }
            if (operate == "EDIT")
            {
                btnLink.Text = "变更所属客户";
                InitPage();
            }
            else if (operate == "ADD")
            {
                this.Title = "添加产品线";
                this.btnOk.Text = "保存";
                this.labHeading.Text = "添加产品线";
                int Plid = Convert.ToInt32(Request["Plid"]);
                ClientsInfo mlClient = ESP.Media.BusinessLogic.ClientsManager.GetModel(Plid);
                imgTitle.Visible = false;

                this.imgTitle.Height = 20;
                this.imgTitle.Width = 20;
                if (mlClient != null)
                {
                    this.labClient.Text = mlClient.Clientcfullname;
                    this.btnOk.Enabled = true;
                }
            }
            else if (operate == "DEL")
            {
                int Plid = Convert.ToInt32(Request["Plid"]);
                string errmeg;
                int ret = ESP.Media.BusinessLogic.ProductlinesManager.Delete(GetObject(), out errmeg);
                if (ret > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ProductLineList.aspx';alert('删除成功！');", Plid, operate), true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
                }
            }
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
        this.btnOk.Text = "保存";
        this.labHeading.Text = "编辑产品线";
        if (Request["Plid"] != null && int.TryParse(Request["Plid"], out id))
        {
            ProductlinesInfo mlProductLine = ESP.Media.BusinessLogic.ProductlinesManager.GetModel(id);
            if (mlProductLine != null)
            {
                ClientsInfo clients = ESP.Media.BusinessLogic.ClientsManager.GetModel(mlProductLine.Clientid);
                this.labClient.Text = clients == null ? "" : clients.Clientcfullname;
                this.txtProductLineName.Text = mlProductLine.Productlinename;
                this.txtDes.Text = mlProductLine.Productlinedescription;
                hidCustom.Value = mlProductLine.Clientid.ToString();
                txtCustom.Text = clients.Clientcfullname;
                if (mlProductLine.Productlinetitle != string.Empty)
                {
                    this.imgTitle.Visible = true;
                    this.imgTitle.ImageUrl = mlProductLine.Productlinetitle;
                    
                }
                else 
                {
                    this.imgTitle.Visible = false;
                }
                this.imgTitle.Height = 20;
                this.imgTitle.Width = 20;
                this.btnOk.Enabled = true;
            }
        }
    }
    #endregion

    #region 获得对象
    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <returns></returns>
    private ProductlinesInfo GetObject()
    {
        ProductlinesInfo mlProductline = null;
        if (Request["Plid"] != null)
        {
            mlProductline = ESP.Media.BusinessLogic.ProductlinesManager.GetModel(Convert.ToInt32(Request["Plid"]));
        }
        else
        {
            mlProductline = new ProductlinesInfo();
        }
        if (Request["Cid"] != null && Request["Cid"].Length>0)
        {
            mlProductline.Clientid = Convert.ToInt32(Request["Cid"]);
        }
        mlProductline.Productlinename = this.txtProductLineName.Text;
        if (this.txtDes.Text.Trim().Length > 200)
            mlProductline.Productlinedescription = this.txtDes.Text.Trim().Substring(0, 200);
        else
            mlProductline.Productlinedescription = this.txtDes.Text.Trim();

        mlProductline.Createddate = mlProductline.Createddate == string.Empty ? DateTime.Now.ToString() : mlProductline.Createddate;
        mlProductline.Createdbyuserid = mlProductline.Createdbyuserid == 0 ? 1 : mlProductline.Createdbyuserid;
        mlProductline.Createdip = mlProductline.Createdip == string.Empty ? Request.UserHostAddress : mlProductline.Createdip;
        mlProductline.Lastmodifieddate = DateTime.Now.ToString();
        mlProductline.Lastmodifiedbyuserid = CurrentUserID;
        mlProductline.Lastmodifiedip = Request.UserHostAddress;
        mlProductline.Clientid = int.Parse(hidCustom.Value==""? "0" : hidCustom.Value);
     
        if (imgTitle.ImageUrl != null && imgTitle.ImageUrl.Length > 0)
        {
            //mlProductline.Productlinetitle = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.ProductLineLogoPath + imgTitle.ImageUrl);
            mlProductline.Productlinetitle = imgTitle.ImageUrl;
        }
        return mlProductline;
    }
    #endregion

    #region 确认
    /// <summary>
    /// Handles the Click event of the btnOk control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnOk_Click(object sender, System.EventArgs e)
    {
        int ret;
        int cid;
        string errmsg;
        //byte[] logodata = null;
        string logoname = string.Empty;
        if (fplTitle.HasFile)
        {
            HttpPostedFile myFile = fplTitle.PostedFile;

            if (myFile.FileName != null && myFile.ContentLength > 0)
            {
                string ext = "";
                for (int n = 0; n < Config.PHOTO_EXTENSIONS.Length; ++n)
                {
                    string FileExt = System.IO.Path.GetExtension(myFile.FileName).ToLower();
                    if (FileExt == Config.PHOTO_EXTENSIONS[n])
                    {
                        ext = Config.PHOTO_EXTENSIONS[n];
                        break;
                    }
                }
                if (ext.Length == 0)
                {
                    MessageBox.Show(this.Page, "图片格式错误，需要：GIF、JPEG或BMP文件格式！");
                    return;
                }
                else if (myFile.ContentLength > Config.PHOTO_CONTENT_LENGTH)
                {
                    MessageBox.Show(this.Page, "图片大小不得超过1024k！");
                    return;
                }
                else
                {
                    logoname = ImageHelper.SavePhoto(myFile.InputStream, Config.PhotoSizeSettings.LARGESIZE, myFile.ContentLength,CurrentUser.SysID, ESP.Configuration.ConfigurationManager.SafeAppSettings["ProductLineLogoPath"]).filename;
                }
            }
        }

        if (Request["Plid"] != null)
        {
            cid = Convert.ToInt32(Request["Plid"]);
        }
        string operate = Request["Operate"] == null ? "" : Request["Operate"];
        string url = Session[ESP.Media.Access.Utilities.Global.RequestKey.CurrentRootPage] == null ? SiteMap.RootNode.Url : Session[ESP.Media.Access.Utilities.Global.RequestKey.CurrentRootPage].ToString();
        if (Request["Plid"] == null)//插入
        {
            ret = ESP.Media.BusinessLogic.ProductlinesManager.Add(GetObject(), logoname,  CurrentUserID, out errmsg);
            if (ret > 0)
            {
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

                if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
                {

                    url = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request["backurl"]))
                        url = Request["backurl"];
                }
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", ret);
                if (!string.IsNullOrEmpty(Request["alert"]))
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());
                url = string.Format(url + "?{0}", param);
       

                if (Request["Cid"] == null)
                {
                    if(!string.IsNullOrEmpty(Request["alert"]))
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.opener.location.reload();alert('保存成功！');window.close();"), true);
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='{0}?{1}';alert('保存成功！');", "ProductLineDisplay.aspx", param), true);
                
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request["alert"]))
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.opener.location.reload();alert('保存成功！');window.close();"), true);
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("opener.location = opener.location;alert('保存成功！');window.close();", url), true);
                }
               
            }
            else if (ret == -1)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }
        }
        else//修改
        {
            ret = ESP.Media.BusinessLogic.ProductlinesManager.Update(GetObject(), logoname, CurrentUserID, out errmsg);
            if (ret > 0)
            {
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

                if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
                {

                    url = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request["backurl"]))
                        url = Request["backurl"];
                }
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", Request["Plid"]);
                if (!string.IsNullOrEmpty(Request["alert"]))
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());
                url = string.Format(url + "?{0}", param);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='{0}?{1}';alert('保存成功！');", "ProductLineDisplay.aspx", param), true);
               
            }
            else if (ret == -1)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string backUrl = "ProductLineList.aspx";
        if (!string.IsNullOrEmpty(Request["backurl"]))
            backUrl = Request["backurl"];
        else if (!string.IsNullOrEmpty(Request["Cid"]))
            backUrl = "ClientAddAndEdit.aspx";
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "EDIT");
        param = ESP.Media.Access.Utilities.Global.RemoveParam(param, "Plid");

        if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
        {

            backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
        }
        if (!string.IsNullOrEmpty(Request["alert"]))
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());

        Response.Redirect(string.Format(backUrl + "?{0}", param));
    }
    #endregion
}
