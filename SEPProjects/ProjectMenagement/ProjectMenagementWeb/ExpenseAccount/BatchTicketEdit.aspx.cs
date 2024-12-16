using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ExpenseAccount
{
    public partial class BatchTicketEdit : ESP.Web.UI.PageBase
    {
        private int batchid=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            batchid = int.Parse(Request["BatchId"]);
            if (!IsPostBack)
            {
                BindList();

            }

        }

        private void BindList()
        {
            
            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            labPurchaeBatchCode.Text = batchModel.PurchaseBatchCode;
            labTotal.Text = batchModel.Amounts.Value.ToString("#,##0.00");
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetTicketBatch(batchid);
            this.gvG.DataSource = returnlist;
            this.gvG.DataBind();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }

           
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
            //}
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int returnid = int.Parse(e.CommandArgument.ToString());
            
            if (e.CommandName == "Delete")
            {
                ESP.Finance.BusinessLogic.PNBatchRelationManager.Delete(batchid, returnid);
                IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchid);
                var total= returnlist.Sum(x => x.PreFee);
                ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                batchModel.Amounts = total;
                ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);

                BindList();
            }
        }
        protected void gdG_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        protected void btnYes_Click(object sender, EventArgs e)
        {
            decimal retamount = 0;
            try
            {
                retamount = decimal.Parse(txtRetAmount.Text);
            }
            catch
            {
                retamount = 0;
            }
            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchid);
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode);
            ESP.Compatible.Employee financeFirst = new ESP.Compatible.Employee(branchModel.FirstFinanceID);

            batchModel.TicketReturnPoint = retamount;
            batchModel.PaymentUserID = int.Parse(financeFirst.SysID);
            batchModel.PaymentEmployeeName = financeFirst.Name;
            batchModel.PaymentCode = financeFirst.ID;
            batchModel.PaymentUserName = financeFirst.ITCode;
            batchModel.Status = (int)ESP.Finance.Utility.PaymentStatus.Ticket_Received;
            ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);
            foreach (ESP.Finance.Entity.ReturnInfo m in returnlist)
            {
                m.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Ticket_Received;
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
            
            Response.Redirect("/Ticket/BatchList.aspx");
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            ESP.Finance.BusinessLogic.PNBatchManager.Delete(batchid);
            Response.Redirect("/Ticket/BatchList.aspx");
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Ticket/BatchList.aspx");
        }
    }
}
