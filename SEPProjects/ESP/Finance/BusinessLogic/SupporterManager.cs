using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类SupporterBLL 的摘要说明。
	/// </summary>
     
     
    public static class SupporterManager
	{
		//private readonly ESP.Finance.DataAccess.SupporterDAL dal=new ESP.Finance.DataAccess.SupporterDAL();

        private static ESP.Finance.IDataAccess.ISupporterDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ISupporterDataProvider>.Instance;}}
        //private const string _dalProviderName = "SupporterDALProvider";

        public static int GetSupporterCount(int projectid, int supporterid, int groupid)
        {
            return DataProvider.GetSupporterCount(projectid, supporterid, groupid);
        }
        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(ESP.Finance.Entity.SupporterInfo model)
		{
            if (!CheckerManager.CheckProjectOddAmount(model.ProjectID, model.BudgetAllocation == null ? Convert.ToDecimal(0) : model.BudgetAllocation.Value))
            {
                return 0;
            }
            int res = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    model.SupporterCode = DataProvider.CreateSupporterCode();
                    res = DataProvider.Add(model,trans);
                    if (res > 0)
                    {
                        //增加授权
                        ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
                        datainfo.DataId = res;
                        datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Supporter;
                        List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                        ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                        p.UserId = model.LeaderUserID.Value;
                        p.IsEditor = true;
                        p.IsViewer = true;
                        permissionList.Add(p);
                        ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList,trans);
                        //将项目负责人添加到项目成员中
                        SupportMemberInfo member = new SupportMemberInfo();
                        member.CreateTime = DateTime.Now;
                        member.SupportID = res;
                        member.ProjectID = model.ProjectID;
                        member.GroupID = model.GroupID;
                        member.GroupName = model.GroupName;
                        member.MemberUserID = model.LeaderUserID;
                        member.MemberCode = model.LeaderCode;
                        member.MemberEmployeeName = model.LeaderEmployeeName;
                        member.MemberUserName = model.LeaderUserName;
                        SupportMemberManager.Add(member,trans);
                        //将项目负责人添加到项目成员中
                    }
                    trans.Commit();
                }
                catch
                {
                    res = 0;
                    trans.Rollback();
                }
            }
            return res;
		}

        /// <summary>
        /// 根据项目ID更新项目号
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectCode"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>
         
         
        public static ESP.Finance.Utility.UpdateResult UpdateProjectCode(int projectId, string projectCode)
        {
            int res = 0;
            try
            {
                //trans//res = DataProvider.UpdateProjectCode(projectId,projectCode, true);
                res = DataProvider.UpdateProjectCode(projectId, projectCode);
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
        /// 用于 工作流 审批
        /// </summary>
        /// <param name="supporterId"></param>
        /// <param name="workItemID"></param>
        /// <param name="workItemName"></param>
        /// <param name="processID"></param>
        /// <param name="instanceID"></param>
        /// <returns></returns>
         
         
        public static UpdateResult UpdateWorkFlow(int supporterId, int workItemID, string workItemName, int processID, int instanceID)
        {
            int res = 0;
            try
            {
                //trans//res = DataProvider.UpdateWorkFlow(supporterId, workItemID, workItemName, processID, instanceID,true);
                res = DataProvider.UpdateWorkFlow(supporterId, workItemID, workItemName, processID, instanceID);
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

        public static int UpdateDimission(ESP.Finance.Entity.SupporterInfo model,SqlTransaction trans)
        {
            return DataProvider.Update(model, trans);
        }
        public static int UpdateDimission(ESP.Finance.Entity.SupporterInfo model)
        {
            return DataProvider.Update(model);
        }

        /// <summary>
		/// 更新一条数据
		/// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.SupporterInfo model)
        {
            return Update(model, false);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.SupporterInfo model, bool IsSaveLog)
        {
            SupporterInfo old = GetModel(model.SupportID);
            IList<ESP.Finance.Entity.SupportMemberInfo> memberList = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(" supportID=" + model.SupportID.ToString());
            IList<ESP.Finance.Entity.SupporterAuditHistInfo> auditList = ESP.Finance.BusinessLogic.SupporterAuditHistManager.GetList(" SupporterID=" + model.SupportID.ToString());
                  
            decimal bugdet = model.BudgetAllocation == null ? 0 : model.BudgetAllocation.Value;
            decimal oldBugdet = old.BudgetAllocation == null ? 0 : old.BudgetAllocation.Value;
            decimal balance = bugdet - oldBugdet;
            if (!CheckerManager.CheckProjectOddAmount(model.ProjectID, balance))
            {
                return UpdateResult.AmountOverflow;
            }
            int res = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    res = DataProvider.Update(model,trans);
                    //如果支持方是已审批状态,增加历史
                    if (model.Status == (int)ESP.Finance.Utility.Status.FinanceAuditComplete)
                    {
                        SupportHistoryManager.Add(model, trans);
                    }
                    if (model.LeaderUserID.Value != old.LeaderUserID.Value)
                    {
                        ESP.Finance.Entity.SupportMemberInfo member = new SupportMemberInfo();
                        member.CreateTime = DateTime.Now;
                        member.GroupID = model.GroupID.Value;
                        member.GroupName = model.GroupName;
                        member.MemberUserID = model.LeaderUserID;
                        member.MemberEmployeeName = model.LeaderEmployeeName;
                        member.ProjectID = model.ProjectID;
                        member.ProjectCode = model.ProjectCode;
                        member.SupportID = model.SupportID;
                        ESP.Finance.BusinessLogic.SupportMemberManager.Add(member,trans);
                    }
                    //增加授权
                    ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
                    datainfo.DataId = model.SupportID;
                    datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Supporter;
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                    ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                    p.UserId = model.LeaderUserID.Value;
                    p.IsEditor = true;
                    p.IsViewer = true;
                    permissionList.Add(p);
                     foreach (ESP.Finance.Entity.SupportMemberInfo mem in memberList)
                    {
                        ESP.Purchase.Entity.DataPermissionInfo p1 = new ESP.Purchase.Entity.DataPermissionInfo();
                        p1.UserId = mem.MemberUserID.Value;
                        p1.IsEditor = false;
                        p1.IsViewer = true;
                        permissionList.Add(p1);
                    }
                    foreach (ESP.Finance.Entity.SupporterAuditHistInfo audit in auditList)
                    {
                        ESP.Purchase.Entity.DataPermissionInfo p2 = new ESP.Purchase.Entity.DataPermissionInfo();
                        p2.UserId = audit.AuditorUserID.Value;
                        p2.IsEditor = true;
                        p2.IsViewer = true;
                        permissionList.Add(p2);
                    }
                    ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList,trans);

                    //支持方提交成本占用 $$$$$
                    #region   增加流水账表记录
                    if (IsSaveLog)
                    {

                        var supporterCosts = ESP.Finance.BusinessLogic.SupporterCostManager.GetList(" SupportId = " + model.SupportID);

                        var supporterExpenses = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(" SupporterID = " + model.SupportID);

                        ESP.Purchase.BusinessLogic.CostRecordsManager.InsertSupporter(model, supporterCosts, supporterExpenses, trans);

                    }
                    #endregion

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    return UpdateResult.Failed;
                    
                }
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
		public static DeleteResult Delete(int SupportID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(SupportID);
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


         
         
        public static decimal GetTotalAmountByProject(int projectId)
        {
            //trans//return DataProvider.GetTotalAmountByProject(projectId, true);
            return DataProvider.GetTotalAmountByProject(projectId);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static ESP.Finance.Entity.SupporterInfo GetModel(int SupportID)
		{
			
			return DataProvider.GetModel(SupportID);
		}

        public static ESP.Finance.Entity.SupporterInfo GetModel(int SupportID,SqlTransaction trans)
        {

            return DataProvider.GetModel(SupportID,trans);
        }

        public static ESP.Finance.Entity.SupporterInfo GetModelWithList(int supportId,SqlTransaction trans)
        {
            ESP.Finance.Entity.SupporterInfo model = GetModel(supportId,trans);
            model.SupporterCosts = ESP.Finance.BusinessLogic.SupporterCostManager.GetList(supportId,trans);
            model.SupporterExpenses = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(supportId,trans);
            model.SupporterMembers = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(supportId,trans);
            model.SupporterSchedules = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList( supportId,trans);
            return model;
        }


        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupporterInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupporterInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.SupporterInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetListByProject(projectID, term, param);
        }

        public static DataTable GetSupportListJoinHist(int userId)
        {
            return DataProvider.GetSupportListJoinHist(userId);
        }
        public static int changeLeader(string supportIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            return DataProvider.changeLeader(supportIds, oldUserId, newUserId,currentUser);
        }
        public static int changAuditor(string supportIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            return DataProvider.changAuditor(supportIds, oldUserId, newUserId,currentUser);
        }

        #endregion 获得数据列表

        public static IList<SupporterInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            return DataProvider.GetWaitAuditList(userIds, strTerms, parms);
        }


        public static IList<SupporterInfo> GetWaitAuditList(int[] userIds)
        {
            return DataProvider.GetWaitAuditList(userIds);
        }
		#endregion  成员方法

        #region ISupporterProvider 成员




        #endregion
    }
}

