using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

public partial class project_FinancialAuditOperation : ESP.Finance.WebPage.ViewPageForProject
{
    string query = string.Empty;
    int projectid = 0;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    string users2 = string.Empty;
    string castatus = ESP.Finance.Configuration.ConfigurationManager.CAStatus;
    IList<ESP.Finance.Entity.CustomerAuditorInfo> cuslist;
    ESP.Finance.Entity.BranchInfo branchModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(project_FinancialAuditOperation));
        this.ddlBank.Attributes.Add("onchange", "selectBank(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
        hidBranchId.Value = projectinfo.BranchID.ToString();

        cuslist = ESP.Finance.BusinessLogic.CustomerAudtiorManager.GetList(" BranchCode='" + projectinfo.BranchCode + "' and customerCode='" + projectinfo.CustomerCode + "'");
        branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectinfo.BranchID.Value);
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(projectinfo.GroupID.Value);
        ESP.Framework.Entity.AuditBackUpInfo RiskDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(manageModel.RiskControlAccounter);

        query = Request.Url.Query;
        Server.ScriptTimeout = 600;
        users2 = this.GetUser(projectinfo);
        if (int.Parse(CurrentUser.SysID) != branchModel.FinalAccounter)
        {
            btnTerminate2.Visible = false;
        }
        if (int.Parse(CurrentUser.SysID) == manageModel.RiskControlAccounter || (RiskDelegate != null && RiskDelegate.BackupUserID == CurrentUserID))
        {
            btnWaiting.Visible = false;
            btnTerminate2.Visible = false;
        }

        if (!IsPostBack)
        {
            if (!ValidateConfirm())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
                Response.Redirect(GetBackUrl());
            }
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {

                this.lblLog.Text = this.GetAuditLog(projectid);

                if (projectinfo.BankId != 0)
                {
                    ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(projectinfo.BankId);
                    this.hidBankID.Value = bankModel.BankID.ToString();

                    this.lblAccount.Text = bankModel.BankAccount;
                    this.lblAccountName.Text = bankModel.BankAccountName;
                    this.lblBankAddress.Text = bankModel.Address;
                }

                TopMessage.ProjectModel = projectinfo;
                this.PaymentDisplay.ProjectInfo = projectinfo;
                this.PaymentDisplay.InitProjectInfo();
                if (!string.IsNullOrEmpty(projectinfo.ProjectCode))
                {
                    txtProjecCode.Text = projectinfo.ProjectCode;
                }
                txtRelevanceProjectCode.Text = projectinfo.RelevanceProjectCode;
                hidRelevanceProjectId.Value = (projectinfo.RelevanceProjectId ?? 0).ToString();
                IList<ESP.Finance.Entity.ProjectHistInfo> projecthistList = getProjectList(projectinfo.ProjectId);
                ESP.Finance.Entity.ProjectInfo oldProjectModel = null;
                if (projecthistList != null && projecthistList.Count() != 0)
                {
                    oldProjectModel = projecthistList[0].ProjectModel.ObjectDeserialize<ProjectInfo>();
                }
                if (oldProjectModel != null && oldProjectModel.ContractStatusName == ProjectType.BDProject && projectinfo.ContractStatusName != ProjectType.BDProject)
                {
                    this.lblBD.Text = "BD项目转正!";
                }
            }
            if (users2.IndexOf("," + CurrentUserID.ToString() + ",") >= 0)
            {
                this.pnlCode.Style["display"] = "block";
            }
            else
            {
                this.pnlCode.Style["display"] = "none";
            }

            //40%
            //decimal taxfee = 0;
            //decimal servicefee = 0;
            //decimal profilerate = 0;
            //if (projectinfo.ContractTax != null && projectinfo.ContractTaxID != null)
            //{
            //    ESP.Finance.Entity.TaxRateInfo rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectinfo.ContractTaxID.Value);
            //    if (projectinfo.IsCalculateByVAT == 1)
            //    {
            //        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(projectinfo, rateModel);
            //        servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectinfo, rateModel);
            //    }
            //    else
            //    {
            //        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(projectinfo, rateModel);
            //        servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectinfo, rateModel);
            //    }
            //}

            //if (projectinfo.TotalAmount > 0)
            //{
            //    profilerate = (servicefee / Convert.ToDecimal(projectinfo.TotalAmount) * 100);
            //}
            //if (profilerate < 40 && projectinfo.ContractStatusName != ESP.Finance.Utility.ProjectType.BDProject)
            //{
            //    this.lblTip.Text = "项目毛利率为" + profilerate.ToString("#,##0.00") + "%，低于40%，请说明立项原因。";
            //    this.txtReason.Text = projectinfo.ProfileReason;
            //}
            //else
            //{
            //    this.tabReason.Visible = false;
            //}
        }
    }



    private IList<ESP.Finance.Entity.ProjectHistInfo> getProjectList(int ProjectID)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();
        string term = " ProjectID=@ProjectID and Status=@Status";
        System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = ProjectID;
        paramList.Add(p2);
        System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
        paramList.Add(p1);
        IList<ESP.Finance.Entity.ProjectHistInfo> projectList = ESP.Finance.BusinessLogic.ProjectHistManager.GetList(term, paramList);
        var tmplist = projectList.OrderByDescending(N => N.SubmitDate).ToList();
        return tmplist;
    }

    private bool ValidateConfirm()
    {
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" ProjectAccounter=" + CurrentUser.SysID + " or FinalAccounter=" + CurrentUser.SysID);
        string BranchCodes = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> Delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        IList<ESP.Finance.Entity.BranchInfo> DelegateList = null;

        foreach (ESP.Framework.Entity.AuditBackUpInfo model in Delegates)
        {
            DelegateList = ESP.Finance.BusinessLogic.BranchManager.GetList(" ProjectAccounter=" + model.UserID.ToString());
            foreach (ESP.Finance.Entity.BranchInfo bmodel in DelegateList)
            {
                branchList.Add(bmodel);
            }

            var branchProjectList = ESP.Finance.BusinessLogic.BranchProjectManager.GetList(model.UserID);

            foreach (var b in branchProjectList)
            {
                ESP.Finance.Entity.BranchInfo branch = ESP.Finance.BusinessLogic.BranchManager.GetModel(b.BranchId);
                branchList.Add(branch);
            }

        }

        var branchProjectList2 = ESP.Finance.BusinessLogic.BranchProjectManager.GetList(CurrentUserID);

        foreach (var b in branchProjectList2)
        {
            ESP.Finance.Entity.BranchInfo branch = ESP.Finance.BusinessLogic.BranchManager.GetModel(b.BranchId);
            branchList.Add(branch);
        }

        foreach (ESP.Finance.Entity.BranchInfo model in branchList)
        {
            BranchCodes += "'" + model.BranchCode + "',";
        }
        BranchCodes = BranchCodes.TrimEnd(',');


        string finalCounters = ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters();
        string[] finals = finalCounters.Split(',');
        string FinalCounterDelegate = "," + finalCounters;
        for (int i = 0; i < finals.Length; i++)
        {
            if (!string.IsNullOrEmpty(finals[i]))
            {
                ESP.Framework.Entity.AuditBackUpInfo model = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(finals[i]));
                if (model != null)
                    FinalCounterDelegate += "," + model.BackupUserID.ToString();
            }
        }
        FinalCounterDelegate += ",";

        string ContractCounters = ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters();
        string[] Contracts = ContractCounters.Split(',');
        string ContractCounterDelegate = "," + ContractCounters;
        for (int i = 0; i < Contracts.Length; i++)
        {
            if (!string.IsNullOrEmpty(Contracts[i]))
            {
                ESP.Framework.Entity.AuditBackUpInfo model = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(Contracts[i]));
                if (model != null)
                    ContractCounterDelegate += "," + model.BackupUserID.ToString();
            }
        }
        ContractCounterDelegate += ",";

        if (!string.IsNullOrEmpty(BranchCodes) && (projectinfo.Status != (int)Status.FinanceAuditing))//北京上海广州--任媛
        {
            term = " and (Status=@Status1 or status =@Status2) ";
            if (cuslist != null && cuslist.Count > 0)
            {
                term += "and (branchcode in(" + BranchCodes + ") or (branchCode ='" + cuslist[0].BranchCode + "' and customerCode='" + cuslist[0].CustomerCode + "'))";
            }
            else
            {
                term += " and branchcode in(" + BranchCodes + ")";
            }

            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.BizAuditComplete;
            paramlist.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@Status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)ESP.Finance.Utility.Status.ContractAudit;
            paramlist.Add(p2);

        }
        else if (FinalCounterDelegate.IndexOf(CurrentUserID.ToString()) >= 0 && (projectinfo.Status == (int)Status.FinanceAuditing))//eddy or caroline
        {
            term = " and (Status=@Status and totalamount>@totalamount)";
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.FinanceAuditing;
            paramlist.Add(p1);
            System.Data.SqlClient.SqlParameter p0 = new System.Data.SqlClient.SqlParameter("@totalamount", System.Data.SqlDbType.Decimal, 18);
            p0.SqlValue = ESP.Finance.Configuration.ConfigurationManager.FinancialAmount;
            paramlist.Add(p0);

        }
        else if (ContractCounterDelegate.IndexOf(CurrentUserID.ToString()) >= 0)//合同审批人
        {
            string contractIds = "(" + ESP.Finance.Configuration.ConfigurationManager.CAStatus + "," + ESP.Finance.Configuration.ConfigurationManager.FCAStatus + ")";
            term = " and (Status=@Status and ContractStatusID in "+contractIds+" ) or CheckContract=@CheckContract";
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.BizAuditComplete;
            paramlist.Add(p1);
            System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@ContractStatusID", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = ESP.Finance.Configuration.ConfigurationManager.CAStatus;
            paramlist.Add(p3);
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@CheckContract", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)ProjectCheckContract.ContractUpdate;
            paramlist.Add(p2);
        }


        IList<ProjectInfo> listResult = ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
        foreach (ProjectInfo pro in listResult)
        {
            if (pro.ProjectId.ToString() == Request[ESP.Finance.Utility.RequestName.ProjectID])
            {
                return true;
            }
        }
        return false;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (!ValidateConfirm())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        string exMail = string.Empty;

        users2 = GetUser(projectinfo);
        List<ESP.Finance.Entity.TimingLogInfo> changeList = ESP.Finance.BusinessLogic.TimingLogManager.GetList("ProjectID=0 and ','+remark+',' like '%," + projectid.ToString() + ",%'");


        //拆分N的财务审批权
        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectinfo.GroupID.Value);

        int financeAuditor = branchModel.ProjectAccounter;
        if (branchProject != null)
            financeAuditor = branchProject.AuditorID;

        IList<ESP.Finance.Entity.ContractInfo> contractList = ESP.Finance.BusinessLogic.ContractManager.GetList(" projectId=" + projectid.ToString() + " and isdelay=1");
        ESP.Framework.Entity.AuditBackUpInfo ProjectDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(financeAuditor);
        ESP.Framework.Entity.AuditBackUpInfo FinalDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.FinalAccounter);
        ESP.Framework.Entity.AuditBackUpInfo ContractDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.ContractAccounter);
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(projectinfo.GroupID.Value);
        ESP.Framework.Entity.AuditBackUpInfo RiskDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(manageModel.RiskControlAccounter);

        if (!string.IsNullOrEmpty(hidBankID.Value))
        {
            projectinfo.BankId = int.Parse(hidBankID.Value);
        }
        projectinfo.ProfileReason = this.txtReason.Text;

        if ((CurrentUserID == branchModel.ContractAccounter || (ContractDelegate != null && ContractDelegate.BackupUserID == CurrentUserID)) && projectinfo.ContractStatusID == 1 && projectinfo.Status == (int)Status.BizAuditComplete)//合同审批
        {
            if (projectinfo.Status != (int)Status.FinanceAuditComplete && projectinfo.Status != (int)Status.ProjectPreClose && projectinfo.Status != (int)Status.FinanceAuditing)
            {
                ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.ContractAudit);
                SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_Contract, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);
                SetContractAuditLog(projectinfo);

                try
                {
                    SendMailHelper.SendMailContractOK(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(financeAuditor));
                    if (ProjectDelegate != null)
                        SendMailHelper.SendMailContractOK(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(ProjectDelegate.BackupUserID));
                }
                catch (Exception ex)
                {
                    exMail = ex.Message;
                }
            }
        }
        else if (((cuslist != null && cuslist.Count > 0) && (CurrentUserID == branchModel.FinalAccounter || (FinalDelegate != null && FinalDelegate.BackupUserID == CurrentUserID))) || (CurrentUserID == financeAuditor || CurrentUserID == branchModel.FinalAccounter || (users2.IndexOf(CurrentUser.SysID) >= 0)))//任媛戴琼角色的审批
        {
            projectinfo.ProjectCode = this.txtProjecCode.Text.Trim();
            projectinfo.SubmitDate = DateTime.Now;
            if (!string.IsNullOrEmpty(txtRelevanceProjectCode.Text) && !string.IsNullOrEmpty(hidRelevanceProjectId.Value))
            {
                projectinfo.RelevanceProjectId = int.Parse(hidRelevanceProjectId.Value);
                projectinfo.RelevanceProjectCode = txtRelevanceProjectCode.Text.Trim();
            }
            UpdateResult result;

            if ((projectinfo.EndDate.Value < DateTime.Now.AddMonths(-1) && (contractList == null || contractList.Count == 0)))
            {
                result = ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.ProjectPreClose);//审批完成，生成项目号
            }
            else
            {
                result = ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.FinanceAuditComplete);//审批完成，生成项目号
                ESP.Finance.BusinessLogic.SupporterManager.UpdateProjectCode(projectinfo.ProjectId, projectinfo.ProjectCode);
            }
            if (result == UpdateResult.Succeed)
            {

                ESP.Finance.BusinessLogic.PaymentManager.UpdatePaymentBankInfo(projectinfo.ProjectId, projectinfo.BankId);

                SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);

                //充值类项目，向项目媒体关联表插入数据
                if (projectinfo.isRecharge)
                {
                    ProjectMediaInfo media = ESP.Finance.BusinessLogic.ProjectMediaManager.GetList(" projectId=" + projectinfo.ProjectId + " and endDate is null").FirstOrDefault();
                    if (media == null)
                    {
                        ProjectMediaInfo pmInfo = new ProjectMediaInfo();
                        pmInfo.ProjectId = projectinfo.ProjectId;
                        pmInfo.SupplierId = projectinfo.MediaId.Value;
                        pmInfo.Recharge = projectinfo.Recharge.Value;
                        pmInfo.CostRate = projectinfo.MediaCostRate.Value;
                        pmInfo.BeginDate = DateTime.Now;
                        ESP.Finance.BusinessLogic.ProjectMediaManager.Add(pmInfo);
                    }
                    else
                    {
                        media.SupplierId = projectinfo.MediaId.Value;
                        media.Recharge = projectinfo.Recharge.Value;
                        media.CostRate = projectinfo.MediaCostRate.Value;
                        ESP.Finance.BusinessLogic.ProjectMediaManager.Update(media);
                    }
                }

                try
                {
                    //给创建人和负责人发送邮件通知
                    SendMailHelper.SendMailAuditComplete(projectinfo, projectinfo.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)));
                    SendMailHelper.SendMailAuditComplete(projectinfo, projectinfo.CreatorName, getEmployeeEmailBySysUserId(financeAuditor));

                    //财务审批完成，给支持方发送申请通知
                    if (!string.IsNullOrEmpty(projectinfo.ProjectCode))
                    {
                        foreach (ESP.Finance.Entity.SupporterInfo mem in projectinfo.Supporters)
                        {
                            SendMailHelper.SendMailToSupporter(projectinfo, getEmployeeEmailBySysUserId(Convert.ToInt32(mem.LeaderUserID)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    exMail += ex.Message;
                }
            }


            else if (result == UpdateResult.Iterative)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('项目号或项目名称有重复，请重新生成！');", true);
                return;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + result.ToString() + "！');", true);
                return;
            }
        }
        else if (projectinfo.Status == (int)Status.WaitRiskControl && (CurrentUserID == manageModel.RiskControlAccounter || (RiskDelegate != null && RiskDelegate.BackupUserID == CurrentUserID)))
        {
            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.BizAuditComplete);
            SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_RiskControl, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);
            IList<ESP.Finance.Entity.ContractAuditLogInfo> calist = ESP.Finance.BusinessLogic.ContractAuditLogManager.GetList(" ProjectId=" + projectinfo.ProjectId.ToString());
            try
            {
                if ((projectinfo.ContractStatusID == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.CAStatus)
                    || projectinfo.ContractStatusID == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.FCAStatus))
                    && (calist == null || calist.Count == 0) && branchModel.ContractAccounter>0)
                {
                    SendMailHelper.SendMailBizOK(projectinfo, CurrentUser.Name, getEmployeeEmailBySysUserId(Convert.ToInt32(branchModel.ContractAccounter)), "Finance");
                    ESP.Framework.Entity.AuditBackUpInfo ContractUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(branchModel.ContractAccounter));
                    if (ContractUser != null)
                    {
                        SendMailHelper.SendMailBizOK(projectinfo, CurrentUser.Name, getEmployeeEmailBySysUserId(ContractUser.BackupUserID), "Finance");
                    }
                }
                else//否则不需要合同审批，直接发到财务人员
                {
                    int financeAuditer = branchModel.ProjectAccounter;
                    if (branchProject != null)
                        financeAuditer = branchProject.AuditorID;

                    SendMailHelper.SendMailBizOK(projectinfo, CurrentUser.Name, getEmployeeEmailBySysUserId(financeAuditer), "Finance");
                    ESP.Framework.Entity.AuditBackUpInfo FinanceUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(financeAuditer);
                    if (FinanceUser != null)
                    {
                        SendMailHelper.SendMailBizOK(projectinfo, CurrentUser.Name, getEmployeeEmailBySysUserId(FinanceUser.BackupUserID), "Finance");
                    }
                }

            }
            catch (Exception ex)
            {
                exMail = ex.Message;
            }
        }


        if (exMail != string.Empty)
        {
            exMail = "(邮件发送失败!)";
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号财务审核成功！" + exMail + "');", true);
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
    private void SetContractAuditLog(ESP.Finance.Entity.ProjectInfo projectModel)
    {
        ESP.Finance.Entity.ContractAuditLogInfo audit = new ContractAuditLogInfo();
        audit.Contractor = CurrentUser.Name;
        audit.ContractorId = int.Parse(CurrentUser.SysID);
        audit.AuditDate = DateTime.Now;
        audit.AuditDesc = this.txtAuditRemark.Text;
        audit.ProjectId = projectModel.ProjectId;
        ESP.Finance.BusinessLogic.ContractAuditLogManager.Add(audit);
    }
    private void SetAuditHistory(ESP.Finance.Entity.ProjectInfo projectModel, int audittype, int auditstatus)
    {
        string term = string.Format(" projectid={0} and audittype={1} and (auditStatus={2} or auditStatus={3})", projectModel.ProjectId, audittype, (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing, (int)ESP.Finance.Utility.AuditHistoryStatus.WaitingContract);
        IList<ESP.Finance.Entity.AuditHistoryInfo> auditlist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term);
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
            audit.ProjectID = projectinfo.ProjectId;
            audit.AuditStatus = auditstatus;
            audit.AuditDate = DateTime.Now;
            ESP.Finance.BusinessLogic.AuditHistoryManager.Update(audit);
        }
    }

    private void SetAuditHistory2(int pid)
    {
        string term = string.Format(" projectid={0} and audittype={1} and (auditStatus={2})", pid, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);
        IList<ESP.Finance.Entity.AuditHistoryInfo> auditlist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term);
        if (auditlist != null && auditlist.Count > 0)
        {
            ESP.Finance.Entity.AuditHistoryInfo audit = auditlist[0];
            audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            ESP.Finance.BusinessLogic.AuditHistoryManager.Update(audit);
        }
        ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
        log.AuditDate = DateTime.Now;
        log.AuditorEmployeeName = CurrentUser.Name;
        log.AuditorSysID = int.Parse(CurrentUser.SysID);
        log.AuditorUserCode = CurrentUser.ITCode;
        log.AuditorUserName = CurrentUser.ID;
        log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
        log.FormID = pid;
        log.FormType = (int)ESP.Finance.Utility.FormType.Project;
        log.Suggestion = this.txtAuditRemark.Text;
        ESP.Finance.BusinessLogic.AuditLogManager.Add(log);

    }

    private string GetUser(ESP.Finance.Entity.ProjectInfo projectModel)
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        string retuser = user;
        string[] users = user.Split(',');
        for (int i = 0; i < users.Length; i++)
        {
            if (!string.IsNullOrEmpty(users[i]))
            {
                DataTable dt = ESP.Framework.BusinessLogic.AuditBackUpManager.GetList(" BeginDate < GETDATE() and  EndDate > GETDATE() and UserID=" + users[i] + " and status=1 and type=1").Tables[0];
                if (dt != null)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                        retuser += dt.Rows[j]["BackupUserID"].ToString() + ",";
                }
            }
        }
        IList<ESP.Finance.Entity.CustomerAuditorInfo> cuslist = ESP.Finance.BusinessLogic.CustomerAudtiorManager.GetList(" BranchCode='" + projectModel.BranchCode + "' and customerCode='" + projectModel.CustomerCode + "'");
        foreach (ESP.Finance.Entity.CustomerAuditorInfo cus in cuslist)
        {
            retuser += cus.ProjectAuditor + ",";
        }

        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectModel.GroupID.Value);

        if (branchProject != null)
        {
            retuser += branchProject.AuditorID + ",";
        }

        return retuser;
    }
    protected void btnTerminate_Click(object sender, EventArgs e)
    {

        if (!ValidateConfirm())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (projectinfo.Status == (int)Status.FinanceAuditComplete || projectinfo.Status == (int)Status.ProjectPreClose)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        //审批驳回

        //拆分N的财务审批权
        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectinfo.GroupID.Value);

        int financeAuditor = branchModel.ProjectAccounter;
        if (branchProject != null)
            financeAuditor = branchProject.AuditorID;

        ESP.Framework.Entity.AuditBackUpInfo ProjectDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(financeAuditor);
        ESP.Framework.Entity.AuditBackUpInfo FinalDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.FinalAccounter);
        ESP.Framework.Entity.AuditBackUpInfo ContractDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.ContractAccounter);
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(projectinfo.GroupID.Value);
        ESP.Framework.Entity.AuditBackUpInfo RiskDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(manageModel.RiskControlAccounter);

        string exMail = string.Empty;

        if (((cuslist != null && cuslist.Count > 0) || CurrentUserID == financeAuditor) || (ProjectDelegate != null && ProjectDelegate.BackupUserID == CurrentUserID))//任媛戴琼角色的审批
        {

            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.FinanceReject);//驳回
            SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
            //给项目号创建人和负责人发出驳回通知
            try
            {
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.CreatorID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号申请驳回完成！" + exMail + "');", true);

        }

        else if (CurrentUserID == branchModel.FinalAccounter || (FinalDelegate != null && FinalDelegate.BackupUserID == CurrentUserID))//由eddy审批
        {

            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.FinanceReject);//驳回
            SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
            //给项目号创建人和负责人发出驳回通知
            try
            {
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.CreatorID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(financeAuditor));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号申请驳回完成！" + exMail + "');", true);

        }
        else if (CurrentUserID == branchModel.ContractAccounter || (ContractDelegate != null && ContractDelegate.BackupUserID == CurrentUserID))//由contract审批
        {

            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.ContractReject);//驳回
            SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_Contract, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
            //给项目号创建人和负责人发出驳回通知
            try
            {
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.CreatorID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(financeAuditor));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号申请驳回完成！" + exMail + "');", true);

        }
        else if (CurrentUserID == manageModel.RiskControlAccounter || (RiskDelegate != null && RiskDelegate.BackupUserID == CurrentUserID))
        {
            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.BizReject);//驳回
            SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_RiskControl, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
            //给项目号创建人和负责人发出驳回通知
            try
            {
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.CreatorID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(financeAuditor));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号申请驳回完成！" + exMail + "');", true);
        }
        else
        {
            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.BizReject);//驳回
            SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_Contract, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
            //给项目号创建人和负责人发出驳回通知
            try
            {
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.CreatorID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)));
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(financeAuditor));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号申请驳回完成！" + exMail + "');", true);
        }

    }

    protected void btnTerminate2_Click(object sender, EventArgs e)
    {
        if (!ValidateConfirm())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (projectinfo.Status == (int)Status.FinanceAuditComplete || projectinfo.Status == (int)Status.ProjectPreClose)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }

        //拆分N的财务审批权
        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectinfo.GroupID.Value);

        int financeAuditor = branchModel.ProjectAccounter;
        if (branchProject != null)
            financeAuditor = branchProject.AuditorID;

        ESP.Framework.Entity.AuditBackUpInfo ProjectDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(financeAuditor);
        ESP.Framework.Entity.AuditBackUpInfo FinalDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.FinalAccounter);

        if (CurrentUserID == branchModel.FinalAccounter || (FinalDelegate != null && FinalDelegate.BackupUserID == CurrentUserID))//由eddy审批
        {

            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, ESP.Finance.Utility.Status.BizAuditComplete);//驳回
            SetAuditHistory2(projectinfo.ProjectId);
            string exMail = string.Empty;
            //给项目号创建人和负责人发出驳回通知
            try
            {
                SendMailHelper.SendMailAuditFinancialReject(projectinfo, CurrentUserName, getEmployeeEmailBySysUserId(financeAuditor));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('项目号申请驳回完成！" + exMail + "');", true);

        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }

    [AjaxPro.AjaxMethod]
    public static string CreateProjectCode(string id)
    {
        string pcode = ESP.Finance.BusinessLogic.ProjectManager.CreateProjectCode(Convert.ToInt32(id));
        return pcode;
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string pcode = ESP.Finance.BusinessLogic.ProjectManager.CreateProjectCode(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        this.txtProjecCode.Text = pcode;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!ValidateConfirm())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        query = query.AddParam(RequestName.Operate, "FinancialAudit");
        Response.Redirect("ProjectEdit.aspx?" + query);
    }

    protected void btnWaiting_Click(object sender, EventArgs e)
    {
        if (!ValidateConfirm())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }

        //拆分N的财务审批权
        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectinfo.GroupID.Value);

        int financeAuditor = branchModel.ProjectAccounter;
        if (branchProject != null)
            financeAuditor = branchProject.AuditorID;

        users2 = GetUser(projectinfo);
        ESP.Framework.Entity.AuditBackUpInfo ProjectDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(financeAuditor);
        ESP.Framework.Entity.AuditBackUpInfo FinalDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.FinalAccounter);
        ESP.Framework.Entity.AuditBackUpInfo ContractDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.ContractAccounter);

        if (CurrentUserID == branchModel.ContractAccounter || (ContractDelegate != null && ContractDelegate.BackupUserID == CurrentUserID))//合同审批
        {
            //如果是合同更新，项目继续使用，不终止项目
            if ((projectinfo.Status == (int)Status.FinanceAuditComplete || projectinfo.Status == (int)Status.ProjectPreClose) && projectinfo.CheckContract == (int)ESP.Finance.Utility.ProjectCheckContract.ContractUpdate)
            {
                ESP.Finance.BusinessLogic.ProjectManager.ChangeCheckContractStatus(projectinfo.ProjectId, ProjectCheckContract.InitialContract);
            }//否则项目状态为等待合同
            else
            {
                ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, Status.Waiting);
            }
            SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_Contract, (int)ESP.Finance.Utility.AuditHistoryStatus.WaitingContract);
            SetContractAuditLog(projectinfo);

            try
            {
                SendMailHelper.SendMailAuditContract(projectinfo, projectinfo.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)));
                SendMailHelper.SendMailAuditContract(projectinfo, projectinfo.CreatorName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.CreatorID)));
            }
            catch
            {
            }
            Response.Redirect(GetBackUrl());
        }
        else if ((projectinfo.Status == (int)Status.FinanceAuditComplete || projectinfo.Status == (int)Status.ProjectPreClose) && projectinfo.CheckContract == (int)ESP.Finance.Utility.ProjectCheckContract.InitialContract)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
            }

        else if (CurrentUserID == branchModel.FinalAccounter || (FinalDelegate != null && FinalDelegate.BackupUserID == CurrentUserID))//
        {
            if (projectinfo.ContractStatusID == int.Parse(ESP.Finance.Configuration.ConfigurationManager.FCAStatus))
            {
                //如果合同状态为框架合同，将客户框架协议改为可用状态
                foreach (var attchId in projectinfo.CustomerAttachID.Trim(',').Split(','))
                {
                    ESP.Finance.Entity.CustomerAttachInfo attachInfo = ESP.Finance.BusinessLogic.CustomerAttachManager.GetModel(int.Parse(attchId));
                    attachInfo.Status = Common.CustomerAttachStatus.Used;
                    ESP.Finance.BusinessLogic.CustomerAttachManager.Update(attachInfo);
                }
            }

            ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectinfo, Status.Waiting);
            SetAuditHistory(projectinfo, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial, (int)ESP.Finance.Utility.AuditHistoryStatus.WaitingContract);

            try
            {
                SendMailHelper.SendMailAuditContract(projectinfo, projectinfo.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.ApplicantUserID)));
                SendMailHelper.SendMailAuditContract(projectinfo, projectinfo.CreatorName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectinfo.CreatorID)));
            }
            catch { }
            Response.Redirect(GetBackUrl());
        }
    }

    private string GetAuditLog(int pid)
    {
        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetProjectList(projectinfo.ProjectId);

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
            else if (model.AuditStatus == (int)AuditHistoryStatus.WaitingContract)
            {
                austatus = "等待合同";
            }
            string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

        }
        return loginfo;
    }

    private string getEmployeeEmailBySysUserId(int SysUserId)
    {
        return new ESP.Compatible.Employee(SysUserId).EMail;
    }

    private string GetBackUrl()
    {
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "OperationAuditList.aspx" : Request["BackUrl"];
    }

    [AjaxPro.AjaxMethod]
    public static List<string> GetBankModel(int bankID)
    {
        List<string> list = new List<string>();
        ESP.Finance.Entity.BankInfo bankmodel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankID);
        list.Add(bankmodel.BankID.ToString());
        list.Add(bankmodel.BankName);
        list.Add(bankmodel.BankAccount);
        list.Add(bankmodel.BankAccountName);
        list.Add(bankmodel.Address);

        return list;
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> GetBanks(int branchid)
    {
        List<List<string>> retlists = new List<List<string>>();
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        paramlist.Add(new System.Data.SqlClient.SqlParameter("@branchid", branchid));
        IList<BankInfo> paylist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchid=@branchid", paramlist);
        List<string> first = new List<string>();
        first.Add("-1");
        first.Add("请选择..");
        retlists.Add(first);
        foreach (BankInfo item in paylist)
        {
            List<string> i = new List<string>();
            i.Add(item.BankID.ToString());
            i.Add(item.BankName);
            retlists.Add(i);
        }

        return retlists;
    }

    [System.Web.Services.WebMethod]
    public static string SetRelevanceProjectId(string relevanceProjectCode)
    {
        var relevanceProject = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(relevanceProjectCode);
        if (relevanceProject != null)
        {
            return relevanceProject.ProjectId.ToString();
        }
        return "error";
    }

}
