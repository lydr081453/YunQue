using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;
using SerializeFactory = WorkFlowDAO.SerializeFactory;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

public partial class UserControls_Edit_setAuditor : System.Web.UI.UserControl
{
    int generalid = 0;
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    ModelProcess mp;//模板工作流的实例
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.ProcessInstanceDao p;//工作流数据访问对象
    PROCESSINSTANCES pi;//一个工作流实例
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
    protected GeneralInfo generalInfo = null;

    private ESP.Compatible.Employee currentUser;
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            generalInfo = GeneralInfoManager.GetModel(generalid);
            if (generalInfo.status != State.requisition_save)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此条数据已经提交，不能重复提交！');window.location.href='/Purchase/Default.aspx';", true);
            ViewContorl(generalInfo);
        }

        AddMajordomo.Attributes["onclick"] = "javascript:openEmployee('N4','" + OperationAuditManageManager.GetDirectorIds() + "');return false;";
        Addgeneral.Attributes["onclick"] = "javascript:openEmployee('N5','" + OperationAuditManageManager.GetManagerIds() + "');return false;";
    }

    /// <summary>
    /// Views the contorl.
    /// </summary>
    /// <param name="model">The model.</param>
    private void ViewContorl(GeneralInfo model)
    {
        if (model.totalprice <= 100000)
        {
            palGeneral.Visible = false;
            palCEO.Visible = false;
        }
        else
        {
            //int[] depts = new ESP.Compatible.Employee(model.requestor).GetDepartmentIDs();
            //OperationAuditManageInfo manageModel = OperationAuditManageManager.GetModelByUserId(model.requestor);
            ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;

            if (model.Project_id != 0)
            {
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(model.Project_id);
            }
            if (manageModel == null)
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.requestor); ;

            if (manageModel == null)
                manageModel = OperationAuditManageManager.GetModelByDepId(model.Departmentid);

            labCEO.Text = manageModel.CEOName;
            hidCEO.Value = manageModel.CEOId.ToString();
        }
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int processid = 0;
            //创建工作流实例

            processid = this.createTemplateProcess(generalInfo);
            mp = manager.loadProcessModelByID(processid);
            context = new Hashtable();
            //创建一个发起者，实际应用时就是单据的创建者
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = generalInfo.requestor;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.CURRENT_USER_ASSIGNMENT, initiators);//将发起人数组加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR单业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR单业务审核");//提交操作代码：1


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

            generalInfo.WorkitemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
            generalInfo.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
            generalInfo.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
            generalInfo.ProcessID = processid;
            //complete start任务
            np.load_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME);
            ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(Purchase_Requisition_SetOperationAudit_TaskCompleteEvent);
            np.complete_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());

            generalInfo.status = State.requisition_operationAduit;//业务审核
            generalInfo.PrNo = GeneralInfoManager.createPrNo();//生成申请单号
            GeneralInfoManager.Update(generalInfo);
            //如果申请单为协议供应商删除付款账期信息
            if ((generalInfo.status == State.requisition_commit || generalInfo.status == State.requisition_temporary_commit) && generalInfo.source == "协议供应商")
            {
                int periodsCount = PaymentPeriodManager.GetList(" gid = " + generalInfo.id).Tables[0].Rows.Count;

                if (periodsCount == 0)
                {
                    PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                    paymentPeriod.gid = generalInfo.id;
                    //paymentPeriod.periodPrice = (generalInfo.totalprice - decimal.Parse(generalInfo.sow4));
                    //paymentPeriod.expectPaymentPrice = generalInfo.totalprice;
                    if (generalInfo.totalprice == 0)
                    {
                        paymentPeriod.expectPaymentPrice = getTotalPrice(" general_id = " + generalInfo.id);
                    }
                    else
                    {
                        paymentPeriod.expectPaymentPrice = generalInfo.totalprice;
                    }
                    paymentPeriod.expectPaymentPercent = 100;
                    //paymentPeriod.beginDate = DateTime.Now.AddDays(30);
                    paymentPeriod.periodDay = "45-60";
                    paymentPeriod.beginDate = DateTime.Parse(DateTime.Now.AddMonths(2).ToString("yyyy-MM") + "-01").AddDays(14);
                    paymentPeriod.endDate = DateTime.Parse(DateTime.Now.AddMonths(3).ToString("yyyy-MM") + "-01").AddDays(-1);
                    paymentPeriod.periodType = (int)State.PeriodType.period;

                    PaymentPeriodManager.Add(paymentPeriod);
                }
            }

            //发信
            LogInfo log = new LogInfo();
            log.Gid = generalInfo.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = int.Parse(CurrentUser.SysID);
            log.Des = string.Format(State.log_requisition_commit, CurrentUser.Name + "(" + currentUser.ITCode + ")", DateTime.Now.ToString());
            LogManager.AddLog(log, Request);

            string exMail = string.Empty;
            try
            {
                int firstAuditorId = 0;//(hidprejudication.Value.Split(',').Length == 0 || hidprejudication.Value.Split(',')[0].Trim() == "") ? int.Parse(hidAddMajordomo.Value.Split(',')[0].Trim()) : int.Parse(hidprejudication.Value.Split(',')[0]);
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
                            ZHEmails += State.getEmployeeEmailBySysUserId(int.Parse(majordomoIds[i])) + ",";
                    }
                    //给知会人员发信
                    if (ZHEmails != "")
                    {
                        SendMailHelper.SendMailToZH2(generalInfo, generalInfo.PrNo, CurrentUser.Name, new ESP.Compatible.Employee(firstAuditorId).Name, ZHEmails.TrimEnd(','));
                    }
                }
                string ret = SendMailHelper.SendMailPR(generalInfo, Request, generalInfo.PrNo, generalInfo.requestorname, State.getEmployeeEmailBySysUserId(generalInfo.enduser), State.getEmployeeEmailBySysUserId(generalInfo.goods_receiver), State.getEmployeeEmailBySysUserId(firstAuditorId));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }

            Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='RequisitionSaveList.aspx';alert('" + generalInfo.PrNo + "已成功设置业务审核人并提交成功，请在查询中心查询审批状态。"+exMail+"');", true);
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('设置业务审核人失败！')", true);
        }
    }

    /// <summary>
    /// Gets the total price.
    /// </summary>
    /// <param name="term">The term.</param>
    /// <returns></returns>
    public decimal getTotalPrice(string term)
    {
        DataSet ds = OrderInfoManager.GetList(term);

        decimal totalprice = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            totalprice += decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString());
        }
        return totalprice;
    }

    /// <summary>
    /// Purchase_s the requisition_ set operation audit_ task complete event.
    /// </summary>
    /// <param name="context">The context.</param>
    void Purchase_Requisition_SetOperationAudit_TaskCompleteEvent(Hashtable context)
    {
        workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(generalInfo));
    }

    /// <summary>
    /// Creates the template process.
    /// </summary>
    /// <param name="generalInfo">The general info.</param>
    /// <returns></returns>
    private int createTemplateProcess(GeneralInfo generalInfo)
    {
        int ret;
        ModelTemplate.BLL.ModelManager manager = new ModelManager();
        List<ModelTemplate.ModelTask> lists = new List<ModelTask>();
        ModelTemplate.ModelTask task = new ModelTask();
        task.TaskName = "start";
        task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
        task.DisPlayName = "start";
        task.RoleName = generalInfo.requestor.ToString();

        ModelTemplate.ModelTask lastTask = null;
        //预审人
        if (hidprejudication.Value != "")
        {
            string[] prejudicationIds = hidprejudication.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < prejudicationIds.Length; i++)
            {
                if (prejudicationIds[i].Trim() != "")
                {
                    string userName = new ESP.Compatible.Employee(int.Parse(prejudicationIds[i])).Name;
                    ModelTemplate.ModelTask prejudicationTask = new ModelTask();
                    prejudicationTask.TaskName = "PR单审核" + userName;
                    prejudicationTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    prejudicationTask.DisPlayName = "PR单审核" + userName;
                    prejudicationTask.RoleName = prejudicationIds[i];

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
                    ZHMajordomoTask.TaskName = "PR单审核" + userName;
                    ZHMajordomoTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHMajordomoTask.DisPlayName = "PR单审核" + userName;
                    ZHMajordomoTask.RoleName = ZHMajordomoIds[i];
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
        if (hidAddMajordomo.Value != "")
        {
            string[] addMajordomoIds = hidAddMajordomo.Value.TrimEnd(',').Split(',');
            List<string> HzIdList = hidZHMajordomo.Value.Trim(',').Split(',').ToList();
            for (int i = 0; i < addMajordomoIds.Length; i++)
            {
                if (addMajordomoIds[i].Trim() != "" && !HzIdList.Contains(addMajordomoIds[i].Trim()))
                {
                    string userName = new ESP.Compatible.Employee(int.Parse(addMajordomoIds[i])).Name;
                    ModelTemplate.ModelTask addMajordomoTask = new ModelTask();
                    addMajordomoTask.TaskName = "PR单审核" + userName;
                    addMajordomoTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    addMajordomoTask.DisPlayName = "PR单审核" + userName;
                    addMajordomoTask.RoleName = addMajordomoIds[i];

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
        }

        //总经理上附加审核人
        if (hidAppend1.Value != "")
        {
            string[] append1Ids = hidAppend1.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < append1Ids.Length; i++)
            {
                if (append1Ids[i].Trim() != "")
                {
                    string userName = new ESP.Compatible.Employee(int.Parse(append1Ids[i])).Name;
                    ModelTemplate.ModelTask append1Task = new ModelTask();
                    append1Task.TaskName = "PR单审核" + userName;
                    append1Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    append1Task.DisPlayName = "PR单审核" + userName;
                    append1Task.RoleName = append1Ids[i];

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = append1Task.DisPlayName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);

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
                    ZHgeneralTask.TaskName = "PR单审核" + userName;
                    ZHgeneralTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHgeneralTask.DisPlayName = "PR单审核" + userName;
                    ZHgeneralTask.RoleName = ZHgeneralIds[i];

                    //if (i == 0)
                    //{
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = ZHgeneralTask.TaskName;
                    lastTask.Transations.Add(trans);

                    lists.Add(ZHgeneralTask);

                    //}
                    //else
                    //lists.Add(ZHgeneralTask);
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
                    string userName = new ESP.Compatible.Employee(int.Parse(addGeneralIds[i])).Name;
                    ModelTemplate.ModelTask addGeneralTask = new ModelTask();
                    addGeneralTask.TaskName = "PR单审核" + userName;
                    addGeneralTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    addGeneralTask.DisPlayName = "PR单审核" + userName;
                    addGeneralTask.RoleName = addGeneralIds[i];

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = addGeneralTask.DisPlayName;
                    lastTask.Transations.Add(trans);

                    lists.Add(lastTask);
                    lastTask = addGeneralTask;
                }
            }
        }

        if (hidCEO.Value != "")
        {
            //CEO上附加审核人
            if (hidAppend2.Value != "")
            {
                string[] append2Ids = hidAppend2.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < append2Ids.Length; i++)
                {
                    if (append2Ids[i].Trim() != "")
                    {
                        string userName = new ESP.Compatible.Employee(int.Parse(append2Ids[i])).Name;
                        ModelTemplate.ModelTask append2Task = new ModelTask();
                        append2Task.TaskName = "PR单审核" + userName;
                        append2Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append2Task.DisPlayName = "PR单审核" + userName;
                        append2Task.RoleName = append2Ids[i];

                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = append2Task.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = append2Task;
                    }
                }
            }


            //CEO
            string CEOName = new ESP.Compatible.Employee(int.Parse(hidCEO.Value)).Name;
            ModelTemplate.ModelTask CEOTask = new ModelTask();
            CEOTask.TaskName = "PR审核" + CEOName;
            CEOTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
            CEOTask.DisPlayName = "PR审核" + CEOName;
            CEOTask.RoleName = hidCEO.Value;

            if (lastTask != null)
            {
                ModelTemplate.Transition trans = new Transition();
                trans.TransitionName = lastTask.DisPlayName;
                trans.TransitionTo = CEOTask.DisPlayName;
                lastTask.Transations.Add(trans);
                lists.Add(lastTask);

                lastTask = CEOTask;
            }
        }

        ModelTemplate.Transition endTrans = new Transition();
        endTrans.TransitionName = lastTask.DisPlayName;
        endTrans.TransitionTo = "end";
        lastTask.Transations.Add(endTrans);
        lists.Add(lastTask);

        ret = manager.ImportData("PR单业务审核(" + generalInfo.PrNo + ")", "PR单业务审核(" + generalInfo.PrNo + ")", "1.0", generalInfo.requestorname, lists);
        return ret;
    }
}
