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
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Common;
using ESP.Media.BusinessLogic;

public partial class Client_ClientAddAndEdit : ESP.Web.UI.PageBase
{
    int id = 0;
    int alertvalue = 0;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ddlCategory.Attributes.Add("onChange", "getCategory(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text)");
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ClientAddAndEdit + string.Format("?Cid={0}&Operate={1}", Request["Cid"], Request["Operate"]);
        if (!IsPostBack)
        {
            this.hidUrl.Value = ESP.Media.Access.Utilities.Global.Url.ClientList;

            string operate = "null";
            this.btnOk.Enabled = false;

            this.ddlCategory.DataSource = ClientCategoryManager.getAll();
            ddlCategory.DataTextField = "categoryname";
            ddlCategory.DataValueField = "categoryid";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("请选择", "0"));

            if (Request["Operate"] != null)
            {
                operate = Request["Operate"];
                btnLink.Visible = false;
                pProduct.Visible = false;

                if (operate == "EDIT")
                {
                    InitPage();
                }
                else if (operate == "ADD")
                {
                    this.btnOk.Enabled = true;
                    this.imgLogo.Visible = true;
                    //this.imgLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
                    this.imgLogo.Visible = false;
                    this.imgLogo.Height = 36;
                    this.imgLogo.Width = 36;
                }
                else if (operate == "DEL")
                {
                    string errmsg;
                    int ret = ClientsManager.Delete(GetObject(), out errmsg);
                    if (ret > 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ClientList.aspx?Operate={0}';alert('删除成功！');", operate), true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
                    }
                }
                else if (operate == "DELProductLine")
                {
                    string errmsg;
                    if (Request["Plid"] != null)
                    {

                        int ret = ProductlinesManager.DeleteRelation(Convert.ToInt32(Request["Plid"]), out errmsg);
                        if (ret > 0)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ClientAddAndEdit.aspx?Cid={0}&Operate=EDIT&CientOperate=';alert('删除成功');", Request["Cid"]), true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ClientAddAndEdit.aspx?Cid={0}&Operate=EDIT&CientOperate=';alert('{1}');", Request["Cid"], errmsg), true);
                        }
                    }
                }
            }
        }
        setAddProductLine();
    }

    #region 绑定页面信息
    /// <summary>
    /// Inits the page.
    /// </summary>
    private void InitPage()
    {
        btnLink.Visible = true;
        pProduct.Visible = true;
        this.Title = "编辑客户";
        this.btnOk.Text = "保存";
        this.labHeading.Text = "编辑客户";
        if (Request["Cid"] != null)
        {
            hidClientId.Value = Request["Cid"];
            if (int.TryParse(Request["Cid"], out id))
            {
                ClientsInfo mlClient = ClientsManager.GetModel(id);
                if (mlClient != null)
                {
                    this.txtChFullName.Text = mlClient.Clientcfullname;
                    this.txtChShortName.Text = mlClient.Clientcshortname;
                    this.txtEnFullName.Text = mlClient.Clientefullname;
                    this.txtEnShortName.Text = mlClient.Clienteshortname;
                    this.txtDes.Text = mlClient.Clientdescription;
                    if (mlClient.Clientlogo != string.Empty)
                    {
                        this.imgLogo.Visible = true;
                        this.imgLogo.ImageUrl = mlClient.Clientlogo;
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
                    this.btnOk.Enabled = true;
                   
                    for (int i = 0; i < ddlCategory.Items.Count; i++)
                    {
                        if (ddlCategory.Items[i].Value == mlClient.Categoryid.ToString())
                        {
                            this.ddlCategory.SelectedIndex = i;
                            this.hidCategoryid.Value = this.ddlCategory.Items[i].Value;
                            this.HidCategoryName.Value = this.ddlCategory.Items[i].Text;
                            break;
                        }
                    }

                }
                ListBind(id);
            }
        }

      
    }
    #endregion

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "ProductLineTitle#ProductLineName#ProductLineDescription#ProductLineID#ProductLineID";
        string strHeader = "产品线图片#产品线名称#描述#编辑#删除";
        string sort = "#ProductLineName#ProductLineDescription###";
        string strH = "center###center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
    }
    #endregion

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    /// <param name="clientID">The client ID.</param>
    private void ListBind(int clientID)
    {
        string term = "ClientID = @ClientID";
        Hashtable ht = new Hashtable();
        ht.Add("@ClientID", clientID);
        DataTable dt = ProductlinesManager.GetList(term, ht);
        this.dgList.DataSource = dt.DefaultView;

    }

    /// <summary>
    /// Handles the RowDataBound event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", e.Row.Cells[3].Text);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "backurl", ESP.Media.Access.Utilities.Global.Url.ClientAddAndEdit);

            if (e.Row.Cells[0].Text == "&nbsp;")
            {
                e.Row.Cells[0].Text = string.Format("<img  src='{0}'style='width:80px;height:60px;'  runat='server' onclick=\"window.location='ProductLineDisplay.aspx?Plid={1}';\"/>", ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath, e.Row.Cells[3].Text);
            }
            else
            {
                e.Row.Cells[0].Text = string.Format("<img  src='{0}'style='width:80px;height:60px;'  runat='server' onclick=\"window.location='ProductLineDisplay.aspx?Plid={1}';\"/>", e.Row.Cells[0].Text.Substring(e.Row.Cells[0].Text.IndexOf("~") + 1), e.Row.Cells[3].Text);
            }

            e.Row.Cells[0].Wrap = false;

            e.Row.Cells[1].Text = string.Format("<a onclick=\"window.location='ProductLineDisplay.aspx?Plid={0}';\">{1}</a>", e.Row.Cells[3].Text, e.Row.Cells[1].Text);
            //e.Row.Cells[3].Text = string.Format("<a href='ProductLineDisplay.aspx?Plid={0}&Cid={1}&Operate={2}&backurl={3}'><img src='{4}' /></a>", e.Row.Cells[3].Text, Request["Cid"],Request["Operate"] ,ESP.Media.Access.Utilities.Global.Url.ClientAddAndEdit, ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath);

            e.Row.Cells[3].Text = string.Format("<a href='ProductLineAddAndEdit.aspx?Operate=EDIT&Plid={0}&Cid={1}&backurl={2}' ><img src='{3}' /></a>", e.Row.Cells[3].Text, Request["Cid"], ESP.Media.Access.Utilities.Global.Url.ClientAddAndEdit, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);

            e.Row.Cells[4].Text = string.Format("<a href='ClientAddAndEdit.aspx?Operate=DELProductLine&Plid={0}&Cid={1}' onclick= \"return confirm( '真的要删除吗?');\" ><img src='{2}' /></a>", e.Row.Cells[4].Text, Request["Cid"], ESP.Media.Access.Utilities.ConfigManager.DelIconPath);

        }
    }
    #endregion

    #region 获得对象
    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <returns></returns>
    private ClientsInfo GetObject()
    {
        ClientsInfo mlClient = null;
        if (Request["Cid"] != null)
        {
            mlClient = ClientsManager.GetModel(Convert.ToInt32(Request["Cid"]));
        }
        else
        {
            mlClient = new ClientsInfo();
        }
        mlClient.Clientcfullname = txtChFullName.Text.Trim();
        mlClient.Clientcshortname = txtChShortName.Text.Trim();
        mlClient.Clientefullname = txtEnFullName.Text.Trim();
        mlClient.Clienteshortname = txtEnShortName.Text.Trim();
        if (txtDes.Text.Trim().Length > 1024)
            mlClient.Clientdescription = txtDes.Text.Trim().Substring(0, 1024);
        else
            mlClient.Clientdescription = txtDes.Text.Trim();
        mlClient.Createddate = mlClient.Createddate == string.Empty ? DateTime.Now.ToString() : mlClient.Createddate;
        mlClient.Createdbyuserid = mlClient.Createdbyuserid == 0 ? 1 : mlClient.Createdbyuserid;
        mlClient.Createdip = mlClient.Createdip == string.Empty ? Request.UserHostAddress : mlClient.Createdip;
        mlClient.Lastmodifieddate = DateTime.Now.ToString();
        mlClient.Lastmodifiedbyuserid = CurrentUserID;
        mlClient.Lastmodifiedip = Request.UserHostAddress;
        if (imgLogo.ImageUrl != null && imgLogo.ImageUrl.Length > 0)
        {
            mlClient.Clientlogo = imgLogo.ImageUrl;
        }
        if (this.hidCategoryid.Value != "")
            mlClient.Categoryid = Convert.ToInt32(this.hidCategoryid.Value);
        else
            mlClient.Categoryid = 0;
        mlClient.Categoryname = HidCategoryName.Value;

        return mlClient;
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
        string errmsg;
        //byte[] logodata = null;
        string logoname = string.Empty;
        if (fplLogo.HasFile)
        {
            //logodata = fplLogo.FileBytes;
            //logoname = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.ClientLogoPath + fplLogo.FileName);
            HttpPostedFile myFile = fplLogo.PostedFile;

            if (myFile.FileName != null && myFile.ContentLength > 0)//&& theFile.ContentLength <= Config.PHOTO_CONTENT_LENGTH)
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
                    logoname = ImageHelper.SavePhoto(myFile.InputStream, Config.PhotoSizeSettings.LARGESIZE, myFile.ContentLength, CurrentUser.SysID, ESP.Configuration.ConfigurationManager.SafeAppSettings["ProductLineLogoPath"]).filename;
                }
            }
        }
        string operate = Request["CientOperate"] == null ? "" : Request["CientOperate"];
        string url = "ClientDetail.aspx";
        if (Request["Cid"] == null)//插入
        {
            ret = ClientsManager.Add(GetObject(), logoname, CurrentUserID, out errmsg);
            if (ret > 0)
            {
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
                {

                    url = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
                }
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", ret);
                if (!string.IsNullOrEmpty(Request["alert"]))
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());
                url = string.Format(url + "?{0}", param);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='{0}';alert('保存成功！');", url), true);
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
            ret = ClientsManager.Update(GetObject(), logoname, CurrentUserID, out errmsg);
            if (ret > 0)
            {
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
                {

                    url = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
                }
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", Request["Cid"]);
                if (!string.IsNullOrEmpty(Request["alert"]))
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());
                url = string.Format(url + "?{0}", param);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='{0}';alert('保存成功！');", url), true);
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
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string backUrl = "ClientList.aspx";
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

        if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
        {

            backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
        }
        if (!string.IsNullOrEmpty(Request["alert"]))
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());

        Response.Redirect(string.Format(backUrl + "?{0}", param));
    }

    private void setAddProductLine()
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "ADD");
        //if (alertvalue > 0)
        //{
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
        //}
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "backurl", "ClientAddAndEdit.aspx");
        string url = string.Format(@"ProductLineAddAndEdit.aspx?{0}", param);
        //Response.Redirect(url);
        btnLink.Attributes["onclick"] = "javascript:window.open('" + url + "','','" + ESP.Media.Access.Utilities.Global.OpenClass.Common + "');return false;";
    }
}
