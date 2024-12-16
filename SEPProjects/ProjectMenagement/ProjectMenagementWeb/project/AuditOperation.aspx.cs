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
using WorkFlowModel;
using ESP.Finance.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
public partial class project_AuditOperation : ESP.Finance.WebPage.ViewPageForProject
{
    string query = string.Empty;
    int projectid = 0;
    ESP.Finance.Entity.ProjectInfo projectModel;
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
    ProcessInstanceDao PIDao = new ProcessInstanceDao();
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 600;
        query = Request.Url.Query;
        if (!IsPostBack)
        {
            //if (!GetWorkFlowList())
            //{
            //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            //    Response.Redirect(GetBackUrl());
            //}
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                this.lblLog.Text = this.GetAuditLog(projectid);
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                TopMessage.ProjectModel = projectModel;
                //SetWorkFlowData(projectModel);
                this.PaymentDisplay.ProjectInfo = projectModel;
                this.PaymentDisplay.InitProjectInfo();
            }
            //40%
            //decimal taxfee = 0;
            //decimal servicefee = 0;
            //decimal profilerate = 0;
            //ESP.Finance.Entity.TaxRateInfo rateModel = null;
            //if (projectModel.ContractTaxID != null)
            //    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);

            //if (projectModel.ContractTax != null)
            //{
            //    if (projectModel.IsCalculateByVAT == 1)
            //    {
            //        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(projectModel, rateModel);
            //    }
            //    else
            //    {
            //        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(projectModel, rateModel);
            //    }
            //}

            //if (projectModel.IsCalculateByVAT == 1)
            //    servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectModel, rateModel);
            //else
            //    servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectModel, rateModel);

            //if (projectModel.TotalAmount > 0)
            //{
            //    profilerate = (servicefee / Convert.ToDecimal(projectModel.TotalAmount) * 100);
            //}
            //if (profilerate < 40 && projectModel.ContractStatusName != ESP.Finance.Utility.ProjectType.BDProject)
            //{
            //    this.lblTip.Text = "项目毛利率为" + profilerate.ToString("#,##0.00") + "%，低于40%，请说明立项原因。";
            //    this.txtReason.Text = projectModel.ProfileReason;
            //}
            //else
            //{
            //    this.tabReason.Visible = false;
            //}
        }
    }

    public List<WorkFlowModel.WorkItemData> getProcessDataList(string roleid, string type)
    {
        List<WorkFlowModel.WorkItemData> lists = new List<WorkFlowModel.WorkItemData>();

        Database db = DatabaseFactory.CreateDatabase();

        string sqlCommand = @"select we_workitemdata.ordercontent,p.workitemid,p.instanceid,p.processid,p.taskname from we_workitems as p 
                                inner join we_workitemdata on p.workitemid=we_workitemdata.workitemid 
                                inner join we_processinstances on p.instanceid=we_processinstances.instanceid
                                where roleid='" + roleid + "' and p.taskname like '" + type + "%' and p.state<100 and we_processinstances.processinstancestate<100";
        DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
        System.Data.SqlClient.SqlDataReader reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);

        while (reader.Read())
        {
            Object obj = null;
            //obj = SerializeFactory.DeserializeObject((byte[])reader.GetValue(0));
            WorkFlowModel.WorkItemData item = new WorkFlowModel.WorkItemData();
            item.WorkItemID = Convert.ToInt32(reader.GetValue(1));
            item.InstanceID = Convert.ToInt32(reader.GetValue(2));
            item.ProcessID = Convert.ToInt32(reader.GetValue(3));
            item.WorkItemName = reader.GetValue(4).ToString();
            item.ItemData = obj;
            lists.Add(item);
        }
        reader.Close();

        return lists;

    }

    private void SetWorkFlowData(ESP.Finance.Entity.ProjectInfo pmodel)
    {
        int count = 0;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        List<WorkFlowModel.WorkItemData> list1 = getProcessDataList(CurrentUser.SysID, "项目号申请");
        List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        List<WorkFlowModel.WorkItemData> listDelegate = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            listDelegate = getProcessDataList(model.UserID.ToString(), "项目号申请");
            foreach (WorkFlowModel.WorkItemData o in listDelegate)
            {
                list2.Add(o);
            }
        }

        foreach (WorkFlowModel.WorkItemData o in list1)
        {
           // ESP.Finance.Entity.ProjectInfo model = (ESP.Finance.Entity.ProjectInfo)o.ItemData;
            if (o.InstanceID == pmodel.InstanceID)
            {
                pmodel.WorkItemID = o.WorkItemID;
                pmodel.InstanceID = o.InstanceID;
                pmodel.WorkItemName = o.WorkItemName;
                pmodel.ProcessID = o.ProcessID;
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
                    //ESP.Finance.Entity.ProjectInfo model = (ESP.Finance.Entity.ProjectInfo)o.ItemData;
                    if (o.InstanceID == pmodel.InstanceID)
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
        if (projectModel == null)
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        this.SetWorkFlowData(projectModel);
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        List<WorkFlowModel.WorkItemData> list1 = getProcessDataList(CurrentUser.SysID, "项目号申请");
        List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        List<WorkFlowModel.WorkItemData> listDelegate = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            listDelegate = getProcessDataList(model.UserID.ToString(), "项目号申请");
            foreach (WorkFlowModel.WorkItemData o in listDelegate)
            {
                list2.Add(o);
            }
        }

        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            if (o.WorkItemID.ToString() == projectModel.WorkItemID.Value.ToString())
            {
                return true;
            }
        }
        if (list2 != null && list2.Count > 0)
        {
            foreach (WorkFlowModel.WorkItemData o in list2)
            {
                if (o.WorkItemID.ToString() == projectModel.WorkItemID.Value.ToString())
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (!GetWorkFlowList())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }


        //创建工作流实例

        context = new Hashtable();
        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);

        this.SetWorkFlowData(projectModel);
        WFUSERS wfuser = new WFUSERS();
        wfuser.Id = projectModel.ApplicantUserID;
        initiators = new WFUSERS[1];
        initiators[0] = wfuser;
        context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
        context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_NAME, "项目号申请业务审核");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "项目号申请业务审核");//提交操作代码：1
        try
        {
            np = processMgr.load_process(projectModel.ProcessID.Value, projectModel.InstanceID.Value.ToString(), context);
        }
        catch (Exception ex)
        {
            Response.Write("processMgr.load_process" + ex.Message);
        }
        //激活start任务
        try
        {
            np.load_task(projectModel.WorkItemID.Value.ToString(), projectModel.WorkItemName);
        }
        catch (Exception ex2)
        {
            Response.Write("np load_task" + ex2.Message);
        }
        try
        {
            np.get_lastActivity().active();
        }
        catch (Exception ex2)
        {
            Response.Write("np active" + ex2.Message);
        }
        //complete start任务
        try
        {
            np.load_task(projectModel.WorkItemID.Value.ToString(), projectModel.WorkItemName);
            ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(project_AuditOperation_TaskCompleteEvent);
            ((MySubject)np).CompleteEvent += new MySubject.CompleteHandler(project_AuditOperation_CompleteEvent);
        }
        catch (Exception ex2)
        {
            Response.Write("np load_task and event" + ex2.Message);
        }
        try
        {
            np.complete_task(projectModel.WorkItemID.Value.ToString(), projectModel.WorkItemName, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());
        }
        catch (Exception ex)
        {
            Response.Write("np complete_task" + ex.Message);
        }
        //所有工作项都已经完成，工作流结束

        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号申请审核成功！');", true);

    }


    protected void btnTip_Click(object sender, EventArgs e)
    {
        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
        audit.FormID = projectid;
        audit.Suggestion = this.txtAuditRemark.Text;
        audit.AuditDate = DateTime.Now;
        audit.AuditorSysID = int.Parse(CurrentUser.SysID);
        audit.AuditorUserCode = CurrentUser.ID;
        audit.AuditorEmployeeName = CurrentUser.Name;
        audit.AuditorUserName = CurrentUser.ITCode;
        audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
        audit.FormType = (int)ESP.Finance.Utility.FormType.Project;
        int ret = ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('留言保存成功！');", true);

    }


    void project_AuditOperation_CompleteEvent(Hashtable context)
    {

    }

    private string GetAuditLog(int pid)
    {
        projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(pid);
        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetProjectList(projectModel.ProjectId);

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
    void project_AuditOperation_TaskCompleteEvent(Hashtable context)
    {
        int sysId = 0;
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);

        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectModel.GroupID.Value);

        ESP.Compatible.Employee employee = null;
        string exMail = string.Empty;

        projectModel.ProfileReason = this.txtReason.Text;

        if (np.get_lastActivity() != null)//工作流还有后续任务
        {
            //写序列化
            workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(projectModel));
            //更新申请单状态
            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectModel, ESP.Finance.Utility.Status.BizAuditing);
            SetAuditHist(projectModel, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);
            //发信
            sysId = int.Parse(np.get_lastActivity().getWorkItem().RoleID);
            employee = new ESP.Compatible.Employee(sysId);
            ArrayList notifylist = (ArrayList)context[ContextConstants.NOTIFY_LIST];//得到当前workitem的会知人的ID
            if (notifylist != null && notifylist.Count != 0)//如果有会知列表
            {
                string ZHEmails = "";
                for (int i = 0; i < notifylist.Count; i++)//循环查找会知人的email
                {
                    ESP.Compatible.Employee zhEmployee = new ESP.Compatible.Employee(int.Parse(notifylist[i].ToString()));
                    ZHEmails += getEmployeeEmailBySysUserId(int.Parse(notifylist[i].ToString())) + ",";
                }
                try
                {
                    if (ZHEmails != "")
                    {
                        SendMailHelper.SendMailToZH(projectModel, CurrentUser.Name, employee.Name, ZHEmails.TrimEnd(','));
                    }
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
            }

            try
            {
                SendMailHelper.SendMailBizOK(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(sysId), "Biz");

                ESP.Framework.Entity.AuditBackUpInfo delegateUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(sysId);
                if (delegateUser != null)
                {
                    SendMailHelper.SendMailBizOK(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(delegateUser.BackupUserID), "Biz");
                }
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }

        }
        else//业务审核结束
        {
            ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(projectModel.GroupID.Value);

            //更新申请单状态
            var projectStatus = ESP.Finance.Utility.Status.BizAuditComplete;
            if (string.IsNullOrEmpty(projectModel.ProjectCode) && manageModel.RiskControlAccounter > 0)
            {
                projectStatus = ESP.Finance.Utility.Status.WaitRiskControl;
            }
            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectModel, projectStatus);
            SetAuditHist(projectModel, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);
            sysId = Convert.ToInt32(CurrentUser.SysID);
            employee = new ESP.Compatible.Employee(sysId);
            IList<ESP.Finance.Entity.ContractAuditLogInfo> calist = ESP.Finance.BusinessLogic.ContractAuditLogManager.GetList(" ProjectId=" + projectModel.ProjectId.ToString());

            //发信到财务合同审核人
            try
            {
                if (string.IsNullOrEmpty(projectModel.ProjectCode) && manageModel.RiskControlAccounter > 0)
                {
                    int RCAuditer = manageModel.RiskControlAccounter;

                    SendMailHelper.SendMailBizOK(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(RCAuditer), "Finance");
                    ESP.Framework.Entity.AuditBackUpInfo FinanceUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(RCAuditer);
                    if (FinanceUser != null)
                    {
                        SendMailHelper.SendMailBizOK(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(FinanceUser.BackupUserID), "Finance");
                    }
                }
                else
                {
                    if ((projectModel.ContractStatusID == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.CAStatus)
                        || projectModel.ContractStatusID == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.FCAStatus))
                        && (calist == null || calist.Count == 0))
                    {
                        SendMailHelper.SendMailBizOK(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(Convert.ToInt32(branchModel.ContractAccounter)), "Finance");
                        ESP.Framework.Entity.AuditBackUpInfo ContractUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(branchModel.ContractAccounter));
                        if (ContractUser != null)
                        {
                            SendMailHelper.SendMailBizOK(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(ContractUser.BackupUserID), "Finance");
                        }
                    }
                    else//否则不需要合同审批，直接发到财务人员
                    {
                        int financeAuditer = branchModel.ProjectAccounter;
                        if (branchProject != null)
                            financeAuditer = branchProject.AuditorID;

                        SendMailHelper.SendMailBizOK(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(financeAuditer), "Finance");
                        ESP.Framework.Entity.AuditBackUpInfo FinanceUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(financeAuditer);
                        if (FinanceUser != null)
                        {
                            SendMailHelper.SendMailBizOK(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(FinanceUser.BackupUserID), "Finance");
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
    protected void btnTerminate_Click(object sender, EventArgs e)
    {
        if (!GetWorkFlowList())
        {

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;



        }
        //创建工作流实例
        context = new Hashtable();
        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
        SetWorkFlowData(projectModel);
        WFUSERS wfuser = new WFUSERS();
        wfuser.Id = projectModel.ApplicantUserID;
        initiators = new WFUSERS[1];
        initiators[0] = wfuser;
        context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
        context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_NAME, "项目号申请业务审核");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "项目号申请业务审核");//提交操作代码：1

        np = processMgr.load_process(projectModel.ProcessID.Value, projectModel.InstanceID.Value.ToString(), context);
        ((MySubject)np).TerminateEvent += new MySubject.TerminateHandler(project_AuditOperation_TerminateEvent);
        np.terminate();
    }

    private void SetAuditHist(ESP.Finance.Entity.ProjectInfo model, int auditStatus)
    {
        string term = " projectid=@projectid  and auditstatus=@auditstatus";
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@projectid", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = model.ProjectId;
        paramlist.Add(p1);
        //System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@auditoruserid", System.Data.SqlDbType.Int, 4);
        //p2.SqlValue = CurrentUser.SysID;
        //paramlist.Add(p2);
        System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@auditstatus", System.Data.SqlDbType.Int, 4);
        p3.SqlValue = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
        paramlist.Add(p3);
        IList<ESP.Finance.Entity.AuditHistoryInfo> auditlist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term, paramlist);
        if (auditlist != null && auditlist.Count > 0)
        {
            ESP.Finance.Entity.AuditHistoryInfo audit = auditlist[0];
            if (audit.AuditorUserID.Value != Convert.ToInt32(CurrentUser.SysID))
            {
                audit.Suggestion = this.txtAuditRemark.Text + "[" + CurrentUser.Name + "为" + audit.AuditorEmployeeName + "的代理人]";
            }
            else
            {
                audit.Suggestion = this.txtAuditRemark.Text;
            }
            audit.AuditStatus = auditStatus;
            audit.AuditDate = DateTime.Now;
            ESP.Finance.BusinessLogic.AuditHistoryManager.Update(audit);
        }
    }

    void project_AuditOperation_TerminateEvent(Hashtable context)
    {
        string exMail = string.Empty;
        SetAuditHist(projectModel, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
        if (projectModel.ProcessID != null && projectModel.InstanceID != null)
        {
            PIDao.TerminateProcess(projectModel.ProcessID.Value, projectModel.InstanceID.Value);
        }
        //更新申请单状态
        ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectModel, ESP.Finance.Utility.Status.BizReject);
        //驳回
        try
        {
            SendMailHelper.SendMailAuditBizReject(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(Convert.ToInt32(projectModel.CreatorID)));
            SendMailHelper.SendMailAuditBizReject(projectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(Convert.ToInt32(projectModel.ApplicantUserID)));
        }
        catch
        {
            exMail = "(邮件发送失败!)";
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号申请单驳回成功！"+exMail+"');", true);

    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }

    private string getEmployeeEmailBySysUserId(int SysUserId)
    {
        return new ESP.Compatible.Employee(SysUserId).EMail;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!GetWorkFlowList())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        query = query.AddParam(RequestName.Operate, "BizAudit");
        Response.Redirect("ProjectEdit.aspx?" + query);
    }

    private string GetBackUrl()
    {
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "ProjectAuditList.aspx" : Request["BackUrl"];
    }

}