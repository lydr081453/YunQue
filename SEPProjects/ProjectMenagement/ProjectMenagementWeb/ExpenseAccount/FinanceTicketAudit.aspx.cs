using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using System.Data.SqlClient;
using System.Data;

namespace FinanceWeb.ExpenseAccount
{
    public partial class FinanceTicketAudit : ESP.Web.UI.PageBase
    {
        private int batchid = 0;
        ESP.Finance.Entity.PNBatchInfo batchModel = null;
        //string HuNanBranch = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            batchid = int.Parse(Request["BatchId"]);
            batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            // HuNanBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditHuNanBranch"];
            if (!IsPostBack)
            {
                bindInfo();
                BindList();
                if (batchModel.Status == 109)
                {
                    trNext.Visible = true;
                }
                else
                {
                    trNext.Visible = false;
                }
            }

        }

        private void bindInfo()
        {
            this.lblBatchId.Text = batchModel.BatchID.ToString();
            labPurchaeBatchCode.Text = batchModel.PurchaseBatchCode;

            lblTotal.Text = batchModel.Amounts.Value.ToString("#,##0.00");
            lblReturn.Text = batchModel.TicketReturnPoint.ToString("#,##0.00");
            lblFactFee.Text = (batchModel.Amounts.Value - batchModel.TicketReturnPoint).ToString("#,##0.00");

            lblBranch.Text = batchModel.BranchCode;
            labCreateUser.Text = new ESP.Compatible.Employee(batchModel.CreatorID.Value).Name;
            txtBatchCode.Text = batchModel.BatchCode;
            List<SqlParameter> paramlist = new List<SqlParameter>();

            string term = " IsBatch=@IsBatch";
            SqlParameter p = new SqlParameter("@IsBatch", SqlDbType.Bit);
            p.Value = true;
            paramlist.Add(p);
            IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(term, paramlist);
            ddlPaymentType.DataSource = paylist;
            ddlPaymentType.DataTextField = "PaymentTypeName";
            ddlPaymentType.DataValueField = "PaymentTypeID";
            ddlPaymentType.DataBind();
            ddlPaymentType.Items.Insert(0, new ListItem("请选择", "0"));

            paramlist.Clear();
            SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@branchcode", SqlDbType.NVarChar, 50);
            p1.Value = batchModel.BranchCode;
            paramlist.Add(p1);
            IList<BankInfo> bankist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchcode=@branchcode ", paramlist);
            ddlBank.DataSource = bankist;
            ddlBank.DataTextField = "BankName";
            ddlBank.DataValueField = "BankID";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("请选择", "0"));
            if (batchModel.BankID != null)
                ddlBank.SelectedValue = batchModel.BankID.Value.ToString();
            if (batchModel.PaymentTypeID != null)
                ddlPaymentType.SelectedValue = batchModel.PaymentTypeID.Value.ToString();
            this.labAuditLog.Text = GetAuditLog(batchid);
        }

        protected void ddlBank_SelectedIndexChangeed(object sender, EventArgs e)
        {
            int bankid = Convert.ToInt32(this.ddlBank.SelectedItem.Value);
            if (bankid == 0)
            {
                this.lblAccountName.Text = "";
                this.lblAccount.Text = "";
                this.lblBankAddress.Text = "";
            }
            else
            {
                ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankid);
                this.lblAccountName.Text = bankModel.BankAccountName;
                this.lblAccount.Text = bankModel.BankAccount;
                this.lblBankAddress.Text = bankModel.Address;
            }
        }

        private void BindList()
        {

            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetTicketBatch(batchid);
            this.gvG.DataSource = returnlist;
            this.gvG.DataBind();


        }

        private string GetAuditLog(int Rid)
        {
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetBatchList(Rid);
            string loginfo = string.Empty;
            foreach (ESP.Finance.Entity.AuditLogInfo model in histList)
            {
                string austatus = string.Empty;
                if (model.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (model.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }
                string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

            }
            return loginfo;
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
                ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
                if (returnModel != null)
                    e.Row.Cells[1].Text = "<a target='_blank' href=\"/ExpenseAccount/Display.aspx?id=" + returnModel.ReturnID.ToString() + "\">" + returnModel.ReturnCode + "</a>";
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void gdG_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        protected void btnYes_Click(object sender, EventArgs e)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchid);
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode);
            ESP.Compatible.Employee financeFirst = null;

            if (!string.IsNullOrEmpty(hidNextAuditor.Value))
            {
                financeFirst = new ESP.Compatible.Employee(int.Parse(hidNextAuditor.Value));
            }
            if (financeFirst != null)
            {
                batchModel.PaymentUserID = int.Parse(financeFirst.SysID);
                batchModel.PaymentEmployeeName = financeFirst.Name;
                batchModel.PaymentCode = financeFirst.ID;
                batchModel.PaymentUserName = financeFirst.ITCode;
                batchModel.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
                batchModel.PaymentType = ddlPaymentType.SelectedItem.Text;
                batchModel.BatchCode = txtBatchCode.Text;
                batchModel.IsInvoice = int.Parse(this.radioInvoice.SelectedValue);
                batchModel.BankID = int.Parse(ddlBank.SelectedValue);
                batchModel.BankName = ddlBank.SelectedItem.Text;
                batchModel.BankAccount = this.lblAccount.Text;
                batchModel.BankAccountName = this.lblAccountName.Text;
                batchModel.BankAddress = this.lblBankAddress.Text;
            }
            batchModel.Status = GetStatus();
            ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);
            foreach (ESP.Finance.Entity.ReturnInfo m in returnlist)
            {
                m.ReturnStatus = batchModel.Status;
                m.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
                m.PaymentTypeName = ddlPaymentType.SelectedItem.Text;
                m.IsInvoice = int.Parse(this.radioInvoice.SelectedValue);
                m.BankID = int.Parse(ddlBank.SelectedValue);
                m.BankName = ddlBank.SelectedItem.Text;
                m.BankAccount = this.lblAccount.Text;
                m.BankAccountName = this.lblAccountName.Text;
                m.BankAddress = this.lblBankAddress.Text;
                ESP.Finance.BusinessLogic.ReturnManager.Update(m);

                ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                log.AuditeDate = DateTime.Now;
                log.AuditorEmployeeName = CurrentUser.Name;
                log.AuditorUserID = int.Parse(CurrentUser.SysID);
                log.AuditorUserCode = CurrentUser.ID;
                log.AuditorUserName = CurrentUser.ITCode;
                log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                log.ExpenseAuditID = m.ReturnID;
                log.Suggestion = this.txtRemark.Text;
                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
            }

            ESP.Finance.Entity.AuditLogInfo returnlog = new ESP.Finance.Entity.AuditLogInfo();
            returnlog.AuditDate = DateTime.Now;
            returnlog.AuditorEmployeeName = CurrentUser.Name;
            returnlog.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
            returnlog.AuditorUserCode = CurrentUser.ID;
            returnlog.AuditorUserName = CurrentUser.ITCode;
            returnlog.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
            returnlog.FormID = batchModel.BatchID;
            returnlog.FormType = (int)ESP.Finance.Utility.FormType.PNBatch;
            returnlog.Suggestion = this.txtRemark.Text;
            ESP.Finance.BusinessLogic.AuditLogManager.Add(returnlog);

            Response.Redirect("FinanceTicketList.aspx");
        }

        private int GetStatus()
        {
            int status = batchModel.Status.Value;
            switch (batchModel.Status)
            {
                case 109:
                    status = (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel1;
                    break;
                case 110:
                    status = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    break;
                case 120:
                    status = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    break;
                case 130:
                    status = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    break;
            }
            return status;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinanceTicketList.aspx");
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetPayments()
        {
            List<List<string>> retlists = new List<List<string>>();
            string term = " IsBatch=@IsBatch";
            List<SqlParameter> paramlist = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@IsBatch", SqlDbType.Bit);
            p.Value = true;
            paramlist.Add(p);
            IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(term, paramlist);
            List<string> first = new List<string>();
            first.Add("-1");
            first.Add("请选择..");
            retlists.Add(first);
            foreach (PaymentTypeInfo item in paylist)
            {
                List<string> i = new List<string>();
                i.Add(item.PaymentTypeID.ToString());
                i.Add(item.PaymentTypeName);
                retlists.Add(i);
            }

            return retlists;
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetBanks(string branchcode)
        {
            List<List<string>> retlists = new List<List<string>>();
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@branchcode", SqlDbType.NVarChar, 50);
            p1.Value = branchcode;
            paramlist.Add(p1);
            IList<BankInfo> paylist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchcode=@branchcode ", paramlist);
            List<string> first = new List<string>();
            first.Add("-1");
            first.Add("请选择..");
            retlists.Add(first);
            foreach (BankInfo item in paylist)
            {
                List<string> i = new List<string>();
                i.Add(item.BankID.ToString());
                i.Add(item.BankName);
                retlists.Add(i);
            }

            return retlists;
        }

        [AjaxPro.AjaxMethod]
        public static List<string> GetBankModel(int bankID)
        {
            List<string> list = new List<string>();
            ESP.Finance.Entity.BankInfo bankmodel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankID);
            list.Add(bankmodel.BankID.ToString());
            list.Add(bankmodel.BankName);
            list.Add(bankmodel.BankAccount);
            list.Add(bankmodel.BankAccountName);
            list.Add(bankmodel.Address);

            return list;
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchid);
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode);

            batchModel.Status = (int)ESP.Finance.Utility.PaymentStatus.Save;
            ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);
            foreach (ESP.Finance.Entity.ReturnInfo m in returnlist)
            {
                m.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm;

                ESP.Finance.BusinessLogic.ReturnManager.Update(m);

                ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                log.AuditeDate = DateTime.Now;
                log.AuditorEmployeeName = CurrentUser.Name;
                log.AuditorUserID = int.Parse(CurrentUser.SysID);
                log.AuditorUserCode = CurrentUser.ID;
                log.AuditorUserName = CurrentUser.ITCode;
                log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Reject;
                log.ExpenseAuditID = m.ReturnID;
                log.Suggestion = this.txtRemark.Text;
                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
            }

            ESP.Finance.Entity.AuditLogInfo returnlog = new ESP.Finance.Entity.AuditLogInfo();
            returnlog.AuditDate = DateTime.Now;
            returnlog.AuditorEmployeeName = CurrentUser.Name;
            returnlog.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
            returnlog.AuditorUserCode = CurrentUser.ID;
            returnlog.AuditorUserName = CurrentUser.ITCode;
            returnlog.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
            returnlog.FormID = batchModel.BatchID;
            returnlog.FormType = (int)ESP.Finance.Utility.FormType.PNBatch;
            returnlog.Suggestion = this.txtRemark.Text;
            ESP.Finance.BusinessLogic.AuditLogManager.Add(returnlog);

            Response.Redirect("FinanceTicketList.aspx");
        }
    }
}
