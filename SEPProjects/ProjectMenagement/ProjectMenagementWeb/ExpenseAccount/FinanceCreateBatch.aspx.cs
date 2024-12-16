/************************************************************************\
 * 审核列表页
 * 
 * 根据登录人不同角色，列出所有此人待审核的报销单
 *       
 *
\************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Entity;

namespace FinanceWeb.ExpenseAccount
{
    public partial class FinanceCreateBatch : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;

        bool isFinances = false;
        bool isF1 = false;

        int batchid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request["BatchID"]))
            {
                batchid = Convert.ToInt32(Request["BatchID"]);
            }
            if (!IsPostBack)
            {
                bindBranch();
                bindReturnType();

                if (batchid > 0)
                {
                    panBatch.Visible = true;
                    bindbatchInfo();

                    bindList();
                    bindBatchList();
                    btnAuditBatch.Visible = true;
                }
                else
                {
                    panBatch.Visible = false;
                    btnAuditBatch.Visible = false;
                }
            }
        }

        protected void btnReLoad_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["BatchID"]))
            {
                batchid = Convert.ToInt32(Request["BatchID"]);
            }

            if (batchid > 0)
            {
                panBatch.Visible = true;
                bindbatchInfo();
                bindBranch();
                bindReturnType();
                bindList();
                bindBatchList();
            }
            else
            {
                panBatch.Visible = false;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinanceCreateBatchList.aspx");
        }

        private void bindList()
        {
            string whereStr = "";
            // and wi.WorkflowName != 'Charge'
            whereStr += string.Format(" and wi.WorkItemName != '收货' and (wi.workitemname = '财务第一级审批')  and wa.assigneeid in ({0}) and wi.status = {1} ", GetDelegateUser(), (int)ESP.Workflow.WorkItemStatus.Open);

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                whereStr += string.Format(" and ( r.ProjectCode like '%{0}%' or r.ReturnCode like '%{0}%' or r.RequestEmployeeName like '%{0}%' or r.PreFee like '%{0}%' or  r.PaymentTypeCode like '%{0}%' )", txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPNList.Text.Trim()))
            {
                string[] pnlist = txtPNList.Text.Trim().Split(',');
                string pnsql = " and r.ReturnCode in(";

                foreach (string pn in pnlist)
                {
                    if (!string.IsNullOrEmpty(pn))
                        pnsql += "'" + pn + "',";
                }

                pnsql = pnsql.Substring(0, pnsql.Length - 1) + ")";

                whereStr += pnsql;
            }
            if (!string.IsNullOrEmpty(ddlBranch.SelectedValue) && !"0".Equals(ddlBranch.SelectedValue))
            {
                whereStr += string.Format(" and r.BranchCode = '{0}' ", ddlBranch.SelectedItem.Text);
            }
            if (!string.IsNullOrEmpty(ddlReturnType.SelectedValue) && !"0".Equals(ddlReturnType.SelectedValue))
            {
                whereStr += string.Format(" and r.ReturnType = '{0}' ", ddlReturnType.SelectedValue);
            }

            whereStr += string.Format(" and r.ReturnID not in (select returnid from F_PNBatchRelation)");
            //DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditList(whereStr, " and (a.LastAuditPassTime < a.ExpenseCommitDeadLine or a.LastAuditPassTime < a.ExpenseCommitDeadLine2 or a.ReturnType != 30  or  (a.ParticipantName != '财务第一级' and a.ParticipantName != '财务第二级'  and a.ParticipantName != '财务第三级'  and a.ParticipantName != 'FA' ) ) ");
            DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditList(whereStr, "");
            isFinances = true;
            isF1 = true;

            btnExport.Visible = false;

            GridNoNeed.DataSource = ds.Tables[0];
            GridNoNeed.DataBind();



        }

        protected void ddlBranch_SelectedIndexChangeed(object sender, EventArgs e)
        {
            BindBank();
            bindList();
            bindBatchList();
        }

        private void BindBank()
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
            if (!string.IsNullOrEmpty(Request["BatchID"]))
            {
                batchid = Convert.ToInt32(Request["BatchID"]);
                ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                foreach (ESP.Finance.Entity.BankInfo bank in paylist)
                {
                    if (batchModel.BankID == bank.BankID)
                    {
                        this.ddlBank.SelectedValue = bank.BankID.ToString();
                        this.lblAccountName.Text = bank.BankAccountName;
                        this.lblAccount.Text = bank.BankAccount;
                        this.lblBankAddress.Text = bank.Address;
                        break;
                    }

                }
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

        private void bindBatchList()
        {
            string whereStr = "";
            whereStr += string.Format(" and wi.WorkItemName != '收货' and (wi.workitemname = '财务第一级审批')  and wa.assigneeid in ({0}) and wi.status = {1} ", GetDelegateUser(), (int)ESP.Workflow.WorkItemStatus.Open);
            whereStr += string.Format(" and r.ReturnID in (select returnid from F_PNBatchRelation  where batchid = {0} )", batchid);
            DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditList(whereStr, "");

            gvInBatch.DataSource = ds.Tables[0];
            gvInBatch.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
            bindBatchList();
        }

        protected string getUrl(string webpage, string workitemid, string workMonth)
        {
            var sum = "<a href='" + webpage + "&workitemid=" + workitemid + "'><img src='images/Audit.gif' /></a>";
            if (workMonth != "1.本次处理" && isFinances)
            {
                return "";
            }
            return sum;
        }

        protected string getPrintUrl(string returnID, string returnType)
        {
            var sum = "";
            if (Convert.ToInt32(returnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty && Convert.ToInt32(returnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
            {
                sum = "<a href='Print/ExpensePrint.aspx?expenseID=" + returnID + "' target='_blank'><img src='/images/Icon_Output.gif' /></a>";
            }
            else
            {
                sum = "<a href='Print/ThirdPartyPrint.aspx?expenseID=" + returnID + "' target='_blank'><img src='/images/Icon_Output.gif' /></a>";
            }
            return sum;
        }

        protected string getEditUrl(string workitemid)
        {
            return "<a href='FinanceBatchAuditEditByOpen.aspx?workitemid=" + workitemid + "' target='_blank'><img src='/images/edit.gif' /></a>";
        }

        protected string getIsBatchInput(decimal preFee, string workitemid, decimal confirmFee, string returntype, string workMonth)
        {
            bool isBatchAudit = true;

            if (Convert.ToInt32(returntype) == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
            {
                isBatchAudit = true;
            }
            else
            {

                if (confirmFee > 0 && preFee != confirmFee)
                {
                    isBatchAudit = false;
                }
                else
                {
                    isBatchAudit = true;
                }
            }

            if (isBatchAudit && ("1.本次处理".Equals(workMonth) || !isFinances))
            {

                return "<input type=\"checkbox\" id=\"chkAudit\" name=\"chkAudit\" value='" + workitemid + "' />";
            }
            return "";
        }

        /// <summary>
        /// 创建及保存批次信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateBatch_Click(object sender, EventArgs e)
        {
            if (ESP.Finance.BusinessLogic.PNBatchManager.CheckBatchCode(txtBatchNo.Text.Trim(), batchid))
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次号已存在，请检查！');", true);
                return;
            }

            //设置财务下级审批人参数
            List<int> nextAuditerList = new List<int>();
            nextAuditerList.Add(Convert.ToInt32(hidNextAuditor.Value));
            ESP.Compatible.Employee nextEmpInfo = new ESP.Compatible.Employee(Convert.ToInt32(hidNextAuditor.Value));

            ESP.Finance.Entity.PNBatchInfo batchModel = null;

            //try
            //{
                if (batchid == 0)
                {
                    batchModel = new ESP.Finance.Entity.PNBatchInfo();
                    batchModel.BatchCode = txtBatchNo.Text.Trim();
                    batchModel.CreateDate = DateTime.Now;
                    batchModel.CreatorID = UserInfo.UserID;
                    batchModel.Status = (int)PaymentStatus.FAAudit;
                    batchModel.PaymentUserID = nextEmpInfo.IntID;
                    batchModel.PaymentCode = nextEmpInfo.ID;
                    batchModel.PaymentEmployeeName = nextEmpInfo.Name;
                    batchModel.PaymentUserName = nextEmpInfo.ITCode;
                    batchModel.BatchType = 2;

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
                    batchModel.PurchaseBatchCode = ESP.Finance.BusinessLogic.PNBatchManager.CreatePurchaseBatchCode();
                    //添加批次
                    batchid = ESP.Finance.BusinessLogic.PNBatchManager.Add(batchModel);

                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('创建批次" + batchModel.PurchaseBatchCode + "成功!');window.location='FinanceCreateBatch.aspx?BatchID=" + batchid + "';", true);
                    return;
                }
                else
                {
                    batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                    batchModel.PaymentUserID = nextEmpInfo.IntID;
                    batchModel.PaymentCode = nextEmpInfo.ID;
                    batchModel.PaymentEmployeeName = nextEmpInfo.Name;
                    batchModel.PaymentUserName = nextEmpInfo.ITCode;
                    batchModel.BatchCode = txtBatchNo.Text.Trim();
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
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);

                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');window.location='FinanceCreateBatch.aspx?BatchID=" + batchModel.BatchID + "';", true);
                    return;
                }
            //}
            //catch (Exception ex)
            //{
            //    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');window.location='FinanceCreateBatch.aspx?BatchID=" + batchid + "';", true);
            //    return;
            //}
        }

        /// <summary>
        /// 添加到批次中去
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddBatch_Click(object sender, EventArgs e)
        {
            string[] workItemIds = GridNoNeed.SelectedKeys;
            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            if (workItemIds.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择要添加到批次中的报销单！');", true);
                return;
            }

            int workitemid = 0;
            string returnids = "";
            int stepReturnType = 0;
            int stepPonter = 1;
            string stepBranchCode = "";

            List<ESP.Finance.Entity.PNBatchRelationInfo> batchRelationList = (List<ESP.Finance.Entity.PNBatchRelationInfo>)ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchID=" + batchid, null);
            int batchReturnType = 0;
            string batchBranchCode = "";
            string steprecipient = string.Empty;
            string recipient = string.Empty;
            if (batchRelationList.Count > 0)
            {
                batchReturnType = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctReturnTypeByBatchID(batchid);
                batchBranchCode = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctBranchCodeByBatchID(batchid);
                steprecipient = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctRecipientByBatchID(batchid);
            }

            for (int i = 0; i < workItemIds.Length; i++)
            {
                workitemid = Convert.ToInt32(workItemIds[i]);
                if (workitemid > 0)
                {

                    workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(workitemid);
                    model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);

                    if (stepPonter == 1)
                    {
                        stepReturnType = model.ReturnType.Value;
                        stepBranchCode = model.BranchCode;
                        steprecipient = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctRecipientByReturnID(model.ReturnID);
                    }
                    stepPonter++;

                    try
                    {

                        if (stepReturnType != model.ReturnType.Value)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.ReturnCode + "单据类型不同，无法添加到批次，请检查！');", true);
                            return;
                        }

                        if (stepBranchCode != model.BranchCode)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.ReturnCode + "项目号公司不同，无法添加到批次，请检查！');", true);
                            return;
                        }
                        if (stepReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                        {
                            recipient = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctRecipientByReturnID(model.ReturnID);
                            if ((!string.IsNullOrEmpty(steprecipient)) && steprecipient != recipient)
                            {
                                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.ReturnCode + "支票电汇的收款人不同，无法添加到批次，请检查！');", true);
                                return;
                            }
                        }

                        if (batchRelationList.Count > 0)
                        {
                            if (batchReturnType != 0)
                            {
                                if (batchReturnType != model.ReturnType.Value)
                                {
                                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.ReturnCode + "单据类型不同，无法添加到批次，请检查！');", true);
                                    return;
                                }
                            }

                            if (batchBranchCode != "")
                            {
                                if (batchBranchCode != model.BranchCode)
                                {
                                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.ReturnCode + "项目号公司不同，无法添加到批次，请检查！');", true);
                                    return;
                                }
                            }

                            if (batchReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                            {
                                recipient = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctRecipientByReturnID(model.ReturnID);
                                if (steprecipient != recipient)
                                {
                                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.ReturnCode + "支票电汇的收款人不同，无法添加到批次，请检查！');", true);
                                    return;
                                }
                            }
                        }

                        decimal ooptotal = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalOOPByReturnID(model.ReturnID);
                        decimal costtotal = model.PreFee.Value - ooptotal;
                        //if (ESP.Finance.BusinessLogic.ExpenseAccountManager.ValidateMoney(model, costtotal, ooptotal))
                        //{
                        returnids += model.ReturnID + ",";
                        //}
                        //else
                        //{
                        //    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.ReturnCode + "项目成本金额不足，无法添加到批次，请检查！');", true);
                        //    return;
                        //}


                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加批次操作失败，请重试！');", true);
                        return;
                    }
                }
            }
            if (ESP.Finance.BusinessLogic.PNBatchManager.InsertExpenseAccoutToBatch(batchModel, returnids.TrimEnd(',').Split(',')) == true)
            {
                //alert('添加批次" + batchModel.BatchCode + "明细成功!');
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location='FinanceCreateBatch.aspx?BatchID=" + batchModel.BatchID + "';", true);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加批次明细失败，请重试！');", true);
                return;
            }
        }

        /// <summary>
        /// 从批次中移除 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemoveBatch_Click(object sender, EventArgs e)
        {
            string[] workItemIds = gvInBatch.SelectedKeys;
            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            if (workItemIds.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择要移除的报销单！');", true);
                return;
            }

            int workitemid = 0;
            string returnids = "";


            for (int i = 0; i < workItemIds.Length; i++)
            {
                workitemid = Convert.ToInt32(workItemIds[i]);
                if (workitemid > 0)
                {
                    try
                    {
                        workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(workitemid);
                        model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                        returnids += model.ReturnID + ",";
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('移除操作失败，请重试！');", true);
                        return;
                    }
                }
            }
            if (ESP.Finance.BusinessLogic.PNBatchManager.RemoveExpenseAccoutInBatch(batchModel, returnids.TrimEnd(',').Split(',')) == true)
            {
                //alert('移除批次" + batchModel.BatchCode + "明细成功!');
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location='FinanceCreateBatch.aspx?BatchID=" + batchModel.BatchID + "';", true);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('移除批次明细失败，请重试！');", true);
                return;
            }
        }

        protected void btnAuditBatch_Click(object sender, EventArgs e)
        {
            string whereStr = "";
            whereStr += string.Format(" and wi.WorkItemName != '收货' and (wi.workitemname = '财务第一级审批')  and wa.assigneeid in ({0}) and wi.status = {1} ", GetDelegateUser(), (int)ESP.Workflow.WorkItemStatus.Open);
            whereStr += string.Format(" and r.ReturnID in (select returnid from F_PNBatchRelation  where batchid = {0} )", batchid);
            DataTable dt = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditList(whereStr, "").Tables[0];

            if (dt.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次中没有单据，请添加后重试！');window.location='FinanceCreateBatch.aspx?batchid=" + batchid + "';", true);
                return;
            }

            string workitemids = "";
            foreach (DataRow dr in dt.Rows)
            {
                workitemids += dr["WorkItemID"] + ",";
            }

            string returnids = "";
            List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList = new List<ESP.Finance.Entity.ExpenseAccountBatchAudit>();
            ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo = null;

            if (!string.IsNullOrEmpty(workitemids) && workitemids.TrimEnd(',').Split(',').Length > 0)
            {
                int workitemid = 0;
                for (int i = 0; i < workitemids.TrimEnd(',').Split(',').Length; i++)
                {
                    workitemid = Convert.ToInt32(workitemids.TrimEnd(',').Split(',')[i]);
                    if (workitemid > 0)
                    {
                        auditInfo = new ESP.Finance.Entity.ExpenseAccountBatchAudit();

                        workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(workitemid);
                        model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                        returnids += model.ReturnID + ",";
                        auditInfo.Model = model;
                        auditInfo.Workitem = workitem;

                        try
                        {
                            //设置审核日志记录
                            ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                            logModel.AuditeDate = DateTime.Now;
                            logModel.AuditorEmployeeName = CurrentUser.Name;
                            logModel.AuditorUserID = Convert.ToInt32(CurrentUser.SysID);
                            logModel.AuditorUserCode = CurrentUser.ID;
                            logModel.AuditorUserName = CurrentUser.ITCode;
                            logModel.AuditType = GetStep();
                            logModel.ExpenseAuditID = model.ReturnID;
                            logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                            logModel.Suggestion = "财务批次审批";

                            int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);
                            if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                            {
                                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                                logModel.Suggestion += "(" + CurrentUser.Name + "(" + CurrentUser.ITCode + ")" + " 代理 " + emp.Name + ")";
                            }

                            //设置工作流委托方法的参数
                            Dictionary<string, object> prarms = new Dictionary<string, object>() 
                            { 
                                { "EntID", model.ReturnID } ,
                                { "ReturnStatus",  GetStatus()} ,
                                { "LogModel", logModel }
                            };
                            auditInfo.Prarms = prarms;
                            auditInfo.CurrentUserId = assigneeID;

                            auditList.Add(auditInfo);

                        }
                        catch (Exception ex)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次审批操作失败，请重试！');", true);
                            return;
                        }
                    }

                }


                //设置财务下级审批人参数
                List<int> nextAuditerList = new List<int>();
                nextAuditerList.Add(Convert.ToInt32(hidNextAuditor.Value));
                ESP.Compatible.Employee nextEmpInfo = new ESP.Compatible.Employee(Convert.ToInt32(hidNextAuditor.Value));

                ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                batchModel.BatchCode = txtBatchNo.Text.Trim();
                batchModel.Status = GetStatus();
                batchModel.PaymentUserID = nextEmpInfo.IntID;
                batchModel.PaymentCode = nextEmpInfo.ID;
                batchModel.PaymentEmployeeName = nextEmpInfo.Name;
                batchModel.PaymentUserName = nextEmpInfo.ITCode;

                if (ESP.Finance.BusinessLogic.PNBatchManager.AddItemsByExpenseAccount(batchModel, auditList, nextAuditerList, IsNeedFinanceDirectorAudit(batchModel.BatchID)))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次" + batchModel.BatchCode + "审批成功!');window.location='FinanceCreateBatchList.aspx';", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次审批操作失败，请重试！');", true);
                    return;
                }



            }


        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string workitemids = hidWorkItemID.Value;
            string whereStr = "";

            whereStr += string.Format(" and wi.WorkItemName != '收货' and wi.status = {0} ", (int)ESP.Workflow.WorkItemStatus.Open);
            whereStr += string.Format(" and wi.WorkItemId in({0}) ", workitemids.TrimEnd(','));

            DataTable dt = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetExportExpenseDetail(whereStr).Tables[0];

            ESP.Finance.BusinessLogic.ExpenseAccountManager.ExportExpenseAccountDetail(dt, Response);
        }

        #region
        protected int GetStep()
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=pa") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                }
                else if (workitem.WebPage.IndexOf("step=pm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_XMFZ;
                }
                else if (workitem.WebPage.IndexOf("step=mj") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                }
                else if (workitem.WebPage.IndexOf("step=gm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                }
                else if (workitem.WebPage.IndexOf("step=ceo") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;
                }
                else if (workitem.WebPage.IndexOf("step=fa") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_FA;
                }
                else if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3;
                }
                else if (workitem.WebPage.IndexOf("step=fm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_FinancialMajor;
                }
                else if (workitem.WebPage.IndexOf("step=ra") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FJ_Recived;
                }
            }
            return 0;
        }

        protected int GetStatus()
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=pa") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.PrepareAudit;
                }
                else if (workitem.WebPage.IndexOf("step=pm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.ProjectManagerAudit;
                }
                else if (workitem.WebPage.IndexOf("step=mj") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                }
                else if (workitem.WebPage.IndexOf("step=gm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.GeneralManagerAudit;
                }
                else if (workitem.WebPage.IndexOf("step=ceo") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.CEOAudit;
                }
                else if (workitem.WebPage.IndexOf("step=fa") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FAAudit;
                }
                else if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    if (IsNeedFinanceDirectorAudit(batchid))
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel1;
                    }
                    else
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel3;
                    }
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel3;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    if (model.ReturnType == 32 || (model.ReturnType == 31 && model.IsFixCheque == true))
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving;
                    }
                    else
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    }
                }
                else if (workitem.WebPage.IndexOf("step=f4") > 0)
                {
                    if (model.ReturnType == 32 || (model.ReturnType == 31 && model.IsFixCheque == true))
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving;
                    }
                    else
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    }
                }
                else if (workitem.WebPage.IndexOf("step=fm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.EddyAudit;
                }
                else if (workitem.WebPage.IndexOf("step=rv") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.Recived;
                }
            }
            return 0;
        }
        #endregion



        private bool IsNeedFinanceDirectorAudit(int bid)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(bid);
            bool retval = false;

            if (returnlist != null && returnlist.Count > 0)
            {
                foreach (var ret in returnlist)
                {
                    if (ret.PreFee >= 100000)
                    {
                        retval = true;
                        break;
                    }

                }
            }
            return retval;
        }


        #region 绑定基础信息
        /// <summary>
        /// 绑定批次信息
        /// </summary>
        protected void bindbatchInfo()
        {
            if (batchid > 0)
            {
                ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                txtBatchNo.Text = batchModel.BatchCode;
                hidNextAuditor.Value = batchModel.PaymentUserID.ToString();
                txtNextAuditor.Text = batchModel.PaymentEmployeeName;
                lblBatchId.Text = batchModel.BatchID.ToString();
                lblPurchaseBatchCode.Text = batchModel.PurchaseBatchCode;
            }
        }

        /// <summary>
        /// 绑定公司
        /// </summary>
        protected void bindBranch()
        {
            //string whereStr = string.Format(" FirstFinanceID = {0}", CurrentUserID);
            List<ESP.Finance.Entity.BranchInfo> list = (List<ESP.Finance.Entity.BranchInfo>)ESP.Finance.BusinessLogic.BranchManager.GetList("");

            ddlBranch.DataSource = list;
            ddlBranch.DataTextField = "BranchCode";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("请选择..", "0"));

            if (!string.IsNullOrEmpty(Request["BatchID"]))
            {
                batchid = Convert.ToInt32(Request["BatchID"]);
                ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                foreach (ESP.Finance.Entity.BranchInfo branch in list)
                {
                    if (batchModel.BranchID == branch.BranchID)
                    {
                        this.ddlBranch.SelectedValue = branch.BranchID.ToString();
                        BindBank();
                        break;
                    }
                }



            }
        }

        /// <summary>
        /// 绑定单据类型下拉框
        /// </summary>
        protected void bindReturnType()
        {
            ddlReturnType.Items.Clear();
            ddlReturnType.Items.Insert(0, new ListItem("请选择..", "0"));
            ddlReturnType.Items.Insert(1, new ListItem("报销单", "30"));
            ddlReturnType.Items.Insert(2, new ListItem("支票/电汇付款单", "31"));
            ddlReturnType.Items.Insert(3, new ListItem("现金借款单", "32"));
            ddlReturnType.Items.Insert(4, new ListItem("商务卡报销单", "33"));
            ddlReturnType.Items.Insert(5, new ListItem("PR现金借款冲销", "34"));
            ddlReturnType.Items.Insert(6, new ListItem("第三方报销单", "35"));
            ddlReturnType.Items.Insert(7, new ListItem("借款冲销单", "36"));

        }

        /// <summary>
        /// 更具代理人获取其代理的所有人
        /// </summary>
        /// <returns></returns>
        private string GetDelegateUser()
        {
            string users = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
            {
                users += model.UserID.ToString() + ",";
            }
            users += CurrentUser.SysID;
            return users;
        }
        #endregion

        protected void btnAddBatchSort_Click(object sender, EventArgs e)
        {
            int stepReturnType = 0;
            int stepPonter = 1;
            string stepBranchCode = "";
            string[] pnlist;
            string returnids = string.Empty;

            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);


            if (!string.IsNullOrEmpty(txtPNList.Text.Trim()))
            {
                pnlist = txtPNList.Text.Trim().Split(',');
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('请输入报销单号！');", true);
                return;
            }

            List<ESP.Finance.Entity.PNBatchRelationInfo> batchRelationList = (List<ESP.Finance.Entity.PNBatchRelationInfo>)ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchID=" + batchid, null);
            int batchReturnType = 0;
            string batchBranchCode = "";
            string steprecipient = string.Empty;
            string recipient = string.Empty;
            if (batchRelationList.Count > 0)
            {
                batchReturnType = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctReturnTypeByBatchID(batchid);
                batchBranchCode = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctBranchCodeByBatchID(batchid);
                steprecipient = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctRecipientByBatchID(batchid);
            }

            for (int i = 0; i < pnlist.Length; i++)
            {
                if (string.IsNullOrEmpty(pnlist[i]))
                {
                    continue;
                }
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetList(" returncode ='"+pnlist[i]+"'").FirstOrDefault();
                if (returnModel!=null)
                {
                    if (stepPonter == 1)
                    {
                        stepReturnType = returnModel.ReturnType.Value;
                        stepBranchCode = returnModel.BranchCode;
                        steprecipient = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctRecipientByReturnID(returnModel.ReturnID);
                    }
                    stepPonter++;

                    try
                    {

                        if (stepReturnType != returnModel.ReturnType.Value)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + returnModel.ReturnCode + "单据类型不同，无法添加到批次，请检查！');", true);
                            return;
                        }

                        if (stepBranchCode != returnModel.BranchCode)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + returnModel.ReturnCode + "项目号公司不同，无法添加到批次，请检查！');", true);
                            return;
                        }
                        if (stepReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                        {
                            recipient = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctRecipientByReturnID(returnModel.ReturnID);
                            if ((!string.IsNullOrEmpty(steprecipient)) && steprecipient != recipient)
                            {
                                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + returnModel.ReturnCode + "支票电汇的收款人不同，无法添加到批次，请检查！');", true);
                                return;
                            }
                        }

                        if (batchRelationList.Count > 0)
                        {
                            if (batchReturnType != 0)
                            {
                                if (batchReturnType != returnModel.ReturnType.Value)
                                {
                                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + returnModel.ReturnCode + "单据类型不同，无法添加到批次，请检查！');", true);
                                    return;
                                }
                            }

                            if (batchBranchCode != "")
                            {
                                if (batchBranchCode != returnModel.BranchCode)
                                {
                                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + returnModel.ReturnCode + "项目号公司不同，无法添加到批次，请检查！');", true);
                                    return;
                                }
                            }

                            if (batchReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                            {
                                recipient = ESP.Finance.BusinessLogic.ReturnManager.GetDistinctRecipientByReturnID(returnModel.ReturnID);
                                if (steprecipient != recipient)
                                {
                                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + returnModel.ReturnCode + "支票电汇的收款人不同，无法添加到批次，请检查！');", true);
                                    return;
                                }
                            }
                        }

                        decimal ooptotal = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalOOPByReturnID(returnModel.ReturnID);
                        decimal costtotal = returnModel.PreFee.Value - ooptotal;

                        returnids += returnModel.ReturnID + ",";


                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加批次操作失败，请重试！');", true);
                        return;
                    }
                }
            }
            if (ESP.Finance.BusinessLogic.PNBatchManager.InsertExpenseAccoutToBatch(batchModel, returnids.TrimEnd(',').Split(',')) == true)
            {
                //alert('添加批次" + batchModel.BatchCode + "明细成功!');
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location='FinanceCreateBatch.aspx?BatchID=" + batchModel.BatchID + "';", true);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加批次明细失败，请重试！');", true);
                return;
            }
        }
    }
}
