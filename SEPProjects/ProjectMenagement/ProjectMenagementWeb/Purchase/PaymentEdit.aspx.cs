using System;
using System.Data;
using System.Data.SqlClient;
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

public partial class Purchase_PaymentEdit : ESP.Finance.WebPage.ViewPageForReturn
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
            returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);

        }
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        ESP.Purchase.Entity.GeneralInfo generalModel = null;

        if (returnModel != null)
        {
            generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            ESP.Purchase.Entity.PaymentPeriodInfo paymentPeriod = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(returnModel.ReturnID);

            if (returnModel.ParentID != null && returnModel.ParentID != 0)
            {
                this.panParent.Visible = true;
                ESP.Finance.Entity.ReturnInfo parentModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnModel.ParentID.Value);
                if (parentModel.PRID != null && parentModel.PRID.Value != 0)
                {
                    this.lblParentPrNo.Text = generalModel.PrNo;
                    this.lblParentPrTotal.Text = generalModel.totalprice.ToString("#,##0.00");
                }
                this.lblParentAmount.Text = parentModel.FactFee.Value.ToString("#,##0.00");
                this.lblParentCode.Text = parentModel.ReturnCode;
            }


            if (generalModel.PRType == 6 && generalModel.HaveInvoice == false && paymentPeriod.TaxTypes != 0)
            {
                double tax = ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(double.Parse(returnModel.PreFee.Value.ToString()), 1);
                lblTaxDesc.Text = "个税金额:" + tax.ToString() + ";     税后支付金额:" + (returnModel.PreFee.Value - decimal.Parse(tax.ToString())).ToString();
            }

            this.lblLog.Text = this.GetAuditLog(generalModel, returnModel);

        }
        hidPrID.Value = returnModel.PRID.ToString();
        hidProjectID.Value = returnModel.ProjectID.ToString();
        lblApplicant.Text = returnModel.RequestEmployeeName;
        lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
        lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");

        lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
        lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
        lblPeriodType.Text = returnModel.PaymentTypeName;
        txtPayRemark.Text = returnModel.ReturnContent;
        txtOtherRemark.Text = returnModel.Remark;
        if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
            lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
        else
            lblPRNo.Text = returnModel.PRNo;
        lblProjectCode.Text = returnModel.ProjectCode;
        lblReturnCode.Text = returnModel.ReturnCode;
        lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);

        this.lblSupplierName.Text = returnModel.SupplierName;
        this.txtSupplierBank.Text = returnModel.SupplierBankName;
        this.txtSupplierAccount.Text = returnModel.SupplierBankAccount;

        //从重汇列表获取供应商信息
        IList<ESP.Finance.Entity.BankCancelInfo> cancelList = ESP.Finance.BusinessLogic.BankCancelManager.GetList(" ReturnID=" + returnModel.ReturnID.ToString() + " and (ordertype is null or ordertype=1 )");
        if (cancelList != null && cancelList.Count > 0)
        {
            this.lblSupplierName.Text = cancelList[cancelList.Count - 1].OldBankAccountName;
            this.txtSupplierBank.Text = cancelList[cancelList.Count - 1].NewBankName;
            this.txtSupplierAccount.Text = cancelList[cancelList.Count - 1].NewBankAccount;
        }
        labDepartment.Text = returnModel.DepartmentName;
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }

    private void SetWorkFlowData(ReturnInfo rmodel)
    {
        int count = 0;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
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
        returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
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
            returnModel.Remark = this.txtOtherRemark.Text.Trim();
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
            string exMail = string.Empty;
            SetAuditHistory(returnModel, 0, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);

            try
            {
                SendMailHelper.SendMailPROperaOverruleFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批驳回成功！" + exMail + "');window.location.href='" + GetBackUrl() + "';", true);

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
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批驳回成功！" + exMail + "');window.location.href='" + GetBackUrl() + "';", true);
    }

    private void SetAuditHistory(ESP.Finance.Entity.ReturnInfo model, int audittype, int auditstatus)
    {
        string term = string.Format(" ReturnID={0}  and AuditeStatus={1}", model.ReturnID, (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing);
        IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term);
        if (auditlist != null && auditlist.Count > 0)
        {
            ESP.Finance.Entity.ReturnAuditHistInfo audit = auditlist[0];
            if (audit.AuditorUserID.Value != Convert.ToInt32(CurrentUser.SysID))
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
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
        SetWorkFlowData(returnModel);
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
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
        returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(returnModel.ProjectCode);
        ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);

        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }

        //创建工作流实例
        context = new Hashtable();

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

        returnModel.WorkItemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
        returnModel.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
        returnModel.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
        returnModel.ProcessID = returnModel.ProcessID.Value;
        //complete start任务
        np.load_task(returnModel.WorkItemID.Value.ToString(), returnModel.WorkItemName);
        ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(Purchase_Requisition_OperationAudit_TaskCompleteEvent);
        ((MySubject)np).CompleteEvent += new MySubject.CompleteHandler(Purchase_Requisition_OperationAudit_CompleteEvent);
        np.complete_task(returnModel.WorkItemID.Value.ToString(), returnModel.WorkItemName, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());
        //所有工作项都已经完成，工作流结束
    }


    protected void btnTip_Click(object sender, EventArgs e)
    {
        returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
        audit.FormID = returnId;
        audit.Suggestion = this.txtRemark.Text;
        audit.AuditDate = DateTime.Now;
        audit.AuditorSysID = int.Parse(CurrentUser.SysID);
        audit.AuditorUserCode = CurrentUser.ID;
        audit.AuditorEmployeeName = CurrentUser.Name;
        audit.AuditorUserName = CurrentUser.ITCode;
        audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
        audit.FormType = (int)ESP.Finance.Utility.FormType.Return;
        int ret = ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('留言保存成功！');", true);

    }


    private ESP.Framework.Entity.DepartmentInfo GetRootDepartmentID(int deptid)
    {
        ESP.Framework.Entity.DepartmentInfo model = null;
        ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptid);
        if (!string.IsNullOrEmpty(departmentInfo.Description))
        {
            model = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(departmentInfo.Description));
        }
        //List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
        //if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
        //{
        //    // 添加当前用户上级部门信息
        //    depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(deptid, depList);
        //    foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
        //    {
        //        if (dm != null && dm.DepartmentLevel == 1)
        //        {
        //            model = dm;
        //        }
        //    }
        //}

        return model;
    }


    /// <summary>
    /// 采购物料类别是否包含北京采购部专属类别
    /// </summary>
    /// <returns></returns>
    private int getAuditor(ESP.Purchase.Entity.GeneralInfo generalModel)
    {
        List<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(generalModel.id);
        ESP.Purchase.Entity.SupplierInfo supplierModel = null;
        ESP.Purchase.Entity.TypeInfo typeInfo = null;
        // ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);
        ESP.Framework.Entity.OperationAuditManageInfo operation = null;

        if (generalModel.Project_id != 0)
        {
            operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(generalModel.Project_id);
        }
        if (operation == null)
            operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor); ;

        if (operation == null)
            operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(generalModel.Departmentid);

        foreach (ESP.Purchase.Entity.OrderInfo orderModel in orderList)
        {
            if (orderModel.supplierId > 0)
            {
                supplierModel = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(orderModel.supplierId);
                typeInfo = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderModel.producttype);
                break;
            }
        }
        if (supplierModel != null)
        {
            if (operation != null && operation.PurchaseAuditorId != 0)
            {
                return operation.PurchaseAuditorId;
            }
            else
            {
                ESP.Framework.Entity.DepartmentInfo deptModel = GetRootDepartmentID(generalModel.Departmentid);

                if (deptModel.DepartmentID == 19)
                {
                    return typeInfo.BJPaymentUserID;
                }
                else if (deptModel.DepartmentID == 17)
                {
                    return typeInfo.SHPaymentUserID;
                }
                else if (deptModel.DepartmentID == 18)
                {
                    return typeInfo.GZPaymentUserID;
                }
                else
                {
                    return typeInfo.BJPaymentUserID;
                }
            }
        }

        return 0;
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

            workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(returnModel));
            updateReturnInfo(nextId, nextName, isLast);
            try
            {
                SendMailHelper.SendMailPRFirstOperaPassFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, employee.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail, employee.EMail, 0, "Biz");
            }
            catch { }
        }
        else//工作流完全结束
        {

            ESP.Compatible.Employee emp = null;
            string operationFlag = "";
            if (!returnModel.NeedPurchaseAudit)
            {
                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
                int FirstFinanceID = branchModel.FirstFinanceID;
                //增加部门判断,N的不同部门对应不同的第一级财务审批人
                ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
                if (branchdept != null)
                    FirstFinanceID = branchdept.FianceFirstAuditorID;
                emp = new ESP.Compatible.Employee(FirstFinanceID);
                operationFlag = "Finance";
            }
            else
            {
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
                emp = new ESP.Compatible.Employee(getAuditor(generalModel));
                operationFlag = "Purchase";
            }
            nextId = int.Parse(emp.SysID);
            nextName = emp.Name;
            isLast = true;
            updateReturnInfo(nextId, nextName, isLast);

            try
            {
                SendMailHelper.SendMailPRFirstOperaPassFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, emp.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail, emp.EMail, 0, operationFlag);
            }
            catch { }
        }

    }

    public void updateReturnInfo(int nextId, string nextName, bool isLast)
    {
        returnModel.SupplierBankName = this.txtSupplierBank.Text.Trim();
        returnModel.SupplierBankAccount = this.txtSupplierAccount.Text.Trim();
        returnModel.PaymentUserID = nextId;
        returnModel.PaymentEmployeeName = nextName;
        returnModel.ReturnContent = this.txtPayRemark.Text;
        returnModel.Remark = this.txtOtherRemark.Text.Trim();
        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
        if (isLast)
        {
            if (!returnModel.NeedPurchaseAudit)
                returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
            else
                returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.PurchaseFirst;
        }
        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

        SetAuditHistory(returnModel, 0, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);

        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批通过成功！');window.location.href='" + GetBackUrl() + "';", true);
    }


    private string GetAuditLog(ESP.Purchase.Entity.GeneralInfo generalModel, ReturnInfo ReturnModel)
    {
        string loginfo = string.Empty;

        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);

        if (generalModel != null && generalModel.ValueLevel == 1)
        {
            //业务审核日志
            IList<ESP.Purchase.Entity.AuditLogInfo> oploglist = ESP.Purchase.BusinessLogic.AuditLogManager.GetModelListByGID(generalModel.id);

            foreach (ESP.Purchase.Entity.AuditLogInfo log in oploglist)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.auditUserId);
                loginfo += log.auditUserName + "(" + emp.FullNameEN + ")" + ESP.Purchase.Common.State.operationAudit_statusName[log.auditType] + " " + log.remark + " " + log.remarkDate + "<br/>";
            }
        }
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
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "/Purchase/ReturnAuditList.aspx" : Request["BackUrl"];
    }
}

