using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Framework.BusinessLogic;

public partial class Purchase_FinancialAudit : ESP.Web.UI.PageBase
{
    private ESP.Finance.Entity.ReturnInfo returnModel;
    private Dictionary<int, string> MediaOrderPaymentUsers;
    string BeiJingBranch = string.Empty;
    //string hunanGroup = string.Empty;
    protected List<int> SelectedItems
    {
        get
        {
            var list = (List<int>)ViewState["mySelectedItems"];
            if (list == null)
            {
                list = new List<int>();
                ViewState["mySelectedItems"] = list;
            }
            return list;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_FinancialAudit));
        this.ddlPaymentType.Attributes.Add("onchange", "selectPaymentType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        this.ddlBank.Attributes.Add("onchange", "selectBank(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        BeiJingBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditBJBranch"];
        //hunanGroup = System.Configuration.ConfigurationManager.AppSettings["hunanGroup"]; 
        if (!IsPostBack)
        {
            returnModel = LoadModel();
            BindData();
            bindSupplierLog();
        }
    }

    protected void txtFactFee_TextChanged(object sender, EventArgs e)
    {
        if (hidTaxShow.Value == "1")
        {
            double tax = ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(double.Parse(txtFactFee.Text), 1);
            lblTaxDesc.Text = "个税金额:" + tax.ToString() + ";     税后支付金额:" + (decimal.Parse(txtFactFee.Text) - decimal.Parse(tax.ToString())).ToString();
        }
    }

    private void bindSupplierLog()
    {
        System.Text.StringBuilder log = new System.Text.StringBuilder();
        if (returnModel != null && returnModel.PRID != null && returnModel.PRID.Value != 0)
        {
            ESP.Purchase.Entity.OrderInfo order = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(returnModel.PRID.Value);
            if (order != null && order.supplierId != 0)
            {
                ESP.Purchase.Entity.ESPAndSupplySuppliersRelation relation = ESP.Purchase.BusinessLogic.ESPAndSupplySuppliersRelationManager.GetModelByEid(order.supplierId);
                if (relation != null)
                {
                    IList<ESP.Purchase.Entity.SupplierLogInfo> loglist = ESP.Purchase.BusinessLogic.SupplierLogManager.GetList(" and supplysupplierid= " + relation.SupplySupplierId.ToString());
                    if (loglist != null && loglist.Count > 0)
                    {
                        foreach (ESP.Purchase.Entity.SupplierLogInfo l in loglist)
                        {
                            log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.ChangeDate).Append(" ")
                               .Append("由").Append(" " + l.OldName + " 变更为" + l.NewName)
                               .Append("<br/>");
                        }
                    }
                }
            }
        }
        lblSupplierLog.Text = log.ToString();
    }

    private ESP.Finance.Entity.ReturnInfo LoadModel()
    {
        int returnId;
        if (!int.TryParse(Request[ESP.Finance.Utility.RequestName.ReturnID], out returnId) || returnId <= 0)
            return null;

        var m = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        return m;
    }

    private bool BindData()
    {
      
        this.lblPayCode.Style["display"] = "none";
        this.txtPayCode.Style["display"] = "none";
        BindInfo(returnModel);

        return true;
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
            var paymentUserId = model.PaymentUserID ?? 0;
            if (paymentUserId != 0)
            {
                string paymentUserName = string.Empty;
                this.MediaOrderPaymentUsers.TryGetValue(paymentUserId, out paymentUserName);
                labPaymenter.Text = paymentUserName;
                labPaymenter.Attributes.Add("onclick", "javascript:showUserInfoAsync(" + paymentUserId + ");");
            }
            CheckBox cb = e.Row.FindControl("chkMedia") as CheckBox;
            //这里的处理是为了回显之前选中的情况

            var selectedItems = this.SelectedItems;
            if (selectedItems.Contains(model.MeidaOrderID))
            {
                cb.Checked = true;
                if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                    cb.Enabled = false;
            }
            else
            {
                cb.Checked = false;
                cb.Enabled = true;
            }
        }
    }
    private void SetSubTotal(IList<ESP.Purchase.Entity.MediaOrderInfo> mediaList)
    {
        Dictionary<int, decimal> mapping = new Dictionary<int, decimal>();
        foreach (var m in mediaList)
        {
            var u = m.PaymentUserID ?? 0;
            decimal d;
            if (!mapping.TryGetValue(u, out d))
            {
                mapping.Add(u, m.TotalAmount ?? 0);
            }
            else
            {
                mapping[u] = d + (m.TotalAmount ?? 0);
            }
        }
        decimal totalUnPaid = 0;
        mapping.TryGetValue(0, out totalUnPaid);

        var retstr = new System.Text.StringBuilder();
        foreach (var e in mapping)
        {
            string username;
            MediaOrderPaymentUsers.TryGetValue(e.Key, out username);
            retstr.Append(username).Append(" 已付金额：").Append(e.Value.ToString("#,##0.00")).Append("<br/>");
        }
        retstr.Append(" 未付金额：").Append(totalUnPaid.ToString("#,##0.00")).Append("<br/>");

        lblTotal.Text = retstr.ToString();
    }

    /// <summary>
    /// 从当前页收集选中项的情况
    /// </summary>
    protected void CollectSelected()
    {
        var list = this.SelectedItems;

        for (int i = 0; i < this.gvG.Rows.Count; i++)
        {
            var MID = gvG.Rows[i].Cells[1].Text.Trim();
            int mid;
            int.TryParse(MID, out mid);

            CheckBox cb = this.gvG.Rows[i].FindControl("chkMedia") as CheckBox;
            bool contains = list.Contains(mid);
            if (contains && !cb.Checked)
                list.Remove(mid);
            else if (!contains && cb.Checked)
                list.Add(mid);
        }
    }

    private string GetAuditLog(ESP.Purchase.Entity.GeneralInfo pr, ReturnInfo ReturnModel)
    {
        IList<ESP.Purchase.Entity.AuditLogInfo> oploglist = null;

        if (pr != null)
        {
            if (pr.ValueLevel == 1)
            {
                oploglist = ESP.Purchase.BusinessLogic.AuditLogManager.GetModelListByGID(pr.id);
            }
        }

        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);

        System.Text.StringBuilder log = new System.Text.StringBuilder();

        if (oploglist != null && oploglist.Count > 0)
        {
            foreach (var l in oploglist)
            {
                log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.remarkDate).Append(" ")
                    .Append(l.auditUserName)
                    .Append(ESP.Purchase.Common.State.operationAudit_statusName[l.auditType]).Append(" ")
                    .Append(l.remark).Append("<br/>");
            }
        }

        foreach (var l in histList)
        {
            string austatus = string.Empty;
            if (l.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
            {
                austatus = "审批通过";
            }
            else if (l.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
            {
                austatus = "审批驳回";
            }

            log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.AuditDate).Append(" ")
                .Append(l.AuditorEmployeeName).Append("(").Append(l.AuditorUserName).Append(") ")
                .Append(austatus).Append(" ")
                .Append(l.Suggestion).Append("<br/>");
        }

        return log.ToString();
    }


    private void BindInfo(ESP.Finance.Entity.ReturnInfo returnModel)
    {
        var parentId = returnModel.ParentID ?? 0;
        var parentModel = parentId > 0 ? ReturnManager.GetModel(parentId) : null;
        ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
        ESP.Purchase.Entity.PaymentPeriodInfo paymentPeriod = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(returnModel.ReturnID);
        ESP.Finance.Entity.BankInfo bankModel = null;
        if (parentModel != null)
        {
            this.panParent.Visible = true;

            this.lblParentAmount.Text = parentModel.FactFee.Value.ToString("#,##0.00");
            this.lblParentCode.Text = parentModel.ReturnCode;
            this.lblFee.Text = "<font color='red'>退款金额:</font>";
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : "<font color='red'>" + returnModel.PreFee.Value.ToString("#,##0.00") + "</font>";

            //var parentPrId = parentModel.PRID ?? 0;
            //var parentPr = parentPrId > 0 ? ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(parentPrId) : null;
            if (generalModel != null)
            {
                this.lblParentPrNo.Text = generalModel.PrNo;
                this.lblParentPrTotal.Text = generalModel.totalprice.ToString("#,##0.00");

                
            }
        }
        else
        {
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
        }

        if (returnModel.PRID != null && returnModel.PRID.Value != 0)
        {
            //ESP.Purchase.Entity.GeneralInfo general = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            //ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(returnModel.PRID.Value);


            if (generalModel.PRType == 6 && generalModel.HaveInvoice == false && paymentPeriod.TaxTypes != 0)
            {
                hidTaxShow.Value = "1";
                double tax = ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(double.Parse(returnModel.PreFee.Value.ToString()), 1);
                lblTaxDesc.Text = "个税金额:" + tax.ToString() + ";     税后支付金额:" + (returnModel.PreFee.Value - decimal.Parse(tax.ToString())).ToString("#0.00");
            }
        }

        if (generalModel!=null && generalModel.IsFactoring > 0)
        {
            tbFactoring.Visible = true;

            bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(generalModel.IsFactoring);
            if (bankModel != null)
            {
                lblFactoringAccount.Text = bankModel.BankAccountName;
                lblFactoringAccountNo.Text = bankModel.BankAccount;
                lblFactoringBank.Text = bankModel.BankName;
            }
        }

        hidPrID.Value = returnModel.PRID.ToString();
        hidProjectID.Value = returnModel.ProjectID.ToString();

        lblApplicant.Text = returnModel.RequestEmployeeName;
        lblApplicant.Attributes["onclick"] = "javascript:showUserInfoAsync(" + (returnModel.RequestorID ?? 0) + ");";

        lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
        lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");

        lblPeriodType.Text = returnModel.PaymentTypeName;
        txtPayRemark.Text = returnModel.ReturnContent;
        txtOtherRemark.Text = returnModel.Remark;

        if (returnModel.ReturnType == null || returnModel.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.CommonPR)
            lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
        else
            lblPRNo.Text = returnModel.PRNo;

        lblProjectCode.Text = returnModel.ProjectCode;
        lblReturnCode.Text = returnModel.ReturnCode;

        radioInvoice.SelectedValue = (returnModel.IsInvoice ?? -1).ToString();
        lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);

        this.lblSupplierName.Text = returnModel.SupplierName;
        this.txtSupplierBank.Text = returnModel.SupplierBankName;
        this.txtSupplierAccount.Text = returnModel.SupplierBankAccount;

        IList<ESP.Finance.Entity.BankCancelInfo> cancelList = ESP.Finance.BusinessLogic.BankCancelManager.GetList(" ReturnID=" + returnModel.ReturnID + " and (ordertype is null or ordertype=1 )");

        if (cancelList != null && cancelList.Count > 0)
        {
            this.lblSupplierName.Text = cancelList[cancelList.Count - 1].OldBankAccountName;
            this.txtSupplierBank.Text = cancelList[cancelList.Count - 1].NewBankName;
            this.txtSupplierAccount.Text = cancelList[cancelList.Count - 1].NewBankAccount;
            this.lblOldSupplierAccount.Text = cancelList[cancelList.Count - 1].OldBankAccount;
            this.lblOldSupplierBank.Text = cancelList[cancelList.Count - 1].OldBankName;
        }

        this.lblLog.Text = this.GetAuditLog(generalModel,returnModel);


        PaymentTypeInfo paymentType = returnModel.PaymentTypeID != null ? PaymentTypeManager.GetModel(returnModel.PaymentTypeID.Value) : null;
        if (!string.IsNullOrEmpty(returnModel.PaymentTypeCode) || (paymentType != null && paymentType.IsNeedCode == true))
        {
            this.lblPayCode.Style["display"] = "block";
            this.txtPayCode.Style["display"] = "block";
            this.txtPayCode.Text = returnModel.PaymentTypeCode;
        }

        if (returnModel.BankID != null)
            this.hidBankID.Value = returnModel.BankID.ToString() + "," + returnModel.BankName;

        this.lblAccount.Text = returnModel.BankAccount;
        this.lblAccountName.Text = returnModel.BankAccountName;
        this.lblBankAddress.Text = returnModel.BankAddress;

        if (returnModel.PaymentTypeID != null)
        {
            this.hidPaymentTypeID.Value = returnModel.PaymentTypeID.Value.ToString() + "," + returnModel.PaymentTypeName;
            this.txtPayCode.Text = returnModel.PaymentTypeCode;
        }


        if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit)
        {
            this.txtFactFee.Text = (returnModel.PreFee == null ? 0 : returnModel.PreFee.Value).ToString("#,##0.00;;#");
        }
        else
        {
            this.txtFactFee.Text = (returnModel.FactFee == null ? 0 : returnModel.FactFee.Value).ToString("#,##0.00;;#");
        }

        if (returnModel.ReturnPreDate != null)
            txtReturnPreDate.Text = string.Format("{0:yyyy-MM-dd}", returnModel.ReturnPreDate);

        if (BeiJingBranch.IndexOf(returnModel.BranchCode.ToLower()) >= 0)
        {
            if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit )
            {
                trNext.Visible = true;
            }
            else if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel3 )
            {
                trNext.Visible = true;
                txtReturnPreDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtReturnPreDate.ReadOnly = true;
            }
            else
            {
                trNext.Visible = false;
                txtReturnPreDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtReturnPreDate.ReadOnly = true;
            }
        }
        else
        {
            if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel1 || returnModel.ReturnStatus == (int)PaymentStatus.WaitReceiving)
            {
                trNext.Visible = false;
                txtReturnPreDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtReturnPreDate.ReadOnly = true;
            }
        }

        if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel1 || returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel3 || returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel2 || returnModel.ReturnStatus == (int)PaymentStatus.WaitReceiving)
        {
            this.txtFactFee.ReadOnly = false;
        }

        if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)
        {
            IList<ESP.Purchase.Entity.MediaOrderInfo> mediaList = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModelList(" meidaOrderID in(" + returnModel.MediaOrderIDs.TrimEnd(',') + ")");

            var users = new HashSet<int>();
            var selected = this.SelectedItems;
            foreach (ESP.Purchase.Entity.MediaOrderInfo model in mediaList)
            {
                if (model.IsPayment == 1)
                {
                    selected.Add(model.MeidaOrderID);
                }
                var uid = model.PaymentUserID ?? 0;
                if (uid != 0)
                    users.Add(uid);
            }

            MediaOrderPaymentUsers = ESP.Framework.BusinessLogic.UserManagerEx.GetUserNames(users.ToArray());

            this.gvG.DataSource = mediaList.OrderBy(x => x.BankName).OrderBy(x => x.CityName);
            this.gvG.DataBind();

            SetSubTotal(mediaList);

            trMedia.Visible = true;
            trMediaHeader.Visible = true;
            trTotal.Visible = true;
        }
        else
        {
            trMedia.Visible = false;
            trMediaHeader.Visible = false;
            trTotal.Visible = false;
        }
        labDepartment.Text = returnModel.DepartmentName;
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }

    protected void btnRepay_Click(object sender, EventArgs e)
    {
        int returnId;
        if (!int.TryParse(Request[ESP.Finance.Utility.RequestName.ReturnID], out returnId) || returnId <= 0)
            returnId = 0;
        // /Purchase/FinanceRePay.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() + "' style='cursor: hand'><img title='重汇' src='/images/Edit.gif' border='0px;' ></img></a>";
        Response.Redirect("/Purchase/FinanceRePay.aspx?" + RequestName.ReturnID + "=" + returnId.ToString() + "&BackUrl=/Purchase/ReturnAuditList.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }

        returnModel.SupplierBankAccount = this.txtSupplierAccount.Text.Trim();
        returnModel.SupplierBankName = this.txtSupplierBank.Text.Trim();
        returnModel.ReturnContent = this.txtPayRemark.Text;
        returnModel.Remark = this.txtOtherRemark.Text.Trim();

        if (this.radioInvoice.SelectedIndex >= 0)
            returnModel.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);
        if (!string.IsNullOrEmpty(txtFactFee.Text.Trim()))
            returnModel.FactFee = Convert.ToDecimal(txtFactFee.Text.Trim());

        if (!string.IsNullOrEmpty(txtReturnPreDate.Text.Trim()))
            returnModel.ReturnPreDate = Convert.ToDateTime(txtReturnPreDate.Text);
        setModelInfo(returnModel);

        ReturnManager.Update(returnModel);
    }


    private bool ValidAudited()
    {
            returnModel = LoadModel();
            if (returnModel == null)
                return false;
        var status = returnModel.ReturnStatus ?? 0;
        if (status == 0) return false;

        var estatus = (PaymentStatus)status;

        if (estatus != PaymentStatus.MajorAudit
            && estatus != PaymentStatus.FinanceLevel1 && estatus != PaymentStatus.FinanceLevel2 && estatus != PaymentStatus.FinanceLevel3
            && estatus != PaymentStatus.WaitReceiving )
            return false;


        var user = returnModel.PaymentUserID ?? 0;
        if (user == 0)
            return false;

        //  int currentUserId = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
        if (user == CurrentUserID)
            return true;

        var delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(CurrentUserID);
        if (delegates == null || delegates.Count == 0)
            return false;

        if (delegates.Select(x => x.UserID).Contains(user))
            return true;

        return false;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }

        //如果不是GM项目，且PR提交日期小于6月25日关闭日期，且项目号已经关闭
        if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket
            && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PROJECT_Media
            && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA
            && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA
            && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR
            && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR
            && returnModel.ProjectCode.IndexOf("GM*") < 0)
        {
             if (returnModel.ProjectID.HasValue && returnModel.ProjectID.Value > 0)
            {
                if (CheckerManager.CheckPNOver(returnModel))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('已经超出项目总成本，请检查!');", true);
                    return;
                }
            }
        }

        if (this.radioInvoice.SelectedIndex >= 0)
            returnModel.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);

        if (this.txtFactFee.Text == string.Empty)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('实际付款金额必填!');", true);
            return;
        }
        returnModel.FactFee = Convert.ToDecimal(txtFactFee.Text.Trim());
        returnModel.SupplierBankAccount = this.txtSupplierAccount.Text.Trim();
        returnModel.SupplierBankName = this.txtSupplierBank.Text.Trim();
        returnModel.ReturnContent = this.txtPayRemark.Text.Trim();
        returnModel.Remark = this.txtOtherRemark.Text.Trim();

        if (this.txtReturnPreDate.Text == string.Empty)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计付款时间必填!');", true);
            return;
        }

        returnModel.ReturnPreDate = Convert.ToDateTime(txtReturnPreDate.Text);
        setModelInfo(returnModel);

        if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit)//待出纳第一级审批
        {
            if (returnModel.PreFee >= 100000)
            {
                returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel3;
            }
            else
            {
                returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel1;
            }
        }
        else if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel1)//第二级终审
        {
            //当在财务总监最终付款时，如果付款类型是现金的话，PN状态变成待报销
            if (returnModel.PaymentTypeID == 1)
            {
                returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving;
            }
            else
            {
                returnModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
            }
            //当在财务总监最终付款时，如果是押金或抵押金付款，PN状态变成待报销
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift)
                returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving;
            else if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_KillCash)
                returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving; //当在财务总监最终付款时,如果是抵消现金，PN状态变成待报销
            else
            {
                if (returnModel.PaymentTypeID == 1)
                {
                    returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving;
                }
                else
                {
                    returnModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
                }
            }

        }
        else if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel3)
        {

            returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel1;
        }
        else if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel2)
        {

            returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel1;
        }
        else if (returnModel.ReturnStatus == (int)PaymentStatus.WaitReceiving)
        {
            //returnModel.ReturnFactDate = DateTime.Now;
            returnModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
        }


        if (returnModel.ReturnStatus != (int)PaymentStatus.FinanceComplete && returnModel.ReturnStatus != (int)PaymentStatus.WaitReceiving)
        {
            if (this.txtNextAuditor.Text == string.Empty || this.hidNextAuditor.Value == string.Empty)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('下一级审核人必填!');", true);
                return;
            }
            if (this.hidNextAuditor.Value == CurrentUser.SysID)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('下一级审核人不能是当前审核人!');", true);
                return;
            }
            returnModel.PaymentEmployeeName = this.txtNextAuditor.Text;
            returnModel.PaymentUserID = Convert.ToInt32(this.hidNextAuditor.Value);

        }
        int nextAuditorID = 0;
        if (!string.IsNullOrEmpty(hidNextAuditor.Value) && hidNextAuditor.Value != "0")
            nextAuditorID = Convert.ToInt32(hidNextAuditor.Value);
        //如果是媒体稿费收集已付款的记者
       // CollectSelected();

        var currentUser = UserManager.Get();

        UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel, this.SelectedItems, currentUser.UserID);
        SaveHistory(returnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, nextAuditorID);
        string exMail = string.Empty;
        if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceComplete || returnModel.ReturnStatus == (int)PaymentStatus.WaitReceiving)
        {
            ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(returnModel.RequestorID.Value);
            try
            {
                SendMailHelper.SendMailReturnComplete(returnModel, returnModel.RequestUserName, emp.Email);
            }
            catch 
            {
                exMail = "(邮件发送失败!)";
            }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批成功!"+exMail+"');", true);
           
        }
        else
        {
            if (result == UpdateResult.Succeed)
            {
                if (returnModel.PaymentUserID != null)
                {
                    try
                    {
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(returnModel.PaymentUserID.Value);
                        SendMailHelper.SendMailReturnFinanceOK(returnModel, currentUser.FullNameCN, emp.FullNameCN, emp.Email);
                    }
                    catch
                    {
                        exMail = "(邮件发送失败!)";
                    }
                }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批成功!"+exMail+"');", true);
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "!');", true);
        }

        Response.Redirect(GetBackUrl());

    }

    protected void btnTip_Click(object sender, EventArgs e)
    {
        if (returnModel == null)
        {
            returnModel = LoadModel();
        }
        ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
        audit.FormID = returnModel.ReturnID;
        audit.Suggestion = this.txtRemark.Text;
        audit.AuditDate = DateTime.Now;
        audit.AuditorSysID = int.Parse(CurrentUser.SysID);
        audit.AuditorUserCode = CurrentUser.ID;
        audit.AuditorEmployeeName = CurrentUser.Name;
        audit.AuditorUserName = CurrentUser.ITCode;
        audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
        audit.FormType = (int)ESP.Finance.Utility.FormType.Return;
        int ret = ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('留言保存成功！');", true);
    }

    private void setModelInfo(ReturnInfo returns)
    {
        if (!string.IsNullOrEmpty(this.hidBankID.Value))
        {
            string[] strs = this.hidBankID.Value.Split(',');
            ESP.Finance.Entity.BankInfo model = ESP.Finance.BusinessLogic.BankManager.GetModel(Convert.ToInt32(strs[0]));
            returns.BankID = model.BankID;
            returns.BankName = model.BankName;
            returns.BranchCode = model.BranchCode;
            returns.BranchID = model.BranchID;
            returns.BranchName = model.BranchName;
            returns.DBCode = model.DBCode;
            returns.DBManager = model.DBManager;
            returns.BankAccount = model.BankAccount;
            returns.BankAccountName = model.BankAccountName;
            returns.BankAddress = model.Address;
            returns.ReturnContent = this.txtPayRemark.Text;
        }
        if (!string.IsNullOrEmpty(this.hidPaymentTypeID.Value))
        {
            string[] strs = this.hidPaymentTypeID.Value.Split(',');
            returns.PaymentTypeID = Convert.ToInt32(strs[0]);
            returns.PaymentTypeName = strs[1];
            returns.PaymentTypeCode = this.txtPayCode.Text;
        }
        if (this.radioInvoice.SelectedIndex >= 0)
            returns.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        string exMail = string.Empty;
        returnModel.ReturnStatus = (int)PaymentStatus.Save;
        returnModel.ReturnContent = this.txtPayRemark.Text.Trim();
        returnModel.Remark = this.txtOtherRemark.Text.Trim();
        UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
        int nextAuditorID = 0;
        if (!string.IsNullOrEmpty(hidNextAuditor.Value) && hidNextAuditor.Value != "0")
            nextAuditorID = Convert.ToInt32(hidNextAuditor.Value);
        SaveHistory(returnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing, nextAuditorID);
        if (result == UpdateResult.Succeed)
        {
            try
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(returnModel.RequestorID.Value);
                SendMailHelper.SendMailReturnReject(returnModel, CurrentUser.Name, returnModel.RequestUserName, emp.Email);
                //在此处增加给申请人fa送邮件的通知
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('已驳回成功!"+exMail+"');", true);
        }
        else
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "!');", true);

        Response.Redirect(GetBackUrl());
    }

    protected void btnNoFinance_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        string exMail = string.Empty;
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
        int FirstFinanceID = branchModel.FirstFinanceID;
        ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
        if (branchdept != null)
            FirstFinanceID = branchdept.FianceFirstAuditorID;

        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(FirstFinanceID);
        returnModel.PaymentUserID = Convert.ToInt32(emp.SysID);
        returnModel.PaymentUserName = emp.ITCode;
        returnModel.PaymentCode = emp.ID;
        returnModel.PaymentEmployeeName = emp.Name;
        returnModel.ReturnStatus = (int)PaymentStatus.MajorAudit;
        returnModel.ReturnContent = this.txtPayRemark.Text.Trim();
        returnModel.Remark = this.txtOtherRemark.Text.Trim();
        UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

        SaveHistory(returnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing, 0);
        if (result == UpdateResult.Succeed)
        {
            try
            {
                SendMailHelper.SendMailReturnReject(returnModel, CurrentUser.Name, emp.Name, emp.EMail);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('已驳回成功!"+exMail+"');", true);
        }
        else
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "!');", true);

        Response.Redirect(GetBackUrl());
    }

    private void SaveHistory(ReturnInfo returnModel, int Status, int nextAuditor)
    {
        string DelegateUsers = string.Empty;
        string DelegateUserNames = string.Empty;
        string term = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
            DelegateUserNames += new ESP.Compatible.Employee(model.UserID).Name;
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        DelegateUserNames = DelegateUserNames.TrimEnd(',');

        if (!string.IsNullOrEmpty(DelegateUsers))
            term = " AuditType=@AuditType and (AuditorUserID=@AuditorUserID or AuditorUserID in(" + DelegateUsers + ")) and returnID=@ReturnID ";
        else
            term = " AuditType=@AuditType and AuditorUserID=@AuditorUserID and returnID=@ReturnID ";

        List<SqlParameter> paramlist = new List<SqlParameter>();
        SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
        p1.Value = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p2.Value = CurrentUser.SysID;
        paramlist.Add(p2);
        SqlParameter p3 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
        p3.Value = returnModel.ReturnID;
        paramlist.Add(p3);
        IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList;
        //查询审批历史中是否存在当前审批人
        auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist);
        ReturnAuditHistInfo auditHist;
        if (auditList.Count == 0)
        {//如果不存在新建一个历史记录
            auditHist = new ReturnAuditHistInfo();
            auditHist.ReturnID = returnModel.ReturnID;
            auditHist.AuditorUserID = int.Parse(CurrentUser.SysID);
            auditHist.AuditeDate = DateTime.Now;
            auditHist.AuditeStatus = Status;
            if (!string.IsNullOrEmpty(DelegateUsers) && CurrentUser.Name != DelegateUserNames)
                auditHist.Suggestion = txtRemark.Text.Trim() + "[" + CurrentUser.Name + "为" + DelegateUserNames + "的代理人]";
            else
                auditHist.Suggestion = txtRemark.Text.Trim();
            auditHist.AuditorUserCode = CurrentUser.ID;
            auditHist.AuditorUserName = CurrentUser.ITCode;
            auditHist.AuditorEmployeeName = CurrentUser.Name;
            auditHist.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(auditHist);
        }
        else
        {//否则更新审批历史
            auditHist = auditList[0];
            //auditHist.AuditorUserCode = CurrentUser.ID;
            //auditHist.AuditorUserName = CurrentUser.ITCode;
            //auditHist.AuditorEmployeeName = CurrentUser.Name;
            auditHist.AuditeDate = DateTime.Now;
            auditHist.AuditeStatus = Status;
            if (!string.IsNullOrEmpty(DelegateUsers) && CurrentUser.Name != auditList[0].AuditorEmployeeName)
                auditHist.Suggestion = txtRemark.Text.Trim() + "[" + CurrentUser.Name + "为" + auditList[0].AuditorEmployeeName + "的代理人]";
            else
                auditHist.Suggestion = txtRemark.Text.Trim();
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Update(auditHist);
        }
        if (nextAuditor != 0 && returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.MajorAudit && returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
        {
            term = " AuditType=@AuditType and AuditorUserID=@AuditorUserID and returnID=@ReturnID ";
            paramlist.Clear();
            paramlist.Add(p1);
            p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p2.Value = nextAuditor;
            paramlist.Add(p2);
            paramlist.Add(p3);
            auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist);
            if (auditList.Count == 0)
            {
                ReturnAuditHistInfo NextAuditHist = new ReturnAuditHistInfo();
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(nextAuditor);
                NextAuditHist.ReturnID = returnModel.ReturnID;
                NextAuditHist.AuditorUserID = emp.UserID;
                NextAuditHist.AuditorUserName = emp.Username;
                NextAuditHist.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                NextAuditHist.AuditorUserCode = emp.Code;
                NextAuditHist.AuditorEmployeeName = emp.FullNameCN;
                NextAuditHist.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(NextAuditHist);
            }
        }

        if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit)
        {//驳回到财务第一级
            term = " AuditType=@AuditType and ReturnID=@ReturnID";
            paramlist.Clear();
            p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
            p1.Value = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            paramlist.Add(p1);
            p2 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
            p2.Value = returnModel.ReturnID;
            paramlist.Add(p2);
            auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist);
            foreach (ESP.Finance.Entity.ReturnAuditHistInfo model in auditList)
            {
                ESP.Finance.BusinessLogic.ReturnAuditHistManager.Delete(model.ReturnAuditID);
            }
            
            //ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
            //int FirstFinanceID = branchModel.FirstFinanceID;
            //ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
            //if (branchdept != null)
            //    FirstFinanceID = branchdept.FianceFirstAuditorID;

            ReturnAuditHistInfo AuditHist = new ReturnAuditHistInfo();
            ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(returnModel.PaymentUserID.Value);
            AuditHist.ReturnID = returnModel.ReturnID;
            AuditHist.AuditorUserID = emp.UserID;
            AuditHist.AuditorUserName = emp.Username;
            AuditHist.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            AuditHist.AuditorUserCode = emp.Code;
            AuditHist.AuditorEmployeeName = emp.FullNameCN;
            AuditHist.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(AuditHist);
        }
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> GetPayments()
    {
        List<List<string>> retlists = new List<List<string>>();
        IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(null, null);
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
    public static List<List<string>> GetBanks(int returnid)
    {
        ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
        string branchcode = string.Empty;
        if (model != null && !string.IsNullOrEmpty(model.ProjectCode))
        {
            branchcode = model.ProjectCode.Substring(0, 1);
        }

        List<List<string>> retlists = new List<List<string>>();
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        paramlist.Add(new System.Data.SqlClient.SqlParameter("@branchcode", branchcode));
        IList<BankInfo> paylist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchcode=@branchcode", paramlist);
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
    public static List<string> GetPaymentTypeModel(int paymentTypeID)
    {
        List<string> list = new List<string>();
        ESP.Finance.Entity.PaymentTypeInfo model = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(paymentTypeID);
        list.Add(model.PaymentTypeID.ToString());
        list.Add(model.PaymentTypeName);
        list.Add(model.IsNeedCode.ToString());
        list.Add(model.IsNeedBank.ToString());
        list.Add(model.Tag.ToString());
        return list;
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


    private string GetBackUrl()
    {
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "PurchasePaymentList.aspx" : Request["BackUrl"];
    }
}