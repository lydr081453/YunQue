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
using ESP.Compatible;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Common;

public partial class Media_MediaAddAndEdit : ESP.Web.UI.PageBase
{
    UserControl uc = null;
    string operate = "null";
    string mediaType = "null";
    string errmsg = null;
    string source = string.Empty;
    MediaitemsInfo media = null;
    public DropDownList ddlIndustry = null;

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
        int userid = UserID;

    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.MediaitemsManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.CountryManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.CityManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.ProvinceManager));

        hidUrl.Value = ESP.Media.Access.Utilities.Global.Url.MediaList;
        if (Request["listadd"] != null)
        {
            //this.btnBack.Visible = false;
        }
       
        InitPage();
    }

    #region 初始化页面信息
    /// <summary>
    /// Inits the page.
    /// </summary>
    private void InitPage()
    {
        if (Request["Source"] != null)
        {
            source = Request["Source"];
            this.hidUrl.Value = Request["Source"];
        }
        else
        {
            if (Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] != null)
                this.hidUrl.Value = Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage].ToString();
            else
                this.hidUrl.Value = "../Media/MediaList.aspx";
        }


        //this.btnOk.Enabled = false;
        if (Request["MediaType"] != null)
        {
            mediaType = Request["MediaType"];
        }
        if (Request["Operate"] != null)
        {
            operate = Request["Operate"];
        }

        if (operate == "EDIT")
        {
            InitEdit(mediaType);

        }
        else if (operate == "ADD")
        {
            InitAdd();
        }
        else if (operate == "Del")
        {
            if (Request["Mid"] != null)
            {

                int ret = ESP.Media.BusinessLogic.MediaitemsManager.Delete(ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"])), out errmsg);
                if (ret > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='MediaList.aspx';alert('删除成功！');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='MediaList.aspx';alert('{0}');", errmsg), true);
                }
            }
        }
        else if (operate == "DELReporter")
        {
            if (Request["Rid"] != null)
            {

                int ret = ESP.Media.BusinessLogic.ReportersManager.DeleteRelation(Convert.ToInt32(Request["Rid"]), out errmsg);
                if (ret > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='MediaAddAndEdit.aspx?Mid={0}&Operate=EDIT';alert('删除成功');", Request["Mid"]), true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='MediaAddAndEdit.aspx?Mid={0}&Operate=EDIT';alert('{1}');", Request["Mid"], errmsg), true);
                }
            }
        }
        if (mediaType.Equals("plane"))
        {
            uc = (UserControl)this.LoadControl("skins/PlaneMediaAddAndEdit.ascx");
            uc.ID = "ucPlane";
            ddlIndustry = (DropDownList)uc.FindControl("ddlIndustry");
            panelMediaAddAndEdit.Controls.Add(uc);

        }
        else if (mediaType.Equals("tv"))
        {
            uc = (UserControl)this.LoadControl("skins/TvMediaAddAndEdit.ascx");
            uc.ID = "ucTV";
            ddlIndustry = (DropDownList)uc.FindControl("ddlIndustry");
            panelMediaAddAndEdit.Controls.Add(uc);
        }

        else if (mediaType.Equals("web"))
        {
            uc = (UserControl)this.LoadControl("skins/WebMediaAddAndEdit.ascx");
            uc.ID = "ucWeb";
            ddlIndustry = (DropDownList)uc.FindControl("ddlIndustry");
            panelMediaAddAndEdit.Controls.Add(uc);
        }
        else if (mediaType.Equals("dab"))
        {
            uc = (UserControl)this.LoadControl("skins/DABMediaAddAndEdit.ascx");
            ddlIndustry = (DropDownList)uc.FindControl("ddlIndustry");
            uc.ID = "ucDab";
            panelMediaAddAndEdit.Controls.Add(uc);
        }


    }
    #endregion

    /// <summary>
    /// Inits the add.
    /// </summary>
    private void InitAdd()
    {

        this.Title = "添加媒体";
        //this.btnOk.Text = "添加";
        //this.btnOk.Enabled = true;

        btnLink.Visible = false;
        pReaporter.Visible = false;

    }

    /// <summary>
    /// Inits the edit.
    /// </summary>
    /// <param name="mediaType">Type of the media.</param>
    private void InitEdit(string mediaType)
    {
        btnLink.Visible = true;
        pReaporter.Visible = true;
        this.Title = "编辑媒体";
        //this.btnOk.Text = "修改";
        if (Request["Mid"] != null)
        {
            hidMediaId.Value = Request["Mid"];
            media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"]));
            if (media != null)
            {
                if (media.Mediaitemtype == 1)
                {
                    uc = (UserControl)this.LoadControl("skins/PlaneMediaAddAndEdit.ascx");
                    uc.ID = "ucPlane";
                    panelMediaAddAndEdit.Controls.Add(uc);
                    ddlIndustry = (DropDownList)uc.FindControl("ddlIndustry");
                    ((Media_skins_PlaneMediaAddAndEdit)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 3)
                {
                    uc = (UserControl)this.LoadControl("skins/TvMediaAddAndEdit.ascx");
                    uc.ID = "ucTV";
                    panelMediaAddAndEdit.Controls.Add(uc);
                   //ddlIndustry = (DropDownList)uc.FindControl("ddlIndustry");
                    ((Media_skins_TvMediaAddAndEdit)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 2)
                {
                    uc = (UserControl)this.LoadControl("skins/WebMediaAddAndEdit.ascx");
                    uc.ID = "ucWeb";
                    panelMediaAddAndEdit.Controls.Add(uc);
                    ddlIndustry = (DropDownList)uc.FindControl("ddlIndustry");
                    ((Media_skins_WebMediaAddAndEdit)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 4)
                {
                    uc = (UserControl)this.LoadControl("skins/DABMediaAddAndEdit.ascx");
                    uc.ID = "ucDab";
                   ddlIndustry = (DropDownList)uc.FindControl("ddlIndustry");
                    panelMediaAddAndEdit.Controls.Add(uc);
                    ((Media_skins_DABMediaAddAndEdit)uc).InitPage(media);
                }
                if (media.Status == (int)ESP.Media.Access.Utilities.Global.MediaAuditStatus.Save)
                {
                    this.btnSubmit.Visible = true;
                    this.btnOk.Visible = false;
                }
                else
                {
                    this.btnSubmit.Visible = false;
                    this.btnOk.Visible = true;
                }
            }
            //this.btnOk.Enabled = true;
            ListBind(media.Mediaitemid);

        }
    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        //string strColumn = "ReporterID#ReporterName#Sex#Birthday#UsualMobile#Tel_O#QQ#MSN#Experience#ReporterID#ReporterID#ReporterID";
        //string strHeader = "选择#姓名#性别#出生日期#手机#固话#QQ#MSN#工作单位#查看#编辑#删除";
        //string sort = "#ReporterName#Sex#Birthday#########";
        //MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
        string strColumn = "ReporterID#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email#ReporterID#ReporterID";
        string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱#编辑#删除";
        string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain#####";
        string strH = "center#########center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
    }
    #endregion

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    /// <param name="mid">The mid.</param>
    private void ListBind(int mid)
    {
        DataTable dt = ESP.Media.BusinessLogic.ReportersManager.GetList("and Media_ID=" + mid.ToString(), null);
        this.dgList.DataSource = dt.DefaultView;

    }

    /// <summary>
    /// Handles the RowDataBound event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int mid = 0;
        if (Request["Mid"] != null)
            mid = Convert.ToInt32(Request["Mid"]);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type='checkbox'  value='{0}'/>", e.Row.Cells[0].Text);
            e.Row.Cells[0].Width = 40;

            //e.Row.Cells[1].Wrap = false;

            //if (e.Row.Cells[2].Text == "1")
            //    e.Row.Cells[2].Text = "男";
            //else if (e.Row.Cells[2].Text == "2")
            //    e.Row.Cells[2].Text = "女";
            //else
            //    e.Row.Cells[2].Text = "未知";
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Text = e.Row.Cells[3].Text.Split(' ')[0];
            //if (e.Row.Cells[3].Text.Equals("1900-1-1"))
            //{
            //    e.Row.Cells[3].Text = "";
            //}
            //e.Row.Cells[3].Wrap = false;
            //e.Row.Cells[4].Wrap = false;
            //e.Row.Cells[5].Wrap = false;
            //e.Row.Cells[6].Wrap = false;
            //e.Row.Cells[7].Wrap = false;

            //e.Row.Cells[8].Text = GetWorkString(e.Row.Cells[8].Text);
            //e.Row.Cells[8].Wrap = false;
            int reprotId = int.Parse(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
            e.Row.Cells[1].Text = string.Format("<a onclick=\"window.open('ReporterDisplay.aspx?alert=1&Rid={0}','','{2}')\";>{1}</a>", reprotId, e.Row.Cells[1].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common);
            //  e.Row.Cells[9].Text = string.Format("<a href='ReporterDisplay.aspx?Rid={0}&Mid={1}' ><img src='{2}' /></a>", e.Row.Cells[9].Text, Request["Mid"], ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath);
            //e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            //e.Row.Cells[9].Width = 0;


            e.Row.Cells[9].Text = string.Format("<a onclick=\"window.open('ReporterAddAndEdit.aspx?alert=1&Operate=EDIT&Rid={0}&Mid={1}','','{3}');\" ><img src='{2}' /></a>", e.Row.Cells[9].Text, mid, ESP.Media.Access.Utilities.ConfigManager.EditIconPath, ESP.Media.Access.Utilities.Global.OpenClass.Common);

            e.Row.Cells[10].Text = string.Format("<a href='MediaAddAndEdit.aspx?Operate=DELReporter&Rid={0}&Mid={1}' onclick= \"return confirm( '真的要删除吗?');\" ><img src='{2}' /></a>", e.Row.Cells[10].Text, mid, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);

        }
    }

    /// <summary>
    /// Gets the work string.
    /// </summary>
    /// <param name="xml">The XML.</param>
    /// <returns></returns>
    private string GetWorkString(string xml)
    {
        xml = Server.HtmlDecode(xml);
        Media_skins_Experience.InitExperienceTable();
        DataTable dt = Media_skins_Experience.ExperienceTable.Clone();
        System.IO.StringReader sr = new System.IO.StringReader(xml);
        dt.ReadXml(sr);
        if (dt.Rows.Count > 0)
            return dt.Rows[0]["单位名称"].ToString();
        else
            return xml;
    }
    #endregion

    /// <summary>
    /// Handles the Click event of the btnOk control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        int ret;

       // FileUpload Adsprice = null;
        FileUpload Logo = null;
        FileUpload Briefing = null;
        MediaindustryrelationInfo[] industries = null;
        ESP.Media.BusinessLogic.MediaAttach attach = new ESP.Media.BusinessLogic.MediaAttach();
        System.Collections.Generic.List<BranchInfo> branchs = null;
        MediaitemsInfo media = null;
        if (Request["Mid"] != null)
        {
            media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"]));
        }
        else
        {
            media = new MediaitemsInfo();
        }

        if (mediaType.Equals("plane") || media.Mediaitemtype == 1)
        {
            Media_skins_PlaneMediaAddAndEdit plane = panelMediaAddAndEdit.FindControl("ucPlane") as Media_skins_PlaneMediaAddAndEdit;
            media = plane.GetObject();
            //Adsprice = plane.Adsprice;
            Logo = plane.Logo;
            Briefing = plane.Briefing;
            industries = plane.Industries;
            branchs = plane.GetBranchObjects();
        }
        else if (mediaType.Equals("tv") || media.Mediaitemtype == 3)
        {
            Media_skins_TvMediaAddAndEdit tv = panelMediaAddAndEdit.FindControl("ucTv") as Media_skins_TvMediaAddAndEdit;
            media = tv.GetObject();
            //Adsprice = tv.Adsprice;
            Logo = tv.Logo;
            Briefing = tv.Briefing;
            industries = tv.Industries;
            branchs = tv.GetBranchObjects();
        }

        else if (mediaType.Equals("web") || media.Mediaitemtype == 2)
        {
            Media_skins_WebMediaAddAndEdit web = panelMediaAddAndEdit.FindControl("ucWeb") as Media_skins_WebMediaAddAndEdit;
            media = web.GetObject();
            //Adsprice = web.Adsprice;
            Logo = web.Logo;
            Briefing = web.Briefing;
            industries = web.Industries;
            branchs = web.GetBranchObjects();
        }
        else if (mediaType.Equals("dab") || media.Mediaitemtype == 4)
        {
            Media_skins_DABMediaAddAndEdit dab = panelMediaAddAndEdit.FindControl("ucDab") as Media_skins_DABMediaAddAndEdit;
            media = dab.GetObject();
            //Adsprice = dab.Adsprice;
            Logo = dab.Logo;
            Briefing = dab.Briefing;
            industries = dab.Industries;
            branchs = dab.GetBranchObjects();
        }

        //if (Adsprice.HasFile)
        //{
        //    attach.PriceFileData = Adsprice.FileBytes;
        //    attach.PriceFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaPricePath + Adsprice.FileName);
        //}

        if (Logo.HasFile)
        {
            ////attach.LogoFileData = Logo.FileBytes;
            ////attach.LogoFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaLogoPath + Logo.FileName);
            HttpPostedFile myFile = Logo.PostedFile;

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
                    attach.LogoFileName = ImageHelper.SavePhoto(myFile.InputStream, Config.PhotoSizeSettings.LARGESIZE, myFile.ContentLength, CurrentUser.SysID, ESP.Configuration.ConfigurationManager.SafeAppSettings["ProductLineLogoPath"]).filename;
                }
            }
        }

        if (Briefing.HasFile)
        {
            attach.BriefFileData = Briefing.FileBytes;
            attach.BriefFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaBriefPath + Briefing.FileName);
        }

        if (operate == "ADD")
        {
            media.Createddate = DateTime.Now.ToString();
            media.Createdbyuserid = int.Parse(CurrentUser.SysID);
            media.Lastmodifieddate = DateTime.Now.ToString();
            media.Lastmodifiedbyuserid = int.Parse(CurrentUser.SysID);
            media.Status = (int)ESP.Media.Access.Utilities.Global.MediaAuditStatus.FirstLevelAudit;
            int Mid = ESP.Media.BusinessLogic.MediaitemsManager.Add(media,branchs, attach, out errmsg, int.Parse(CurrentUser.SysID));
            if (Mid > 0)
            {
                string backUrl = "MediaDisplay.aspx";
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

                if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
                {

                    backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
                }
                if (!string.IsNullOrEmpty(Request["alert"]))
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "Mid", Mid);
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "Edit");
                param = ESP.Media.Access.Utilities.Global.RemoveParam(param, "MediaType");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='" + string.Format(backUrl + "?{0}", param) + "';alert('保存成功！');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }
        }
        else if (operate == "EDIT")
        {
            ret = ESP.Media.BusinessLogic.MediaitemsManager.Update(media, branchs, attach, CurrentUserID, out errmsg);
            if (ret > 0)
            {
                string backUrl = "MediaDisplay.aspx";
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

                if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
                {

                    backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
                }
                if (!string.IsNullOrEmpty(Request["alert"]))
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='" + string.Format(backUrl + "?{0}", param) + "';alert('保存成功！');", true);
        
                //if (string.IsNullOrEmpty(Request["Source"]))
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='MediaDisplay.aspx?Mid=" + Request["Mid"] + "';alert('修改成功！');", true);
                //else if (Request["Source"] == "Audit")
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='MediaDisplay.aspx?backUrl=AuditedMediaList.aspx&Mid=" + Request["Mid"] + "';alert('修改成功！');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }

        }
    }

    /// <summary>
    /// Handles the Click event of the btnSubmit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ret;

       // FileUpload Adsprice = null;
        FileUpload Logo = null;
        FileUpload Briefing = null;
        MediaindustryrelationInfo[] industries = null;
        System.Collections.Generic.List<BranchInfo> branchs = null;
        ESP.Media.BusinessLogic.MediaAttach attach = new ESP.Media.BusinessLogic.MediaAttach();
        MediaitemsInfo media = null;
        if (Request["Mid"] != null)
        {
            media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"]));
        }
        else
        {
            media = new MediaitemsInfo();
        }

        if (mediaType.Equals("plane") || media.Mediaitemtype == 1)
        {
            Media_skins_PlaneMediaAddAndEdit plane = panelMediaAddAndEdit.FindControl("ucPlane") as Media_skins_PlaneMediaAddAndEdit;
            media = plane.GetObject();
        //    Adsprice = plane.Adsprice;
            Logo = plane.Logo;
            Briefing = plane.Briefing;
            industries = plane.Industries;
            branchs = plane.GetBranchObjects();
        }
        else if (mediaType.Equals("tv") || media.Mediaitemtype == 3)
        {
            Media_skins_TvMediaAddAndEdit tv = panelMediaAddAndEdit.FindControl("ucTv") as Media_skins_TvMediaAddAndEdit;
            media = tv.GetObject();
         //   Adsprice = tv.Adsprice;
            Logo = tv.Logo;
            Briefing = tv.Briefing;
            industries = tv.Industries;
            branchs = tv.GetBranchObjects();

        }

        else if (mediaType.Equals("web") || media.Mediaitemtype == 2)
        {
            Media_skins_WebMediaAddAndEdit web = panelMediaAddAndEdit.FindControl("ucWeb") as Media_skins_WebMediaAddAndEdit;
            media = web.GetObject();
           // Adsprice = web.Adsprice;
            Logo = web.Logo;
            Briefing = web.Briefing;
            industries = web.Industries;
            branchs = web.GetBranchObjects();
        }
        else if (mediaType.Equals("dab") || media.Mediaitemtype == 4)
        {
            Media_skins_DABMediaAddAndEdit dab = panelMediaAddAndEdit.FindControl("ucDab") as Media_skins_DABMediaAddAndEdit;
            media = dab.GetObject();
           // Adsprice = dab.Adsprice;
            Logo = dab.Logo;
            Briefing = dab.Briefing;
            industries = dab.Industries;
            branchs = dab.GetBranchObjects();
        }

        //if (Adsprice.HasFile)
        //{
        //    attach.PriceFileData = Adsprice.FileBytes;
        //    attach.PriceFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaPricePath + Adsprice.FileName);
        //}

        if (Logo.HasFile)
        {
            //attach.LogoFileData = Logo.FileBytes;
            //attach.LogoFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaLogoPath + Logo.FileName);

            HttpPostedFile myFile = Logo.PostedFile;

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
                    attach.LogoFileName =
                        ImageHelper.SavePhoto(myFile.InputStream, Config.PhotoSizeSettings.LARGESIZE,
                                              myFile.ContentLength, CurrentUser.SysID,
                                              ESP.Configuration.ConfigurationManager.SafeAppSettings["ProductLineLogoPath"]).filename;
                }
            }
        }

        if (Briefing.HasFile)
        {
            attach.BriefFileData = Briefing.FileBytes;
            attach.BriefFileName = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.MediaBriefPath + Briefing.FileName);
        }


        media.Status = (int)ESP.Media.Access.Utilities.Global.MediaAuditStatus.FirstLevelAudit;

        ret = ESP.Media.BusinessLogic.MediaitemsManager.Update(media, branchs, attach, CurrentUserID, out errmsg);
        if (ret > 0)
        {
            string backUrl = "MediaDisplay.aspx";
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

            if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
            {

                backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
            }
            if (!string.IsNullOrEmpty(Request["alert"]))
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='" + string.Format(backUrl + "?{0}", param) + "';alert('保存成功！');", true);
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
        }


    }

    /// <summary>
    /// Handles the Click event of the btnBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string backUrl = "AuditedMediaList.aspx";
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        if (!string.IsNullOrEmpty(Request["backurl"]))
        {
            backUrl = Request["backurl"];
            if (Request["backurl"].StartsWith("AuditedMediaList.aspx"))
            { param = param.Replace("backurl", "outtimeurl"); }
        }

        if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
        {

            backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
        }

        if (!string.IsNullOrEmpty(Request["alert"]))
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());

        Response.Redirect(string.Format(backUrl + "?{0}", param));
    }
}