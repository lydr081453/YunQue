using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;
using ESP.HumanResource.Common;

namespace SEPAdmin.HR.Join
{
    public partial class HeadAccountAudit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            int haid = int.Parse(Request["haid"]);
            HeadAccountInfo model = new HeadAccountManager().GetModel(haid);
            var deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.GroupId);

            PositionLevelsInfo pb = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(model.LevelId);

            this.lblDept.Text = deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3;
            lblCreator.Text = model.Creator;
            lblAppDate.Text = model.CreateDate.ToString();
            this.lblRemark.Text = model.Remark;
            this.lblPosition.Text = model.Position;
            lblUserPosition.Text = model.Position;
            this.lblLevel.Text = model.LevelName;
            this.chkAAD.Checked = model.IsAAD;
            this.chkAAD.Enabled = false;
            this.lblCustomer.Text = model.CustomerName;
            this.lblReplaceReason.Text = model.ReplaceReason;
            if (model.ReplaceUserId != 0)
            {
                ESP.Framework.Entity.UserInfo replaceModel = ESP.Framework.BusinessLogic.UserManager.Get(model.ReplaceUserId);
                this.lblReplaceUser.Text = replaceModel.FullNameCN + "  " + model.ReplaceUserPosition;
            }
            this.lblRequestment.Text = model.Requestment;
            this.lblResponse.Text = model.Response;
            this.lblDimissionDate.Text = model.DimissionDate == null ? "" : model.DimissionDate.Value.ToString("yyyy-MM-dd");
            if (model.NewBiz == "立项")
            {
                chkCreate.Checked = true;
                chkUnCreate.Checked = false;
            }
            else if (model.NewBiz == "未立项")
            {
                chkCreate.Checked = false;
                chkUnCreate.Checked = true;
            }
            else
            {
                chkCreate.Checked = false;
                chkUnCreate.Checked = false;
            }



            var loglist = new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().GetList(" and HeadAccountId=" + model.Id);

            foreach (var log in loglist)
            {
                string auditstatus = "审批通过";
                if (log.Status == 2)
                    auditstatus = "审批驳回";
                else if (log.Status == 3)
                {
                    auditstatus = "审批留言";
                }

                string logstr = string.Format("[{0}] {1}{2}：{3}（{4}）", ESP.HumanResource.Common.Status.HeadAccountStatus_Names[log.AuditType], log.Auditor, auditstatus, log.Remark, log.AuditDate.ToString());
                lblLog.Text += logstr + "</br>";
            }

            if (!string.IsNullOrEmpty(model.CostUrl))
            {
                hypAttach.ToolTip = "下载附件：" + model.CostUrl;
                this.hypAttach.NavigateUrl = "/HR/Employees/FileDownLoad.aspx?ContractID=" + model.Id.ToString();
                hypAttach.Visible = true;
                //fileupContract.Visible = false;
            }

            this.lblSalary.Text = pb.SalaryLow.ToString("#,##0.00") + " - " + pb.SalaryHigh.ToString("#,##0.00");

            if (model.OfferLetterUserId != 0)
            {
                initForm(model.OfferLetterUserId);
            }
            else
            {
                if (model.TalentId != 0)
                {
                    BindTalent(model.TalentId);
                }
            }
        }

        private void BindTalent(int talentId)
        {
            try
            {
                ESP.HumanResource.Entity.TalentInfo talentModel = (new TalentManager()).GetModel(talentId);

                lblLastNameCn.Text = talentModel.NameCN.Substring(0, 1);
                lblFirstNameCn.Text = talentModel.NameCN.Substring(1);
                lblPrivateEmail.Text = talentModel.EMail;

                lblMobile.Text = talentModel.Mobile;

            }
            catch (Exception ex)
            {
            }
        }


        protected void initForm(int sysid)
        {
            try
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);
                ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);
                ESP.Administrative.Entity.OperationAuditManageInfo audit = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(user.UserID);

                ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(user.UserID);

                ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(position.DepartmentID);

                lblLastNameCn.Text = user.LastNameCN;
                lblFirstNameCn.Text = user.FirstNameCN;
                lblPrivateEmail.Text = model.PrivateEmail;
                lblJoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");

                lblMobile.Text = model.MobilePhone;
                lblUserRemark.Text = model.Memo;
                lblIDCard.Text = model.IDNumber;
                lblNowBasePay.Text = model.Pay.ToString();
                lblNowMeritPay.Text = model.Performance.ToString();
                lblNowAttendance.Text = model.Attendance.ToString();

                if (operation != null)
                {
                    this.lblVPAuditor.Text = operation.HeadCountDirector;
                }

                if (audit != null)
                {
                    this.lblKaoqin.Text = audit.TeamLeaderName;
                }


                this.lblUserType.Text = ESP.Framework.BusinessLogic.EmployeeManager.GetTypeName(model.TypeID);


                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eiplist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + model.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = eiplist[0];


                if (!string.IsNullOrEmpty(model.WorkCity))
                {
                    var dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(model.WorkCity));
                    lblLocation.Text = dept == null ? "" : dept.DepartmentName;
                }

                //lblTmplate.Text = model.OfferLetterTemplate.ToString();  // Offer Letter模板
                chkExamen.Checked = model.IsExamen;   // 是否是应届毕业生。
                lblSeniority.Text = model.Seniority.ToString();
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
            }
        }


        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            int haid = int.Parse(Request["haid"]);
            HeadAccountInfo model = new HeadAccountManager().GetModel(haid);
            ESP.HumanResource.Entity.HeadAccountLogInfo log = new HeadAccountLogInfo();
            log.AuditDate = DateTime.Now;
            log.Auditor = UserInfo.FullNameCN;
            log.AuditorId = UserID;
            log.HeadAccountId = model.Id;
            log.Remark = this.txtAudit.Text;
            log.Status = 1;
            int ret = 0;

            ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupId);

            if (string.IsNullOrEmpty(this.txtAudit.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请输入审批意见！');", true);

                return;
            }
            if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit)
            {
                if (operationModel.HeadCountAuditorId != operationModel.HeadCountDirectorId && CurrentUserID == operationModel.HeadCountAuditorId)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您已经审批过该条记录！');", true);

                    return;
                }

                if (this.fileupContract.FileName != string.Empty)
                {
                    fileName = "HeadAccount_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;

                    this.fileupContract.SaveAs(ESP.Finance.Configuration.ConfigurationManager.ContractPath + fileName);

                    model.CostUrl = fileName;
                }


                log.AuditType = model.Status;
                model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit;

                //如果终审人和VP是同一个人
                if (operationModel.HeadCountDirectorId == operationModel.HCFinalAuditor)
                {
                    model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.FinanceApproved;
                }

                ret = new ESP.HumanResource.BusinessLogic.HeadAccountManager().Audit(model, log);

                SendMail("HeadCount VP审批通过", model);
            }
            else if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit)
            {

                model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.FinanceApproved;

                ret = new ESP.HumanResource.BusinessLogic.HeadAccountManager().Audit(model, log);

                SendMail("HeadCount 团队终审通过", model);
            }
            else if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitPreVPAudit)
            {

                if (this.fileupContract.FileName != string.Empty)
                {
                    fileName = "HeadAccount_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;

                    this.fileupContract.SaveAs(ESP.Finance.Configuration.ConfigurationManager.ContractPath + fileName);

                    model.CostUrl = fileName;
                }
                //else
                //{
                //    if (string.IsNullOrEmpty(model.CostUrl))
                //    {
                //        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请上传团队报告相关的附件！');", true);

                //        return;
                //    }
                //}

                log.AuditType = model.Status;
                model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit;


                ret = new ESP.HumanResource.BusinessLogic.HeadAccountManager().Audit(model, log);

                SendMail("HeadCount 团队预审通过", model);
            }
            else if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.VPApproved)
            {
                log.AuditType = model.Status;
                model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit;
               // model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.FinanceApproved;
                ret = new ESP.HumanResource.BusinessLogic.HeadAccountManager().Audit(model, log);

                SendMail("HeadCount 团队VP审批通过", model);
            }
            else if (model.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView)
            {
                ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupId);

                ESP.HumanResource.Entity.EmployeeBaseInfo empHR = EmployeeBaseManager.GetModel(operation.HRId); 

                ESP.HumanResource.Entity.EmployeeBaseInfo empModel = EmployeeBaseManager.GetModel(model.OfferLetterUserId);
                empModel.Status = ESP.HumanResource.Common.Status.IsSendOfferLetter;
                decimal pay = empModel.Pay;
                decimal performance = empModel.Performance;
                decimal attendance = empModel.Attendance;

                log.AuditType = model.Status;
                model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.OfferLetteAudited;

                ret = new ESP.HumanResource.BusinessLogic.HeadAccountManager().Audit(model, empModel, log);


                if (!string.IsNullOrEmpty(empModel.PrivateEmail))
                {
                    //普通模板1
                    //总监级以上模板2
                    //实习生模板3
                    //销售模板4
                    //12薪模板5
                    //高级销售模板6

                    string url = "";
                    //if (empModel.OfferLetterTemplate == 3)
                    //    url = "http://" + Request.Url.Authority + "/HR/Join/InternOfferLetter.aspx?userid=" + empModel.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                    //else if (empModel.OfferLetterTemplate == 4)
                    //    url = "http://" + Request.Url.Authority + "/HR/Join/SalesOfferLetter.aspx?userid=" + empModel.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                    //else if (empModel.OfferLetterTemplate == 5)
                    //    url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter12.aspx?userid=" + empModel.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                    //else if (empModel.OfferLetterTemplate == 6)
                    //    url = "http://" + Request.Url.Authority + "/HR/Join/SalesOfferLetter.aspx?userid=" + empModel.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&showSalary=1";
                    //else
                    url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter.aspx?userid=" + empModel.UserID + "&nowBasePay=" + pay.ToString("#,##0.00") + "&nowMeritPay=" + performance.ToString("#,##0.00") + "&nowAttendance=" + attendance.ToString("#,##0.00") + "&showSalary=1";
                    try
                    {
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

                        ESP.Mail.MailManager.Send("聘用任职信", body, true, new MailAddress[] { new MailAddress(empModel.PrivateEmail), new MailAddress(empHR.InternalEmail) });
                    }
                    catch
                    { }
                    ShowCompleteMessage("聘用任职信发送成功！", "OfferLetterAuditList.aspx");
                }
                else
                {
                    // ESP.Logging.Logger.Add("没有填写" + empModel.FullNameCN + "个人邮箱。");
                    ShowCompleteMessage("聘用任职信发送失败！", "OfferLetterAuditList.aspx");
                }
                
            }
           

            if (ret == 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批成功！');window.location.href='HeadAccountAuditList.aspx';", true);
            }
        }

        private void SendMail(string title, ESP.HumanResource.Entity.HeadAccountInfo model)
        {
            string recipientAddress = "";

            ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupId);
            ESP.HumanResource.Entity.EmployeeBaseInfo directorModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(operation.DirectorId);
            ESP.HumanResource.Entity.EmployeeBaseInfo hrModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(operation.HRId);
            ESP.HumanResource.Entity.EmployeeBaseInfo financeModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(System.Configuration.ConfigurationManager.AppSettings["AdvanceID"]));
            recipientAddress += string.IsNullOrEmpty(directorModel.InternalEmail) ? "" : directorModel.InternalEmail + ",";
            recipientAddress += string.IsNullOrEmpty(hrModel.InternalEmail) ? "" : hrModel.InternalEmail + ",";
            recipientAddress += string.IsNullOrEmpty(financeModel.InternalEmail) ? "" : financeModel.InternalEmail + ",";

            string url = "http://" + Request.Url.Authority + "/HR/Print/HeadCountMail.aspx?haid=" + model.Id.ToString();
            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
            ESP.Logging.Logger.Add(recipientAddress);
            try
            {
                SendMailHelper.SendMail(title, recipientAddress, body, null);
            }
            catch { }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAudit.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请输入审批意见！');", true);

                return;
            }
            int haid = int.Parse(Request["haid"]);
            HeadAccountInfo model = new HeadAccountManager().GetModel(haid);
            ESP.HumanResource.Entity.HeadAccountLogInfo log = new HeadAccountLogInfo();
            log.AuditDate = DateTime.Now;
            log.Auditor = UserInfo.FullNameCN;
            log.AuditorId = UserID;

            log.AuditType = model.Status;

            model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Reject;

            log.HeadAccountId = model.Id;
            log.Remark = this.txtAudit.Text;
            log.Status = 2;

            int ret = new ESP.HumanResource.BusinessLogic.HeadAccountManager().Audit(model, log);
            if (ret == 1)
            {
                SendMail("HeadCount 审批驳回", model);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('驳回成功！');window.location.href='HeadAccountAuditList.aspx';", true);
            }
        }


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeadAccountAuditList.aspx");
        }

        protected void btnMessage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAudit.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请在审批意见一栏输入留言！');", true);

                return;
            }
            int haid = int.Parse(Request["haid"]);
            HeadAccountInfo model = new HeadAccountManager().GetModel(haid);
            ESP.HumanResource.Entity.HeadAccountLogInfo log = new HeadAccountLogInfo();
            log.AuditDate = DateTime.Now;
            log.Auditor = UserInfo.FullNameCN;
            log.AuditorId = UserID;

            log.AuditType = model.Status;

            model.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Reject;

            log.HeadAccountId = model.Id;
            log.Remark = this.txtAudit.Text;
            log.Status = 3;

            int ret = new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().Add(log);
            if (ret > 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('留言成功！');window.location.href='HeadAccountAuditList.aspx';", true);
            }
        }

    }
}
