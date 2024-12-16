using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.Entity;
using ESP.Compatible;
using System.Data;
//using Web.Components;
//using ESP.Media.Access.Utilities;

public partial class UserControl_ReporterControl_ReporterEdit : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPage(ReportersInfo reporter)
    {
        if (reporter != null)
        {
            hidReporterID.Value = reporter.Reporterid.ToString();
            MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(reporter.Media_id);
            if (media != null)
            {
                txtMediaName.Text = media.Mediacname + " " + media.Channelname + " " + media.Topicname;
                hidMedia.Value = media.Mediaitemid.ToString();
            }

            //基本信息
            txtName.Text = reporter.Reportername.Trim();//姓名
            txtPenName.Text = reporter.Penname.Trim();//笔名
            ddlSex.SelectedValue = reporter.Sex.ToString();
            dpBirthday.Text = reporter.Birthday.Split(' ')[0];//生日 
            if (dpBirthday.Text.Equals("1900-1-1"))
            {
                dpBirthday.Text = "";
            }
            txtPostCord.Text = reporter.Postcode_h.Trim();//家庭邮编 
            txtIdCard.Text = reporter.Cardnumber;//身份证号 
            txtAddress.Text = reporter.Address_h;//住址
            txtReporterPosition.Text = reporter.Reporterposition;
            //联系信息
            txtOfficePhone.Text = reporter.Tel_o.Trim();//办公电话 
            txtHomePhone.Text = reporter.Tel_h.Trim();//家庭电话 
            txtUsualMobile.Text = reporter.Usualmobile.Trim();//常用手机 
            txtBackupMobile.Text = reporter.Backupmobile.Trim();//备用手机 
            txtFax.Text = reporter.Fax.Trim();//传真 
            txtQq.Text = reporter.Qq.Trim();//QQ 
            txtOtherMessageSoftware.Text = reporter.Othermessagesoftware.Trim();
            txtMsn.Text = reporter.Msn.Trim();//MSN 
            txtEmailOne.Text = reporter.Emailone.Trim();//E-mail1
            txtEmailTwo.Text = reporter.Emailtwo.Trim();//E-mail2s
            txtEmailThree.Text = reporter.Emailthree.Trim();//E-mail3
            //个人信息
            txtHometown.Text = reporter.Hometown.Trim();//籍贯
            txtHobby.Text = reporter.Hobby.Trim();//兴趣爱好
            txtCharacter.Text = reporter.Character.Trim();//性格特点
            ddlMarriage.SelectedValue = reporter.Marriage.ToString();//婚姻状况
            txtFamily.Text = reporter.Family.Trim();//家庭成员
            txtWriting.Text = reporter.Writing.Trim();//主要作品
            //教育信息
            txtEducation.Text = reporter.Education.Trim();//教育背景
            //照片

            //this.uploadimage.Visible = true;
            //uploadimage.ImageUrl = reporter.Photo;//照片

            //负责领域
            txtresponsibledomain.Text = reporter.Responsibledomain;

            //工作信息
            //Media_skins_Experience.InitExperienceTable();
            //DataTable dt = Media_skins_Experience.ExperienceTable;
            //System.IO.StringReader sr = new System.IO.StringReader(reporter.Experience);
            //dt.ReadXml(sr);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Media_skins_Experience uc = (Media_skins_Experience)this.LoadControl("skins/Experience.ascx");
            //    uc.ID = "uc" + ++count;
            //    ht.Add(uc.ID, uc);
            //    uc.SetProperties(i);
            //}
            txtExperience.Text = reporter.Experience;
        }
    }

    public HttpPostedFile GetFileName()
    {
        string imgname = string.Empty;
        if (ImageUpload.HasFile)
        {
            //imgdata = ImageUpload.FileBytes;
            //imgname =  Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.ReporterLogoPath + ImageUpload.FileName);
            HttpPostedFile myFile = ImageUpload.PostedFile;
            return myFile;
        }
        else
            return null;
    }

    public ReportersInfo GetObject()
    {
        ReportersInfo reporter = null;
        if (hidReporterID.Value != string.Empty)
        {
            reporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(Convert.ToInt32(hidReporterID.Value));
        }
        else
        {
            reporter = new ReportersInfo();
        }
        reporter.Media_id = Convert.ToInt32(hidMedia.Value);

        //基本信息
        reporter.Reportername = txtName.Text.Trim();//姓名
        reporter.Penname = txtPenName.Text.Trim();//笔名
        reporter.Sex = Convert.ToInt32(ddlSex.SelectedValue);
        reporter.Birthday = dpBirthday.Text.Trim();//生日 
        reporter.Postcode_h = txtPostCord.Text.Trim();//家庭邮编 
        reporter.Cardnumber = txtIdCard.Text.Trim();//身份证号 
        reporter.Address_h = txtAddress.Text.Trim();//住址
        reporter.Reporterposition = txtReporterPosition.Text.Trim();//职务
        //联系信息
        reporter.Tel_o = txtOfficePhone.Text.Trim();//办公电话 
        reporter.Tel_h = txtHomePhone.Text.Trim();//家庭电话 
        reporter.Usualmobile = txtUsualMobile.Text.Trim();//常用手机 
        reporter.Backupmobile = txtBackupMobile.Text.Trim();//备用手机 
        reporter.Fax = txtFax.Text.Trim();//传真 
        reporter.Qq = txtQq.Text.Trim();//QQ 
        reporter.Othermessagesoftware = txtOtherMessageSoftware.Text.Trim();
        reporter.Msn = txtMsn.Text.Trim();//MSN 
        reporter.Emailone = txtEmailOne.Text.Trim();//E-mail1
        reporter.Emailtwo = txtEmailTwo.Text.Trim();//E-mail2
        reporter.Emailthree = txtEmailThree.Text.Trim();//E-mail3
        //个人信息
        reporter.Hometown = txtHometown.Text;//籍贯
        reporter.Hobby = txtHobby.Text.Trim();//兴趣爱好
        reporter.Character = txtCharacter.Text.Trim();//性格特点
        reporter.Marriage = Convert.ToInt32(ddlMarriage.SelectedValue);//婚姻状况

        if (txtFamily.Text.Trim().Length > 300)
            reporter.Family = txtFamily.Text.Trim().Substring(0, 300);//家庭成员
        else
            reporter.Family = txtFamily.Text.Trim();

        if (txtWriting.Text.Trim().Length > 800)
            reporter.Writing = txtWriting.Text.Trim().Substring(0, 800);//主要作品
        else
            reporter.Writing = txtWriting.Text.Trim();

        if (txtEducation.Text.Trim().Length > 800)
            reporter.Education = txtEducation.Text.Trim().Substring(0, 800);//教育背景
        else
            reporter.Education = txtEducation.Text.Trim();

        if (txtExperience.Text.Trim().Length > 800)
            reporter.Experience = txtExperience.Text;//职业经历
        else
            reporter.Experience = txtExperience.Text;
        //照片
        //if (this.uploadimage.ImageUrl != null && this.uploadimage.ImageUrl.Length > 0)
        //{
        //    reporter.Photo = this.uploadimage.ImageUrl;//照片
        //}

        //负责领域
        reporter.Responsibledomain = txtresponsibledomain.Text.Trim();

        return reporter;
    }
}