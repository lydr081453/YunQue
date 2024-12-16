using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Transfer
{
    public partial class TransferAudit : ESP.Web.UI.PageBase
    {
        private int transferId = 0;
        public int oldGroupId = 0;
        public bool IsProject = false;
        private TransferInfo transferModel = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            transferId = int.Parse(Request["id"]);
            if (transferId > 0)
            {
                transferModel = TransferManager.GetModel(transferId);


            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    if (int.TryParse(Request["id"], out transferId))
                    {
                        BindData(transferId);

                    }
                }
            }

        }

        public Dictionary<string, ESP.HumanResource.Entity.TransferDetailsInfo> DicTransfer
        {
            get
            {
                return this.ViewState["DicTransfer"] == null ? null :
                    (Dictionary<string, ESP.HumanResource.Entity.TransferDetailsInfo>)this.ViewState["DicTransfer"];
            }
            set
            {
                this.ViewState["DicTransfer"] = value;
            }
        }


        protected void CollectSelected()
        {
            for (int i = 0; i < this.gvDetailList.Rows.Count; i++)
            {
                int formId = 0;
                if (!int.TryParse(gvDetailList.DataKeys[i].Values["FormId"].ToString(), out formId))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                        "alert('系统出现错误，请与系统管理员联系');", true);
                    return;
                }
                string formType = gvDetailList.DataKeys[i].Values["FormType"].ToString();
                if (DicTransfer.ContainsKey(formId + "-" + formType))
                {
                    HiddenField hid = this.gvDetailList.Rows[i].FindControl("hidReceiverName") as HiddenField;
                    CheckBox chkTransfer = this.gvDetailList.Rows[i].FindControl("chkTransfer") as CheckBox;
                    if (chkTransfer.Checked)//转组
                    {
                        ESP.HumanResource.Entity.TransferDetailsInfo transferDetailInfo = DicTransfer[formId + "-" + formType];
                        transferDetailInfo.TransGroup = 1;
                    }
                    else
                    {
                        string val = hid.Value;
                        int receiverId = int.Parse(val);
                        if (receiverId > 0)
                        {
                            ESP.HumanResource.Entity.TransferDetailsInfo transferDetailInfo = DicTransfer[formId + "-" + formType];
                            transferDetailInfo.ReceiverId = receiverId;
                            transferDetailInfo.ReceiverName = ESP.Framework.BusinessLogic.UserManager.Get(receiverId).FullNameCN;
                            IList<ESP.Framework.Entity.EmployeePositionInfo> empPositionList = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(receiverId);
                            if (empPositionList != null && empPositionList.Count > 0)
                            {
                                transferDetailInfo.ReceiverDepartmentId = empPositionList[0].DepartmentID;
                                transferDetailInfo.ReceiverDepartmentName = empPositionList[0].DepartmentName;
                            }
                        }
                    }
                }
            }
        }

        private void BindData(int id)
        {
            //TransferInfo model = TransferManager.GetModel(id);
            if (transferModel != null)
            {
                ESP.Finance.Entity.DepartmentViewInfo oldDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(transferModel.OldGroupId);
                ESP.Finance.Entity.DepartmentViewInfo newDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(transferModel.NewGroupId);
                oldGroupId = transferModel.OldGroupId;
                hidGroupId.Value = transferModel.OldGroupId.ToString();
                lblTransOutGroup.Text = transferModel.OldGroupName;
                lblTransOutCompany.Text = oldDeptView.level1;
                lblTransOutDept.Text = oldDeptView.level2;
                lblTransInPosition.Text = transferModel.NewPositionName;
                lblTransInGroup.Text = transferModel.NewGroupName;
                lblTransInCompany.Text = newDeptView.level1;
                lblTransInDept.Text = newDeptView.level2;
                lblTransUser.Text = transferModel.TransName;

                lblTransInDate.Text = transferModel.TransOutDate.ToString("yyyy-MM-dd");
                lblTransOutDate.Text = transferModel.TransInDate.ToString("yyyy-MM-dd");
                lblRemark.Text = transferModel.Remark;
                //ReceiverConfirm
                if (transferModel.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.ReceiverConfirm || transferModel.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.HRConfirmed || transferModel.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmOut)
                {

                }
                else
                {
                    lblSalaryBase.Text = transferModel.SalaryBase.ToString("#,##0.00");
                    lblSalaryPromotion.Text = transferModel.SalaryPromotion.ToString("#,##0.00");
                }

                // 审批日志信息
                string strAuditLog = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetTransferLogInfos(transferModel.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm);
                lblLog.Text = strAuditLog;

                if (transferModel.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.HRConfirmed)
                {
                    GetTranferData();
                    this.btnCommit.Visible = true;
                    this.btnAudit.Visible = false;
                    btnTransferConfirm.Visible = false;
                }
                else if (transferModel.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.TransferConfirm)
                {
                    this.btnCommit.Visible = false;
                    this.btnAudit.Visible = false;
                    btnReject.Visible = false;
                    btnTransferConfirm.Visible = true;
                    tabData.Visible = false;
                }
                else
                {
                    GetTranferDataByTransferId(transferModel.Id);
                    this.btnCommit.Visible = false;
                    btnTransferConfirm.Visible = false;
                    this.btnAudit.Visible = true;
                }

            }

        }

        private void GetTranferData()
        {
            var list = TransferManager.GetTransferDataByUserId(transferModel.TransId);

            SetTransferData(list);

            gvDetailList.DataSource = list;
            gvDetailList.DataBind();
        }
        private void GetTranferDataByTransferId(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@transferid", SqlDbType.Int, 4) };
            parameters[0].Value = id;

            var list = TransferDetailsManager.GetList(" transferid=@transferid", parameters.ToList());
            pnlTop.Visible = false;
            pnlBottom.Visible = false;
            gvDetailList.DataSource = list;
            gvDetailList.DataBind();
        }

        protected void SetTransferData(List<ESP.HumanResource.Entity.TransferDetailsInfo> list)
        {
            if (list != null && list.Count > 0)
            {
                Dictionary<string, ESP.HumanResource.Entity.TransferDetailsInfo> detailList = new Dictionary<string, ESP.HumanResource.Entity.TransferDetailsInfo>();
                foreach (ESP.HumanResource.Entity.TransferDetailsInfo detail in list)
                {
                    if (!detailList.ContainsKey(detail.FormId + "-" + detail.FormType))
                    {
                        detailList.Add(detail.FormId + "-" + detail.FormType, detail);
                    }
                    if (detail.FormType == "项目号")
                    {
                        IsProject = true;
                    }
                }
                DicTransfer = detailList;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferAuditList.aspx");
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            CollectSelected();

            TransferInfo model = TransferManager.GetModel(transferId);

            //更新日志
            var logModel = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(CurrentUserID, model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
            logModel.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
            logModel.AuditDate = DateTime.Now;
            logModel.Requesition = txtRemark.Text;

            bool ret = TransferDetailsManager.TransferSettingReceiver(model, DicTransfer, logModel);

            if (ret)
            {
                var auditLogList = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);

                if (auditLogList != null)
                {
                    var auditLog = auditLogList.FirstOrDefault();
                    //发邮件
                    ESP.Framework.Entity.UserInfo nextAudit = ESP.Framework.BusinessLogic.UserManager.Get(auditLog.AuditorId);

                    List<MailAddress> mailAddressList = new List<MailAddress>();
                    mailAddressList.Add(new MailAddress(nextAudit.Email));
                    if (mailAddressList != null && mailAddressList.Count > 0)
                    {
                        string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + model.Id;
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        ESP.Mail.MailManager.Send("员工转组 - " + CurrentUserName + "确认了" + model.TransName + "的转组申请", body, true, mailAddressList.ToArray());
                    }
                }
                else
                {
                    if (model.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmOut)
                    {
                        List<MailAddress> mailAddressList = new List<MailAddress>();

                        var receiverList = ESP.HumanResource.BusinessLogic.TransferDetailsManager.GetReceiverInfo(model.Id);
                        if (receiverList.Count > 0)
                        {
                            foreach (var re in receiverList)
                            {
                                mailAddressList.Add(new MailAddress(re.Email));
                            }
                            if (mailAddressList != null && mailAddressList.Count > 0)
                            {
                                string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + model.Id;
                                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                ESP.Mail.MailManager.Send("员工转组 - " + model.TransName + "的转组申请需要您进行单据交接", body, true, mailAddressList.ToArray());
                            }
                        }
                    }
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核成功！');window.location.href='TransferAuditList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核操作失败，请联系管理员。');window.location.href='TransferAuditList.aspx';", true);
            }
        }

        protected void gvDetailList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                Label lab = e.Row.FindControl("labReceiverName") as Label;
                CheckBox chkTransfer = e.Row.FindControl("chkTransfer") as CheckBox;
                HiddenField hid = e.Row.FindControl("hidReceiverName") as HiddenField;
                ESP.HumanResource.Entity.TransferDetailsInfo detailsInfo = e.Row.DataItem as ESP.HumanResource.Entity.TransferDetailsInfo;
                lab.Text = detailsInfo.ReceiverName;
                hid.Value = detailsInfo.ReceiverId.ToString();
                if (detailsInfo.TransGroup == 1)
                {//transferModel.Status != (int)ESP.HumanResource.Common.Status.TransferStatus.Save && transferModel.Status != (int)ESP.HumanResource.Common.Status.TransferStatus.HRCommit && transferModel.Status != (int)ESP.HumanResource.Common.Status.TransferStatus.HRConfirmed 
                    chkTransfer.Checked = true;
                    lab.Text = "转组";
                }

                if (detailsInfo.FormType == "项目号" || detailsInfo.FormType == "支持方")
                {
                    chkTransfer.Enabled = true;
                }
                else
                {
                    chkTransfer.Enabled = false;
                }

                if (transferModel.Status != (int)ESP.HumanResource.Common.Status.TransferStatus.HRConfirmed)
                {
                    chkTransfer.Enabled = false;
                    e.Row.Cells[10].Text = "";
                }

            }
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            TransferInfo model = TransferManager.GetModel(transferId);

            //更新日志
            var logModel = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(CurrentUserID, model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
            logModel.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
            logModel.AuditDate = DateTime.Now;
            logModel.Requesition = txtRemark.Text;

            bool ret = TransferDetailsManager.TransferAudit(model, logModel);

            if (ret)
            {
                var auditLogList = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);

                if (auditLogList != null)
                {
                    var auditLog = auditLogList.FirstOrDefault();
                    //发邮件
                    ESP.Framework.Entity.UserInfo nextAudit = ESP.Framework.BusinessLogic.UserManager.Get(auditLog.AuditorId);

                    List<MailAddress> mailAddressList = new List<MailAddress>();
                    mailAddressList.Add(new MailAddress(nextAudit.Email));
                    if (mailAddressList != null && mailAddressList.Count > 0)
                    {
                        string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + model.Id;
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        ESP.Mail.MailManager.Send("员工转组 - " + CurrentUserName + "确认了" + model.TransName + "的转组申请", body, true, mailAddressList.ToArray());
                    }
                }
                else
                { //审批完成

                    List<MailAddress> mailAddressList = new List<MailAddress>();

                    string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + model.Id;
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("员工转组 - " + CurrentUserName + "确认了" + model.TransName + "的转组申请", body, true, mailAddressList.ToArray());

                }

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核成功！');window.location.href='TransferAuditList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核操作失败，请联系管理员。');window.location.href='TransferAuditList.aspx';", true);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            TransferInfo model = TransferManager.GetModel(transferId);

            //更新日志
            var logModel = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(CurrentUserID, model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
            logModel.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
            logModel.AuditDate = DateTime.Now;
            logModel.Requesition = txtRemark.Text;

            bool ret = TransferDetailsManager.TransferReject(model, logModel);

            if (ret)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批驳回成功！');window.location.href='TransferAuditList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批驳回失败，请联系管理员。');window.location.href='TransferAuditList.aspx';", true);
            }
        }

        protected void btnTransferConfirm_Click(object sender, EventArgs e)
        {
            TransferInfo model = TransferManager.GetModel(transferId);

            model.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.HRConfirmed;


            //更新日志
            var logModel = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(CurrentUserID, model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
            logModel.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
            logModel.AuditDate = DateTime.Now;
            logModel.Requesition = txtRemark.Text;

            ESP.Framework.Entity.OperationAuditManageInfo oldOperation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.OldGroupId);
            //添加下一级审批人日志
            ESP.Framework.Entity.UserInfo nextHr = ESP.Framework.BusinessLogic.UserManager.Get(oldOperation.DirectorId);
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
                    ESP.Mail.MailManager.Send("员工转组 - " + CurrentUserName + "确认了" + model.TransName + "的转组申请", body, true, mailAddressList.ToArray());
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