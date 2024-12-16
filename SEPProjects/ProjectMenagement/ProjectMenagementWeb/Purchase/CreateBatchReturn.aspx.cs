using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

namespace FinanceWeb.Purchase
{
    public partial class CreateBatchReturn : ESP.Web.UI.PageBase
    {

        int batchID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                bindInfo();
            }
        }

        private void bindInfo()
        {
            string term = " IsBatch=@IsBatch";
            List<SqlParameter> paramlist = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@IsBatch", SqlDbType.Bit);
            p.Value = true;
            paramlist.Add(p);
            IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(term, paramlist);
            ddlPaymentType.DataSource = paylist;
            ddlPaymentType.DataTextField = "PaymentTypeName";
            ddlPaymentType.DataValueField = "PaymentTypeID";
            ddlPaymentType.DataBind();
            ddlPaymentType.Items.Insert(0, new ListItem("请选择", "0"));

            IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
            ddlBranch.DataSource = blist;
            ddlBranch.DataTextField = "BranchCode";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("请选择", "0"));
        }

        protected void CollectSelectedPN()
        {
            if (this.PNSelectedItems == null)
                PNSelectedItems = new ArrayList();
            else
                PNSelectedItems.Clear();
            string MID = string.Empty;
            for (int i = 0; i < this.gvG.Rows.Count; i++)
            {
                MID = gvG.Rows[i].Cells[1].Text.Trim();
                CheckBox cb = this.gvG.Rows[i].FindControl("chkReturn") as CheckBox;
                if (PNSelectedItems.Contains(MID) && !cb.Checked)
                    PNSelectedItems.Remove(MID);
                if (!PNSelectedItems.Contains(MID) && cb.Checked)
                    PNSelectedItems.Add(MID);
            }
            this.PNSelectedItems = PNSelectedItems;
        }

        protected ArrayList PNSelectedItems
        {
            get
            {
                return (ViewState["PNSelectedItems"] != null) ? (ArrayList)ViewState["PNSelectedItems"] : null;
            }
            set
            {
                ViewState["PNSelectedItems"] = value;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PNListBind();
        }

        private void PNListBind()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            string DelegateUsers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');
            if (!string.IsNullOrEmpty(DelegateUsers))
                term = " (ReturnStatus=@status1) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
            else
                term = " (ReturnStatus=@status1) AND PaymentUserID=@sysID ";
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.MajorAudit;
            paramlist.Add(p1);
            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
            if (!string.IsNullOrEmpty(term))
            {
                if (txtSupplier.Text.Trim() != string.Empty)
                {
                    term += " and SupplierName  like '%'+@supplierName+'%'";
                    SqlParameter supp = new SqlParameter("@supplierName", SqlDbType.NVarChar, 50);
                    supp.SqlValue = txtSupplier.Text.Trim();
                    paramlist.Add(supp);
                }
                if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
                {
                    term += " and returncode  like '%'+@key+'%'";
                    SqlParameter key = new SqlParameter("@key", SqlDbType.NVarChar, 50);
                    key.SqlValue = txtKey.Text.Trim();
                    paramlist.Add(key);
                }
                term += " and returnid not in(select returnid from f_pnbatchrelation)";
                if (ddlBranch.SelectedValue != "0")
                {
                    term += " and projectcode like ''+@BranchCode+'%' ";
                    System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                    pBrach.SqlValue = ddlBranch.SelectedItem.Text; //batchModel.BranchCode;
                    paramlist.Add(pBrach);
                }
                if (ddlPaymentType.SelectedValue != "0")
                {
                    term += " and PaymentTypeID=@PaymentTypeID";
                    System.Data.SqlClient.SqlParameter pPaymentTypeID = new System.Data.SqlClient.SqlParameter("@PaymentTypeID", System.Data.SqlDbType.Int, 4);
                    pPaymentTypeID.SqlValue = ddlPaymentType.SelectedValue; //batchModel.PaymentTypeID.Value;
                    paramlist.Add(pPaymentTypeID);
                }
                if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                {
                    term += " and (RequestDate between @BeginDate and @EndDate)";
                    System.Data.SqlClient.SqlParameter pbegin = new System.Data.SqlClient.SqlParameter("@BeginDate", System.Data.SqlDbType.DateTime, 8);
                    pbegin.SqlValue = this.txtBeginDate.Text.Trim() + "00:00:00";
                    System.Data.SqlClient.SqlParameter pend = new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.DateTime, 8);
                    pend.SqlValue = this.txtEndDate.Text.Trim() + "23:59:59";
                    paramlist.Add(pbegin);
                    paramlist.Add(pend);
                }
                IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
                this.gvG.DataSource = returnList;
                this.gvG.DataBind();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            CollectSelectedPN();
            try
            {
                ESP.Finance.Entity.PNBatchInfo batchModel = new PNBatchInfo();
                batchModel.CreateDate = DateTime.Now;
                batchModel.CreatorID = CurrentUserID;
                batchModel.BatchType = 1;
                batchModel.BatchCode = this.txtBatchCode.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtReturnPreDate.Text.Trim()))
                    batchModel.PaymentDate = Convert.ToDateTime(this.txtReturnPreDate.Text.Trim());
                if (this.radioInvoice.SelectedIndex >= 0)
                    batchModel.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);
                if (!string.IsNullOrEmpty(this.ddlBank.SelectedItem.Value))
                {
                    ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(Convert.ToInt32(this.ddlBank.SelectedItem.Value));
                    batchModel.BankID = bankModel.BankID;
                    batchModel.BankName = bankModel.BankName;
                    batchModel.BankAddress = bankModel.Address;
                    batchModel.BankAccount = bankModel.BankAccount;
                    batchModel.BankAccountName = bankModel.BankAccountName;
                    batchModel.DBCode = bankModel.DBCode;
                    batchModel.DBManager = bankModel.DBManager;
                    batchModel.ExchangeNo = bankModel.ExchangeNo;
                    batchModel.RequestPhone = bankModel.RequestPhone;
                }
                batchModel.BranchID = Convert.ToInt32(this.ddlBranch.SelectedItem.Value);
                batchModel.BranchCode = this.ddlBranch.SelectedItem.Text;
                batchModel.PaymentTypeID = Convert.ToInt32(this.ddlPaymentType.SelectedItem.Value);
                batchModel.PaymentType = this.ddlPaymentType.SelectedItem.Text;
                batchModel.PurchaseBatchCode = ESP.Finance.BusinessLogic.PNBatchManager.CreatePurchaseBatchCode();

                DateTime PaymentDate = Convert.ToDateTime(this.txtReturnPreDate.Text.Trim());
                int ret = ESP.Finance.BusinessLogic.PNBatchManager.Add(batchModel, PNSelectedItems, CurrentUser);
                if (ret > 0)
                {
                    Response.Redirect("BatchReturnEdit.aspx?" + RequestName.BatchID + "=" + ret.ToString());
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('创建失败!');'", true);

                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }

        }

        private void setModelInfo(PNBatchInfo batchModel)
        {
            if (this.radioInvoice.SelectedIndex >= 0)
                batchModel.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);

            if ((batchModel.Status == (int)PaymentStatus.FinanceLevel2 && batchModel.Total <= 10000) || batchModel.Status == (int)PaymentStatus.FinanceLevel3)
            {
                batchModel.Status = (int)PaymentStatus.FinanceComplete;
            }
            else if (batchModel.Status == (int)PaymentStatus.FinanceLevel2 && batchModel.Total > 10000)
            {
                batchModel.Status = (int)PaymentStatus.FinanceLevel3;
            }
            else if (batchModel.Status == (int)PaymentStatus.FinanceLevel1)
            {
                batchModel.Status = (int)PaymentStatus.FinanceLevel2;
            }
            else if (batchModel.Status == (int)PaymentStatus.MajorAudit)
            {
                batchModel.Status = (int)PaymentStatus.FinanceLevel1;
            }
            batchModel.BatchCode = this.txtBatchCode.Text.Trim();
        }

        private bool ValidAudited()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            string DelegateUsers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');
            if (!string.IsNullOrEmpty(DelegateUsers))
                term = " (Status=@status1 or  Status=@status2 or Status=@status3 or Status=@status4 or Status=@status5) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + ")) and BatchType=1 ";
            else
                term = " (Status=@status1 or  Status=@status2 or Status=@status3 or Status=@status4 or Status=@status5) AND PaymentUserID=@sysID and BatchType=1 ";
            SqlParameter p3 = new SqlParameter("@status3", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = (int)PaymentStatus.MajorAudit;
            paramlist.Add(p3);
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.FinanceLevel1;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.FinanceLevel2;
            paramlist.Add(p2);
            SqlParameter p4 = new SqlParameter("@status4", System.Data.SqlDbType.Int, 4);
            p4.SqlValue = (int)PaymentStatus.FinanceLevel3;
            paramlist.Add(p4);
            SqlParameter p5 = new SqlParameter("@status5", System.Data.SqlDbType.Int, 4);
            p5.SqlValue = (int)PaymentStatus.WaitReceiving;
            paramlist.Add(p5);

            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
            IList<PNBatchInfo> Batchlist = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramlist);
            foreach (PNBatchInfo pro in Batchlist)
            {
                if (pro.BatchID.ToString() == Request[ESP.Finance.Utility.RequestName.BatchID])
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("BatchReturnList.aspx");
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int reutrnID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.Entity.ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(reutrnID);
                ESP.Finance.BusinessLogic.PNBatchRelationManager.DeleteByReturnID(Convert.ToInt32(Request[RequestName.BatchID]),model);
            }

        }

        protected void ddlBank_SelectedIndexChangeed(object sender, EventArgs e)
        {
            int bankid = Convert.ToInt32(this.ddlBank.SelectedItem.Value);
            ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankid);
            this.lblAccountName.Text = bankModel.BankAccountName;
            this.lblAccount.Text = bankModel.BankAccount;
            this.lblBankAddress.Text = bankModel.Address;

        }
        protected void ddlBranch_SelectedIndexChangeed(object sender, EventArgs e)
        {
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@branchcode", SqlDbType.NVarChar, 50);
            p1.Value = this.ddlBranch.SelectedItem.Text;
            paramlist.Add(p1);
            IList<BankInfo> paylist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchcode=@branchcode ", paramlist);
            ddlBank.DataSource = paylist;
            ddlBank.DataTextField = "BankName";
            ddlBank.DataValueField = "BankID";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("请选择", "0"));
            PNListBind();
        }
        protected void ddlPaymentType_SelectedIndexChangeed(object sender, EventArgs e)
        {
            PNListBind();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
                Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
                Label lblSupplierAccount = (Label)e.Row.FindControl("lblSupplierAccount");
                Label lblSupplierBank = (Label)e.Row.FindControl("lblSupplierBank");

                lblSupplier.Text = model.SupplierName;
                lblSupplierAccount.Text = model.SupplierBankAccount;
                lblSupplierBank.Text = model.SupplierBankName;
                Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
                if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                    labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
                Label lblInvoice = (Label)e.Row.FindControl("lblInvoice");
                if (lblInvoice != null)
                    if (model.IsInvoice != null)
                    {
                        if (model.IsInvoice.Value == 1)
                            lblInvoice.Text = "已开";
                        else if (model.IsInvoice.Value == 0)
                            lblInvoice.Text = "未开";
                        else
                            lblInvoice.Text = "无需发票";
                    }

                Label lblPR = (Label)e.Row.FindControl("lblPR");
                if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                    lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
                else
                    lblPR.Text = model.PRNo;
                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
                HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
                if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                    lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);
                //这里的处理是为了回显之前选中的情况
                if (e.Row.RowIndex > -1 && this.PNSelectedItems != null)
                {
                    CheckBox cb = e.Row.FindControl("chkReturn") as CheckBox;
                    if (this.PNSelectedItems != null && model != null)
                    {
                        if (this.PNSelectedItems.Contains(model.ReturnID))
                            cb.Checked = true;
                        else
                            cb.Checked = false;
                    }
                }
            }
        }


    }
}
