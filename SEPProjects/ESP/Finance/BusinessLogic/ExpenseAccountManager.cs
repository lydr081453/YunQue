using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ExpenseBLL 的摘要说明。
    /// </summary>


    public static class ExpenseAccountManager
    {

        private static ESP.Finance.IDataAccess.IExpenseAccountProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IExpenseAccountProvider>.Instance; } }


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ExpenseAccountInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ExpenseAccountInfo model)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
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
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int id)
        {

            int res = 0;
            try
            {
                res = DataProvider.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ExpenseAccountInfo GetModel(int id)
        {

            return DataProvider.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ExpenseAccountInfo GetModelByReturnID(int ReturnID)
        {
            return DataProvider.GetModelByReturnID(ReturnID);
        }

        public static ESP.Finance.Entity.ExpenseAccountExtendsInfo GetWorkItemModel(int id)
        {
            return DataProvider.GetWorkItemModel(id);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAccountInfo> GetAllList()
        {
            return DataProvider.GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAccountInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }

        /// <summary>
        /// 单笔金额是否大于2000
        /// </summary>
        /// <param name="projectCode"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool ExpenseMoneyGreaterThan2000(int expenseAccountId)
        {
            return DataProvider.ExpenseMoneyGreaterThan2000(expenseAccountId);
        }

        /// <summary>
        /// 获得当前登录人的收货WorkFlowItems
        /// </summary>
        /// <param name="curUserid"></param>
        /// <param name="definition"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<ESP.Finance.Entity.ExpenseAccountExtendsInfo> GetWorkFlowItemsByRecive(int curUserid, string definition, int status)
        {
            string definition1 = "/Workflows/Reimbursement.xpdl/Reimbursement";
            string definition2 = "/Workflows/Reimbursement.xpdl/Loan";

            ESP.Finance.SqlDataAccess.WorkFlowDataContext dc = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
            var q = from wi in dc.WorkItem
                    join wf in dc.WorkflowInstance on wi.WorkflowInstanceId equals wf.InstanceId
                    join wa in dc.WorkItemAssignee on wi.WorkItemId equals wa.WorkItemId
                    where wa.AssigneeId == curUserid
                        && (wf.Definition == definition1 || wf.Definition == definition2)
                        && wi.WorkItemName == "收货"
                        && wi.Status == status
                    select wi;
            List<ESP.Finance.SqlDataAccess.WorkItem> workitems = q.ToList<ESP.Finance.SqlDataAccess.WorkItem>();
            List<ESP.Finance.Entity.ExpenseAccountExtendsInfo> list = new List<ESP.Finance.Entity.ExpenseAccountExtendsInfo>();
            foreach (ESP.Finance.SqlDataAccess.WorkItem workitem in workitems)
            {
                ExpenseAccountExtendsInfo extendInfo = DataProvider.GetWorkItemModel(workitem.EntityId);
                extendInfo.WorkItemID = workitem.WorkItemId;
                extendInfo.WorkItemName = workitem.WorkItemName;
                extendInfo.WebPage = workitem.WebPage;
                extendInfo.ParticipantName = workitem.ParticipantName;
                list.Add(extendInfo);
            }
            return list;
        }

        /// <summary>
        /// 获得当前登录人的是否需要收货
        /// </summary>
        /// <param name="curUserid"></param>
        /// <param name="definition"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static ESP.Finance.SqlDataAccess.WorkItem GetWorkflowIsConfirm(int curUserid, int returnId, int status, ESP.Finance.SqlDataAccess.WorkFlowDataContext dc)
        {
            string definition = "/Workflows/Reimbursement.xpdl/Loan";

            var q = from wi in dc.WorkItem
                    join wf in dc.WorkflowInstance on wi.WorkflowInstanceId equals wf.InstanceId
                    join wa in dc.WorkItemAssignee on wi.WorkItemId equals wa.WorkItemId
                    where wa.AssigneeId == curUserid
                        && wf.Definition == definition
                        && wi.WorkItemName == "收货"
                        && wi.Status == status
                        && wi.EntityId == returnId
                    select wi;
            List<ESP.Finance.SqlDataAccess.WorkItem> workitems = q.ToList<ESP.Finance.SqlDataAccess.WorkItem>();

            if (workitems.Count > 0)
            {
                return workitems[0];
            }
            return null;
        }

        /// <summary>
        /// 获得此单据的审批人ID
        /// </summary>
        /// <param name="curUserid"></param>
        /// <param name="definition"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int GetAssigneeIdByWorkItemID(int workItemID, ESP.Finance.SqlDataAccess.WorkFlowDataContext dc)
        {

            var q = from wa in dc.WorkItemAssignee
                    where wa.WorkItemId == workItemID
                    select wa;

            List<ESP.Finance.SqlDataAccess.WorkItemAssignee> workitemAssignee = q.ToList<ESP.Finance.SqlDataAccess.WorkItemAssignee>();
            int assigneeID = 0;
            if (workitemAssignee.Count > 0)
            {
                assigneeID = workitemAssignee[0].AssigneeId;
            }

            return assigneeID;
        }

        /// <summary>
        /// 获得当前审批人
        /// </summary>
        /// <param name="curUserid"></param>
        /// <param name="definition"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetAssigneeNameByWorkItemID(int returnId, int status, ESP.Finance.SqlDataAccess.WorkFlowDataContext dc)
        {
            string definition1 = "/Workflows/Reimbursement.xpdl/Reimbursement";
            string definition2 = "/Workflows/Reimbursement.xpdl/Loan";
            string definition3 = "/Workflows/Reimbursement.xpdl/Charge";
            var q = from wi in dc.WorkItem
                    join wf in dc.WorkflowInstance on wi.WorkflowInstanceId equals wf.InstanceId
                    join wa in dc.WorkItemAssignee on wi.WorkItemId equals wa.WorkItemId
                    where wi.Status == status
                        && (wf.Definition == definition1 || wf.Definition == definition2 || wf.Definition == definition3)
                        && wi.EntityId == returnId
                    select wa;
            List<ESP.Finance.SqlDataAccess.WorkItemAssignee> workitemAssignee = q.ToList<ESP.Finance.SqlDataAccess.WorkItemAssignee>();
            string assigneeName = "";
            if (workitemAssignee.Count > 0)
            {
                assigneeName = new ESP.Compatible.Employee(workitemAssignee[0].AssigneeId).Name;
            }

            return assigneeName;
        }

        /// <summary>
        /// 获得当前登录人的活动WorkFlowItems
        /// </summary>
        /// <param name="curUserid"></param>
        /// <param name="definition"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<ESP.Finance.Entity.ExpenseAccountExtendsInfo> GetWorkFlowItems(int curUserid, string definition, int status)
        {
            string definition1 = "/Workflows/Reimbursement.xpdl/Reimbursement";
            string definition2 = "/Workflows/Reimbursement.xpdl/Loan";

            ESP.Finance.SqlDataAccess.WorkFlowDataContext dc = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
            var q = from wi in dc.WorkItem
                    join wf in dc.WorkflowInstance on wi.WorkflowInstanceId equals wf.InstanceId
                    join wa in dc.WorkItemAssignee on wi.WorkItemId equals wa.WorkItemId
                    where wa.AssigneeId == curUserid
                        && (wf.Definition == definition1 || wf.Definition == definition2)
                        && wi.WorkItemName != "收货"
                        && wi.Status == status
                    select wi;
            List<ESP.Finance.SqlDataAccess.WorkItem> workitems = q.ToList<ESP.Finance.SqlDataAccess.WorkItem>();
            List<ESP.Finance.Entity.ExpenseAccountExtendsInfo> list = new List<ESP.Finance.Entity.ExpenseAccountExtendsInfo>();
            foreach (ESP.Finance.SqlDataAccess.WorkItem workitem in workitems)
            {
                ExpenseAccountExtendsInfo extendInfo = DataProvider.GetWorkItemModel(workitem.EntityId);
                extendInfo.WorkItemID = workitem.WorkItemId;
                extendInfo.WorkItemName = workitem.WorkItemName;
                extendInfo.WebPage = workitem.WebPage;
                extendInfo.ParticipantName = workitem.ParticipantName;
                list.Add(extendInfo);
            }
            return list;
        }

        public static DataSet GetMajorAuditList(string whereStr, string WhereStr2)
        {
            return DataProvider.GetMajorAuditList(whereStr, WhereStr2);
        }

        public static DataSet GetMajorAuditListByBatch(string whereStr)
        {
            return DataProvider.GetMajorAuditListByBatch(whereStr);
        }

        public static DataSet GetExpenseOrderView(string whereStr)
        {
            return DataProvider.GetExpenseOrderView(whereStr);
        }

        public static DataSet GetMajorAlreadyAuditList(string whereStr)
        {
            return DataProvider.GetMajorAlreadyAuditList(whereStr);
        }

        /// <summary>
        /// 获得工作流状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static int GetWorkFlowInstanceStatus(Guid instanceId)
        {
            ESP.Finance.SqlDataAccess.WorkFlowDataContext dc = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
            var q = from wf in dc.WorkflowInstance
                    where wf.InstanceId == instanceId
                    select wf;
            int status = 0;
            try
            {
                status = q.ToList<ESP.Finance.SqlDataAccess.WorkflowInstance>()[0].Status;
            }
            catch { }
            return status;
        }

        public static ReturnInfo GetWorkFlowLastAuditPassTime(int returnID, ESP.Compatible.Employee currentEmp)
        {
            ESP.Finance.SqlDataAccess.WorkFlowDataContext dc = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
            var q = from wi in dc.WorkItem
                    where wi.EntityId == returnID
                    && wi.Status == (int)ESP.Workflow.WorkItemStatus.Open
                    select wi;

            ESP.Finance.SqlDataAccess.WorkItem workItem = null;
            ESP.Finance.Entity.ReturnInfo returnModel = null;




            try
            {
                workItem = q.ToList<ESP.Finance.SqlDataAccess.WorkItem>()[0];
            }
            catch { }

            if (workItem != null)
            {
                returnModel = ReturnManager.GetModel(returnID);
                int[] depts = new ESP.Compatible.Employee(returnModel.RequestorID.Value).GetDepartmentIDs();
                //申请人是否行政人员
                bool isAdministrative = false;
                if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AdministrativeIDs"].IndexOf("," + depts[0] + ",") >= 0)
                {
                    isAdministrative = true;
                }
                //是否GM项目号
                bool isGm = false;
                if (returnModel.ProjectCode.IndexOf("GM*") >= 0 || returnModel.ProjectCode.IndexOf("*GM") >= 0 || returnModel.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
                {
                    isGm = true;
                }


                if (workItem.ParticipantName.Equals("FA"))
                {
                    if (returnModel.LastAuditPassTime == null)
                    {
                        returnModel.LastAuditPassTime = workItem.CreatedTime;
                        returnModel.LastAuditUserID = currentEmp.IntID;
                        returnModel.LastAuditUserName = currentEmp.Name;
                        return returnModel;
                    }
                }
                else if (workItem.ParticipantName.Equals("财务第一级"))
                {
                    if (returnModel.LastAuditPassTime == null)
                    {
                        returnModel.LastAuditPassTime = workItem.CreatedTime;
                        returnModel.LastAuditUserID = currentEmp.IntID;
                        returnModel.LastAuditUserName = currentEmp.Name;
                    }
                    //不是GM项目号  或者是GM项目号但申请人是行政人员  则需要记录FA审核通过时间
                    if (!isGm || (isAdministrative && isGm))
                    {
                        returnModel.FaAuditPassTime = workItem.CreatedTime;
                        returnModel.FaAuditUserID = currentEmp.IntID;
                        returnModel.FaAuditUserName = currentEmp.Name;
                    }


                    return returnModel;
                }

            }

            return null;
        }

        /// <summary>
        /// 获得工作流Model
        /// </summary>
        /// <param name="workItemId"></param>
        /// <returns></returns>
        public static ESP.Finance.SqlDataAccess.WorkItem GetWorkFlowModel(int workItemId)
        {
            ESP.Finance.SqlDataAccess.WorkFlowDataContext dc = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
            var q = from wi in dc.WorkItem where wi.WorkItemId == workItemId select wi;
            return q.ToList<ESP.Finance.SqlDataAccess.WorkItem>()[0];
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public static void UpdateStatus(Object o)
        {
            //int id, int status, ESP.Finance.Entity.AuditLogInfo logModel
            Dictionary<string, object> dictionary = (Dictionary<string, object>)o;
            int id = (int)dictionary["EntID"];
            int status = (int)dictionary["ReturnStatus"];

            ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = (ESP.Finance.Entity.ExpenseAuditDetailInfo)dictionary["LogModel"];

            ESP.Finance.Entity.ReturnInfo returnInfo = ESP.Finance.BusinessLogic.ReturnManager.GetModel(id);
            returnInfo.ReturnStatus = status;

            AuditLogInfo loginfo = new AuditLogInfo();
            loginfo.FormID = logModel.ExpenseAuditID;
            loginfo.FormType = (int)ESP.Finance.Utility.FormType.ExpenseAccount;
            loginfo.AuditorSysID = logModel.AuditorUserID;
            loginfo.AuditorUserName = logModel.AuditorUserName;
            loginfo.AuditorUserCode = logModel.AuditorUserCode;
            loginfo.AuditorEmployeeName = logModel.AuditorEmployeeName;
            loginfo.Suggestion = logModel.Suggestion;
            loginfo.AuditDate = logModel.AuditeDate;
            loginfo.AuditStatus = logModel.AuditeStatus;
          

            try
            {
                if (dictionary.ContainsKey("FactFee") && dictionary["FactFee"] != null)
                {
                    decimal factFee = Convert.ToDecimal(dictionary["FactFee"]);
                    returnInfo.FactFee = factFee;
                }

            }
            catch { }

            try
            {
                if (dictionary.ContainsKey("ReciveModel") && dictionary["ReciveModel"] != null)
                {
                    ESP.Finance.Entity.ExpenseAccountInfo recive = (ESP.Finance.Entity.ExpenseAccountInfo)dictionary["ReciveModel"];
                    ESP.Finance.BusinessLogic.ExpenseAccountManager.Add(recive);
                }
            }
            catch { }


            try
            {
                if (returnInfo.CommitDate == null)
                {
                    if (dictionary.ContainsKey("SubmitDate") && dictionary["SubmitDate"] != null)
                    {
                        returnInfo.CommitDate = (DateTime)dictionary["SubmitDate"];
                    }
                }
            }
            catch { }


            try
            {
                if (dictionary.ContainsKey("PaymentTypeCode") && dictionary["PaymentTypeCode"] != null)
                {
                    returnInfo.PaymentTypeCode = (string)dictionary["PaymentTypeCode"];
                }
            }
            catch { }
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnInfo);
            ESP.Finance.BusinessLogic.AuditLogManager.Add(loginfo);
            ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(logModel);
        }

        private static int GetTopDeptId()
        {
            int userId = ESP.Finance.Utility.Common.CurrentUserSysID;
            IList<ESP.Compatible.Department> dtdep = ESP.Compatible.Employee.GetDepartments(userId);
            if (dtdep.Count > 0)
            {
                string level = dtdep[0].Level.ToString();
                if (level == "1")
                {
                    return dtdep[0].UniqID;
                }
                else if (level == "2")
                {
                    ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                    return dep.ParentID;

                }
                else if (level == "3")
                {
                    ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                    ESP.Compatible.Department dep2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dep.Parent.UniqID);
                    return dep2.ParentID;
                }
            }
            return 0;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public static void UpdateStatus(int id, int status, ESP.Finance.Entity.AuditLogInfo logModel)
        {
            ESP.Finance.Entity.ReturnInfo returnInfo = ESP.Finance.BusinessLogic.ReturnManager.GetModel(id);
            returnInfo.ReturnStatus = status;
            if (returnInfo.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket && returnInfo.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit)
            {
                returnInfo.FactFee = returnInfo.PreFee;
            }
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnInfo);
            if (logModel != null)
            {
                ESP.Finance.BusinessLogic.AuditLogManager.Add(logModel);
            }
        }


        public static void UpdateStatus(int id, string checkNo, int status, ESP.Finance.Entity.AuditLogInfo logModel)
        {
            ESP.Finance.Entity.ReturnInfo returnInfo = ESP.Finance.BusinessLogic.ReturnManager.GetModel(id);
            returnInfo.ReturnStatus = status;
            returnInfo.PaymentTypeCode = checkNo;
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnInfo);
            if (logModel != null)
            {
                ESP.Finance.BusinessLogic.AuditLogManager.Add(logModel);
            }
        }
        /// <summary>
        /// 驳回到财务第一级
        /// </summary>
        /// <param name="instanceId">工作流实体ID</param>
        /// <param name="returnId">媒体预付单ID</param>
        /// <returns></returns>
        public static int WorkFlowRejectToFinance1(Guid instanceId, int returnId)
        {
            return DataProvider.WorkFlowRejectToFinance1(instanceId, returnId);
        }
        /// <summary>
        /// 驳回到申请人
        /// </summary>
        /// <param name="instanceId">工作流实体ID</param>
        /// <param name="returnId">媒体预付单ID</param>
        /// <returns></returns>
        public static int WorkFlowReject(Guid instanceId, int returnId)
        {
            return DataProvider.WorkFlowReject(instanceId, returnId);
        }
        /// <summary>
        /// 媒体预付走报销流程，财务第四级审批通过。工作流结束
        /// </summary>
        /// <param name="instanceId">工作流实体ID</param>
        /// <returns>媒体预付单ID</returns>
        public static int CloseWorkFlow(Guid instanceId, int operatorId)
        {
            return DataProvider.CloseWorkFlow(instanceId, operatorId);
        }

        /// <summary>
        /// 验证金额
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        //public static bool ValidateMoney(ReturnInfo returnModel,decimal cost,decimal oop)
        //{
        //    if (returnModel.ProjectID !=null && returnModel.ProjectID > 0)
        //    {
        //        if (ESP.ITIL.BusinessLogic.报销单验证规则.第三方报销单提交验证(returnModel, cost))
        //        {
        //            return ESP.ITIL.BusinessLogic.报销单验证规则.OPP报销单提交验证(returnModel, oop);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        public static bool BatchAuditExpense(List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList, List<int> nextAuditerList, string batchNo)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo in auditList)
                    {

                        Dictionary<string, object> nextAuditer = null;
                        auditInfo.Prarms.Add("PaymentTypeCode", batchNo);

                        if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && (auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0 || auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0))
                        {
                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0)
                            {
                                nextAuditer = new Dictionary<string, object>() 
                                { 
                                    { "Finance3Id" ,  nextAuditerList.ToArray() }
                                };
                            }

                            //if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0)
                            //{
                            //    nextAuditer = new Dictionary<string, object>() 
                            //    { 
                            //        { "Finance3Id" ,  nextAuditerList.ToArray() }
                            //    };
                            //}

                            //调用工作流
                            ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", nextAuditer, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);

                            //如果审批结束，则改变单据状态为审批结束
                            if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(auditInfo.Workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
                            {
                                ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(auditInfo.Model.ReturnID, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, null);
                            }
                        }
                        else
                        {
                            //如果是财务第三级审批现金借款单，则把实际收货金额写入冲销金额； 
                            //如果是财务第三级审批报销单，则把预计报销总金额写入实际报销金额； 
                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f3") > 0)
                            {

                                if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount || auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                                {
                                    auditInfo.Prarms.Add("FactFee", auditInfo.Model.PreFee.Value);
                                }
                            }
                            //调用工作流
                            try
                            {
                                ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);
                            }
                            catch { }
                            //如果是业务审批最后一级审批通过，则记录时间
                            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowLastAuditPassTime(auditInfo.Model.ReturnID, new ESP.Compatible.Employee(auditInfo.CurrentUserId));
                            if (returnModel != null)
                            {
                                ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                            }

                            //如果审批结束，则改变单据状态为审批结束
                            if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(auditInfo.Workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
                            {
                                if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia || (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && (auditInfo.Model.IsFixCheque == null || auditInfo.Model.IsFixCheque == false)) || auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard || auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount || auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                                {
                                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(auditInfo.Model.ReturnID, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, null);
                                }
                                else
                                {
                                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(auditInfo.Model.ReturnID, (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving, null);
                                }
                                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.AddLogList(auditInfo.Model.ReturnID);
                            }
                        }
                    }
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
        /// 财务批次号审批
        /// </summary>
        /// <param name="auditList"></param>
        /// <param name="nextAuditerList"></param>
        /// <param name="batchModel"></param>
        /// <returns></returns>
        public static bool FinanceBatchAuditExpense(List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList, List<int> nextAuditerList, ESP.Finance.Entity.PNBatchInfo batchModel, bool isNeedFinanceDiretor)
        {
            string BeiJingBranch = string.Empty;
            BeiJingBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditBJBranch"];

            string hunanGroup = System.Configuration.ConfigurationManager.AppSettings["hunanGroup"];

            bool isbeijingPN = false;
            int logIndex = 0;
            int log = 0;
            if (BeiJingBranch.IndexOf(batchModel.BranchCode.ToLower()) >= 0)
            {
                isbeijingPN = true;
            }
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {


                    foreach (ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo in auditList)
                    {
                        logIndex = auditInfo.Workitem.EntityId;
                        Dictionary<string, object> nextAuditer = null;

                        if (!auditInfo.Prarms.ContainsKey("FactFee"))
                        {
                            auditInfo.Prarms.Add("FactFee", auditInfo.Model.PreFee.Value);
                        }

                        if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && (auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0 || auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0 || (isbeijingPN && auditInfo.Workitem.WebPage.IndexOf("step=f3") > 0)))
                        {
                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0)
                            {
                                if (isNeedFinanceDiretor)
                                {
                                    nextAuditer = new Dictionary<string, object>() 
                                   { 
                                    { "Finance2Id" ,  nextAuditerList.ToArray() }
                                    };
                                }
                                else
                                {
                                    nextAuditer = new Dictionary<string, object>() 
                                    { 
                                      { "Finance3Id" ,  nextAuditerList.ToArray() }
                                     };
                                }
                            }

                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0)
                            {
                                nextAuditer = new Dictionary<string, object>() 
                                { 
                                  { "Finance3Id" ,  nextAuditerList.ToArray() }
                                };
                            }


                            if (auditInfo.Model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel3)
                            {
                                int status = 140;
                                if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow || (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && (auditInfo.Model.IsFixCheque == true)))
                                {
                                    status = 136;
                                }
                                else
                                {
                                    status = 140;
                                }

                                ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(auditInfo.Model.ReturnID, status, null);
                                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add((ExpenseAuditDetailInfo)auditInfo.Prarms["LogModel"]);
                                ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CloseWorkflow(auditInfo.Workitem.WorkflowInstanceId.ToString(), auditInfo.Workitem.WorkItemId);

                            }
                            else
                            {
                                log = 1;
                                //调用工作流
                                ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", nextAuditer, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);
                                log = 2;
                                //如果审批结束，则改变单据状态为审批结束
                                if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(auditInfo.Workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
                                {
                                    int status = 140;
                                    if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow || (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && (auditInfo.Model.IsFixCheque == true)))
                                    {
                                        status = 136;
                                    }
                                    else
                                    {
                                        status = 140;
                                    }
                                    log = 3;
                                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(auditInfo.Model.ReturnID, status, null);
                                }
                            }
                        }
                        else
                        {
                            log = 5;
                            //调用工作流
                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f4") > 0)
                            {
                                ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CloseWorkflow(auditInfo.Workitem.WorkflowInstanceId.ToString(), auditInfo.Workitem.WorkItemId);
                                ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(auditInfo.Prarms);
                                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add((ExpenseAuditDetailInfo)auditInfo.Prarms["LogModel"]);

                            }
                            else
                            {
                                //如果是业务审批最后一级审批通过，则记录时间
                                ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);
                                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowLastAuditPassTime(auditInfo.Model.ReturnID, new ESP.Compatible.Employee(auditInfo.CurrentUserId));
                                if (returnModel != null)
                                {
                                    ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                                }
                            }
                            log = 6;

                            //如果审批结束，则改变单据状态为审批结束
                            if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(auditInfo.Workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
                            {
                                if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia || (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && (auditInfo.Model.IsFixCheque == null || auditInfo.Model.IsFixCheque == false)) || auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard || auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount || auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                                {
                                    log = 8;
                                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(auditInfo.Model.ReturnID, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, null);
                                    log = 9;
                                }
                                else
                                {
                                    log = 10;
                                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(auditInfo.Model.ReturnID, (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving, null);
                                    log = 11;
                                }
                                log = 12;
                                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.AddLogList(auditInfo.Model.ReturnID);
                                log = 13;
                            }
                        }
                    }
                    log = 14;
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);
                    log = 15;
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message + "--" + logIndex.ToString() + "--" + log.ToString());
                }
            }
        }

        /// <summary>
        /// 财务批次号审批驳回至F1
        /// </summary>
        /// <param name="auditList"></param>
        /// <param name="nextAuditerList"></param>
        /// <param name="batchModel"></param>
        /// <returns></returns>
        public static bool FinanceBatchUnAuditReturnF1(List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList, ESP.Finance.Entity.PNBatchInfo batchModel)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);

                    foreach (ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo in auditList)
                    {
                        ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Returned", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);
                    }
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
        /// 财务批次号审批驳回至申请人
        /// </summary>
        /// <param name="auditList"></param>
        /// <param name="nextAuditerList"></param>
        /// <param name="batchModel"></param>
        /// <returns></returns>
        public static bool FinanceBatchUnAuditReject(List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList, int batchid)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.BusinessLogic.PNBatchRelationManager.DeleteByBatchID(batchid);
                    ESP.Finance.BusinessLogic.PNBatchManager.Delete(batchid);

                    foreach (ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo in auditList)
                    {
                        //调用工作流
                        ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Rejected", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);
                    }
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


        public static bool BatchAuditReceiving(List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList, List<int> nextAuditerList, string batchNo)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo in auditList)
                    {

                        Dictionary<string, object> nextAuditer = null;
                        auditInfo.Prarms.Add("PaymentTypeCode", batchNo);

                        if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && (auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0 || auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0))
                        {
                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0)
                            {
                                nextAuditer = new Dictionary<string, object>() 
                                { 
                                    { "Finance2Id" ,  nextAuditerList.ToArray() }
                                };
                            }

                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0)
                            {
                                nextAuditer = new Dictionary<string, object>() 
                                { 
                                    { "Finance3Id" ,  nextAuditerList.ToArray() }
                                };
                            }

                            //调用工作流
                            ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", nextAuditer, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);

                        }
                        else
                        {
                            //如果是财务第三级审批现金借款单，则把实际收货金额写入冲销金额； 
                            //如果是财务第三级审批报销单，则把预计报销总金额写入实际报销金额； 
                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f3") > 0)
                            {
                                if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                                {
                                    auditInfo.Prarms.Add("FactFee", auditInfo.Model.PreFee);
                                }
                                else
                                {
                                    auditInfo.Prarms.Add("FactFee", ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(auditInfo.Model.ReturnID));
                                }

                            }
                            //调用工作流
                            ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);

                            //如果是业务审批最后一级审批通过，则记录时间
                            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowLastAuditPassTime(auditInfo.Model.ReturnID, new ESP.Compatible.Employee(auditInfo.CurrentUserId));
                            if (returnModel != null)
                            {
                                ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                            }

                            //如果是pr转pn借款单，则反写实际金额
                            if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                            {
                                ESP.Finance.Entity.ReturnInfo oldModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(auditInfo.Model.ParentID.Value);
                                oldModel.FactFee = auditInfo.Model.PreFee;
                                ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                            }

                            //如果审批结束，则改变单据状态为审批结束
                            if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(auditInfo.Workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
                            {
                                ESP.Finance.BusinessLogic.ExpenseAccountManager.RecivingOver(auditInfo.Model);
                            }
                        }
                    }
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
        /// 财务批次号 审批
        /// </summary>
        /// <param name="auditList"></param>
        /// <param name="nextAuditerList"></param>
        /// <param name="batchModel"></param>
        /// <returns></returns>
        public static bool FinanceBatchAuditReceiving(List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList, List<int> nextAuditerList, ESP.Finance.Entity.PNBatchInfo batchModel)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);
                    foreach (ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo in auditList)
                    {

                        Dictionary<string, object> nextAuditer = null;

                        if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && (auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0))
                        {
                            //if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0)
                            //{
                            nextAuditer = new Dictionary<string, object>() 
                                { 
                                    { "Finance2Id" ,  nextAuditerList.ToArray() }
                                };
                            //}

                            //if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0)
                            //{
                            //    nextAuditer = new Dictionary<string, object>() 
                            //    { 
                            //        { "Finance3Id" ,  nextAuditerList.ToArray() }
                            //    };
                            //}

                            //调用工作流
                            ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", nextAuditer, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);

                        }
                        else
                        {
                            //如果是财务第三级审批现金借款单，则把实际收货金额写入冲销金额； 
                            //如果是财务第三级审批报销单，则把预计报销总金额写入实际报销金额； 
                            if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && (auditInfo.Workitem.WebPage.IndexOf("step=f3") > 0 || auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0))
                            {
                                if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                                {
                                    auditInfo.Prarms.Add("FactFee", auditInfo.Model.PreFee);
                                }
                                else
                                {
                                    auditInfo.Prarms.Add("FactFee", ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(auditInfo.Model.ReturnID));
                                }

                            }
                            //调用工作流
                            ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);

                            //如果是业务审批最后一级审批通过，则记录时间
                            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowLastAuditPassTime(auditInfo.Model.ReturnID, new ESP.Compatible.Employee(auditInfo.CurrentUserId));
                            if (returnModel != null)
                            {
                                ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                            }

                            //如果是pr转pn借款单，则反写实际金额
                            if (auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || auditInfo.Model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                            {
                                ESP.Finance.Entity.ReturnInfo oldModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(auditInfo.Model.ParentID.Value);
                                oldModel.FactFee = auditInfo.Model.PreFee;
                                ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                            }

                            //如果审批结束，则改变单据状态为审批结束
                            if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(auditInfo.Workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
                            {
                                ESP.Finance.BusinessLogic.ExpenseAccountManager.RecivingOver(auditInfo.Model);
                            }
                        }
                    }
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

        #endregion 获得数据列表


        /// <summary>
        /// 导出报销的明细信息
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns>DataSet</returns>
        public static DataSet GetExportExpenseDetail(string whereStr)
        {
            return DataProvider.GetExportExpenseDetail(whereStr);
        }

        /// <summary>
        /// 导出报销的明细信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="response"></param>
        /// <returns>Excel</returns>
        public static string ExportExpenseAccountDetail(DataTable dt, System.Web.HttpResponse response)
        {
            string sourceFileName = "/Tmp/ExpenseAccount/ExpenseAccountTemplate.xls";
            string sourceFile = Common.GetLocalPath(sourceFileName);
            if (dt == null || dt.Rows.Count == 0) return sourceFileName;

            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[1];
            int counter = 0;//计数器
            int lineoffset = 4;//行数索引
            foreach (DataRow dr in dt.Rows)
            {
                //单据类型
                string ReturnTypeName_cell = "A" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, ReturnTypeName_cell, dr["ReturnTypeName"] != DBNull.Value ? dr["ReturnTypeName"].ToString() : "");

                //PN号
                string ReturnCode_cell = "B" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, ReturnCode_cell, dr["ReturnCode"] != DBNull.Value ? dr["ReturnCode"].ToString() : "");

                //项目号
                string projectCode_cell = "C" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, projectCode_cell, dr["ProjectCode"] != DBNull.Value ? dr["ProjectCode"].ToString() : "");

                //	项目名称
                string ReturnContent_cell = "D" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, ReturnContent_cell, dr["ReturnContent"] != DBNull.Value ? dr["ReturnContent"].ToString().Trim() : "");

                //	申请人
                string app_cell = "E" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, app_cell, dr["RequestEmployeeName"] != DBNull.Value ? dr["RequestEmployeeName"].ToString() : "");

                //	员工编号
                string empcode_cell = "F" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, empcode_cell, dr["RequestUserCode"] != DBNull.Value ? dr["RequestUserCode"].ToString() : "");

                //费用所属组
                string group_cell = "G" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, group_cell, dr["DepartmentName"] != DBNull.Value ? dr["DepartmentName"].ToString() : "");

                //费用发生日期
                string date_cell = "H" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, date_cell, dr["ExpenseDate"] == DBNull.Value ? "" : dr["ExpenseDate"].ToString());

                //项目成本明细
                string costDetail_cell = "I" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, costDetail_cell, dr["CostDetailTypeName"] != DBNull.Value ? dr["CostDetailTypeName"].ToString() : "");

                //费用类型
                string expenseType_cell = "J" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, expenseType_cell, dr["ExpenseTypeName"] != DBNull.Value ? dr["ExpenseTypeName"].ToString() : "");

                //费用明细描述
                string detail_cell = "K" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, detail_cell, dr["ExpenseDesc"] != DBNull.Value ? dr["ExpenseDesc"].ToString().Trim() : dr["ReturnContent"].ToString().Trim());

                //数量
                string number_cell = "L" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, number_cell, dr["ExpenseTypeNumber"] != DBNull.Value ? dr["ExpenseTypeNumber"].ToString() : "");

                //申请金额
                string expenseMoney_cell = "M" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, expenseMoney_cell, dr["ExpenseMoney"] != DBNull.Value ? Convert.ToDecimal(dr["ExpenseMoney"]).ToString("#,##0.00") : Convert.ToDecimal(dr["PreFee"]).ToString("#,##0.00"));

                if (dr["ReturnTypeName"] != DBNull.Value && (dr["ReturnTypeName"].ToString().Trim() == "报销单" || dr["ReturnTypeName"].ToString().Trim() == "现金借款单" || dr["ReturnTypeName"].ToString().Trim() == "借款冲销单"))
                {
                    int requestorId = int.Parse(dr["RequestorID"].ToString());
                    ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(requestorId);
                    //开户银行
                    string expenseBank_cell = "N" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, expenseBank_cell, empModel.SalaryBank);

                    //帐号
                    string expenseAccount_cell = "O" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, expenseAccount_cell, "'" + empModel.SalaryCardNo);

                    //收款方
                    string expenseReceiver_cell = "P" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, expenseReceiver_cell, "'" + empModel.FullNameCN);
                }
                else
                {
                    //开户银行
                    string expenseBank_cell = "N" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, expenseBank_cell, dr["BankName"].ToString());

                    //帐号
                    string expenseAccount_cell = "O" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, expenseAccount_cell, "'" + dr["BankAccountNo"].ToString());

                    //收款方
                    string expenseReceiver_cell = "P" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, expenseReceiver_cell, "'" + dr["Recipient"].ToString());

                    //所在城市
                    string expenseCity_cell = "Q" + (lineoffset + 1).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, expenseCity_cell, "'" + dr["City"].ToString());
                }

                lineoffset++;
            }
            //ExcelHandle.WriteCell(excel.CurSheet, "B" + (lineoffset + 4).ToString(), "公司名称");
            //ExcelHandle.WriteCell(excel.CurSheet, "B" + (lineoffset + 5).ToString(), "开户行名称");
            //ExcelHandle.WriteCell(excel.CurSheet, "B" + (lineoffset + 6).ToString(), "银行帐号");

            //string suppliername_cell = "E" + (lineoffset + 4).ToString();
            //ExcelHandle.WriteCell(excel.CurSheet, suppliername_cell, returnList[0].SupplierName);
            //string supplierBank_cell = "E" + (lineoffset + 5).ToString();
            //ExcelHandle.WriteCell(excel.CurSheet, supplierBank_cell, returnList[0].SupplierBankName);
            //string supplierAccount_cell = "E" + (lineoffset + 6).ToString();
            //ExcelHandle.WriteCell(excel.CurSheet, supplierAccount_cell, returnList[0].SupplierBankAccount);

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


        /// <summary>
        /// 创建冲销单
        /// </summary>
        /// <param name="returnInfo"></param>
        /// <returns></returns>
        public static int CreateRecivingForm(ESP.Finance.Entity.ReturnInfo returnInfo)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnInfo.ReturnStatus = (int)PaymentStatus.Receiving;
                    ESP.Finance.BusinessLogic.ReturnManager.Update(returnInfo);

                    List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = null;
                    if (returnInfo != null)
                    {
                        list = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID = " + returnInfo.ReturnID);
                    }

                    List<ESP.Finance.Entity.ExpenseAuditerListInfo> operationList = ESP.Finance.BusinessLogic.ExpenseAuditerListManager.GetUnDelList(returnInfo.ReturnID, "");

                    returnInfo.ParentID = returnInfo.ReturnID;
                    returnInfo.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving;
                    returnInfo.ReturnStatus = (int)PaymentStatus.ConfirmReceiving;
                    int newReturnID = ESP.Finance.BusinessLogic.ReturnManager.CreateReturnInFinance(returnInfo);

                    if (list != null)
                    {
                        foreach (ESP.Finance.Entity.ExpenseAccountDetailInfo detail in list)
                        {
                            detail.ReturnID = newReturnID;
                            detail.Status = 0;
                            ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Add(detail);
                        }
                    }

                    if (operationList != null && operationList.Count > 0)
                    {
                        foreach (ESP.Finance.Entity.ExpenseAuditerListInfo operation in operationList)
                        {
                            operation.ReturnID = newReturnID;
                            ESP.Finance.BusinessLogic.ExpenseAuditerListManager.Add(operation);
                        }
                    }

                    trans.Commit();
                    return newReturnID;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 冲销完成
        /// </summary>
        /// <param name="returnInfo"></param>
        /// <returns></returns>
        public static bool RecivingOver(ESP.Finance.Entity.ReturnInfo model)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //PR现金借款冲销
                    //if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                    //{
                    //修改原借款单状态为冲销完成
                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(model.ParentID.Value, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, null);
                    //修改当前冲销单状态为财务审核完成
                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(model.ReturnID, (int)ESP.Finance.Utility.PaymentStatus.ReceivingEnd, null);
                    // }//现金借款冲销
                    //else
                    //{
                    //修改原借款单状态为冲销完成
                    //  ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(model.ParentID.Value, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, null);
                    //修改当前冲销单状态为财务审核完成
                    //   ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(model.ReturnID, (int)ESP.Finance.Utility.PaymentStatus.ReceivingEnd, null);
                    // }

                    //修改原借款单明细状态为不记入成本
                    ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.UpdateStatusByReturnID(model.ParentID.Value, 0);
                    //修改当前冲销单明细状态为记入成本
                    ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.UpdateStatusByReturnID(model.ReturnID, 1);
                    //记录日志
                    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.AddLogList(model.ReturnID);

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
        /// 根据批次ID 获得工作项
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public static List<ESP.Finance.SqlDataAccess.WorkItem> GetWorkItemsByBatchID(int batchid)
        {
            return DataProvider.GetWorkItemsByBatchID(batchid);
        }

        /// <summary>
        /// 根据批次ID 获得工作项IDS
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public static string GetWorkItemIDsByBatchID(int batchid)
        {
            List<ESP.Finance.SqlDataAccess.WorkItem> items = DataProvider.GetWorkItemsByBatchID(batchid);
            string ids = "";
            foreach (ESP.Finance.SqlDataAccess.WorkItem item in items)
            {
                ids += item.WorkItemId + ",";
            }
            return ids.TrimEnd(',');
        }

        /// <summary>
        /// 获得下属人员已审批报销单明细
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public static DataSet GetAlreadyAuditDetailList(string whereStr)
        {
            return DataProvider.GetAlreadyAuditDetailList(whereStr);
        }
        #endregion  成员方法
    }
}

