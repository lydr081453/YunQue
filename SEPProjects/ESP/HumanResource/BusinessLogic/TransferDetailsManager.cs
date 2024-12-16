using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ESP.HumanResource.BusinessLogic
{
    public class TransferDetailsManager
    {
        private static TransferDetailsProvider dal = new TransferDetailsProvider();
        public TransferDetailsManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(TransferDetailsInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(TransferDetailsInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static TransferDetailsInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<TransferDetailsInfo> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }

        public static List<ESP.Framework.Entity.UserInfo> GetReceiverInfo(int transferId)
        {
            return dal.GetReceiverInfo(transferId);
        }

        public static IList<TransferDetailsInfo> GetList(string strWhere, List<SqlParameter> parms, SqlTransaction trans)
        {
            return dal.GetList(strWhere, parms, trans);
        }

        public static bool TransferHRDone(TransferInfo transfer, HRAuditLogInfo hrAuditLogInfo, out Dictionary<int, string> mailInfo)
        {
            mailInfo = new Dictionary<int, string>();

            ESP.HumanResource.Entity.EmployeeBaseInfo userModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(transfer.TransId);
            ESP.HumanResource.DataAccess.EmployeesInPositionsDataProvider positionProvider = new EmployeesInPositionsDataProvider();

            ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = new ESP.HumanResource.Entity.EmployeesInPositionsInfo();

            ESP.HumanResource.Entity.HeadAccountInfo headcount = (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).GetModel(transfer.HeadCountId);
            headcount.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.OfferLetteAudited;

            

            PositionLogInfo oldPositionLogInfo = PositionLogManager.GetModel(userModel.UserID, transfer.OldPositionId);

            var detailList = GetList(" transferId=" + transfer.Id.ToString(), null);

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //写日志
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrAuditLogDal.Update(hrAuditLogInfo, trans);

                    #region 人员转组涉及部门、职务、日志
                    positionModel.DepartmentID = transfer.NewGroupId;

                    positionModel.DepartmentPositionID = transfer.NewPositionId;
                    positionModel.UserID = transfer.TransId;
                    positionModel.IsManager = false;
                    positionModel.IsActing = false;
                    positionModel.BeginDate = transfer.TransInDate;

                    PositionLogInfo posLogInfo = new PositionLogInfo();

                    posLogInfo.UserId = positionModel.UserID;
                    posLogInfo.UserName = positionModel.UserName;
                    posLogInfo.UserCode = positionModel.UserCode;
                    posLogInfo.DepartmentId = positionModel.DepartmentID;
                    posLogInfo.DepartmentName = positionModel.DepartmentName;

                    ESP.Framework.Entity.DepartmentPositionInfo dPInfo = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(transfer.NewPositionId);
                    if (dPInfo != null)
                    {
                        posLogInfo.PositionId = dPInfo.DepartmentPositionID;
                        posLogInfo.PositionName = dPInfo.DepartmentPositionName;
                        PositionBaseInfo pBInfo = PositionBaseManager.GetModel(dPInfo.PositionBaseId);
                        if (pBInfo != null)
                        {
                            posLogInfo.PositionBaseId = pBInfo.Id;
                            posLogInfo.PositionBaseName = pBInfo.PositionName;

                            posLogInfo.LevelId = pBInfo.LeveId;
                            posLogInfo.LevelName = pBInfo.LevelName;

                        }

                    }
                    posLogInfo.BeginDate = transfer.TransInDate;
                    posLogInfo.CreateDate = DateTime.Now;

                    //删除现有职务
                    positionProvider.Delete(transfer.TransId, trans);
                    //增加新职务
                    positionProvider.Add(positionModel, trans);
                    //写职务变更时间轴
                    PositionLogManager.Add(posLogInfo, trans);
                    //更新旧职务时间轴结束日期
                    if (oldPositionLogInfo != null)
                    {
                        oldPositionLogInfo.EndDate = transfer.TransInDate;
                        PositionLogManager.Update(oldPositionLogInfo, trans);
                    }
                    //更新考勤级别
                   //new ESP.Administrative.BusinessLogic.CommuterTimeManager().UpdateUserCommuteType(positionModel.UserID, attendancetype, trans);
                   //new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().UpdateUserCommuteType(positionModel.UserID, attendancetype, trans);

                    #endregion

                    int retval = 0;
                    string desc = string.Empty;
                    //单据转组涉及项目号、支持方
                    var projectList = detailList.Where(x => x.TransGroup == 1 && x.FormType == "项目号");
                    var supporterList = detailList.Where(x => x.TransGroup == 1 && x.FormType == "支持方");
                    foreach (var item in projectList)
                    {
                        retval+=dal.TransProject(item.FormId, transfer.NewGroupId, transfer.NewGroupName, transfer.OldGroupId, trans);
                        desc = item.ProjectCode + ": 项目号（" + transfer.OldGroupName + "），于 " + DateTime.Now.ToString("yyyy-MM-dd") + " 组别由 " + transfer.OldGroupName + " 变更为 " + transfer.NewGroupName;

                        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(item.ProjectCode.Substring(0,1));
                        if (!mailInfo.ContainsKey(branchModel.DepartmentId))
                            mailInfo.Add(branchModel.DepartmentId, desc);
                        else
                            mailInfo[branchModel.DepartmentId] += "<br/>" + desc;
                    }
                    foreach (var item in supporterList)
                    {
                        retval+=dal.TransSupporter(item.FormId, transfer.NewGroupId, transfer.NewGroupName, transfer.OldGroupId, trans);
                        desc = item.ProjectCode + ": 支持方（" + transfer.OldGroupName + "），于 " + DateTime.Now.ToString("yyyy-MM-dd") + " 组别由 " + transfer.OldGroupName + " 变更为 " + transfer.NewGroupName;

                        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(item.ProjectCode.Substring(0, 1));
                        if (!mailInfo.ContainsKey(branchModel.DepartmentId))
                            mailInfo.Add(branchModel.DepartmentId, desc);
                        else
                            mailInfo[branchModel.DepartmentId] += "<br/>" + desc;
                    }

                    //单据转交接人，单据权限
                    //项目号
                    var receiverProjectList = detailList.Where(x => x.ReceiverId != 0 && x.FormType == "项目号");
                    
                    foreach (var item in receiverProjectList)
                    { 
                        var receiver =ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(item.ReceiverId);
                        retval+=dal.ReceiverProject(item.FormId, receiver, positionModel, trans);

                        desc = item.ProjectCode + ": 项目号（" + transfer.OldGroupName + "），于 " + DateTime.Now.ToString("yyyy-MM-dd") + " 负责人由 " + item.UserName + " 变更为 " + item.ReceiverName;

                        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(item.ProjectCode.Substring(0, 1));
                        if (!mailInfo.ContainsKey(branchModel.DepartmentId))
                            mailInfo.Add(branchModel.DepartmentId, desc);
                        else
                            mailInfo[branchModel.DepartmentId] += "<br/>" + desc;
                    }

                    //支持方
                    var receiverSupporterList = detailList.Where(x => x.ReceiverId != 0 && x.FormType == "支持方");
                    foreach (var item in receiverSupporterList)
                    {
                        var receiver = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(item.ReceiverId);
                        retval += dal.ReceiverSupporter(item.FormId, receiver, positionModel, trans);

                        desc = item.ProjectCode + ": 支持方（" + transfer.OldGroupName + "），于 " + DateTime.Now.ToString("yyyy-MM-dd") + " 负责人由 " + item.UserName + " 变更为 " + item.ReceiverName;

                        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(item.ProjectCode.Substring(0, 1));
                        if (!mailInfo.ContainsKey(branchModel.DepartmentId))
                            mailInfo.Add(branchModel.DepartmentId, desc);
                        else
                            mailInfo[branchModel.DepartmentId] += "<br/>" + desc;
                    }
                   
                        

                    //PR
                    var receiverPRList = detailList.Where(x => x.ReceiverId != 0 && x.FormType == "PR单");
                    foreach (var item in receiverPRList)
                    {
                        var receiver = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(item.ReceiverId);
                        retval += dal.ReceiverPR(item.FormId, receiver, positionModel, trans);
                    }
                        

                    //收货
                    var receiverRecipientList = detailList.Where(x => x.ReceiverId != 0 && x.FormType == "收货");
                    foreach (var item in receiverRecipientList)
                    {
                        var receiver = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(item.ReceiverId);
                        retval += dal.ReceiverRecipient(item.FormId, receiver, positionModel, trans);
                    }
                        
                    //附加收货
                    var receiverAttendList = detailList.Where(x => x.ReceiverId != 0 && x.FormType == "附加收货");
                    foreach (var item in receiverAttendList)
                    {
                        var receiver = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(item.ReceiverId);
                        retval += dal.ReceiverRecipientAppend(item.FormId, receiver, positionModel, trans);
                    }
                       

                    //支票电汇
                    var receiverOOPList = detailList.Where(x => x.ReceiverId != 0 && x.FormType == "支票电汇");
                    foreach (var item in receiverOOPList)
                    {
                        var receiver = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(item.ReceiverId);
                        retval += dal.ReceiverOOP(item.FormId, receiver, positionModel, trans);
                    }
                        

                    //更新单据状态
                    TransferManager.Update(transfer, trans);
                    //更新headcount状态
                    (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).Update(headcount, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }
        public static bool TransferAudit(TransferInfo transfer, HRAuditLogInfo hrAuditLogInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrAuditLogDal.Update(hrAuditLogInfo, trans);

                    ESP.HumanResource.Entity.HRAuditLogInfo manangeAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

                    ESP.Framework.Entity.OperationAuditManageInfo oldManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(transfer.OldGroupId);
                    ESP.Framework.Entity.OperationAuditManageInfo newManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(transfer.NewGroupId);

                    int nextAuditor = 0;

                    //待转出总经理审批（交接完毕）
                    if (transfer.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.ReceiverConfirm)
                    {
                        if (oldManageModel.DirectorId == newManageModel.DirectorId)
                        {
                            if (oldManageModel.ManagerId == newManageModel.ManagerId)
                            {
                                transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn;
                                nextAuditor = 0;
                            }
                            else
                            {
                                transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmIn;
                                nextAuditor = newManageModel.ManagerId;
                            }
                        }
                        else
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmOut;
                            nextAuditor = newManageModel.DirectorId;
                        }

                    }
                    //转出组总经理已审批
                    else if (transfer.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmOut)
                    {
                        if (newManageModel.DirectorId == newManageModel.ManagerId)
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn;
                            nextAuditor = 0;
                        }
                        else if (oldManageModel.ManagerId == newManageModel.ManagerId)
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn;
                            nextAuditor = 0;
                        }
                        else
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmIn;
                            nextAuditor = newManageModel.ManagerId;
                        }
                    }
                    //转入组总监已审批
                    else if (transfer.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmIn)
                    {
                        transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn;
                        nextAuditor = 0;
                    }

                    if (nextAuditor != 0)
                    {
                        ESP.Framework.Entity.UserInfo nextHr = ESP.Framework.BusinessLogic.UserManager.Get(nextAuditor);
                        manangeAuditLogInfo.AuditorId = nextHr.UserID;
                        manangeAuditLogInfo.AuditorName = nextHr.FullNameCN;
                        manangeAuditLogInfo.AuditLevel = transfer.Status;

                        manangeAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                        manangeAuditLogInfo.FormId = transfer.Id;
                        manangeAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;
                        hrAuditLogDal.Add(manangeAuditLogInfo, trans);
                    }

                    TransferManager.Update(transfer, trans);  // 修改离职单信息
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        public static bool TransferReject(TransferInfo transfer, HRAuditLogInfo hrAuditLogInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrAuditLogDal.Update(hrAuditLogInfo, trans);

                    transfer.Status = 2;

                    TransferManager.Update(transfer, trans);  // 修改离职单信息
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }


        public static bool TransferSettingReceiver(ESP.HumanResource.Entity.TransferInfo transfer,
   Dictionary<string, ESP.HumanResource.Entity.TransferDetailsInfo> dicTransfer, ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo)
        {

            ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(transfer.OldGroupId);
            ESP.Framework.Entity.OperationAuditManageInfo newManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(transfer.NewGroupId);

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrAuditLogDal.Update(hrAuditLogInfo, trans);
                    int nextAuditor = 0;
                    ESP.Framework.Entity.UserInfo nextHr = null;

                    // 保存交接单据信息
                    if (dicTransfer != null && dicTransfer.Count > 0)
                    {
                        foreach (KeyValuePair<string, ESP.HumanResource.Entity.TransferDetailsInfo> pair in dicTransfer)
                        {
                            pair.Value.CreateTime = DateTime.Now;
                            pair.Value.TransferId = transfer.Id;
                            pair.Value.Status = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                            dal.Add(pair.Value, trans);
                        }

                      

                        IList<ESP.HumanResource.Entity.TransferDetailsInfo> detailList = dal.GetList(" TransferId="+transfer.Id+" and receiverId<>0", new List<SqlParameter>(), trans);

                        if (detailList != null && detailList.Count > 0)
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmOut;
                        }
                        else
                        {
                            ESP.HumanResource.Entity.HRAuditLogInfo manangeAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

                            if (manageModel.DirectorId == manageModel.ManagerId)
                            {
                                transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmOut;
                                nextAuditor = newManageModel.DirectorId;

                            }
                            else if (manageModel.DirectorId == newManageModel.DirectorId)
                            {
                                transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmIn;
                                nextAuditor = newManageModel.ManagerId;
                            }
                            else if (manageModel.DirectorId == newManageModel.ManagerId)
                            {
                                transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn;
                                nextAuditor = 0;
                            }
                            else
                            {
                                transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmOut;
                                nextAuditor = manageModel.ManagerId;
                            }

                            if (nextAuditor != 0)
                            {
                                nextHr = ESP.Framework.BusinessLogic.UserManager.Get(nextAuditor);
                                manangeAuditLogInfo.AuditorId = nextHr.UserID;
                                manangeAuditLogInfo.AuditorName = nextHr.FullNameCN;
                                manangeAuditLogInfo.AuditLevel = transfer.Status;

                                manangeAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                                manangeAuditLogInfo.FormId = transfer.Id;
                                manangeAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;
                                hrAuditLogDal.Add(manangeAuditLogInfo, trans);
                            }
                        }
                    }
                    else
                    {
                        ESP.HumanResource.Entity.HRAuditLogInfo manangeAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

                        if (manageModel.DirectorId == manageModel.ManagerId)
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmOut;
                            nextAuditor = newManageModel.DirectorId;

                        }
                        else if (manageModel.DirectorId == newManageModel.DirectorId)
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmIn;
                            nextAuditor = newManageModel.ManagerId;
                        }
                        else if (manageModel.DirectorId == newManageModel.ManagerId)
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn;
                            nextAuditor = 0;
                        }
                        else
                        {
                            transfer.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmOut;
                            nextAuditor = manageModel.ManagerId;
                        }

                        if (nextAuditor != 0)
                        {
                            nextHr = ESP.Framework.BusinessLogic.UserManager.Get(nextAuditor);
                            manangeAuditLogInfo.AuditorId = nextHr.UserID;
                            manangeAuditLogInfo.AuditorName = nextHr.FullNameCN;
                            manangeAuditLogInfo.AuditLevel = transfer.Status;

                            manangeAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                            manangeAuditLogInfo.FormId = transfer.Id;
                            manangeAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;
                            hrAuditLogDal.Add(manangeAuditLogInfo, trans);
                        }

                    }

                    TransferManager.Update(transfer, trans);  // 修改离职单信息
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }


        public static bool UpdateDetailAndCheckTransfer(ESP.HumanResource.Entity.TransferDetailsInfo detailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    #region 修改交接单信息。
                    detailInfo.Status = (int)ESP.HumanResource.Common.AuditStatus.Audited;

                    dal.Update(detailInfo, trans);
                    #endregion

                    #region 检查是否需要修改离职单信息（判断如果是最后一条交接单，就修改离职单状态，并转交下一人审批）。
                    int flag = 0;  // 标记是否需要修改离职单

                    var detailList = dal.GetList(" transferId=@transferId and receiverid<>0", new List<SqlParameter>() { new SqlParameter("@transferId", detailInfo.TransferId) }, trans);
                    if (detailList != null && detailList.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.TransferDetailsInfo detail in detailList)
                        {
                            if (detail.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit && detail.Id != detailInfo.Id)
                            {
                                flag = 1;
                            }
                        }
                    }
                    if (flag == 0)
                    {
                        int nextAuditor = 0;
                        ESP.HumanResource.DataAccess.TransferDataProvider dimFormDal = new ESP.HumanResource.DataAccess.TransferDataProvider();
                        ESP.HumanResource.Entity.TransferInfo formInfo = dimFormDal.GetModel(detailInfo.TransferId);

                        #region 添加审批日志
                        ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                        ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

                        ESP.Framework.Entity.OperationAuditManageInfo oldManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(formInfo.OldGroupId);
                        ESP.Framework.Entity.OperationAuditManageInfo newManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(formInfo.NewGroupId);


                        //转出组总监=转出组总经理
                        if (oldManageModel.DirectorId == oldManageModel.ManagerId)
                        {
                            nextAuditor = newManageModel.DirectorId;
                            formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmOut;
                        }
                        //转出组总监=转入组总监
                        else if (oldManageModel.DirectorId == newManageModel.DirectorId)
                        {
                            if (oldManageModel.ManagerId == newManageModel.ManagerId)
                            {
                                nextAuditor = newManageModel.ManagerId;
                                formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmIn;
                            }
                            else
                            {
                                nextAuditor = oldManageModel.ManagerId;
                                formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ReceiverConfirm;
                            }
                        }
                        //转出组总监=转入组总经理
                        else if (oldManageModel.DirectorId == newManageModel.ManagerId)
                        {
                            if (oldManageModel.DirectorId == newManageModel.DirectorId)
                            {
                                nextAuditor = 0;
                                formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn;
                            }
                            else
                            {
                                nextAuditor = newManageModel.DirectorId;
                                formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmOut;
                            }
                        }
                        else
                        {
                            nextAuditor = oldManageModel.ManagerId;
                            formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ReceiverConfirm;
                        }

                        ESP.Framework.Entity.UserInfo nextHr = ESP.Framework.BusinessLogic.UserManager.Get(nextAuditor);
                        hrAuditLogInfo.AuditorId = nextHr.UserID;
                        hrAuditLogInfo.AuditorName = nextHr.FullNameCN;
                        hrAuditLogInfo.AuditLevel = formInfo.Status;
                        hrAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                        hrAuditLogInfo.FormId = formInfo.Id;
                        hrAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;

                        hrAuditLogDal.Add(hrAuditLogInfo, trans);
                        #endregion

                        dimFormDal.Update(formInfo, trans);
                    }
                    #endregion

                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public static bool UpdateDetailAndCheckTransfer(List<ESP.HumanResource.Entity.TransferDetailsInfo> detailInfos)
        {
            int tempFlag = 0;
            int transferId = 0;
            bool b = false;
            ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int flag = 0;  // 标记是否需要修改离职单
                    foreach (ESP.HumanResource.Entity.TransferDetailsInfo detailInfo in detailInfos)
                    {
                        #region 修改交接单信息。

                        detailInfo.Status = (int)ESP.HumanResource.Common.AuditStatus.Audited;
                        dal.Update(detailInfo, trans);
                        #endregion
                        transferId = detailInfo.TransferId;

                        #region 检查是否需要修改离职单信息（判断如果是最后一条交接单，就修改离职单状态，并转交下一人审批）。
                        flag = 0;
                        var detailList = dal.GetList(" transferId=@transferId and receiverid<>0", new List<SqlParameter>() { new SqlParameter("@transferId", detailInfo.TransferId) }, trans);

                        if (detailList != null && detailList.Count > 0)
                        {
                            foreach (ESP.HumanResource.Entity.TransferDetailsInfo detail in detailList)
                            {
                                if (detail.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit && detail.Id != detailInfo.Id)
                                {
                                    flag = 1;
                                }
                            }
                        }
                        #endregion
                    }

                    if (flag == 0)
                    {
                        int nextAuditor = 0;
                        ESP.HumanResource.DataAccess.TransferDataProvider dimFormDal = new ESP.HumanResource.DataAccess.TransferDataProvider();
                        ESP.HumanResource.Entity.TransferInfo formInfo = dimFormDal.GetModel(transferId);

                        #region 添加审批日志
                        ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                        ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

                        ESP.Framework.Entity.OperationAuditManageInfo oldManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(formInfo.OldGroupId);
                        ESP.Framework.Entity.OperationAuditManageInfo newManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(formInfo.NewGroupId);


                        //转出组总监=转出组总经理
                        if (oldManageModel.DirectorId == oldManageModel.ManagerId)
                        {
                            nextAuditor = newManageModel.DirectorId;
                            formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmOut;
                        }
                        //转出组总监=转入组总监
                        else if (oldManageModel.DirectorId == newManageModel.DirectorId)
                        {
                            if (oldManageModel.ManagerId == newManageModel.ManagerId)
                            {
                                nextAuditor = newManageModel.ManagerId;
                                formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.LeaderConfirmIn;
                            }
                            else
                            {
                                nextAuditor = oldManageModel.ManagerId;
                                formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ReceiverConfirm;
                            }
                        }
                        //转出组总监=转入组总经理
                        else if (oldManageModel.DirectorId == newManageModel.ManagerId)
                        {
                            if (oldManageModel.DirectorId == newManageModel.DirectorId)
                            {
                                nextAuditor = 0;
                                formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn;
                            }
                            else
                            {
                                nextAuditor = newManageModel.DirectorId;
                                formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmOut;
                            }
                        }
                        else
                        {
                            nextAuditor = oldManageModel.ManagerId;
                            formInfo.Status = (int)ESP.HumanResource.Common.Status.TransferStatus.ReceiverConfirm;
                        }

                        ESP.Framework.Entity.UserInfo nextHr = ESP.Framework.BusinessLogic.UserManager.Get(nextAuditor);
                        hrAuditLogInfo.AuditorId = nextHr.UserID;
                        hrAuditLogInfo.AuditorName = nextHr.FullNameCN;
                        hrAuditLogInfo.AuditLevel = formInfo.Status;
                        hrAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                        hrAuditLogInfo.FormId = formInfo.Id;
                        hrAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.TransferForm;

                        hrAuditLogDal.Add(hrAuditLogInfo, trans);
                        #endregion

                        dimFormDal.Update(formInfo, trans);


        #endregion
                    }

                    trans.Commit();
                    b = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    b = false;
                }
            }

            return b;
        }

    }
}
