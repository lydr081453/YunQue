using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Collections;





namespace FinanceWeb.Purchase
{
    public partial class CreateBatchPurchase : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
                PNListBind();
            }
        }

        protected void ddlCompany_SelectedIndexChangeed(object sender, EventArgs e)
        {
            PNListBind();
        }

        protected void ddlPaymentType_SelectedIndexChangeed(object sender, EventArgs e)
        {
            PNListBind();
        }

        protected void ddlDirector_SelectedIndexChanged(object sender, EventArgs e)
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
                term = " and (ReturnStatus=@status1) AND (a.PaymentUserID=@sysID or a.PaymentUserID in(" + DelegateUsers + ")) ";
            else
                term = " and (ReturnStatus=@status1) AND a.PaymentUserID=@sysID ";

            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.PurchaseFirst;
            paramlist.Add(p1);

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

                //term += " and returnstatus=" + (int)PaymentStatus.PurchaseFirst;
                term += " and a.returnid not in(select returnid from f_pnbatchrelation)";
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
                if (ddlDirector.SelectedValue != "0")
                {
                    int defaultAuditor = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]);

                    if (defaultAuditor != int.Parse(ddlDirector.SelectedValue))
                    {
                        string depts = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetDeptByPurchaseDirector(int.Parse(ddlDirector.SelectedValue));

                        term += " and a.departmentid in(" + depts + ")";
                    }
                }

                if (txtKey.Text.Trim() != "")
                {
                    term += " and (a.returncode like '%'+@keys+'%' or b.prno like '%'+@keys+'%' or a.projectcode like '%'+@keys+'%')";
                    paramlist.Add(new SqlParameter("@keys", txtKey.Text.Trim()));
                }
                DataTable returnList = ESP.Finance.BusinessLogic.ReturnManager.GetPNTableLinkPR(term, paramlist);
                this.GridView1.DataSource = returnList;
                this.GridView1.DataBind();
                decimal total = 0m;
                foreach (DataRow dr in returnList.Rows)
                {
                    total += decimal.Parse(dr["PreFee"].ToString());
                }
                labTotal.Text = total.ToString("#,##0.00");
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

            Hashtable ht = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetPurchaseDirectorsByAuditor(CurrentUserID);
            ddlDirector.Items.Add( new ListItem("请选择", "0"));

            if (ht.Count > 0)
            {
                foreach (DictionaryEntry dic in ht)
                {
                    ddlDirector.Items.Add(new ListItem(dic.Key.ToString(), dic.Value.ToString()));
                }
            }
            else
            {
                ESP.Framework.Entity.EmployeeInfo defaultAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]));

                ddlDirector.Items.Add(new ListItem(defaultAuditor.FullNameCN.ToString(), defaultAuditor.UserID.ToString()));
            }
        }

        private PNBatchInfo SaveInfo(bool isCommit)
        {
            PNBatchInfo batchModel = null;
            if (batchModel == null)
            {
                batchModel = new PNBatchInfo();
                batchModel.BatchType = 1;
                batchModel.CreateDate = DateTime.Now;
                batchModel.CreatorID = int.Parse(CurrentUser.SysID);
            }
            if (ddlCompany.SelectedValue == "0")
                return null;
            batchModel.BranchID = int.Parse(ddlCompany.SelectedValue);
            batchModel.BranchCode = ddlCompany.SelectedItem.Text;
            batchModel.Description = txtRemark.Text.Trim();
            decimal amounts = 0;
            if (Request["chkItem"] == null)
                return null;
            IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" returnid in (" + Request["chkItem"] + ")");
            foreach (ReturnInfo model in returnList)
            {
                if (model != null)
                    amounts += model.PreFee.Value;
            }
            var operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(returnList[0].DepartmentID.Value);

            batchModel.Amounts = amounts;
            int currentAduitType = 0;
            int nextAuditType = 0;
            ESP.Framework.Entity.EmployeeInfo nextAuditor = null;
            bool isLast = false;

            currentAduitType = (int)ESP.Finance.Utility.auditorType.purchase_first;
            nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ddlDirector.SelectedValue));

            batchModel.Status = (int)PaymentStatus.PurchaseMajor1;
            nextAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
            if (string.IsNullOrEmpty(batchModel.PurchaseBatchCode))
                    batchModel.PurchaseBatchCode = ESP.Finance.BusinessLogic.PNBatchManager.CreatePurchaseBatchCode();
           

                batchModel.PaymentUserID = nextAuditor.UserID;
                batchModel.PaymentCode = nextAuditor.Code;
                batchModel.PaymentEmployeeName = nextAuditor.FullNameCN;
                batchModel.PaymentUserName = nextAuditor.FullNameEN;
           
            batchModel.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
            batchModel.PaymentType = ddlPaymentType.SelectedItem.Text;

            string[] itemIds = Request["chkItem"].Split(',');
            batchModel = ESP.Finance.BusinessLogic.PNBatchManager.AddItems(batchModel, itemIds);
            ESP.Finance.BusinessLogic.PNBatchManager.BatchAuditForPurchase(null, batchModel, CurrentUser, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, currentAduitType, nextAuditType);
            try
            {
                ESP.Finance.Utility.SendMailHelper.SendMailPurchaseBatch(true, isLast, nextAuditor.FullNameCN, nextAuditor.Email, CurrentUser, batchModel);
            }
            catch { }
            return batchModel;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["chkItem"]) || ddlCompany.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择分公司代码并选择付款申请!');", true);
                return;
            }
            if (ddlDirector.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择下一级审批人!');", true);
                return;
            }
            try
            {
            SaveInfo(true);
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('批次审批通过！');window.location='CreateBatchPurchase.aspx';", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["chkItem"]) || ddlCompany.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择分公司代码并选择付款申请!');", true);
                return;
            }
            //检查是否选择了不同的供应商和分公司
            IList<ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" returnid in(" + Request["chkItem"] + ")");
            returnList = returnList.OrderByDescending(N => N.ProjectCode.Substring(0, 1)).OrderByDescending(N => N.PRNo).ToList();
            string originalSupplier = "";
            string originalBank = "";
            string originalAccount = "";
            foreach (ReturnInfo returnModel in returnList)
            {
                if (string.IsNullOrEmpty(originalSupplier))
                {
                    originalSupplier = returnModel.SupplierName;
                    originalBank = returnModel.SupplierBankName;
                    originalAccount = returnModel.SupplierBankAccount;
                }
                else
                {
                    if (originalSupplier != returnModel.SupplierName || originalBank != returnModel.SupplierBankName || originalAccount != returnModel.SupplierBankAccount)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('您选择了不同的供应商!');", true);
                        return;
                    }
                }
            }
            ESP.Finance.BusinessLogic.PNBatchManager.ExportPurchasePN(returnList, Response);
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
                term = " (Status=@status1 or  Status=@status2 or Status is null)  and BatchType=1 ";
            else
                term = " (Status=@status1 or  Status=@status2 or Status is null) and BatchType=1 ";
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
            //}
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
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
                Label labLastTime = (Label)e.Row.FindControl("labLastTime");
                IList<ESP.Purchase.Entity.AuditLogInfo> auditList = ESP.Purchase.BusinessLogic.AuditLogManager.GetModelListByGID(Convert.ToInt32(dv["prid"]));
                if (auditList != null && auditList.Count > 0)
                {
                    labLastTime.Text = auditList[auditList.Count - 1].remarkDate.ToString();
                }
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            //DropDownList ddlUser = (DropDownList)GridView1.Rows[e.NewEditIndex].Cells[12].Controls[0]; ; //e.Row.FindControl("ddlUser");
            //ddlUser.DataSource = ESP.Purchase.BusinessLogic.TypeManager.getAllPaymentUser();
            //ddlUser.DataValueField = "paymentuserid";
            //ddlUser.DataValueField = "paymentusername";
            //ddlUser.DataBind();
            PNListBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList ddlUser = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlUser");
            ESP.Finance.BusinessLogic.ReturnManager.ChangedPaymentUser(int.Parse(ddlUser.SelectedValue), int.Parse(((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text));
            GridView1.EditIndex = -1;
            PNListBind();
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
    }
}
