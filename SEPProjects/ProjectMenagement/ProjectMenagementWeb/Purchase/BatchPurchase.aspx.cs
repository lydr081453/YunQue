using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Data;

public partial class Purchase_BatchPurchase : ESP.Web.UI.PageBase
{
    int BatchID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.BatchID]))
            BatchID = int.Parse(Request[RequestName.BatchID]);
        if (!string.IsNullOrEmpty(hidBatchId.Value))
            BatchID = int.Parse(hidBatchId.Value);
        if (!IsPostBack)
        {
            BindInfo();
            ListBind();
            PNListBind();
        }
    }

    private void ListBind()
    {
        if (!string.IsNullOrEmpty(Request[RequestName.BatchID]))
            BatchID = int.Parse(Request[RequestName.BatchID]);
        ESP.Finance.Entity.PNBatchInfo batchModel = null;

        if (BatchID != 0)
        {
            batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);
        }
        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(BatchID);
        gvG.DataSource = returnList;
        gvG.DataBind();
        decimal total = 0m;
        foreach (ReturnInfo model in returnList)
        {
            total += model.PreFee.Value;
        }
        labTotal.Text = total.ToString("#,##0.00");
        if (batchModel != null && batchModel.BatchType != 3)
            ddlCompany.Enabled = ddlPaymentType.Enabled = returnList.Count == 0;
    }

    protected void ddlCompany_SelectedIndexChangeed(object sender, EventArgs e)
    {
        PNListBind();
    }

    protected void ddlPaymentType_SelectedIndexChangeed(object sender, EventArgs e)
    {
        PNListBind();
    }

    private void PNListBind()
    {
        //if (BatchID == 0)
        //    return;
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
            PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);
            if (batchModel.BatchType != 3)
            {
                if (batchModel != null)
                {
                    if (batchModel.Status == (int)PaymentStatus.PurchaseFirst)
                    {
                        term += " and returnstatus=" + (int)PaymentStatus.PurchaseFirst;
                    }
                    else if (batchModel.Status == (int)PaymentStatus.PurchaseMajor1)
                    {
                        term += " and returnstatus=" + (int)PaymentStatus.PurchaseMajor1;
                    }
                    //else if (batchModel.Status == (int)PaymentStatus.PurchaseMajor2)
                    //{
                    //    term += " and returnstatus=" + (int)PaymentStatus.PurchaseMajor2;
                    //}
                }
                else
                {
                    term += " and returnstatus=" + (int)PaymentStatus.PurchaseFirst;
                }
                term += " and a.returnid not in(select returnid from f_pnbatchrelation)";
                if (ddlCompany.SelectedValue != "0")
                {
                    term += " and a.projectcode like ''+@BranchCode+'%' ";
                    System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                    pBrach.SqlValue = ddlCompany.SelectedItem.Text; //batchModel.BranchCode;
                    paramlist.Add(pBrach);
                }
                if (ddlPaymentType.SelectedValue != "0")
                {
                    term += " and a.PaymentTypeID=@PaymentTypeID";
                    System.Data.SqlClient.SqlParameter pPaymentTypeID = new System.Data.SqlClient.SqlParameter("@PaymentTypeID", System.Data.SqlDbType.Int, 4);
                    pPaymentTypeID.SqlValue = ddlPaymentType.SelectedValue; //batchModel.PaymentTypeID.Value;
                    paramlist.Add(pPaymentTypeID);
                }
                if (txtKey.Text.Trim() != "")
                {
                    term += " and (a.returncode like '%'+@keys+'%' or b.prno like '%'+@keys+'%' or a.projectcode like '%'+@keys+'%')";
                    paramlist.Add(new SqlParameter("@keys", txtKey.Text.Trim()));
                }
                DataTable returnList = ESP.Finance.BusinessLogic.ReturnManager.GetPNTableLinkPR(term, paramlist);
                DataView viewList = returnList.DefaultView;
                viewList.Sort = "PreBeginDate DESC";
                this.GridView1.DataSource = viewList;
                this.GridView1.DataBind();
            }
        }
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
                Response.Redirect("BatchPurchase.aspx?" + RequestName.BatchID + "=" + model.BatchID);
                //string script = @"window.location='BatchPurchase.aspx?" + RequestName.BatchID + "=" + BatchID + "';";
                //ScriptManager.RegisterStartupScript(this, Page.GetType(), Guid.NewGuid().ToString(), "alert('添加成功!');" + script, true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
        }
    }

    private void BindInfo()
    {
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
            if (batchModel.Status != (int)PaymentStatus.PurchaseFirst)//非待物料审核人状态，隐藏待审核pn列表
            {
                palPN.Visible = false;
                btnNo1.Visible = true;
            }

            labCreateUser.Text = ESP.Framework.BusinessLogic.EmployeeManager.Get(batchModel.CreatorID.Value).FullNameCN;
            labPurchaeBatchCode.Text = batchModel.PurchaseBatchCode;
        }
        getAuditLog(batchModel.BatchID);
    }


    private void getAuditLog(int batchID)
    {
        IList<AuditLogInfo> logList = ESP.Finance.BusinessLogic.AuditLogManager.GetBatchList(batchID);
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
    private PNBatchInfo SaveInfo(bool isCommit)
    {
        PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);
        PNBatchInfo oldBatchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);

        var returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(BatchID);

        var operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(returnlist[0].DepartmentID.Value);

        if (batchModel == null)
        {
            batchModel = new PNBatchInfo();
            batchModel.BatchType = 1;
            batchModel.CreateDate = DateTime.Now;
            batchModel.CreatorID = int.Parse(CurrentUser.SysID);
        }
        batchModel.BranchID = int.Parse(ddlCompany.SelectedValue);
        batchModel.BranchCode = ddlCompany.SelectedItem.Text;
        batchModel.Description = txtRemark.Text.Trim();
        batchModel.Amounts = decimal.Parse(labTotal.Text);
        int currentAduitType = 0;
        int nextAuditType = 0;
        ESP.Framework.Entity.EmployeeInfo nextAuditor = null;
        bool isLast = false;
        if (isCommit)
        {
            if (batchModel.Status == (int)PaymentStatus.PurchaseFirst)
            {
                int defaultAuditor = operationModel.PurchaseDirectorId;

                if (defaultAuditor == 0)
                {
                    defaultAuditor = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]);
                }
                currentAduitType = (int)ESP.Finance.Utility.auditorType.purchase_first;
                nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(defaultAuditor);
                batchModel.Status = (int)PaymentStatus.PurchaseMajor1;
                nextAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
            }
            else
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
        //else
        //{
        //    if (batchModel.Status == null)
        //    {
        //        nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(CurrentUser.SysID));
        //        batchModel.PaymentUserID = nextAuditor.UserID;
        //        batchModel.PaymentCode = nextAuditor.Code;
        //        batchModel.PaymentEmployeeName = nextAuditor.FullNameCN;
        //        batchModel.PaymentUserName = nextAuditor.FullNameEN;

        //        batchModel.Status = (int)PaymentStatus.PurchaseMajor1;

        //    }
        //}
        //batchModel.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
        //batchModel.PaymentType = ddlPaymentType.SelectedItem.Text;
        //if (batchModel.BatchID == 0)
        //    batchModel.BatchID = ESP.Finance.BusinessLogic.PNBatchManager.Add(batchModel);
        //else
        //{
            if (!isCommit)
                ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);
            else
            {
                ESP.Finance.BusinessLogic.PNBatchManager.BatchAuditForPurchase(oldBatchModel, batchModel, CurrentUser, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, currentAduitType, nextAuditType);
                try
                {
                    ESP.Finance.Utility.SendMailHelper.SendMailPurchaseBatch(true, isLast, nextAuditor.FullNameCN, nextAuditor.Email, CurrentUser, batchModel);
                }
                catch { }
            }
        //}
        return batchModel;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ESP.Finance.Entity.PNBatchInfo model = SaveInfo(false);
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('批次保存成功！');window.location='BatchPurchase.aspx?" + RequestName.BatchID + "=" + model.BatchID + "';", true);
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (gvG.Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请添加批次内付款申请!');", true);
            return;
        }
        ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);
        if (batchModel.BatchType != 3)
        {
            //检查批次内是否包含被停用、暂停的申请单
            IList<ReturnInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(BatchID);
            foreach (ReturnInfo returnModel in returnList)
            {
                ESP.Purchase.Entity.GeneralInfo model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
                string alertMessage = "";
                if (model.InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
                {
                    if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.Disable)
                        alertMessage = returnModel.ReturnCode + "所属申请单被暂停，该批次不能审批通过，请驳回批次。";
                    else if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.DisableProject)
                        alertMessage = returnModel.ReturnCode + "所属项目被暂停，该批次不能审批通过，请驳回批次。";
                }
                if (model.status == ESP.Purchase.Common.State.requisition_Stop)
                    alertMessage = returnModel.ReturnCode + "所属申请单被停止，该批次不能审批通过，请驳回批次。";
                if (alertMessage != "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMessage + "');", true);
                    return;
                }
            }
        }
        SaveInfo(true);
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('批次审批通过！');window.location='BatchPurchaseList.aspx';", true);
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (ESP.Finance.BusinessLogic.PNBatchManager.returnBatchForPurchase(BatchID, CurrentUser, this.txtRemark.Text.Trim()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('审批驳回成功！');window.location='BatchPurchaseList.aspx';", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('批次驳回失败！');", true);
        }
    }

    protected void btnNo1_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(BatchID);
        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(batchModel.CreatorID.Value);
        batchModel.PaymentUserID = Convert.ToInt32(emp.SysID);
        batchModel.PaymentUserName = emp.ITCode;
        batchModel.PaymentCode = emp.ID;
        batchModel.PaymentEmployeeName = emp.Name;
        batchModel.Description = txtRemark.Text.Trim();
        batchModel.Status = (int)PaymentStatus.PurchaseFirst;
        int result = ESP.Finance.BusinessLogic.PNBatchManager.BatchAudit(batchModel, null, "", CurrentUser, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing, false);
        if (result == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('审批驳回成功！');window.location='BatchPurchaseList.aspx';", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('批次驳回失败！');", true);
        }
    }

    protected void btnEditList_Click(object sender, EventArgs e)
    {
        ESP.Finance.Entity.PNBatchInfo model = SaveInfo(false);
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.open('returnDlgForPurchase.aspx?" + RequestName.BatchID + "=" + model.BatchID + "');window.location='BatchPurchase.aspx?" + RequestName.BatchID + "=" + model.BatchID + "'", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PNListBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BatchPurchaseList.aspx");
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
            term = " (Status=@status1 or  Status=@status2 or Status is null ) and batchType in(1,3) ";
        else
            term = " (Status=@status1 or  Status=@status2 or Status is null ) and batchType in(1,3) ";
        SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)PaymentStatus.PurchaseFirst;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = (int)PaymentStatus.PurchaseMajor1;
        paramlist.Add(p2);

        //if (CurrentUser.SysID != ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"])
        //{
            if (!string.IsNullOrEmpty(DelegateUsers))
                term += " AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
            else
                term += " AND PaymentUserID=@sysID";

            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
       // }
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

    private bool ValidAuditedBySingle(int returnID)
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
            term = " (returnStatus=@status1) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
        else
            term = " (returnStatus=@status1) AND PaymentUserID=@sysID";
        SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)PaymentStatus.PurchaseFirst;
        paramlist.Add(p1);
        SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
        p6.SqlValue = CurrentUser.SysID;
        paramlist.Add(p6);
        IList<ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
        foreach (ReturnInfo pro in returnList)
        {
            if (pro.ReturnID == returnID)
            {
                return true;
            }
        }
        return false;
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int reutrnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(reutrnID);
            ESP.Finance.BusinessLogic.PNBatchRelationManager.DeleteByReturnID(Convert.ToInt32(Request[RequestName.BatchID]), model);
            ListBind();
            PNListBind();
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
                lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, null);
            Literal litGRNo = (Literal)e.Row.FindControl("litGRNo");
            DataTable recpientList = ESP.Finance.BusinessLogic.ReturnManager.GetRecipientIds(dv["returnid"].ToString());
            foreach (DataRow dr in recpientList.Rows)
            {
                litGRNo.Text += "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\Print\\MultiRecipientPrint.aspx?newPrint=true&id=" + dr["recipientid"] + "' target='_blank'>" + dr["recipientNo"].ToString() + "</a>&nbsp;";
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Return")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            if (!ValidAuditedBySingle(returnID))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
                return;
            }
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(returnModel.RequestorID.Value);
            returnModel.PaymentUserID = Convert.ToInt32(emp.SysID);
            returnModel.PaymentUserName = emp.ITCode;
            returnModel.PaymentCode = emp.ID;
            returnModel.PaymentEmployeeName = emp.Name;
            returnModel.ReturnStatus = (int)PaymentStatus.Save;
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
            SaveHistory(returnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing, 0);
            PNListBind();
        }
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
                auditHist.Suggestion = "物料审核驳回" + "[" + CurrentUser.Name + "为" + DelegateUserNames + "的代理人]";
            else
                auditHist.Suggestion = "物料审核驳回";
            auditHist.AuditorUserCode = CurrentUser.ID;
            auditHist.AuditorUserName = CurrentUser.ITCode;
            auditHist.AuditorEmployeeName = CurrentUser.Name;
            auditHist.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(auditHist);
        }
        else
        {//否则更新审批历史
            auditHist = auditList[0];
            auditHist.AuditeDate = DateTime.Now;
            auditHist.AuditeStatus = Status;
            if (!string.IsNullOrEmpty(DelegateUsers) && CurrentUser.Name != auditList[0].AuditorEmployeeName)
                auditHist.Suggestion = "物料审核驳回" + "[" + CurrentUser.Name + "为" + auditList[0].AuditorEmployeeName + "的代理人]";
            else
                auditHist.Suggestion = "物料审核驳回";
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

            if (model.ReturnStatus != (int)PaymentStatus.PurchaseFirst)
            {
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.Visible = false;
            }
            Literal litGRNo = (Literal)e.Row.FindControl("litGRNo");
            DataTable recpientList = ESP.Finance.BusinessLogic.ReturnManager.GetRecipientIds(model.ReturnID.ToString());
            foreach (DataRow dr in recpientList.Rows)
            {
                litGRNo.Text += "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\Print\\MultiRecipientPrint.aspx?newPrint=true&id=" + dr["recipientid"] + "' target='_blank'>" + dr["recipientNo"].ToString() + "</a>&nbsp;";
            }
        }
    }
}
