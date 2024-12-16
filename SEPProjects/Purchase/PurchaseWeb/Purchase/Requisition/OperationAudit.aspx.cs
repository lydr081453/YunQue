using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;
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

public partial class Purchase_Requisition_OperationAudit : ESP.Purchase.WebPage.ViewPageForPR
{
    private int generalid = 0;
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    GeneralInfo generalInfo;
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Request.QueryString[RequestName.GeneralID]))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择基础数据！');window.location='frmAddGeneral.aspx'", true);
        }
        else
        {
            Session[RequestName.GeneralID] = Request.QueryString[RequestName.GeneralID];
            generalid = int.Parse(Request[RequestName.GeneralID]);
            productInfo.CurrentUserId = CurrentUserID;

        }
        //审核检查
        if (!checkAudit())
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }

        if (!IsPostBack)
        {
            //productInfo.ItemListBind(" general_id = " + Session[RequestName.GeneralID]);
            BindInfo();
        }
    }

    protected void btnTip_Click(object sender, EventArgs e)
    {
        int gid = int.Parse(Session[RequestName.GeneralID].ToString());
        generalInfo = GeneralInfoManager.GetModel(gid);
        var log = ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.Message, generalInfo.PrNo, generalInfo.id, txtRemark.Text, CurrentUser, UserID);

        int ret = AuditLogManager.Add(log, Request);

        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('留言保存成功！');", true);

    }


    public void BindInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(int.Parse(Session[RequestName.GeneralID].ToString()));

        if (null != g)
        {
            GenericInfo.Model = g;
            GenericInfo.BindInfo();
            projectInfo.Model = g;
            projectInfo.BindInfo();
            supplierInfo.Model = g;
            supplierInfo.BindInfo();
            RequirementDescInfo.BindInfo(g);
            productInfo.Model = g;
            
            productInfo.BindInfo();
            if (g.Project_id != 0)
            {
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(g.Project_id);
                productInfo.ValidProfit(projectModel);
            }
            paymentInfo.Model = g;
            paymentInfo.BindInfo();
            paymentInfo.TotalPrice = g.totalprice;
            lablasttime.Text = g.lasttime.ToString();
            labrequisition_committime.Text = g.requisition_committime.ToString() == State.datetime_minvalue ? "" : g.requisition_committime.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private bool checkAudit()
    {
        bool isHave = false;
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PR单");
        List<GeneralInfo> list = new List<GeneralInfo>();
        List<WorkFlowModel.WorkItemData> list2 = null;
        //取得授权审核人的数据 begin
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
        {
            list2 = workitemdata.getProcessDataList(backUp.UserID.ToString(), "PR单");
            foreach (WorkFlowModel.WorkItemData o in list2)
            {
                if (((GeneralInfo)o.ItemData).id == generalid)
                {
                    isHave = true;
                    break;
                }
            }
        }
        //取得授权审核人的数据 end
        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            if (((GeneralInfo)o.ItemData).id == generalid)
            {
                isHave = true;
                break;
            }
        }
        return isHave;
    }

    /// <summary>
    /// 审核通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnYes_Click(object sender, EventArgs e)
    {
        //审核检查
        if (!checkAudit())
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='OperationAuditList.aspx';", true);
            return;
        }
        generalInfo = GeneralInfoManager.GetModel(generalid);

        string Msg = ESP.ITIL.BusinessLogic.申请单业务设置.业务审核通过(CurrentUser, ref generalInfo);
        if (Msg != "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }

        try
        {
            //创建工作流实例
            context = new Hashtable();
            SetWorkItemData(generalInfo);
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = generalInfo.requestor;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR单业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR单业务审核");//提交操作代码：1

            np = processMgr.load_process(generalInfo.ProcessID, generalInfo.InstanceID.ToString(), context);

            //激活start任务
            np.load_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName);
            np.get_lastActivity().active();

            generalInfo.WorkitemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
            generalInfo.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
            if (context["AuditUserID"] == null)
                context.Add("AuditUserID", np.get_lastActivity().getWorkItem().RoleID);
            else
                context["AuditUserID"] = np.get_lastActivity().getWorkItem().RoleID;

            generalInfo.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
            generalInfo.ProcessID = generalInfo.ProcessID;
            //complete start任务
            np.load_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName);
            ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(Purchase_Requisition_OperationAudit_TaskCompleteEvent);
            ((MySubject)np).CompleteEvent += new MySubject.CompleteHandler(Purchase_Requisition_OperationAudit_CompleteEvent);
            np.complete_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());
            //所有工作项都已经完成，工作流结束

            //$$$$$ PR单业务审批通过 插入log信息

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='OperationAuditList.aspx';alert('申请单审核成功！');", true);
        }
        catch
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核失败！');", true);
        }
    }

    void Purchase_Requisition_OperationAudit_CompleteEvent(Hashtable context)
    {
    }

    private void SetWorkItemData(GeneralInfo gmodel)
    {
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PR单");

        //取得授权审核人的数据 begin
        List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
        {
            list2 = workitemdata.getProcessDataList(backUp.UserID.ToString(), "PR单");
            foreach (WorkFlowModel.WorkItemData o in list2)
            {
                if (((GeneralInfo)o.ItemData).id == gmodel.id)
                {
                    gmodel.WorkitemID = o.WorkItemID;
                    gmodel.InstanceID = o.InstanceID;
                    gmodel.WorkItemName = o.WorkItemName;
                    gmodel.ProcessID = o.ProcessID;
                    break;
                }
            }
        }
        //取得授权审核人的数据 end

        List<GeneralInfo> list = new List<GeneralInfo>();
        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            GeneralInfo model = (GeneralInfo)o.ItemData;
            if (model.id == gmodel.id)
            {
                gmodel.WorkitemID = o.WorkItemID;
                gmodel.InstanceID = o.InstanceID;
                gmodel.WorkItemName = o.WorkItemName;
                gmodel.ProcessID = o.ProcessID;
                break;
            }
        }
    }
    void Purchase_Requisition_OperationAudit_TaskCompleteEvent(Hashtable context)
    {
        int auditUser = 0;
        try
        {
            if (np.get_lastActivity() != null)//还有后续任务，该工作流尚未结束
            {
                //发信
                int sysId = int.Parse(np.get_lastActivity().getWorkItem().RoleID);
                ESP.Compatible.Employee employee = new ESP.Compatible.Employee(sysId);

                //写操作日志
                if (context["AuditUserID"] != null)
                {
                    auditUser = Convert.ToInt32(context["AuditUserID"]);
                }
                AuditLogManager.Add(ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.Yes, generalInfo.PrNo, generalInfo.id, txtRemark.Text, CurrentUser, auditUser), Request);
                 workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(generalInfo));
                 try
                 {
                     ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPROperaPass(generalInfo, generalInfo.PrNo, CurrentUser.Name, employee.Name, State.getEmployeeEmailBySysUserId(generalInfo.requestor), State.getEmployeeEmailBySysUserId(sysId), 0, true);
                 }
                 catch { }
            }
            else//工作流完全结束
            {


                //写工作流结束的事件

                if (context["AuditUserID"] != null)
                {
                    auditUser = Convert.ToInt32(context["AuditUserID"]);
                }
                string auditremark;
                if (auditUser != 0 && auditUser != Convert.ToInt32(CurrentUser.SysID))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(auditUser);
                    auditremark = txtRemark.Text.Trim().Length > 280 ? txtRemark.Text.Trim().Substring(0, 280) : txtRemark.Text.Trim();
                    auditremark += "[" + CurrentUser.Name + "(" + CurrentUser.ITCode + ")" + "为" + emp.Name + "代理人]";
                }
                else
                    auditremark = this.txtRemark.Text;
                //记操作日志
                GeneralInfoManager.UpdateAndAddLog(generalInfo, null, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(true, generalInfo.PrNo, generalInfo.id, auditremark, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单业务审核通过);
                //插入审核日志
                InsertAuditLog((int)State.operationAudit_status.Yes, auditUser);

                string FiliorAcrName = string.Empty;
                string FiliorAcrEmail = string.Empty;
                //新增待风控审批，状态和角色配置检查
                if (generalInfo.status == ESP.Purchase.Common.State.requisition_RiskControl && generalInfo.RCAuditor != null)
                {
                    FiliorAcrName = generalInfo.RCAuditorName;
                    FiliorAcrEmail = State.getEmployeeEmailBySysUserId(generalInfo.RCAuditor.Value);
                }
                else//按原有流程待物料审批
                {
                    FiliorAcrName = generalInfo.Filiale_Auditor == 0 ? (new ESP.Compatible.Employee(generalInfo.first_assessor)).Name : (new ESP.Compatible.Employee(generalInfo.Filiale_Auditor)).Name;
                    FiliorAcrEmail = generalInfo.Filiale_Auditor == 0 ? State.getEmployeeEmailBySysUserId(generalInfo.first_assessor) : State.getEmployeeEmailBySysUserId(generalInfo.Filiale_Auditor);
                }


                try
                {
                    ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRLastOperaPass(generalInfo, generalInfo.PrNo, CurrentUser.Name, FiliorAcrName, State.getEmployeeEmailBySysUserId(generalInfo.requestor), FiliorAcrEmail, false, true);

                    if (generalInfo.status == (int)State.order_sended)
                    {
                        SendPOMail(generalInfo);
                    }
                }
                catch { }
            }
           
        }
        catch
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核失败！');", true);
        }
    }


    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="generalid"></param>
    protected void SendPOMail(GeneralInfo generalInfo)
    {
        string htmlFilePath = "";
        string url = "";
        string body = "";
        string clause = "";
        string clause2 = "";
        try
        {
            if (generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
            {
                url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/ADOrder.aspx?id=" + generalInfo.id + "&mail=yes";
                body = ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
            }
            else
            {
                url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/OrderPrint.aspx?id=" + generalInfo.id + "&mail=yes";
                body = ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
            }
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));

            htmlFilePath = Server.MapPath("~") + "ExcelTemplate\\" + "订单" + generalInfo.orderid + ".htm";
            clause = Server.MapPath("~") + "ExcelTemplate\\" + branchModel.POTerm;
            clause2 = Server.MapPath("~") + "ExcelTemplate\\" + branchModel.POStandard;

            FileHelper.DeleteFile(htmlFilePath);
            FileHelper.SaveFile(htmlFilePath, body);
            List<OrderInfo> orders = OrderInfoManager.GetListByGeneralId(generalInfo.id);
            Hashtable attFiles = new Hashtable();
            attFiles.Add(branchModel.POTerm, clause);
            attFiles.Add(branchModel.POStandard, clause2);
            attFiles.Add("", htmlFilePath);
            //李彦娥处理产生的新PR单不需要发工作描述
            if ((generalInfo.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && generalInfo.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA) && generalInfo.sow2.Trim() != "")
            {
                attFiles.Add("工作描述" + generalInfo.sow2.Substring(generalInfo.sow2.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + generalInfo.sow2);
            }

            int filecount = 1;
            foreach (OrderInfo model in orders)
            {
                string[] upfiles = model.upfile.TrimEnd('#').Split('#');
                foreach (string upfile in upfiles)
                {
                    if (upfile.Trim() != "")
                    {
                        if (generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
                        {
                        }
                        else
                        {
                            attFiles.Add("采购物品报价" + filecount.ToString() + upfile.Trim().Substring(upfile.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + upfile.Trim());
                        }
                        filecount++;
                    }
                }
            }
            string supplierEmail = generalInfo.supplier_email;

            string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPO(generalInfo, generalInfo.orderid, State.getEmployeeEmailBySysUserId(generalInfo.requestor), supplierEmail, body, attFiles);

        }
        catch (ESP.ConfigCommon.MailException ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.ToString() + "');", true);
            return;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.ToString() + "');", true);
            return;
        }
    }


    /// <summary>
    /// 插入业务审核表
    /// </summary>
    /// <param name="auditStatus">审核状态 0审核驳回 1审核通过</param>
    private void InsertAuditLog(int auditStatus, int audituser)
    {
        OperationAuditLogInfo auditLog = new OperationAuditLogInfo();
        auditLog.auditorId = int.Parse(CurrentUser.SysID);
        auditLog.auditorName = CurrentUser.Name;
        auditLog.auditTime = DateTime.Now;
        auditLog.Gid = generalid;
        auditLog.audtiStatus = auditStatus;
        if (audituser != 0 && audituser != Convert.ToInt32(CurrentUser.SysID))
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(audituser);
            string remark = txtRemark.Text.Trim().Length > 280 ? txtRemark.Text.Trim().Substring(0, 280) : txtRemark.Text.Trim();
            remark += "[" + CurrentUser.Name + "(" + CurrentUser.ITCode + ")" + "为" + emp.Name + "代理人]";
            auditLog.auditRemark = remark;
        }
        else
            auditLog.auditRemark = txtRemark.Text.Trim().Length > 300 ? txtRemark.Text.Trim().Substring(0, 300) : txtRemark.Text.Trim();
        OperationAuditLogManager.Add(auditLog, Request);
    }

    /// <summary>
    /// 审核驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNo_Click(object sender, EventArgs e)
    {
        try
        {
            //创建工作流实例
            context = new Hashtable();
            generalInfo = GeneralInfoManager.GetModel(generalid);
            SetWorkItemData(generalInfo);
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = generalInfo.requestor;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR单业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR单业务审核");//提交操作代码：1

            np = processMgr.load_process(generalInfo.ProcessID, generalInfo.InstanceID.ToString(), context);
            //激活start任务
            np.load_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName);
            np.get_lastActivity().active();
            np.load_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName);
            if (context["AuditUserID"] == null)
                context.Add("AuditUserID", np.get_lastActivity().getWorkItem().RoleID);
            else
                context["AuditUserID"] = np.get_lastActivity().getWorkItem().RoleID;
            ((MySubject)np).TerminateEvent += new MySubject.TerminateHandler(Purchase_Requisition_OperationAudit_TerminateEvent);
            np.terminate();

            //$$$$$ PR单业务审批驳回 插入log信息
#if debug
                System.Diagnostics.Debug.WriteLine("PR单业务审批驳回");
                Trace.Write("PR单业务审批驳回");
#endif
        }
        catch
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核失败！');", true);
        }
    }

    void Purchase_Requisition_OperationAudit_TerminateEvent(Hashtable context)
    {
        try
        {
            //驳回
            //插入审核日志
            int auditUser = 0;
            if (context["AuditUserID"] != null)
            {
                auditUser = Convert.ToInt32(context["AuditUserID"]);
            }
            InsertAuditLog((int)State.operationAudit_status.No, auditUser);

            ESP.ITIL.BusinessLogic.申请单业务设置.业务审核驳回(CurrentUser, ref generalInfo);

            //记操作日志
            GeneralInfoManager.UpdateAndAddLog(generalInfo, null, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.No, generalInfo.PrNo, generalInfo.id, txtRemark.Text, CurrentUser, auditUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单业务审核驳回);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, generalid, "业务审核驳回"), "业务审核");
            string exMail = string.Empty;
            //发信
            try
            {
                ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPROperaPass(generalInfo, generalInfo.PrNo, CurrentUser.Name, "", State.getEmployeeEmailBySysUserId(generalInfo.requestor), "", 0, false);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='OperationAuditList.aspx';alert('申请单驳回成功！"+exMail+"');", true);
        }
        catch
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核失败！');", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"].ToString());
    }
}