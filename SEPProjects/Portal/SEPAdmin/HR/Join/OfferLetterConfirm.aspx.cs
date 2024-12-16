using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System.Net.Mail;
using ESP.HumanResource.Common;

public partial class OfferLetterConfirm : System.Web.UI.Page
{
    protected int curUserId
    {
        get
        {
            return ViewState["curUserId"] == null ? 0 : (int)ViewState["curUserId"];
        }
        set
        {
            ViewState["curUserId"] = value;
        }
    }

    /// <summary>
    /// 上传前验证必填项是否为空
    /// </summary>
    /// <returns></returns>
    private bool CheckIsNull()
    {
        if (string.IsNullOrEmpty(this.txtBase_FirstNameEn.Text))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请填写您的名字!');", true);
            txtBase_FirstNameEn.Focus();
            return false;
        }
        else
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[A-Za-z]+$");
            if (!r.IsMatch(this.txtBase_FirstNameEn.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('First Name只能输入字母!');", true);
                txtBase_FirstNameEn.Focus();
                return false;
            }
        }

        if (string.IsNullOrEmpty(this.txtBase_LastNameEn.Text))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请填写您的姓氏!');", true);
            txtBase_LastNameEn.Focus();
            return false;
        }
        else
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[A-Za-z]+$");
            if (!r.IsMatch(this.txtBase_LastNameEn.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('Last Name只能输入字母!');", true);
                txtBase_LastNameEn.Focus();
                return false;
            }
        }
        if (string.IsNullOrEmpty(this.txtIDCard.Text))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请填写您的身份证号码!');", true);
            txtIDCard.Focus();
            return false;
        }
        
        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!string.IsNullOrEmpty(Request["code"]))
            //{
            //    int userid = 0;
            //    if (int.TryParse(ESP.Salary.Utility.DESEncrypt.Decode(Request["code"]), out userid))
            //        curUserId = userid;
            //    ESP.HumanResource.Entity.UsersInfo userInfo = UsersManager.GetModel(curUserId);
            //    ESP.HumanResource.Entity.EmployeeBaseInfo employeeBaseInfo = EmployeeBaseManager.GetModel(curUserId);
            //    if (employeeBaseInfo.OfferLetterSendTime != null)
            //    {
            //        DateTime sendTime = employeeBaseInfo.OfferLetterSendTime.Value.AddDays(5);
            //        sendTime = sendTime.AddHours(23 - sendTime.Hour);
            //        sendTime = sendTime.AddMinutes(59 - sendTime.Minute);
            //        sendTime = sendTime.AddSeconds(59 - sendTime.Second);
            //        if (sendTime < DateTime.Now)
            //        {
            //            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此链接地址已过期，如需帮助请与星言云汇人力资源部联系。');window.location='';", true);
            //        }
            //    }

            //    if (userInfo != null)
            //    {
            //        labUserName.Text = userInfo.LastNameCN + userInfo.FirstNameCN;
            //        labJoinDate.Text = employeeBaseInfo.EmployeeJobInfo.joinDate.ToString("yyyy年MM月dd日");

            //        if (employeeBaseInfo.EmployeeJobInfo.companyid == 230)//重庆
            //        {
            //            labAddress.Text = "重庆市渝北区大竹林街道杨柳路6号三狼公园6号D4-102";
            //            lblCard.Text = "重庆：";
            //            lblPhone.Text = "";
            //            lblIT.Text = "";
            //        }
            //        else
            //        {
            //            labAddress.Text = "北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼";
            //            lblCard.Text = "北京：";
            //            lblPhone.Text = "（北京）010-8509 5766";
            //            lblIT.Text = "";
            //        }
            //    }
            //    else
            //    {
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此链接地址无效。');window.location='';", true);
            //    }
            //    //上海市普陀区南郑路355弄9号鸿企中心22层2201室
            //    //广州市海珠区新滘中路88号唯品同创汇6区6-4B栋
            //    //北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼
            //}
            //else
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此链接地址无效。');window.location='';", true);
            //}
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {

        try
        {
            ESP.HumanResource.Entity.UsersInfo userInfo = UsersManager.GetModel(curUserId);
            EmployeeBaseInfo empBaseInfo = EmployeeBaseManager.GetModel(curUserId);
            if (userInfo != null && empBaseInfo != null && empBaseInfo.Status == (int)ESP.HumanResource.Common.Status.IsSendOfferLetter)
            {
                if (string.IsNullOrEmpty(this.txtBase_FirstNameEn.Text))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('请填写您的名字!');", true);
                    txtBase_FirstNameEn.Focus();
                    return;
                }
                else
                {
                    System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[A-Za-z]+$");
                    if (!r.IsMatch(this.txtBase_FirstNameEn.Text))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('First Name只能输入字母!');", true);
                        txtBase_FirstNameEn.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(this.txtBase_LastNameEn.Text))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('请填写您的姓氏!');", true);
                    txtBase_LastNameEn.Focus();
                    return;
                }
                else
                {
                    System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[A-Za-z]+$");
                    if (!r.IsMatch(this.txtBase_LastNameEn.Text))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('Last Name只能输入字母!');", true);
                        txtBase_LastNameEn.Focus();
                        return;
                    }
                }
                if (EmployeeBaseManager.checkUserCodeExists((txtBase_FirstNameEn.Text.Trim() + "." + txtBase_LastNameEn.Text.Trim()).ToLower()))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('登录名已存在,请重新输入。');", true);
                    return;
                }
                else
                {
                    userInfo.FirstNameEN = txtBase_FirstNameEn.Text.Trim();
                    userInfo.LastNameEN = txtBase_LastNameEn.Text.Trim();
                    userInfo.Username = (userInfo.FirstNameEN + "." + userInfo.LastNameEN).ToLower();
                    userInfo.Email = userInfo.Username + "@xc-ch.com";

                    empBaseInfo.InternalEmail = userInfo.Email;
                    empBaseInfo.IDNumber = txtIDCard.Text.Trim();
                    empBaseInfo.MobilePhone = txtMobilePhone.Text.Trim();

                    int birthdayYear = int.Parse(empBaseInfo.IDNumber.Substring(6, 4));
                    int birthdayMonth = int.Parse(empBaseInfo.IDNumber.Substring(10, 2));
                    int birthdayDay = int.Parse(empBaseInfo.IDNumber.Substring(12, 2));

                    empBaseInfo.Birthday = new DateTime(birthdayYear, birthdayMonth, birthdayDay);

                    empBaseInfo.Phone2 = txtMobilePhone.Text.Trim();
                    empBaseInfo.MobilePhone = txtMobilePhone.Text.Trim();
                    empBaseInfo.WorkBegin = Convert.ToDateTime(txtWorkBegin.Text);
                    empBaseInfo.Residence = ddlHuji.SelectedValue;
                    empBaseInfo.Political = ddlPolicity.SelectedValue;
                    empBaseInfo.SocialSecurityType = int.Parse(ddlSocialSecurty.SelectedValue);
                    empBaseInfo.Nation = txtNation.Text;
                    empBaseInfo.DomicilePlace = txtBase_DomicilePlace.Text;

                    empBaseInfo.EmergencyContact = txtBase_EmergencyLinkman.Text;
                    empBaseInfo.EmergencyContactPhone = txtBase_EmergencyPhone.Text;
                    empBaseInfo.WorkExperience = txtBase_WorkExperience.Text;
                    empBaseInfo.WorkSpecialty = txtBase_WorkSpecialty.Text;

                    empBaseInfo.ContractBeginDate = DateTime.Now;
                    empBaseInfo.ContractEndDate = DateTime.Now;
                    empBaseInfo.ContractSignDate = DateTime.Now;

                    empBaseInfo.MaritalStatus = int.Parse(txtBase_Marriage.SelectedItem.Value);

                    empBaseInfo.Address = txtBase_Address1.Text;

                    empBaseInfo.SalaryBank = txtSalaryBank.Text;
                    empBaseInfo.SalaryCardNo = txtSalaryCardNo.Text;


                    //empBaseInfo.Pay = 0;
                    //empBaseInfo.Performance = 0;
                    //empBaseInfo.Attendance = 0;


                    //毕业院校信息
                    ESP.HumanResource.Entity.EmpEducationInfo education = new EmpEducationInfo();
                    education.UserId = empBaseInfo.UserID;
                    education.Degree = txtBase_Education.SelectedValue;
                    education.BeginDate = DateTime.Now;
                    education.EndDate = DateTime.Now;
                    education.Profession = txtProfessional.Text;
                    education.School = txtSchool.Text;


                    empBaseInfo.Gender = int.Parse(this.ddlGender.SelectedValue);

                    if (rdComputer.SelectedValue == "")  // 是否自带笔记本
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择是否自带笔记本!')", true);
                        return;
                    }
                    else
                    {
                        if (rdComputer.SelectedValue == "1")
                            empBaseInfo.OwnedPC = true;
                        else
                            empBaseInfo.OwnedPC = false;
                    }

                    empBaseInfo.Photo = "blank.jpg";

                    empBaseInfo.Status = ESP.HumanResource.Common.Status.WaitEntry;


                    #region 日志信息
                    ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                    logModel.LogMedifiedTeme = DateTime.Now;
                    logModel.Des = userInfo.LastNameCN + userInfo.FirstNameCN + "确认了Offer Letter";
                    logModel.LogUserId = userInfo.UserID;
                    logModel.LogUserName = userInfo.Username;
                    #endregion

                    bool ret = EmployeeBaseManager.Update(userInfo, empBaseInfo, logModel);
                    if (ret)
                    {
                        ESP.HumanResource.BusinessLogic.EmpEducationManager.Add(education);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您已确认入职！');", true);
                        SendMail(empBaseInfo, userInfo);
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此链接地址无效。');window.location='';", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('确认入职时出现错误，请稍后再试。');", true);
            ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
        }
    }

    void SendMail(EmployeeBaseInfo empBaseInfo, ESP.HumanResource.Entity.UsersInfo userInfo)
    {
        int sender = empBaseInfo.OfferLetterSender;
        if (sender <= 0)
            return;
        ESP.Framework.Entity.UserInfo senderInfo = UserManager.Get(sender);
        if (senderInfo != null && !string.IsNullOrEmpty(senderInfo.Email))
        {
            ESP.Mail.MailManager.Send("Offer Letter已确认", "您好，" + userInfo.LastNameCN + userInfo.FirstNameCN + "已确认了Offer Letter,请做好入职准备。", true, new MailAddress[] { new MailAddress(senderInfo.Email) });
        }
    }
}
