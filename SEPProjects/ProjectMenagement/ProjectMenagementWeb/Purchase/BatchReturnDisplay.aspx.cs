using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace FinanceWeb.Purchase
{
    public partial class BatchReturnDisplay : System.Web.UI.Page
    {
        int batchID = 0;
        ESP.Finance.Entity.PNBatchInfo batchModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
            }
        }

        private void Search()
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnList = null;
            if (string.IsNullOrEmpty(Request[RequestName.BatchID]))
            {
                if (!string.IsNullOrEmpty(lblBatchCode.Text.Trim()))
                {
                    batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModelByBatchCode(lblBatchCode.Text.Trim());
                    returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchModel.BatchID);
                }
            }
            else
            {
                batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(Convert.ToInt32(Request[RequestName.BatchID]));
                returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(Convert.ToInt32(Request[RequestName.BatchID]));
            }
            if (batchModel != null)
            {
                string batchcode = batchModel.BatchCode;
                if (string.IsNullOrEmpty(batchcode))
                    batchcode = this.lblBatchCode.Text.Trim();
                if (!string.IsNullOrEmpty(batchcode))
                {
                    this.lblBatchId.Text = batchModel.BatchID.ToString();
                    this.lblPurchaseBatchCode.Text = batchModel.PurchaseBatchCode;
                    this.lblBatchCode.Text = batchModel.BatchCode;
                    this.lblBankName.Text = batchModel.BankName;
                    this.lblBranchCode.Text = batchModel.BranchCode;
                    this.lblSupplierName.Text = batchModel.SupplierName;
                    this.lblSupplierName.Text = batchModel.SupplierBankAccount;
                    this.lblSupplierBankName.Text = batchModel.SupplierBankName;
                    this.lblAccount.Text = batchModel.BankAccount;
                    this.lblAccountName.Text = batchModel.BankAccountName;
                    this.lblBankAddress.Text = batchModel.BankAddress;
                    this.lblPaymentDate.Text = batchModel.PaymentDate==null? "":batchModel.PaymentDate.Value.ToString("yyyy-MM-dd");
                    this.lblPaymentType.Text = batchModel.PaymentType;
                    if (batchModel.IsInvoice != null)
                    {
                        switch (batchModel.IsInvoice.Value)
                        { 
                            case 0:
                                this.lblInvoice.Text = "未开";
                                break;
                            case 1:
                                this.lblInvoice.Text = "已开";
                                break;
                            case 2:
                                this.lblInvoice.Text = "无需发票";
                                break;
                        }
                    }
                    var sortReturnList = from c in returnList orderby c.ReturnID select c;
                    this.gvG.DataSource = sortReturnList;
                    this.gvG.DataBind();
                    #region "记者稿费信息"
                    string mediaorderIDs = string.Empty;
                    List<ESP.Purchase.Entity.MediaOrderInfo> mediaList = new List<ESP.Purchase.Entity.MediaOrderInfo>();

                    foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
                    {
                        if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                        {
                            mediaorderIDs += model.MediaOrderIDs.TrimEnd(',') + ",";

                            if (!string.IsNullOrEmpty(model.MediaOrderIDs.TrimEnd(',')))
                            {
                                List<ESP.Purchase.Entity.MediaOrderInfo> tmpList = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModelList(" meidaOrderID in(" + model.MediaOrderIDs.TrimEnd(',') + ")");
                                if (tmpList != null && tmpList.Count > 0)
                                {
                                    foreach (ESP.Purchase.Entity.MediaOrderInfo m in tmpList)
                                    {
                                        m.ReturnID = model.ReturnID;
                                        m.ReturnCode = model.ReturnCode;
                                        mediaList.Add(m);
                                    }
                                }
                            }
                        }
                    }
                    ArrayList tmplist = new ArrayList();

                    var sortList = from c in mediaList orderby c.ReturnID, c.CityName, c.BankName select c;
                    if (mediaList.Count > 0)
                    {
                        foreach (ESP.Purchase.Entity.MediaOrderInfo model in mediaList)
                        {
                            if (model.IsPayment == 1)
                            {
                                tmplist.Add(model.MeidaOrderID);
                            }
                        }
                        this.SelectedItems = tmplist;
                        this.gvMedia.DataSource = sortList;
                        this.gvMedia.DataBind();
                        SetSubTotal(mediaorderIDs.TrimEnd(','));
                        trMedia.Visible = true;
                        trTotal.Visible = true;
                        trMediaHeader.Visible = true;
                    }
                    else
                    {
                        trMedia.Visible = false;
                        trTotal.Visible = false;
                        trMediaHeader.Visible = false;
                    }
                    #endregion
                    decimal totalAmounts = 0;
                    if (returnList != null && returnList.Count > 0)
                    {
                        foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
                        {
                            totalAmounts += model.PreFee.Value;
                        }
                    }
                    this.lblFee.Text = totalAmounts.ToString("#,##0.00");
                }
            }
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
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(reutrnID);
                ESP.Finance.BusinessLogic.PNBatchRelationManager.DeleteByReturnID(batchModel.BatchID,returnModel);
                Search();
            }

        }

        protected void CollectSelected()
        {
            if (this.SelectedItems == null)
                SelectedItems = new ArrayList();
            else
                SelectedItems.Clear();
            string MID = string.Empty;
            for (int i = 0; i < this.gvMedia.Rows.Count; i++)
            {
                MID = gvMedia.Rows[i].Cells[1].Text.Trim();
                CheckBox cb = this.gvMedia.Rows[i].FindControl("chkMedia") as CheckBox;
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

        protected void gvMedia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.PNBatchInfo batchModel = null;
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BatchID]))
            {
                batchID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.BatchID]);
                batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchID);
            }
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label labPaymenter = (Label)e.Row.FindControl("labPaymenter");

                ESP.Purchase.Entity.MediaOrderInfo model = (ESP.Purchase.Entity.MediaOrderInfo)e.Row.DataItem;
                if (model.PaymentUserID != null && model.PaymentUserID.Value != 0)
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(model.PaymentUserID.Value);
                    labPaymenter.Text = emp.Name;
                    labPaymenter.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(emp.SysID)) + "');");
                }
                CheckBox cb = e.Row.FindControl("chkMedia") as CheckBox;
                //这里的处理是为了回显之前选中的情况
                if (e.Row.RowIndex > -1 && this.SelectedItems != null)
                {
                    if (this.SelectedItems != null && model != null)
                    {
                        if (this.SelectedItems.Contains(model.MeidaOrderID))
                        {
                            cb.Checked = true;
                            if (batchModel.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                                cb.Enabled = false;
                        }
                        else
                        {
                            cb.Checked = false;
                            cb.Enabled = true;
                        }
                    }
                }
            }
        }

        private void SetSubTotal(string ids)
        {
            lblTotal.Text = ESP.Purchase.BusinessLogic.MediaOrderManager.GetTotalAmountByUser(ids);
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
                Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
                lblSupplier.Text = model.SupplierName;
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
                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (hylPrint != null)
                {
                    hylPrint.Target = "_blank";
                    hylPrint.NavigateUrl = "Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
                }
            }
        }
    }
}
