using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

public partial class project_SupporterAuditOperation : ESP.Finance.WebPage.ViewPageForSupporter
{
    string query = string.Empty;
    int SupporterID = 0;
    ESP.Finance.Entity.SupporterInfo SupporterModel;
    ESP.Finance.Entity.ProjectInfo ProjectModel;
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    ProcessInstanceDao PIDao = new ProcessInstanceDao();

    protected void Page_Load(object sender, EventArgs e)
    {
        query = Request.Url.Query;
        if (!IsPostBack)
        {
            if (!GetWorkFlowList())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
                Response.Redirect(GetBackUrl());
            }
            SupporterID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
            TopMessage.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(ESP.Finance.BusinessLogic.SupporterManager.GetModel(SupporterID).ProjectID);
            this.lblLog.Text = this.GetAuditLog(SupporterID);
        }
    }

    WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();

    private void SetWorkFlowData(SupporterInfo pmodel)
    {
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "支持方申请");
        int count = 0;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        List<WorkFlowModel.WorkItemData> listDelegate = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            listDelegate = workitemdata.getProcessDataList(model.UserID.ToString(), "支持方申请");
            foreach (WorkFlowModel.WorkItemData o in listDelegate)
            {
                list2.Add(o);
            }
        }
        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            ESP.Finance.Entity.SupporterInfo model = (ESP.Finance.Entity.SupporterInfo)o.ItemData;
            if (model.SupportID == pmodel.SupportID)
            {
                pmodel.WorkItemID = o.WorkItemID;
                pmodel.InstanceID = o.InstanceID;
                pmodel.WorkItemName = o.WorkItemName;
                pmodel.ProcessID = o.ProcessID;
                break;
            }

        }
        if (count == 0)
        {
            if (list2 != null && list2.Count > 0)
            {
                foreach (WorkFlowModel.WorkItemData o in list2)
                {
                    ESP.Finance.Entity.SupporterInfo model = (ESP.Finance.Entity.SupporterInfo)o.ItemData;
                    if (model.SupportID == pmodel.SupportID)
                    {
                        pmodel.WorkItemID = o.WorkItemID;
                        pmodel.InstanceID = o.InstanceID;
                        pmodel.WorkItemName = o.WorkItemName;
                        pmodel.ProcessID = o.ProcessID;
                        count = 1;
                        break;
                    }
                }
            }
        }
    }
    private bool GetWorkFlowList()
    {
        if (SupporterModel == null)
        {
            SupporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(SupporterModel.ProjectID);
        }
        this.SetWorkFlowData(SupporterModel);
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "支持方申请");
        List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        List<WorkFlowModel.WorkItemData> listDelegate = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            listDelegate = workitemdata.getProcessDataList(model.UserID.ToString(), "支持方申请");
            foreach (WorkFlowModel.WorkItemData o in listDelegate)
            {
                list2.Add(o);
            }
        }

        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            if (o.WorkItemID.ToString() == SupporterModel.WorkItemID.Value.ToString())
            {
                return true;
            }
        }
        if (list2 != null && list2.Count > 0)
        {
            foreach (WorkFlowModel.WorkItemData o in list2)
            {
                if (o.WorkItemID.ToString() == SupporterModel.WorkItemID.Value.ToString())
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!GetWorkFlowList())
        {

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;



        }
        //创建工作流实例
        context = new Hashtable();
        SupporterID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
        SupporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(SupporterID);
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(SupporterModel.ProjectID);
        SetWorkFlowData(SupporterModel);
        WFUSERS wfuser = new WFUSERS();
        wfuser.Id = SupporterModel.LeaderUserID.Value;
        initiators = new WFUSERS[1];
        initiators[0] = wfuser;
        context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
        context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_NAME, "支持方申请业务审核");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "支持方申请业务审核");//提交操作代码：1

        np = processMgr.load_process(SupporterModel.ProcessID.Value, SupporterModel.InstanceID.Value.ToString(), context);

        //激活start任务
        np.load_task(SupporterModel.WorkItemID.Value.ToString(), SupporterModel.WorkItemName);
        np.get_lastActivity().active();
        //complete start任务
        np.load_task(SupporterModel.WorkItemID.Value.ToString(), SupporterModel.WorkItemName);
        ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(project_SupporterAuditOperation_TaskCompleteEvent);
        ((MySubject)np).CompleteEvent += new MySubject.CompleteHandler(project_SupporterAuditOperation_CompleteEvent);
        np.complete_task(SupporterModel.WorkItemID.Value.ToString(), SupporterModel.WorkItemName, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());
        //所有工作项都已经完成，工作流结束

        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('支持方申请审核成功！');", true);

    }


    protected void btnTip_Click(object sender, EventArgs e)
    {
        SupporterID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
        ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
        audit.FormID = SupporterID;
        audit.Suggestion = this.txtAuditRemark.Text;
        audit.AuditDate = DateTime.Now;
        audit.AuditorSysID = int.Parse(CurrentUser.SysID);
        audit.AuditorUserCode = CurrentUser.ID;
        audit.AuditorEmployeeName = CurrentUser.Name;
        audit.AuditorUserName = CurrentUser.ITCode;
        audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
        audit.FormType = (int)ESP.Finance.Utility.FormType.Supporter;
        int ret = ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('留言保存成功！');", true);

    }

    void project_SupporterAuditOperation_CompleteEvent(Hashtable context)
    {

    }

    void project_SupporterAuditOperation_TaskCompleteEvent(Hashtable context)
    {
        int sysId = 0;
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(ProjectModel.BranchID.Value);

        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, SupporterModel.GroupID.Value);
        string exMail = string.Empty;
        ESP.Compatible.Employee employee = null;
        if (np.get_lastActivity() != null)//工作流还有后续任务
        {
            //写序列化
            workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(SupporterModel));
            //更新申请单状态
            SupporterModel.Status = (int)ESP.Finance.Utility.Status.BizAuditing;
            ESP.Finance.BusinessLogic.SupporterManager.Update(SupporterModel);
            SetAuditHist(SupporterModel, (int)AuditHistoryStatus.PassAuditing);
            //发信
            sysId = int.Parse(np.get_lastActivity().getWorkItem().RoleID);
            employee = new ESP.Compatible.Employee(sysId);
            try
            {
                ArrayList notifylist = (ArrayList)context[ContextConstants.NOTIFY_LIST];//得到当前workitem的会知人的ID
                if (notifylist != null && notifylist.Count != 0)//如果有会知列表
                {
                    string ZHEmails = "";
                    for (int i = 0; i < notifylist.Count; i++)//循环查找会知人的email
                    {
                        ESP.Compatible.Employee zhEmployee = new ESP.Compatible.Employee(int.Parse(notifylist[i].ToString()));
                        ZHEmails += getEmployeeEmailBySysUserId(int.Parse(notifylist[i].ToString())) + ",";
                    }
                    if (ZHEmails != "")
                    {
                        SendMailHelper.SendSupporterMailToZH(SupporterModel, CurrentUser.Name, employee.Name, ZHEmails.TrimEnd(','));
                    }
                }

                SendMailHelper.SendSupporterMailBizOK(SupporterModel, CurrentUser.Name, getEmployeeEmailBySysUserId(sysId), "Biz");
                ESP.Framework.Entity.AuditBackUpInfo delegateUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(sysId);
                if (delegateUser != null)
                {
                    SendMailHelper.SendSupporterMailBizOK(SupporterModel, CurrentUser.Name, getEmployeeEmailBySysUserId(delegateUser.BackupUserID), "Biz");
                }
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }

        }
        else//业务审核结束
        {
            sysId = int.Parse(CurrentUser.SysID);
            employee = new ESP.Compatible.Employee(sysId);
            //更新申请单状态
            SupporterModel.Status = (int)ESP.Finance.Utility.Status.BizAuditComplete;
            ESP.Finance.BusinessLogic.SupporterManager.Update(SupporterModel);
            SetAuditHist(SupporterModel, (int)AuditHistoryStatus.PassAuditing);
            try
            {
                SendMailHelper.SendSupporterMailBizOK(SupporterModel, CurrentUser.Name, getEmployeeEmailBySysUserId(SupporterModel.LeaderUserID.Value), "Finance");
                if (SupporterModel.BudgetAllocation.Value <= Convert.ToDecimal(ESP.Finance.Configuration.ConfigurationManager.FinancialAmount))
                {
                    int financeAuditer = branchModel.ProjectAccounter;
                    if (branchProject != null)
                        financeAuditer = branchProject.AuditorID;

                    SendMailHelper.SendSupporterMailBizOK(SupporterModel, CurrentUser.Name, getEmployeeEmailBySysUserId(financeAuditer), "Finance");
                    ESP.Framework.Entity.AuditBackUpInfo FinanceUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(financeAuditer);
                    if (FinanceUser != null)
                    {
                        SendMailHelper.SendSupporterMailBizOK(SupporterModel, CurrentUser.Name, getEmployeeEmailBySysUserId(FinanceUser.BackupUserID), "Finance");
                    }
                }
                else//大于50000，查财务
                {
                    string[] finance = ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters().Split(',');
                    for (int i = 0; i < finance.Length; i++)
                    {
                        SendMailHelper.SendSupporterMailBizOK(SupporterModel, CurrentUser.Name, getEmployeeEmailBySysUserId(Convert.ToInt32(finance[i])), "Finance");
                        ESP.Framework.Entity.AuditBackUpInfo FinanceUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(finance[i]));
                        if (FinanceUser != null)
                        {
                            SendMailHelper.SendSupporterMailBizOK(SupporterModel, CurrentUser.Name, getEmployeeEmailBySysUserId(FinanceUser.BackupUserID), "Finance");
                        }
                    }
                }
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }

        }


    }

    private void SetAuditHist(SupporterInfo supmodel, int audithist)
    {
        string term = " supporterid=@supporterid  and auditstatus=@auditstatus";
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@supporterid", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = SupporterModel.SupportID;
        paramlist.Add(p1);
        System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@auditstatus", System.Data.SqlDbType.Int, 4);
        p3.SqlValue = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
        paramlist.Add(p3);
        IList<ESP.Finance.Entity.SupporterAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.SupporterAuditHistManager.GetList(term, paramlist);
        var tempList = auditlist.OrderBy(N => N.SquenceLevel).ToList();
        if (tempList != null && tempList.Count > 0)
        {
            ESP.Finance.Entity.SupporterAuditHistInfo audit = tempList[0];
            if (audit.AuditorUserID.Value != Convert.ToInt32(CurrentUser.SysID))
            {
                audit.Suggestion = this.txtAuditRemark.Text + "[" + CurrentUser.Name + "为" + audit.AuditorEmployeeName + "的代理人]";
            }
            else
            {
                audit.Suggestion = this.txtAuditRemark.Text;
            }
            audit.AuditStatus = audithist;
            audit.AuditDate = DateTime.Now;
            ESP.Finance.BusinessLogic.SupporterAuditHistManager.Update(audit);
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (!GetWorkFlowList())
        {

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        //创建工作流实例
        context = new Hashtable();
        SupporterID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
        SupporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(SupporterID);
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(SupporterModel.ProjectID);
        SetWorkFlowData(SupporterModel);
        WFUSERS wfuser = new WFUSERS();
        wfuser.Id = SupporterModel.LeaderUserID.Value;
        initiators = new WFUSERS[1];
        initiators[0] = wfuser;
        context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
        context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_NAME, "支持方申请业务审核");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "支持方申请业务审核");//提交操作代码：1

        np = processMgr.load_process(SupporterModel.ProcessID.Value, SupporterModel.InstanceID.Value.ToString(), context);
        ((MySubject)np).TerminateEvent += new MySubject.TerminateHandler(project_SupporterAuditOperation_TerminateEvent);
        np.terminate();
    }

    void project_SupporterAuditOperation_TerminateEvent(Hashtable context)
    {
        string term = " SupporterID=@SupporterID and auditoruserid=@auditoruserid and auditstatus=@auditstatus";
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@SupporterID", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = SupporterModel.SupportID;
        paramlist.Add(p1);
        System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@auditoruserid", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = CurrentUser.SysID;
        paramlist.Add(p2);
        System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@auditstatus", System.Data.SqlDbType.Int, 4);
        p3.SqlValue = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
        paramlist.Add(p3);
        IList<ESP.Finance.Entity.SupporterAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.SupporterAuditHistManager.GetList(term, paramlist);
        if (auditlist != null && auditlist.Count > 0)
        {
            ESP.Finance.Entity.SupporterAuditHistInfo audit = auditlist[0];
            audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
            audit.Suggestion = this.txtAuditRemark.Text;
            audit.AuditDate = DateTime.Now;
            ESP.Finance.BusinessLogic.SupporterAuditHistManager.Update(audit);
        }
        if (SupporterModel.ProcessID != null && SupporterModel.InstanceID != null)
        {
            PIDao.TerminateProcess(SupporterModel.ProcessID.Value, SupporterModel.InstanceID.Value);
        }
        //更新申请单状态
        SupporterModel.Status = (int)ESP.Finance.Utility.Status.BizReject;
        ESP.Finance.BusinessLogic.SupporterManager.Update(SupporterModel);
        SetAuditHist(SupporterModel, (int)AuditHistoryStatus.TerminateAuditing);
        //驳回
        string exMail = string.Empty;
        try
        {
            SendMailHelper.SendSupporterMailAuditBizReject(SupporterModel, SupporterModel.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(SupporterModel.ApplicantUserID)));
        }
        catch
        {
            exMail = "(邮件发送失败!)";
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('支持方申请单驳回成功！"+exMail+"');", true);

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }
    private string getEmployeeEmailBySysUserId(int SysUserId)
    {
        return new ESP.Compatible.Employee(SysUserId).EMail;
    }

    private string GetAuditLog(int sid)
    {
        SupporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(sid);

        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetSupporterList(SupporterModel.SupportID);
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
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "SupporterAuditList.aspx" : Request["BackUrl"];
    }
}

