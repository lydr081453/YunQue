using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;

namespace SEPAdmin.HR.Join
{
    public partial class OfferLetterAudit : ESP.Web.UI.PageBase
    {
        protected int userId
        {
            get { return ViewState["userid"] == null ? 0 : (int)ViewState["userid"]; }
            set { ViewState["userid"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["userid"]))
                {
                    int uid = 0;
                    if (int.TryParse(Request["userid"].Trim(), out uid))
                        userId = uid;
                    initForm(userId);
                }
            }
        }
        #region 根据Model对象，初始化页面控件
        /// <summary>
        /// Inits the form.
        /// </summary>
        /// <param name="sysid">The sysid.</param>
        protected void initForm(int sysid)
        {
            try
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);
                ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);
                lblBase_FirstNameCn.Text = user.FirstNameCN;
                lblBase_LastNameCn.Text = user.LastNameCN;
                lblUserType.Text = ESP.Framework.BusinessLogic.EmployeeManager.GetTypeName(model.TypeID);
                lblPrivateEmail.Text = model.PrivateEmail;
                lblJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
                lblJob_CompanyName.Text = model.EmployeeJobInfo.companyName;
                lblMobilePhone.Text = model.MobilePhone;
                lblJob_DepartmentName.Text = model.EmployeeJobInfo.departmentName;
                lblJob_GroupName.Text = model.EmployeeJobInfo.groupName;
                lblJob_Memo.Text = model.Memo;
                lblIDCard.Text = model.IDNumber;

                ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.UserID);
                if (position != null)
                {
                    ESP.Framework.Entity.DepartmentInfo dep = ESP.Framework.BusinessLogic.DepartmentManager.Get(position.DepartmentID);
                    lblWorktype.Text = dep.DepartmentName;
                }
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eiplist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + model.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = eiplist[0];
                ESP.Framework.Entity.DepartmentPositionInfo positionModel =ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(deps.DepartmentPositionID);
                ESP.HumanResource.Entity.PositionBaseInfo baseinfo = ESP.HumanResource.BusinessLogic.PositionBaseManager.GetModel(positionModel.PositionBaseId);
                ESP.HumanResource.Entity.PositionLevelsInfo levelModel = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(baseinfo.LeveId);

                if (CurrentUserID == int.Parse(System.Configuration.ConfigurationManager.AppSettings["OfferLetterAudit"].ToString().Trim()))
                {
                    lblNowBasePay.Text = model.Pay.ToString("#,##0.00");
                    lblNowMeritPay.Text = model.Performance.ToString("#,##0.00");
                    lblLevel.Text = baseinfo.LevelName;
                    lblLevelLow.Text = levelModel.SalaryLow.ToString("#,##0.00");
                    lblLevelHigh.Text = levelModel.SalaryHigh.ToString("#,##0.00");
                }

                lblJob_JoinJob.Text = deps.DepartmentPositionName;
                if (model.OfferLetterTemplate == 1)
                {
                    lblOfferTemplate.Text = "普通模板";
                }
                else if (model.OfferLetterTemplate == 2)
                {
                    lblOfferTemplate.Text = "总监级以上模板";
                }
                else if (model.OfferLetterTemplate == 3)
                {
                    lblOfferTemplate.Text = "实习生模板";
                }
                else if (model.OfferLetterTemplate == 4)
                {
                    lblOfferTemplate.Text = "销售模板";
                }
                else if (model.OfferLetterTemplate == 5)
                {
                    lblOfferTemplate.Text = "12薪模板";
                }

                if (model.IsExamen)
                {
                    lblExamen.Text = "是应届毕业生";
                }
                else
                {
                    lblExamen.Text = "不是应届毕业生";
                }
                lblSeniority.Text = model.Seniority.ToString();
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
            }
        }

        #endregion

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userId);
            if (CurrentUser.SysID == System.Configuration.ConfigurationManager.AppSettings["OfferLetterAudit"])
            {
                model.Status = ESP.HumanResource.Common.Status.IsSendOfferLetter;
                decimal pay = model.Pay;
                decimal performance = model.Performance;
                decimal attendance = model.Attendance;
                //model.Pay = 0;
                //model.Performance = 0;

                EmployeeBaseManager.Update(model);

                if (!string.IsNullOrEmpty(model.PrivateEmail))
                {
                    string url = "";
                    //if (model.OfferLetterTemplate == 3)
                    //    url = "http://" + Request.Url.Authority + "/HR/Join/InternOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                    //else if (model.OfferLetterTemplate == 4)
                    //    url = "http://" + Request.Url.Authority + "/HR/Join/SalesOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                    //else if (model.OfferLetterTemplate == 5)
                    //    url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter12.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                    //else if (model.OfferLetterTemplate == 6)
                    //    url = "http://" + Request.Url.Authority + "/HR/Join/SalesOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                    //else
                    url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&nowAttendance=" + attendance.ToString("#,##0.00") + "&showSalary=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

                    ESP.Mail.MailManager.Send("聘用任职信", body, true, new MailAddress[] { new MailAddress(model.PrivateEmail) });
                    ShowCompleteMessage("聘用任职信发送成功！", "OfferLetterAuditList.aspx");
                }
                else
                {
                    ESP.Logging.Logger.Add("没有填写" + model.FullNameCN + "个人邮箱。");
                    ShowCompleteMessage("聘用任职信发送失败！", "OfferLetterAuditList.aspx");
                }
            } 
            else
            {
                model.Status = ESP.HumanResource.Common.Status.OfferHRAudit;
                EmployeeBaseManager.Update(model);

                string audit = "";
                audit = ESP.Configuration.ConfigurationManager.SafeAppSettings["OfferLetterAudit"];
                ESP.Framework.Entity.UserInfo userInfo = new ESP.Framework.Entity.UserInfo();
                if (audit != string.Empty)
                {
                    userInfo = ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(audit));
                }
                if (!string.IsNullOrEmpty(userInfo.Email))
                {
                    if (!string.IsNullOrEmpty(userInfo.Email))
                    {
                        string url = "";
                        if (model.OfferLetterTemplate == 3)
                            url = "http://" + Request.Url.Authority + "/HR/Join/InternAuditOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay.ToString("#,##0.00") + "&showSalary=1";
                        else
                            url = "http://" + Request.Url.Authority + "/HR/Join/AuditOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay.ToString("#,##0.00") + "&nowMeritPay=" + model.Performance.ToString("#,##0.00") + "&showSalary=1";
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        ESP.Mail.MailManager.Send("待审核(聘用任职信)", body, true, new MailAddress[] { new MailAddress(userInfo.Email) });
                        ShowCompleteMessage("提交成功，邮件已发送，请等待审核！", "OfferLetterAuditList.aspx");
                    }
                    else
                    {
                        ESP.Logging.Logger.Add("没有填写" + model.FullNameCN + "个人邮箱。");
                        ShowCompleteMessage("提交成功，邮件发送失败！", "OfferLetterAuditList.aspx");
                    }
                }
                else
                {
                    ESP.Logging.Logger.Add("没有填写" + model.FullNameCN + "个人邮箱。");
                    ShowCompleteMessage("提交成功，邮件发送失败！", "OfferLetterAuditList.aspx");
                }

            }
           

        }

        protected void btnBackAudit_Click(object sender, EventArgs e)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userId);
            model.Status = ESP.HumanResource.Common.Status.IsSaved;
            EmployeeBaseManager.Update(model);
            ShowCompleteMessage("Offer Letter已驳回！", "OfferLetterAuditList.aspx");
        }
    }

}
