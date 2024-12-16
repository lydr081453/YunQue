using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ESP.ITIL.BusinessLogic
{
    /// <summary>
    /// 财务PA运维业务逻辑
    /// </summary>
    public partial class Finance
    {

        /// <summary>
        /// 项目内容填写完毕，提交审核人时调用此函数
        /// 更新项目申请单状态，关联工作流信息。
        /// 发送邮件给相关人员
        /// </summary>
        /// <param name="ProjectModel"></param>
        /// <param name="ZH1Emails"></param>
        /// <param name="CurrentUserName"></param>
        /// <param name="firstAuditorId"></param>
        /// <returns></returns>
        public static int 提交项目号申请单(ESP.Finance.Entity.ProjectInfo ProjectModel, string ZH1Emails, ESP.Compatible.Employee CurrentUser, int firstAuditorId)
        {
            int ret = 0;
            ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(ProjectModel);
            if (result == ESP.Finance.Utility.UpdateResult.Succeed)
            {
                ESP.Finance.BusinessLogic.ProjectManager.UpdateWorkFlow(ProjectModel.ProjectId, ProjectModel.WorkItemID.Value, ProjectModel.WorkItemName, ProjectModel.ProcessID.Value, ProjectModel.InstanceID.Value);
                if (!string.IsNullOrEmpty(ZH1Emails))
                {
                    ESP.Finance.Utility.SendMailHelper.SendMailToZH2(ProjectModel, CurrentUser.Name, ZH1Emails.TrimEnd(','));
                }
                ESP.Finance.Utility.SendMailHelper.SendMailPR(ProjectModel, getEmployeeEmailBySysUserId(ProjectModel.CreatorID), getEmployeeEmailBySysUserId(Convert.ToInt32(ProjectModel.ApplicantUserID)), getEmployeeEmailBySysUserId(firstAuditorId));
                ret = 1;
            }
            return ret;
        }
        /// <summary>
        /// 项目号申请业务审核部分
        /// 根据工作流进行审核流转，同时发送邮件给相关人员
        /// </summary>
        /// <param name="ProjectModel"></param>
        /// <param name="ZH1Emails"></param>
        /// <param name="CurrentUser"></param>
        /// <param name="NextAuditor"></param>
        /// <param name="auditRemark"></param>
        /// <param name="bizAuditComplete"></param>
        /// <returns></returns>
        public static int 项目号业务审核(ESP.Finance.Entity.ProjectInfo ProjectModel, string ZH1Emails, ESP.Compatible.Employee CurrentUser, ESP.Compatible.Employee NextAuditor, string auditRemark, bool bizAuditComplete)
        {
            int ret = 0;
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(ProjectModel.BranchID.Value);
            if (bizAuditComplete)
            {
                //更新申请单状态
                ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(ProjectModel, ESP.Finance.Utility.Status.BizAuditComplete);
                if (result == ESP.Finance.Utility.UpdateResult.Succeed)
                {
                    ret = 1;
                    //更新审批列表状态
                    SetBizAuditHistory(ProjectModel, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, CurrentUser, auditRemark);
                    //发信到财务合同审核人
                    if (ProjectModel.ContractStatusID == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.CAStatus))
                    {
                        ESP.Finance.Utility.SendMailHelper.SendMailBizOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(Convert.ToInt32(branchModel.ContractAccounter)), "Finance");
                        ESP.Framework.Entity.AuditBackUpInfo ContractUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(branchModel.ContractAccounter));
                        if (ContractUser != null)
                        {
                            ESP.Finance.Utility.SendMailHelper.SendMailBizOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(ContractUser.BackupUserID), "Finance");
                        }
                    }
                    else//否则不需要合同审批，直接发到财务人员
                    {
                        //判断金额<=50000 查对应的财务
                        if (ProjectModel.TotalAmount.Value <= Convert.ToDecimal(ESP.Finance.Configuration.ConfigurationManager.FinancialAmount))
                        {
                            ESP.Finance.Utility.SendMailHelper.SendMailBizOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(branchModel.ProjectAccounter), "Finance");
                            ESP.Framework.Entity.AuditBackUpInfo FinanceUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.ProjectAccounter);
                            if (FinanceUser != null)
                            {
                                ESP.Finance.Utility.SendMailHelper.SendMailBizOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(FinanceUser.BackupUserID), "Finance");
                            }
                        }
                        else//大于50000，查财务
                        {
                            ESP.Finance.Utility.SendMailHelper.SendMailBizOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(branchModel.FinalAccounter), "Finance");
                            ESP.Framework.Entity.AuditBackUpInfo FinalUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.FinalAccounter);
                            if (FinalUser != null)
                            {
                                ESP.Finance.Utility.SendMailHelper.SendMailBizOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(FinalUser.BackupUserID), "Finance");
                            }
                        }
                    }
                }
            }
            else
            {
                //更新申请单状态
                ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(ProjectModel, ESP.Finance.Utility.Status.BizAuditing);
                if (result == ESP.Finance.Utility.UpdateResult.Succeed)
                {
                    ret = 1;
                    //更新审批列表状态
                    SetBizAuditHistory(ProjectModel, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, CurrentUser, auditRemark);
                    if (ZH1Emails != "")
                    {
                        ESP.Finance.Utility.SendMailHelper.SendMailToZH(ProjectModel, CurrentUser.Name, NextAuditor.Name, ZH1Emails.TrimEnd(','));
                    }
                    ESP.Finance.Utility.SendMailHelper.SendMailBizOK(ProjectModel, CurrentUser.Name, NextAuditor.EMail, "Biz");
                    ESP.Framework.Entity.AuditBackUpInfo delegateUser = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(NextAuditor.SysID));
                    if (delegateUser != null)
                    {
                        ESP.Finance.Utility.SendMailHelper.SendMailBizOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(delegateUser.BackupUserID), "Biz");
                    }
                }
            }

            return ret;
        }
        /// <summary>
        /// 项目号申请财务审批部分
        /// 工作流完成后，流转到财务
        /// 同时发送邮件给相关人员
        /// </summary>
        /// <param name="ProjectModel"></param>
        /// <param name="ZH1Emails"></param>
        /// <param name="CurrentUser"></param>
        /// <param name="NextAuditor"></param>
        /// <param name="auditRemark"></param>
        /// <returns></returns>
        public static int 项目号财务审核(ESP.Finance.Entity.ProjectInfo ProjectModel, string ZH1Emails, ESP.Compatible.Employee CurrentUser, ESP.Compatible.Employee NextAuditor, string auditRemark,string ProjectCodeGenerated)
        {
            int ret = 0;
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(ProjectModel.BranchID.Value);

            ESP.Framework.Entity.AuditBackUpInfo ContractDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.ContractAccounter);
            ESP.Framework.Entity.AuditBackUpInfo ProjectDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.ProjectAccounter);
            ESP.Framework.Entity.AuditBackUpInfo FinalDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.FinalAccounter);

            if (Convert.ToInt32(CurrentUser.SysID) == branchModel.ContractAccounter || (ContractDelegate != null && ContractDelegate.BackupUserID == Convert.ToInt32(CurrentUser.SysID)))//合同审批
            {
                if (ProjectModel.Status != (int)ESP.Finance.Utility.Status.FinanceAuditComplete)
                {
                    ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(ProjectModel, ESP.Finance.Utility.Status.ContractAudit);
                    SetFinanceAuditHistory(ProjectModel, ESP.Finance.Utility.auditorType.operationAudit_Type_Contract, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing,CurrentUser,auditRemark);
                    if (ProjectModel.TotalAmount.Value <= Convert.ToDecimal(ESP.Finance.Configuration.ConfigurationManager.FinancialAmount))//<=50000任媛戴琼审批
                    {
                        ESP.Finance.Utility.SendMailHelper.SendMailContractOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(branchModel.ProjectAccounter));
                        if (ProjectDelegate != null)
                            ESP.Finance.Utility.SendMailHelper.SendMailContractOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(ProjectDelegate.BackupUserID));
                    }
                    else//否则由eddy审批
                    {
                        ESP.Finance.Utility.SendMailHelper.SendMailContractOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(branchModel.FinalAccounter));
                        if (FinalDelegate != null)
                            ESP.Finance.Utility.SendMailHelper.SendMailContractOK(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(FinalDelegate.BackupUserID));
                    }
                }
                else
                {
                    ESP.Finance.BusinessLogic.ProjectManager.ChangeCheckContractStatus(ProjectModel.ProjectId, ESP.Finance.Utility.ProjectCheckContract.InitialContract);
                }
            }
            else if (Convert.ToInt32(CurrentUser.SysID) == branchModel.ProjectAccounter || (ProjectDelegate != null && ProjectDelegate.BackupUserID == Convert.ToInt32(CurrentUser.SysID)))//任媛戴琼角色的审批
            {
                ProjectModel.ProjectCode = ProjectCodeGenerated;
                ProjectModel.SubmitDate = DateTime.Now;
                ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(ProjectModel, ESP.Finance.Utility.Status.FinanceAuditComplete);//审批完成，生成项目号
                if (result == ESP.Finance.Utility.UpdateResult.Iterative)
                {
                    ret = 2;
                }
                else if (result == ESP.Finance.Utility.UpdateResult.Succeed)
                {
                    SetFinanceAuditHistory(ProjectModel, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing,CurrentUser,auditRemark);
                }
                else
                {
                    ret = 0;
                }
                ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(ProjectModel.ProjectId);//反查出项目号
                //给创建人和负责人发送邮件通知
                ESP.Finance.Utility.SendMailHelper.SendMailAuditComplete(ProjectModel, ProjectModel.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(ProjectModel.ApplicantUserID)));
                ESP.Finance.Utility.SendMailHelper.SendMailAuditComplete(ProjectModel, ProjectModel.CreatorName, getEmployeeEmailBySysUserId(Convert.ToInt32(ProjectModel.CreatorID)));
                //财务审批完成，给支持方发送申请通知
                foreach (ESP.Finance.Entity.SupporterInfo mem in ProjectModel.Supporters)
                {
                    ESP.Finance.Utility.SendMailHelper.SendMailToSupporter(ProjectModel, getEmployeeEmailBySysUserId(Convert.ToInt32(mem.LeaderUserID)));
                }

                ret = 1;
            }
            else if (Convert.ToInt32(CurrentUser.SysID) == branchModel.FinalAccounter || (FinalDelegate != null && FinalDelegate.BackupUserID == Convert.ToInt32(CurrentUser.SysID)))//由eddy审批
            {
                ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(ProjectModel, ESP.Finance.Utility.Status.FinanceAuditing);//审批完成，生成项目号
                SetFinanceAuditHistory(ProjectModel, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing,CurrentUser,auditRemark);
                ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(ProjectModel.ProjectId);//反查出项目号
                //给任媛戴琼发送邮件，让她们对客户、项目号等信息进行二次确认
                ESP.Finance.Utility.SendMailHelper.SendMailToFinancialEdit(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(branchModel.ProjectAccounter));
                if (ProjectDelegate != null)
                    ESP.Finance.Utility.SendMailHelper.SendMailToFinancialEdit(ProjectModel, CurrentUser.Name, getEmployeeEmailBySysUserId(ProjectDelegate.BackupUserID));

                ret = 1;
            }

            return ret;
        }
        /// <summary>
        /// 项目变更，修改相关数据，会触发项目审批流程
        /// </summary>
        /// <param name="ProjectModel"></param>
        /// <param name="CurrentUser"></param>
        /// <returns></returns>

        #region "私有方法"
        private static string getEmployeeEmailBySysUserId(int SysUserId)
        {
            return new ESP.Compatible.Employee(SysUserId).EMail;
        }

        private static void SetBizAuditHistory(ESP.Finance.Entity.ProjectInfo model, int auditStatus, ESP.Compatible.Employee CurrentUser, string auditRemark)
        {
            string term = " projectid=@projectid  and auditstatus=@auditstatus";
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@projectid", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = model.ProjectId;
            paramlist.Add(p1);
            System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@auditstatus", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            paramlist.Add(p3);
            IList<ESP.Finance.Entity.AuditHistoryInfo> auditlist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term, paramlist);
            if (auditlist != null && auditlist.Count > 0)
            {
                ESP.Finance.Entity.AuditHistoryInfo audit = auditlist[0];
                if (audit.AuditorUserID.Value != Convert.ToInt32(CurrentUser.SysID))
                {
                    audit.Suggestion = auditRemark + "[" + CurrentUser.Name + "为" + audit.AuditorEmployeeName + "的代理人]";
                }
                else
                {
                    audit.Suggestion = auditRemark;
                }
                audit.AuditStatus = auditStatus;
                audit.AuditDate = DateTime.Now;
                ESP.Finance.BusinessLogic.AuditHistoryManager.Update(audit);
            }
        }

        private static void SetFinanceAuditHistory(ESP.Finance.Entity.ProjectInfo projectModel, int audittype, int auditstatus, ESP.Compatible.Employee CurrentUser, string auditRemark)
        {
            string term = string.Format(" projectid={0} and audittype={1} and (auditStatus={2} or auditStatus={3})", projectModel.ProjectId, audittype, (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing, (int)ESP.Finance.Utility.AuditHistoryStatus.WaitingContract);
            IList<ESP.Finance.Entity.AuditHistoryInfo> auditlist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term);
            if (auditlist != null && auditlist.Count > 0)
            {
                ESP.Finance.Entity.AuditHistoryInfo audit = auditlist[0];
                if (audit.AuditorUserID.Value != Convert.ToInt32(CurrentUser.SysID))
                {
                    audit.Suggestion = auditRemark + "[" + CurrentUser.Name + "为" + audit.AuditorEmployeeName + "的代理人]";
                }
                else
                {
                    audit.Suggestion = auditRemark;
                }
                audit.ProjectID = projectModel.ProjectId;
                audit.AuditStatus = auditstatus;
                audit.AuditDate = DateTime.Now;
                ESP.Finance.BusinessLogic.AuditHistoryManager.Update(audit);
            }
        }
        #endregion
    }
}
