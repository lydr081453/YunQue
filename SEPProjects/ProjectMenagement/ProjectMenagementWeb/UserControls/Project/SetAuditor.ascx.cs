using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;
using ESP.Finance.Utility;
public partial class UserControls_Project_SetAuditor : System.Web.UI.UserControl
{
    int projectid = 0;
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    ModelProcess mp;//模板工作流的实例
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    //WorkFlowDAO.ProcessInstanceDao p;//工作流数据访问对象
    PROCESSINSTANCES pi;//一个工作流实例
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
    protected ESP.Finance.Entity.ProjectInfo projectinfo = null;
   
    private ESP.Compatible.Employee currentUser;
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }
    private int _deptid = 0;
    public int DeptID
    {
        get { return _deptid; }
        set { _deptid = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectid = int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
            this.hidResponser.Value = projectinfo.ApplicantUserID.ToString();
            ViewContorl(projectinfo);
        }

        AddMajordomo.Attributes["onclick"] = "javascript:openEmployee('N4','0');return false;";
        Addgeneral.Attributes["onclick"] = "javascript:openEmployee('N5','0');return false;";
    }

    private void ViewContorl(ESP.Finance.Entity.ProjectInfo model)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int processid = 0;
            //创建工作流实例

            processid = this.createTemplateProcess(projectinfo);
            mp = manager.loadProcessModelByID(processid);
            context = new Hashtable();
            //创建一个发起者，实际应用时就是单据的创建者
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = projectinfo.ApplicantUserID;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.CURRENT_USER_ASSIGNMENT, initiators);//将发起人数组加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "项目号申请单业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "项目号申请单业务审核");//提交操作代码：1


            //设置生成工作流的必备项
            np = processMgr.preCreate_process(Convert.ToInt32(mp.ProcessID), context);

            processMgr.create_process(np, initiators, "start");


            pi = np.get_instance_data();//获取工作流数据
            pi.ACTIVEPERSONID = 1;
            pi.ACTIVEWOEKITEMID = 1;
            pi.NOTIFYPARENTPROCESS = 1;
            pi.PARENTADDRESS = null;
            //以上4参数暂时没有特别用出，使用默认值即可
            //持久化工作流的事件处理
            np.persist();//持久化


            //激活start任务
            np.load_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME);
            np.get_lastActivity().active();

            projectinfo.WorkItemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
            projectinfo.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
            projectinfo.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
            projectinfo.ProcessID = processid;
            ESP.Finance.BusinessLogic.ProjectManager.UpdateWorkFlow(Convert.ToInt32(projectinfo.ProjectId), Convert.ToInt32(projectinfo.WorkItemID), projectinfo.WorkItemName, Convert.ToInt32(projectinfo.ProcessID), Convert.ToInt32(projectinfo.InstanceID));
            //complete start任务
            np.load_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME);
            ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(UserControls_Project_SetAuditor_TaskCompleteEvent);
            np.complete_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());

            
            //$$$$$
            #if debug
                System.Diagnostics.Debug.WriteLine("项目号成本预算");
                Trace.Write("项目号成本预算");
            #endif
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('设置业务审核人失败！');", true);
        }

        try
        {
            int firstAuditorId = 0;
            if (hidprejudication.Value != "")
            {
                firstAuditorId = int.Parse(hidprejudication.Value.Split(',')[0]);
            }
            else
            {
                string[] majordomoIds = hidAddMajordomo.Value.TrimEnd(',').Split(',');
                List<string> HZMajordomoIds = hidZHMajordomo.Value.TrimEnd(',').Split(',').ToList();
                string ZHEmails = "";
                for (int i = 0; i < majordomoIds.Length; i++)
                {
                    if (firstAuditorId == 0 && majordomoIds[i] != "" && !HZMajordomoIds.Contains(majordomoIds[i]))
                        firstAuditorId = int.Parse(majordomoIds[i]);
                    else
                    {
                        ZHEmails += getEmployeeEmailBySysUserId(int.Parse(majordomoIds[i])) + ",";
                    }
                }
                //给知会人员发信
                if (ZHEmails != "")
                {
                    SendMailHelper.SendMailToZH2(projectinfo, new ESP.Compatible.Employee(firstAuditorId).Name, ZHEmails.TrimEnd(','));
                }
            }
            SendMailHelper.SendMailPR(projectinfo, getEmployeeEmailBySysUserId(projectinfo.CreatorID), getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)), getEmployeeEmailBySysUserId(firstAuditorId));
        }
        catch(Exception) 
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('设置业务审核完成，发送邮件时失败！');", true);
        }

            ESP.Finance.BusinessLogic.ProjectManager.Submit(projectinfo);

          Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='ProjectList.aspx';alert('" + projectinfo.SerialCode + "已成功设置业务审核人并提交成功，请在查询中心查询审批状态。');", true);
      
    }

    void UserControls_Project_SetAuditor_TaskCompleteEvent(Hashtable context)
    {
        
        workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(projectinfo));
    }

    /// <summary>
    /// 获得用户邮箱
    /// </summary>
    /// <param name="SysUserId"></param>
    /// <returns></returns>
    private  string getEmployeeEmailBySysUserId(int SysUserId)
    {
        return new ESP.Compatible.Employee(SysUserId).EMail;
    }

    private int createTemplateProcess(ESP.Finance.Entity.ProjectInfo generalInfo)
    {
        int ret=0;
        List<ESP.Finance.Entity.AuditHistoryInfo> auditList = new List<ESP.Finance.Entity.AuditHistoryInfo>();
        ModelTemplate.BLL.ModelManager manager = new ModelManager();
        List<ModelTemplate.ModelTask> lists = new List<ModelTask>();
        ModelTemplate.ModelTask task = new ModelTask();
        task.TaskName = "start";
        task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
        task.DisPlayName = "start";
        task.RoleName = generalInfo.CreatorID.ToString();

        ModelTemplate.ModelTask lastTask = null;

        if (generalInfo.CreatorID != generalInfo.ApplicantUserID)
        {
            ModelTemplate.ModelTask taskResponser = new ModelTask();
            taskResponser.TaskName = "项目号申请单审核" + generalInfo.ApplicantEmployeeName;
            taskResponser.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
            taskResponser.DisPlayName = "项目号申请单审核" + generalInfo.ApplicantEmployeeName;
            taskResponser.RoleName = generalInfo.ApplicantUserID.ToString();

            ModelTemplate.Transition tranResponser = new Transition();
            tranResponser.TransitionName = "start";
            tranResponser.TransitionTo = taskResponser.TaskName;
            task.Transations.Add(tranResponser);
            
            lists.Add(task);
            lastTask = taskResponser;
            ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
            responser.AuditorEmployeeName = generalInfo.ApplicantEmployeeName;
            responser.AuditorUserCode = generalInfo.ApplicantCode;
            responser.AuditorUserID = generalInfo.ApplicantUserID;
            responser.AuditorUserName = generalInfo.ApplicantUserName;
            responser.ProjectID = generalInfo.ProjectId;
            responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
            responser.SquenceLevel = auditList.Count+1 ;
            auditList.Add(responser);
        }
    

        if (hidFA.Value.Trim() != "")
        {
            string[] fas = hidFA.Value.TrimEnd(',').Split(',');
            if (fas[0].Trim() != "")
            {
                ESP.Compatible.Employee emp=new ESP.Compatible.Employee(int.Parse(fas[0]));
                string userName = emp.Name;
                ModelTemplate.ModelTask fatask = new ModelTask();
                fatask.TaskName = "项目号申请单审核" + userName;
                fatask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                fatask.DisPlayName = "项目号申请单审核" + userName;
                fatask.RoleName = fas[0];
                ESP.Finance.Entity.AuditHistoryInfo fa = new ESP.Finance.Entity.AuditHistoryInfo();
                fa.AuditorEmployeeName = emp.Name;
                fa.AuditorUserCode = emp.ID;
                fa.AuditorUserID = Convert.ToInt32(emp.SysID);
                fa.AuditorUserName = emp.ITCode;
                fa.ProjectID = generalInfo.ProjectId;
                fa.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                if (lastTask == null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
                    trans.TransitionTo = fatask.TaskName;
                    task.Transations.Add(trans);
                    lists.Add(task);
                }
                else
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = fatask.TaskName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);
                }
                fa.SquenceLevel = auditList.Count + 1;
                auditList.Add(fa);
                lastTask=fatask;
            }
        }
        


        //预审人
        if (hidprejudication.Value != "")
        {
            string[] prejudicationIds = hidprejudication.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < prejudicationIds.Length; i++)
            {
                if (prejudicationIds[i].Trim() != "")
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(prejudicationIds[i]));
                    string userName = emp.Name;
                    ModelTemplate.ModelTask prejudicationTask = new ModelTask();
                    prejudicationTask.TaskName = "项目号申请单审核" + userName;
                    prejudicationTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    prejudicationTask.DisPlayName = "项目号申请单审核" + userName;
                    prejudicationTask.RoleName = prejudicationIds[i];

                    ESP.Finance.Entity.AuditHistoryInfo pre = new ESP.Finance.Entity.AuditHistoryInfo();
                    pre.AuditorEmployeeName = emp.Name;
                    pre.AuditorUserCode = emp.ID;
                    pre.AuditorUserID = Convert.ToInt32(emp.SysID);
                    pre.AuditorUserName = emp.ITCode;
                    pre.ProjectID = generalInfo.ProjectId;
                    pre.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                    if (lastTask == null)
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = task.TaskName;
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
                    pre.SquenceLevel =auditList.Count+1;
                    auditList.Add(pre);
                    lastTask = prejudicationTask;
                }
            }
        }

        //总监级知会人
        if (hidZHMajordomo.Value != "")
        {
            string[] ZHMajordomoIds = hidZHMajordomo.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZHMajordomoIds.Length; i++)
            {
                if (ZHMajordomoIds[i].Trim() != "")
                {
                    string userName = new ESP.Compatible.Employee(int.Parse(ZHMajordomoIds[i])).Name;
                    ModelTemplate.ModelTask ZHMajordomoTask = new ModelTask();
                    ZHMajordomoTask.TaskName = "项目号申请单知会" + userName;
                    ZHMajordomoTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHMajordomoTask.DisPlayName = "项目号申请单知会" + userName;
                    ZHMajordomoTask.RoleName = ZHMajordomoIds[i];

                    if (lastTask == null)
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = task.TaskName;
                        trans.TransitionTo = ZHMajordomoTask.TaskName;
                        task.Transations.Add(trans);
                        lists.Add(task);
                    }
                    else
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.TaskName;
                        trans.TransitionTo = ZHMajordomoTask.TaskName;
                        lastTask.Transations.Add(trans);
                    }
                    lists.Add(ZHMajordomoTask);

                }
            }
        }

        //总监级审核人
        if (hidAddMajordomo.Value != "")
        {
            string[] addMajordomoIds = hidAddMajordomo.Value.TrimEnd(',').Split(',');
            List<string> HzIdList = hidZHMajordomo.Value.Trim(',').Split(',').ToList();
            for (int i = 0; i < addMajordomoIds.Length; i++)
            {
                if (addMajordomoIds[i].Trim() != "" && !HzIdList.Contains(addMajordomoIds[i].Trim()))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(addMajordomoIds[i]));

                    string userName = emp.Name;
                    ModelTemplate.ModelTask addMajordomoTask = new ModelTask();
                    addMajordomoTask.TaskName = "项目号申请单审核" + userName;
                    addMajordomoTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    addMajordomoTask.DisPlayName = "项目号申请单审核" + userName;
                    addMajordomoTask.RoleName = addMajordomoIds[i];

                    ESP.Finance.Entity.AuditHistoryInfo Major = new ESP.Finance.Entity.AuditHistoryInfo();
                    Major.AuditorEmployeeName = emp.Name;
                    Major.AuditorUserCode = emp.ID;
                    Major.AuditorUserID = Convert.ToInt32(emp.SysID);
                    Major.AuditorUserName = emp.ITCode;
                    Major.ProjectID = generalInfo.ProjectId;
                    Major.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                    if (lastTask == null)
                    {
                            ModelTemplate.Transition trans = new Transition();
                            trans.TransitionName =task.TaskName;
                            trans.TransitionTo = addMajordomoTask.TaskName;
                            task.Transations.Add(trans);
                            lists.Add(task);
                    }
                    else
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.TaskName;
                        trans.TransitionTo = addMajordomoTask.TaskName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);
                    }
                    Major.SquenceLevel = auditList.Count+1;
                    auditList.Add(Major);

                    lastTask = addMajordomoTask;
                }
            }
        }

        //总经理上附加审核人
        if (hidAppend1.Value != "")
        {
            string[] append1Ids = hidAppend1.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < append1Ids.Length; i++)
            {
                if (append1Ids[i].Trim() != "")
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(append1Ids[i]));
                    string userName = emp.Name;
                    ModelTemplate.ModelTask append1Task = new ModelTask();
                    append1Task.TaskName = "项目号申请单审核" + userName;
                    append1Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    append1Task.DisPlayName = "项目号申请单审核" + userName;
                    append1Task.RoleName = append1Ids[i];

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = append1Task.DisPlayName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);

                    ESP.Finance.Entity.AuditHistoryInfo append1 = new ESP.Finance.Entity.AuditHistoryInfo();
                    append1.AuditorEmployeeName = emp.Name;
                    append1.AuditorUserCode = emp.ID;
                    append1.AuditorUserID = Convert.ToInt32(emp.SysID);
                    append1.AuditorUserName = emp.ITCode;
                    append1.ProjectID = generalInfo.ProjectId;
                    append1.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                    append1.SquenceLevel = auditList.Count+1;
                    auditList.Add(append1);
                    lastTask = append1Task;
                }
            }
        }

        //总经理级知会人
        if (hidZHgeneral.Value != "")
        {
            string[] ZHgeneralIds = hidZHgeneral.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZHgeneralIds.Length; i++)
            {
                if (ZHgeneralIds[i].Trim() != "")
                {
                    string userName = new ESP.Compatible.Employee(int.Parse(ZHgeneralIds[i])).Name;
                    ModelTemplate.ModelTask ZHgeneralTask = new ModelTask();
                    ZHgeneralTask.TaskName = "项目号申请单知会" + userName;
                    ZHgeneralTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHgeneralTask.DisPlayName = "项目号申请单知会" + userName;
                    ZHgeneralTask.RoleName = ZHgeneralIds[i];

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = ZHgeneralTask.TaskName;
                    lastTask.Transations.Add(trans);

                    lists.Add(ZHgeneralTask);

                }
            }
        }

        //总经理级审核人
        if (hidAddgeneral.Value != "")
        {
            string[] addGeneralIds = hidAddgeneral.Value.TrimEnd(',').Split(',');
            List<string> HzIdList = hidZHgeneral.Value.Trim(',').Split(',').ToList();
            for (int i = 0; i < addGeneralIds.Length; i++)
            {
                if (addGeneralIds[i].Trim() != "" && !HzIdList.Contains(addGeneralIds[i].Trim()))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(addGeneralIds[i]));
                    string userName = emp.Name;
                    ModelTemplate.ModelTask addGeneralTask = new ModelTask();
                    addGeneralTask.TaskName = "项目号申请单审核" + userName;
                    addGeneralTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    addGeneralTask.DisPlayName = "项目号申请单审核" + userName;
                    addGeneralTask.RoleName = addGeneralIds[i];

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = addGeneralTask.DisPlayName;
                    lastTask.Transations.Add(trans);

                    lists.Add(lastTask);

                    ESP.Finance.Entity.AuditHistoryInfo general = new ESP.Finance.Entity.AuditHistoryInfo();
                    general.AuditorEmployeeName = emp.Name;
                    general.AuditorUserCode = emp.ID;
                    general.AuditorUserID = Convert.ToInt32(emp.SysID);
                    general.AuditorUserName = emp.ITCode;
                    general.ProjectID = generalInfo.ProjectId;
                    general.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                    general.SquenceLevel = auditList.Count+1;
                    auditList.Add(general);

                    lastTask = addGeneralTask;
                }
            }
        }

        ModelTemplate.Transition endTrans = new Transition();
        endTrans.TransitionName = lastTask.DisPlayName;
        endTrans.TransitionTo = "end";
        lastTask.Transations.Add(endTrans);
        lists.Add(lastTask);

        ret = manager.ImportData("项目号申请单业务审核(" + generalInfo.SerialCode + ")", "项目号申请单业务审核(" + generalInfo.SerialCode + ")", "1.0", generalInfo.ApplicantEmployeeName, lists);

        ESP.Finance.BusinessLogic.AuditHistoryManager.Add(auditList);
        return ret;
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        string query = Request.Url.Query;
        Response.Redirect("ProjectStep21.aspx"+query);
    }
}
