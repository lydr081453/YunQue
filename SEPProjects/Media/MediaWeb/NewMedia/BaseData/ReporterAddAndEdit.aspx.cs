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
using ESP.MediaLinq.Utilities;
using System.Collections.Generic;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class ReporterAddAndEdit : ESP.Web.UI.PageBase
    {
        int Mid = 0;
        int Rid = 0;
        int Aid = 0;
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
                    media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Mid);
                    txtMediaName.Text = media.MediaCName + " " + media.ChannelName + " " + media.TopicName;
                    hidMedia.Value = media.MediaitemID.ToString();

                    media_AgencyInfo agency = ESP.MediaLinq.BusinessLogic.AgencyManager.GetModelByMediaID(Mid);
                    if (agency != null)
                    {
                        txtAgencyName.Text = agency.AgencyCName + " " + agency.TopicName;
                        hidAgency.Value = agency.AgencyID.ToString();
                    }
                    else
                    {
                        btnAgencyLink.Text = "关联所属机构";
                    }
                }
                else
                {
                    btnLink.Text = "关联所属媒体";
                    btnAgencyLink.Text = "关联所属机构";
                }
            }
            if (string.IsNullOrEmpty(Request["Rid"]))
            {
                btnLink.Attributes["onclick"] = "javascript:window.open('ReporterSelectMediaList.aspx?alert=1&Rid=" + Rid + "&Mid=" + Mid + "&Operate=New','关联所属媒体','" + ESP.MediaLinq.Utilities.Global.OpenClass.Common + "');return false;";
                // btnAgencyLink.Attributes["onclick"] = "javascript:window.open('ReporterSelectAgencyList.aspx?alert=1&Rid=" + Rid + "&Mid=" + Mid + "&Operate=New','关联所属机构','" + ESP.MediaLinq.Utilities.Global.OpenClass.Common + "');return false;";
                btnAgencyLink.Attributes["onclick"] = "return openSelectAgency()";
                hidRid.Value = "0";
            }
           
            if (!string.IsNullOrEmpty(Request["Operate"]) && Request["Operate"] == "ADD" && !string.IsNullOrEmpty(Request["alert"]))
            {
                btnLink.Visible = false;
                btnAgencyLink.Visible = false;
            }
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = Global.AddParam(param, "Rid", Request["Rid"]);
            param = Global.AddParam(param, "Mid", Mid.ToString());
            param = Global.AddParam(param, "Operate", "MediaSelect");
            param = Global.AddParam(param, "backurl", "ReporterAddAndEdit.aspx");
            string url = string.Format(@"ReporterSelectMediaList.aspx?{0}", param);
            Response.Redirect(url);
        }

        protected void btnAgencyLink_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = Global.AddParam(param, "Rid", Request["Rid"]);
            param = Global.AddParam(param, "Mid", hidMedia.Value);
            param = Global.AddParam(param, "Operate", "AgencySelect");
            param = Global.AddParam(param, "backurl", "ReporterAddAndEdit.aspx");
            string url = string.Format(@"ReporterSelectAgencyList.aspx?{0}", param);
            Response.Redirect(url);
        }


        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            int userid = UserInfo.UserID;
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
                this.btnAgencyLink.Enabled = false;
                //UserControl uc = (UserControl)this.LoadControl("skins/Experience.ascx");
                //uc.ID = "uc" + ++count;
                //panelExperience.Controls.Add(uc);
                //ht.Add(uc.ID, uc);
            }
            else if (operate == "DEL")
            {
                int mid = Convert.ToInt32(Request["Mid"]);
                string errmeg;
                int ret = ESP.MediaLinq.BusinessLogic.ReporterManager.Delete(GetObject(), out errmeg);
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
                media_ReportersInfo mlReporter = ESP.MediaLinq.BusinessLogic.ReporterManager.GetModel(id);
                if (mlReporter != null)
                {
                   

                    //基本信息
                    txtName.Text = mlReporter.ReporterName;//姓名
                    txtPenName.Text = mlReporter.PenName;//笔名
                    ddlSex.SelectedValue = mlReporter.Sex == null ? "1" : mlReporter.Sex.ToString();
                    dpBirthday.Text = mlReporter.Birthday == null ? "" : mlReporter.Birthday.ToString().Split(' ')[0];//生日 
                    if (dpBirthday.Text.Equals("1900-1-1"))
                    {
                        dpBirthday.Text = "";
                    }
                    txtPostCord.Text = mlReporter.Postcode_H;//家庭邮编 
                    txtIdCard.Text = mlReporter.CardNumber;//身份证号 
                    txtAddress.Text = mlReporter.Address_H;//住址
                    txtReporterPosition.Text = mlReporter.ReporterPosition;
                    //联系信息
                    txtOfficePhone.Text = mlReporter.Tel_O;//办公电话 
                    txtHomePhone.Text = mlReporter.Tel_H;//家庭电话 
                    txtUsualMobile.Text = mlReporter.UsualMobile;//常用手机 
                    txtBackupMobile.Text = mlReporter.BackupMobile;//备用手机 
                    txtFax.Text = mlReporter.Fax;//传真 
                    txtQq.Text = mlReporter.QQ;//QQ 
                    txtOtherMessageSoftware.Text = mlReporter.OtherMessageSoftware;
                    txtMsn.Text = mlReporter.MSN;//MSN 
                    txtEmailOne.Text = mlReporter.EmailOne;//E-mail1
                    txtEmailTwo.Text = mlReporter.EmailTwo;//E-mail2
                    //garry zhang add new attributes at 11-3
                    this.txtOfficeAddress.Text = mlReporter.OfficeAddress;
                    this.txtOfficePostID.Text = mlReporter.OfficePostID;
                    //个人信息
                    txtHometown.Text = mlReporter.hometown;//籍贯
                    txtHobby.Text = mlReporter.Hobby;//兴趣爱好
                    txtCharacter.Text = mlReporter.Character;//性格特点
                    ddlMarriage.SelectedValue = mlReporter.Marriage ==null ? "0" : mlReporter.Marriage.ToString();//婚姻状况
                    txtFamily.Text = mlReporter.Family;//家庭成员
                    txtWriting.Text = mlReporter.Writing;//主要作品
                    //教育信息
                    txtEducation.Text = mlReporter.Education;//教育背景
                    //照片

                    this.uploadimage.Visible = true;
                    uploadimage.ImageUrl = mlReporter.Photo;//照片

                    //负责领域
                    txtresponsibledomain.Text = mlReporter.ResponsibleDomain;
                                        
                    txtExperience.Text = mlReporter.Experience;
                    this.btnOk.Enabled = true;

                    //媒体和机构                 
                    if (mlReporter.AgencyID != null)
                    {
                        media_AgencyInfo agency = ESP.MediaLinq.BusinessLogic.AgencyManager.GetModel((int)mlReporter.AgencyID);
                        if (agency != null)
                        {
                            txtAgencyName.Text = agency.AgencyCName + "" + agency.AgencyEName;
                            hidAgency.Value = agency.AgencyID.ToString();
                        }
                    }
                    
                    if(mlReporter.Media_ID != null)
                    {
                        media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel((int)mlReporter.Media_ID);
                        if (media != null)
                        {
                            txtMediaName.Text = media.MediaCName + " " + media.ChannelName + " " + media.TopicName;
                            hidMedia.Value = media.MediaitemID.ToString();
                        }
                    }
                           
                   
                }
            }
        }
        #endregion

        #region 获得对象
        private media_ReportersInfo GetObject()
        {
            media_ReportersInfo mlReporter = null;
            if (Request["Rid"] != null)
            {
                mlReporter = ESP.MediaLinq.BusinessLogic.ReporterManager.GetModel(Convert.ToInt32(Request["Rid"]));
            }
            else
            {
                mlReporter = new media_ReportersInfo();
            }
            //if (Request["Mid"] != null)
            //{
            //    mlReporter.Media_ID = Convert.ToInt32(Request["Mid"]);
            //}
            
            mlReporter.Media_ID = Convert.ToInt32(hidMedia.Value);
            mlReporter.AgencyID = Convert.ToInt32(hidAgency.Value);
            //基本信息
            mlReporter.ReporterName = txtName.Text.Trim();//姓名
            mlReporter.PenName = txtPenName.Text.Trim();//笔名
            mlReporter.Sex = short.Parse(ddlSex.SelectedValue);
            try
            {
                mlReporter.Birthday = DateTime.Parse(dpBirthday.Text.Trim());//生日 
            }
            catch { }
            mlReporter.Postcode_H = txtPostCord.Text.Trim();//家庭邮编 
            mlReporter.CardNumber = txtIdCard.Text.Trim();//身份证号 
            mlReporter.Address_H = txtAddress.Text.Trim();//住址
            mlReporter.ReporterPosition = txtReporterPosition.Text.Trim();//职务
            //联系信息
            mlReporter.Tel_O = txtOfficePhone.Text.Trim();//办公电话 
            mlReporter.Tel_H = txtHomePhone.Text.Trim();//家庭电话 
            mlReporter.UsualMobile = txtUsualMobile.Text.Trim();//常用手机 
            mlReporter.BackupMobile = txtBackupMobile.Text.Trim();//备用手机 
            mlReporter.Fax = txtFax.Text.Trim();//传真 
            mlReporter.QQ = txtQq.Text.Trim();//QQ 
            mlReporter.OtherMessageSoftware = txtOtherMessageSoftware.Text.Trim();
            mlReporter.MSN = txtMsn.Text.Trim();//MSN 
            mlReporter.EmailOne = txtEmailOne.Text.Trim();//E-mail1
            mlReporter.EmailTwo = txtEmailTwo.Text.Trim();//E-mail2
            mlReporter.OfficePostID = this.txtOfficePostID.Text;
            mlReporter.OfficeAddress = this.txtOfficeAddress.Text;
            //个人信息
            mlReporter.hometown = txtHometown.Text;//籍贯
            mlReporter.Hobby = txtHobby.Text.Trim();//兴趣爱好
            mlReporter.Character = txtCharacter.Text.Trim();//性格特点
            mlReporter.Marriage = short.Parse(ddlMarriage.SelectedValue);//婚姻状况

            if (txtFamily.Text.Trim().Length > 300)
                mlReporter.Family = txtFamily.Text.Trim().Substring(0, 300);//家庭成员
            else
                mlReporter.Family = txtFamily.Text.Trim();

            if (txtWriting.Text.Trim().Length > 800)
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

            if (Request["Rid"] == null)            
            { 
                mlReporter.CreatedDate = DateTime.Now; 
            }
            mlReporter.CreatedByUserID = UserInfo.UserID;
            mlReporter.CreatedIP = string.IsNullOrEmpty(mlReporter.CreatedIP)? Request.UserHostAddress : mlReporter.CreatedIP;
            mlReporter.LastModifiedDate = DateTime.Now;
            mlReporter.LastModifiedByUserID = UserInfo.UserID;
            mlReporter.LastModifiedIP = Request.UserHostAddress;

            //负责领域
            mlReporter.ResponsibleDomain = txtresponsibledomain.Text.Trim();


            //SN
            mlReporter.SN = "";
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
            // ESP.Media.Entity.ProjectreporterrelationInfo relation = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(int.Parse(Request["relationId"]));
            // if (relation != null)
            if (!string.IsNullOrEmpty(Request["Rid"]))
            {
                media_ReportersInfo reporter = ESP.MediaLinq.BusinessLogic.ReporterManager.GetModel(int.Parse(Request["Rid"]));

                int ReportId = reporter == null ? 0 : reporter.ReporterID;
                //currentPrivateId = relation.Privateinfoid;
                DataTable dt = ESP.MediaLinq.BusinessLogic.PrivateManager.getListByReportId(ReportId);
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
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, "relationId", relationId);
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.ProjectID, Pjid);
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, "PrivateId", dgList.DataKeys[e.Row.RowIndex].Value);

                e.Row.Cells[0].Text = "<input type='radio' id='rad' name='rad' value='" + e.Row.Cells[0].Text + "'/>";
                e.Row.Cells[1].Text = e.Row.Cells[1].Text == currentPrivateId.ToString() ? "正在使用" : "";
                e.Row.Cells[6].Text = string.Format("<a href='..\\Project\\ReporterPrivacy.aspx?{0}'><img src='{1}'></a>", param, ESP.MediaLinq.Utilities.ConfigManager.EditIconPath);
            }
        }
        /////
        //private void SavePrivacy()
        //{
        //    string errmeg = string.Empty;
        //    if (string.IsNullOrEmpty(Request["rad"]))
        //    {
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择记者私密信息！');</script>");
        //        return;
        //    }
        //    ESP.Media.Entity.ProjectreporterrelationInfo relation = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(int.Parse(Request["relationId"]));
        //    relation.Privateinfoid = int.Parse(Request["rad"]);
        //    ESP.Media.BusinessLogic.ProjectsManager.SetReporterProviteMsg(relation, int.Parse(CurrentUser.SysID), out errmeg);
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            media_PrivateInfo model = GetPrivateObject();
            if (!string.IsNullOrEmpty(Request["Operate"]))
            {
                //if (Request["Operate"] == "EDIT")
                //{
                //    ESP.Media.BusinessLogic.PrivateinfoManager.modify(model, int.Parse(CurrentUser.SysID));
                //}
                //else if (Request["Operate"] == "ADD")
                //{
                
                ESP.MediaLinq.BusinessLogic.PrivateManager.Add(model);
                this.txtBankName.Text = string.Empty;
                this.txtbankacountname.Text = string.Empty;
                this.txtBankcardCode.Text = string.Empty;
                this.txtTel.Text = string.Empty;
                this.txtMobile.Text = string.Empty;
                this.txtPrivateRemark.Text = string.Empty;                
                //}
            }
            ListBind();
        }

        private media_PrivateInfo GetPrivateObject()
        {
            //int id = Convert.ToInt32(Request["relationId"]);
            //ESP.Media.Entity.ProjectreporterrelationInfo relation = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(id);
            media_PrivateInfo model = new media_PrivateInfo();
            model.bankcardcode = txtBankcardCode.Text;
            //model.Bankcardname = txtBankCardName.Text;
            model.bankname = txtBankName.Text; 
            model.Tel = txtTel.Text.Trim();
            model.Mobile = txtMobile.Text.Trim();
            model.bankacountname = txtbankacountname.Text.Trim();

            if (txtPrivateRemark.Text.Trim().Length > 300)
                model.PrivateRemark = txtPrivateRemark.Text.Trim().Substring(0, 300);
            else
                model.PrivateRemark = txtPrivateRemark.Text.Trim();
            model.ReporterID = int.Parse(Request["Rid"]);
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
                media_ReportersInfo report = GetObject();
                report.Media_ID = int.Parse(hidMedia.Value == "" ? "0" : hidMedia.Value);
                if (report == null) return;
                /////有疑问
                //if (Request[RequestName.ProjectID] != null && Request[RequestName.ProjectID].Length > 0)
                //{
                //    ret = ESP.Media.BusinessLogic.ReportersManager.AddWithProject(report, imgname, Convert.ToInt32(Request[RequestName.ProjectID]), CurrentUserID, out errmeg);
                //}
                //else
                //{
                    ret = ESP.MediaLinq.BusinessLogic.ReporterManager.Add(report, imgname, UserInfo.UserID, out errmeg);
                //}
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
                    param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Rid", ret);
                    if (!string.IsNullOrEmpty(Request["alert"]))
                        param = ESP.MediaLinq.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());

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
                ret = ESP.MediaLinq.BusinessLogic.ReporterManager.Update(GetObject(), imgname, CurrentUserID, out errmeg);
                if (ret > 0)
                {
                  //  this.SavePrivacy();

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
                    param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Rid", Request["Rid"]);
                    if (!string.IsNullOrEmpty(Request["alert"]))
                        param = ESP.MediaLinq.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());

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
                                                                        (int)ESP.MediaLinq.Utilities.Global.ProjectTabs.MediaAndReporter +
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
}
