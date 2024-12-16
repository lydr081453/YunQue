using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;
using System.Configuration;
using ESP.Media.Entity;

public partial class UserControl_ReporterControl_ReporterDisplay : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Bind reporter
    /// </summary>
    /// <param name="reporter"></param>
    public void InitPage(ReportersInfo reporter)
    {
        if (reporter != null)
        {
            if (reporter.Lastmodifiedbyuserid > 0)
            {
                labLastModifyUser.Text = new ESP.Compatible.Employee(reporter.Lastmodifiedbyuserid).Name;
            }
            if (reporter.Lastmodifieddate != null && reporter.Lastmodifieddate.Length > 0)
            {
                labLastModifyDate.Text = reporter.Lastmodifieddate;
            }
            //媒体信息
            int mid = reporter.Media_id;
            MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(mid);
            if (media != null)
            {
                labMediaName.Text = media.Mediacname + " " + media.Channelname + " " + media.Topicname;
                //lnkMediaName.Text = media.Mediacname + " " + media.Channelname + " " + media.Topicname;
            }
            //基本信息
            getFullImageUrl(reporter.Photo);
            labName.Text = reporter.Reportername.Trim();//姓名
            labPenName.Text = reporter.Penname.Trim();//笔名
            if (reporter.Sex == 1)//性别
                labSex.Text = "男";
            else if (reporter.Sex == 2)
                labSex.Text = "女";
            labBirthday.Text = reporter.Birthday.Trim().Split(' ')[0];//生日  
            if (labBirthday.Text.Equals("1900-1-1"))
            {
                labBirthday.Text = "";
            }
            labPostCord.Text = reporter.Postcode_h.Trim();//家庭邮编 
            labIdCard.Text = reporter.Cardnumber.Trim();//身份证号 
            labAddress.Text = reporter.Address_h.Trim();//住址
            this.labQq.Text = reporter.Qq.Trim();
            this.labMsn.Text = reporter.Msn.Trim();
            labOtherMessageSoftware.Text = reporter.Othermessagesoftware.Trim();
            //联系信息
            labOfficePhone.Text = reporter.Tel_o.Trim();//办公电话 
            labHomePhone.Text = reporter.Tel_h.Trim();//家庭电话 
            labUsualMobile.Text = reporter.Usualmobile.Trim();//常用手机 
            labBackupMobile.Text = reporter.Backupmobile.Trim();//备用手机 

            this.labreporterposition.Text = reporter.Reporterposition;
            //labFax.Text = reporter.Fax.Trim();//传真 
            //labQq.Text = reporter.Qq.Trim();//QQ 
            //labMsn.Text = reporter.Msn.Trim();//MSN 
            labEmailOne.Text = reporter.Emailone.Trim();//E-mail1
            labEmailTwo.Text = reporter.Emailtwo.Trim();//E-mail2
            labEmailThree.Text = reporter.Emailthree.Trim();//E-mail3
            //个人信息
            //labAttention.Text = reporter.Attention.Trim();//职责
            labHobby.Text = reporter.Hobby.Trim();//兴趣爱好
            labCharacter.Text = reporter.Character.Trim();//性格特点
            if (reporter.Marriage == 1)//婚姻状况
                labMarriage.Text = "已婚";
            else if (reporter.Marriage == 2)
                labMarriage.Text = "未婚";
            else
                labMarriage.Text = "保密";
            labFamily.Text = reporter.Family.Trim();//家庭成员
            labWriting.Text = reporter.Writing.Trim();//主要作品

            //负责领域
            labresponsibledomain.Text = reporter.Responsibledomain;

            //if (!CurrentUser.SysID.Equals(reporter.Createdbyuserid.ToString()))
            //{
            //    palPrivateInfo.Visible = false;
            //}
            //if (Request[RequestName.ProjectID] != null && Request[RequestName.ProjectID].Length > 0)
            //{
            //    ESP.Media.Entity.ProjectreporterrelationInfo r = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(Convert.ToInt32(Request[RequestName.ProjectID]), mlReporter.Reporterid);
            //    if (r != null)
            //    {
            //        //开户银行
            //        labbankname.Text = r.Bankname;
            //        //开户姓名
            //        labbankacountname.Text = r.Bankacountname;
            //        //帐号
            //        labbankcardcode.Text = r.Bankcardcode;
            //        //稿酬标准
            //        labwritingfee.Text = r.Writingfee.ToString();
            //        //付款方式
            //        labpaymentmode.Text = r.Paymentmode == 0 ? "" : ESP.Media.Access.Utilities.Global.PaymentModeName[r.Paymentmode].ToString();
            //        //备注
            //        labPrivateRemark.Text = r.Privateremark;
            //        //合作情况
            //        labcooperatecircs.Text = r.Cooperatecircs;
            //        palPrivateInfo.Visible = true;
            //    }
            //}
            //else
            //{
            //    //开户银行
            //    labbankname.Text = reporter.Bankname;
            //    //开户姓名
            //    labbankacountname.Text = reporter.Bankacountname;
            //    //帐号
            //    labbankcardcode.Text = reporter.Bankcardcode;
            //    //稿酬标准
            //    labwritingfee.Text = reporter.Writingfee.ToString();
            //    //付款方式
            //    labpaymentmode.Text = reporter.Paymentmode == 0 ? "" : ESP.Media.Access.Utilities.Global.PaymentModeName[reporter.Paymentmode].ToString();
            //    //备注
            //    labPrivateRemark.Text = reporter.Privateremark;
            //    //合作情况
            //    labcooperatecircs.Text = reporter.Cooperatecircs;
            //}

            //教育信息
            labEducation.Text = reporter.Education.Trim();//教育背景
            //hidMidId.Value = mid + "";
            //hidRidId.Value = id + "";

            labHometown.Text = reporter.Hometown;//籍贯
            //工作信息
            labWorkStory.Text = reporter.Experience;//职业经历
            //ListBind(reporter.Experience);
        }

    }

    private void getFullImageUrl(string imgurl)
    {
        if (string.IsNullOrEmpty(imgurl))
        {
            this.imgPic.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["ImageUrl"].ToString() + ESP.Configuration.ConfigurationManager.SafeAppSettings["DefauleImgPath"].ToString().Replace(".jpg", "_full.jpg");
        }
        else
            this.imgPic.ImageUrl = imgurl.Replace(".jpg", "_full.jpg");
    }

    //protected void btnChangeMedia_Click(object sender, EventArgs e)
    //{ }
}