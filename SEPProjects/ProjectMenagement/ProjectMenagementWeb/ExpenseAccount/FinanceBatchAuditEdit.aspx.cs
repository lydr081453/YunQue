/************************************************************************\
 * 审批页面
 *      
 * 不同角色进入后，页面显示权限不同，根据step参数区分
 * 
 * workitemid为工作项ID
 *
\************************************************************************/
using System;
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

    public partial class FinanceBatchAuditEdit : ESP.Web.UI.PageBase
    {
        string BeiJingBranch = string.Empty;
        decimal totalExpense = 0;
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        ESP.Finance.Entity.PNBatchInfo batchInfo = null;
        int batchid = 0;
        bool isF3 = false;
        bool isChongxiao = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 6000;
            BeiJingBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditBJBranch"];
            if (!string.IsNullOrEmpty(Request["batchid"]))
            {
                batchid = Convert.ToInt32(Request["batchid"]);
                batchInfo = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            }
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private bool getChongxiao(int bid)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(bid);

            if (returnlist != null && returnlist.Count > 0)
            {
                if (returnlist[0].ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || returnlist[0].ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                {
                    return true;
                }
            }
            return false;
        }


        private bool IsNeedFinanceDirectorAudit(int bid)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(bid);
            bool retval = false;

            if (returnlist != null && returnlist.Count > 0)
            {
               foreach(var ret in returnlist)
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

        protected void BindInfo()
        {

            if (batchid > 0 && batchInfo != null)
            {

                isChongxiao = getChongxiao(batchid);

                BindList();
                txtBatchCode.Text = batchInfo.BatchCode;
                lblBatchId.Text = batchInfo.BatchID.ToString();
                lblPurchaseBatchCode.Text = batchInfo.PurchaseBatchCode;
                txtSuggestion.Text = batchInfo.Description;

                bindBranch();


                if (isF3)
                {
                    trNextAuditer.Visible = false;
                }
                else
                {
                    trNextAuditer.Visible = true;
                }



            }

        }

        protected void btnModifyBatchCode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBatchCode.Text.Trim()))
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次号不能为空！');", true);
                return;
            }
            if (ESP.Finance.BusinessLogic.PNBatchManager.CheckBatchCode(txtBatchCode.Text.Trim(), batchid))
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次号已存在，请检查！');", true);
                return;
            }
            ESP.Finance.Entity.PNBatchInfo batchInfo = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            batchInfo.BatchCode = txtBatchCode.Text.Trim();
            batchInfo.Description = txtSuggestion.Text.Trim();
            if (ESP.Finance.BusinessLogic.PNBatchManager.Update(batchInfo) > 0)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功！');", true);
                return;
            }
        }

        protected void BindList()
        {
            DataSet ds = new DataSet();
            string whereStr = "";

            whereStr += string.Format(" and wa.assigneeid in ({0}) and wi.status = {1} and pb.batchid = {2} ", GetDelegateUser(), (int)ESP.Workflow.WorkItemStatus.Open, batchid);

            ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditListByBatch(whereStr);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["WebPage"].ToString().IndexOf("step=f3") > 0)
                    {
                        //if (BeiJingBranch.IndexOf(batchInfo.BranchCode.ToLower()) >= 0)
                        //{
                        //    isF3 = false;
                        //}
                        //else
                        isF3 = true;
                        break;
                    }
                    else if (dr["WebPage"].ToString().IndexOf("step=f4") > 0)
                    {
                        isF3 = true;
                        break;
                    }
                    else if (dr["WebPage"].ToString().IndexOf("step=f2") > 0 && (isChongxiao == true || !IsNeedFinanceDirectorAudit(batchid)))
                    {
                        isF3 = true;
                        break;
                    }
                }
                gvG.DataSource = ds.Tables[0];
                gvG.DataBind();
            }
            this.lblTotal.Text = batchInfo.Amounts.Value.ToString("#,##0.00");

            List<ESP.Finance.Entity.ExpenseAuditDetailInfo> loglist = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and expenseauditid=" + ds.Tables[0].Rows[0]["ReturnID"].ToString() + " and audittype in(11,12,13)");
            foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo info in loglist)
            {
                lblLog.Text += info.AuditorEmployeeName + "(" + info.AuditorUserName + ")" + ";&nbsp;&nbsp;&nbsp;" + info.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((info.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[info.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + info.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
            }

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

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinanceBatchAuditList.aspx");
        }

        protected void btnEditFA_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ExpenseAccountEditFA.aspx?workitemid=" + workitem.WorkItemId);
        }

        protected void btnUnAudit_Click(object sender, EventArgs e)
        {
            //$$$$$
#if debug
                System.Diagnostics.Debug.WriteLine("批次报销审批-驳回至申请人");
                Trace.Write("批次报销审批-驳回至申请人");
#endif

            string workitemids = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemIDsByBatchID(batchid);
            string returnids = "";
            List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList = new List<ESP.Finance.Entity.ExpenseAccountBatchAudit>();
            ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo = null;
            int stepReturnType = 0;
            int stepPonter = 1;

            if (!string.IsNullOrEmpty(workitemids) && workitemids.Split(',').Length > 0)
            {
                int workitemid = 0;
                for (int i = 0; i < workitemids.Split(',').Length; i++)
                {
                    workitemid = Convert.ToInt32(workitemids.Split(',')[i]);
                    if (workitemid > 0)
                    {
                        auditInfo = new ESP.Finance.Entity.ExpenseAccountBatchAudit();

                        workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(workitemid);
                        model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                        returnids += model.ReturnID + ",";
                        auditInfo.Model = model;
                        auditInfo.Workitem = workitem;

                        if (stepPonter == 1)
                        {
                            stepReturnType = model.ReturnType.Value;
                        }
                        stepPonter++;

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
                            logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Reject;
                            logModel.Suggestion = txtSuggestion.Text.Trim();

                            int returnStatus = (int)ESP.Finance.Utility.PaymentStatus.Rejected;
                            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                            {
                                returnStatus = (int)ESP.Finance.Utility.PaymentStatus.ConfirmReceiving;
                            }

                            int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);
                            if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                            {
                                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                                logModel.Suggestion += "(" + CurrentUser.Name + " 代理 " + emp.Name + ")";
                            }

                            //设置工作流委托方法的参数
                            Dictionary<string, object> prarms = new Dictionary<string, object>() 
                            { 
                                { "EntID", model.ReturnID } ,
                                { "ReturnStatus", returnStatus } ,
                                { "LogModel", logModel }
                            };

                            auditInfo.Prarms = prarms;
                            auditInfo.CurrentUserId = assigneeID;
                            batchInfo.Status = (int)ESP.Finance.Utility.PaymentStatus.FAAudit;
                            auditList.Add(auditInfo);

                        }
                        catch (Exception ex)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('驳回操作失败，请重试！');", true);
                            return;
                        }
                    }

                }

                //调用工作流
                if (ESP.Finance.BusinessLogic.ExpenseAccountManager.FinanceBatchUnAuditReject(auditList, batchid))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='FinanceBatchAuditList.aspx';", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');", true);
                    return;
                }

            }
        }

        protected void btnAuditF_Click(object sender, EventArgs e)
        {
            if (trNextAuditer.Visible == true)
            {
                if (string.IsNullOrEmpty(hidNextAuditor.Value))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择财务下级审批人！');", true);
                    return;
                }
            }

            btnAuditF.Enabled = false;

            string workitemids = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemIDsByBatchID(batchid);
            isChongxiao = getChongxiao(batchid);
            string returnids = "";
            List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList = new List<ESP.Finance.Entity.ExpenseAccountBatchAudit>();
            ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo = null;
            int stepReturnType = 0;
            int stepPonter = 1;

            if (!string.IsNullOrEmpty(workitemids) && workitemids.Split(',').Length > 0)
            {
                int workitemid = 0;
                for (int i = 0; i < workitemids.Split(',').Length; i++)
                {
                    workitemid = Convert.ToInt32(workitemids.Split(',')[i]);
                    if (workitemid > 0)
                    {
                        auditInfo = new ESP.Finance.Entity.ExpenseAccountBatchAudit();

                        workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(workitemid);
                        model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                        returnids += model.ReturnID + ",";
                        auditInfo.Model = model;
                        auditInfo.Workitem = workitem;

                        if (stepPonter == 1)
                        {
                            stepReturnType = model.ReturnType.Value;
                        }
                        stepPonter++;

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
                            logModel.Suggestion = txtSuggestion.Text.Trim();

                            int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);
                            if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                            {
                                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                                logModel.Suggestion += "(" + CurrentUser.Name + " 代理 " + emp.Name + ")";
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

                            batchInfo.Status = GetStatus();
                            if (!string.IsNullOrEmpty(this.ddlBank.SelectedItem.Value))
                            {
                                ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(Convert.ToInt32(this.ddlBank.SelectedItem.Value));
                                batchInfo.BankID = bankModel.BankID;
                                batchInfo.BankName = bankModel.BankName;
                                batchInfo.BankAddress = bankModel.Address;
                                batchInfo.BankAccount = bankModel.BankAccount;
                                batchInfo.BankAccountName = bankModel.BankAccountName;
                                batchInfo.DBCode = bankModel.DBCode;
                                batchInfo.DBManager = bankModel.DBManager;
                                batchInfo.ExchangeNo = bankModel.ExchangeNo;
                                batchInfo.RequestPhone = bankModel.RequestPhone;
                            }
                            batchInfo.BranchID = Convert.ToInt32(this.ddlBranch.SelectedItem.Value);
                            batchInfo.BranchCode = this.ddlBranch.SelectedItem.Text;


                            auditList.Add(auditInfo);

                        }
                        catch (Exception ex)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.Message + "！');", true);
                            return;
                        }
                    }

                }

                //设置财务下级审批人参数
                List<int> nextAuditerList = new List<int>();
                if (hidNextAuditor != null && !string.IsNullOrEmpty(hidNextAuditor.Value))
                {
                    nextAuditerList.Add(Convert.ToInt32(hidNextAuditor.Value));
                    ESP.Compatible.Employee nextEmpInfo = new ESP.Compatible.Employee(Convert.ToInt32(hidNextAuditor.Value));
                    batchInfo.PaymentUserID = nextEmpInfo.IntID;
                    batchInfo.PaymentCode = nextEmpInfo.ID;
                    batchInfo.PaymentEmployeeName = nextEmpInfo.Name;
                    batchInfo.PaymentUserName = nextEmpInfo.ITCode;
                }

                bool isSuccess = false;
                //
                if (stepReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || stepReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                {

                    isSuccess = ESP.Finance.BusinessLogic.ExpenseAccountManager.FinanceBatchAuditReceiving(auditList, nextAuditerList, batchInfo);
                }
                else
                {
                    isSuccess = ESP.Finance.BusinessLogic.ExpenseAccountManager.FinanceBatchAuditExpense(auditList, nextAuditerList, batchInfo,IsNeedFinanceDirectorAudit(batchInfo.BatchID));
                }

                if (isSuccess)
                {

                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量审核通过操作成功！');window.location='FinanceBatchAuditList.aspx';", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量审核操作失败，请重试！');window.location='FinanceBatchAuditList.aspx';", true);
                    return;
                }

            }
        }

        protected void btnUnAuditF_Click(object sender, EventArgs e)
        {
            string workitemids = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemIDsByBatchID(batchid);
            string returnids = "";
            List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList = new List<ESP.Finance.Entity.ExpenseAccountBatchAudit>();
            ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo = null;
            int stepReturnType = 0;
            int stepPonter = 1;

            if (!string.IsNullOrEmpty(workitemids) && workitemids.Split(',').Length > 0)
            {
                int workitemid = 0;
                for (int i = 0; i < workitemids.Split(',').Length; i++)
                {
                    workitemid = Convert.ToInt32(workitemids.Split(',')[i]);
                    if (workitemid > 0)
                    {
                        auditInfo = new ESP.Finance.Entity.ExpenseAccountBatchAudit();

                        workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(workitemid);
                        model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                        returnids += model.ReturnID + ",";
                        auditInfo.Model = model;
                        auditInfo.Workitem = workitem;

                        if (stepPonter == 1)
                        {
                            stepReturnType = model.ReturnType.Value;
                        }
                        stepPonter++;

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
                            logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_RejectToF1;
                            logModel.Suggestion = txtSuggestion.Text.Trim();

                            int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);

                            if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                            {
                                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                                logModel.Suggestion += "(" + CurrentUser.Name + " 代理 " + emp.Name + ")";
                            }

                            //设置工作流委托方法的参数
                            Dictionary<string, object> prarms = new Dictionary<string, object>() 
                            { 
                                { "EntID", model.ReturnID } ,
                                { "ReturnStatus", (int)ESP.Finance.Utility.PaymentStatus.FAAudit } ,
                                { "LogModel", logModel }
                            };

                            auditInfo.Prarms = prarms;
                            auditInfo.CurrentUserId = assigneeID;
                            batchInfo.Status = (int)ESP.Finance.Utility.PaymentStatus.FAAudit;
                            auditList.Add(auditInfo);

                        }
                        catch (Exception ex)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('驳回操作失败，请重试！');", true);
                            return;
                        }
                    }

                }

                ESP.Compatible.Employee nextEmpInfo = new ESP.Compatible.Employee(batchInfo.CreatorID.Value);
                batchInfo.PaymentUserID = nextEmpInfo.IntID;
                batchInfo.PaymentCode = nextEmpInfo.ID;
                batchInfo.PaymentEmployeeName = nextEmpInfo.Name;
                batchInfo.PaymentUserName = nextEmpInfo.ITCode;

                //调用工作流
                if (ESP.Finance.BusinessLogic.ExpenseAccountManager.FinanceBatchUnAuditReturnF1(auditList, batchInfo))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='FinanceBatchAuditList.aspx';", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');", true);
                    return;
                }

            }
        }

        #region
        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;

                Label lblPreFee = (Label)e.Row.FindControl("lblPreFee");
                lblPreFee.Text = dr["PreFee"].ToString();

                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (Convert.ToInt32(dr["ReturnType"]) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                {
                    hylPrint.NavigateUrl = "Print/ExpensePrint.aspx?expenseID=" + dr["ReturnID"];
                }
                else
                {
                    hylPrint.NavigateUrl = "Print/ThirdPartyPrint.aspx?expenseID=" + dr["ReturnID"];
                }
                hylPrint.Target = "_blank";

                HyperLink hylView = (HyperLink)e.Row.FindControl("hylView");
                hylView.NavigateUrl = "FinanceBatchDetailEdit.aspx?id=" + dr["ReturnID"] + "&batchid=" + batchid + "&workitemid=" + dr["WorkItemID"];
                totalExpense += Convert.ToDecimal(dr["PreFee"]);
            }
        }


        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        #endregion


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
                else if (workitem.WebPage.IndexOf("step=f4") > 0)
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


        /// <summary>
        /// 绑定公司
        /// </summary>
        protected void bindBranch()
        {
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


        protected void ddlBranch_SelectedIndexChangeed(object sender, EventArgs e)
        {
            BindBank();
        }


    }
}
