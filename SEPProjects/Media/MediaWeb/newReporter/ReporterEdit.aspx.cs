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

namespace MediaWeb.newReporter
{
    public partial class ReporterEdit : ESP.Web.UI.PageBase
    {
        int reporterId = 0;
        ReportersInfo mlReporter = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.ReporterID]))
                reporterId = int.Parse(Request[RequestName.ReporterID]);
            mlReporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(Convert.ToInt32(Request["Rid"]));
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private void getFullImageUrl(string imgurl)
        {
            if (string.IsNullOrEmpty(imgurl))
            {
                this.uploadimage.ImageUrl = "/images/head-e.jpg";
            }
            else
                this.uploadimage.ImageUrl = imgurl;
        }

        private void BindInfo()
        {
            if (mlReporter != null)
            {
                MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(mlReporter.Media_id);
                if (media != null)
                {
                    lnkMediaName.Text = media.Mediacname + " " + media.Channelname + " " + media.Topicname;
                    string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, RequestName.MediaID, mlReporter.Media_id.ToString());
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "visible", "false");
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, RequestName.Alert, "1");
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "enablededit", "0");
                    lnkMediaName.Attributes["onclick"] = string.Format("javascript:window.open('/Media/MediaDisplay.aspx?{0}','','{1}')", param, ESP.Media.Access.Utilities.Global.OpenClass.Common);
                }

                //基本信息
                getFullImageUrl(mlReporter.Photo);
                txtName.Text = mlReporter.Reportername.Trim();//姓名
                txtPenName.Text = mlReporter.Penname.Trim();//笔名
                ddlSex.SelectedValue = mlReporter.Sex.ToString();
                txtBirthday.Text = mlReporter.Birthday.Split(' ')[0];//生日 
                if (txtBirthday.Text.Equals("1900-1-1"))
                {
                    txtBirthday.Text = "";
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

                //负责领域
                txtresponsibledomain.Text = mlReporter.Responsibledomain;
                txtExperience.Text = mlReporter.Experience;
            }
        }

        private ReportersInfo GetObject()
        {
            //基本信息
            mlReporter.Reportername = txtName.Text.Trim();//姓名
            mlReporter.Penname = txtPenName.Text.Trim();//笔名
            mlReporter.Sex = Convert.ToInt32(ddlSex.SelectedValue);
            mlReporter.Birthday = txtBirthday.Text.Trim();//生日 
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporterInfo.aspx?"+RequestName.ReporterID+"="+reporterId);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ret = 0;
            string errmeg;
            string imgname = string.Empty;
            if (ImageUpload.HasFile)
            {
                HttpPostedFile myFile = ImageUpload.PostedFile;

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
                        imgname = ImageHelper.SavePhoto(myFile.InputStream, Config.PhotoSizeSettings.LARGESIZE, myFile.ContentLength, CurrentUser.SysID, ESP.Configuration.ConfigurationManager.SafeAppSettings["ProductLineLogoPath"]).filename;
                    }
                }
            }

            ret = ESP.Media.BusinessLogic.ReportersManager.Update(GetObject(), imgname, CurrentUserID, out errmeg);
            if (ret > 0)
            {
                Response.Redirect("ReporterInfo.aspx?" + RequestName.ReporterID + "=" + reporterId);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
            }
        }
    }

}

