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

public partial class Media_ReporterAddAndEdit : ESP.Web.UI.PageBase
{
    int Mid = 0;
    int Rid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["type"]) && Request["type"] == "project")
            {
                tbPrivacy.Visible = true;
                tbPrivacyList.Visible = true;
                ListBind();
            }
            string[] paymentModes = ESP.Media.Access.Utilities.Global.PaymentModeName;
            for (int i = 1; i < paymentModes.Length; i++)
            {
                ddlpaymentmode.Items.Add(new ListItem(paymentModes[i].ToString(), (i).ToString()));
            }
            ddlpaymentmode.Items.Insert(0, new ListItem("请选择", "0"));            
            InitPage();
        }

        if (string.IsNullOrEmpty(Request["alert"]))
        {
            btnClose.Visible = false;
            
        }
        else
        {
            btnBack.Visible = false;
            
        }
        if (!string.IsNullOrEmpty(Request["Mid"]))
        {
            Mid = int.Parse(Request["Mid"]);
            if (Mid > 0)
            {
                MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Mid);
                txtMediaName.Text = media.Mediacname + " " + media.Channelname + " " + media.Topicname;
                hidMedia.Value = media.Mediaitemid.ToString();
            }
            else
            {
                btnLink.Text = "关联所属媒体";
            }
        }
        if(string.IsNullOrEmpty(Request["Rid"]))
            btnLink.Attributes["onclick"] = "javascript:window.open('ReporterSelectMediaList.aspx?alert=1&Rid="+Rid+"&Mid="+Mid+"&Operate=New','关联所属媒体','" + ESP.Media.Access.Utilities.Global.OpenClass.Common + "');return false;";
        if (!string.IsNullOrEmpty(Request["Operate"]) && Request["Operate"] == "ADD" && !string.IsNullOrEmpty(Request["alert"]))
        {
            btnLink.Visible = false;
        }
    }

    protected void btnLink_Click(object sender, EventArgs e)
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);       
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Rid", Request["Rid"]);
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Mid", Mid.ToString());
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "MediaSelect");
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "backurl", "ReporterAddAndEdit.aspx");
        string url = string.Format(@"ReporterSelectMediaList.aspx?{0}", param);
        Response.Redirect(url);
    }

    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
        int userid = CurrentUserID; 
        InitDataGridColumn();
    }

    #region 初始化页面信息
    private void InitPage()
    {
        string operate = "null";
        this.btnOk.Enabled = false;
        if (Request["Operate"] != null)
        {
            operate = Request["Operate"];
        }
        if (operate == "EDIT")
        {
            InitEdit();
        }
        else if (operate == "ADD")
        {
            this.Title = "添加记者";
            this.btnOk.Text = "保存";
            this.labHeading.Text = "添加记者";
            this.btnOk.Enabled = true;
            this.uploadimage.Visible = false;
            //UserControl uc = (UserControl)this.LoadControl("skins/Experience.ascx");
            //uc.ID = "uc" + ++count;
            //panelExperience.Controls.Add(uc);
            //ht.Add(uc.ID, uc);
        }
        else if (operate == "DEL")
        {
            int mid = Convert.ToInt32(Request["Mid"]);
            string errmeg;
            int ret = ESP.Media.BusinessLogic.ReportersManager.Delete(GetObject(), out errmeg);
            if (ret > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ReporterList.aspx?Mid=" + Request["Mid"] + "';alert('删除成功！');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ReporterList.aspx';alert('{0}');", errmeg), true);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string backUrl = "ReporterList.aspx";
        if (!string.IsNullOrEmpty(Request["backurl"]))
            backUrl = Request["backurl"];
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

        if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
        {

            backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
        }

        Response.Redirect(string.Format(backUrl + "?{0}", param));
    }

    private void InitEdit()
    {
        this.Title = "编辑记者";
        this.btnOk.Text = "保存";
        this.labHeading.Text = "编辑记者";
        if (Request["Rid"] != null)
        {
            int id = Convert.ToInt32(Request["Rid"]);
            ReportersInfo mlReporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(id);
            if (mlReporter != null)
            {
                MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(mlReporter.Media_id);
                if (media != null)
                {
                    txtMediaName.Text = media.Mediacname + " " + media.Channelname + " " + media.Topicname;
                    hidMedia.Value = media.Mediaitemid.ToString();
                }

                //基本信息
                txtName.Text = mlReporter.Reportername.Trim();//姓名
                txtPenName.Text = mlReporter.Penname.Trim();//笔名
                ddlSex.SelectedValue = mlReporter.Sex.ToString();
                dpBirthday.Text = mlReporter.Birthday.Split(' ')[0];//生日 
                if (dpBirthday.Text.Equals("1900-1-1"))
                {
                    dpBirthday.Text = "";
                }
                txtPostCord.Text = mlReporter.Postcode_h.Trim();//家庭邮编 
                txtIdCard.Text = mlReporter.Cardnumber;//身份证号 
                txtAddress.Text = mlReporter.Address_h;//住址
                txtReporterPosition.Text = mlReporter.Reporterposition;
                //联系信息
                txtOfficePhone.Text = mlReporter.Tel_o.Trim();//办公电话 
                txtHomePhone.Text = mlReporter.Tel_h.Trim();//家庭电话 
                txtUsualMobile.Text = mlReporter.Usualmobile.Trim();//常用手机 
                txtBackupMobile.Text = mlReporter.Backupmobile.Trim();//备用手机 
                txtFax.Text = mlReporter.Fax.Trim();//传真 
                txtQq.Text = mlReporter.Qq.Trim();//QQ 
                txtOtherMessageSoftware.Text = mlReporter.Othermessagesoftware.Trim();
                txtMsn.Text = mlReporter.Msn.Trim();//MSN 
                txtEmailOne.Text = mlReporter.Emailone.Trim();//E-mail1
                txtEmailTwo.Text = mlReporter.Emailtwo.Trim();//E-mail2
                //garry zhang add new attributes at 11-3
                this.txtOfficeAddress.Text = mlReporter.OfficeAddress;
                this.txtOfficePostID.Text = mlReporter.OfficePostID;
                //个人信息
                txtHometown.Text = mlReporter.Hometown.Trim();//籍贯
                txtHobby.Text = mlReporter.Hobby.Trim();//兴趣爱好
                txtCharacter.Text = mlReporter.Character.Trim();//性格特点
                ddlMarriage.SelectedValue = mlReporter.Marriage.ToString();//婚姻状况
                txtFamily.Text = mlReporter.Family.Trim();//家庭成员
                txtWriting.Text = mlReporter.Writing.Trim();//主要作品
                //教育信息
                txtEducation.Text = mlReporter.Education.Trim();//教育背景
                //照片

                this.uploadimage.Visible = true;
                uploadimage.ImageUrl = mlReporter.Photo;//照片

                //负责领域
                txtresponsibledomain.Text = mlReporter.Responsibledomain;

                //工作信息
                //Media_skins_Experience.InitExperienceTable();
                //DataTable dt = Media_skins_Experience.ExperienceTable;
                //System.IO.StringReader sr = new System.IO.StringReader(mlReporter.Experience);
                //dt.ReadXml(sr);
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    Media_skins_Experience uc = (Media_skins_Experience)this.LoadControl("skins/Experience.ascx");
                //    uc.ID = "uc" + ++count;
                //    ht.Add(uc.ID, uc);
                //    uc.SetProperties(i);
                //}
                txtExperience.Text = mlReporter.Experience;
                this.btnOk.Enabled = true;
            }
        }
    }
    #endregion

    #region 获得对象
    private ReportersInfo GetObject()
    {
        ReportersInfo mlReporter = null;
        if (Request["Rid"] != null)
        {
            mlReporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(Convert.ToInt32(Request["Rid"]));
        }
        else
        {
            mlReporter = new ReportersInfo();
        }
        if (Request["Mid"] != null)
        {
            mlReporter.Media_id = Convert.ToInt32(Request["Mid"]);
        }
        //基本信息
        mlReporter.Reportername = txtName.Text.Trim();//姓名
        mlReporter.Penname = txtPenName.Text.Trim();//笔名
        mlReporter.Sex = Convert.ToInt32(ddlSex.SelectedValue);
        mlReporter.Birthday = dpBirthday.Text.Trim();//生日 
        mlReporter.Postcode_h = txtPostCord.Text.Trim();//家庭邮编 
        mlReporter.Cardnumber = txtIdCard.Text.Trim();//身份证号 
        mlReporter.Address_h = txtAddress.Text.Trim();//住址
        mlReporter.Reporterposition = txtReporterPosition.Text.Trim();//职务
        //联系信息
        mlReporter.Tel_o = txtOfficePhone.Text.Trim();//办公电话 
        mlReporter.Tel_h = txtHomePhone.Text.Trim();//家庭电话 
        mlReporter.Usualmobile = txtUsualMobile.Text.Trim();//常用手机 
        mlReporter.Backupmobile = txtBackupMobile.Text.Trim();//备用手机 
        mlReporter.Fax = txtFax.Text.Trim();//传真 
        mlReporter.Qq = txtQq.Text.Trim();//QQ 
        mlReporter.Othermessagesoftware = txtOtherMessageSoftware.Text.Trim();
        mlReporter.Msn = txtMsn.Text.Trim();//MSN 
        mlReporter.Emailone = txtEmailOne.Text.Trim();//E-mail1
        mlReporter.Emailtwo = txtEmailTwo.Text.Trim();//E-mail2
        mlReporter.OfficePostID = this.txtOfficePostID.Text;
        mlReporter.OfficeAddress = this.txtOfficeAddress.Text;
        //个人信息
        mlReporter.Hometown = txtHometown.Text;//籍贯
        mlReporter.Hobby = txtHobby.Text.Trim();//兴趣爱好
        mlReporter.Character = txtCharacter.Text.Trim();//性格特点
        mlReporter.Marriage = Convert.ToInt32(ddlMarriage.SelectedValue);//婚姻状况

        if (txtFamily.Text.Trim().Length > 300)
            mlReporter.Family = txtFamily.Text.Trim().Substring(0, 300);//家庭成员
        else
            mlReporter.Family = txtFamily.Text.Trim();

        if(txtWriting.Text.Trim().Length > 800)
            mlReporter.Writing = txtWriting.Text.Trim().Substring(0, 800);//主要作品
        else
            mlReporter.Writing = txtWriting.Text.Trim();

        if (txtEducation.Text.Trim().Length > 800)
            mlReporter.Education = txtEducation.Text.Trim().Substring(0, 800);//教育背景
        else
            mlReporter.Education = txtEducation.Text.Trim();

        if (txtExperience.Text.Trim().Length > 800)
            mlReporter.Experience = txtExperience.Text;//职业经历
        else
            mlReporter.Experience = txtExperience.Text;
        //照片
        if (this.uploadimage.ImageUrl != null && this.uploadimage.ImageUrl.Length > 0)
        {
            mlReporter.Photo = this.uploadimage.ImageUrl;//照片
        }
        
        mlReporter.Createddate = mlReporter.Createddate == string.Empty ? DateTime.Now.ToString() : mlReporter.Createddate;
        mlReporter.Createdbyuserid = int.Parse(CurrentUser.SysID);
        mlReporter.Createdip = mlReporter.Createdip == string.Empty ? Request.UserHostAddress : mlReporter.Createdip;
        mlReporter.Lastmodifieddate = DateTime.Now.ToString();
        mlReporter.Lastmodifiedbyuserid = int.Parse(CurrentUser.SysID);
        mlReporter.Lastmodifiedip = Request.UserHostAddress;

        //负责领域
        mlReporter.Responsibledomain = txtresponsibledomain.Text.Trim();

        return mlReporter;
    }
    #endregion
    
    int currentPrivateId = 0;
    int relationId = 0;
    int Pjid = 0;
    private void ListBind()
    {
        relationId = int.Parse(Request["relationId"]);
        Pjid = int.Parse(Request[RequestName.ProjectID]);
        ESP.Media.Entity.ProjectreporterrelationInfo relation = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(int.Parse(Request["relationId"]));
        if (relation != null)
        {
            ReportersInfo reporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(relation.Reporterid);

            int ReportId = reporter == null ? 0 : reporter.Reporterid;
            currentPrivateId = relation.Privateinfoid;
            DataTable dt = ESP.Media.BusinessLogic.PrivateinfoManager.getListByReportId(ReportId);
            if (dt == null)
                dt = new DataTable();
            dgList.DataSource = dt.DefaultView;
        }
    }

    private void InitDataGridColumn()
    {
        string strColumn = "id#id#bankname#bankcardname#bankacountname#bankcardcode#id";
        string strHeader = "选择#私密信息状态#开户银行#银行卡姓名#开户姓名#账号#编辑";
        string strH = "center#center#####center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, null, strH, this.dgList);
    }

    protected void dgList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "relationId", relationId);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, RequestName.ProjectID, Pjid);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "PrivateId", dgList.DataKeys[e.Row.RowIndex].Value);

            e.Row.Cells[0].Text = "<input type='radio' id='rad' name='rad' value='" + e.Row.Cells[0].Text + "'/>";
            e.Row.Cells[1].Text = e.Row.Cells[1].Text == currentPrivateId.ToString() ? "正在使用" : "";
            e.Row.Cells[6].Text = string.Format("<a href='..\\Project\\ReporterPrivacy.aspx?{0}'><img src='{1}'></a>", param, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);
        }
    }

    private void SavePrivacy()
    {
        string errmeg = string.Empty;
        if (string.IsNullOrEmpty(Request["rad"]))
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择记者私密信息！');</script>");
            return;
        }
        ESP.Media.Entity.ProjectreporterrelationInfo relation = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(int.Parse(Request["relationId"]));
        relation.Privateinfoid = int.Parse(Request["rad"]);
        ESP.Media.BusinessLogic.ProjectsManager.SetReporterProviteMsg(relation, int.Parse(CurrentUser.SysID), out errmeg);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Privateinfo model = GetPrivateObject();
        if (!string.IsNullOrEmpty(Request["Operate"]))
        {
            //if (Request["Operate"] == "EDIT")
            //{
            //    ESP.Media.BusinessLogic.PrivateinfoManager.modify(model, int.Parse(CurrentUser.SysID));
            //}
            //else if (Request["Operate"] == "ADD")
            //{
                ESP.Media.BusinessLogic.PrivateinfoManager.add(model, int.Parse(CurrentUser.SysID));
                this.txtBankName.Text = string.Empty;
                this.txtbankacountname.Text = string.Empty;
                this.txtBankcardCode.Text = string.Empty;
                this.txtWritingfee.Text = string.Empty;
                this.txtcooperatecircs.Text = string.Empty;
                this.txtPrivateRemark.Text = string.Empty;
                this.ddlpaymentmode.SelectedIndex = 0;
                this.ddlHaveInvoice.SelectedIndex = 0;
            //}
        }
        ListBind();
    }

    private Privateinfo GetPrivateObject()
    {
        //int id = Convert.ToInt32(Request["relationId"]);
        //ESP.Media.Entity.ProjectreporterrelationInfo relation = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(id);
        Privateinfo model = new Privateinfo();
        model.Bankcardcode = txtBankcardCode.Text;
        //model.Bankcardname = txtBankCardName.Text;
        model.Bankname = txtBankName.Text;
        model.Haveinvoice = int.Parse(ddlHaveInvoice.SelectedValue);
        if (txtWritingfee.Text.Trim() != string.Empty)
            model.Writingfee = double.Parse(txtWritingfee.Text.Trim());
        model.Paytype = int.Parse(ddlpaymentmode.SelectedValue);
        //model.Referral = txtReferral.Text.Trim();
        model.Bankacountname = txtbankacountname.Text.Trim();

        if (txtPrivateRemark.Text.Trim().Length > 300)
            model.Privateremark = txtPrivateRemark.Text.Trim().Substring(0,300);
        else
            model.Privateremark = txtPrivateRemark.Text.Trim();

        if (txtcooperatecircs.Text.Trim().Length > 300)
            model.Cooperatecircs = txtcooperatecircs.Text.Trim().Substring(0,300);
        else
            model.Cooperatecircs = txtcooperatecircs.Text.Trim();

        model.Reporterid = int.Parse(Request["Rid"]);
        return model;
    }
    #region 确认
    protected void btnOk_Click(object sender, EventArgs e)
    {
        int ret = 0;
        int mid;
        string errmeg;
        //byte[] imgdata = null;
        string imgname = string.Empty;
        if (ImageUpload.HasFile)
        {
            //imgdata = ImageUpload.FileBytes;
            //imgname =  Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.ReporterLogoPath + ImageUpload.FileName);
            HttpPostedFile myFile = ImageUpload.PostedFile;

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
                    imgname = ImageHelper.SavePhoto(myFile.InputStream, Config.PhotoSizeSettings.LARGESIZE, myFile.ContentLength, CurrentUser.SysID, ESP.Configuration.ConfigurationManager.SafeAppSettings["ProductLineLogoPath"]).filename;
                }
            }
        }
        if (Request["Mid"] != null)
        {
            mid = Convert.ToInt32(Request["Mid"]);
        }
        //else
        //{
        //    return;
        //}
        if (string.IsNullOrEmpty(Request["Rid"]))//插入
        {
            ReportersInfo report = GetObject();
            report.Media_id = int.Parse(hidMedia.Value == "" ? "0" : hidMedia.Value);
            if (report == null) return;
            if (Request[RequestName.ProjectID] != null && Request[RequestName.ProjectID].Length > 0)
            {
                ret = ESP.Media.BusinessLogic.ReportersManager.AddWithProject(report, imgname, Convert.ToInt32(Request[RequestName.ProjectID]), CurrentUserID, out errmeg);
            }
            else
            {
                ret = ESP.Media.BusinessLogic.ReportersManager.Add(report, imgname,  CurrentUserID, out errmeg);
            }
            if (ret > 0)
            {

                string backUrl = "ReporterDisplay.aspx";
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
                {

                    backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request["backurl"]))
                        backUrl = Request["backurl"];
                }
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "Rid", ret);
                if (!string.IsNullOrEmpty(Request["alert"]))
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());
                
                if (string.IsNullOrEmpty(Request["alert"]))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='" + string.Format(backUrl + "?{0}", param) + "';alert('保存成功！');", true);
                }
                else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.opener.location.reload();alert('保存成功！');window.close();", ret), true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
            }

        }
        else//修改
        {
            ret = ESP.Media.BusinessLogic.ReportersManager.Update(GetObject(), imgname,  CurrentUserID,out errmeg);
            if (ret > 0)
            {
                this.SavePrivacy();

                string backUrl = "ReporterDisplay.aspx";
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
                {

                    backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request["backurl"]))
                        backUrl = Request["backurl"];
                }
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "Rid", Request["Rid"]);
                if (!string.IsNullOrEmpty(Request["alert"]))
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());

                if (string.IsNullOrEmpty(Request["alert"]))
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='" + string.Format(backUrl + "?{0}", param) + "';alert('保存成功！');", true);
                else
                {
                    //string script = string.Format(
                    //    @"window.opener.location.replace(''''+window.opener.location+'&TabIndex='+" +
                    //    (int)ESP.Media.Access.Utilities.Global.ProjectTabs.MediaAndReporter + "+'''');alert('保存成功！');window.close();", ret);
                    if (!string.IsNullOrEmpty(Request[RequestName.ProjectID]))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                                                                    "ReloadOpener(" +
                                                                    (int)ESP.Media.Access.Utilities.Global.ProjectTabs.MediaAndReporter +
                                                                    ", " + Request[RequestName.ProjectID] + ");", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "opener.location.reload();alert('保存成功');window.close();", true);
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
            }
        }
    }
    #endregion
}