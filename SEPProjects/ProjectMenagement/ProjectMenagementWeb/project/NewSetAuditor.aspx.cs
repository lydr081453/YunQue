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
using ESP.Finance.Entity;
using System.Data.SqlClient;

public partial class project_NewSetAuditor : ESP.Web.UI.PageBase
{
    int projectid = 0;
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    ModelProcess mp;//模板工作流的实例
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.ProcessInstanceDao PIDao;//工作流数据访问对象
    PROCESSINSTANCES pi;//一个工作流实例
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
    protected ESP.Finance.Entity.ProjectInfo ProjectModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectid = int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
            if (!IsPostBack)
            {
                ViewContorl(ProjectModel);
            }
        }
    }

    private bool CheckDuplicate(IList<ESP.Finance.Entity.AuditHistoryInfo> histList, int UserID)
    {
        bool retValue = false;
        if (histList != null && histList.Count > 0)
        {
            foreach (ESP.Finance.Entity.AuditHistoryInfo operation in histList)
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
    private void ViewContorl(ESP.Finance.Entity.ProjectInfo model)
    {
        //bool isHaveZJ = false;
        //bool isHaveZJL = false;
        // bool isHaveFA = false;
        int serNum = 1;
        string removeTypes = "";

        projectid = int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
        //int[] depts = new ESP.Compatible.Employee(ProjectModel.ApplicantUserID).GetDepartmentIDs();
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(ProjectModel.ApplicantUserID);
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(ProjectModel.GroupID.Value);

        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Contract + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2 + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3 + ",";
        if (manageModel.FAId == 0)
        {
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";
        }
        if (model.TotalAmount.Value <= 500000 && model.ContractStatusName != ProjectType.BDProject)
        {
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLZH + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ + ",";
        }

        if ((int.Parse(CurrentUser.SysID) != ProjectModel.ApplicantUserID) && (ProjectModel.CreatorID != ProjectModel.ApplicantUserID))
        {
            System.Web.UI.HtmlControls.HtmlTableRow FARow = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableCell FACell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell FACell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell FACell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell FACell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell FACell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell FACell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
            FACell1.Align = "Center";
            FACell2.Align = "Center";
            FACell3.Align = "Center";
            FACell4.Align = "Center";
            FACell5.Align = "Center";
            FACell6.Align = "Center";
            FACell1.InnerHtml = serNum.ToString();
            FACell2.InnerHtml = ProjectModel.ApplicantEmployeeName;
            FACell3.InnerHtml = new ESP.Compatible.Employee(ProjectModel.ApplicantUserID).PositionDescription;
            FACell4.InnerHtml = "&nbsp;";
            FACell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_YS];
            FACell6.InnerHtml = "&nbsp;";

            FARow.Attributes["id"] = "tr_" + ProjectModel.ApplicantUserID + "_YS";

            hidYS.Value += ProjectModel.ApplicantUserID.ToString() + ",";
            FARow.Cells.Add(FACell1);
            FARow.Cells.Add(FACell2);
            FARow.Cells.Add(FACell3);
            FARow.Cells.Add(FACell4);
            FARow.Cells.Add(FACell5);
            FARow.Cells.Add(FACell6);
            FARow.Attributes["class"] = "td";
            tab.Rows.Add(FARow);
            serNum++;
        }

        if (manageModel != null)
        {
            if (ProjectModel.ApplicantUserID != manageModel.DirectorId)
            {
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

                ZJCell1.InnerHtml = serNum.ToString();
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
                serNum++;
            }
            ESP.Compatible.Employee empManager = new ESP.Compatible.Employee(manageModel.ManagerId);
            if (manageModel.DirectorId == manageModel.ManagerId)
                empManager = new ESP.Compatible.Employee(manageModel.CEOId);

            System.Web.UI.HtmlControls.HtmlTableRow ZJLRow = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableCell ZJLCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell ZJLCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell ZJLCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell ZJLCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell ZJLCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell ZJLCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
            ZJLCell1.Align = "Center";
            ZJLCell2.Align = "Center";
            ZJLCell3.Align = "Center";
            ZJLCell4.Align = "Center";
            ZJLCell5.Align = "Center";
            ZJLCell6.Align = "Center";

            ZJLCell1.InnerHtml = serNum.ToString();
            ZJLCell2.InnerHtml = empManager.Name;
            ZJLCell3.InnerHtml = empManager.PositionDescription;

            string trId1 = ZJLRow.Attributes["id"] = "tr_" + manageModel.ManagerId + "_ZJLSP";
            if (empManager.IntID.ToString() == ESP.Configuration.ConfigurationManager.SafeAppSettings["DavidZhangID"].Trim())
            {
                ZJLCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00") + "以上";
                ZJLCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
            }
            else
            {
                ZJLCell4.InnerHtml = manageModel.ManagerAmount.ToString("#,##0.00");
                ZJLCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP];
            }
            ZJLCell6.InnerHtml = "";

            hidZJLSP.Value = empManager.IntID.ToString();
            ZJLRow.Cells.Add(ZJLCell1);
            ZJLRow.Cells.Add(ZJLCell2);
            ZJLRow.Cells.Add(ZJLCell3);
            ZJLRow.Cells.Add(ZJLCell4);
            ZJLRow.Cells.Add(ZJLCell5);
            ZJLRow.Cells.Add(ZJLCell6);
            ZJLRow.Attributes["class"] = "td";
            tab.Rows.Add(ZJLRow);
            serNum++;

            if (manageModel.ManagerId != manageModel.CEOId && manageModel.CEOId!=manageModel.RiskControlAccounter)
            {
                ESP.Compatible.Employee empCEO = new ESP.Compatible.Employee(manageModel.CEOId);

                System.Web.UI.HtmlControls.HtmlTableRow CEORow = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell CEOCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell CEOCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell CEOCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell CEOCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell CEOCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell CEOCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                CEOCell1.Align = "Center";
                CEOCell2.Align = "Center";
                CEOCell3.Align = "Center";
                CEOCell4.Align = "Center";
                CEOCell5.Align = "Center";
                CEOCell6.Align = "Center";

                CEOCell1.InnerHtml = serNum.ToString();
                CEOCell2.InnerHtml = manageModel.CEOName;
                CEOCell3.InnerHtml = empCEO.PositionDescription;

                string trIdceo = CEORow.Attributes["id"] = "tr_" + manageModel.CEOId + "_CEOSP";
                CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00") + "以上";
                CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];

                CEOCell6.InnerHtml = "";

                hidCEO.Value = manageModel.CEOId.ToString();
                CEORow.Cells.Add(CEOCell1);
                CEORow.Cells.Add(CEOCell2);
                CEORow.Cells.Add(CEOCell3);
                CEORow.Cells.Add(CEOCell4);
                CEORow.Cells.Add(CEOCell5);
                CEORow.Cells.Add(CEOCell6);
                CEORow.Attributes["class"] = "td";
                tab.Rows.Add(CEORow);
                serNum++;
            }
        }
    }


    protected void btnCommit_Click(object sender, EventArgs e)
    {
        ///////begin validation/////////////

        decimal totalPercent = 0;
        decimal totalFee = 0;
        projectid = int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
        List<SqlParameter> paramlist = new List<SqlParameter>();

        if (ProjectModel.ContractStatusName != ProjectType.BDProject && ProjectModel.CustomerCode.ToLower() != ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())
        {
            IList<PaymentInfo> listPayment = ESP.Finance.BusinessLogic.PaymentManager.GetList(" ProjectID = " + int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            decimal totalPay = 0;

            foreach (PaymentInfo payment in listPayment)
            {
                totalPay += Convert.ToDecimal(payment.PaymentBudget);
            }
            if (!ProjectModel.isRecharge)
            {
                if (Convert.ToDecimal(ProjectModel.TotalAmount) != totalPay)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款通知总额与项目合同总金额不符');", true);
                    return;
                }
            }
            else
            {
                if (Convert.ToDecimal(ProjectModel.AccountsReceivable) != totalPay)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款通知总额与应收金额不符');", true);
                    return;
                }
            }

        }

        if (ProjectModel.ContractStatusName != ProjectType.BDProject)
        {
            string condition = " projectID =@projectID AND usable=1";
            paramlist.Clear();
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@projectID", ProjectModel.ProjectId.ToString()));

            IList<ContractInfo> listcontract = ESP.Finance.BusinessLogic.ContractManager.GetList(condition, paramlist);
            if (listcontract == null || listcontract.Count == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请完整添加合同信息!');", true);
                return;
            }
        }

        foreach (ProjectScheduleInfo model in ProjectModel.ProjectSchedules)
        {
            totalPercent += model.MonthPercent.Value;
            totalFee += model.Fee == null ? 0 : model.Fee.Value;
        }

        decimal servicefee = 0;
        if (ProjectModel.IsCalculateByVAT == 1)
            servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(ProjectModel, null);
        else
            servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(ProjectModel, null);

        if (totalFee != servicefee)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计完工百分比输入有误.');", true);
            return;
        }

        //////////end validation///////////


        //try
        //{
        int processid = createTemplateProcess(ProjectModel, "commit");
        if (processid > 0)
        {

            //创建工作流实例
            ProjectModel.Status = (int)ESP.Finance.Utility.Status.Submit;
            mp = manager.loadProcessModelByID(processid);
            context = new Hashtable();
            //创建一个发起者，实际应用时就是单据的创建者
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = ProjectModel.ApplicantUserID;
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

            ProjectModel.WorkItemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
            ProjectModel.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
            ProjectModel.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
            ProjectModel.ProcessID = processid;
            //complete start任务
            np.load_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME);
            ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(Purchase_Requisition_SetOperationAudit_TaskCompleteEvent);
            np.complete_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());

            ESP.Finance.BusinessLogic.ProjectManager.Update(ProjectModel);
            ESP.Finance.BusinessLogic.ProjectManager.UpdateWorkFlow(ProjectModel.ProjectId, ProjectModel.WorkItemID.Value, ProjectModel.WorkItemName, ProjectModel.ProcessID.Value, ProjectModel.InstanceID.Value);

            string exMail = string.Empty;
            int firstAuditorId = 0;
            if (ProjectModel.CreatorID != ProjectModel.ApplicantUserID)
            {
                firstAuditorId = ProjectModel.ApplicantUserID;
            }
            else if (hidYS.Value != "")
            {
                firstAuditorId = int.Parse(hidYS.Value.Split(',')[0]);
            }
            else if (hidFA.Value != "")
            {
                firstAuditorId = int.Parse(hidFA.Value.Split(',')[0]);
            }
            else if (hidZJSP.Value != "")
            {
                string[] majordomoIds = hidZJSP.Value.TrimEnd(',').Split(',');
                firstAuditorId = int.Parse(majordomoIds[0]);

            }
            else if (hidZJLSP.Value != "")
            {
                firstAuditorId = int.Parse(hidZJLSP.Value);
            }
            else if (hidCEO.Value != "")
            {
                firstAuditorId = int.Parse(hidCEO.Value);
            }

            string backUrl = string.Empty;

            if (!string.IsNullOrEmpty(Request[RequestName.Operate]))
            {
                backUrl = "/Search/ProjectTabList.aspx";
            }
            else
            {
                backUrl = "/Edit/ProjectTabEdit.aspx";
            }

            try
            {
                SendMailHelper.SendMailPR(ProjectModel, getEmployeeEmailBySysUserId(ProjectModel.CreatorID), getEmployeeEmailBySysUserId(Convert.ToInt32(ProjectModel.ApplicantUserID)), getEmployeeEmailBySysUserId(firstAuditorId));
            }
            catch (Exception ex)
            {
                exMail += ex.Message;
            }
            string script = string.Empty;
            if (exMail != string.Empty)
            {
                exMail = "(邮件发送失败!)";
            }
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.Operate]))
            {

                script = string.Format("window.location.href='{0}';alert('项目号变更成功！在第一级审核前，您可以在已提交列表撤销该次变更，重新编辑该项目号。" + exMail + "');", backUrl);
            }
            else
            {
                script = string.Format("window.location.href='{0}';alert('已成功设置项目号申请业务审批人并提交成功！" + exMail + "');", backUrl);
            }
            Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
        }
        else
        {
            throw new Exception();
        }

    }

    private string getEmployeeEmailBySysUserId(int SysUserId)
    {
        return new ESP.Compatible.Employee(SysUserId).EMail;
    }

    void Purchase_Requisition_SetOperationAudit_TaskCompleteEvent(Hashtable context)
    {
        workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(ProjectModel));

    }


    private int createTemplateProcess(ESP.Finance.Entity.ProjectInfo model, string flag)
    {
        int ret;
        string checkAuditor = ",";
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(model.BranchID.Value);
        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, model.GroupID.Value);
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupID.Value);

        List<ESP.Finance.Entity.AuditHistoryInfo> auditList = new List<ESP.Finance.Entity.AuditHistoryInfo>();
        ModelTemplate.BLL.ModelManager manager = new ModelManager();
        List<ModelTemplate.ModelTask> lists = new List<ModelTask>();
        ModelTemplate.ModelTask task = new ModelTask();
        task.TaskName = "start";
        task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
        task.DisPlayName = "start";
        task.RoleName = model.ApplicantUserID.ToString();

        ModelTemplate.ModelTask lastTask = null;

        //预审人
        if (hidYS.Value != "")
        {
            string[] prejudicationIds = hidYS.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < prejudicationIds.Length; i++)
            {
                if (prejudicationIds[i].Trim() != "")
                {
                    if (checkAuditor.IndexOf("," + prejudicationIds[i].Trim() + ",") < 0)
                    {
                        checkAuditor += prejudicationIds[i].Trim() + ",";
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(prejudicationIds[i]));
                        ModelTemplate.ModelTask prejudicationTask = new ModelTask();
                        prejudicationTask.TaskName = "项目号申请单预审业务审核" + emp.Name + prejudicationIds[i];
                        prejudicationTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        prejudicationTask.DisPlayName = "项目号申请单预审业务审核" + emp.Name + prejudicationIds[i];
                        prejudicationTask.RoleName = prejudicationIds[i];

                        ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
                        responser.AuditorEmployeeName = emp.Name;
                        responser.AuditorUserCode = emp.ID;
                        responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                        responser.AuditorUserName = emp.ITCode;
                        responser.ProjectID = model.ProjectId;
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
                    else
                    {
                        continue;
                    }
                }
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
                    ZHMajordomoTask.TaskName = "项目号申请单业务审核总监知会" + emp.Name + ZH1Ids[i];
                    ZHMajordomoTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHMajordomoTask.DisPlayName = "项目号申请单业务审核总监知会" + emp.Name + ZH1Ids[i];
                    ZHMajordomoTask.RoleName = ZH1Ids[i];
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
            if (checkAuditor.IndexOf("," + hidZJSP.Value.TrimEnd(',') + ",") < 0)
            {
                checkAuditor += hidZJSP.Value.TrimEnd(',') + ",";
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidZJSP.Value.TrimEnd(',')));
                ModelTemplate.ModelTask addMajordomoTask = new ModelTask();
                addMajordomoTask.TaskName = "项目号申请单总监业务审核" + emp.Name + hidZJSP.Value.TrimEnd(',');
                addMajordomoTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addMajordomoTask.DisPlayName = "项目号申请单总监业务审核" + emp.Name + hidZJSP.Value.TrimEnd(',');
                addMajordomoTask.RoleName = hidZJSP.Value.TrimEnd(',');

                ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
                responser.AuditorEmployeeName = emp.Name;
                responser.AuditorUserCode = emp.ID;
                responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                responser.AuditorUserName = emp.ITCode;
                responser.ProjectID = model.ProjectId;
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
                    if (checkAuditor.IndexOf("," + fj1Ids[i].Trim() + ",") < 0)
                    {
                        checkAuditor += fj1Ids[i].Trim() + ",";
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(fj1Ids[i]));
                        ModelTemplate.ModelTask append1Task = new ModelTask();
                        append1Task.TaskName = "项目号申请单总监附加业务审核" + emp.Name + fj1Ids[i];
                        append1Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append1Task.DisPlayName = "项目号申请单总监附加业务审核" + emp.Name + fj1Ids[i];
                        append1Task.RoleName = emp.SysID;

                        ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
                        responser.AuditorEmployeeName = emp.Name;
                        responser.AuditorUserCode = emp.ID;
                        responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                        responser.AuditorUserName = emp.ITCode;
                        responser.ProjectID = model.ProjectId;
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
                    ZHgeneralTask.TaskName = "项目号申请单业务审核总经理知会" + emp.Name + zh2Ids[i];
                    ZHgeneralTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHgeneralTask.DisPlayName = "项目号申请单业务审核总经理知会" + emp.Name + zh2Ids[i];
                    ZHgeneralTask.RoleName = zh2Ids[i];
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
            if (checkAuditor.IndexOf("," + hidZJLSP.Value.TrimEnd(',') + ",") < 0)
            {
                checkAuditor += hidZJLSP.Value.TrimEnd(',') + ",";
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidZJLSP.Value.TrimEnd(',')));
                ModelTemplate.ModelTask addGeneralTask = new ModelTask();
                addGeneralTask.TaskName = "项目号申请单总经理业务审核" + emp.Name + hidZJLSP.Value.TrimEnd(',');
                addGeneralTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addGeneralTask.DisPlayName = "项目号申请单总经理业务审核" + emp.Name + hidZJLSP.Value.TrimEnd(',');
                addGeneralTask.RoleName = hidZJLSP.Value.TrimEnd(',');


                ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
                responser.AuditorEmployeeName = emp.Name;
                responser.AuditorUserCode = emp.ID;
                responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                responser.AuditorUserName = emp.ITCode;
                responser.ProjectID = model.ProjectId;
                responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                responser.SquenceLevel = auditList.Count + 1;
                auditList.Add(responser);



                if (lastTask == null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
                    trans.TransitionTo = addGeneralTask.TaskName;
                    task.Transations.Add(trans);
                    lists.Add(task);
                }
                else
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = addGeneralTask.TaskName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);
                }
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
                    if (checkAuditor.IndexOf("," + fj2Ids[i].Trim() + ",") < 0)
                    {
                        checkAuditor += fj2Ids[i].Trim() + ",";
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(fj2Ids[i]));
                        ModelTemplate.ModelTask append2Task = new ModelTask();
                        append2Task.TaskName = "项目号申请单总经理附加业务审核" + emp.Name + fj2Ids[i];
                        append2Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append2Task.DisPlayName = "项目号申请单总经理附加业务审核" + emp.Name + fj2Ids[i];
                        append2Task.RoleName = emp.SysID;
                        ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
                        responser.AuditorEmployeeName = emp.Name;
                        responser.AuditorUserCode = emp.ID;
                        responser.AuditorUserID = Convert.ToInt32(emp.SysID);
                        responser.AuditorUserName = emp.ITCode;
                        responser.ProjectID = model.ProjectId;
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

        if (hidCEO.Value != "")
        {
            if (checkAuditor.IndexOf("," + hidCEO.Value.TrimEnd(',') + ",") < 0)
            {
                int ceoid = int.Parse(hidCEO.Value.TrimEnd(','));
                ESP.Compatible.Employee ceoemp = new ESP.Compatible.Employee(ceoid);

                if (checkAuditor.IndexOf("," + ceoemp.SysID + ",") < 0)
                {
                    checkAuditor += ceoemp.SysID + ",";
                    ModelTemplate.ModelTask append3Task = new ModelTask();
                    append3Task.TaskName = "项目号申请单CEO业务审核" + ceoemp.Name + ceoemp.SysID;
                    append3Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    append3Task.DisPlayName = "项目号申请单CEO业务审核" + ceoemp.Name + ceoemp.SysID;
                    append3Task.RoleName = ceoemp.SysID;
                    ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
                    responser.AuditorEmployeeName = ceoemp.Name;
                    responser.AuditorUserCode = ceoemp.ID;
                    responser.AuditorUserID = Convert.ToInt32(ceoemp.SysID);
                    responser.AuditorUserName = ceoemp.ITCode;
                    responser.ProjectID = model.ProjectId;
                    responser.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;
                    responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                    responser.SquenceLevel = auditList.Count + 1;
                    auditList.Add(responser);

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = append3Task.DisPlayName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);

                    lastTask = append3Task;
                }
            }
        }
        // }

        ModelTemplate.Transition endTrans = new Transition();
        endTrans.TransitionName = lastTask.DisPlayName;
        endTrans.TransitionTo = "end";
        lastTask.Transations.Add(endTrans);
        lists.Add(lastTask);

        //风控人员设置
        if (string.IsNullOrEmpty(model.ProjectCode) && manageModel.RiskControlAccounter > 0)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(manageModel.RiskControlAccounter);
            //if (checkAuditor.IndexOf("," + emp.SysID + ",") < 0)
            //{
            checkAuditor += emp.SysID + ",";
            ESP.Finance.Entity.AuditHistoryInfo contract = new ESP.Finance.Entity.AuditHistoryInfo();
            contract.AuditorEmployeeName = emp.Name;
            contract.AuditorUserCode = emp.ID;
            contract.AuditorUserID = Convert.ToInt32(emp.SysID);
            contract.AuditorUserName = emp.ITCode;
            contract.ProjectID = model.ProjectId;
            contract.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_RiskControl;
            contract.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
            contract.SquenceLevel = auditList.Count + 1;
            auditList.Add(contract);
            // }
        }

        //财务人员设置

        IList<ESP.Finance.Entity.CustomerAuditorInfo> cuslist = ESP.Finance.BusinessLogic.CustomerAudtiorManager.GetList(" branchid=" + branchModel.BranchID.ToString() + " and customerCode='" + model.Customer.ShortEN + "'");
        IList<ESP.Finance.Entity.ContractAuditLogInfo> calist = ESP.Finance.BusinessLogic.ContractAuditLogManager.GetList(" ProjectId=" + model.ProjectId.ToString());
        //有合同并且合同审批人没有点过“等待合同”
        if ((model.ContractStatusID.Value == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.CAStatus)
            || model.ContractStatusID.Value == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.FCAStatus))
            && (calist == null || calist.Count == 0) && branchModel.ContractAccounter > 0)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(branchModel.ContractAccounter);
            if (checkAuditor.IndexOf("," + emp.SysID + ",") < 0)
            {
                checkAuditor += emp.SysID + ",";
                ESP.Finance.Entity.AuditHistoryInfo contract = new ESP.Finance.Entity.AuditHistoryInfo();
                contract.AuditorEmployeeName = emp.Name;
                contract.AuditorUserCode = emp.ID;
                contract.AuditorUserID = Convert.ToInt32(emp.SysID);
                contract.AuditorUserName = emp.ITCode;
                contract.ProjectID = model.ProjectId;
                contract.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Contract;
                contract.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                contract.SquenceLevel = auditList.Count + 1;
                auditList.Add(contract);
            }
        }

        if (model.TotalAmount <= Convert.ToDecimal(ESP.Finance.Configuration.ConfigurationManager.FinancialAmount))
        {
            ESP.Compatible.Employee emp = null;
            ESP.Finance.Entity.AuditHistoryInfo contract = new ESP.Finance.Entity.AuditHistoryInfo();
            if (cuslist != null && cuslist.Count > 0)
                emp = new ESP.Compatible.Employee(cuslist[0].ProjectAuditor);
            else
            {
                if (branchProject == null)
                {
                    emp = new ESP.Compatible.Employee(branchModel.ProjectAccounter);
                }
                else
                {
                    emp = new ESP.Compatible.Employee(branchProject.AuditorID);
                }
            }
            contract.AuditorEmployeeName = emp.Name;
            contract.AuditorUserCode = emp.ID;
            contract.AuditorUserID = Convert.ToInt32(emp.SysID);
            contract.AuditorUserName = emp.ITCode;
            contract.ProjectID = model.ProjectId;
            contract.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            contract.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
            contract.SquenceLevel = auditList.Count + 1;
            auditList.Add(contract);
        }
        else
        {

            //新增加功能，分公司/客户对应财务人员的判断
            ESP.Finance.Entity.AuditHistoryInfo contract2 = new ESP.Finance.Entity.AuditHistoryInfo();
            ESP.Compatible.Employee emp = null;
            if (cuslist != null && cuslist.Count > 0)
                emp = new ESP.Compatible.Employee(cuslist[0].ProjectAuditor);
            else
            {
                if (branchProject == null)
                {
                    emp = new ESP.Compatible.Employee(branchModel.ProjectAccounter);
                }
                else
                {
                    emp = new ESP.Compatible.Employee(branchProject.AuditorID);
                }
            }
            //emp = new ESP.Compatible.Employee(branchModel.ProjectAccounter);
            contract2.AuditorEmployeeName = emp.Name;
            contract2.AuditorUserCode = emp.ID;
            contract2.AuditorUserID = Convert.ToInt32(emp.SysID);
            contract2.AuditorUserName = emp.ITCode;
            contract2.ProjectID = model.ProjectId;
            contract2.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            contract2.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
            contract2.SquenceLevel = auditList.Count + 1;
            auditList.Add(contract2);

        }
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
            ret = manager.ImportData("项目号申请单业务审核(" + model.SerialCode + ")", "项目号申请单业务审核(" + model.SerialCode + ")", "1.0", model.AuditorEmployeeName, lists);

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
        if (!string.IsNullOrEmpty(Request[RequestName.Operate]))
        {
            Response.Redirect("ProjectAuditedModify.aspx" + query);
        }
        else
        {
            Response.Redirect("ProjectStep21.aspx" + query);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        projectid = int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);

        if (createTemplateProcess(ProjectModel, "save") > 0)
        {
            string query = string.Empty;
            query = Request.Url.Query;
            // Response.Redirect(Request[ESP.Finance.Utility.RequestName.BackUrl] + query);
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存业务审批人成功！');", true);
        }
        else
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存业务审批人失败！');", true);
    }

    /// <summary>
    /// 保存业务审批人
    /// </summary>
    /// <returns></returns>
    private bool SaveOperationAuditor(List<ESP.Finance.Entity.AuditHistoryInfo> auditList)
    {
        return (ESP.Finance.BusinessLogic.AuditHistoryManager.Add(auditList) > 0);
    }
}
