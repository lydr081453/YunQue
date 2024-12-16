using ESP.HumanResource.BusinessLogic;
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
    public partial class TransferHrEdit : ESP.Web.UI.PageBase
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
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int id = int.Parse(Request["id"]);

                TransferInfo model = TransferManager.GetModel(id);
                if (model != null)
                {
                    ESP.Finance.Entity.DepartmentViewInfo oldDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.OldGroupId);
                    ESP.Finance.Entity.DepartmentViewInfo newDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.NewGroupId);

                    hidGroupId.Value = model.OldGroupId.ToString();
                    lblTransOutGroup.Text = model.OldGroupName;
                    lblTransOutCompany.Text = oldDeptView.level1;
                    lblTransOutDept.Text = oldDeptView.level2;

                    lblTransInGroup.Text = model.NewGroupName;
                    lblTransInCompany.Text = newDeptView.level1;
                    lblTransInDept.Text = newDeptView.level2;
                    lblTransInPosition.Text = model.NewPositionName;

                    //lblSalaryBase.Text = model.SalaryBase.ToString("#,##0.00");
                    //lblSalaryPromotion.Text = model.SalaryPromotion.ToString("#,##0.00");
                    lblTransInDate.Text = model.TransInDate.ToString("yyyy-MM-dd");
                   // txtTransOutDate.Text = model.TransOutDate.ToString("yyyy-MM-dd");
                    lblRemark.Text = model.Remark;
                }
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransAuditList.aspx"); 
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request["id"]);

            TransferInfo model = TransferManager.GetModel(id);

            if (DateTime.Parse(txtTransOutDate.Text) != model.TransInDate)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('转入转出日期请保持一致。');window.location.href='TransferAuditList.aspx';", true);
                return;
            }

            model.TransId = int.Parse( hidTransUserId.Value);
            model.TransName = txtTransUser.Text;

            ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.TransId);
            ESP.Framework.Entity.DepartmentPositionInfo position = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(positionModel.DepartmentPositionID);

            ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(model.TransId);

            model.TransCode = empModel.Code;
            model.OldPositionId = position.DepartmentPositionID;
            model.OldPositionName = position.DepartmentPositionName;

            model.TransOutDate = DateTime.Parse( txtTransOutDate.Text);
            model.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.TransferConfirm;


            //更新日志
            var logModel = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(CurrentUserID, model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
            logModel.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
            logModel.AuditDate = DateTime.Now;
            logModel.Requesition = txtRemark.Text;

            //ESP.Framework.Entity.OperationAuditManageInfo oldOperation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.OldGroupId);
            //添加下一级审批人日志
            ESP.Framework.Entity.UserInfo nextHr = ESP.Framework.BusinessLogic.UserManager.Get(model.TransId);
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
            oldHRAuditLogInfo.AuditorId = nextHr.UserID;
            oldHRAuditLogInfo.AuditorName = nextHr.FullNameCN;
            oldHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
            oldHRAuditLogInfo.FormId = model.Id;
            oldHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;
            oldHRAuditLogInfo.AuditLevel = model.Status;

            bool ret = TransferManager.HRConfirmIn(model, logModel, oldHRAuditLogInfo);
            if (ret)
            {
                //发邮件
                List<MailAddress> mailAddressList = new List<MailAddress>();
                mailAddressList.Add(new MailAddress(nextHr.Email));
                if (mailAddressList != null && mailAddressList.Count > 0)
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + model.Id;
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("员工转组 - " + CurrentUserName + "确认了"+model.TransName+"的转组申请", body, true, mailAddressList.ToArray());
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核成功！');window.location.href='TransferAuditList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核操作失败，请联系管理员。');window.location.href='TransferAuditList.aspx';", true);
            }
        }
    }
}