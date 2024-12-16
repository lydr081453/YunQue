using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SEPAdmin.HR.Transfer
{
    public partial class TransferDone : ESP.Web.UI.PageBase
    {
        private int transferId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                transferId = int.Parse(Request["id"]);
            }
            if (!IsPostBack)
            {
                BindData(transferId);
                GetTranferData(transferId);
            }
        }

        private void BindData(int id)
        {
            TransferInfo model = TransferManager.GetModel(id);
            if (model != null)
            {
                ESP.Finance.Entity.DepartmentViewInfo oldDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.OldGroupId);
                ESP.Finance.Entity.DepartmentViewInfo newDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.NewGroupId);

                hidGroupId.Value = model.OldGroupId.ToString();
                lblTransOutGroup.Text = model.OldGroupName;
                lblTransOutCompany.Text = oldDeptView.level1;
                lblTransOutDept.Text = oldDeptView.level2;
                lblTransInPosition.Text = model.NewPositionName;
                lblTransInGroup.Text = model.NewGroupName;
                lblTransInCompany.Text = newDeptView.level1;
                lblTransInDept.Text = newDeptView.level2;
                lblTransUser.Text = model.TransName;
                lblSalaryBase.Text = model.SalaryBase.ToString("#,##0.00");
                lblSalaryPromotion.Text = model.SalaryPromotion.ToString("#,##0.00");
                lblTransInDate.Text = model.TransOutDate.ToString("yyyy-MM-dd");
                lblTransOutDate.Text = model.TransInDate.ToString("yyyy-MM-dd");
                lblRemark.Text = model.Remark;

                // 审批日志信息
                string strAuditLog = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetTransferLogInfos(model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm);
                lblLog.Text = strAuditLog;
            }

        }

        private void GetTranferData(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@transferid", SqlDbType.Int,4)
				};
            parameters[0].Value = id;

            var list = TransferDetailsManager.GetList(" transferid=@transferid", parameters.ToList());

            gvDetailList.DataSource = list;
            gvDetailList.DataBind();
        }


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferMgt.aspx");
        }
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            //人员转组涉及部门、职务、日志
            //单据转组涉及项目号、PR、PN
            //单据转交接人，单据权限

            TransferInfo model = TransferManager.GetModel(transferId);

            model.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.Complete;

            //更新日志
            HRAuditLogInfo logModel = new HRAuditLogInfo();
            logModel.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
            logModel.AuditDate = DateTime.Now;
            logModel.Requesition = "转组确认";
            logModel.AuditLevel = model.Status;
            logModel.AuditorId = UserID;
            logModel.AuditorName = CurrentUserName;
            logModel.FormId = model.Id;
            logModel.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;

            Dictionary<int, string> mailInfo = new Dictionary<int, string>();

            bool ret = TransferDetailsManager.TransferHRDone(model, logModel,out mailInfo);

            if (mailInfo != null && mailInfo.Count > 0)
            {
                foreach (KeyValuePair<int, string> kvp in mailInfo)
                {
                    List<System.Net.Mail.MailAddress> recipients = new List<System.Net.Mail.MailAddress>();
                    List<string> userIds = new List<string>();
                    if (kvp.Key == 19)
                    {
                        userIds = ConfigurationManager.AppSettings["BeijingProjectAccounterChange"].Split(new char[] { ',' }).ToList<string>();
                    }
                    else if (kvp.Key == 18)
                    {
                        userIds = ConfigurationManager.AppSettings["GuangzhouProjectAccounterChange"].Split(new char[] { ',' }).ToList<string>();
                    }
                    else if (kvp.Key == 17)
                    {
                        userIds = ConfigurationManager.AppSettings["ShanghaiProjectAccounterChange"].Split(new char[] { ',' }).ToList<string>();
                    }
                    else
                    {
                        userIds = ConfigurationManager.AppSettings["BeijingProjectAccounterChange"].Split(new char[] { ',' }).ToList<string>();
                    }
                    foreach (string userid in userIds)
                    {
                        ESP.Framework.Entity.UserInfo empInfo = ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(userid));
                        if (empInfo != null && !string.IsNullOrEmpty(empInfo.Email))
                        {
                            recipients.Add(new System.Net.Mail.MailAddress(empInfo.Email));
                        }
                    }
                    ESP.Mail.MailManager.Send("转组交接", kvp.Value, false, recipients.ToArray());
                }
            }

            if (ret)
            {
                var oldOperation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.OldGroupId);
                var newOperation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.NewGroupId);

                List<MailAddress> mailAddressList = new List<MailAddress>();
                mailAddressList.Add(new MailAddress((ESP.Framework.BusinessLogic.UserManager.Get(oldOperation.DirectorId)).Email));
                mailAddressList.Add(new MailAddress((ESP.Framework.BusinessLogic.UserManager.Get(oldOperation.HRId)).Email));
                if(oldOperation.DirectorId!=oldOperation.ManagerId)
                    mailAddressList.Add(new MailAddress((ESP.Framework.BusinessLogic.UserManager.Get(oldOperation.ManagerId)).Email));
                if(oldOperation.DirectorId!=newOperation.DirectorId)
                    mailAddressList.Add(new MailAddress((ESP.Framework.BusinessLogic.UserManager.Get(newOperation.DirectorId)).Email));
                if(oldOperation.ManagerId!=newOperation.ManagerId)
                    mailAddressList.Add(new MailAddress((ESP.Framework.BusinessLogic.UserManager.Get(newOperation.ManagerId)).Email));
                if (oldOperation.HRId != newOperation.HRId)
                    mailAddressList.Add(new MailAddress((ESP.Framework.BusinessLogic.UserManager.Get(newOperation.HRId)).Email));

                if (mailAddressList != null && mailAddressList.Count > 0)
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + model.Id;
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("员工转组 - " + model.TransName + "的转组申请已经完成", body, true, mailAddressList.ToArray());
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核成功！');window.location.href='TransferMgt.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核操作失败，请联系管理员。');window.location.href='TransferMgt.aspx';", true);
            }

        }
        protected void gvDetailList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                Label lab = e.Row.FindControl("labReceiverName") as Label;
                CheckBox chkTransfer = e.Row.FindControl("chkTransfer") as CheckBox;
                ESP.HumanResource.Entity.TransferDetailsInfo detailsInfo = e.Row.DataItem as ESP.HumanResource.Entity.TransferDetailsInfo;
                lab.Text = detailsInfo.ReceiverName;
                if (detailsInfo.TransGroup == 1)
                {
                    chkTransfer.Checked = true;
                    lab.Text = "转组";
                }

            }
        }
    }
}