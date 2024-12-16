using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Data;

public partial class Purchase_BatchPurchaseDisplay : ESP.Web.UI.PageBase
{
    int BatchID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.BatchID]))
            BatchID = int.Parse(Request[RequestName.BatchID]);
        if (!IsPostBack)
        {
            BindInfo();
            ListBind();
        }
    }

    private void ListBind()
    {
        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(BatchID);
        returnList.OrderByDescending(N => N.ProjectCode.Substring(0, 1)).OrderByDescending(N => N.PRNo);
        gvG.DataSource = returnList;
        gvG.DataBind();
        decimal total = 0m;
        foreach (ReturnInfo model in returnList)
        {
            total += model.PreFee.Value;
        }
        labTotal.Text = total.ToString("#,##0.00");
        ddlCompany.Enabled = ddlPaymentType.Enabled = false;
    }

    private void BindInfo()
    {
        //绑定付款方式
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

        //绑定公司代码
        IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
        ddlCompany.DataSource = blist;
        ddlCompany.DataTextField = "BranchCode";
        ddlCompany.DataValueField = "BranchID";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new ListItem("请选择", "0"));
        //绑定批次信息
        ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);
        if (batchModel != null)
        {
            ddlCompany.SelectedValue = batchModel.BranchID == null ? "0" : batchModel.BranchID.Value.ToString();
            ddlPaymentType.SelectedValue = batchModel.PaymentTypeID == null ? "0" : batchModel.PaymentTypeID.Value.ToString();
            labCreateUser.Text = ESP.Framework.BusinessLogic.EmployeeManager.Get(batchModel.CreatorID.Value).FullNameCN;
            labPurchaeBatchCode.Text = batchModel.PurchaseBatchCode;
            lblBatchCode.Text = batchModel.BatchCode;
            lblBatchId.Text = batchModel.BatchID.ToString();
        }
        getAuditLog(batchModel.BatchID);
    }


    private void getAuditLog(int batchID)
    {
        IList<AuditLogInfo> logList = ESP.Finance.BusinessLogic.AuditLogManager.GetBatchList( batchID);
        if (logList.Count > 0)
        {
            foreach (AuditLogInfo hist in logList)
            {
                string austatus = string.Empty;
                if (hist.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (hist.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }
                string auditdate = hist.AuditDate == null ? "" : hist.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                this.labAuditLog.Text += hist.AuditorEmployeeName + "(" + hist.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + hist.Suggestion + "<br/>";
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BatchPurchaseList.aspx");
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
                lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value),0,model.IsDiscount);

            Literal litGRNo = (Literal)e.Row.FindControl("litGRNo");
            DataTable recpientList = ESP.Finance.BusinessLogic.ReturnManager.GetRecipientIds(model.ReturnID.ToString());
            foreach (DataRow dr in recpientList.Rows)
            {
                litGRNo.Text += "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\Print\\MultiRecipientPrint.aspx?newPrint=true&id=" + dr["recipientid"] + "' target='_blank'>" + dr["recipientNo"].ToString() + "</a>&nbsp;";
            }
        }
    }
}
