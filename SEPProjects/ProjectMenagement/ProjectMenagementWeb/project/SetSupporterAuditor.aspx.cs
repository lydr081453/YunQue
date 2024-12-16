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

public partial class project_SetSupporterAuditor : ESP.Web.UI.PageBase
{
    int SupportID = 0;
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    ModelProcess mp;//模板工作流的实例
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.ProcessInstanceDao PIDao;//工作流数据访问对象
    PROCESSINSTANCES pi;//一个工作流实例
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
    protected ESP.Finance.Entity.SupporterInfo SupportModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
        {
            SupportID = int.Parse(Request[ESP.Finance.Utility.RequestName.SupportID]);
            SupportModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(SupportID);
            if (!IsPostBack)
            {
                ViewContorl(SupportModel);
            }
        }
    }


    private void ViewContorl(ESP.Finance.Entity.SupporterInfo model)
    {
        bool isHaveZJ = false;
        //bool isHaveZJL = false;
       // bool isHaveFA = false;
        string removeTypes = "";
       // string NoFADept = System.Configuration.ConfigurationManager.AppSettings["DoNotNeedFA"].ToString();
        SupportID = int.Parse(Request[ESP.Finance.Utility.RequestName.SupportID]);
        SupportModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(SupportID);
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Contract + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2 + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3 + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLZH + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";

         int[] depts = new ESP.Compatible.Employee(SupportModel.LeaderUserID.Value).GetDepartmentIDs();
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(depts[0]);

        if (!isHaveZJ)
        {
            if (manageModel != null)
            {
                //if (!CheckDuplicate(operationList, manageModel.DirectorId))
                //{
                //默认总监
                System.Web.UI.HtmlControls.HtmlTableRow ZJRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell ZJCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                ZJCell1.Align = "Center";
                ZJCell2.Align = "Center";
                ZJCell3.Align = "Center";
                ZJCell4.Align = "Center";
                ZJCell5.Align = "Center";
                ZJCell6.Align = "Center";

                ZJCell1.InnerHtml = "1";
                ZJCell2.InnerHtml = manageModel.DirectorName;
                ZJCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.DirectorId).PositionDescription;
                ZJCell4.InnerHtml = manageModel.DirectorAmount.ToString("#,##0.00");
                string trId = ZJRow.Attributes["id"] = "tr_" + manageModel.DirectorId + "_ZJSP";
                ZJCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP];
                ZJCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + manageModel.DirectorId + "');return false;\">更改</a>";//&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + manageModel.DirectorId + "','" + trId + "');return false;\">删除</a>";

                hidZJSP.Value = manageModel.DirectorId.ToString();
                ZJRow.Cells.Add(ZJCell1);
                ZJRow.Cells.Add(ZJCell2);
                ZJRow.Cells.Add(ZJCell3);
                ZJRow.Cells.Add(ZJCell4);
                ZJRow.Cells.Add(ZJCell5);
                ZJRow.Cells.Add(ZJCell6);
                ZJRow.Attributes["class"] = "td";
                tab.Rows.Add(ZJRow);
                //}
            }
        }

    }


    protected void btnCommit_Click(object sender, EventArgs e)
    {
        try
        {
            SupportID = int.Parse(Request[ESP.Finance.Utility.RequestName.SupportID]);
            SupportModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(SupportID);
            int processid = createTemplateProcess(SupportModel, "commit");
            if (processid > 0)
            {

                //创建工作流实例
                SupportModel.Status = (int)ESP.Finance.Utility.Status.Submit;
                mp = manager.loadProcessModelByID(processid);
                context = new Hashtable();
                //创建一个发起者，实际应用时就是单据的创建者
                WFUSERS wfuser = new WFUSERS();
                wfuser.Id = SupportModel.LeaderUserID.Value;
                initiators = new WFUSERS[1];
                initiators[0] = wfuser;
                context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
                context.Add(ContextConstants.CURRENT_USER_ASSIGNMENT, initiators);//将发起人数组加入上下文
                context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
                context.Add(ContextConstants.SUBMIT_ACTION_NAME, "支持方申请业务审核");//提交操作代码：1
                context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "支持方申请业务审核");//提交操作代码：1


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

                SupportModel.WorkItemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
                SupportModel.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
                SupportModel.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
                SupportModel.ProcessID = processid;
                //complete start任务
                np.load_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME);
                ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(Purchase_Requisition_SetOperationAudit_TaskCompleteEvent);
                np.complete_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());

                ESP.Finance.BusinessLogic.SupporterManager.Update(SupportModel);
                ESP.Finance.BusinessLogic.SupporterManager.UpdateWorkFlow(SupportModel.SupportID, SupportModel.WorkItemID.Value, SupportModel.WorkItemName, SupportModel.ProcessID.Value, SupportModel.InstanceID.Value);


                int firstAuditorId = 0;
                string exMail = string.Empty;

                if (hidYS.Value != "")
                {
                    firstAuditorId = int.Parse(hidYS.Value.Split(',')[0]);
                }
                else if (hidFA.Value != "")
                {
                    firstAuditorId = int.Parse(hidFA.Value.Split(',')[0]);
                }
                else
                {
                    string[] majordomoIds = hidZJSP.Value.TrimEnd(',').Split(',');
                    List<string> ZH1Ids = hidZJZH.Value.TrimEnd(',').Split(',').ToList();
                    string ZH1Emails = "";
                    firstAuditorId = int.Parse(majordomoIds[0]);
                    for (int i = 0; i < ZH1Ids.Count; i++)
                    {
                        if (ZH1Ids[i].Trim() != "")
                            ZH1Emails += new ESP.Compatible.Employee(int.Parse(ZH1Ids[i])).EMail + ",";
                    }
                    try
                    {
                        //给知会人员发信
                        if (ZH1Emails != "")
                        {
                            SendMailHelper.SendSupporterMailToZH2(SupportModel, new ESP.Compatible.Employee(firstAuditorId).Name, ZH1Emails.TrimEnd(','));
                        }
                    }
                    catch
                    {
                        exMail = "(邮件发送失败!)";
                    }
                }
                try
                {
                    SendMailHelper.SendSupporterMail(SupportModel, "", getEmployeeEmailBySysUserId(Convert.ToInt32(SupportModel.LeaderUserID.Value)), getEmployeeEmailBySysUserId(firstAuditorId));
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
                string backUrl = string.Empty;
                if (!string.IsNullOrEmpty(Request[RequestName.Operate]))
                {
                    backUrl = "/Search/SupporterTabList.aspx";
                }
                else
                {
                    backUrl = "/Edit/SupporterTabEdit.aspx";
                }
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='" + backUrl + "';alert('已成功设置支持方申请业务审批人并提交成功！"+exMail+"');", true);
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('设置支持方申请业务审核人失败！');", true);
            Console.WriteLine(ex.Message);
        }
    }

    private string getEmployeeEmailBySysUserId(int SysUserId)
    {
        return new ESP.Compatible.Employee(SysUserId).EMail;
    }

    void Purchase_Requisition_SetOperationAudit_TaskCompleteEvent(Hashtable context)
    {
        workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(SupportModel));

    }

    private bool CheckDuplicate(IList<ESP.Finance.Entity.SupporterAuditHistInfo> histList, int UserID)
    {
        bool retValue = false;
        if (UserID == Convert.ToInt32(CurrentUser.SysID))
        {
            return true;
        }
        if (histList != null && histList.Count > 0)
        {
            foreach (ESP.Finance.Entity.SupporterAuditHistInfo operation in histList)
            {
                if (UserID == operation.AuditorUserID.Value)
                {
                    retValue = true;
                    break;
                }
            }
        }
        return retValue;
    }
    private int createTemplateProcess(ESP.Finance.Entity.SupporterInfo model, string flag)
    {
        int ret;
        string checkAuditor = ",";
        ESP.Finance.Entity.ProjectInfo ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID);
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(ProjectModel.BranchID.Value);
        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, model.GroupID.Value);

        List<ESP.Finance.Entity.SupporterAuditHistInfo> auditList = new List<ESP.Finance.Entity.SupporterAuditHistInfo>();
        ModelTemplate.BLL.ModelManager manager = new ModelManager();
        List<ModelTemplate.ModelTask> lists = new List<ModelTask>();
        ModelTemplate.ModelTask task = new ModelTask();
        task.TaskName = "start";
        task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
        task.DisPlayName = "start";
        task.RoleName = model.LeaderUserID.ToString();

        ModelTemplate.ModelTask lastTask = null;

        //预审人
        if (hidYS.Value != "")
        {
            string[] prejudicationIds = hidYS.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < prejudicationIds.Length; i++)
            {
                if (prejudicationIds[i].Trim() != "")
                {
                    if (checkAuditor.IndexOf(prejudicationIds[i].Trim()) < 0)
                    {
                        checkAuditor += prejudicationIds[i].Trim() + ",";
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(prejudicationIds[i]));
                        ModelTemplate.ModelTask prejudicationTask = new ModelTask();
                        prejudicationTask.TaskName = "支持方申请预审业务审核" + emp.Name + prejudicationIds[i];
                        prejudicationTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        prejudicationTask.DisPlayName = "支持方申请预审业务审核" + emp.Name + prejudicationIds[i];
                        prejudicationTask.RoleName = prejudicationIds[i];

                        ESP.Finance.Entity.SupporterAuditHistInfo responser = new ESP.Finance.Entity.SupporterAuditHistInfo();
                        responser.AuditorEmployeeName = emp.Name;
                        responser.AuditorUserCode = emp.ID;
                        responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                        responser.AuditorUserName = emp.ITCode;
                        responser.SupporterID = model.SupportID;
                        responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                        responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                        responser.SquenceLevel = auditList.Count + 1;
                        auditList.Add(responser);


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
                }
            }
        }

        //FA
        if (hidFA.Value != "")
        {
            if (checkAuditor.IndexOf(hidFA.Value.TrimEnd(',')) < 0)
            {
                checkAuditor += hidFA.Value.TrimEnd(',');
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidFA.Value.TrimEnd(',')));
                ModelTemplate.ModelTask FATask = new ModelTask();
                FATask.TaskName = "支持方申请FA业务审核" + emp.Name + hidFA.Value.TrimEnd(',');
                FATask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                FATask.DisPlayName = "支持方申请FA业务审核" + emp.Name + hidFA.Value.TrimEnd(',');
                FATask.RoleName = hidFA.Value.TrimEnd(',');

                ESP.Finance.Entity.SupporterAuditHistInfo responser = new ESP.Finance.Entity.SupporterAuditHistInfo();
                responser.AuditorEmployeeName = emp.Name;
                responser.AuditorUserCode = emp.ID;
                responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                responser.AuditorUserName = emp.ITCode;
                responser.SupporterID = model.SupportID;
                responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_FA;
                responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                responser.SquenceLevel = auditList.Count + 1;
                auditList.Add(responser);

                if (lastTask != null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = FATask.DisPlayName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);
                }
                else
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
                    trans.TransitionTo = FATask.TaskName;
                    task.Transations.Add(trans);
                    lists.Add(task);
                }
                lastTask = FATask;
            }
        }
        //总监级知会人
        if (hidZJZH.Value != "")
        {
            string[] ZH1Ids = hidZJZH.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZH1Ids.Length; i++)
            {
                if (ZH1Ids[i].Trim() != "")
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(ZH1Ids[i]));
                    ModelTemplate.ModelTask ZHMajordomoTask = new ModelTask();
                    ZHMajordomoTask.TaskName = "支持方申请业务审核总监知会" + emp.Name + ZH1Ids[i];
                    ZHMajordomoTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHMajordomoTask.DisPlayName = "支持方申请业务审核总监知会" + emp.Name + ZH1Ids[i];
                    ZHMajordomoTask.RoleName = ZH1Ids[i];

                    //ESP.Finance.Entity.SupporterAuditHistInfo responser = new ESP.Finance.Entity.SupporterAuditHistInfo();
                    //responser.AuditorEmployeeName = emp.Name;
                    //responser.AuditorUserCode = emp.ID;
                    //responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                    //responser.AuditorUserName = emp.ITCode;
                    //responser.SupporterID = model.SupportID;
                    //responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJZH;
                    //responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                    //responser.SquenceLevel = auditList.Count + 1;
                    //auditList.Add(responser);

                    if (lastTask == null)
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = "start";
                        trans.TransitionTo = ZHMajordomoTask.TaskName;
                        task.Transations.Add(trans);
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
        if (hidZJSP.Value != "")
        {
            if (checkAuditor.IndexOf(hidZJSP.Value.TrimEnd(',')) < 0)
            {
                checkAuditor += hidZJSP.Value.TrimEnd(',') + ",";
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidZJSP.Value.TrimEnd(',')));
                ModelTemplate.ModelTask addMajordomoTask = new ModelTask();
                addMajordomoTask.TaskName = "支持方申请总监业务审核" + emp.Name + hidZJSP.Value.TrimEnd(',');
                addMajordomoTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addMajordomoTask.DisPlayName = "支持方申请总监业务审核" + emp.Name + hidZJSP.Value.TrimEnd(',');
                addMajordomoTask.RoleName = hidZJSP.Value.TrimEnd(',');

                ESP.Finance.Entity.SupporterAuditHistInfo responser = new ESP.Finance.Entity.SupporterAuditHistInfo();
                responser.AuditorEmployeeName = emp.Name;
                responser.AuditorUserCode = emp.ID;
                responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                responser.AuditorUserName = emp.ITCode;
                responser.SupporterID = model.SupportID;
                responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                responser.SquenceLevel = auditList.Count + 1;
                auditList.Add(responser);

                if (lastTask == null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
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

                lastTask = addMajordomoTask;
            }
        }
        if (hidZJFJ.Value != "")
        {
            string[] fj1Ids = hidZJFJ.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < fj1Ids.Length; i++)
            {
                if (fj1Ids[i].Trim() != "")
                {
                    if (checkAuditor.IndexOf(fj1Ids[i].Trim()) < 0)
                    {
                        checkAuditor += fj1Ids[i].Trim() + ",";
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(fj1Ids[i]));
                        ModelTemplate.ModelTask append1Task = new ModelTask();
                        append1Task.TaskName = "支持方申请单总监附加业务审核" + emp.Name + fj1Ids[i];
                        append1Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append1Task.DisPlayName = "支持方申请单总监附加业务审核" + emp.Name + fj1Ids[i];
                        append1Task.RoleName = fj1Ids[i].Trim();

                        ESP.Finance.Entity.SupporterAuditHistInfo responser = new ESP.Finance.Entity.SupporterAuditHistInfo();
                        responser.AuditorEmployeeName = emp.Name;
                        responser.AuditorUserCode = emp.ID;
                        responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                        responser.AuditorUserName = emp.ITCode;
                        responser.SupporterID = model.SupportID;
                        responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ;
                        responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                        responser.SquenceLevel = auditList.Count + 1;
                        auditList.Add(responser);

                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = append1Task.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = append1Task;
                    }
                }
            }
        }
        //总经理级知会人
        if (hidZJLZH.Value != "")
        {
            string[] zh2Ids = hidZJLZH.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < zh2Ids.Length; i++)
            {
                if (zh2Ids[i].Trim() != "")
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(zh2Ids[i]));
                    ModelTemplate.ModelTask ZHgeneralTask = new ModelTask();
                    ZHgeneralTask.TaskName = "支持方申请业务审核总经理知会" + emp.Name + zh2Ids[i];
                    ZHgeneralTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHgeneralTask.DisPlayName = "支持方申请业务审核总经理知会" + emp.Name + zh2Ids[i];
                    ZHgeneralTask.RoleName = zh2Ids[i];

                    //ESP.Finance.Entity.SupporterAuditHistInfo responser = new ESP.Finance.Entity.SupporterAuditHistInfo();
                    //responser.AuditorEmployeeName = emp.Name;
                    //responser.AuditorUserCode = emp.ID;
                    //responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                    //responser.AuditorUserName = emp.ITCode;
                    //responser.SupporterID = model.SupportID;
                    //responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLZH;
                    //responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                    //responser.SquenceLevel = auditList.Count + 1;
                    //auditList.Add(responser);

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = ZHgeneralTask.TaskName;
                    lastTask.Transations.Add(trans);

                    lists.Add(ZHgeneralTask);
                }
            }
        }

        //总经理级审核人
        if (hidZJLSP.Value != "")
        {
            if (checkAuditor.IndexOf(hidZJLSP.Value.TrimEnd(',')) < 0)
            {
                checkAuditor += hidZJLSP.Value.TrimEnd(',') + ",";
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidZJLSP.Value.TrimEnd(',')));
                ModelTemplate.ModelTask addGeneralTask = new ModelTask();
                addGeneralTask.TaskName = "支持方申请总经理业务审核" + emp.Name + hidZJLSP.Value.TrimEnd(',');
                addGeneralTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addGeneralTask.DisPlayName = "支持方申请总经理业务审核" + emp.Name + hidZJLSP.Value.TrimEnd(',');
                addGeneralTask.RoleName = hidZJLSP.Value.TrimEnd(',');

                ESP.Finance.Entity.SupporterAuditHistInfo responser = new ESP.Finance.Entity.SupporterAuditHistInfo();
                responser.AuditorEmployeeName = emp.Name;
                responser.AuditorUserCode = emp.ID;
                responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                responser.AuditorUserName = emp.ITCode;
                responser.SupporterID = model.SupportID;
                responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                responser.SquenceLevel = auditList.Count + 1;
                auditList.Add(responser);

                ModelTemplate.Transition trans = new Transition();
                trans.TransitionName = lastTask.DisPlayName;
                trans.TransitionTo = addGeneralTask.DisPlayName;
                lastTask.Transations.Add(trans);

                lists.Add(lastTask);
                lastTask = addGeneralTask;
            }
        }
        //总经理级附加
        if (hidZJLFJ.Value != "")
        {
            string[] fj2Ids = hidZJLFJ.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < fj2Ids.Length; i++)
            {
                if (fj2Ids[i].Trim() != "")
                {
                    if (checkAuditor.IndexOf(fj2Ids[i].Trim()) < 0)
                    {
                        checkAuditor += fj2Ids[i].Trim() + ",";
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(fj2Ids[i]));
                        ModelTemplate.ModelTask append2Task = new ModelTask();
                        append2Task.TaskName = "支持方申请单总经理附加业务审核" + emp.Name + fj2Ids[i];
                        append2Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append2Task.DisPlayName = "支持方申请单总经理附加业务审核" + emp.Name + fj2Ids[i];
                        append2Task.RoleName = fj2Ids[i];
                        ESP.Finance.Entity.SupporterAuditHistInfo responser = new ESP.Finance.Entity.SupporterAuditHistInfo();
                        responser.AuditorEmployeeName = emp.Name;
                        responser.AuditorUserCode = emp.ID;
                        responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                        responser.AuditorUserName = emp.ITCode;
                        responser.SupporterID = model.SupportID;
                        responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ;
                        responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                        responser.SquenceLevel = auditList.Count + 1;
                        auditList.Add(responser);

                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = append2Task.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = append2Task;
                    }
                }
            }
        }
        ModelTemplate.Transition endTrans = new Transition();
        endTrans.TransitionName = lastTask.DisPlayName;
        endTrans.TransitionTo = "end";
        lastTask.Transations.Add(endTrans);
        lists.Add(lastTask);


        ESP.Compatible.Employee financeemp = null;

        if (branchProject == null)
        {
            financeemp = new ESP.Compatible.Employee(branchModel.ProjectAccounter);
        }
        else
        {
            financeemp = new ESP.Compatible.Employee(branchProject.AuditorID);
        }

        ESP.Finance.Entity.SupporterAuditHistInfo contract = new ESP.Finance.Entity.SupporterAuditHistInfo();

        contract.AuditorEmployeeName = financeemp.Name;
        contract.AuditorUserCode = financeemp.ID;
        contract.AuditorUserID = Convert.ToInt32(financeemp.SysID);
        contract.AuditorUserName = financeemp.ITCode;
        contract.SupporterID = model.SupportID;
        contract.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2;
        contract.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
        contract.SquenceLevel = auditList.Count + 1;
        auditList.Add(contract);

        if (flag == "save")
        {
            ret = SaveOperationAuditor(auditList) == true ? 1 : 0;
        }
        else
        {
            if (model.ProcessID != null && model.InstanceID != null)
            {
                PIDao = new ProcessInstanceDao();
                PIDao.TerminateProcess(model.ProcessID.Value, model.InstanceID.Value);
            }
            ret = manager.ImportData("支持方申请业务审核(" + model.ProjectCode + ")", "支持方申请业务审核(" + model.SupportID + ")", "1.0", model.LeaderEmployeeName, lists);
            if (ret > 0)
            {
                SaveOperationAuditor(auditList);
            }
        }
        return ret;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string query = string.Empty;
        query = Request.Url.Query;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.Operate]))
            Response.Redirect("SupporterModify.aspx" + query);
        else
            Response.Redirect("SupporterEdit.aspx" + query);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SupportID = int.Parse(Request[ESP.Finance.Utility.RequestName.SupportID]);
        SupportModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(SupportID);
        try
        {
            if (createTemplateProcess(SupportModel, "save") > 0)
            {
                //if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BackUrl]))
                //    Response.Redirect(Request[ESP.Finance.Utility.RequestName.BackUrl] + "?" + RequestName.SupportID + "=" + Request[ESP.Finance.Utility.RequestName.SupportID] + "&" + RequestName.ProjectID + "=" + Request[ESP.Finance.Utility.RequestName.ProjectID]);
                //else
                //    Response.Redirect("SupporterEdit.aspx?" + RequestName.SupportID + "=" + Request[ESP.Finance.Utility.RequestName.SupportID] + "&" + RequestName.ProjectID + "=" + Request[ESP.Finance.Utility.RequestName.ProjectID]);
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存业务审批人成功！');", true);
            }
            else
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存业务审批人失败！');", true);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('出现未知异常！');", true);
        }
    }

    /// <summary>
    /// 保存业务审批人
    /// </summary>
    /// <returns></returns>
    private bool SaveOperationAuditor(List<ESP.Finance.Entity.SupporterAuditHistInfo> auditList)
    {

        return (ESP.Finance.BusinessLogic.SupporterAuditHistManager.Add(auditList) > 0);
    }
}

