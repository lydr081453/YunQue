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
    public partial class ReporterHistView : System.Web.UI.Page
    {
        int reporterId = 0;
        int reporterHistId = 0;
        ReportershistInfo mlReporter = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
                reporterHistId = int.Parse(Request["id"]);
            mlReporter = ESP.Media.BusinessLogic.ReportersManager.GetHistModel(reporterHistId);
            reporterId = mlReporter.Reporterid;
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporterHist.aspx?" + RequestName.ReporterID + "=" + reporterId);
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
                labName.Text = mlReporter.Reportername.Trim();
                
                lblReproterName.Text = mlReporter.Reportername.Trim();//姓名
                lblPenName.Text = mlReporter.Penname.Trim();//笔名
                if (mlReporter.Sex.ToString().Trim() == "1")
                    lblSex.Text = "男";
                else if (mlReporter.Sex.ToString().Trim() == "2")
                    lblSex.Text = "女";
                else
                    lblSex.Text = "保密";
                lblBirthday.Text = mlReporter.Birthday.Split(' ')[0];//生日 
                if (lblBirthday.Text.Equals("1900-1-1"))
                {
                    lblBirthday.Text = "";
                }
                lblHomePost.Text = mlReporter.Postcode_h.Trim();//家庭邮编 
                lblIdCard.Text = mlReporter.Cardnumber;//身份证号 
                lblHomeAddress.Text = mlReporter.Address_h;//住址
                lblPosition.Text = mlReporter.Reporterposition;
                //联系信息
                lblOfficePhone.Text = mlReporter.Tel_o.Trim();//办公电话 
                lblHomePhone.Text = mlReporter.Tel_h.Trim();//家庭电话 
                lblUsualMobile.Text = mlReporter.Usualmobile.Trim();//常用手机 
                lblBackupMobile.Text = mlReporter.Backupmobile.Trim();//备用手机 
                lblFax.Text = mlReporter.Fax.Trim();//传真 
                lblQQ.Text = mlReporter.Qq.Trim();//QQ 
                lblOtherPhone.Text = mlReporter.Othermessagesoftware.Trim();
                lblMsn.Text = mlReporter.Msn.Trim();//MSN 
                lblEmail1.Text = mlReporter.Emailone.Trim();//E-mail1
                lblEmail2.Text = mlReporter.Emailtwo.Trim();//E-mail2
                //garry zhang add new attributes at 11-3
                //this.lblOfficeAddress.Text = mlReporter.homeAddress;
                //this.lblOfficePost.Text = mlReporte;
                //个人信息
                lblHomeTown.Text = mlReporter.Hometown.Trim();//籍贯
                lblHobby.Text = mlReporter.Hobby.Trim();//兴趣爱好
                lblCharacter.Text = mlReporter.Character.Trim();//性格特点
                if (mlReporter.Marriage.ToString().Trim() == "1")//婚姻状况
                {
                    lblMarrige.Text = "未婚";
                }
                else if (mlReporter.Marriage.ToString().Trim() == "2")
                {
                    lblMarrige.Text = "已婚";
                }
                else
                {
                    lblMarrige.Text = "保密";
                }
                lblFamilly.Text = mlReporter.Family.Trim();//家庭成员
                lblWritting.Text = mlReporter.Writing.Trim();//主要作品
                //教育信息
                lblEducation.Text = mlReporter.Education.Trim();//教育背景
                //照片

                //负责领域
                lblDomain.Text = mlReporter.Responsibledomain;
                lblExperience.Text = mlReporter.Experience;
            }
        }

    }
}
