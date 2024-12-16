using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using WorkFlowModel;
using System.Text;
using ESP.Framework.BusinessLogic;

namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ProjectBLL 的摘要说明。
    /// </summary>


    public static class ProjectManager
    {
        //private static ESP.Finance.DataAccess.ProjectDAL dal=new ESP.Finance.DataAccess.ProjectDAL();

        private static ESP.Finance.IDataAccess.IProjectDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectDataProvider>.Instance; } }
        //private const string _dalProviderName = "ProjectDALProvider";

        private static string _tableName = "ProjectInfo";


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// 项目号(ProjectCode)  是否有重复,重复则插入失败
        /// </summary>
        public static int Add(ESP.Finance.Entity.ProjectInfo model)
        {
            int result = 0;
            List<ESP.Finance.Entity.ProjectInfo> allList = (List<ESP.Finance.Entity.ProjectInfo>)GetAllList();
            if (allList.Exists(n => n.BusinessDescription.Trim() == model.BusinessDescription.Trim()))
            {
                return -1;//项目名称重复
            }

            if (!string.IsNullOrEmpty(model.ProjectCode))
            {
                if (allList.Exists(n => n.ProjectCode.Trim() == model.ProjectCode.Trim()))
                {
                    return -2;//项目号重复
                }
            }
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    model.CreateDate = DateTime.Now;
                    model.Status = (int)Utility.Status.Saved;//设置为保存状态
                    model.SerialCode = DataProvider.CreateSerialCode();//生成项目流水号
                    model.Del = (int)Utility.RecordStatus.Usable;
                    result = DataProvider.Add(model, trans);
                    if (result > 0)//添加成功
                    {
                        //将项目负责人添加到项目成员中
                        ProjectMemberInfo member = new ProjectMemberInfo();
                        member.CreateTime = DateTime.Now;
                        member.ProjectId = result;
                        member.GroupID = model.GroupID;
                        member.GroupName = model.GroupName;
                        member.MemberUserID = model.ApplicantUserID;
                        member.MemberCode = model.ApplicantCode;
                        member.MemberEmployeeName = model.ApplicantEmployeeName;
                        member.MemberUserName = model.ApplicantUserName;
                        member.MemberEmail = model.ApplicantUserEmail;
                        member.MemberPhone = model.ApplicantUserPhone;
                        member.RoleName = model.ApplicantUserPosition;
                        ProjectMemberManager.Add(member, trans);
                        //将项目负责人添加到项目成员中

                        model.ProjectId = result;
                        //只有是最终审批通过后才记入历史表
                        if (model.Status == (int)Utility.Status.FinanceAuditComplete)
                        {
                            ProjectHistManager.Add(model, trans);
                        }
                        //增加权限数据
                        ESP.Purchase.Entity.DataInfo projectdatainfo = new ESP.Purchase.Entity.DataInfo();
                        projectdatainfo.DataId = result;
                        projectdatainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Project;
                        List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                        ESP.Purchase.Entity.DataPermissionInfo permissionCreator = new ESP.Purchase.Entity.DataPermissionInfo();
                        permissionCreator.UserId = model.CreatorID;
                        permissionCreator.IsEditor = true;
                        permissionCreator.IsViewer = true;
                        permissionList.Add(permissionCreator);
                        ESP.Purchase.Entity.DataPermissionInfo permissionResponser = new ESP.Purchase.Entity.DataPermissionInfo();
                        permissionResponser.UserId = model.ApplicantUserID;
                        permissionResponser.IsEditor = true;
                        permissionResponser.IsViewer = true;
                        permissionList.Add(permissionResponser);

                        ESP.Purchase.Entity.DataPermissionInfo permissionBusinessPerson = new ESP.Purchase.Entity.DataPermissionInfo();
                        permissionBusinessPerson.UserId = model.BusinessPersonId.Value;
                        permissionBusinessPerson.IsEditor = true;
                        permissionBusinessPerson.IsViewer = true;
                        permissionList.Add(permissionBusinessPerson);

                        ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(projectdatainfo, permissionList, trans);

                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            return result;

        }

        /// <summary>
        /// 更新项目号
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static UpdateResult Update(ESP.Finance.Entity.ProjectInfo model, SqlTransaction trans)
        {

            return Update(model, trans, false);
        }


        /// <summary>
        /// 更新项目号，包括财务审批通过后，增加流水账记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <param name="IsSaveLog">true 添加流水账   false 不添加</param>
        /// <returns></returns>
        public static UpdateResult Update(ESP.Finance.Entity.ProjectInfo model, SqlTransaction trans, bool IsSaveLog)
        {
            // bool contractAdded = false;
            int contractID = 0;
            ProjectInfo old = DataProvider.GetModel(model.ProjectId, trans);
            ProjectMemberInfo member = ProjectMemberManager.GetModelByPrjMember(old.ProjectId, old.ApplicantUserID, trans);
            List<ESP.Finance.Entity.ProjectInfo> allList = (List<ESP.Finance.Entity.ProjectInfo>)GetAllList();
            //除本条记录 项目名称(BusinessDescription)  是否有重复,重复则更新失败
            if (allList.Exists(n => n.BusinessDescription != null && n.BusinessDescription.Trim() == model.BusinessDescription.Trim() && n.ProjectId != model.ProjectId))
            {
                return UpdateResult.Iterative;
            }

            if (old != null && old.ContractStatusName == ProjectType.BDProject && model.ContractStatusName != ProjectType.BDProject)
            {
                if (string.IsNullOrEmpty(old.ProjectCode) && old.ProjectCode != model.ProjectCode)
                {
                    ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                    log.AuditDate = DateTime.Now;
                    log.AuditorEmployeeName = "";
                    log.AuditorSysID = 0;
                    log.AuditorUserCode = "";
                    log.AuditorUserName = "";
                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                    log.FormID = model.ProjectId;
                    log.FormType = (int)ESP.Finance.Utility.FormType.Project;
                    log.Suggestion = "<font color='red'>原项目号:" + old.ProjectCode + "</font>";
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);
                }
            }

            // 除本条记录 项目号(ProjectCode)  是否有重复,重复则更新失败
            if (!string.IsNullOrEmpty(model.ProjectCode))
            {
                if (allList.Exists(n => n.ProjectCode != null && n.ProjectCode.Trim() == model.ProjectCode.Trim() && n.ProjectId != model.ProjectId))
                {
                    return UpdateResult.Iterative;
                }
            }
            int res = 0;
            res = DataProvider.Update(model, trans);
            if (res > 0)
            {

                //将项目负责人添加到项目成员中(如果已存在则更新)
                bool isExists = true;

                if (member == null)
                {
                    member = new ProjectMemberInfo();
                    member.CreateTime = DateTime.Now;
                    member.ProjectId = model.ProjectId;
                    isExists = false;
                }
                member.GroupID = model.GroupID;
                member.GroupName = model.GroupName;
                member.MemberUserID = model.ApplicantUserID;
                member.MemberCode = model.ApplicantCode;
                member.MemberEmployeeName = model.ApplicantEmployeeName;
                member.MemberUserName = model.ApplicantUserName;
                member.MemberEmail = model.ApplicantUserEmail;
                member.MemberPhone = model.ApplicantUserPhone;
                member.RoleName = model.ApplicantUserPosition;

                if (isExists)
                {
                    new DataAccess.ProjectMemberDataProvider().Update(member, trans);
                }
                else
                {
                    new DataAccess.ProjectMemberDataProvider().Add(member, trans);
                }
                //只有是最终审批通过后才记入历史表
                if (model.Status == (int)Utility.Status.FinanceAuditComplete || model.Status == (int)Utility.Status.ProjectPreClose)
                {
                    ProjectHistManager.Add(model, trans);
                    ContractManager.UpdateContractDel(model.ProjectId, false, trans);
                }

                //增加权限数据
                ESP.Purchase.Entity.DataInfo projectdatainfo = new ESP.Purchase.Entity.DataInfo();
                projectdatainfo.DataId = model.ProjectId;
                projectdatainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Project;
                List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                ESP.Purchase.Entity.DataPermissionInfo permissionCreator = new ESP.Purchase.Entity.DataPermissionInfo();
                permissionCreator.UserId = model.CreatorID;
                permissionCreator.IsEditor = true;
                permissionCreator.IsViewer = true;
                permissionList.Add(permissionCreator);
                ESP.Purchase.Entity.DataPermissionInfo permissionResponser = new ESP.Purchase.Entity.DataPermissionInfo();
                permissionResponser.UserId = model.ApplicantUserID;
                permissionResponser.IsEditor = true;
                permissionResponser.IsViewer = true;
                permissionList.Add(permissionResponser);
                ESP.Purchase.Entity.DataPermissionInfo permissionBusinessPerson = new ESP.Purchase.Entity.DataPermissionInfo();
                permissionBusinessPerson.UserId = model.BusinessPersonId.Value;
                permissionBusinessPerson.IsEditor = true;
                permissionBusinessPerson.IsViewer = true;
                permissionList.Add(permissionBusinessPerson);
                IList<ESP.Finance.Entity.ProjectMemberInfo> memberlist = ESP.Finance.BusinessLogic.ProjectMemberManager.GetListByProject(model.ProjectId, null, null, trans);
                if (memberlist != null)
                {
                    foreach (ESP.Finance.Entity.ProjectMemberInfo mem in memberlist)
                    {
                        ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                        p.UserId = mem.MemberUserID.Value;
                        p.IsEditor = false;
                        p.IsViewer = true;
                        permissionList.Add(p);
                    }
                }
                IList<ESP.Finance.Entity.AuditHistoryInfo> auditList = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(" projectid=@projectid", new List<SqlParameter> { new SqlParameter("@projectid", model.ProjectId) }, trans);
                foreach (ESP.Finance.Entity.AuditHistoryInfo audit in auditList)
                {
                    ESP.Purchase.Entity.DataPermissionInfo p1 = new ESP.Purchase.Entity.DataPermissionInfo();
                    p1.UserId = audit.AuditorUserID.Value;
                    p1.IsEditor = true;
                    p1.IsViewer = true;
                    permissionList.Add(p1);
                }

                ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(projectdatainfo, permissionList, trans);

                //项目号提交成本占用 $$$$$
                #region   增加流水账表记录
                if (IsSaveLog)
                {
                    var contractCosts = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(model.ProjectId, "", null);

                    var projectExpenses = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetListByProject(model.ProjectId);

                    ESP.Purchase.BusinessLogic.CostRecordsManager.InsertProject(model, contractCosts, projectExpenses, trans);
                }
                #endregion

                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;

        }

        /// <summary>
        /// 更新一条数据
        /// 在更新之前首先判断
        /// 最后更新时间 时间戳 是否大于 model 中的时间戳 , 大于则不更新
        /// 除本条记录 项目号(ProjectCode)  是否有重复,重复则更新失败
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ProjectInfo model)
        {
            return Update(model, false);
        }

        public static UpdateResult UpdateAndSaveRecharge(ProjectInfo model, decimal recharge)
        {
            int res = 0;
            ESP.Finance.DataAccess.ProjectExpenseDataProvider expenseDataProvider = new DataAccess.ProjectExpenseDataProvider();
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    res = DataProvider.Update(model, trans);

                    if (res > 0)
                    {
                        List<SqlParameter> parms = new List<SqlParameter>();
                        parms.Add(new SqlParameter("@projectId", model.ProjectId));
                        parms.Add(new SqlParameter("@Description", ESP.Finance.Utility.ProjectExpense_Desc.Recharge));
                        expenseDataProvider.Delete(" projectId=@projectId and Description=@Description", parms, trans);

                        ProjectExpenseInfo expenseInfo = new ProjectExpenseInfo();
                        expenseInfo.ProjectID = model.ProjectId;
                        expenseInfo.Description = ESP.Finance.Utility.ProjectExpense_Desc.Recharge;
                        expenseInfo.Expense = recharge;
                        expenseDataProvider.Add(expenseInfo);

                    }
                    trans.Commit();
                    return UpdateResult.Succeed;
                }
                catch
                {
                    trans.Rollback();
                }
            }
            return UpdateResult.Failed;
        }

        /// <summary>
        /// 更新一条数据
        /// 在更新之前首先判断
        /// 最后更新时间 时间戳 是否大于 model 中的时间戳 , 大于则不更新
        /// 除本条记录 项目号(ProjectCode)  是否有重复,重复则更新失败
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ProjectInfo model, bool IsSaveLog)
        {
            bool contractAdded = false;
            int contractID = 0;
            ProjectInfo old = DataProvider.GetModel(model.ProjectId);
            ProjectMemberInfo member = ProjectMemberManager.GetModelByPrjMember(old.ProjectId, old.ApplicantUserID);

            ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.ApplicantUserID);

            List<ESP.Finance.Entity.ProjectInfo> allList = (List<ESP.Finance.Entity.ProjectInfo>)GetAllList();
            //除本条记录 项目名称(BusinessDescription)  是否有重复,重复则更新失败
            if (allList.Exists(n => n.BusinessDescription != null && n.BusinessDescription.Trim() == model.BusinessDescription.Trim() && n.ProjectId != model.ProjectId))
            {
                return UpdateResult.Iterative;
            }

            // 除本条记录 项目号(ProjectCode)  是否有重复,重复则更新失败
            if (!string.IsNullOrEmpty(model.ProjectCode))
            {
                if (allList.Exists(n => n.ProjectCode != null && n.ProjectCode.Trim() == model.ProjectCode.Trim() && n.ProjectId != model.ProjectId))
                {
                    return UpdateResult.Iterative;
                }
            }
            int res = 0;
            int i = 0;

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    res = DataProvider.Update(model, trans);

                    if (res > 0)
                    {
                        //将项目负责人添加到项目成员中(如果已存在则更新)
                        bool isExists = true;

                        if (member == null)
                        {
                            member = new ProjectMemberInfo();
                            member.CreateTime = DateTime.Now;
                            member.ProjectId = model.ProjectId;
                            isExists = false;
                        }
                        member.GroupID = positionModel.DepartmentID;
                        member.GroupName = positionModel.DepartmentName;
                        member.MemberUserID = model.ApplicantUserID;
                        member.MemberCode = model.ApplicantCode;
                        member.MemberEmployeeName = model.ApplicantEmployeeName;
                        member.MemberUserName = model.ApplicantUserName;
                        member.MemberEmail = model.ApplicantUserEmail;
                        member.MemberPhone = model.ApplicantUserPhone;
                        member.RoleName = model.ApplicantUserPosition;

                        if (isExists)
                        {
                            new DataAccess.ProjectMemberDataProvider().Update(member, trans);
                        }
                        else
                        {
                            new DataAccess.ProjectMemberDataProvider().Add(member, trans);
                        }

                        //将项目负责人添加到项目成员中

                        //只有是最终审批通过后才记入历史表
                        if (model.Status == (int)Utility.Status.FinanceAuditComplete)
                        {
                            ProjectHistManager.Add(model, trans);
                            ContractManager.UpdateContractDel(model.ProjectId, false, trans);
                        }
                        //如果项目状态是审批中的状态
                        if (model.Status >= (int)Utility.Status.Submit)
                        {
                            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(model.BranchID.Value, trans);
                            string term = " AuditorUserID=@AuditorUserID and AuditType=@AuditType and ProjectID=@ProjectID";
                            List<System.Data.SqlClient.SqlParameter> paramlist = new List<SqlParameter>();
                            SqlParameter p1 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
                            p1.Value = branchModel.ContractAccounter;
                            paramlist.Add(p1);
                            SqlParameter p2 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                            p2.Value = ESP.Finance.Utility.auditorType.operationAudit_Type_Contract;
                            paramlist.Add(p2);
                            SqlParameter p3 = new SqlParameter("@ProjectID", SqlDbType.Int, 4);
                            p3.Value = model.ProjectId;
                            paramlist.Add(p3);
                            IList<ESP.Finance.Entity.AuditHistoryInfo> histList = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term, paramlist, trans);
                            IList<ESP.Finance.Entity.AuditHistoryInfo> financehistList = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList("ProjectID=" + model.ProjectId.ToString() + " and audittype in(11,12)", paramlist, trans);
                            IList<ESP.Finance.Entity.AuditHistoryInfo> countList = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList("ProjectID=" + model.ProjectId.ToString(), paramlist, trans);
                            IList<ESP.Finance.Entity.ContractAuditLogInfo> calist = ESP.Finance.BusinessLogic.ContractAuditLogManager.GetList(" ProjectId=" + model.ProjectId.ToString());

                            //类型是合同的检查是否有合同审批这一级
                            if (model.ContractStatusID.Value == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.CAStatus) && (calist == null || calist.Count == 0) && branchModel.ContractAccounter > 0)
                            {

                                if (histList == null || histList.Count == 0)//如果没有合同审批这一级，添加合同审批
                                {
                                    ESP.Finance.Entity.AuditHistoryInfo contract = new ESP.Finance.Entity.AuditHistoryInfo();
                                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(branchModel.ContractAccounter);
                                    contractID = branchModel.ContractAccounter;
                                    contract.AuditorEmployeeName = emp.Name;
                                    contract.AuditorUserCode = emp.ID;
                                    contract.AuditorUserID = Convert.ToInt32(emp.SysID);
                                    contract.AuditorUserName = emp.ITCode;
                                    contract.ProjectID = model.ProjectId;
                                    contract.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Contract;
                                    contract.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                                    contract.SquenceLevel = countList.Count - financehistList.Count;
                                    new DataAccess.AuditHistoryDataProvider().Add(contract, trans);
                                    contractAdded = true;


                                }
                                if (model.Status != (int)ESP.Finance.Utility.Status.Submit && model.Status != (int)ESP.Finance.Utility.Status.ContractAudit && model.Status != (int)ESP.Finance.Utility.Status.FinanceAuditing && model.Status != (int)ESP.Finance.Utility.Status.FinanceAuditComplete && model.Status != (int)ESP.Finance.Utility.Status.ProjectPreClose)
                                {
                                    foreach (ESP.Finance.Entity.AuditHistoryInfo au in financehistList)
                                    {
                                        new DataAccess.AuditHistoryDataProvider().Delete(au.AuditID, trans);
                                    }
                                    foreach (ESP.Finance.Entity.AuditHistoryInfo au in financehistList)
                                    {
                                        au.AuditID = 0;
                                        i += 1;
                                        au.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                                        au.SquenceLevel = countList.Count - financehistList.Count + i;
                                        new DataAccess.AuditHistoryDataProvider().Add(au, trans);
                                    }
                                }
                            }
                            else//类型不是合同的删除合同级的审批
                            {
                                if (histList != null && histList.Count > 0)
                                {
                                    foreach (ESP.Finance.Entity.AuditHistoryInfo histModel in histList)
                                    {
                                        new DataAccess.AuditHistoryDataProvider().Delete(histModel.AuditID, trans);
                                    }
                                }
                            }
                        }
                        //增加权限数据
                        ESP.Purchase.Entity.DataInfo projectdatainfo = new ESP.Purchase.Entity.DataInfo();
                        projectdatainfo.DataId = model.ProjectId;
                        projectdatainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Project;
                        List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                        ESP.Purchase.Entity.DataPermissionInfo permissionCreator = new ESP.Purchase.Entity.DataPermissionInfo();
                        permissionCreator.UserId = model.CreatorID;
                        permissionCreator.IsEditor = true;
                        permissionCreator.IsViewer = true;
                        permissionList.Add(permissionCreator);
                        ESP.Purchase.Entity.DataPermissionInfo permissionResponser = new ESP.Purchase.Entity.DataPermissionInfo();
                        permissionResponser.UserId = model.ApplicantUserID;
                        permissionResponser.IsEditor = true;
                        permissionResponser.IsViewer = true;
                        permissionList.Add(permissionResponser);
                        ESP.Purchase.Entity.DataPermissionInfo permissionBusinessPerson = new ESP.Purchase.Entity.DataPermissionInfo();
                        permissionBusinessPerson.UserId = model.BusinessPersonId.Value;
                        permissionBusinessPerson.IsEditor = true;
                        permissionBusinessPerson.IsViewer = true;
                        permissionList.Add(permissionBusinessPerson);
                        IList<ESP.Finance.Entity.ProjectMemberInfo> memberlist = ESP.Finance.BusinessLogic.ProjectMemberManager.GetListByProject(model.ProjectId, null, null, trans);
                        if (memberlist != null)
                        {
                            foreach (ESP.Finance.Entity.ProjectMemberInfo mem in memberlist)
                            {
                                ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                                p.UserId = mem.MemberUserID.Value;
                                p.IsEditor = false;
                                p.IsViewer = true;
                                permissionList.Add(p);
                            }
                        }
                        IList<ESP.Finance.Entity.AuditHistoryInfo> auditList = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(" projectid=@projectid", new List<SqlParameter> { new SqlParameter("@projectid", model.ProjectId) }, trans);
                        foreach (ESP.Finance.Entity.AuditHistoryInfo audit in auditList)
                        {
                            ESP.Purchase.Entity.DataPermissionInfo per1 = new ESP.Purchase.Entity.DataPermissionInfo();
                            per1.UserId = audit.AuditorUserID.Value;
                            per1.IsEditor = true;
                            per1.IsViewer = true;
                            permissionList.Add(per1);
                        }
                        if (contractAdded)
                        {
                            ESP.Purchase.Entity.DataPermissionInfo per2 = new ESP.Purchase.Entity.DataPermissionInfo();
                            per2.UserId = contractID;
                            per2.IsEditor = true;
                            per2.IsViewer = true;
                            permissionList.Add(per2);
                        }
                        ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(projectdatainfo, permissionList, trans);
                        trans.Commit();
                        return UpdateResult.Succeed;
                    }
                    else if (res == 0)
                    {
                        trans.Commit();
                        return UpdateResult.UnExecute;
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            return UpdateResult.Failed;

        }

        public static ESP.Finance.Utility.UpdateResult UpdateWorkFlow(int projectId, int workItemID, string workItemName, int processID, int instanceID)
        {
            ProjectInfo model = GetModelWithOutDetailList(projectId);//重新取一遍对象(为避免时间戳问题)
            int res = 0;
            try
            {
                //trans//res = DataProvider.UpdateWorkFlow(projectId, workItemID, workItemName, processID, instanceID, true);
                res = DataProvider.UpdateWorkFlow(projectId, workItemID, workItemName, processID, instanceID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            if (res > 0)
            {
                //ProjectHistManager.Add(model);
                //LogManager.Add("Update", _tableName, "工作流");
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;

        }

        /// <summary>
        /// 更新检查合同字段(未检查时间戳)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="status"></param>
        /// <returns></returns>



        public static ESP.Finance.Utility.UpdateResult ChangeCheckContractStatus(int projectId, Utility.ProjectCheckContract status)
        {

            int res = 0;
            try
            {
                //trans//res = DataProvider.ChangeCheckContractStatus(projectId, status, true);
                res = DataProvider.ChangeCheckContractStatus(projectId, status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;

        }


        /// <summary>
        /// 更新一条数据
        /// 在更新之前首先判断
        /// 最后更新时间 时间戳 是否大于 model 中的时间戳 , 大于则不更新
        /// 除本条记录 项目号(ProjectCode)  是否有重复,重复则更新失败
        /// </summary>



        public static UpdateResult Submit(ESP.Finance.Entity.ProjectInfo model)
        {

            if (CheckerManager.CheckProjectSubmit(model.ProjectId))
            {
                model = GetModelWithOutDetailList(model.ProjectId);
                model.Status = (int)Utility.Status.Submit;
                model.SubmitDate = DateTime.Now;
                return Update(model);
            }
            else
            {
                return UpdateResult.CannotSubmit;
            }

        }

        public static UpdateResult SubmitProject(ESP.Finance.Entity.ProjectInfo model)
        {

            if (CheckerManager.CheckProjectSubmit(model.ProjectId))
            {
                model = GetModelWithOutDetailList(model.ProjectId);
                model.Status = (int)Utility.Status.Submit;
                model.SubmitDate = DateTime.Now;
                //$$$$$
                return Update(model, true);
            }
            else
            {
                return UpdateResult.CannotSubmit;
            }

        }

        private static IList<ESP.Finance.Entity.ProjectHistInfo> getProjectList(int ProjectID)
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
            var tmplist = projectList.OrderByDescending(N => N.ProjectHistID).ToList();
            return tmplist;
        }

        /// <summary>
        /// 更新状态 (为审批用)
        /// </summary>
        public static UpdateResult ChangeStatus(ESP.Finance.Entity.ProjectInfo model, ESP.Finance.Utility.Status status)
        {
            IList<ESP.Finance.Entity.ProjectHistInfo> projecthistList = getProjectList(model.ProjectId);
            ESP.Finance.Entity.ProjectInfo oldProjectModel = null;
            if (projecthistList != null && projecthistList.Count() != 0)
            {
                oldProjectModel = projecthistList[0].ProjectModel.ObjectDeserialize<ProjectInfo>();
            }
            UpdateResult result = new UpdateResult();
            model.Status = (int)status;
            if (status == Status.Saved)
            {
                result = Update(model);
            }
            else if (status == Status.Submit)
            {
                result = Submit(model);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        model.LastAuditDate = DateTime.Now;
                        if (status == Status.FinanceAuditComplete || status == Status.ProjectPreClose)//业务审批结束生成项目号
                        {

                            new DataAccess.ContractDataProvider().UpdateContractDelay(model.ProjectId, trans);//更新延期合同文件状态
                            new DataAccess.PaymentDataProvider().UpdatePaymentBudgetConfirm(model.ProjectId, trans);//财务确认付款通知金额

                            if (oldProjectModel != null && oldProjectModel.ContractStatusName == ProjectType.BDProject && model.ContractStatusName != ProjectType.BDProject)
                            {
                                model.BDProjectCode = oldProjectModel.ProjectCode;
                            }

                        }

                        Update(model, trans, true);
                        trans.Commit();
                        result = UpdateResult.Succeed;
                    }
                    catch (Exception ex)
                    {
                        result = UpdateResult.Failed;
                        trans.Rollback();
                        throw new Exception(ex.ToString());
                    }
                }
            }
            return result;
        }



        /// <summary>
        /// 计算服务费
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>


        //public static decimal GetServiceFee(ESP.Finance.Entity.ProjectInfo model)
        //{
        //    decimal totalamount = model.TotalAmount == null ? 0 : model.TotalAmount.Value;
        //    ESP.Finance.Entity.TaxRateInfo rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(model.ContractTaxID.Value);


        //    decimal totalContractCost = 0;
        //    decimal totalSupportCost = 0;
        //    decimal totalPrjExpense = 0;

        //    if (model.CostDetails != null && model.CostDetails.Count > 0)//项目总成本
        //    {
        //        totalContractCost = model.CostDetails.Sum(n => n.Cost) ?? 0;
        //    }

        //    if (model.Supporters != null && model.Supporters.Count > 0)//付给支持方的成本
        //    {
        //        totalSupportCost = model.Supporters.Sum(n => n.BudgetAllocation) ?? 0;
        //    }

        //    if (model.Expenses != null && model.Expenses.Count > 0)//项目总费用
        //    {
        //        totalPrjExpense = model.Expenses.Sum(n => n.Expense) ?? 0;
        //    }

        //    decimal fee = totalamount - CheckerManager.GetTax(model, rateModel) - totalContractCost - totalSupportCost - totalPrjExpense;//change totalamount * taxRate
        //    return fee;
        //}


        //public static decimal GetServiceFee(ESP.Finance.Entity.ProjectInfo model, decimal tax)
        //{
        //    decimal totalamount = model.TotalAmount == null ? 0 : model.TotalAmount.Value;


        //    decimal totalContractCost = 0;
        //    decimal totalSupportCost = 0;
        //    decimal totalPrjExpense = 0;

        //    if (model.CostDetails != null && model.CostDetails.Count > 0)//项目总成本
        //    {
        //        totalContractCost = model.CostDetails.Sum(n => n.Cost) ?? 0;
        //    }

        //    if (model.Supporters != null && model.Supporters.Count > 0)//付给支持方的成本
        //    {
        //        totalSupportCost = model.Supporters.Sum(n => n.BudgetAllocation) ?? 0;
        //    }

        //    if (model.Expenses != null && model.Expenses.Count > 0)//项目总费用
        //    {
        //        totalPrjExpense = model.Expenses.Sum(n => n.Expense) ?? 0;
        //    }

        //    decimal fee = totalamount - tax - totalContractCost - totalSupportCost - totalPrjExpense;//change totalamount * taxRate
        //    return fee;
        //}

        /// <summary>
        /// 逻辑删除一条数据
        /// </summary>
        public static DeleteResult LogicDelete(int ProjectId)
        {
            Entity.ProjectInfo model = GetModelWithOutDetailList(ProjectId);
            int res = 0;
            try
            {
                model.Del = (int)Utility.RecordStatus.Del;
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                LogManager.Add("Delete", _tableName, "逻辑删除一条数据");
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }


        /// <summary>
        /// 物理删除一条数据
        /// </summary>
        public static DeleteResult PhysicalDelete(int ProjectId)
        {
            Entity.ProjectInfo model = GetModelWithOutDetailList(ProjectId);
            if (!string.IsNullOrEmpty(model.ProjectCode))
            {
                return DeleteResult.Failed;
            }
            int res = 0;
            try
            {
                res = DataProvider.Delete(ProjectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                LogManager.Add("Delete", _tableName, "物理删除一条数据");
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }


        public static string CreateProjectCode(ProjectInfo model)
        {
            DateTime DeadLine = DateTime.Now;

            //-----------加结帐日判断------------
            IList<DeadLineInfo> list = DeadLineManager.GetList(n => n.ProjectDeadLineYear == DeadLine.Year && n.ProjectDeadLineMonth == DeadLine.Month);
            if (list != null && list.Count > 0 && list[0] != null)
            {
                if (DeadLine > new DateTime(list[0].ProjectDeadLineYear, list[0].ProjectDeadLineMonth, list[0].ProjectDeadLineDay, 23, 59, 59))//如果大于结帐日,则视为下个月
                {
                    DeadLine = DeadLine.AddMonths(1);
                }
            }
            //-------------------------------------
            //trans//return DataProvider.CreateProjectCode(DeadLine,model, true);
            return DataProvider.CreateProjectCode(DeadLine, model);
        }




        public static string CreateProjectCode(int prjId)
        {
            Entity.ProjectInfo model = GetModel(prjId);
            return CreateProjectCode(model);
        }


        /// <summary>
        /// 得到一个对象实体,包括其中的明细列表
        /// </summary>
        public static ESP.Finance.Entity.ProjectInfo GetModel(int ProjectId)
        {
            return GetModel(ProjectId, true);

        }

        public static ESP.Finance.Entity.ProjectInfo GetModel(int ProjectId, SqlTransaction trans)
        {
            return GetModel(ProjectId, true, trans);

        }

        /// <summary>
        /// 得到对象,不包括其中的明细列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Entity.ProjectInfo GetModelWithOutDetailList(int projectId)
        {
            return GetModel(projectId, false);
        }

        public static Entity.ProjectInfo GetModelWithOutDetailList(int projectId, SqlTransaction trans)
        {
            return GetModel(projectId, false, trans);
        }

        /// <summary>
        /// 变更项目号
        /// </summary>
        /// <param name="model">ProjectInfo对象</param>
        /// <param name="oldProjectCode">原始项目号</param>
        /// <returns></returns>
        public static bool ProjectCodeChanged(Entity.ProjectInfo model, string oldProjectCode)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DataProvider.Update(model, trans);//修改Project中项目号
                    //记录项目变更日志

                    //向项目号变更表中添加项目信息
                    ESP.Purchase.Entity.ProjectCodeChangedInfo projectCode = new ESP.Purchase.Entity.ProjectCodeChangedInfo();
                    projectCode.ChangedUserId = Common.CurrentUser.UserID;
                    projectCode.ChangedUserName = Common.CurrentUser.FullNameCN;
                    projectCode.DataId = model.ProjectId;
                    projectCode.DataType = (int)ESP.Purchase.Common.State.DataType.Project;
                    projectCode.OldProjectCode = oldProjectCode;
                    ESP.Purchase.DataAccess.ProjectCodeChangedLogProvider.Add(projectCode, trans);

                    List<ESP.Purchase.Entity.GeneralInfo> generalInfoList = ESP.Purchase.DataAccess.GeneralInfoDataProvider.GetListByProjectId(model.ProjectId);
                    ESP.Purchase.Entity.ProjectCodeChangedInfo projectCodeChangedModel = null;
                    string generalIds = "";
                    foreach (ESP.Purchase.Entity.GeneralInfo generalInfo in generalInfoList)
                    {
                        generalInfo.project_code = model.ProjectCode;
                        ESP.Purchase.DataAccess.GeneralInfoDataProvider.Update(generalInfo, trans.Connection, trans);//修改项目号下所有PR单
                        projectCodeChangedModel = new ESP.Purchase.Entity.ProjectCodeChangedInfo();
                        projectCodeChangedModel.ChangedUserId = Common.CurrentUser.UserID;
                        projectCodeChangedModel.ChangedUserName = Common.CurrentUser.FullNameCN;
                        projectCodeChangedModel.DataId = generalInfo.id;
                        projectCodeChangedModel.DataType = (int)ESP.Purchase.Common.State.DataType.GR;
                        projectCodeChangedModel.OldProjectCode = oldProjectCode;
                        ESP.Purchase.DataAccess.ProjectCodeChangedLogProvider.Add(projectCodeChangedModel, trans);//向项目号变更表中添加PR单信息
                        generalIds += generalInfo.id + "、";
                    }
                    trans.Commit();
                    ESP.Logging.Logger.Add(string.Format("{0}将流水号为:{1}的的申请单项目号变更为{2}", ESP.Finance.Utility.Common.CurrentUser.FullNameCN, generalIds.TrimEnd(','), model.ProjectCode), "项目号变更");
                    return true;

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "项目号变更", ESP.Logging.LogLevel.Error, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 项目暂停
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool ProjectDisabled(int projectId, System.Web.HttpRequest request)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectId);
                    projectModel.Status = (int)ESP.Finance.Utility.Status.ProjectPreClose;
                    DataProvider.Update(projectModel, trans);//停用项目
                    //ESP.Purchase.BusinessLogic.GeneralInfoManager.UsedPRByProjectId(projectId, projectModel.ProjectCode, false, Common.CurrentUser.UserID, Common.CurrentUser.FullNameCN, request, trans);//停用项目下申请单
                    trans.Commit();
                    ESP.Logging.Logger.Add(string.Format("{0}将项目ID为:{1}的项目暂停", ESP.Finance.Utility.Common.CurrentUser.FullNameCN, projectId), "项目暂停");
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "项目停用", ESP.Logging.LogLevel.Error, ex);
                    return false;
                }
            }
        }


        /// <summary>
        /// 项目启用
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool ProjectUsed(int projectId, System.Web.HttpRequest request)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectId);
                    projectModel.Status = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
                    DataProvider.Update(projectModel, trans);//启用
                    //ESP.Purchase.BusinessLogic.GeneralInfoManager.UsedPRByProjectId(projectId, projectModel.ProjectCode, true, Common.CurrentUser.UserID, Common.CurrentUser.FullNameCN, request, trans);//启用项目下申请单
                    trans.Commit();
                    ESP.Logging.Logger.Add(string.Format("{0}将项目ID为:{1}的项目启用", ESP.Finance.Utility.Common.CurrentUser.FullNameCN, projectId), "项目启用");
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "项目启用", ESP.Logging.LogLevel.Error, ex);
                    return false;
                }
            }
        }


        /// <summary>
        /// 得到一个project对象
        /// </summary>
        /// <param name="ProjectId">ProjectID</param>
        /// <param name="withDetailList">是否包括明细列表</param>
        /// <returns></returns>
        private static ESP.Finance.Entity.ProjectInfo GetModel(int ProjectId, bool withDetailList)
        {
            ESP.Finance.Entity.ProjectInfo model = DataProvider.GetModel(ProjectId);
            if (withDetailList && model != null)
            {
                getListItem(ProjectId, ref model);
            }

            return model;
        }

        private static ESP.Finance.Entity.ProjectInfo GetModel(int ProjectId, bool withDetailList, SqlTransaction trans)
        {
            ESP.Finance.Entity.ProjectInfo model = DataProvider.GetModel(ProjectId, trans);
            if (withDetailList && model != null)
            {
                getListItem(ProjectId, ref model, trans);
            }

            return model;
        }


        /// <summary>
        /// 得到项目的明细列表
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="model"></param>
        public static void getListItem(int ProjectId, ref Entity.ProjectInfo model)
        {
            if (model == null) return;
            model.Members = ProjectMemberManager.GetListByProject(ProjectId, null, null);//成员列表
            model.Supporters = SupporterManager.GetListByProject(ProjectId, null, null);//支持方列表
            model.Contracts = ContractManager.GetListByProject(ProjectId, null, null);//合同列表
            model.Payments = PaymentManager.GetListByProject(ProjectId, null, null);//支付通知列表
            model.CostDetails = ContractCostManager.GetListByProject(ProjectId, null, null);//成本明细列表
            model.Consumptions = ConsumptionManager.GetList(" projectId= " + ProjectId.ToString());//消耗
            model.ProjectSchedules = ProjectScheduleManager.GetListByProject(ProjectId);//各月完百分比
            model.Expenses = ProjectExpenseManager.GetListByProject(ProjectId);//OOP
            int customerid = model.CustomerID ?? 0;//客户
            if (customerid > 0)
            {
                model.Customer = CustomerTmpManager.GetModel(customerid);
            }
        }

        private static void getListItem(int ProjectId, ref Entity.ProjectInfo model, SqlTransaction trans)
        {
            if (model == null) return;
            model.Members = new DataAccess.ProjectMemberDataProvider().GetListByProject(ProjectId, "", null, trans); //成员列表
            model.Supporters = new DataAccess.SupporterDataProvider().GetListByProject(ProjectId, "", null, trans); //支持方列表
            model.Contracts = new DataAccess.ContractDataProvider().GetListByProject(ProjectId, "", null, trans); //合同列表
            model.Payments = new DataAccess.PaymentDataProvider().GetListByProject(ProjectId, "", null, trans);//支付通知列表
            model.CostDetails = new DataAccess.ContractCostDataProvider().GetListByProject(ProjectId, "", null, trans);//成本明细列表
            //model.Hists = ProjectHistManager.GetListByProject(ProjectId, null, null);//历史信息
            model.ProjectSchedules = ProjectScheduleManager.GetAllList().Where(n => n.ProjectID == ProjectId).ToList<ESP.Finance.Entity.ProjectScheduleInfo>();//各月完百分比
            model.Expenses = ProjectExpenseManager.GetAllList().Where(n => n.ProjectID == ProjectId).ToList<ESP.Finance.Entity.ProjectExpenseInfo>();//OOP
            int customerid = model.CustomerID == null ? 0 : Convert.ToInt32(model.CustomerID);//客户
            if (customerid > 0)
            {
                model.Customer = CustomerTmpManager.GetModel(customerid);
            }
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

        #endregion  成员方法


        #region IProjectProvider 成员


        private static void AddValue(Dictionary<int, decimal> m, int key, decimal val)
        {
            decimal cv;
            if (m.TryGetValue(key, out cv))
            {
                m[key] = cv + val;
            }
            else
            {
                m.Add(key, val);
            }
        }

        private static decimal getUsedCost(ESP.Finance.Entity.ProjectInfo projectModel)
        {
            IList<ESP.Purchase.Entity.GeneralInfo> PRList;
            IList<ReturnInfo> ReturnList;
            IList<ExpenseAccountDetailInfo> ExpenseDetails;
            Dictionary<int, int> TypeMappings;
            IList<ESP.Purchase.Entity.PaymentPeriodInfo> Periods;
            List<ESP.Purchase.Entity.OrderInfo> Orders;
            List<CostRecordInfo> ExpenseRecords;
            List<CostRecordInfo> PRRecords;

            Dictionary<int, decimal> CostMappings = new Dictionary<int, decimal>();
            Dictionary<int, decimal> ExpenseMappings = new Dictionary<int, decimal>();

            var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
            typelvl2[0] = "OOP";
            typelvl2[-1] = "[未知]";

            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(projectModel.ProjectId, projectModel.GroupID.Value);
            ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(projectModel.ProjectId, projectModel.GroupID.Value);
            ExpenseDetails = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(ReturnList.Select(x => x.ReturnID).ToArray());
            TypeMappings = ESP.Purchase.BusinessLogic.TypeManager.GetTypeMappings();
            Orders = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralIds(PRList.Select(x => x.id).ToArray());

            ExpenseRecords = new List<CostRecordInfo>();
            PRRecords = new List<CostRecordInfo>();

            #region "查PR单"
            foreach (var pr in PRList)
            {
                decimal paid = 0;
                var orders = Orders.Where(x => x.general_id == pr.id);

                paid = ReturnList.Where(x => x.PRID == pr.id && x.ReturnType != 11 && x.ReturnStatus == 140).Sum(x => x.FactFee ?? 0);
                foreach (var o in orders)
                {
                    var costTypeId = o.producttype;
                    if (!TypeMappings.TryGetValue(costTypeId, out costTypeId)) costTypeId = 0;

                    if (o.FactTotal != 0)
                        AddValue(CostMappings, costTypeId, o.FactTotal);
                    else
                    {
                        if (paid > o.total)
                            AddValue(CostMappings, costTypeId, paid);
                        else
                            AddValue(CostMappings, costTypeId, o.total);
                    }
                }

                if (paid > pr.totalprice)
                {
                    pr.totalprice = paid;
                }

                var typeid = orders.Select(x => x.producttype).FirstOrDefault();
                if (!TypeMappings.TryGetValue(typeid, out typeid)) typeid = 0;
                CostRecordInfo detail = new CostRecordInfo()
                {
                    PRID = pr.id,
                    PRNO = pr.PrNo,
                    SupplierName = pr.supplier_name,
                    Description = pr.project_descripttion,
                    Requestor = pr.requestorname,
                    GroupName = pr.requestor_group,
                    TypeID = typeid,
                    TypeName = typelvl2[typeid],
                    AppAmount = pr.totalprice,
                    PaidAmount = paid,
                    UnPaidAmount = pr.totalprice - paid,
                    CostPreAmount = projectModel.CostDetails.Where(x => x.CostTypeID == typeid).Select(x => x.Cost ?? 0).FirstOrDefault()
                };
                PRRecords.Add(detail);
            }
            #endregion

            foreach (var record in PRRecords)
            {
                decimal v = 0M;
                CostMappings.TryGetValue(record.TypeID, out v);
                record.TypeTotalAmount = v;
            }
            #region "查报销"
            var expenseReturns = ReturnList.Where(x => x.ReturnType == 30
                || (x.ReturnType == 32 && x.ReturnStatus != 140)
                || x.ReturnType == 31
                || x.ReturnType == 37
                || x.ReturnType == 33
                || x.ReturnType == 40
                || (x.ReturnType == 36 && x.ReturnStatus == 139)
                || x.ReturnType == 35).ToList();
            foreach (var r in expenseReturns)
            {
                var details = ExpenseDetails.Where(x => x.ReturnID == r.ReturnID && x.Status == 1).ToList();
                foreach (var d in details)
                {
                    if (d.TicketStatus == 1)
                        continue;
                    var e = d.ExpenseMoney ?? 0;
                    if (e != 0)
                        AddValue(ExpenseMappings, d.CostDetailID ?? 0, e);

                    var typeid = d.CostDetailID ?? 0;
                    decimal preamount = 0;
                    if (typeid == 0)
                        preamount = projectModel.Expenses.Where(x => x.Description == "OOP").Select(x => x.Expense ?? 0).FirstOrDefault();
                    else
                    {
                        ESP.Finance.Entity.ContractCostInfo costmodel = projectModel.CostDetails.Where(x => x.CostTypeID == typeid).FirstOrDefault();
                        preamount = costmodel == null ? 0 : costmodel.Cost.Value;
                    }
                    CostRecordInfo detail = new CostRecordInfo()
                    {
                        ReturnType = r.ReturnType ?? 0,
                        PRNO = r.ReturnCode,
                        Description = d.ExpenseDesc,
                        Requestor = r.RequestEmployeeName,
                        GroupName = r.DepartmentName,
                        TypeID = typeid,
                        TypeName = typelvl2[typeid],
                        AppAmount = e,
                        PaidAmount = (r.ReturnStatus == 140 || r.ReturnStatus == 139) ? e : 0,
                        UnPaidAmount = (r.ReturnStatus != 140 && r.ReturnStatus != 139) ? e : 0,
                        CostPreAmount = preamount,
                        PNTotal = r.PreFee ?? 0
                    };
                    ExpenseRecords.Add(detail);
                }
            }


            foreach (var record in ExpenseRecords)
            {
                decimal v = 0M;
                ExpenseMappings.TryGetValue(record.TypeID, out v);
                record.TypeTotalAmount = v;
            }
            #endregion
            return CostMappings.Sum(x => x.Value) + ExpenseMappings.Sum(x => x.Value);

        }

        /// <summary>
        /// 项目号登记表
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="stoptime"></param>
        /// <param name="term"></param>
        /// <param name="param"></param>
        /// <returns>生成路径</returns>
        public static string GetProjectSigning(DateTime starttime, DateTime stoptime, IList<ESP.Finance.Entity.ProjectInfo> projectlist, System.Web.HttpResponse response)
        {
            string sourceFileName = "/Tmp/Project/ProjectRecords.xls";

            string sourceFile = Common.GetLocalPath(sourceFileName);
            if (projectlist == null || projectlist.Count == 0) return sourceFileName;

            List<MonthCol> monthCols = GetMonthsCell(starttime, stoptime);//列出起始时间到结束时间的按月分的列
            if (monthCols == null) return null;

            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];

            int lineoffset = 1;//行数索引

            foreach (MonthCol monthcol in monthCols)//动态填写年月的列头
            {
                ExcelHandle.WriteCell(excel.CurSheet, monthcol.cellName + lineoffset.ToString(), monthcol.key);
            }

            foreach (ProjectInfo model in projectlist)
            {
                ProjectInfo project = model;
                getListItem(project.ProjectId, ref project);
                ESP.Finance.Entity.TaxRateInfo rateModel = null;
                if (project.ContractTaxID != null && project.ContractTaxID != 0)
                {
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);
                }

                //项目号
                string projectcode_cell = "A" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, projectcode_cell, model.ProjectCode);

                string closedate_cell = "B" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, closedate_cell, model.BDProjectCode);

                string relationdate_cell = "C" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, relationdate_cell, model.RelevanceProjectCode);
                //组别
                DepartmentViewInfo dept = DepartmentViewManager.GetModel(model.GroupID.Value);
                string projectid_cell = "D" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, projectid_cell, dept.level1 + "-" + dept.level2 + "-" + dept.level3);

                //负责人
                string response_cell = "E" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, response_cell, model.ApplicantEmployeeName);

                // 合同状态
                string ContractStatus_cell = "F" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, ContractStatus_cell, model.ContractStatusName);

                //项目日期
                if (project.BeginDate != null && project.BeginDate > new DateTime(1900, 1, 1))
                {
                    string begintime_cell = "G" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, begintime_cell, model.BeginDate.Value.ToString("yyyy-MM-dd"));
                }
                if (project.EndDate != null && project.EndDate > new DateTime(1900, 1, 1))
                {
                    string endtime_cell = "H" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, endtime_cell, model.EndDate.Value.ToString("yyyy-MM-dd"));
                }

                //项目类型
                if (model.ProjectTypeLevel2ID != null)
                {
                    ProjectTypeInfo typeModel = ProjectTypeManager.GetModel(model.ProjectTypeLevel2ID.Value);
                    if (typeModel != null)
                    {
                        string ProjectTypeCode_cell = "I" + (lineoffset + 1).ToString();
                        ExcelHandle.WriteCell(excel.CurSheet, ProjectTypeCode_cell, typeModel.ProjectTypeName);
                    }
                }
                //品牌
                string brand_cell = "J" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, brand_cell, model.Brands);

                //广告主账户ID
                string AD_cell = "K" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, AD_cell, model.AdvertiserID);

                //客户项目编号
                string customerProject_cell = "L" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, customerProject_cell, model.CustomerProjectCode);

                //客户名称
                if (project.Customer != null && !string.IsNullOrEmpty(project.Customer.FullNameCN))
                {
                    string CustomerName = project.Customer.FullNameCN;

                    string CustomerName_cell = "M" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, CustomerName_cell, CustomerName);
                }
                //项目名称
                string projectName_cell = "N" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, projectName_cell, model.BusinessDescription);

                //媒体付款主体
                string media_cell = "O" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, media_cell, project.MediaName);

                //项目总金额
                string TotalAmount_cell = "P" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, TotalAmount_cell, project.TotalAmount.Value.ToString("#,##0.00"));

                //媒体充值金额
                string rechargeAmount = project.Recharge == null ? string.Empty : project.Recharge.Value.ToString("#,##0.00");
                string rechargeAmount_cell = "Q" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, rechargeAmount_cell, rechargeAmount);

                //预估媒体成本比例
                string mediaCostRate = project.MediaCostRate == null ? string.Empty : project.MediaCostRate.Value * 100 + "%";
                string mediaCostRate_cell = "R" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, mediaCostRate_cell, mediaCostRate);

                //预估媒体成本
                string mediaCostRateAmount = project.MediaCost == null ? string.Empty : project.MediaCost.Value.ToString("#,##0.00");
                string mediaCostRateAmount_cell = "S" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, mediaCostRateAmount_cell, mediaCostRateAmount);

                //预估客户返点比例
                string customerRebateRate = project.CustomerRebateRate == null ? string.Empty : project.CustomerRebateRate.Value * 100 + "%";
                string customerRebateRate_cell = "T" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, customerRebateRate_cell, customerRebateRate);

                //预估客户返点
                string customerRebateAmount = project.CustomerRebate == null ? string.Empty : project.CustomerRebate.Value.ToString("#,##0.00");
                string customerRebateAmount_cell = "U" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, customerRebateAmount_cell, customerRebateAmount);

                //OOP
                string oopAmount = project.Expenses.Where(x => x.Description == "OOP").Sum(x => x.Expense).Value.ToString("#,##0.00");
                string oop_cell = "V" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, oop_cell, oopAmount);

                //外部成本
                string costAmount = project.CostDetails.Sum(x => x.Cost).Value.ToString("#,##0.00");
                string cost_cell = "W" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, cost_cell, costAmount);

                //成本合计 =SUM(V2:W2)
                string costTotal_cell = "X" + (lineoffset + 1).ToString();
                ExcelHandle.SetFormula(excel.CurSheet, costTotal_cell, "=SUM(V" + (lineoffset + 1).ToString() + ":W" + (lineoffset + 1).ToString() + ")");

                //合同服务费
                if (rateModel != null)
                {
                    decimal serviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel);
                    string serviceFee_cell = "Y" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, serviceFee_cell, serviceFee.ToString("#,##0.00"));

                    //项目毛利率
                    string ProfileRate = (serviceFee / Convert.ToDecimal(project.TotalAmount) * 100).ToString("#,##0.00");
                    string ProfileRate_cell = "Z" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, ProfileRate_cell, ProfileRate);

                }

                //应收-不含客返
                if (project.AccountsReceivable != null)
                {
                    string Receivable_cell = "AA" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, Receivable_cell, project.AccountsReceivable.Value.ToString("#,##0.00"));
                }

                //付款通知表内的信息
                int paymentOffset = lineoffset;
                var paymentList = project.Payments.Where(x => x.PaymentCode !=null && x.PaymentCode!=string.Empty);
                foreach (var payment in paymentList)
                {
                    //返点发票			
                    //开票日期
                    if (payment.RebateDate != null)
                    {
                        string RebateDate_cell = "AB" + (paymentOffset + 1).ToString();
                        ExcelHandle.WriteCell(excel.CurSheet, RebateDate_cell, payment.RebateDate.Value.ToString("yyyy-MM-dd"));
                    }
                    //发票金额
                    if (payment.RebateAmount != 0)
                    {
                        string RebateAmount_cell = "AC" + (paymentOffset + 1).ToString();
                        ExcelHandle.WriteCell(excel.CurSheet, RebateAmount_cell, payment.RebateAmount.ToString("#,##0.00"));
                    }
                    //发票号码
                    string RebateNo_cell = "AD" + (paymentOffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, RebateNo_cell, "'" + payment.RebateNo);
                    //发票类型
                    string RebateType_cell = "AE" + (paymentOffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, RebateType_cell, payment.RebateType);

                    //开票			
                    //开票方式
                    string InvoiceType_cell = "AF" + (paymentOffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, InvoiceType_cell, payment.InvoiceType);
                    //发票号码
                    string InvoiceNo_cell = "AG" + (paymentOffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, InvoiceNo_cell, "'"+payment.InvoiceNo);
                    //开票金额
                    if (payment.InvoiceAmount != 0)
                    {
                        string InvoiceAmount_cell = "AH" + (paymentOffset + 1).ToString();
                        ExcelHandle.WriteCell(excel.CurSheet, InvoiceAmount_cell, payment.InvoiceAmount.ToString("#,##0.00"));
                    }
                    //开票时间
                    if (payment.InvoiceDate != null)
                    {
                        string InvoiceDate_cell = "AI" + (paymentOffset + 1).ToString();
                        ExcelHandle.WriteCell(excel.CurSheet, InvoiceDate_cell, payment.InvoiceDate.Value.ToString("yyyy-MM-dd"));
                    }
                    //回款金额
                    if (payment.PaymentFee != 0)
                    {
                        string PaymentFee_cell = "AJ" + (paymentOffset + 1).ToString();
                        ExcelHandle.WriteCell(excel.CurSheet, PaymentFee_cell, payment.PaymentFee.ToString("#,##0.00"));
                    }
                    //回款时间
                    if (payment.PaymentFactDate != null)
                    {
                        string PaymentFactDate_cell = "AK" + (paymentOffset + 1).ToString();
                        ExcelHandle.WriteCell(excel.CurSheet, PaymentFactDate_cell, payment.PaymentFactDate.Value.ToString("yyyy-MM-dd"));
                    }
                    paymentOffset++;
                }

                //消耗信息
                for (int i = 0; i < monthCols.Count; i++)
                {
                    decimal consumptionMonth = project.Consumptions.Where(x => x.OrderYM == monthCols[i].key).Sum(x => x.Amount);
                    if (consumptionMonth != 0)
                        ExcelHandle.WriteCell(excel.CurSheet, monthCols[i].cellName + (lineoffset + 1).ToString(), "'" + consumptionMonth.ToString("#,##0.00"));
                }

                //计算下一个项目号的行数起点
                if (lineoffset == paymentOffset)
                    lineoffset++;
                else
                    lineoffset = paymentOffset;


            }

            string serverpath = Common.GetLocalPath("/Tmp/Project");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Project/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;

        }

        /// <summary>
        /// 列出起始时间到结束时间的按月分的列
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="stoptime"></param>
        /// <returns></returns>
        public static List<MonthCol> GetMonthsCell(DateTime starttime, DateTime stoptime)
        {
            List<MonthCol> monthCol = new List<MonthCol>();
            int offset = Utility.ExcelHandle.LtoN("AU");

            int TotalMonth = (stoptime.Year - starttime.Year - 1) * 12 + (stoptime.Month) + (12 - starttime.Month + 1);//月数总和
            if (TotalMonth <= 0 || offset - 1 + TotalMonth > 702) return null;//不能大于最大列宽

            for (int i = 0; i < TotalMonth; i++)
            {
                DateTime tmptime = starttime.AddMonths(i);
                MonthCol col = new MonthCol();
                col.key = tmptime.ToString("Y", System.Globalization.CultureInfo.CreateSpecificCulture("zh-CN"));//转换为报表中的日期格式
                col.cellName = Utility.ExcelHandle.NtoL(offset + i);
                monthCol.Add(col);
            }
            return monthCol;
        }

        #endregion

        /// <summary>
        /// 根据项目号获得对象
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public static ESP.Finance.Entity.ProjectInfo GetModelByProjectCode(string projectCode)
        {
            return DataProvider.GetModelByProjectCode(projectCode);
        }

        public static DataTable GetProjectListJoinHist(int userId)
        {
            return DataProvider.GetProjectListJoinHist(userId);
        }

        public static int changeApplicant(string projectIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            return DataProvider.changeApplicant(projectIds, oldUserId, newUserId, currentUser);
        }

        public static int changAuditor(string projectIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            return DataProvider.changAuditor(projectIds, oldUserId, newUserId, currentUser);
        }

        /// <summary>
        /// 提交撤销变更过的项目号
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="CurrentUser"></param>
        /// <returns></returns>
        public static UpdateResult CommitChangedProject(int projectId, ESP.Compatible.Employee CurrentUser)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ProjectInfo p = ProjectManager.GetModelWithOutDetailList(projectId, trans);
                    //记录提交日志
                    ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                    log.AuditDate = DateTime.Now;
                    log.AuditorEmployeeName = CurrentUser.Name;
                    log.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                    log.AuditorUserCode = CurrentUser.ID;
                    log.AuditorUserName = CurrentUser.ITCode;
                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.CommitChangedProject;
                    log.FormID = p.ProjectId;
                    log.FormType = (int)ESP.Finance.Utility.FormType.Project;
                    log.Suggestion = "提交项目号";
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);

                    p.Status = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
                    UpdateResult result = ProjectManager.Update(p);
                    trans.Commit();
                    return result;
                }
                catch
                {
                    trans.Rollback();
                    return UpdateResult.Failed;
                }
            }

        }

        /// <summary>
        /// 撤销项目
        /// </summary>
        /// <param name="projectModel"></param>
        /// <param name="CurrentUser"></param>
        /// <returns></returns>
        public static bool CancelProject(ProjectInfo projectModel, ESP.Compatible.Employee CurrentUser)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //变更项目号状态为保存
                    projectModel.Status = (int)ESP.Finance.Utility.Status.Saved;
                    ESP.Finance.BusinessLogic.ProjectManager.Update(projectModel, trans);
                    //记录撤销日志
                    ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                    log.AuditDate = DateTime.Now;
                    log.AuditorEmployeeName = CurrentUser.Name;
                    log.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                    log.AuditorUserCode = CurrentUser.ID;
                    log.AuditorUserName = CurrentUser.ITCode;
                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.RequestorCancel;
                    log.FormID = projectModel.ProjectId;
                    log.FormType = (int)ESP.Finance.Utility.FormType.Project;
                    log.Suggestion = "撤销项目号";
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);
                    //删除审核人信息
                    new ESP.Finance.DataAccess.AuditHistoryDataProvider().DeleteByProjectId(projectModel.ProjectId, trans);
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 是否需要审核
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public static bool isNeedAudit(int ProjectID)
        {
            IList<ContractInfo> contractList = ContractManager.GetList(" ProjectID=" + ProjectID.ToString() + " and isDelay=1");

            List<SqlParameter> paramList = new List<SqlParameter>();
            string term = " ProjectID=@ProjectID and Status=@Status";
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = ProjectID;
            paramList.Add(p2);
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
            paramList.Add(p1);
            IList<ProjectHistInfo> projectList = ESP.Finance.BusinessLogic.ProjectHistManager.GetList(term, paramList);
            if (projectList == null || projectList.Count == 0)
                return true;
            projectList = projectList.OrderByDescending(N => N.SubmitDate).ToList();

            ProjectInfo newProject = GetModel(ProjectID);
            ProjectInfo oldProject = projectList[0].ProjectModel.ObjectDeserialize<ProjectInfo>();
            //合同个数变化projectmodel.Contracts.Where(x => x.Status == null).ToList()

            if (newProject.Contracts.Where(x => x.Status == null && x.ContractType == null).Count() != oldProject.Contracts.Where(x => x.Status == null && x.ContractType == null).Count())
            {
                return true;
            }
            //项目负责人
            if (newProject.ApplicantUserID != oldProject.ApplicantUserID)
                return true;
            //业务组
            if (newProject.GroupID != oldProject.GroupID)
                return true;
            //总成本
            decimal newCost = ContractCostManager.GetTotalAmountByProject(newProject.ProjectId);
            newCost += ProjectExpenseManager.GetTotalExpense(newProject.ProjectId);
            newCost += SupporterManager.GetTotalAmountByProject(newProject.ProjectId);

            decimal oldCost = oldProject.CostDetails.Sum(x => x.Cost.Value);
            oldCost += oldProject.Expenses.Sum(x => x.Expense.Value);
            oldCost += oldProject.Supporters.Sum(x => x.BudgetAllocation.Value);

            if (newCost != oldCost)
                return true;
            //项目总金额
            if (newProject.TotalAmount != oldProject.TotalAmount)
                return true;
            //延期合同
            if (contractList != null && contractList.Count > 0)
                return true;

            return false;
        }

        public static List<ESP.Framework.Entity.TaskItemInfo> GetTaskItems(string userIds)
        {
            return DataProvider.GetTaskItems(userIds);
        }

        public static IList<ProjectInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            return DataProvider.GetWaitAuditList(userIds, strTerms, parms);
        }

        public static IList<ProjectInfo> GetWaitAuditList(int[] userIds)
        {
            return DataProvider.GetWaitAuditList(userIds);
        }

        public static string ExportCostMonitor(DataTable dt, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/ExpenseAccount/CostMonitor.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];

            int rownum = 2;
            string cell = string.Empty;

            string state = string.Empty, prtype = string.Empty;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                state = ESP.Purchase.Common.State.requistionOrorder_state[int.Parse(dt.Rows[i]["status"].ToString())];
                int v = int.Parse(dt.Rows[i]["prtype"].ToString());

                switch (v)
                {
                    case 0:
                        prtype = "对公申请";
                        break;
                    case 1:
                        prtype = "稿费申请";
                        break;
                    case 6:
                        prtype = "对私申请";
                        break;
                    case 7:
                        prtype = "媒体合作";
                        break;
                    case 98:
                        prtype = "广告采买";
                        break;
                    default:
                        prtype = "对公申请";
                        break;
                }

                //PR流水
                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["id"].ToString());
                //PR No.
                cell = string.Format("B{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["prno"].ToString());
                //项目号
                cell = string.Format("C{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["project_code"].ToString());
                //申请人
                cell = string.Format("D{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["requestorname"].ToString());
                //收货人
                cell = string.Format("E{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["receivername"].ToString());
                //附加收货人
                cell = string.Format("F{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["appendreceivername"].ToString());
                //项目负责人
                cell = string.Format("G{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["applicantemployeename"].ToString());
                //成本所属组
                cell = string.Format("H{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["dept"].ToString());
                //项目描述
                cell = string.Format("I{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["businessdescription"].ToString());
                //供应商
                cell = string.Format("J{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["supplier_name"].ToString());
                //费用描述
                cell = string.Format("K{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["itemno"].ToString());
                //总额
                cell = string.Format("L{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["totalprice"].ToString());
                //申请日期
                cell = string.Format("M{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["app_date"].ToString());
                //单据类型
                cell = string.Format("N{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, prtype);
                //单据状态
                cell = string.Format("O{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, state);
                rownum++;
            }

            string serverpath = Common.GetLocalPath("/Tmp/ExpenseAccount");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/ExpenseAccount/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        public static IList<ESP.Purchase.Entity.V_GetProjectList> GetCurrentProject(int userid, string strWhere)
        {
            StringBuilder strTerms = new StringBuilder();
            StringBuilder strTerms2 = new StringBuilder();
            List<SqlParameter> parms = new List<SqlParameter>();
            int[] depts = new ESP.Compatible.Employee(userid).GetDepartmentIDs();
            ESP.Compatible.Employee CurrentUser = new ESP.Compatible.Employee(userid);

            strTerms.Append(" and status <> @status and ((projectcode is not null) and projectcode <> '')");
            parms.Add(new SqlParameter("@status", 34));

            strTerms2.Append(" and status <> 34  and ((projectcode is not null) and projectcode <> '')");// + ESP.Purchase.Common.State.projectstatus_ok);

            if (!OperationAuditManageManager.GetCurrentUserIsManager(userid.ToString())
                && !OperationAuditManageManager.GetCurrentUserIsDirector(userid.ToString())
                && !(ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAdministrativeIDs"].IndexOf("," + depts[0] + ",") >= 0)
                && !(ESP.Configuration.ConfigurationManager.SafeAppSettings["SpecialDeptIDs"].IndexOf("," + depts[0] + ",") >= 0)
                )
            {
                strTerms.Append(" and memberUserID = @userId");
                parms.Add(new SqlParameter("@userId", userid));
            }

            if (!string.IsNullOrEmpty(strWhere))
            {
                strTerms.Append(strWhere);
                strTerms2.Append(strWhere);
            }

            List<ESP.Purchase.Entity.V_GetProjectList> list = null;
            //添加分公司GM项目号
            IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetAllList();


            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAdministrativeIDs"].IndexOf("," + depts[0] + ",") >= 0 || ESP.Configuration.ConfigurationManager.SafeAppSettings["SpecialDeptIDs"].IndexOf("," + depts[0] + ",") >= 0)
            {
                list = ESP.Purchase.BusinessLogic.V_GetProjectList.GetList(strTerms.ToString(), parms);
                var q = (from project in list
                         select new ESP.Purchase.Entity.V_GetProjectList
                         {
                             ProjectId = project.ProjectId,
                             ProjectCode = project.ProjectCode,
                             SerialCode = project.SerialCode,
                             SubmitDate = project.SubmitDate,
                             GroupID = project.GroupID,
                             BusinessDescription = project.BusinessDescription
                         }).Distinct(new V_GetProjectListComparer());

                list = q.ToList();
            }
            else if (OperationAuditManageManager.GetCurrentUserIsManager(userid.ToString()))
            {
                list = ESP.Purchase.BusinessLogic.V_GetProjectList.GetProjectListByManager(userid, strTerms2.ToString());

            }
            else if (OperationAuditManageManager.GetCurrentUserIsDirector(userid.ToString()))
            {
                list = ESP.Purchase.BusinessLogic.V_GetProjectList.GetProjectListByDirector(userid, strTerms2.ToString());
            }
            else
            {
                list = ESP.Purchase.BusinessLogic.V_GetProjectList.GetList(strTerms.ToString(), parms);
            }
            int deptid = CurrentUser.GetDepartmentIDs()[0];
            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAdministrativeIDs"].IndexOf("," + deptid.ToString() + ",") >= 0)
            {
                foreach (ESP.Finance.Entity.BranchInfo branch in branchList)
                {
                    ESP.Purchase.Entity.V_GetProjectList newProject = new ESP.Purchase.Entity.V_GetProjectList();
                    newProject.ProjectCode = branch.BranchCode + "-" + "GM*-*-" + DateTime.Now.Year.ToString().Substring(2) + DateTime.Now.Month.ToString("00") + "001";
                    newProject.SubmitDate = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-01");
                    list.Add(newProject);
                }
            }

            return list;
        }

        public static string ExportOriginalDataCost(DataTable dt, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/ExpenseAccount/OriginalDataCostStructure.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            string[] ExcelColumn = new string[260] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                                     "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
                                                     "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
                                                     "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ",
                                                     "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM", "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ",
                                                     "EA", "EB", "EC", "ED", "EE", "EF", "EG", "EH", "EI", "EJ", "EK", "EL", "EM", "EN", "EO", "EP", "EQ", "ER", "ES", "ET", "EU", "EV", "EW", "EX", "EY", "EZ",
                                                     "FA", "FB", "FC", "FD", "FE", "FF", "FG", "FH", "FI", "FJ", "FK", "FL", "FM", "FN", "FO", "FP", "FQ", "FR", "FS", "FT", "FU", "FV", "FW", "FX", "FY", "FZ",
                                                     "GA", "GB", "GC", "GD", "GE", "GF", "GG", "GH", "GI", "GJ", "GK", "GL", "GM", "GN", "GO", "GP", "GQ", "GR", "GS", "GT", "GU", "GV", "GW", "GX", "GY", "GZ",
                                                     "HA", "HB", "HC", "HD", "HE", "HF", "HG", "HH", "HI", "HJ", "HK", "HL", "HM", "HN", "HO", "HP", "HQ", "HR", "HS", "HT", "HU", "HV", "HW", "HX", "HY", "HZ",
                                                     "IA", "IB", "IC", "ID", "IE", "IF", "IG", "IH", "II", "IJ", "IK", "IL", "IM", "IN", "IO", "IP", "IQ", "IR", "IS", "IT", "IU", "IV", "IW", "IX", "IY", "IZ"
            };

            List<ESP.Purchase.Entity.TypeInfo> typelist = ESP.Purchase.BusinessLogic.TypeManager.GetModelList(" and typelevel=2 ");
            DataTable deptlist = ESP.Finance.BusinessLogic.DepartmentViewManager.GetList();
            int RowNum = 3;
            int excelColumnTitleNum = 1;
            int excelColumnDeptNum = 1;
            string cell = string.Empty;

            int deptid = 0;

            cell = string.Format("A{0}", 2);
            ExcelHandle.WriteCell(excel.CurSheet, cell, "物料类别");

            //	预计成本	实际成本	成本差额
            for (int i = 0; i < deptlist.Rows.Count; i++)
            {

                cell = string.Format(ExcelColumn[excelColumnDeptNum] + "{0}", 1);
                ExcelHandle.WriteCell(excel.CurSheet, cell, deptlist.Rows[i]["deptname"].ToString());

                excelColumnDeptNum += 3;

                cell = string.Format(ExcelColumn[excelColumnTitleNum] + "{0}", 2);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "预计成本");
                excelColumnTitleNum++;
                cell = string.Format(ExcelColumn[excelColumnTitleNum] + "{0}", 2);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "实际成本");
                excelColumnTitleNum++;
                cell = string.Format(ExcelColumn[excelColumnTitleNum] + "{0}", 2);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "成本差额");
                excelColumnTitleNum++;
            }

            excelColumnTitleNum = 1;

            foreach (ESP.Purchase.Entity.TypeInfo type in typelist)
            {
                //物料类别
                cell = string.Format("A{0}", RowNum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, type.typename);

                for (int i = 0; i < deptlist.Rows.Count; i++)
                {
                    deptid = int.Parse(deptlist.Rows[i]["level3id"].ToString());

                    //预计成本	实际成本	成本差额
                    DataRow[] dr = dt.Select("level3id=" + deptid.ToString() + " and costtypeid=" + type.typeid.ToString());
                    if (dr != null && dr.Count() > 0)
                    {
                        decimal cost = dr[0]["cost"] == System.DBNull.Value ? 0 : decimal.Parse(dr[0]["cost"].ToString());
                        decimal cost1 = dr[0]["cost1"] == System.DBNull.Value ? 0 : decimal.Parse(dr[0]["cost1"].ToString());
                        decimal cost2 = dr[0]["cost2"] == System.DBNull.Value ? 0 : decimal.Parse(dr[0]["cost2"].ToString());
                        decimal cost3 = dr[0]["cost3"] == System.DBNull.Value ? 0 : decimal.Parse(dr[0]["cost3"].ToString());
                        decimal cost4 = dr[0]["cost4"] == System.DBNull.Value ? 0 : decimal.Parse(dr[0]["cost4"].ToString());
                        decimal cost5 = dr[0]["cost5"] == System.DBNull.Value ? 0 : decimal.Parse(dr[0]["cost5"].ToString());
                        decimal cost6 = dr[0]["cost6"] == System.DBNull.Value ? 0 : decimal.Parse(dr[0]["cost6"].ToString());

                        decimal usedTotal = cost1 + cost2 + cost3 + cost4 + cost5 + cost6;
                        decimal differ = cost - usedTotal;
                        cell = string.Format(ExcelColumn[excelColumnTitleNum] + "{0}", RowNum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + cost.ToString("#,##0.00"));
                        excelColumnTitleNum++;

                        cell = string.Format(ExcelColumn[excelColumnTitleNum] + "{0}", RowNum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + usedTotal.ToString("#,##0.00"));
                        excelColumnTitleNum++;


                        cell = string.Format(ExcelColumn[excelColumnTitleNum] + "{0}", RowNum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + differ.ToString("#,##0.00"));
                        excelColumnTitleNum++;

                    }
                    else
                    {
                        excelColumnTitleNum += 3;
                    }
                }
                RowNum++;
                excelColumnTitleNum = 1;
            }

            string serverpath = Common.GetLocalPath("/Tmp/ExpenseAccount");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/ExpenseAccount/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        public static DataTable GetMaterialCost(string begindate, string enddate)
        {
            return DataProvider.GetMaterialCost(begindate, enddate);
        }

        /// <summary>
        /// 待审核证据链的项目列表
        /// </summary>
        /// <returns></returns>
        public static IList<ProjectInfo> GetContractAuditingProjectList()
        {
            return DataProvider.GetContractAuditingProjectList();
        }

        /// <summary>
        /// 待审核发票申请的项目列表
        /// </summary>
        /// <returns></returns>
        public static IList<ProjectInfo> GetApplyForInvioceAuditingProjectList()
        {
            return DataProvider.GetApplyForInvioceAuditingProjectList();
        }

    }

    class V_GetProjectListComparer : IEqualityComparer<ESP.Purchase.Entity.V_GetProjectList>
    {
        #region IEqualityComparer<V_GetProjectList> 成员

        public bool Equals(ESP.Purchase.Entity.V_GetProjectList x, ESP.Purchase.Entity.V_GetProjectList y)
        {
            return x.ProjectId == y.ProjectId
                && x.ProjectCode == y.ProjectCode
                && x.SerialCode == y.SerialCode
                && x.SubmitDate == y.SubmitDate
                && x.GroupID == y.GroupID
                && x.BusinessDescription == y.BusinessDescription;
        }

        public int GetHashCode(ESP.Purchase.Entity.V_GetProjectList obj)
        {
            return obj.ProjectId.GetHashCode();
        }

        #endregion
    }
}

