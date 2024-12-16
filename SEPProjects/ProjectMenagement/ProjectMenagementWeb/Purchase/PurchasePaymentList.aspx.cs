using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Data.SqlClient;
using ESP.Finance.Utility;

public partial class Purchase_PurchasePaymentList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             IList<ESP.Finance.Entity.ReturnInfo> auditList=Search();
             SearchHist(auditList);
        }
    }

    private IList<ESP.Finance.Entity.ReturnInfo> Search()
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string DelegateUsers = string.Empty;
         IList<ReturnInfo> returnlist =null;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        term = " returnid not in(select returnid from f_pnbatchrelation) ";
        if (!string.IsNullOrEmpty(DelegateUsers))
            term += " and (ReturnStatus=@status1 or  ReturnStatus=@status2 or ReturnStatus=@status3 or ReturnStatus=@status4 or (ReturnStatus=@status5 and returnType not in (@PrType,@PrType2,@PrType3,@PrType4) and PaymentTypeID <> @PaymentTypeID)) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
        else
            term += " and (ReturnStatus=@status1 or  ReturnStatus=@status2 or ReturnStatus=@status3 or ReturnStatus=@status4 or (ReturnStatus=@status5 and returnType not in (@PrType,@PrType2,@PrType3,@PrType4) and PaymentTypeID <> @PaymentTypeID)) AND PaymentUserID=@sysID";
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
        SqlParameter pType = new SqlParameter("@PrType", System.Data.SqlDbType.Int, 4);
        pType.SqlValue = (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift;
        paramlist.Add(pType);
        SqlParameter pType2 = new SqlParameter("@PrType2", System.Data.SqlDbType.Int, 4);
        pType2.SqlValue = (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift;
        paramlist.Add(pType2);
        SqlParameter pType3 = new SqlParameter("@PrType3", System.Data.SqlDbType.Int, 4);
        pType3.SqlValue = (int)ESP.Purchase.Common.PRTYpe.PN_KillCash;
        paramlist.Add(pType3);
        SqlParameter pType4 = new SqlParameter("@PrType4", System.Data.SqlDbType.Int, 4);
        pType4.SqlValue = (int)ESP.Purchase.Common.PRTYpe.PROJECT_Media;
        paramlist.Add(pType4);
        SqlParameter PaymentTypeID = new SqlParameter("@PaymentTypeID", System.Data.SqlDbType.Int, 4);
        PaymentTypeID.SqlValue = 1;
        paramlist.Add(PaymentTypeID);
        SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
        p6.SqlValue = CurrentUser.SysID;
        paramlist.Add(p6);
        if (!string.IsNullOrEmpty(term))
        {
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee like '%'+@prno+'%'  or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                sp1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(sp1);

            }
            if (this.ddlStatus.SelectedIndex != 0)
            {
                term += " and returnStatus=@returnStatus";
                System.Data.SqlClient.SqlParameter sp2 = new System.Data.SqlClient.SqlParameter("@returnStatus", System.Data.SqlDbType.Int, 4);
                sp2.SqlValue = this.ddlStatus.SelectedValue;
                paramlist.Add(sp2);
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                {
                    term += " and LastUpdateDateTime between @beginDate and @endDate";
                    System.Data.SqlClient.SqlParameter sp3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                    sp3.SqlValue = this.txtBeginDate.Text;
                    paramlist.Add(sp3);
                    System.Data.SqlClient.SqlParameter sp4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                    sp4.SqlValue = this.txtEndDate.Text;
                    paramlist.Add(sp4);

                }
            }
            if (this.radioInvoice.SelectedIndex >= 0)
            {
                term += " and IsInvoice=@IsInvoice";
                System.Data.SqlClient.SqlParameter sp5 = new System.Data.SqlClient.SqlParameter("@IsInvoice", System.Data.SqlDbType.Int, 4);

                if (this.radioInvoice.SelectedIndex >= 0)
                    sp5.SqlValue = Convert.ToInt32(this.radioInvoice.SelectedValue);
                paramlist.Add(sp5);
            }
            IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            var tmplist = returnList.OrderBy(N => N.PreBeginDate);
            returnlist = tmplist.ToList();
            this.gvG.DataSource = returnlist;
            this.gvG.DataBind();
        }
        return returnlist;
    }


    private void SearchHist(IList<ESP.Finance.Entity.ReturnInfo> returnList)
    {
        string auditingReturnIds = string.Empty;
        string auditingSelected = string.Empty;
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');

        foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
        {
            auditingReturnIds += model.ReturnID.ToString() + ",";
        }
        auditingReturnIds = auditingReturnIds.TrimEnd(',');

        IList<ESP.Finance.Entity.ReturnInfo> list;
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string Branchs = string.Empty;
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%," + CurrentUser.SysID + ",%'");
        if (branchList != null && branchList.Count > 0)
        {
            foreach (ESP.Finance.Entity.BranchInfo b in branchList)
            {
                Branchs += "'"+b.BranchCode + "',";
            }
        }
        Branchs = Branchs.TrimEnd(',');
        if (!string.IsNullOrEmpty(auditingReturnIds))
        {
            auditingSelected = " and returnid not in("+auditingReturnIds+")";
        }

        if (!string.IsNullOrEmpty(DelegateUsers))
        {
            DelegateUsers += "," + CurrentUser.SysID;
        }
        else
            DelegateUsers = CurrentUser.SysID;

        term = " ((returnid in(select distinct formid from f_auditlog  where auditorsysid in(" + DelegateUsers + ") and formtype=@formtype)" + auditingSelected + ")";
        if (!string.IsNullOrEmpty(Branchs))
        {
            term += "or (branchcode in(" + Branchs + ") and returnStatus=140))";
        }
        else
            term += ")";
        SqlParameter p2 = new SqlParameter("@AuditeStatus", SqlDbType.Int, 4);
        p2.Value = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
        paramlist.Add(p2);
        SqlParameter p3 = new SqlParameter("@formtype", SqlDbType.Int, 4);
        p3.Value = (int)ESP.Finance.Utility.FormType.Return ;
        paramlist.Add(p3);
        if (!string.IsNullOrEmpty(term))
        {
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee like '%'+@prno+'%'  or suppliername like '%'+@prno+'%')";
                SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                sp1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(sp1);

            }
            if (this.ddlStatus.SelectedIndex != 0)
            {
                term += " and returnStatus=@returnStatus";
                System.Data.SqlClient.SqlParameter sp2 = new System.Data.SqlClient.SqlParameter("@returnStatus", System.Data.SqlDbType.Int, 4);
                sp2.SqlValue = this.ddlStatus.SelectedValue;
                paramlist.Add(sp2);
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                {
                    term += " and LastUpdateDateTime between @beginDate and @endDate";
                    System.Data.SqlClient.SqlParameter sp3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                    sp3.SqlValue = this.txtBeginDate.Text;
                    paramlist.Add(sp3);
                    System.Data.SqlClient.SqlParameter sp4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                    sp4.SqlValue = this.txtEndDate.Text;
                    paramlist.Add(sp4);

                }
            }
        }
        if (this.radioInvoice.SelectedIndex >= 0)
        {
            term += " and IsInvoice=@IsInvoice";
            System.Data.SqlClient.SqlParameter sp5 = new System.Data.SqlClient.SqlParameter("@IsInvoice", System.Data.SqlDbType.Int, 4);
            if (this.radioInvoice.SelectedValue == "0")
                sp5.SqlValue = false;
            else
                sp5.SqlValue = true;
            paramlist.Add(sp5);
        }
        list = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
        this.gvHist.DataSource = list;
        this.gvHist.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IList<ESP.Finance.Entity.ReturnInfo> returnList =Search();
        SearchHist(returnList);
    }

    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = string.Empty;
        this.ddlStatus.SelectedIndex = 0;
        this.txtBeginDate.Text = string.Empty;
        this.txtEndDate.Text = string.Empty;
        this.radioInvoice.SelectedIndex = -1;
          IList<ESP.Finance.Entity.ReturnInfo> returnList =Search();
          SearchHist(returnList);
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        Search();
    }

    protected void gvHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHist.PageIndex = e.NewPageIndex;
        IList<ESP.Finance.Entity.ReturnInfo> returnList = Search();
        SearchHist(returnList);
    }

    protected void gvHist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblPR = (Label)e.Row.FindControl("lblPR");
            if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
            else
                lblPR.Text = model.PRNo;
            Label lblInvoice = (Label)e.Row.FindControl("lblInvoice");
            //HyperLink hylInvoice = (HyperLink)e.Row.FindControl("hylInvoice");
            if (lblInvoice != null && lblInvoice.Text != string.Empty)
                if (model.IsInvoice != null)
                {
                    if (model.IsInvoice.Value == 1)
                    {
                        lblInvoice.Text = "已开";
                        //hylInvoice.Visible = false;
                    }
                    else if (model.IsInvoice.Value == 0)
                    {
                        lblInvoice.Text = "未开";
                        //hylInvoice.Visible = true;
                        //hylInvoice.NavigateUrl = "ReturnFinanceSave.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString() + "&" + RequestName.BackUrl + "=PurchasePaymentList.aspx";
                    }
                    else
                    {
                        lblInvoice.Text = "无需发票";
                        //hylInvoice.Visible = false;
                    }
                }
            Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
            lblSupplier.Text = model.SupplierName;
            Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
            HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
            if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);
            LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
            LinkButton lnkJournalist = (LinkButton)e.Row.FindControl("lnkJournalist");
            //媒体稿费的单子
            if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出所有记者' border='0'>";
                lnkJournalist.Text = "<img src='/images/PrintDefault.gif' title='导出未付款记者' border='0'>";
            }
            else
            {
                lnkAttach.Text = "";
                lnkJournalist.Text = "";
            }
            Label lblAttach = (Label)e.Row.FindControl("lblAttach");
            if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
                lblAttach.Text += "<a href='Print\\MediaUnPayment.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='未付款记者浏览' src='/images/PrintDefault.gif' border='0px;' /></a>";
          
            }
            else
            {
                lblAttach.Text = "";
            }
            //3000以下对私的单子有附件显示
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                lblAttach.Visible = true;
                lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?id=" + model.PRID.ToString() + "&viewButton=no&Action=ViewOldPr' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
            }
            HyperLink hylRedo = (HyperLink)e.Row.FindControl("hylRedo");
            LinkButton lnkTraffic = (LinkButton)e.Row.FindControl("lnkTraffic");
            if (Convert.ToInt32(hidStatusNameID.Value) != (int)PaymentStatus.FinanceComplete)
            {
                hylRedo.Visible = false;
            }
            else
            {
                hylRedo.NavigateUrl = "FinanceRePay.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
            }

            if ((model.PaymentTypeID != null && model.PaymentTypeID.Value == 1) && model.ReturnStatus == (int)PaymentStatus.WaitReceiving && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_KillCash)
            {
                //现金，等待销账
                lnkTraffic.Visible = true;
                lnkTraffic.CommandName = "KillCash";
            }

            HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
            hylPrint.Target = "_blank";
            hylPrint.NavigateUrl = "Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
            //如果申请单暂停，不能重汇付款申请
            if (model.PRID != null && model.PRID != 0 && ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value).InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
            {
                hylRedo.Visible = false;
            }
        }
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
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
            LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
            LinkButton lnkJournalist = (LinkButton)e.Row.FindControl("lnkJournalist");

            if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出所有记者' border='0'>";
                lnkJournalist.Text = "<img src='/images/PrintDefault.gif' title='导出未付款记者' border='0'>";
            }
            else
            {
                lnkAttach.Text = "";
                lnkJournalist.Text = "";
            }
            Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
            lblSupplier.Text = model.SupplierName;
            Label lblAttach = (Label)e.Row.FindControl("lblAttach");
            //媒体稿费的单子
            if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
                lblAttach.Text += "<a href='Print\\MediaUnPayment.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='未付款记者浏览' src='/images/PrintDefault.gif' border='0px;' /></a>";
            }
            else
            {
                lblAttach.Text = "";
            }
            //3000以下对私的单子有附件显示
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                lblAttach.Visible = true;
                lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?id=" + model.PRID.ToString() + "&viewButton=no&Action=ViewOldPr' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
            }
            HyperLink hylAudit = (HyperLink)e.Row.FindControl("hylAudit");
            if (Convert.ToInt32(hidStatusNameID.Value) == (int)PaymentStatus.FinanceComplete)
            {
                hylAudit.Visible = false;
            }
            else
            {
                if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
                {
                    if (model.ReturnStatus == (int)PaymentStatus.WaitReceiving && model.PaymentTypeID == 1)
                    {
                        hylAudit.NavigateUrl = "CashPNLink.aspx?" + RequestName.ReturnID + "=" + gvG.DataKeys[e.Row.RowIndex].Value.ToString();
                    }
                    else
                    {
                        hylAudit.NavigateUrl = "FinancialAudit.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
                    }
                }
                else
                {
                    hylAudit.NavigateUrl = "/ForeGift/financeAudit.aspx?" + RequestName.ReturnID + "=" + gvG.DataKeys[e.Row.RowIndex].Value.ToString();
                }
            }
            HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
            hylPrint.Target = "_blank";
            hylPrint.NavigateUrl = "Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
            //如果申请单暂停，不能编辑付款申请
            if (model.PRID != null && model.PRID != 0 && ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value).InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
            {
                hylAudit.Visible = false;
            }
        }
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Export")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
            {
                string filename;
                string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(CurrentUserID, returnModel, serverpath, out filename,false);
                if (!string.IsNullOrEmpty(filename))
                {
                    outExcel(serverpath + filename, filename, true);
                }
            }
            else
            {
                return;
            }
        }
        else if (e.CommandName == "Journalist")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
            {
                string filename;
                string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(CurrentUserID, returnModel, serverpath, out filename,true);
                if (!string.IsNullOrEmpty(filename))
                {
                    outExcel(serverpath + filename, filename, true);
                }
            }
            else
            {
                return;
            }
        }
    }
    protected void gvHist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Export")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
            {
                string filename;
                string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(CurrentUserID, returnModel, serverpath, out filename,false);
                if (!string.IsNullOrEmpty(filename))
                {
                    outExcel(serverpath + filename, filename, true);
                }
            }
            else
            {
                return;
            }
        }
        else if (e.CommandName == "ReDo")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.FinanceReject;
            ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
            string exMail = string.Empty;
            if (result == ESP.Finance.Utility.UpdateResult.Succeed)
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(returnModel.RequestorID.Value);
               
                IList<ESP.Finance.Entity.ReturnInfo> returnList = Search();
                SearchHist(returnList);
                try
                {
                    ESP.Finance.Utility.SendMailHelper.SendMailReturnReDo(returnModel, CurrentUser.Name, returnModel.RequestEmployeeName, emp.EMail);
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('该付款申请已退回到申请人!"+exMail+"');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('重汇失败，请重新尝试!');", true);
            }
        }
        else if (e.CommandName == "KillCash")
        {
            //现金销账
            int returnID = int.Parse(e.CommandArgument.ToString());
            Response.Redirect("CashPNLink.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + returnID);
        }
        else  if (e.CommandName == "Journalist")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
            {
                string filename;
                string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(CurrentUserID, returnModel, serverpath, out filename, true);
                if (!string.IsNullOrEmpty(filename))
                {
                    outExcel(serverpath + filename, filename, true);
                }
            }
            else
            {
                return;
            }
        }
    }


    private void outExcel(string pathandname, string filename, bool isDelete)
    {
        if (!File.Exists(pathandname))
            return;
        FileStream fin = File.OpenRead(pathandname);
        Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
        Response.AddHeader("Connection", "Close");
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Length", fin.Length.ToString());

        byte[] buf = new byte[1024];
        while (true)
        {
            int length = fin.Read(buf, 0, buf.Length);
            if (length > 0)
                Response.OutputStream.Write(buf, 0, length);
            if (length < buf.Length)
                break;
        }
        fin.Close();
        Response.Flush();
        Response.Close();
        if (isDelete)
        {
            FileInfo finfo = new FileInfo(pathandname);
            finfo.Delete();
        }
    }
}