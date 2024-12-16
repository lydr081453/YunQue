using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ESP.Finance.BusinessLogic;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using WorkFlowModel;
using System;

public partial class ForeGift_operationAudit : ESP.Web.UI.PageBase
{
    int returnId = 0;
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    ESP.Finance.Entity.ReturnInfo returnModel;
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
    ProcessInstanceDao PIDao = new ProcessInstanceDao();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        }
        if (!IsPostBack)
        {
            this.lblLog.Text = this.GetAuditLog(returnId);
            ViewForeGift.BindInfo(ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId));
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }

    private void SetWorkFlowData(ReturnInfo rmodel)
    {
        int count = 0;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(CurrentUser.SysID));
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PN付款");
        List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        List<WorkFlowModel.WorkItemData> listDelegate = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            listDelegate = workitemdata.getProcessDataList(model.UserID.ToString(), "PN付款");
            foreach (WorkFlowModel.WorkItemData o in listDelegate)
            {
                list2.Add(o);
            }
        }
        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)o.ItemData;
            if (model.ReturnID == rmodel.ReturnID)
            {
                rmodel.WorkItemID = o.WorkItemID;
                rmodel.InstanceID = o.InstanceID;
                rmodel.WorkItemName = o.WorkItemName;
                rmodel.ProcessID = o.ProcessID;
                count = 1;
                break;
            }
        }
        if (count == 0)
        {
            if (list2 != null && list2.Count > 0)
            {
                foreach (WorkFlowModel.WorkItemData o in list2)
                {
                    ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)o.ItemData;
                    if (model.ReturnID == rmodel.ReturnID)
                    {
                        rmodel.WorkItemID = o.WorkItemID;
                        rmodel.InstanceID = o.InstanceID;
                        rmodel.WorkItemName = o.WorkItemName;
                        rmodel.ProcessID = o.ProcessID;
                        count = 1;
                        break;
                    }
                }
            }
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);

        if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
        {
            if (!ValidAudited())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
                return;
            }
            //创建工作流实例
            context = new Hashtable();
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            SetWorkFlowData(returnModel);
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = returnModel.RequestorID.Value;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PN付款业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PN付款业务审核");//提交操作代码：1

            np = processMgr.load_process(returnModel.ProcessID.Value, returnModel.InstanceID.Value.ToString(), context);
            ((MySubject)np).TerminateEvent += new MySubject.TerminateHandler(Purchase_Requisition_OperationAudit_TerminateEvent);
            np.terminate();
        }
        else
        {
            returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Save;
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
            SetAuditHistory(returnModel, 0, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);

            string exMail = string.Empty;
            try
            {
                SendMailHelper.SendMailPROperaOverruleFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), System.Guid.NewGuid().ToString(), "alert('审批驳回成功！" + exMail + "');window.location.href='" + GetBackUrl() + "';", true);

        }
    }

    void Purchase_Requisition_OperationAudit_TerminateEvent(Hashtable context)
    {
        returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Save;
        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
        string exMail = string.Empty;
        if (returnModel.ProcessID != null && returnModel.InstanceID != null)
        {
            PIDao.TerminateProcess(returnModel.ProcessID.Value, returnModel.InstanceID.Value);
        }
        SetAuditHistory(returnModel, 0, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
        try
        {
            SendMailHelper.SendMailPROperaOverruleFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail);
        }
        catch
        {
            exMail = "(邮件发送失败!)";
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), System.Guid.NewGuid().ToString(), "alert('审批驳回成功！" + exMail + "');window.location.href='" + GetBackUrl() + "';", true);
    }

    private void SetAuditHistory(ESP.Finance.Entity.ReturnInfo model, int audittype, int auditstatus)
    {
        string term = string.Format(" ReturnID={0}  and AuditeStatus={1}", model.ReturnID, (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing);
        IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term);
        if (auditlist != null && auditlist.Count > 0)
        {
            ESP.Finance.Entity.ReturnAuditHistInfo audit = auditlist[0];
            if (audit.AuditorUserID.Value != int.Parse(CurrentUser.SysID))
            {
                audit.Suggestion = this.txtRemark.Text + "[" + CurrentUser.Name + "为" + audit.AuditorEmployeeName + "的代理人]";
                //audit.AuditorUserID = int.Parse(CurrentUser.SysID);
                //audit.AuditorUserCode = CurrentUser.ID;
                //audit.AuditorUserName = CurrentUser.ITCode;
                //audit.AuditorEmployeeName = CurrentUser.Name;
            }
            else
            {
                audit.Suggestion = this.txtRemark.Text;
            }
            audit.AuditeStatus = auditstatus;
            audit.AuditeDate = DateTime.Now;
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Update(audit);
        }
    }

    private bool ValidAudited()
    {
        if (returnModel == null)
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]));
        SetWorkFlowData(returnModel);
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(CurrentUser.SysID));
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PN付款");
        List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        List<WorkFlowModel.WorkItemData> listDelegate = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            listDelegate = workitemdata.getProcessDataList(model.UserID.ToString(), "PN付款");
            foreach (WorkFlowModel.WorkItemData o in listDelegate)
            {
                list2.Add(o);
            }
        }
        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            if (o.WorkItemID.ToString() == returnModel.WorkItemID.Value.ToString())
            {
                return true;
            }
        }
        if (list2 != null && list2.Count > 0)
        {
            foreach (WorkFlowModel.WorkItemData o in list2)
            {
                if (o.WorkItemID.ToString() == returnModel.WorkItemID.Value.ToString())
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);

        if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
        {
            if (!ValidAudited())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
                return;
            }

            //创建工作流实例
            context = new Hashtable();
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            SetWorkFlowData(returnModel);
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = returnModel.RequestorID.Value;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PN付款业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PN付款业务审核");//提交操作代码：1

            np = processMgr.load_process(returnModel.ProcessID.Value, returnModel.InstanceID.Value.ToString(), context);

            //激活start任务
            np.load_task(returnModel.WorkItemID.Value.ToString(), returnModel.WorkItemName);
            np.get_lastActivity().active();

            returnModel.WorkItemID = int.Parse(np.get_lastActivity().getWorkItem().Id.ToString());
            returnModel.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
            returnModel.InstanceID = int.Parse(np.get_instance_data().Id.ToString());
            returnModel.ProcessID = returnModel.ProcessID.Value;
            //complete start任务
            np.load_task(returnModel.WorkItemID.Value.ToString(), returnModel.WorkItemName);
            ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(Purchase_Requisition_OperationAudit_TaskCompleteEvent);
            ((MySubject)np).CompleteEvent += new MySubject.CompleteHandler(Purchase_Requisition_OperationAudit_CompleteEvent);
            np.complete_task(returnModel.WorkItemID.Value.ToString(), returnModel.WorkItemName, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());
            //所有工作项都已经完成，工作流结束
        }
        else
        {

            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1)).FirstFinanceID);
            int nextId = int.Parse(emp.SysID);
            string nextName = emp.Name;
            bool isLast = true;
            updateReturnInfo(nextId, nextName, isLast);
            try
            { //发信
                SendMailHelper.SendMailPRFirstOperaPassFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, emp.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail, emp.EMail, 0, "Finance");
            }
            catch { }

        }
    }

    void Purchase_Requisition_OperationAudit_CompleteEvent(Hashtable context)
    {
    }

    void Purchase_Requisition_OperationAudit_TaskCompleteEvent(Hashtable context)
    {
        int nextId = 0;
        string nextName = "";
        bool isLast = false;
        if (np.get_lastActivity() != null)//还有后续任务，该工作流尚未结束
        {
            //发信
            int sysId = int.Parse(np.get_lastActivity().getWorkItem().RoleID);
            ESP.Compatible.Employee employee = new ESP.Compatible.Employee(sysId);
            nextId = int.Parse(employee.SysID);
            nextName = employee.Name;

            workitemdao.insertItemData(int.Parse(np.get_lastActivity().getWorkItem().Id.ToString()), int.Parse(np.get_instance_data().Id.ToString()), SerializeFactory.Serialize(returnModel));
            updateReturnInfo(nextId, nextName, isLast);
            try
            {
                SendMailHelper.SendMailPRFirstOperaPassFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, employee.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail, employee.EMail, 0, "Biz");
            }
            catch { }

        }
        else//工作流完全结束
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1)).FirstFinanceID);
            nextId = int.Parse(emp.SysID);
            nextName = emp.Name;
            isLast = true;

            updateReturnInfo(nextId, nextName, isLast);
            try
            {
                //发信
                SendMailHelper.SendMailPRFirstOperaPassFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, emp.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail, emp.EMail, 0, "Finance");
            }
            catch { }
        }

    }

    public void updateReturnInfo(int nextId, string nextName, bool isLast)
    {
        returnModel.PaymentUserID = nextId;
        returnModel.PaymentEmployeeName = nextName;
        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
        if (isLast)
            returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

        SetAuditHistory(returnModel, 0, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);

        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批通过成功！');window.location.href='" + GetBackUrl() + "';", true);
    }


    private string GetAuditLog(int Rid)
    {
        ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Rid);
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


    private string GetBackUrl()
    {
        return "/Purchase/" + (string.IsNullOrEmpty(Request["BackUrl"]) ? "ManagerAuditList.aspx" : Request["BackUrl"]);
    }
}
