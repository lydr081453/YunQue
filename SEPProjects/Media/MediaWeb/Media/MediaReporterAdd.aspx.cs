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

public partial class Media_MediaReporterAdd : ESP.Web.UI.PageBase
{
    int alertValue = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertValue = Convert.ToInt32(Request["alert"]);
        }
        if (!IsPostBack)
        {
            //count = 0;
            //ht = new Hashtable();
            string[] paymentModes = ESP.Media.Access.Utilities.Global.PaymentModeName;
            for (int i = 0; i < paymentModes.Length; i++)
            {
                ddlpaymentmode.Items.Add(new ListItem(paymentModes[i].ToString(), (i + 1).ToString()));
            }
            ddlpaymentmode.Items.Insert(0, new ListItem("请选择", "0"));
            InitPage();
        }
        //for (int i = 0; i < ht.Count; i++)
        //{
        //    string key = "uc" + (i + 1).ToString();
        //    panelExperience.Controls.Add(ht[key] as UserControl);
        //}
        if (alertValue > 1)
        {
            btnBack.Visible = true;
            btnClose.Visible = true;
        }
        else if (alertValue == 1)
        {
            btnBack.Visible = false;
            btnClose.Visible = true;
        }
        else
        {
            btnBack.Visible = true;
            btnClose.Visible = false;
        }
    }
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
        int userid = CurrentUserID;
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
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        if (!string.IsNullOrEmpty(Request["backurl"]))
        {
            backUrl = Request["backurl"];
        }

        if (!string.IsNullOrEmpty(Request["sname"]) && !string.IsNullOrEmpty(Request["truntocount"]))
        {

            backUrl = ((List<string>)Session[Request["sname"]])[Convert.ToInt32(Request["truntocount"])];
        }

        if (!string.IsNullOrEmpty(Request["alert"]))
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) - 1).ToString());

        Response.Redirect(string.Format(backUrl + "?{0}", param));



        //Response.Redirect(string.IsNullOrEmpty(Request["backurl"]) ? "ReporterList.aspx" : Request["backurl"].ToString());
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
                txtReporterPosition.Text = mlReporter.Reporterposition;
                txtIdCard.Text = mlReporter.Cardnumber;//身份证号 
                txtAddress.Text = mlReporter.Address_h;//住址
                //联系信息
                txtOfficePhone.Text = mlReporter.Tel_o.Trim();//办公电话 
                txtHomePhone.Text = mlReporter.Tel_h.Trim();//家庭电话 
                txtUsualMobile.Text = mlReporter.Usualmobile.Trim();//常用手机 
                txtBackupMobile.Text = mlReporter.Backupmobile.Trim();//备用手机 
                txtFax.Text = mlReporter.Fax.Trim();//传真 
                txtQq.Text = mlReporter.Qq.Trim();//QQ 
                txtMsn.Text = mlReporter.Msn.Trim();//MSN 
                txtOtherMessageSoftware.Text = mlReporter.Othermessagesoftware.Trim();
                txtEmailOne.Text = mlReporter.Emailone.Trim();//E-mail1
                txtEmailTwo.Text = mlReporter.Emailtwo.Trim();//E-mail2
                txtEmailThree.Text = mlReporter.Emailthree.Trim();//E-mail3
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
                //开户银行
                txtbankname.Text = mlReporter.Bankname;
                //开户姓名
                txtbankacountname.Text = mlReporter.Bankacountname;
                //帐号
                txtbankcardcode.Text = mlReporter.Bankcardcode;
                //稿酬标准
                txtwritingfee.Text = mlReporter.Writingfee.ToString();
                //付款方式
                ddlpaymentmode.SelectedValue = mlReporter.Paymentmode.ToString();
                //备注
                txtPrivateRemark.Text = mlReporter.Privateremark;
                //合作情况
                txtcooperatecircs.Text = mlReporter.Cooperatecircs;

                if (!CurrentUser.SysID.Equals(mlReporter.Createdbyuserid.ToString()))
                {
                    palPrivateInfo.Visible = false;
                }
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
        mlReporter.Msn = txtMsn.Text.Trim();//MSN 
        mlReporter.Othermessagesoftware = txtOtherMessageSoftware.Text.Trim();
        mlReporter.Emailone = txtEmailOne.Text.Trim();//E-mail1
        mlReporter.Emailtwo = txtEmailTwo.Text.Trim();//E-mail2
        mlReporter.Emailthree = txtEmailThree.Text.Trim();//E-mail3
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

        //开户银行
        mlReporter.Bankname = txtbankname.Text.Trim();
        //开户姓名
        mlReporter.Bankacountname = txtbankacountname.Text.Trim();
        //帐号
        mlReporter.Bankcardcode = txtbankcardcode.Text.Trim();
        //稿酬标准
        mlReporter.Writingfee = Double.Parse(txtwritingfee.Text.Trim() == "" ? "0" : txtwritingfee.Text.Trim());
        //付款方式
        mlReporter.Paymentmode = int.Parse(ddlpaymentmode.SelectedValue);
        //备注
        mlReporter.Privateremark = txtPrivateRemark.Text.Trim();
        

        //负责领域
        if (txtPrivateRemark.Text.Trim().Length > 500)
            mlReporter.Privateremark = txtPrivateRemark.Text.Trim().Substring(0, 500);
        else
            mlReporter.Privateremark = txtPrivateRemark.Text.Trim();

        //合作情况
        if (txtcooperatecircs.Text.Trim().Length > 500)
            mlReporter.Cooperatecircs = txtcooperatecircs.Text.Trim().Substring(0, 500);
        else
            mlReporter.Cooperatecircs = txtcooperatecircs.Text.Trim();

        return mlReporter;
    }
    #endregion

    //static int count = 2;
    //static Hashtable ht = new Hashtable();

    //protected void btn_AddExper_Click(object sender, EventArgs e)
    //{
    //    UserControl uc = (UserControl) this.LoadControl("skins/Experience.ascx");
    //    uc.ID = "uc" + ++count;
    //    panelExperience.Controls.Add(uc);
    //    ht.Add(uc.ID, uc);
    //}

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
            //imgname = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.ReporterLogoPath + ImageUpload.FileName);
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
        else
        {
            return;
        }
        if (Request["Rid"] == null)//插入
        {
            ReportersInfo report = GetObject();
            if (report == null) return;
            if (Request[RequestName.ProjectID] != null && Request[RequestName.ProjectID].Length > 0)
            {
                ret = ESP.Media.BusinessLogic.ReportersManager.AddWithProject(report, imgname, Convert.ToInt32(Request[RequestName.ProjectID]), CurrentUserID, out errmeg);
            }
            else
            {
                ret = ESP.Media.BusinessLogic.ReportersManager.Add(report, imgname, CurrentUserID, out errmeg);
            }
            if (ret > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');opener.location = opener.location;window.close();", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
            }
        }

    }
    #endregion

}
