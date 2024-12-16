using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

namespace FinanceWeb.Purchase
{
    public partial class NewBatchPurchase : ESP.Web.UI.PageBase
    {
        int BatchID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.BatchID]))
                BatchID = int.Parse(Request[RequestName.BatchID]);

            if (!IsPostBack)
            {
                //绑定批次列表
                Search();
                SearchHist();
                BindInfo();
                PNListBind();
                NeiPNListBind();
            }
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

                ddlCompany.Enabled = ddlPaymentType.Enabled = false;
            }
            else
            {
                ddlCompany.Enabled = ddlPaymentType.Enabled = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //ESP.Finance.Entity.PNBatchInfo model = SaveInfo(false);
            //Response.Redirect("NewBatchPurchase.aspx?" + RequestName.BatchID + "=" + model.BatchID);
            PNListBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request["chkItem"]))
                {
                    string[] itemIds = Request["chkItem"].Split(',');
                    ESP.Finance.Entity.PNBatchInfo model = SaveInfo(false);
                    ESP.Finance.BusinessLogic.PNBatchManager.AddItems(model.BatchID, itemIds);
                    Response.Redirect("NewBatchPurchase.aspx?" + RequestName.BatchID + "=" + model.BatchID);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        private PNBatchInfo SaveInfo(bool isCommit)
        {
            PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);
            PNBatchInfo oldBatchModel = batchModel;
            if (batchModel == null)
            {
                batchModel = new PNBatchInfo();
                batchModel.BatchType = 1;
                batchModel.CreateDate = DateTime.Now;
                batchModel.CreatorID = int.Parse(CurrentUser.SysID);
            }
            batchModel.BranchID = int.Parse(ddlCompany.SelectedValue);
            batchModel.BranchCode = ddlCompany.SelectedItem.Text;
            //batchModel.Description = txtRemark.Text.Trim();
            batchModel.Amounts = decimal.Parse(labTotal.Text);
            int currentAduitType = 0;
            int nextAuditType = 0;
            ESP.Framework.Entity.EmployeeInfo nextAuditor = null;
            bool isLast = false;
            if (isCommit)
            {
                //设置下级审核人
                if (batchModel.Status == null || batchModel.Status == (int)PaymentStatus.PurchaseFirst)//初审人
                {
                    currentAduitType = (int)ESP.Finance.Utility.auditorType.purchase_first;
                    //if (CurrentUserID != int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId2"]))
                    //{
                    //    nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId2"]));
                    //    batchModel.Status = (int)PaymentStatus.PurchaseMajor1;
                    //    nextAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
                    //}
                    //else
                    //{
                        nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]));
                        batchModel.Status = (int)PaymentStatus.PurchaseMajor1;
                        nextAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
                   // }
                }
                //else if (batchModel.Status == (int)PaymentStatus.PurchaseMajor1)//一级总监
                //{
                //    currentAduitType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
                //    nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]));
                //    if (batchModel.Amounts > 50000)
                //    {
                //        batchModel.Status = (int)PaymentStatus.PurchaseMajor1;
                //        nextAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
                //    }
                //    else
                //    {
                //        nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode).FirstFinanceID);
                //        batchModel.Status = (int)PaymentStatus.MajorAudit;
                //        nextAuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                //    }
                //}
                else //if (batchModel.Status == (int)PaymentStatus.PurchaseMajor2)//二级总监
                {
                    currentAduitType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
                    nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode).FirstFinanceID);
                    batchModel.Status = (int)PaymentStatus.MajorAudit;
                    nextAuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                }

                batchModel.PaymentUserID = nextAuditor.UserID;
                batchModel.PaymentCode = nextAuditor.Code;
                batchModel.PaymentEmployeeName = nextAuditor.FullNameCN;
                batchModel.PaymentUserName = nextAuditor.FullNameEN;
            }
            else
            {
                if (batchModel.Status == null)
                {
                    nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(CurrentUser.SysID));
                    batchModel.PaymentUserID = nextAuditor.UserID;
                    batchModel.PaymentCode = nextAuditor.Code;
                    batchModel.PaymentEmployeeName = nextAuditor.FullNameCN;
                    batchModel.PaymentUserName = nextAuditor.FullNameEN;
                    if (CurrentUserID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId2"]))//采购一级总监创建
                        batchModel.Status = (int)PaymentStatus.PurchaseMajor1;
                    else
                        batchModel.Status = (int)PaymentStatus.PurchaseFirst;
                }
            }
            batchModel.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
            batchModel.PaymentType = ddlPaymentType.SelectedItem.Text;
            if (batchModel.BatchID == 0)
                batchModel.BatchID = ESP.Finance.BusinessLogic.PNBatchManager.Add(batchModel);
            else
            {
                if (!isCommit)
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);
                else
                {
                    ESP.Finance.BusinessLogic.PNBatchManager.BatchAuditForPurchase(oldBatchModel,batchModel, CurrentUser, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, currentAduitType, nextAuditType);
                    try
                    {
                        ESP.Finance.Utility.SendMailHelper.SendMailPurchaseBatch(true, isLast, nextAuditor.FullNameCN, nextAuditor.Email, CurrentUser, batchModel);
                    }
                    catch { }
                }
            }
            return batchModel;
        }

        protected void ddlCompany_SelectedIndexChangeed(object sender, EventArgs e)
        {
            PNListBind();
        }

        protected void ddlPaymentType_SelectedIndexChangeed(object sender, EventArgs e)
        {
            PNListBind();
        }

        #region 待审批付款申请
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
                term = " and (ReturnStatus=@status1 or  ReturnStatus=@status2) AND (a.PaymentUserID=@sysID or a.PaymentUserID in(" + DelegateUsers + "))";
            else
                term = " and (ReturnStatus=@status1 or  ReturnStatus=@status2) AND a.PaymentUserID=@sysID";
            //SqlParameter p3 = new SqlParameter("@status3", System.Data.SqlDbType.Int, 4);
            //p3.SqlValue = (int)PaymentStatus.PurchaseMajor2;
            //paramlist.Add(p3);
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.PurchaseFirst;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.PurchaseMajor1;
            paramlist.Add(p2);
            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
            if (!string.IsNullOrEmpty(term))
            {
                if (txtSupplier.Text.Trim() != string.Empty)
                {
                    term += " and a.SupplierName  like '%'+@supplierName+'%'";
                    SqlParameter supp = new SqlParameter("@supplierName", SqlDbType.NVarChar, 50);
                    supp.SqlValue = txtSupplier.Text.Trim();
                    paramlist.Add(supp);
                }
                //PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);
                //if (batchModel.Status == (int)PaymentStatus.PurchaseFirst)
                //{
                //    term += " and returnstatus=" + (int)PaymentStatus.PurchaseFirst;
                //}
                //else if (batchModel.Status == (int)PaymentStatus.PurchaseMajor1)
                //{
                //    term += " and returnstatus=" + (int)PaymentStatus.PurchaseMajor1;
                //}
                //else if (batchModel.Status == (int)PaymentStatus.PurchaseMajor2)
                //{
                //    term += " and returnstatus=" + (int)PaymentStatus.PurchaseMajor2;
                //}
                term += " and returnid not in(select returnid from f_pnbatchrelation)";
                if (ddlCompany.SelectedValue != "0")
                {
                    term += " and projectcode like ''+@BranchCode+'%' ";
                    System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                    pBrach.SqlValue = ddlCompany.SelectedItem.Text; //batchModel.BranchCode;
                    paramlist.Add(pBrach);
                }
                if (ddlPaymentType.SelectedValue != "0")
                {
                    term += " and PaymentTypeID=@PaymentTypeID";
                    System.Data.SqlClient.SqlParameter pPaymentTypeID = new System.Data.SqlClient.SqlParameter("@PaymentTypeID", System.Data.SqlDbType.Int, 4);
                    pPaymentTypeID.SqlValue = ddlPaymentType.SelectedValue; //batchModel.PaymentTypeID.Value;
                    paramlist.Add(pPaymentTypeID);
                }
                DataTable returnList = ESP.Finance.BusinessLogic.ReturnManager.GetPNTableLinkPR(term, paramlist);
                DataView viewList = returnList.DefaultView;
                viewList.Sort = "PreBeginDate DESC";
                this.GridView1.DataSource = viewList;
                this.GridView1.DataBind();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo returnModel =( ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
                DataRowView dv = (DataRowView)e.Row.DataItem;
                Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
                if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                    labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
                Label lblPR = (Label)e.Row.FindControl("lblPR");
                if (int.Parse(dv["ReturnType"].ToString()) != (int)ESP.Purchase.Common.PRTYpe.MediaPR && int.Parse(dv["ReturnType"].ToString()) != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                    lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + dv["PRid"].ToString() + "'style='cursor: hand' target='_blank'>" + dv["PRNo"].ToString() + "</a>";
                else
                    lblPR.Text = dv["PRNo"].ToString();
                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
                HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
                if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                    lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value),0,returnModel.IsDiscount);
            }
        }
        #endregion

        #region 批次内付款申请
        private void NeiPNListBind()
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(BatchID);
            GvNei.DataSource = returnList;
            GvNei.DataBind();
            decimal total = 0m;
            foreach (ReturnInfo model in returnList)
            {
                total += model.PreFee.Value;
            }
            labTotal.Text = total.ToString("#,##0.00");
        }

        protected void GvNei_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int reutrnID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.Entity.ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(reutrnID);
                ESP.Finance.BusinessLogic.PNBatchRelationManager.DeleteByReturnID(Convert.ToInt32(Request[RequestName.BatchID]),model);
                Response.Redirect("NewBatchPurchase.aspx?" + RequestName.BatchID + "=" + BatchID);
            }

        }
        protected void GvNei_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
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
                Label lblEdit = (Label)e.Row.FindControl("lblEdit");
                lblEdit.Text = "<a target='_blank' onclick='window.open(\"ReturnFinanceSave.aspx?" + RequestName.ReturnID + "=" + model.ReturnID + "&" + RequestName.BatchID + "=" + Request[RequestName.BatchID] + "\");' style='cursor: hand' target='_blank'><img  src='/images/ProjectPrint.gif' border='0px;' /></a>";
                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
                txtSupplier.Text = model.SupplierName;
            }
        }
        #endregion

        #region 批次列表
        private void Search()
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
                term = " (Status=@status1 or  Status=@status2 or Status is null) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + ")) and BatchType=1 ";
            else
                term = " (Status=@status1 or  Status=@status2 or Status is null) AND PaymentUserID=@sysID and BatchType=1 ";
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.PurchaseFirst;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.PurchaseMajor1;
            paramlist.Add(p2);
            //SqlParameter p3 = new SqlParameter("@status3", System.Data.SqlDbType.Int, 4);
            //p3.SqlValue = (int)PaymentStatus.PurchaseMajor2;
            //paramlist.Add(p3);
            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
            if (!string.IsNullOrEmpty(term))
            {
                IList<ESP.Finance.Entity.PNBatchInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramlist);
                var tmplist = returnList.OrderBy(N => N.PaymentDate);
                IList<ESP.Finance.Entity.PNBatchInfo> returnlist = tmplist.ToList();
                this.gvG.DataSource = returnlist;
                this.gvG.DataBind();
            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }
        protected void GvHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvHist.PageIndex = e.NewPageIndex;
            SearchHist();
        }
        protected void GvHist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.PNBatchInfo batchModel = (ESP.Finance.Entity.PNBatchInfo)e.Row.DataItem;
                Label lblName = (Label)e.Row.FindControl("lblName");
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(batchModel.CreatorID.Value);
                lblName.Text = emp.Name;
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(batchModel.CreatorID.Value) + "');");
                HyperLink hylDisplay = (HyperLink)e.Row.FindControl("hylDisplay");
                hylDisplay.NavigateUrl = "BatchPurchaseDisplay.aspx?" + RequestName.BatchID + "=" + batchModel.BatchID.ToString();
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = ReturnPaymentType.ReturnStatusString(batchModel.Status.Value, 0, null);
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");
                lblAmounts.Text = ESP.Finance.BusinessLogic.PNBatchManager.GetTotalAmounts(batchModel).ToString("#,##0.00");

            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int BatchID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.PNBatchManager.Delete(BatchID);
                Response.Redirect("NewBatchPurchase.aspx?" + RequestName.BatchID + "=" + BatchID);
            }
            if (e.CommandName == "Export")
            {
                int BatchID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.PNBatchManager.ExportPurchasePN(ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(BatchID), Response);
            }

        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.PNBatchInfo batchModel = (ESP.Finance.Entity.PNBatchInfo)e.Row.DataItem;
                Label lblName = (Label)e.Row.FindControl("lblName");
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(batchModel.CreatorID.Value);
                lblName.Text = emp.Name;
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(batchModel.CreatorID.Value) + "');");
                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                hylEdit.NavigateUrl = "NewBatchPurchase.aspx?" + RequestName.BatchID + "=" + batchModel.BatchID.ToString();
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = ReturnPaymentType.ReturnStatusString(batchModel.Status.Value, 0, null);
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");
                lblAmounts.Text = ESP.Finance.BusinessLogic.PNBatchManager.GetTotalAmounts(batchModel).ToString("#,##0.00");
            }
        }

        private void SearchHist()
        {
            IList<ESP.Finance.Entity.PNBatchInfo> list;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            string auditTypes = auditorType.purchase_first + "," + auditorType.purchase_major2 ;
            term = " batchid  in (select batchid from f_pnbatchrelation where returnid in (select returnid from F_ReturnauditHist where AuditorUserID=@AuditorUserID and AuditeStatus!=@AuditeStatus and auditType in (" + auditTypes + ")))  and BatchType=1 ";
            SqlParameter p1 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p1.Value = CurrentUser.SysID;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@AuditeStatus", SqlDbType.Int, 4);
            p2.Value = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            paramlist.Add(p2);
            list = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramlist);
            this.GvHist.DataSource = list;
            this.GvHist.DataBind();
        }
        #endregion
    }
}
