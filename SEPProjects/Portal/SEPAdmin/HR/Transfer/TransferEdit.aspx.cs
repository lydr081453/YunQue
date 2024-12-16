using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Transfer
{
    public partial class TransferEdit : ESP.Web.UI.PageBase
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
             //haid
            int headcountId = 0;
            ESP.HumanResource.Entity.HeadAccountInfo hcModel = null;

            if (!string.IsNullOrEmpty(Request["haid"]))
            {
                headcountId = int.Parse(Request["haid"]);
                hcModel = (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).GetModel(headcountId);



                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    int id = int.Parse(Request["id"]);

                    TransferInfo model = TransferManager.GetModel(id);
                    if (model != null)
                    {
                        ESP.Finance.Entity.DepartmentViewInfo oldDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.OldGroupId);
                        ESP.Finance.Entity.DepartmentViewInfo newDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.NewGroupId);

                        hidGroupId.Value = model.OldGroupId.ToString();
                        txtJob_GroupName.Text = model.OldGroupName;
                        txtJob_CompanyName.Text = oldDeptView.level1;
                        hidCompanyId.Value = oldDeptView.level1Id.ToString();
                        txtJob_DepartmentName.Text = oldDeptView.level2;
                        hidDepartmentID.Value = oldDeptView.level2Id.ToString();

                        hidTransferGroupIn.Value = model.NewGroupId.ToString();
                        txtTransferGroupIn.Text = model.NewGroupName;
                        txtTransferCompanyIn.Text = newDeptView.level1;
                        hidTransferCompanyIn.Value = newDeptView.level1Id.ToString();
                        txtTransferDeptIn.Text = newDeptView.level2;
                        hidTransferDeptIn.Value = newDeptView.level2Id.ToString();

                        txtSalaryBase.Text = model.SalaryBase.ToString("#,##0.00");
                        txtSalaryPromotion.Text = model.SalaryPromotion.ToString("#,##0.00"); ;
                        txtTransInDate.Text = model.TransInDate.ToString("yyyy-MM-dd");
                        txtRemark.Text = model.Remark;

                        hidHeadCountId.Value = model.HeadCountId.ToString();
                    }
                }
                else
                {
                    ESP.Finance.Entity.DepartmentViewInfo dept =ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(hcModel.GroupId);

                    hidHeadCountId.Value = hcModel.Id.ToString();

                    hidTransferGroupIn.Value = hcModel.GroupId.ToString();
                    txtTransferGroupIn.Text = dept.level3;

                    txtTransferCompanyIn.Text = dept.level1;
                    hidTransferCompanyIn.Value = dept.level1Id.ToString();
                    txtTransferDeptIn.Text = dept.level2;
                    hidTransferDeptIn.Value = dept.level2Id.ToString();

                    txtJob_JoinJob.Value=hcModel.PositionId.ToString();
                    txtPosition.Text = hcModel.Position; ;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            InitModel((int)Status.TransferStatus.Save);
        }

        private void InitModel(int status)
        {


            TransferInfo model = null;
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int id = int.Parse(Request["id"]);
                model = TransferManager.GetModel(id);
            }
            else
                model = new TransferInfo();

            model.CreateDate = DateTime.Now;
            model.Creater = CurrentUser.Name;
            model.CreaterId = CurrentUserID;
            model.NewGroupId = int.Parse(hidTransferGroupIn.Value);
            model.NewGroupName = txtTransferGroupIn.Text;


            model.NewPositionId = int.Parse(txtJob_JoinJob.Value);
            model.NewPositionName = txtPosition.Text;
            model.OldGroupId = int.Parse(hidGroupId.Value);
            model.OldGroupName = txtJob_GroupName.Text;
            model.SalaryBase = decimal.Parse(txtSalaryBase.Text);
            model.SalaryPromotion = decimal.Parse(txtSalaryPromotion.Text);
            model.TransInDate = DateTime.Parse(txtTransInDate.Text);
            model.TransOutDate = new DateTime(1900, 1, 1);
            model.Status = status;
            model.HeadCountId = int.Parse(hidHeadCountId.Value);
            model.Remark = txtRemark.Text;

            ESP.Framework.Entity.OperationAuditManageInfo oldOperation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.OldGroupId);
            ESP.Framework.Entity.OperationAuditManageInfo newOperation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.NewGroupId);

            if (UserID != newOperation.HRId && UserID != int.Parse(System.Configuration.ConfigurationManager.AppSettings["HRAdminID"]))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有该组的转组权限!');window.location.href='TransferList.aspx';", true);

                return;
            }
            int ret = 0;
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                ret = TransferManager.Update(model);
            }
            else
            {
                ESP.HumanResource.Entity.HeadAccountInfo hcModel = (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).GetModel(model.HeadCountId);
                model.Id = ret = TransferManager.Add(model,hcModel);

            }

            string msg = "";

            if (status == (int)Status.TransferStatus.Save)
            {
                msg = "保存成功";
            }
            else
            {

                msg = "提交成功，请等待转出组HR审批！";
            }

            if (ret > 0)
            {
                string responsePage = "TransferList.aspx";

                if (model.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.HRCommit)
                {
                    //添加下一级HR审批人信息

                    ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                    newHRAuditLogInfo.AuditorId = CurrentUserID;
                    newHRAuditLogInfo.AuditorName = CurrentUserName;
                    newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
                    newHRAuditLogInfo.FormId = model.Id;
                    newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;
                    newHRAuditLogInfo.Requesition = "提交转组申请";
                    newHRAuditLogInfo.AuditDate = DateTime.Now;
                    newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.Status.TransferStatus.Save;

                    ESP.HumanResource.BusinessLogic.HRAuditLogManager.Add(newHRAuditLogInfo);


                    ESP.Framework.Entity.UserInfo nextHr = ESP.Framework.BusinessLogic.UserManager.Get(oldOperation.HRId);

                    ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                    oldHRAuditLogInfo.AuditorId = nextHr.UserID;
                    oldHRAuditLogInfo.AuditorName = nextHr.FullNameCN;
                    oldHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                    oldHRAuditLogInfo.FormId = model.Id;
                    oldHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;
                    oldHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.Status.TransferStatus.HRCommit;

                    ESP.HumanResource.BusinessLogic.HRAuditLogManager.Add(oldHRAuditLogInfo);



                    if (oldOperation.HRId == newOperation.HRId)
                    {
                        responsePage = "TransferHREdit.aspx?id=" + model.Id;
                    }
                    //发邮件
                    List<MailAddress> mailAddressList = new List<MailAddress>();
                    mailAddressList.Add(new MailAddress(nextHr.Email));
                    if (mailAddressList != null && mailAddressList.Count > 0)
                    {
                        string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + model.Id;
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        ESP.Mail.MailManager.Send("员工转组 - " + CurrentUserName + "提交了转组申请", body, true, mailAddressList.ToArray());
                    }
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + msg + "');window.location.href='" + responsePage + "';", true);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败，请联系管理员。');window.location.href='TransferList.aspx';", true);
            }


        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferList.aspx");
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            InitModel((int)Status.TransferStatus.HRCommit);
        }
    }
}