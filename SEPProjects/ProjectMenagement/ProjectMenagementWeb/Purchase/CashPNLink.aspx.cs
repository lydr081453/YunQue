using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;
using System.Collections;

public partial class Purchase_CashPNLink : ESP.Web.UI.PageBase
{
    int returnId = 0;
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    ModelProcess mp;//模板工作流的实例
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.ProcessInstanceDao PIDao;//工作流数据访问对象ProcessInstanceDao
    PROCESSINSTANCES pi;//一个工作流实例
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
    protected ESP.Finance.Entity.ReturnInfo returnModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        }
        if (!IsPostBack) 
        {
            BindInfo();
        }
    }

    /// <summary>
    /// 绑定信息
    /// </summary>
    private void BindInfo()
    {
        PaymentTypeInfo paymentType = null;
        if (returnModel.PaymentTypeID != null)
            paymentType = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(returnModel.PaymentTypeID.Value);
        hidPrID.Value = returnModel.PRID.ToString();
        hidProjectID.Value = returnModel.ProjectID.ToString();
        lblApplicant.Text = returnModel.RequestEmployeeName;

        lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
        lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
       // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
        lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
        lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
        lblPeriodType.Text = returnModel.PaymentTypeName;
        txtPayRemark.Text = returnModel.ReturnContent;
        if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
            lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
        else
            lblPRNo.Text = returnModel.PRNo;
        lblProjectCode.Text = returnModel.ProjectCode;
        lblReturnCode.Text = returnModel.ReturnCode;
        lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value,0,returnModel.IsDiscount);

        ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
        if (vmodel != null && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift)
        {
            this.lblSupplierName.Text = vmodel.Account_name;
            this.txtSupplierBank.Text = vmodel.Account_bank;
            this.txtSupplierAccount.Text = vmodel.Account_number;
        }

        labInvoice.Text = "";
        if (!string.IsNullOrEmpty(returnModel.PaymentTypeCode) || (paymentType != null && paymentType.IsNeedCode == true))
        {
            this.lblPayCode.Style["display"] = "block";
            this.txtPayCode.Style["display"] = "block";
            this.txtPayCode.Text = returnModel.PaymentTypeCode;

        }

        this.lblAccount.Text = returnModel.BankAccount;
        this.lblAccountName.Text = returnModel.BankAccountName;
        this.lblBankAddress.Text = returnModel.BankAddress;
        if (returnModel.PaymentTypeID != null)
        {
            this.txtPayCode.Text = returnModel.PaymentTypeCode;
        }
        this.txtFactFee.Text = returnModel.FactFee == null ? "" : returnModel.FactFee.Value.ToString("#,##0.00");

        if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit)
        {
            this.txtFactFee.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
        }

        if (returnModel.ReturnPreDate != null)
            txtReturnPreDate.Text = Convert.ToDateTime(returnModel.ReturnPreDate).ToString("yyyy-MM-dd");

        if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel2 || returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel3 || returnModel.ReturnStatus == (int)PaymentStatus.WaitReceiving)
        {
            txtReturnPreDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        this.lblLog.Text = this.GetAuditLog(returnId);

        listBind(returnModel);
    }

    private string GetAuditLog(int Rid)
    {
        ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Rid);
        if (ReturnModel != null)
        {
            if (ReturnModel.PRID != null && ReturnModel.PRID.Value != 0)
                TopMessage.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(ReturnModel.PRID.Value);
            if ((ReturnModel.PRID == null || ReturnModel.PRID.Value == 0) && ReturnModel.ProjectID != null && ReturnModel.ProjectID.Value != 0)
                TopMessage.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(ReturnModel.ProjectID.Value);
        }
        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
        string loginfo = string.Empty;
        foreach (ESP.Finance.Entity.AuditLogInfo model in histList)
        {
            string austatus = string.Empty;
            if (model.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
            {
                austatus = "审批通过";
            }
            else if (model.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
            {
                austatus = "审批驳回";
            }
            string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

        }
        return loginfo;
    }

    private void listBind(ESP.Finance.Entity.ReturnInfo returnModel)
    {
        if (returnModel == null)
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        string terms = " projectCode=@projectCode and supplierName=@supplierName and returnType=@returnType and returnStatus=@returnStatus";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@projectCode", returnModel.ProjectCode));
        parms.Add(new SqlParameter("@supplierName", returnModel.SupplierName));
        parms.Add(new SqlParameter("@returnType", (int)ESP.Purchase.Common.PRTYpe.PN_KillCash));
        parms.Add(new SqlParameter("@returnStatus", (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving));
        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(terms, parms);
        gvG.DataSource = returnList;
        gvG.DataBind();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        decimal price = decimal.Parse(string.IsNullOrEmpty(txtPrice.Text.Trim()) ? "0" :txtPrice.Text.Trim());
        List<CashPNLinkInfo> cashLinkList = new List<CashPNLinkInfo>();
        if (price <= (returnModel.FactFee.Value * 1.1m))
        {
            //直接通过
            CashPNLinkInfo cashModel = new CashPNLinkInfo();
            cashModel.returnId = returnModel.ReturnID;
            cashModel.returnCashPrice = price;
            cashModel.linkRemark = txtRemark.Text.Trim();
            cashModel.linker = int.Parse(CurrentUser.SysID);
            cashModel.linkDate = DateTime.Now;
            cashModel.oldPrice = returnModel.FactFee.Value;
            cashModel.linkType = 1;
            cashLinkList.Add(cashModel);
            //更新return状态并添加销账记录
            returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
            
            if (ESP.Finance.BusinessLogic.CashPNLinkManager.AddAndUpdateReturn(cashLinkList, new List<ReturnInfo>() { returnModel }))
            {
                ESP.Finance.Entity.AuditLogInfo histModel = new AuditLogInfo();
                histModel.FormID = returnModel.ReturnID;
                histModel.FormType = (int)ESP.Finance.Utility.FormType.Return;
                histModel.AuditDate = DateTime.Now;
                histModel.AuditorEmployeeName = CurrentUser.Name;
                histModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                histModel.AuditorUserCode = CurrentUser.ID;
                histModel.AuditorUserName = CurrentUser.ITCode;
                histModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                histModel.Suggestion ="销账通过";
                ESP.Finance.BusinessLogic.AuditLogManager.Add(histModel);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('销账成功！');window.location='PurchasePaymentList.aspx';", true); //成功
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('销账失败！');", true); //失败
            }
        }
        
        else if(price > (returnModel.FactFee.Value * 1.1m))
        {
            //需要创建新的PN与之关联
            CashPNLinkInfo cashModel = new CashPNLinkInfo();
            cashModel.returnId = returnModel.ReturnID;
            cashModel.returnCashPrice = price;
            cashModel.linkRemark = txtRemark.Text.Trim();
            cashModel.linker = int.Parse(CurrentUser.SysID);
            cashModel.linkDate = DateTime.Now;
            cashModel.oldPrice = returnModel.FactFee.Value;
            cashModel.linkType = 3;
            cashLinkList.Add(cashModel);
            decimal totalP = price;
            List<ReturnInfo> returnModels = new List<ReturnInfo>();
            if (!string.IsNullOrEmpty(Request["chkItem"]))
            {
                string returnIds = Request["chkItem"];
                foreach (string id in returnIds.Split(','))
                {
                    ReturnInfo newReturn = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(id));
                    CashPNLinkInfo newCashModel = new CashPNLinkInfo();
                    newCashModel.returnId = returnModel.ReturnID;
                    newCashModel.returnCashPrice = newReturn.FactFee.Value;
                    newCashModel.linkRemark = txtRemark.Text.Trim();
                    newCashModel.linker = int.Parse(CurrentUser.SysID);
                    newCashModel.linkDate = DateTime.Now;
                    newCashModel.oldPrice = returnModel.FactFee.Value;
                    newCashModel.linkType = 3;
                    newCashModel.linkReturnId = int.Parse(id);
                    cashLinkList.Add(newCashModel);
                    totalP -= newReturn.FactFee.Value;
                    newReturn.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    returnModels.Add(newReturn);
                }
            }
            if (returnModel.FactFee.Value == totalP)
            {
                returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                returnModels.Add(returnModel);
                if (ESP.Finance.BusinessLogic.CashPNLinkManager.AddAndUpdateReturn(cashLinkList, returnModels))
                {
                    ESP.Finance.Entity.AuditLogInfo histModel = new AuditLogInfo();
                    histModel.FormID = returnModel.ReturnID;
                    histModel.FormType = (int)ESP.Finance.Utility.FormType.Return;
                    histModel.AuditDate = DateTime.Now;
                    histModel.AuditorEmployeeName = CurrentUser.Name;
                    histModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                    histModel.AuditorUserCode = CurrentUser.ID;
                    histModel.AuditorUserName = CurrentUser.ITCode;
                    histModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                    histModel.Suggestion = "销账通过";
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(histModel);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('销账成功！');window.location='PurchasePaymentList.aspx';", true); //成功
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('销账失败！');", true); //失败
                }
            }
            else
            {
                //金额不符，不可操作
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('销账金额不符，不可以进行销账操作！');", true);
            }
        }
    }

    void Purchase_Requisition_SetOperationAudit_TaskCompleteEvent(Hashtable context)
    {
        workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(returnModel));
    }

    private int createTemplateProcess(ESP.Finance.Entity.ReturnInfo model,IList<ReturnAuditHistInfo> auditList,ref string YS, ref string ZJSP, ref string ZJLSP, ref string ZJZH)
    {
        int ret;
        string checkAuditor = ",";
        ModelTemplate.BLL.ModelManager manager = new ModelManager();
        List<ModelTemplate.ModelTask> lists = new List<ModelTask>();
        ModelTemplate.ModelTask task = new ModelTask();
        task.TaskName = "start";
        task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
        task.DisPlayName = "start";
        task.RoleName = model.RequestorID.ToString();

        ModelTemplate.ModelTask lastTask = null;
        foreach (ReturnAuditHistInfo auditModel in auditList)
        {
            if (auditModel.AuditType == ESP.Finance.Utility.auditorType.operationAudit_Type_YS)
                YS += auditModel.AuditorUserID + ",";
            else if (auditModel.AuditType == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP)
                ZJSP += auditModel.AuditorUserID + ",";
            else if (auditModel.AuditType == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP)
                ZJLSP += auditModel.AuditorUserID + ",";
            else if (auditModel.AuditType == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJZH)
                ZJZH += auditModel.AuditorUserID + ",";
            if (auditModel.AuditType != ESP.Finance.Utility.auditorType.operationAudit_Type_ZJZH || auditModel.AuditType != ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLZH)
            {
                //审批人
                ModelTemplate.ModelTask prejudicationTask = new ModelTask();
                prejudicationTask.TaskName = "PN付款审核" + auditModel.AuditorEmployeeName + auditModel.AuditorUserID.Value;
                prejudicationTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                prejudicationTask.DisPlayName = "PN付款审核" + auditModel.AuditorEmployeeName + auditModel.AuditorUserID.Value;
                prejudicationTask.RoleName = auditModel.AuditorUserID.Value.ToString();

                if (lastTask == null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
                    trans.TransitionTo = prejudicationTask.TaskName;
                    task.Transations.Add(trans);
                    lists.Add(task);
                }
                else
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = prejudicationTask.TaskName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);
                }
                lastTask = prejudicationTask;
            }
            else
            {
                //知会人
                ModelTemplate.ModelTask ZHTask = new ModelTask();
                ZHTask.TaskName = "PN付款审核知会" + auditModel.AuditorEmployeeName + auditModel.AuditorUserID.Value;
                ZHTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                ZHTask.DisPlayName = "PN付款审核知会" + auditModel.AuditorEmployeeName + auditModel.AuditorUserID.Value;
                ZHTask.RoleName = auditModel.AuditorUserID.Value.ToString();

                if (lastTask == null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
                    trans.TransitionTo = ZHTask.TaskName;
                    task.Transations.Add(trans);
                }
                else
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = ZHTask.TaskName;
                    lastTask.Transations.Add(trans);
                }
                lists.Add(ZHTask);
            }
        }
        ModelTemplate.Transition endTrans = new Transition();
        endTrans.TransitionName = lastTask.DisPlayName;
        endTrans.TransitionTo = "end";
        lastTask.Transations.Add(endTrans);
        lists.Add(lastTask);
        if (model.ProcessID != null && model.InstanceID != null)
        {
            PIDao = new ProcessInstanceDao();
            PIDao.TerminateProcess(model.ProcessID.Value, model.InstanceID.Value);
        }
        ret = manager.ImportData("PN付款业务审核(" + model.ReturnID + ")", "PN付款业务审核(" + model.ReturnID + ")", "1.0", model.RequestEmployeeName, lists);
        return ret;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchasePaymentList.aspx");
    }
}
