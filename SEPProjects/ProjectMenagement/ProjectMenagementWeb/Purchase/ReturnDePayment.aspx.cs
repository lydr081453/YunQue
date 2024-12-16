using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

namespace FinanceWeb.Purchase
{
    public partial class ReturnDePayment : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private string GetAuditLog(int Rid)
        {
            ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Rid);
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
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
        private void BindInfo()
        {
            int returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            if (returnModel != null && returnModel.PRID != null)
                TopMessage.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            lblCreateTime.Text = returnModel.RequestDate.Value.ToString();
            lblApplicant.Text = returnModel.RequestEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
            lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblPayRemark.Text = returnModel.ReturnContent;
            lblPRNo.Text = returnModel.PRNo;
            lblProjectCode.Text = returnModel.ProjectCode;
            lblReturnCode.Text = returnModel.ReturnCode;
            lblPayCode.Text = returnModel.PaymentTypeCode;
            lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value,0,returnModel.IsDiscount);
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
                lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                lblPRNo.Text = returnModel.PRNo;
            ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
            //从重汇列表获取供应商信息
            IList<ESP.Finance.Entity.BankCancelInfo> cancelList = ESP.Finance.BusinessLogic.BankCancelManager.GetList(" ReturnID=" + returnModel.ReturnID.ToString() + " and (ordertype is null or ordertype=1 )");
            if (cancelList != null && cancelList.Count > 0)
            {
                this.lblSupplierName.Text = cancelList[cancelList.Count - 1].OldBankAccountName;
                this.lblSupplierBank.Text = cancelList[cancelList.Count - 1].NewBankName;
                this.lblSupplierAccount.Text = cancelList[cancelList.Count - 1].NewBankAccount;
            }
            else if (vmodel != null)
            {
                this.lblSupplierName.Text = vmodel.Account_name;
                this.lblSupplierBank.Text = vmodel.Account_bank;
                this.lblSupplierAccount.Text = vmodel.Account_number;
            }
            lblFactFee.Text = returnModel.FactFee == null ? "" : returnModel.FactFee.Value.ToString("#,##0.00");
            radioInvoice.SelectedValue = returnModel.IsInvoice == null ? "" : returnModel.IsInvoice.Value.ToString();
            lblPreDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblBankName.Text = returnModel.BankName;
            lblAccountName.Text = returnModel.BankAccountName;
            lblAccount.Text = returnModel.BankAccount;
            lblBankAddress.Text = returnModel.BankAddress;
            lblPaymentType.Text = returnModel.PaymentTypeName;
            this.lblLog.Text = this.GetAuditLog(returnModel.ReturnID);
            if (returnModel.DepartmentID != null && returnModel.DepartmentID.Value != 0)
            {
                ESP.Compatible.Department dept = ESP.Compatible.DepartmentManager.GetDepartmentByPK(returnModel.DepartmentID.Value);
                labDepartment.Text = dept.DepartmentName;
            }
            bindData(returnModel);
        }

        private void bindData(ReturnInfo returnModel)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string strWhere = " and a.Gid = " + returnModel.PRID.Value.ToString();
            //strWhere += " and a.id not in(select recipientId from T_PeriodRecipient) and a.receivePrice = 0 and (isconfirm is null or isconfirm = 0 or isconfirm = " + ESP.Purchase.Common.State.recipentConfirm_Supplier + " or (isconfirm = " + ESP.Purchase.Common.State.recipentConfirm_Emp2 + " and (b.appendReceiver is null or b.appendReceiver=0)) or (isconfirm = " + ESP.Purchase.Common.State.recipentConfirm_Emp2 + "))";
            strWhere += " and a.receivePrice = 0 and (isconfirm is null or isconfirm = 0 or isconfirm = " + ESP.Purchase.Common.State.recipentConfirm_Supplier + " or (isconfirm = " + ESP.Purchase.Common.State.recipentConfirm_Emp2 + " and (b.appendReceiver is null or b.appendReceiver=0)) or (isconfirm = " + ESP.Purchase.Common.State.recipentConfirm_Emp2 + "))";

            DataSet ds = RecipientManager.GetRecipientList(strWhere, parms);
            gvRecipient.DataSource = ds;
            gvRecipient.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CollectSelected();
            if (this.SelectedItems.Count == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择相关的收货单!');", true);
                return;
            }
            string RecipientIds=string.Empty;
            decimal totalAmount = 0;
            for(int i=0;i<this.SelectedItems.Count;i++)
            {
                 RecipientIds += SelectedItems[i].ToString() + ",";
                 totalAmount += ESP.Purchase.BusinessLogic.RecipientManager.GetModel(Convert.ToInt32(SelectedItems[i].ToString())).RecipientAmount;
            }
            RecipientIds=RecipientIds.TrimEnd(',');
                int returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            ESP.Finance.Entity.ReturnInfo OldModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            ESP.Finance.Entity.ReturnInfo NewModel = OldModel;
            NewModel.ReturnID = 0;
            NewModel.ReturnCode = string.Empty;
            NewModel.RecipientIds = RecipientIds;
            NewModel.PreFee = totalAmount;
            NewModel.FactFee = 0;
            NewModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff;
            NewModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.ConfirmReceiving;
            NewModel.ParentID = returnId;

            int ret=ESP.Finance.BusinessLogic.ReturnManager.CreateReturnDePayment(NewModel,returnId);
            if (ret > 0)
            {//WaitReceivingEdit.aspx?id=5945
                Response.Redirect("/ExpenseAccount/WaitReceivingEdit.aspx?id=" + ret.ToString());
            }
            else if (ret == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('该项目号已经关闭，冲销金额不能大于原借款金额!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('提交时出现未知错误!');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Edit/PrDePayment.aspx");
        }

        protected void gvRecipient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
        protected void CollectSelected()
        {
            if (this.SelectedItems == null)
                SelectedItems = new ArrayList();
            else
                SelectedItems.Clear();
            string MID = string.Empty;

            for (int i = 0; i < this.gvRecipient.Rows.Count; i++)
            {
                MID = gvRecipient.Rows[i].Cells[1].Text.Trim();
                CheckBox cb = this.gvRecipient.Rows[i].FindControl("chkItem") as CheckBox;
                if (SelectedItems.Contains(MID) && !cb.Checked)
                    SelectedItems.Remove(MID);
                if (!SelectedItems.Contains(MID) && cb.Checked)
                    SelectedItems.Add(MID);
            }
            this.SelectedItems = SelectedItems;
        }

        protected ArrayList SelectedItems
        {
            get
            {
                return (ViewState["mySelectedItems"] != null) ? (ArrayList)ViewState["mySelectedItems"] : null;
            }
            set
            {
                ViewState["mySelectedItems"] = value;
            }
        }

    }
}
