using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using Excel = Microsoft.Office.Interop.Excel;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using System.Data.SqlClient;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;
using System.Text;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ReturnBLL 的摘要说明。
    /// </summary>


    public static class ReturnManager
    {
        //private readonly ESP.Finance.DataAccess.ReturnDAL dal=new ESP.Finance.DataAccess.ReturnDAL();

        private static ESP.Finance.IDataAccess.IReturnDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IReturnDataProvider>.Instance; } }
        //private const string _dalProviderName = "ReturnDALProvider";


        #region  成员方法

        public static int SettingOOPCurrentAudit(string returncode)
        {
            return DataProvider.SettingOOPCurrentAudit(returncode);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ReturnInfo model)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int ReturnID = DataProvider.Add(model, trans);
                    ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
                    datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Return;
                    datainfo.DataId = ReturnID;
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                    ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                    p.UserId = model.RequestorID.Value;
                    p.IsEditor = true;
                    p.IsViewer = true;
                    permissionList.Add(p);
                    ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList, trans);
                    trans.Commit();
                    return ReturnID;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static int DeleteWorkFlow(int returnid)
        {
            return DataProvider.DeleteWorkFlow(returnid);
        }

        public static void ChangedPaymentUser(int newPaymentUserId, int returnId)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ReturnInfo returnModel = GetModel(returnId);
                    ESP.Framework.Entity.EmployeeInfo newPaymentUser = ESP.Framework.BusinessLogic.EmployeeManager.Get(newPaymentUserId);
                    returnModel.PaymentUserID = newPaymentUser.UserID;
                    returnModel.PaymentEmployeeName = newPaymentUser.FullNameCN;
                    Update(returnModel, trans);
                    new DataAccess.ReturnAuditHistDataProvider().DeleteNotAudit(returnModel.ReturnID, trans);
                    ESP.Finance.Entity.ReturnAuditHistInfo AuditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                    AuditModel.ReturnID = returnId;
                    AuditModel.AuditType = ESP.Finance.Utility.auditorType.purchase_first;
                    AuditModel.AuditorUserID = newPaymentUser.UserID;
                    AuditModel.AuditorUserCode = newPaymentUser.Code;
                    AuditModel.AuditorEmployeeName = newPaymentUser.FullNameCN;
                    AuditModel.AuditorUserName = newPaymentUser.Code;
                    AuditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    new DataAccess.ReturnAuditHistDataProvider().Add(AuditModel, trans);
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// 创建费标准付款申请（如车马费、押金)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int CreateReturnInFinance(ReturnInfo model)
        {
            int ReturnID = 0;
            using (SqlConnection conn = new SqlConnection(ESP.Finance.DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    model.ReturnCode = new ESP.Finance.DataAccess.ReturnDataProvider().GetNewReturnCode(trans);//生成returnCode
                    if (string.IsNullOrEmpty(model.ReturnCode))
                    {
                        trans.Rollback();
                        throw new Exception("生成PN单号失败,请重试!");
                    }
                    ReturnID = new ESP.Finance.DataAccess.ReturnDataProvider().Add(model, trans);
                    //插入授权表
                    ESP.Finance.DataAccess.DbHelperSQL.ExecuteSql("insert into t_datainfo(datatype,dataid) values(5," + ReturnID.ToString() + ");", trans);
                    //取授权表ID
                    object datainfoID = ESP.Finance.DataAccess.DbHelperSQL.GetSingle("select id from t_datainfo where datatype=5 and dataid=" + ReturnID.ToString(), trans);
                    //申请人授权
                    ESP.Finance.DataAccess.DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + model.RequestorID + ",1,1)", trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // ESP.Logging.Logger.Add(ex.Message, "创建付款申请失败-CreateReturnInFinance", ESP.Logging.LogLevel.Error, ex);
                }
            }
            return ReturnID;
        }

        public static int CreateReturnDePayment(ReturnInfo model, int OldReturnID)
        {
            int ReturnID = 0;
            ESP.Finance.Entity.ReturnInfo oldModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(OldReturnID);
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);
            if (projectModel != null)
            {
                if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose)
                {
                    if (model.PreFee > oldModel.PreFee)
                    {
                        return 0;
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(ESP.Finance.DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    oldModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Receiving;
                    ESP.Finance.BusinessLogic.ReturnManager.Update(oldModel, trans);
                    string[] recipientIDs = model.RecipientIds.Split(',');
                    for (int i = 0; i < recipientIDs.Length; i++)
                    {
                        ESP.Purchase.Entity.RecipientInfo recipientModel = ESP.Purchase.BusinessLogic.RecipientManager.GetModel(Convert.ToInt32(recipientIDs[i]));
                        recipientModel.IsConfirm = (int)ESP.Purchase.Common.State.recipentConfirm_PaymentCommit;
                        ESP.Purchase.BusinessLogic.RecipientManager.Update(recipientModel);
                    }
                    model.ReturnCode = new ESP.Finance.DataAccess.ReturnDataProvider().GetNewReturnCode(trans);//生成returnCode
                    if (string.IsNullOrEmpty(model.ReturnCode))
                    {
                        trans.Rollback();
                        throw new Exception("生成PN单号失败,请重试!");
                    }
                    ReturnID = new ESP.Finance.DataAccess.ReturnDataProvider().Add(model, trans);
                    //插入授权表
                    ESP.Finance.DataAccess.DbHelperSQL.ExecuteSql("insert into t_datainfo(datatype,dataid) values(5," + ReturnID.ToString() + ");", trans);
                    //取授权表ID
                    object datainfoID = ESP.Finance.DataAccess.DbHelperSQL.GetSingle("select id from t_datainfo where datatype=5 and dataid=" + ReturnID.ToString(), trans);
                    //申请人授权
                    ESP.Finance.DataAccess.DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + model.RequestorID + ",1,1)", trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // ESP.Logging.Logger.Add(ex.Message, "创建付款申请失败-CreateReturnInFinance", ESP.Logging.LogLevel.Error, ex);
                }
            }
            return ReturnID;
        }

        public static int Add(ESP.Finance.Entity.ReturnInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            int ReturnID = DataProvider.Add(model, trans);
            //ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
            //datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Return;
            //datainfo.DataId = ReturnID;
            //List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
            //ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
            //p.UserId = model.RequestorID.Value;
            //p.IsEditor = true;
            //p.IsViewer = true;
            //permissionList.Add(p);
            //ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList);
            return ReturnID;
        }

        public static int UpdateIsInvoice(int returnID, int IsInvoice)
        {
            return DataProvider.UpdateIsInvoice(returnID, IsInvoice);
        }
        public static int Add(List<ESP.Finance.Entity.ReturnInfo> returnList)
        {
            return DataProvider.Add(returnList);
        }

        public static int UpdateDismission(ESP.Finance.Entity.ReturnInfo model, SqlTransaction trans)
        {
            return DataProvider.Update(model, trans);

        }
        public static int UpdateDismission(ESP.Finance.Entity.ReturnInfo model)
        {
            return DataProvider.Update(model);

        }

        public static UpdateResult Update(ESP.Finance.Entity.ReturnInfo model, SqlTransaction trans)
        {
            int res = 0;
            if (model.ReturnStatus == (int)PaymentStatus.FinanceComplete)
            {
                UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Payment(PaymentStatus.Over, model, trans);
                if (result == UpdateResult.Succeed)
                    res = DataProvider.Update(model, trans);
            }
            else
                res = DataProvider.Update(model, trans);

            ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
            datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Return;
            datainfo.DataId = model.ReturnID;
            List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(" returnid=" + model.ReturnID.ToString(), null, trans);
            ESP.Purchase.Entity.DataPermissionInfo prequest = new ESP.Purchase.Entity.DataPermissionInfo();
            prequest.UserId = model.RequestorID.Value;
            prequest.IsEditor = true;
            prequest.IsViewer = true;
            permissionList.Add(prequest);
            foreach (ESP.Finance.Entity.ReturnAuditHistInfo audit in auditList)
            {
                ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                p.UserId = audit.AuditorUserID.Value;
                p.IsEditor = true;
                p.IsViewer = true;
                permissionList.Add(p);
            }
            ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList, trans);
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

        public static int ConfirmTicketOrder(int returnId, List<ESP.Finance.Entity.ExpenseAccountDetailInfo> ticketlist)
        {
            int res = 0;
            ESP.Finance.Entity.ReturnInfo returnModel = GetModel(returnId);
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Ticket_ReceptionConfirm)
                    {
                        return 2;
                    }
                    decimal prefee = 0;
                    foreach (ESP.Finance.Entity.ExpenseAccountDetailInfo detail in ticketlist)
                    {
                        prefee += detail.ExpenseMoney.Value;
                    }

                    returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Ticket_Received;
                    returnModel.FactFee = prefee;
                    returnModel.PreFee = prefee;
                    ESP.Finance.Utility.UpdateResult result = Update(returnModel, trans);


                    if (result == ESP.Finance.Utility.UpdateResult.Succeed)
                    {
                        foreach (ESP.Finance.Entity.ExpenseAccountDetailInfo detail in ticketlist)
                        {
                            ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Update(detail, trans);
                        }

                        ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                        log.AuditeDate = DateTime.Now;
                        log.AuditorEmployeeName = "供应商:" + returnModel.SupplierName;
                        log.AuditorUserID = 0;
                        log.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                        log.ExpenseAuditID = returnModel.ReturnID;
                        log.Suggestion = "机票确认";
                        ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log, trans);
                        res = 1;
                    }
                    else
                        res = 0;

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    res = 0;
                }
            }
            return res;
        }

        public static UpdateResult Update(ESP.Finance.Entity.ReturnInfo model)
        {
            return Update(model, false);
        }

        public static UpdateResult Refund(ESP.Finance.Entity.ReturnInfo model,ESP.Purchase.Entity.OrderInfo orderModel, ESP.Finance.Entity.AuditLogInfo log)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    res = DataProvider.Update(model, trans);

                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log,trans);

                    ESP.Purchase.BusinessLogic.OrderInfoManager.update(orderModel, trans);

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

        public static UpdateResult Update(ESP.Finance.Entity.ReturnInfo model, bool IsSaveLog)
        {
            int res = 0;
            ESP.Purchase.Entity.GeneralInfo generalModel = null;
            if (model.PRID!=null && model.PRID != 0)
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value);

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model.ReturnStatus == (int)PaymentStatus.FinanceComplete)
                    {
                        UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Payment(PaymentStatus.Over, model);
                        if (result == UpdateResult.Succeed)
                            res = DataProvider.Update(model, trans);
                    }
                    else
                        res = DataProvider.Update(model, trans);
                    
                    ESP.Finance.Entity.PNBatchRelationInfo relationMoel = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetModelByReturnId(model.ReturnID, trans);
                    if (relationMoel != null)
                    {
                        ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(relationMoel.BatchID.Value);
                        batchModel.Amounts = ESP.Finance.BusinessLogic.PNBatchManager.GetTotalAmounts(batchModel.BatchID, trans);
                        ESP.Finance.BusinessLogic.PNBatchManager.UpdateAmounts(batchModel, null);
                    }

                    if (generalModel!=null && (generalModel.account_bank != model.SupplierBankName || generalModel.account_number != model.SupplierBankAccount))
                    {
                        generalModel.account_bank = model.SupplierBankName;
                        generalModel.account_number = model.SupplierBankAccount;
                        ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel,conn, trans);
                    }

                    ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
                    datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Return;
                    datainfo.DataId = model.ReturnID;
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                    IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(" returnid=" + model.ReturnID.ToString(), null);
                    ESP.Purchase.Entity.DataPermissionInfo prequest = new ESP.Purchase.Entity.DataPermissionInfo();
                    prequest.UserId = model.RequestorID.Value;
                    prequest.IsEditor = true;
                    prequest.IsViewer = true;
                    permissionList.Add(prequest);
                    foreach (ESP.Finance.Entity.ReturnAuditHistInfo audit in auditList)
                    {
                        ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                        p.UserId = audit.AuditorUserID.Value;
                        p.IsEditor = true;
                        p.IsViewer = true;
                        permissionList.Add(p);
                        if (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit && audit.AuditType.Value == (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial)
                        {
                            audit.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Update(audit);
                        }
                    }
                    ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList);
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
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ReturnInfo model, List<int> MediaList, int CurrentUserID)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model.ReturnStatus == (int)PaymentStatus.FinanceComplete)
                    {
                            UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Payment(PaymentStatus.Over, model, trans);
                            if (result == UpdateResult.Succeed)
                                res = DataProvider.Update(model, trans);
                    }
                    else
                        res = DataProvider.Update(model, trans);

                    ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
                    datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Return;
                    datainfo.DataId = model.ReturnID;
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                    IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(" returnid=" + model.ReturnID.ToString(), null, trans);
                    ESP.Purchase.Entity.DataPermissionInfo prequest = new ESP.Purchase.Entity.DataPermissionInfo();
                    prequest.UserId = model.RequestorID.Value;
                    prequest.IsEditor = true;
                    prequest.IsViewer = true;
                    permissionList.Add(prequest);
                    foreach (ESP.Finance.Entity.ReturnAuditHistInfo audit in auditList)
                    {
                        ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                        p.UserId = audit.AuditorUserID.Value;
                        p.IsEditor = true;
                        p.IsViewer = true;
                        permissionList.Add(p);
                    }
                    ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList, trans);

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

        public static ESP.Finance.Utility.UpdateResult Payment(ESP.Finance.Utility.PaymentStatus status, ESP.Finance.Entity.ReturnInfo model)
        {

            int res = 0;
            res = DataProvider.Payment(status, model);

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

        public static ESP.Finance.Utility.UpdateResult Payment(ESP.Finance.Utility.PaymentStatus status, ESP.Finance.Entity.ReturnInfo model, SqlTransaction trans)
        {

            int res = 0;
            res = DataProvider.Payment(status, model, trans);

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
                //trans//res = DataProvider.UpdateProjectCode(projectId, projectCode, true);
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
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int ReturnID)
        {

            int res = 0;
            try
            {
                res = DataProvider.Delete(ReturnID);
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





        //public static ESP.Finance.Utility.UpdateResult UpdateWorkFlow(int ReturnID, int workItemID, string workItemName, int processID, int instanceID)
        //{
        //    //ReturnInfo model = GetModel(ReturnID);
        //    int res = 0;
        //    try
        //    {
        //        //trans//res = DataProvider.UpdateWorkFlow(ReturnID, workItemID, workItemName, processID, instanceID, true);
        //        res = DataProvider.UpdateWorkFlow(ReturnID, workItemID, workItemName, processID, instanceID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw ex;
        //    }
        //    if (res > 0)
        //    {
        //        //LogManager.Add("Update", _tableName, "工作流");
        //        return UpdateResult.Succeed;
        //    }
        //    else if (res == 0)
        //    {
        //        return UpdateResult.UnExecute;
        //    }
        //    return UpdateResult.Failed;
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ReturnInfo GetModel(int ReturnID)
        {

            return DataProvider.GetModel(ReturnID);
        }
        public static ESP.Finance.Entity.ReturnInfo GetModel(int ReturnID, SqlTransaction trans)
        {

            return DataProvider.GetModel(ReturnID, trans);
        }

        /// <summary>
        /// 撤销付款申请至采购系统
        /// </summary>
        /// <param name="returnId"></param>
        /// <returns></returns>
        public static bool returnPaymentInfo(int returnId)
        {
            return DataProvider.returnPaymentInfo(returnId);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ReturnInfo> GetAllList()
        {
            return GetList(null);
        }

        public static IList<ReturnInfo> GetDimissionList(string term, SqlTransaction trans)
        {
            return DataProvider.GetList(term, trans);
        }

        public static IList<ReturnInfo> GetDimissionList(string term)
        {
            return DataProvider.GetList(term, new List<SqlParameter>());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ReturnInfo> GetList(string term)
        {
            return GetList(term, null);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ReturnInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static int GetRecordsCount(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetRecordsCount(strWhere, parms);
        }

        public static List<ReturnInfo> GetModelListPage(int pageSize, int pageIndex,
    string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetModelListPage(pageSize, pageIndex, strWhere, parms);
        }




        public static IList<ReturnInfo> GetList(int projectId, int departmentId)
        {
            var s = " ProjectID=" + projectId + " and DepartmentId=" + departmentId + " and  ReturnStatus != 0 and ReturnStatus != 1 and ReturnStatus != -1 ";
            return DataProvider.GetList(s, new List<SqlParameter>());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ReturnInfo> GetList(string term, List<SqlParameter> param, int start, int count, out int total)
        {
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();

            strSql.Append(@"select * from (");
            strSql.Append("select row_number() over (order by Lastupdatetime desc) AS n, * from F_Return ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(@") t where n >= ").Append(start);

            strSql.Append(" order by ReturnCode desc ");
            var sql = strSql.ToString();

            IList<ESP.Finance.Entity.ReturnInfo> returnlist = null;

            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql, param))
            {
                returnlist = MyPhotoUtility.CBO.FillCollection<ReturnInfo>(reader);
            }

            total = returnlist.Count;

            return returnlist.Take(count).ToList();
        }
        public static int GetCount(string term, List<SqlParameter> param)
        {
            return DataProvider.GetCount(term, param);
        }
        public static IList<ReturnInfo> GetListByIds(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return new List<ReturnInfo>();

            var term = new System.Text.StringBuilder(" ReturnID in (");
            term.Append(ids[0]);
            for (var i = 1; i < ids.Length; i++)
            {
                term.Append(",").Append(ids[i]);
            }
            term.Append(") ");

            return GetList(term.ToString());
        }
        /// <summary>
        /// 获得关联PR的PN列表
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetPNTableLinkPR(string terms, List<System.Data.SqlClient.SqlParameter> parms)
        {
            return DataProvider.GetPNTableLinkPR(terms, parms);
        }

        public static DataTable GetPNTableForPurchasePN(string terms, List<SqlParameter> parms)
        {
            return DataProvider.GetPNTableForPurchasePN(terms, parms);
        }

        /// <summary>
        /// 根据returnId获得收货ID
        /// </summary>
        /// <param name="returnIds"></param>
        /// <returns></returns>
        public static DataTable GetRecipientIds(string returnIds)
        {
            return DataProvider.GetRecipientIds(returnIds);
        }

        /// <summary>
        /// 抵消押金
        /// </summary>
        /// <param name="returnList"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool updataSatatusAndAddKillForegift(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Finance.Entity.ForeGiftLinkInfo foregift, int status)
        {
            return DataProvider.updataSatatusAndAddKillForegift(returnModel, foregift, status);
        }

        /// <summary>
        /// 是否以创建押金申请
        /// </summary>
        /// <param name="prId"></param>
        /// <returns></returns>
        public static bool isHaveForeGift(int prId)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string terms = " PRID=@PRID and returnType=@returnType";
            parms.Add(new SqlParameter("@PRID", prId));
            parms.Add(new SqlParameter("@returnType", (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift));
            if (GetList(terms, parms).Count > 0)
                return true;
            else
                return false;
        }
        #endregion 获得数据列表

        #endregion  成员方法

        #region 新增方法

        public static DataTable GetProxyReturnList(string strwhere)
        {
            return DataProvider.GetProxyReturnList(strwhere);

        }

        public static string ExportProxyReturnList(int userid, DataTable dt, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/MediaTemplate/ProxyPnTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            try
            {
                excel.Load(sourceFile);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
                int rownum = 2;
                string cell = "A2";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //PN单号	项目号	费用发生日期	申请人	员工编码	费用所属组	供应商名称	费用明细描述	申请金额
                    // returncode,projectcode,prebegindate,requestemployeename,b.code,c.departmentname,a.suppliername,a.returncontent,a.prefee
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["returncode"].ToString());

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["projectcode"].ToString());

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["prebegindate"].ToString());

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["requestemployeename"].ToString());

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + dt.Rows[i]["code"].ToString());

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["departmentname"].ToString());

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["suppliername"].ToString());

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["returncontent"].ToString());

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, Convert.ToDecimal(dt.Rows[i]["prefee"]).ToString("f2"));

                    rownum++;
                }


                string serverpath = Common.GetLocalPath("/Tmp/MediaTemplate");
                if (!System.IO.Directory.Exists(serverpath))
                    System.IO.Directory.CreateDirectory(serverpath);
                string desFilename = Guid.NewGuid().ToString() + ".xls";
                string desFile = serverpath + "/" + desFilename;
                string desPath = "/Tmp/MediaTemplate/" + desFilename;
                ExcelHandle.SaveAS(excel.CurBook, desFile);
                excel.Dispose();
                ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
                return desPath;
            }
            catch
            {
                excel.Dispose();
                return "";
            }
        }

        public static string ExportBatchData(int userid, int batchid, string serverpath, out string ofname)
        {
            ofname = "PnBatch" + DateTime.Now.Ticks.ToString() + ".xls";

            if (System.IO.File.Exists(serverpath + ofname))//如果有则删除
            {
                try
                {
                    System.IO.File.Delete(serverpath + ofname);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            string temppath = serverpath + "BachPNTemplate.xls";
            ExcelHandle excel = new ExcelHandle();
            //try
            //{
            excel.Load(temppath);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
            int rownum = 2;
            string cell = "A2";

            IList<ESP.Finance.Entity.PNBatchRelationInfo> batchlist = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchid=" + batchid.ToString(), new List<SqlParameter>());
            string media3000downIds = string.Empty;
            string cashPnIds = string.Empty;
            string media3000upIds = string.Empty;
            foreach (ESP.Finance.Entity.PNBatchRelationInfo model in batchlist)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.ReturnID.Value);
                if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
                {
                    if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)
                        media3000downIds += returnModel.MediaOrderIDs + ",";
                    else if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                        media3000upIds += returnModel.ReturnID + ",";
                }
                else
                {
                    cashPnIds += model.ReturnID.ToString() + ",";
                }
            }

            media3000downIds = media3000downIds.TrimEnd(',');
            media3000upIds = media3000upIds.TrimEnd(',');
            cashPnIds = cashPnIds.TrimEnd(',');

            #region Write Media Data
            DataTable dtMedia = null;
            DataTable dtMedia300Up = null;
            DataTable dtCash = null;

            if (!string.IsNullOrEmpty(media3000downIds))
                dtMedia = ESP.Purchase.BusinessLogic.MediaOrderManager.GetBatchMedia3000downInfo(media3000downIds);
            if (!string.IsNullOrEmpty(media3000upIds))
                dtMedia300Up = ESP.Purchase.BusinessLogic.MediaOrderManager.GetBatchMedia3000upInfo(media3000upIds);
            if (!string.IsNullOrEmpty(cashPnIds))
                dtCash = ESP.Purchase.BusinessLogic.MediaOrderManager.GetBatchCashInfo(cashPnIds);
            if (dtMedia != null)
            {
                for (int i = 0; i < dtMedia.Rows.Count; i++)
                {
                    //项目号	费用发生日期	申请人	费用所属组	记者姓名	出版社	金额	银行卡号	身份证号码	PR号
                    //a.project_code,a1.begindate,a.requestorname,department,c.reportername,c.receivername,c.medianame,
                    //c.totalamount,c.bankaccountname,c.cardnumber,a.prno
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["ReturnID"].ToString());
                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["ReturnCode"].ToString());
                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["prno"].ToString());
                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["project_code"].ToString());

                    string releaseDate = string.Empty;
                    if (!string.IsNullOrEmpty(dtMedia.Rows[i]["releasedate"].ToString()))
                        releaseDate = dtMedia.Rows[i]["releasedate"].ToString();
                    else
                        releaseDate = dtMedia.Rows[i]["begindate"].ToString();

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, releaseDate);

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["requestorname"].ToString());

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["department"].ToString());

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["receivername"].ToString());

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["medianame"].ToString());

                    cell = string.Format("J{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["totalamount"].ToString());

                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + dtMedia.Rows[i]["bankaccountname"].ToString());

                    cell = string.Format("L{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + dtMedia.Rows[i]["cardnumber"].ToString());

                    cell = string.Format("M{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["bankname"].ToString());

                    rownum++;
                }
            }
            if (dtMedia300Up != null)
            {
                for (int i = 0; i < dtMedia300Up.Rows.Count; i++)
                {
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["ReturnID"].ToString());

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["ReturnCode"].ToString());

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["prno"].ToString());

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["project_code"].ToString());

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["begindate"].ToString());

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["requestorname"].ToString());

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["department"].ToString());

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["receivername"].ToString());

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["medianame"].ToString());

                    cell = string.Format("J{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia300Up.Rows[i]["totalamount"].ToString());

                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + dtMedia300Up.Rows[i]["bankaccountname"].ToString());

                    cell = string.Format("L{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + dtMedia300Up.Rows[i]["cardnumber"].ToString());

                    cell = string.Format("M{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtMedia.Rows[i]["bankname"].ToString());

                    rownum++;
                }
            }

            #endregion

            #region write Cash Data

            if (dtCash != null)
            {
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[2];
                rownum = 2;
                for (int i = 0; i < dtCash.Rows.Count; i++)
                {
                    //项目号	费用发生日期	申请人	费用所属组	兼职姓名	兼职内容	金额	银行卡号	身份证号码	PR号
                    // a.projectcode,a.returnpredate,b.requestorname,b.departmentid,b.department
                    //a.suppliername,c.desctiprtion,a.prefee,a.supplierbankaccount,b.supplier_address,b.prno
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["ReturnID"].ToString());

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["ReturnCode"].ToString());

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["prno"].ToString());

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["projectcode"].ToString());

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["returnpredate"].ToString());

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["requestorname"].ToString());

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["department"].ToString());

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["suppliername"].ToString());

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["desctiprtion"].ToString());

                    cell = string.Format("J{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtCash.Rows[i]["prefee"].ToString());

                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + dtCash.Rows[i]["supplierbankaccount"].ToString());

                    cell = string.Format("L{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + dtCash.Rows[i]["supplier_address"].ToString());
                    rownum++;

                }
            }
            #endregion

            ExcelHandle.SaveAS(excel.CurBook, serverpath + ofname);
            excel.Dispose();
            return serverpath + ofname;
            //}
            //catch
            //{
            //    excel.Dispose();
            //    throw;
            //}
        }

        public static string ExportPCCredence(int userid, BranchInfo branchModel, IList<ReturnInfo> returnlist, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/ExpenseAccount/PCTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            string drcell = "L81";
            string crcell = "M81";

            //try
            //{
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];

            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
            int rownum = 82;
            string cell = string.Empty;
            //数据库
            cell = "G77";
            ExcelHandle.WriteCell(excel.CurSheet, cell, branchModel.DBCode);
            //凭证类型
            cell = "K77";
            ExcelHandle.WriteCell(excel.CurSheet, cell, "PC");
            //凭证编制
            ESP.Finance.Entity.CredenceInfo credenceModel = ESP.Finance.BusinessLogic.CredenceManager.GetModel(userid);
            cell = "L77";
            ExcelHandle.WriteCell(excel.CurSheet, cell, credenceModel.Code);



            foreach (ReturnInfo model in returnlist)
            {
                ESP.Purchase.Entity.GeneralInfo generalModel = null;
                string desc = string.Empty;
                ESP.Finance.Entity.FinanceObjectInfo financeObjectModel = null;
                ESP.Finance.Entity.FinanceObjectInfo financeObjectModel2 = null;
                ESP.Finance.Entity.DepartmentViewInfo deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.DepartmentID.Value);
                ESP.Finance.Entity.ToGroupInfo togroupModel = ESP.Finance.BusinessLogic.ToGroupManager.GetModel(deptModel.level2Id);
                ESP.Compatible.Employee empModel = new ESP.Compatible.Employee(model.RequestorID.Value);

                ESP.Finance.Entity.ProjectInfo projectModel = null;
                ESP.Finance.Entity.CustomerTmpInfo customerModel = null;
                if (model.ProjectID != null && model.ProjectID.Value != 0)
                {
                    projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);
                    customerModel = ESP.Finance.BusinessLogic.CustomerTmpManager.GetModel(projectModel.CustomerID.Value);
                }
                //General
                //YJ
                //ThirdParty
                //Others
                //david.zhang
                if (model != null && model.PRID != null && model.PRID.Value != 0)
                {
                    generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value);
                    ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(generalModel.id);
                    ESP.Finance.Entity.SupplierFinanceRelationInfo supplierModel = null;
                    if (orderModel != null && orderModel.supplierId != null && orderModel.supplierId != 0)
                    {
                        supplierModel = ESP.Finance.BusinessLogic.SupplierFinanceRelationManager.GetModel(orderModel.supplierId);
                    }
                    ESP.Finance.Entity.MaterialFinanceRelationInfo materialModel = ESP.Finance.BusinessLogic.MaterialFinanceRelationManager.GetModel(orderModel.producttype, 1);
                    if (supplierModel == null)
                        desc = materialModel.FinanceObjectName;
                    else
                        desc = supplierModel.ShortName + ":" + materialModel.FinanceObjectName;
                }
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow )
                {
                    desc = model.RequestEmployeeName + "借款:";
                }

                if (model.RequestorID == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["DavidZhangID"]))
                {
                    financeObjectModel = ESP.Finance.BusinessLogic.FinanceObjectManager.GetModel("PC", 1, "david.zhang");
                }
                else
                {
                    financeObjectModel = ESP.Finance.BusinessLogic.FinanceObjectManager.GetModel("PC", 1, "General");
                }
                financeObjectModel2 = ESP.Finance.BusinessLogic.FinanceObjectManager.GetModel("PC", 2, "人民币");

                #region "现金第一行信息"
                //业务参考
                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //业务日期2009/9/11
                cell = string.Format("C{0}", rownum);
                string factdate = model.ReturnFactDate == null ? "" : model.ReturnFactDate.Value.ToString("yyyy/MM/dd");
                ExcelHandle.WriteCell(excel.CurSheet, cell, factdate);
                //描述
                cell = string.Format("E{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, desc);
                ExcelHandle.SetBold(excel.CurSheet, cell);
                //会计科目代码
                cell = string.Format("G{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, financeObjectModel.ObjectCode);
                //科目名称
                cell = string.Format("H{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, financeObjectModel.ObjectName);
                ExcelHandle.SetBold(excel.CurSheet, cell);
                //金额
                decimal fee = model.FactFee == null ? model.PreFee.Value : model.FactFee.Value;
                cell = string.Format("K{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, fee.ToString("f2"));
                //借方（Dr.）
                cell = string.Format("L{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($K" + rownum.ToString() + "<0,0,ROUND($K" + rownum.ToString() + ",2))");
                //贷方(Cr.)
                //=IF($K86>0,0,ROUND(-$K86,2))
                cell = string.Format("M{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($K" + rownum.ToString() + ">0,0,ROUND(-$K" + rownum.ToString() + ",2))");
                //借贷合计
                cell = string.Format("N{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($L" + rownum.ToString() + ">0,L" + rownum.ToString() + ",-M" + rownum.ToString() + ")");
                //T0组
                cell = string.Format("O{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, togroupModel.ToCode);
                //T1员工
                cell = string.Format("P{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + empModel.ID);
                //T2项目来源
                cell = string.Format("Q{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //T3客户
                string t3 = customerModel == null ? "" : customerModel.CustomerCode;
                cell = string.Format("R{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, t3);
                //T4项目类型
                string t4 = projectModel == null ? "" : projectModel.ProjectTypeCode;
                cell = string.Format("S{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, t4);
                //T5项目号
                string t5 = string.Empty;
                if (projectModel != null)
                {
                    string[] str = projectModel.ProjectCode.Split('-');
                    t5 = str[0] + str[3];
                }
                cell = string.Format("T{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, t5);
                //T6收入/成本
                cell = string.Format("U{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                ExcelHandle.SetBorderRightLine(excel.CurSheet, cell, 3);

                #region 检查用
                //T0
                //=IF(AND(AR82="Y",OR(O82="-",O82="")),"必填",IF(AND(AR82=0,O82=""),"请选留空",""))
                cell = string.Format("AI{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AR" + rownum.ToString() + "=\"Y\",OR(O" + rownum.ToString() + "=\"-\",O" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AR" + rownum.ToString() + "=0,O" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");
                //T1
                //=IF(AND(AS82="Y",OR(P82="-",P82="")),"必填",IF(AND(AS82=0,P82=""),"请选留空",""))
                cell = string.Format("AJ{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AS" + rownum.ToString() + "=\"Y\",OR(P" + rownum.ToString() + "=\"-\",P" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AS" + rownum.ToString() + "=0,P" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //T2
                //=IF(AND(AT82="Y",OR(Q82="-",Q82="")),"必填",IF(AND(AT82=0,Q82=""),"请选留空",""))
                cell = string.Format("AK{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AT" + rownum.ToString() + "=\"Y\",OR(Q" + rownum.ToString() + "=\"-\",Q" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AT" + rownum.ToString() + "=0,Q" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //T3
                //=IF(AND(AU82="Y",OR(R82="-",R82="")),"必填",IF(AND(AU82=0,R82=""),"请选留空",""))
                cell = string.Format("AL{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AU" + rownum.ToString() + "=\"Y\",OR(R" + rownum.ToString() + "=\"-\",R" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AU" + rownum.ToString() + "=0,R" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //T4
                //=IF(AND(AV82="Y",OR(S82="-",S82="")),"必填",IF(AND(AV82=0,S82=""),"请选留空",""))
                cell = string.Format("AM{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AV" + rownum.ToString() + "=\"Y\",OR(S" + rownum.ToString() + "=\"-\",S" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AV" + rownum.ToString() + "=0,S" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");
                //T5
                //=IF(AND(AW82="Y",OR(T82="-",T82="")),"必填",IF(AND(AW82=0,T82=""),"请选留空",""))
                cell = string.Format("AN{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AW" + rownum.ToString() + "=\"Y\",OR(T" + rownum.ToString() + "=\"-\",T" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AW" + rownum.ToString() + "=0,T" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //T6
                //=IF(AND(AX82="Y",OR(U82="-",U82="")),"必填",IF(AND(AX82=0,U82=""),"请选留空",""))
                cell = string.Format("AO{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AX" + rownum.ToString() + "=\"Y\",OR(U" + rownum.ToString() + "=\"-\",U" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AX" + rownum.ToString() + "=0,U" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //数据库对应科目代码
                //=IF($AL$77="管理",$I82,$G82)
                cell = string.Format("AP{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AL$77=\"管理\",$I" + rownum.ToString() + ",$G" + rownum.ToString() + ")");

                ////=IF($AP82=0,"",VLOOKUP($AP82,'\\信息更新.xls科目表'!$B$5:$J$5000,3,FALSE))
                cell = string.Format("AR{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.Code0 + "\")");

                cell = string.Format("AS{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code1 + "\")");

                cell = string.Format("AT{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code2 + "\")");

                cell = string.Format("AU{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code3 + "\")");

                cell = string.Format("AV{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code4 + "\")");

                cell = string.Format("AW{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code5 + "\")");

                cell = string.Format("AX{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code6 + "\")");

                #endregion

                rownum++;
                #endregion

                #region "现金第二行信息"
                //业务参考
                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //业务日期
                cell = string.Format("C{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReturnFactDate.Value.ToString("yyyy/MM/dd"));
                //描述
                cell = string.Format("E{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, desc);
                //会计科目代码
                cell = string.Format("G{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, financeObjectModel2.ObjectCode);
                //科目名称
                cell = string.Format("H{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, financeObjectModel2.ObjectName);
                //金额
                cell = string.Format("K{0}", rownum);
                ExcelHandle.SetColor(excel.CurSheet, cell, System.Drawing.Color.Red);
                ExcelHandle.WriteCell(excel.CurSheet, cell, (0 - fee).ToString("f2"));
                //借方（Dr.）
                cell = string.Format("L{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($K" + rownum.ToString() + "<0,0,ROUND($K" + rownum.ToString() + ",2))");
                //贷方(Cr.)
                cell = string.Format("M{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($K" + rownum.ToString() + ">0,0,ROUND(-$K" + rownum.ToString() + ",2))");
                //借贷合计
                cell = string.Format("N{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($L" + rownum.ToString() + ">0,L" + rownum.ToString() + ",-M" + rownum.ToString() + ")");
                //T0组
                cell = string.Format("O{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //T1员工
                cell = string.Format("P{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //T2项目来源
                cell = string.Format("Q{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //T3客户
                cell = string.Format("R{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //T4项目类型
                cell = string.Format("S{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //T5项目号
                cell = string.Format("T{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                //T6收入/成本
                cell = string.Format("U{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                ExcelHandle.SetBorderRightLine(excel.CurSheet, cell, 3);


                #region 检查用
                //T0
                //=IF(AND(AR82="Y",OR(O82="-",O82="")),"必填",IF(AND(AR82=0,O82=""),"请选留空",""))
                cell = string.Format("AI{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AR" + rownum.ToString() + "=\"Y\",OR(O" + rownum.ToString() + "=\"-\",O" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AR" + rownum.ToString() + "=0,O" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");
                //T1
                //=IF(AND(AS82="Y",OR(P82="-",P82="")),"必填",IF(AND(AS82=0,P82=""),"请选留空",""))
                cell = string.Format("AJ{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AS" + rownum.ToString() + "=\"Y\",OR(P" + rownum.ToString() + "=\"-\",P" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AS" + rownum.ToString() + "=0,P" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //T2
                //=IF(AND(AT82="Y",OR(Q82="-",Q82="")),"必填",IF(AND(AT82=0,Q82=""),"请选留空",""))
                cell = string.Format("AK{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AT" + rownum.ToString() + "=\"Y\",OR(Q" + rownum.ToString() + "=\"-\",Q" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AT" + rownum.ToString() + "=0,Q" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //T3
                //=IF(AND(AU82="Y",OR(R82="-",R82="")),"必填",IF(AND(AU82=0,R82=""),"请选留空",""))
                cell = string.Format("AL{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AU" + rownum.ToString() + "=\"Y\",OR(R" + rownum.ToString() + "=\"-\",R" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AU" + rownum.ToString() + "=0,R" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //T4
                //=IF(AND(AV82="Y",OR(S82="-",S82="")),"必填",IF(AND(AV82=0,S82=""),"请选留空",""))
                cell = string.Format("AM{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AV" + rownum.ToString() + "=\"Y\",OR(S" + rownum.ToString() + "=\"-\",S" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AV" + rownum.ToString() + "=0,S" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");
                //T5
                //=IF(AND(AW82="Y",OR(T82="-",T82="")),"必填",IF(AND(AW82=0,T82=""),"请选留空",""))
                cell = string.Format("AN{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AW" + rownum.ToString() + "=\"Y\",OR(T" + rownum.ToString() + "=\"-\",T" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AW" + rownum.ToString() + "=0,T" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //T6
                //=IF(AND(AX82="Y",OR(U82="-",U82="")),"必填",IF(AND(AX82=0,U82=""),"请选留空",""))
                cell = string.Format("AO{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AX" + rownum.ToString() + "=\"Y\",OR(U" + rownum.ToString() + "=\"-\",U" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AX" + rownum.ToString() + "=0,U" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                //数据库对应科目代码
                //=IF($AL$77="管理",$I82,$G82)
                cell = string.Format("AP{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AL$77=\"管理\",$I" + rownum.ToString() + ",$G" + rownum.ToString() + ")");

                cell = string.Format("AR{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.Code0 + "\")");

                cell = string.Format("AS{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code1 + "\")");

                cell = string.Format("AT{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code2 + "\")");

                cell = string.Format("AU{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code3 + "\")");

                cell = string.Format("AV{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code4 + "\")");

                cell = string.Format("AW{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code5 + "\")");

                cell = string.Format("AX{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code6 + "\")");

                #endregion

                rownum++;

                #region 报销明细
                if (model.ReturnType == 30 || model.ReturnType == 32)
                {
                    IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> detaillist = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnid=" + model.ReturnID.ToString());
                    foreach (ESP.Finance.Entity.ExpenseAccountDetailInfo detail in detaillist)
                    {
                        ESP.Finance.Entity.MaterialFinanceRelationInfo material = ESP.Finance.BusinessLogic.MaterialFinanceRelationManager.GetModel(detail.ExpenseType.Value, 2);
                        //业务参考
                        cell = string.Format("A{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                        //业务日期
                        cell = string.Format("C{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReturnFactDate.Value.ToString("yyyy/MM/dd"));
                        //描述
                        cell = string.Format("E{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, detail.ExpenseDesc);
                        //会计科目代码
                        cell = string.Format("G{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, financeObjectModel2.ObjectCode);
                        //科目名称
                        cell = string.Format("H{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, material.FinanceObjectName);
                        //金额
                        fee = detail.ExpenseMoney.Value;
                        cell = string.Format("K{0}", rownum);
                        ExcelHandle.SetColor(excel.CurSheet, cell, System.Drawing.Color.Red);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, fee.ToString("f2"));

                        //借方（Dr.）=IF($K108<0,0,ROUND($K108,2))
                        cell = string.Format("L{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($K" + rownum.ToString() + "<0,0,ROUND($K" + rownum.ToString() + ",2))");

                        //贷方(Cr.) =IF($K108>0,0,ROUND(-$K108,2))
                        cell = string.Format("M{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($K" + rownum.ToString() + ">0,0,ROUND(-$K" + rownum.ToString() + ",2))");

                        //借贷合计  =IF(L108>0,L108,-M108)
                        cell = string.Format("N{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($L" + rownum.ToString() + ">0,L" + rownum.ToString() + ",-M" + rownum.ToString() + ")");

                        //T0组
                        cell = string.Format("O{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "-");
                        //T1员工
                        cell = string.Format("P{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "-");
                        //T2项目来源
                        cell = string.Format("Q{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "-");
                        //T3客户
                        cell = string.Format("R{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "-");
                        //T4项目类型
                        cell = string.Format("S{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "-");
                        //T5项目号
                        cell = string.Format("T{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "-");
                        //T6收入/成本
                        cell = string.Format("U{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "-");
                        ExcelHandle.SetBorderRightLine(excel.CurSheet, cell, 3);


                        #region 检查用
                        //T0
                        //=IF(AND(AR82="Y",OR(O82="-",O82="")),"必填",IF(AND(AR82=0,O82=""),"请选留空",""))
                        cell = string.Format("AI{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AR" + rownum.ToString() + "=\"Y\",OR(O" + rownum.ToString() + "=\"-\",O" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AR" + rownum.ToString() + "=0,O" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");
                        //T1
                        //=IF(AND(AS82="Y",OR(P82="-",P82="")),"必填",IF(AND(AS82=0,P82=""),"请选留空",""))
                        cell = string.Format("AJ{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AS" + rownum.ToString() + "=\"Y\",OR(P" + rownum.ToString() + "=\"-\",P" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AS" + rownum.ToString() + "=0,P" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                        //T2
                        //=IF(AND(AT82="Y",OR(Q82="-",Q82="")),"必填",IF(AND(AT82=0,Q82=""),"请选留空",""))
                        cell = string.Format("AK{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AT" + rownum.ToString() + "=\"Y\",OR(Q" + rownum.ToString() + "=\"-\",Q" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AT" + rownum.ToString() + "=0,Q" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                        //T3
                        //=IF(AND(AU82="Y",OR(R82="-",R82="")),"必填",IF(AND(AU82=0,R82=""),"请选留空",""))
                        cell = string.Format("AL{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AU" + rownum.ToString() + "=\"Y\",OR(R" + rownum.ToString() + "=\"-\",R" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AU" + rownum.ToString() + "=0,R" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                        //T4
                        //=IF(AND(AV82="Y",OR(S82="-",S82="")),"必填",IF(AND(AV82=0,S82=""),"请选留空",""))
                        cell = string.Format("AM{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AV" + rownum.ToString() + "=\"Y\",OR(S" + rownum.ToString() + "=\"-\",S" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AV" + rownum.ToString() + "=0,S" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");
                        //T5
                        //=IF(AND(AW82="Y",OR(T82="-",T82="")),"必填",IF(AND(AW82=0,T82=""),"请选留空",""))
                        cell = string.Format("AN{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AW" + rownum.ToString() + "=\"Y\",OR(T" + rownum.ToString() + "=\"-\",T" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AW" + rownum.ToString() + "=0,T" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                        //T6
                        //=IF(AND(AX82="Y",OR(U82="-",U82="")),"必填",IF(AND(AX82=0,U82=""),"请选留空",""))
                        cell = string.Format("AO{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF(AND(AX" + rownum.ToString() + "=\"Y\",OR(U" + rownum.ToString() + "=\"-\",U" + rownum.ToString() + "=\"\")),\"必填\",IF(AND(AX" + rownum.ToString() + "=0,U" + rownum.ToString() + "=\"\"),\"请选留空\",\"\"))");

                        //数据库对应科目代码
                        //=IF($AL$77="管理",$I82,$G82)
                        cell = string.Format("AP{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AL$77=\"管理\",$I" + rownum.ToString() + ",$G" + rownum.ToString() + ")");

                        cell = string.Format("AR{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.Code0 + "\")");

                        cell = string.Format("AS{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code1 + "\")");

                        cell = string.Format("AT{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code2 + "\")");

                        cell = string.Format("AU{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code3 + "\")");

                        cell = string.Format("AV{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code4 + "\")");

                        cell = string.Format("AW{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code5 + "\")");

                        cell = string.Format("AX{0}", rownum);
                        ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AP" + rownum.ToString() + "=0,\"\",\"" + financeObjectModel.code6 + "\")");
                        #endregion

                        rownum++;
                    }

                }
                #endregion

                #endregion
            }
            cell = string.Format("U{0}", rownum);
            ExcelHandle.SetBorderRightLine(excel.CurSheet, cell, 3);

            ExcelHandle.SetFormula(excel.CurSheet, cell, "=IF($AL$77=\"星言\",A82,IF($AL$77=\"管理\",B82,\"\"))");

            //=ROUND(SUM(L82:L886),2)
            ExcelHandle.SetFormula(excel.CurSheet, drcell, "=ROUND(SUM(L82:L" + rownum.ToString() + "),2)");
            //=ROUND(SUM(M82:M886),2)
            ExcelHandle.SetFormula(excel.CurSheet, crcell, "=ROUND(SUM(M82:M" + rownum.ToString() + "),2)");
            //=SUM(L81:M81)/2
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "Q79", "R79", System.Drawing.Color.Yellow);
            ExcelHandle.SetFormula(excel.CurSheet, "Q79", "=SUM(L81:M81)/2");

            //=IF(L81=M81,"-","借贷不相等")
            ExcelHandle.SetFormula(excel.CurSheet, "N79", "=IF(L81=M81,\"-\",\"借贷不相等\")");

            rownum++;
            string startcell = string.Format("A{0}", rownum);
            string endcell = string.Format("U{0}", rownum);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, startcell, endcell, System.Drawing.Color.Gray);
            ExcelHandle.SetBorderRightLine(excel.CurSheet, endcell, 3);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, startcell, endcell, "凭    证    结    束");


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
            //}
            //catch(Exception ex)
            //{
            //    excel.Dispose();
            //    throw new Exception(ex.Message);
            //}
        }


        /// <summary>
        /// 导出稿费报销单EXCEL
        /// </summary>
        /// <param name="userid">操作人</param>
        /// <param name="id">稿费报销单ID</param>
        /// <param name="serverpath">存放路径</param>
        /// <param name="ofname">文件名</param>
        /// <param name="isDelete">是否需要删除(如果生成成功，会产生临时文件，需要删除，如果生成失败的话，就不需要删除)</param>
        /// <returns></returns>
        public static string ExportMediaOrderExcel(int userid, ESP.Finance.Entity.ReturnInfo returnModel, string serverpath, out string ofname, bool UnPayment)
        {
            ofname = "MediaOrderList" + DateTime.Now.Ticks.ToString() + ".xls";
            if (System.IO.File.Exists(serverpath + ofname))//如果有则删除
            {
                try
                {
                    System.IO.File.Delete(serverpath + ofname);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            string term = string.Empty;
            if (UnPayment)
            {
                term = " meidaorderid in(" + returnModel.MediaOrderIDs.TrimEnd(',') + ") and (ispayment=0 or ispayment is null)";
            }
            else
            {
                term = " meidaorderid in(" + returnModel.MediaOrderIDs.TrimEnd(',') + ")";
            }
            List<ESP.Purchase.Entity.MediaOrderInfo> mediaOrderList = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModelList(term);

            string temppath = serverpath + "稿费报销明细单.xls";
            ExcelHandle excel = new ExcelHandle();
            try
            {
                excel.Load(temppath);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
                if (mediaOrderList.Count > 0)
                {
                    string username = emp.Name;
                    string userdepartmentname = emp.PositionDescription;
                    string projectcode = returnModel.ProjectCode;
                    ExcelHandle.WriteAfterMerge(excel.CurSheet, "A1", "P1", string.Format("记者发稿费用申请单      \r\n 项目号：{0}   PN号：{1}   PR单号：{2}   组别：{3}   分机号：{4}    申请人：{5}   报销日期：", projectcode, returnModel.ReturnCode, returnModel.PRNo, userdepartmentname, emp.Telephone, username));
                }

                int rownum = 3;
                decimal totalAmount = 0;
                string cell = "A3";
                foreach (ESP.Purchase.Entity.MediaOrderInfo model in mediaOrderList)
                {
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.MediaName);

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.Subject);

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReleaseDate);

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.WordLength);

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.TotalAmount.Value.ToString("f2"));

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "");

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReceiverName);

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.CityName);

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.BankName);

                    cell = string.Format("J{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + model.BankAccountName);

                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + model.CardNumber);

                    cell = string.Format("L{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.Tel);

                    cell = string.Format("M{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.TotalAmount);
                    totalAmount += model.TotalAmount.Value;
                    cell = string.Format("N{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "");

                    cell = string.Format("O{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "");

                    cell = string.Format("P{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                    rownum++;
                }

                //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", cell);
                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("N{0}", rownum), "合计：" + totalAmount.ToString("f2"));
                ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", string.Format("N{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                string signed = "  申请人签字:                       第一级批准人:                      第二级批准人:                       *第三级批准人:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                signed = "  日期:                             日期:                              日期:                               日期:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum += 2;

                cell = string.Format("A{0}", rownum);
                signed = "  媒介中心:                                    采购部:                                     财务部:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                signed = "  日期:                                        日期:                                       日期:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "注：*第三级批准人在金额超过一定标准时才需签署");
                //ExcelHandle.SetColor(excel.CurSheet, cell, System.Drawing.Color.Red);
                rownum++;

                string auditLog = GetAuditLog(returnModel);
                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteAfterMerge(excel.CurSheet, "A" + (rownum + 1).ToString(), "P" + (rownum + 1).ToString(), auditLog);
                ExcelHandle.SetFontSize(excel.CurSheet, cell, 10);

                ExcelHandle.SaveAS(excel.CurBook, serverpath + ofname);
                excel.Dispose();
                return serverpath + ofname;
            }
            catch
            {
                excel.Dispose();
                ofname = "稿费报销明细单.xls";
                return ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath + "稿费报销明细单.xls";
            }
        }

        private static string GetAuditLog(ESP.Finance.Entity.ReturnInfo returnmodel)
        {
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(returnmodel.ReturnID);
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
                string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                loginfo += model.AuditorEmployeeName + austatus + "[" + auditdate + "]" + model.Suggestion + "\r\n";

            }
            return loginfo;
        }

        public static UpdateResult CancelToFinance(int returnId, string CancelSuggestion, ESP.Compatible.Employee CurrentUser)
        {
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            //财务审批第一级
            ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1)).FirstFinanceID);

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnModel.PaymentCode = financeEmp.ID;
                    returnModel.PaymentUserID = Convert.ToInt32(financeEmp.SysID);
                    returnModel.PaymentUserName = financeEmp.ITCode;
                    returnModel.PaymentEmployeeName = financeEmp.Name;
                    returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                    returnModel.RePaymentSuggestion = CancelSuggestion;
                    ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel, trans);
                    if (result == ESP.Finance.Utility.UpdateResult.Succeed)
                    {
                        ESP.Finance.Entity.AuditLogInfo logModel = new ESP.Finance.Entity.AuditLogInfo();
                        logModel.AuditDate = DateTime.Now;
                        logModel.AuditorEmployeeName = CurrentUser.Name;
                        logModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                        logModel.AuditorUserCode = CurrentUser.ID;
                        logModel.AuditorUserName = CurrentUser.ITCode;
                        logModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                        logModel.FormID = returnModel.ReturnID;
                        logModel.FormType = (int)ESP.Finance.Utility.FormType.Return;
                        logModel.Suggestion = "付款重汇" + returnModel.RePaymentSuggestion;
                        new DataAccess.AuditLogDataProvider().Add(logModel, trans);

                        string term = " audittype=@audittype and returnid=@returnID";
                        List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
                        System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@audittype", System.Data.SqlDbType.Int, 4);
                        p1.Value = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                        paramList.Add(p1);
                        System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@returnID", System.Data.SqlDbType.Int, 4);
                        p2.Value = returnModel.ReturnID;
                        paramList.Add(p2);
                        int dresult = new DataAccess.ReturnAuditHistDataProvider().DeleteByParameters(term, paramList, trans);
                        ESP.Finance.Entity.ReturnAuditHistInfo histModel = new ReturnAuditHistInfo();
                        histModel.ReturnID = returnModel.ReturnID;
                        histModel.AuditeStatus = 0;
                        histModel.AuditorUserID = int.Parse(financeEmp.SysID);
                        histModel.AuditorUserCode = financeEmp.ID;
                        histModel.AuditorEmployeeName = financeEmp.Name;
                        histModel.AuditorUserName = financeEmp.ITCode;
                        histModel.AuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                        ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(histModel, trans);
                        if (dresult > 0)
                        {
                            trans.Commit();
                            return UpdateResult.Succeed;
                        }
                        else
                        {
                            trans.Rollback();
                            return UpdateResult.Failed;
                        }
                    }
                    else
                    {
                        trans.Rollback();
                        return result;
                    }
                }
                catch
                {
                    trans.Rollback();
                    return UpdateResult.Failed;
                }
            }
        }
        #endregion

        public static decimal GetTotalPNFee(ESP.Finance.Entity.ReturnInfo returnModel)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ReturnManager.GetList("projectcode='" + returnModel.ProjectCode + "' and returnid !=" + returnModel.ReturnID.ToString() + " and departmentid=" + returnModel.DepartmentID.ToString() + " and returntype not in(11,30,20,31,32,33,34,35,36,37,40)");
            decimal total = 0;
            foreach (ESP.Finance.Entity.ReturnInfo model in returnlist)
            {
                if (model.FactFee != null && model.FactFee.Value != 0)
                {
                    total += model.FactFee.Value;
                }
                else
                    total += model.PreFee.Value;
            }
            return total;
        }

        public static DataTable GetReturnListJoinHist(int userId)
        {
            return DataProvider.GetReturnListJoinHist(userId);
        }

        public static int changeRequestor(string returnIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            return DataProvider.changeRequestor(returnIds, oldUserId, newUserId, currentUser);
        }
        public static int changAuditor(string returnIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            return DataProvider.changAuditor(returnIds, oldUserId, newUserId, currentUser);
        }

        /// <summary>
        /// 得到批次中所有报销单的单据类型
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public static int GetDistinctReturnTypeByBatchID(int batchid)
        {
            return DataProvider.GetDistinctReturnTypeByBatchID(batchid);
        }

        /// <summary>
        /// 得到批次中所有报销单的公司代码
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public static string GetDistinctBranchCodeByBatchID(int batchid)
        {
            return DataProvider.GetDistinctBranchCodeByBatchID(batchid);
        }

        public static string GetDistinctRecipientByBatchID(int batchid)
        {
            return DataProvider.GetDistinctRecipientByBatchID(batchid);
        }

        public static string GetDistinctRecipientByReturnID(int returnId)
        {
            return DataProvider.GetDistinctRecipientByReturnID(returnId);
        }

        public static IList<ReturnInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            return DataProvider.GetWaitAuditList(userIds, strTerms, parms);
        }
        public static IList<ReturnInfo> GetWaitAuditList(int[] userIds)
        {
            return DataProvider.GetWaitAuditList(userIds);
        }

        public static int BatchRepayToRequest(int batchId, string reason, ESP.Compatible.Employee CurrentUser)
        {
            return DataProvider.BatchRepayToRequest(batchId, reason, CurrentUser);
        }

        public static int BatchRepayToFinance(int batchId, string reason, ESP.Compatible.Employee CurrentUser)
        {
            return DataProvider.BatchRepayToFinance(batchId, reason, CurrentUser);
        }

        public static int BatchRepayCommit(int batchId, string supplier, string bank, string account, string remark, ESP.Compatible.Employee CurrentUser)
        {
            return DataProvider.BatchRepayCommit(batchId, supplier, bank, account, remark, CurrentUser);
        }

        public static decimal GetTotalRefund(string projectCode, int departmentId)
        {
            return DataProvider.GetTotalRefund(projectCode, departmentId);
        }

        public static DataTable GetRecipientReport(string where)
        {
            return DataProvider.GetRecipientReport(where);
        }
        public static IList<ESP.Finance.Entity.ReturnInfo> GetPaidPNReport(int userid, DateTime dtbegin, DateTime dtend)
        {
            return DataProvider.GetPaidPNReport(userid, dtbegin, dtend);
        }

        public static string ExportTicketBatch(int batchid, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/ExpenseAccount/TicketBatch.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            try
            {
                excel.Load(sourceFile);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                int rownum = 8;
                string cell = "A8";
                ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                IList<ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetTicketBatch(batchid);
                ESP.Compatible.Employee creator = new ESP.Compatible.Employee(batchModel.CreatorID.Value);
                //批 次 号:
                ExcelHandle.WriteCell(excel.CurSheet, "B5", batchModel.PurchaseBatchCode);
                //申请日期:
                ExcelHandle.WriteCell(excel.CurSheet, "B6", batchModel.CreateDate.Value.ToString("yyyy-MM-dd"));
                //创建人
                ExcelHandle.WriteCell(excel.CurSheet, "G5", creator.Name);
                //公司代码：
                ExcelHandle.WriteCell(excel.CurSheet, "G6", batchModel.BatchCode);

                foreach (ReturnInfo model in returnlist)
                {
                    IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> details = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTicketDetail(model.ReturnID);
                    foreach (ExpenseAccountDetailInfo detail in details)
                    {
                        //PN单号
                        cell = string.Format("A{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReturnCode);
                        //项目号
                        cell = string.Format("B{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.ProjectCode);
                        //费用发生日期
                        cell = string.Format("C{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, detail.ExpenseMoney.Value.ToString("yyyy-MM-dd"));
                        //申请人
                        cell = string.Format("D{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestUserName);
                        //员工编码
                        cell = string.Format("E{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestUserName);
                        //费用所属组
                        cell = string.Format("F{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestUserName);
                        //费用明细描述
                        cell = string.Format("G{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestUserName);
                        //申请金额
                        cell = string.Format("H{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestUserName);
                        //供应商名称
                        cell = string.Format("I{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestUserName);
                        //开户银行
                        cell = string.Format("J{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestUserName);
                        //银行账户
                        cell = string.Format("K{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestUserName);

                        rownum++;
                    }
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
            catch
            {
                excel.Dispose();
                return "";
            }
        }

        public static string ExportRecipientReport(DataTable dt, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/ExpenseAccount/RecipientReportTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            try
            {
                excel.Load(sourceFile);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                int rownum = 4;
                string cell = "A4";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // prid,prno,customer,projecttype,projectcode,suppliername,fee,prstatus,prtype,pnstatus,
                    // invoice,commitdate,flag,pnid,requestor,userCode,dept

                    //PR单号
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["prno"].ToString());
                    //项目号
                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["projectcode"].ToString());
                    //客户代码
                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["customer"].ToString());
                    //项目类型
                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["projecttype"].ToString());
                    //申请人
                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["requestor"].ToString());
                    //员工编号
                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + dt.Rows[i]["userCode"].ToString());
                    //费用所属组
                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["dept"].ToString());
                    //供应商
                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["suppliername"].ToString());
                    //预付金额
                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["fee"].ToString());
                    //申请日期
                    cell = string.Format("J{0}", rownum);
                    DateTime dtcommit = Convert.ToDateTime(dt.Rows[i]["commitdate"].ToString());
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dtcommit.ToString("yyyy/MM/dd"));
                    //申请状态
                    string flg = dt.Rows[i]["flag"].ToString();
                    string statustext = string.Empty;
                    int status = Convert.ToInt32(dt.Rows[i]["pnstatus"].ToString());
                    if (flg == "GR")
                    {
                        statustext = ESP.Purchase.Common.State.recipientConfirm_Names[status];
                    }
                    else
                    {
                        statustext = ReturnPaymentType.ReturnStatusString(status, 0, false);
                    }
                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, statustext);
                    //发票状态
                    cell = string.Format("L{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["invoice"].ToString());

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
            catch
            {
                excel.Dispose();
                return "";
            }
        }

        public static string ExportPaidPN(IList<ReturnInfo> returnlist, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/ExpenseAccount/PaidPNReportTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            try
            {
                excel.Load(sourceFile);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                int rownum = 2;
                string cell = "A4";

                //ReturnCode,ProjectCode,PreBeginDate,ReturnContent,PRNo,RequestEmployeeName,
                //PreFee,SupplierName,ReturnStatus

                foreach (ReturnInfo model in returnlist)
                {
                    ESP.Purchase.Entity.PaymentPeriodInfo paymentPeriod = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(model.ReturnID);
                    ESP.Purchase.Entity.PeriodRecipientInfo periodRecipient = null;
                    ESP.Purchase.Entity.RecipientInfo recipient = null;
                    if (paymentPeriod != null)
                    {
                        periodRecipient = ESP.Purchase.BusinessLogic.PeriodRecipientManager.GetModelByPeriodId(paymentPeriod.id);
                    }
                    if (periodRecipient != null)
                    {
                        recipient = ESP.Purchase.BusinessLogic.RecipientManager.GetModel(periodRecipient.recipientId);
                    }
                    //单号
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReturnCode);
                    //项目号
                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ProjectCode);

                    //费用发生日期
                    string ReturnFactDate;
                    if (recipient != null)
                        ReturnFactDate = recipient.RecipientDate.ToString("yyyy-MM-dd");
                    else
                        ReturnFactDate = model.ReturnFactDate == null ? "" : model.ReturnFactDate.Value.ToString("yyyy-MM-dd");

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, ReturnFactDate);
                    //项目描述
                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReturnContent);
                    //申请PR
                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.PRNo);
                    //申请人
                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestEmployeeName);
                    //费用
                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.PreFee.Value.ToString("#,##0.00"));
                    //供应商
                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.SupplierName);

                    //申请状态
                    cell = string.Format("I{0}", rownum);
                    string statustext = ReturnPaymentType.ReturnStatusString(model.ReturnStatus.Value, 0, false);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, statustext);

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
            catch
            {
                excel.Dispose();
                return "";
            }
        }

        public static string ExportOOPBatch(System.Data.DataTable dt,System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/ExpenseAccount/OOPVoucherTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            try
            {
                excel.Load(sourceFile);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                int rownum = 11;
                string cell = "A11";

                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    //A列财务手动补充
                    //B列隐藏
                    //业务日期
                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, DateTime.Parse(dt.Rows[i]["ExpenseDate"].ToString()).ToString("yyyy-MM-dd"));

                    //D列隐藏

                    //描述
                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ExpenseDesc"].ToString());

                    //F列隐藏

                    //G 会计科目 1301C012
                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell,  dt.Rows[i]["FinanceCode"].ToString());
                    //H 客户名称
                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["customerName"].ToString());
                    //K 金额
                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell,decimal.Parse(dt.Rows[i]["ExpenseMoney"].ToString()).ToString("#,##0.00"));
                    //L  借方(Dr.)  与 K一致
                    cell = string.Format("L{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, decimal.Parse(dt.Rows[i]["ExpenseMoney"].ToString()).ToString("#,##0.00"));
                    //M 贷方(Cr.) 

                    //N  借/贷合计

                    //O T0组 根据报销单关联财务组别
                    cell = string.Format("O{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["T0"].ToString());
                   
                    //P T1 员工 与报销单一致
                    cell = string.Format("P{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'"+dt.Rows[i]["T1"].ToString());
                    //Q T2 项目来源 NULL
                    cell = string.Format("Q{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["T2"].ToString());
                    //R T3 客户 根据报销单关联财务代码
                    cell = string.Format("R{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["T3"].ToString());
                    //S T4 项目类型 与报销单一致 (P)
                    cell = string.Format("S{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["T4"].ToString());
                    //T T5 项目号 与报销单一致 （N2004001V）
                    cell = string.Format("T{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["T5"].ToString());
                    //U T6 收入/成本 自动选取&财务补充（报销费用类型代码）
                    cell = string.Format("U{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["T6"].ToString());
                    rownum++;
                }

                ExcelHandle.SetFormula(excel.CurSheet, "L10", "=ROUND(SUM(L11:L" + rownum.ToString() + "),2)");
                ExcelHandle.SetFormula(excel.CurSheet, "M10", "=ROUND(SUM(M11:M" + rownum.ToString() + "),2)");


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
            catch
            {
                excel.Dispose();
                return "";
            }
        }

        public static IList<ReturnInfo> GetTicketBatch(int batchid)
        {
            return DataProvider.GetTicketBatch(batchid);
        }

        /// <summary>
        /// 获得月度报告-团队，月周度报告-个人
        /// </summary>
        /// <returns>string filepath</returns>
        public static string ExportMonthProjectGroupReport(DateTime beginTime, DateTime endTime, int groupId, System.Web.HttpResponse response
            , bool IsExportMonthForGroup, bool IsExportMonthForUser)
        {
            //ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);

            string temppath = "/Tmp/Salary/ProjectForTimeSheetGroupUserReport.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            string[] arrayNum = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            excel.Load(sourceFile);


            DateTime salaybeginTime = beginTime;
            DateTime salayendTime = endTime;
            Hashtable hashYearMonth = new Hashtable();
            DateTime dtfee = beginTime;
            DateTime dttitle = beginTime;

            //如果选择了导出团队月度报表
            if (IsExportMonthForGroup)
                WriteGroupMonthReport(beginTime, endTime, groupId, response, excel);

            //如果选择了导出个人月度报表
            if (IsExportMonthForUser)
                WriteUserMonthReport(beginTime, endTime, groupId, response, excel);
            ////fee 总计
            //ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + feeTotalNum, "=ROUND(SUM(" + arrayMonth[1] + feeTotalNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + feeTotalNum.ToString() + "),2)");

            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        //获得月度报告-团队
        public static void WriteGroupMonthReport(DateTime beginTime, DateTime endTime1, int groupId, System.Web.HttpResponse response, ExcelHandle excel)
        {
            DateTime endTime = new DateTime(endTime1.Year, endTime1.Month, endTime1.Day, 23, 59, 59);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            string cell = string.Empty;
            int beginRow = 7;
            ESP.Framework.Entity.DepartmentInfo depinfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(groupId);

            ExcelHandle.WriteCell(excel.CurSheet, "A2", beginTime + " -- " + endTime);
            //写部门名称
            if (depinfo != null)
                ExcelHandle.WriteCell(excel.CurSheet, "B3", depinfo.DepartmentName);

            #region 写各个项目号明细

            DataSet ds = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                decimal totalSalary = 0;
                decimal totalFee = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, dr["projectcode"]);   //项目编号
                    ExcelHandle.WriteCell(excel.CurSheet, "B" + beginRow, dr["houramount"]);    //小时数

                    string feemonth = "";
                    int projectid = Convert.ToInt32(dr["projectid"]);
                    DataSet dsFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetMonthProjectGroupReport_Fee(beginTime, endTime, projectid);
                    decimal fee = 0;
                    if (dsFee != null && dsFee.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drFee in dsFee.Tables[0].Rows)
                        {
                            fee += Convert.ToDecimal(drFee["Fee"]);
                            feemonth += drFee["yearvalue"] + "-" + drFee["monthvalue"] + "/";
                        }
                    }
                    feemonth = feemonth.TrimEnd('/');

                    ExcelHandle.WriteCell(excel.CurSheet, "D5", feemonth + "月服务费");    //*月服务费
                    ExcelHandle.WriteCell(excel.CurSheet, "D" + beginRow, fee);   //fee
                    totalFee += fee;
                    DataSet dsSalary = null;
                    decimal salary = 0;
                    if (dsSalary != null && dsSalary.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drSalary in dsSalary.Tables[0].Rows)
                        {
                            if (drSalary["chargerate"] != DBNull.Value)
                            {
                                salary += Convert.ToDecimal(drSalary["hourstotal"]) * Convert.ToDecimal(drSalary["chargerate"]);
                            }
                            //salary += Convert.ToDecimal(drSalary["Fee"]);
                        }
                    }
                    ExcelHandle.WriteCell(excel.CurSheet, "C" + beginRow, salary);    //金额（工资）
                    totalSalary += salary;
                    decimal service = 0;
                    if (fee != 0)
                    {
                        service = (fee - salary) / fee;
                    }
                    if (fee <= 0)
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "E" + beginRow, "无效工时");
                        ExcelHandle.SetColor(excel.CurSheet, "E" + beginRow, System.Drawing.Color.Red);
                    }
                    else
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "E" + beginRow, service);    //(fee - 金额) / fee
                        if (service < 0)
                            ExcelHandle.SetColor(excel.CurSheet, "E" + beginRow, System.Drawing.Color.Red);
                    }
                    beginRow += 1;
                }
                //Total:
                ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, "总计:");
                ExcelHandle.SetFormula(excel.CurSheet, "B" + beginRow, "=ROUND(SUM(B7:B" + (beginRow - 1) + "),2)");
                ExcelHandle.SetFormula(excel.CurSheet, "C" + beginRow, "=ROUND(SUM(C7:C" + (beginRow - 1) + "),2)");
                ExcelHandle.SetFormula(excel.CurSheet, "D" + beginRow, "=ROUND(SUM(D7:D" + (beginRow - 1) + "),2)");
                ExcelHandle.SetFormula(excel.CurSheet, "E" + beginRow, "=(D" + beginRow + "-C" + beginRow + ")/D" + beginRow);
                decimal percent = (totalFee - totalSalary) / totalFee;
                if (percent <= 0)
                    ExcelHandle.SetColor(excel.CurSheet, "E" + beginRow, System.Drawing.Color.Red);

                ExcelHandle.SetBold(excel.CurSheet, "A" + beginRow, "E" + beginRow);

            }
            #endregion
        }

        //月周度报告-个人
        public static void WriteUserMonthReport(DateTime beginTime, DateTime endTime1, int groupId, System.Web.HttpResponse response, ExcelHandle excel)
        {
            DateTime endTime = new DateTime(endTime1.Year, endTime1.Month, endTime1.Day, 23, 59, 59);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[2];
            string cell = string.Empty;
            int beginRow = 7;
            ESP.Framework.Entity.DepartmentInfo depinfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(groupId);


            ExcelHandle.WriteCell(excel.CurSheet, "A2", beginTime + " -- " + endTime);

            //写部门名称
            if (depinfo != null)
                ExcelHandle.WriteCell(excel.CurSheet, "B3", depinfo.DepartmentName);


            #region 写各个项目号明细
            DataSet ds = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string username = string.Empty;
                string projectcode = string.Empty;
                decimal hoursTotalForUser = 0;
                decimal projecthoursTotalForUser = 0;
                decimal billablerate = 0;
                decimal hoursTotalForProject = 0;
                decimal projecthoursTotalForProject = 0;
                decimal totalSumWorkHoursUnbillable = 0;
                decimal totalSumworkHoursBillable = 0;
                int rowscount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (username == dr["UserName"].ToString() && (dr["projectcode"].ToString() != projectcode))
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, username + "(" + projectcode + ")" + "- 小计：");   //如果轮到写另一个Project的时候,先写上一个人的小计
                        ExcelHandle.WriteCell(excel.CurSheet, "H" + beginRow, hoursTotalForProject);
                        ExcelHandle.WriteCell(excel.CurSheet, "I" + beginRow, projecthoursTotalForProject);

                        decimal differ = 0;
                        if (hoursTotalForProject != 0)
                            differ = projecthoursTotalForProject / hoursTotalForProject - billablerate;
                        else
                            differ = 0 - billablerate;

                        hoursTotalForProject = 0;  //写完小计后清空
                        billablerate = 0;
                        projecthoursTotalForProject = 0;  //写完小计后清空
                        beginRow += 1;
                    }

                    if (username != string.Empty && dr["UserName"].ToString() != username)
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, username + "(" + projectcode + ")" + "- 小计：");   //如果轮到写另一个Project的时候,先写上一个人的小计
                        ExcelHandle.WriteCell(excel.CurSheet, "H" + beginRow, hoursTotalForProject);
                        ExcelHandle.WriteCell(excel.CurSheet, "I" + beginRow, projecthoursTotalForProject);

                        decimal differ = 0;
                        if (hoursTotalForProject != 0)
                            differ = projecthoursTotalForProject / hoursTotalForProject - billablerate;
                        else
                            differ = 0 - billablerate;

                        hoursTotalForProject = 0;  //写完小计后清空
                        projecthoursTotalForProject = 0;  //写完小计后清空
                        beginRow += 1;

                        //**************************************************************************//

                        ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, username + "- 个人小计：");   //如果轮到写另一个员工的时候,先写上一个人的小计

                        ExcelHandle.WriteCell(excel.CurSheet, "H" + beginRow, hoursTotalForUser);
                        ExcelHandle.WriteCell(excel.CurSheet, "I" + beginRow, projecthoursTotalForUser);
                        if (hoursTotalForUser != 0)
                            ExcelHandle.WriteCell(excel.CurSheet, "J" + beginRow, projecthoursTotalForUser / hoursTotalForUser);
                        else
                            ExcelHandle.WriteCell(excel.CurSheet, "J" + beginRow, "0");

                        differ = 0;
                        if (hoursTotalForUser != 0)
                            differ = projecthoursTotalForUser / hoursTotalForUser - billablerate;
                        else
                            differ = 0 - billablerate;

                        ExcelHandle.WriteCell(excel.CurSheet, "K" + beginRow, billablerate);
                        ExcelHandle.WriteCell(excel.CurSheet, "L" + beginRow, differ.ToString());
                        if (differ <= 0)
                        {
                            ExcelHandle.SetColor(excel.CurSheet, "L" + beginRow, System.Drawing.Color.Red);
                        }
                        hoursTotalForUser = 0;  //写完小计后清空
                        billablerate = 0;
                        projecthoursTotalForUser = 0;  //写完小计后清空
                        beginRow += 1;
                    }
                    username = dr["UserName"].ToString();
                    projectcode = dr["projectcode"].ToString();

                    rowscount++;

                    ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, dr["UserName"].ToString());   //姓名
                    ExcelHandle.WriteCell(excel.CurSheet, "B" + beginRow, "'" + dr["UserCode"].ToString());   //编号
                    ExcelHandle.WriteCell(excel.CurSheet, "C" + beginRow, dr["departmentpositionname"].ToString());   //职位
                    ExcelHandle.WriteCell(excel.CurSheet, "D" + beginRow, dr["ProjectCode"].ToString());   //项目号

                    ExcelHandle.WriteCell(excel.CurSheet, "E" + beginRow, dr["currentdate"].ToString());   //工作类别
                    ExcelHandle.WriteCell(excel.CurSheet, "F" + beginRow, dr["categoryname"].ToString());   //工作类别
                    ExcelHandle.WriteCell(excel.CurSheet, "G" + beginRow, dr["workitem"].ToString());   //工作内容

                    decimal hoursTotal = 0;
                    if (dr["HoursTotal"] == DBNull.Value || string.IsNullOrEmpty(dr["HoursTotal"].ToString()))
                        hoursTotal = 0;
                    else
                        hoursTotal = Convert.ToDecimal(dr["HoursTotal"].ToString()); //A (总小时数合计 - 休假+OT。注：实际不用加OT，因为OT在总数中已包含。)
                    decimal billableHoursTotal = 0; //B


                    ExcelHandle.WriteCell(excel.CurSheet, "H" + beginRow, hoursTotal);   //总小时数合计
                    hoursTotalForUser += hoursTotal;    //累计个人小时数
                    totalSumWorkHoursUnbillable += hoursTotal;
                    //if (projectcode != string.Empty)
                    //{
                    hoursTotalForProject += hoursTotal;

                    // }
                    billablerate = 0;
                    if (dr["billablerate"] == DBNull.Value || string.IsNullOrEmpty(dr["billablerate"].ToString()))
                    {
                        billablerate = 0;
                    }
                    else
                        billablerate = Convert.ToDecimal(dr["billablerate"].ToString());

                    if (dr["isbillable"] != DBNull.Value && !string.IsNullOrEmpty(dr["isbillable"].ToString()) && bool.Parse(dr["isbillable"].ToString()) == true)
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "I" + beginRow, hoursTotal);   //Billable小时数合计（B）
                        projecthoursTotalForUser += hoursTotal;   //累计个人项目小时数
                        if (projectcode != string.Empty)
                        {
                            projecthoursTotalForProject += hoursTotal;   //累计个人项目小时数
                            totalSumworkHoursBillable += hoursTotal;
                        }
                        billableHoursTotal = hoursTotal;
                    }
                    else
                        ExcelHandle.WriteCell(excel.CurSheet, "I" + beginRow, "");   //项目小时数合计（B）


                    beginRow += 1;


                    if (rowscount == dt.Rows.Count)
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, username + "(" + projectcode + ")" + "- 小计：");   //如果轮到写另一个Project的时候,先写上一个人的小计
                        ExcelHandle.WriteCell(excel.CurSheet, "H" + beginRow, hoursTotalForProject);
                        ExcelHandle.WriteCell(excel.CurSheet, "I" + beginRow, projecthoursTotalForProject);

                        decimal differ = 0;
                        if (hoursTotalForProject != 0)
                            differ = projecthoursTotalForProject / hoursTotalForProject - billablerate;
                        else
                            differ = 0 - billablerate;

                        hoursTotalForProject = 0;  //写完小计后清空
                        projecthoursTotalForProject = 0;  //写完小计后清空
                        beginRow += 1;

                        //**************************************************************************//

                        ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, username + "- 个人小计：");   //如果轮到写另一个员工的时候,先写上一个人的小计

                        ExcelHandle.WriteCell(excel.CurSheet, "H" + beginRow, hoursTotalForUser);
                        ExcelHandle.WriteCell(excel.CurSheet, "I" + beginRow, projecthoursTotalForUser);
                        if (hoursTotalForUser != 0)
                            ExcelHandle.WriteCell(excel.CurSheet, "J" + beginRow, projecthoursTotalForUser / hoursTotalForUser);
                        else
                            ExcelHandle.WriteCell(excel.CurSheet, "J" + beginRow, "0");

                        differ = 0;
                        if (hoursTotalForUser != 0)
                            differ = projecthoursTotalForUser / hoursTotalForUser - billablerate;
                        else
                            differ = 0 - billablerate;

                        ExcelHandle.WriteCell(excel.CurSheet, "K" + beginRow, billablerate);
                        ExcelHandle.WriteCell(excel.CurSheet, "L" + beginRow, differ.ToString());
                        if (differ <= 0)
                        {
                            ExcelHandle.SetColor(excel.CurSheet, "L" + beginRow, System.Drawing.Color.Red);
                        }

                        hoursTotalForUser = 0;  //写完小计后清空
                        billablerate = 0;
                        projecthoursTotalForUser = 0;  //写完小计后清空
                        beginRow += 1;
                    }
                }
                ExcelHandle.WriteCell(excel.CurSheet, "A" + beginRow, "总计:");
                ExcelHandle.WriteCell(excel.CurSheet, "H" + beginRow, totalSumWorkHoursUnbillable);
                ExcelHandle.WriteCell(excel.CurSheet, "I" + beginRow, totalSumworkHoursBillable);

            }
            #endregion
        }

        public class TimesheetRptInfo
        {
            public string Month { get; set; }
            public decimal ChargeTotal { get; set; }
            public decimal MonthFee { get; set; }
        }

        //项目报告（TimeSheet）
        public static string ExportProjectReportOnTimeSheet(int userid, System.Web.HttpResponse response, DateTime beginTime, DateTime endTime, ESP.Finance.Entity.ProjectInfo projectModel, string groupid)
        {
            string temppath = "/Tmp/Salary/ProjectForTimeSheetReport.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];

            string[] arrayMonth = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string cell = string.Empty;

            int yearmonthBeginCell = 3;
            string yearmonthCellNum = "7";

            DateTime salaybeginTime = beginTime;
            DateTime salayendTime = endTime.AddMonths(1).AddDays(-1);
            Hashtable hashYearMonth = new Hashtable();
            DateTime dtfee = beginTime;
            DateTime dttitle = beginTime;

            ExcelHandle.WriteCell(excel.CurSheet, "B2", projectModel.ProjectCode);
            ExcelHandle.WriteCell(excel.CurSheet, "B3", projectModel.BusinessDescription);
            ExcelHandle.WriteCell(excel.CurSheet, "B4", projectModel.BeginDate.Value.ToString("yyyy-MM-dd") + " -- " + projectModel.EndDate.Value.ToString("yyyy-MM-dd"));
            ExcelHandle.WriteCell(excel.CurSheet, "B5", beginTime.ToString("yyyy-MM-dd").Substring(0, beginTime.ToString("yyyy-MM-dd").Length - 3) + " -- " + salayendTime.ToString("yyyy-MM-dd").Substring(0, salayendTime.ToString("yyyy-MM-dd").Length - 3));

            ExcelHandle.WriteCell(excel.CurSheet, "A7", "姓名");
            ExcelHandle.WriteCell(excel.CurSheet, "B7", "工号");
            ExcelHandle.WriteCell(excel.CurSheet, "C7", "职位");

            List<TimesheetRptInfo> tslist = new List<TimesheetRptInfo>();

            #region 写列头的年月
            while (dttitle.Year <= salayendTime.Year && dttitle.Month <= salayendTime.Month)
            {
                cell = arrayMonth[yearmonthBeginCell] + yearmonthCellNum;
                ExcelHandle.WriteCell(excel.CurSheet, cell, dttitle.Year.ToString() + "年" + dttitle.Month + "月");
                hashYearMonth.Add(dttitle.Year.ToString() + "年" + dttitle.Month + "月", arrayMonth[yearmonthBeginCell + 3]);

                dttitle = dttitle.AddMonths(1);
                yearmonthBeginCell = yearmonthBeginCell + 1;
            }
            ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[yearmonthBeginCell] + yearmonthCellNum, "合计");
            #endregion

            //成本起始行
            int salayTotalNum = 8;
            int lieTotalNum = 9;
            DataSet ds = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lieTotalNum += ds.Tables[0].Rows.Count;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    yearmonthBeginCell = 3;//初始化起始列
                    string cellCost = string.Empty;
                    ExcelHandle.WriteCell(excel.CurSheet, "A" + salayTotalNum, dr["UserName"].ToString());
                    ExcelHandle.WriteCell(excel.CurSheet, "B" + salayTotalNum, "'" + dr["Code"].ToString());
                    ExcelHandle.WriteCell(excel.CurSheet, "C" + salayTotalNum, dr["departmentpositionname"].ToString());

                    int costuserid = Convert.ToInt32(dr["userid"].ToString());

                    DateTime dtforcost = beginTime;
                    decimal cost = 0;
                    decimal usercosttotal = 0;
                    DataSet dsDetail = null;

                    if (dsDetail != null && dsDetail.Tables[0].Rows.Count > 0)
                    {
                        while (dtforcost.Year <= salayendTime.Year && dtforcost.Month <= salayendTime.Month)
                        {
                            cellCost = arrayMonth[yearmonthBeginCell] + salayTotalNum;

                            ExcelHandle.WriteCell(excel.CurSheet, cellCost, 0);
                            foreach (DataRow drDetail in dsDetail.Tables[0].Rows)
                            {
                                if ((dtforcost.Year + "-" + dtforcost.Month.ToString("00")) == drDetail["currentdatetime"].ToString())
                                {
                                    decimal hours = Convert.ToDecimal(drDetail["houramount"].ToString());
                                    decimal chargate = Convert.ToDecimal(drDetail["chargerate"].ToString());
                                    ExcelHandle.WriteCell(excel.CurSheet, cellCost, (hours * chargate).ToString("###,###"));
                                    usercosttotal += hours * chargate;
                                    TimesheetRptInfo ts = new TimesheetRptInfo();
                                    ts.Month = dtforcost.Year.ToString() + "-" + dtforcost.Month.ToString();
                                    ts.ChargeTotal = hours * chargate;
                                    tslist.Add(ts);
                                    break;
                                }
                            }
                            dtforcost = dtforcost.AddMonths(1);
                            yearmonthBeginCell += 1;
                        }
                    }
                    else
                    {
                        while (dtforcost.Year <= salayendTime.Year && dtforcost.Month <= salayendTime.Month)
                        {
                            cellCost = arrayMonth[yearmonthBeginCell] + salayTotalNum;
                            ExcelHandle.WriteCell(excel.CurSheet, cellCost, "0");
                            dtforcost = dtforcost.AddMonths(1);
                            yearmonthBeginCell += 1;
                        }
                    }
                    //行合计
                    ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[yearmonthBeginCell] + salayTotalNum, usercosttotal.ToString("###,###"));

                    //向下挪一行
                    salayTotalNum += 1;
                }
            }
            //服务费 列总计
            DateTime dtforcostTotal = beginTime;
            yearmonthBeginCell = 3;//初始化起始列
            ExcelHandle.WriteCell(excel.CurSheet, "A" + lieTotalNum, "合计(A)");
            while (dtforcostTotal.Year <= salayendTime.Year && dtforcostTotal.Month <= salayendTime.Month)
            {
                string cellCostTotal = arrayMonth[yearmonthBeginCell] + lieTotalNum;
                decimal chargetotal = tslist.Where(x => x.Month == dtforcostTotal.Year + "-" + dtforcostTotal.Month.ToString()).Sum(x => x.ChargeTotal);

                ExcelHandle.WriteCell(excel.CurSheet, cellCostTotal, chargetotal.ToString("###,###"));

                //"=SUM(" + arrayMonth[yearmonthBeginCell] + 8 + ":"
                //+ arrayMonth[yearmonthBeginCell] + (lieTotalNum - 1) + ")");
                yearmonthBeginCell += 1;
                dtforcostTotal = dtforcostTotal.AddMonths(1);
            }
            decimal chargetotalsum = tslist.Sum(x => x.ChargeTotal);
            ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[yearmonthBeginCell] + lieTotalNum, chargetotalsum.ToString("###,###"));
            //"=SUM(" + arrayMonth[yearmonthBeginCell ] + 8 + ":"
            //+ arrayMonth[yearmonthBeginCell ] + (lieTotalNum - 1) + ")");

            #region 计算最后Service
            DataSet dsFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetFeeForProjectReportOnTimeSheet(beginTime, salayendTime, projectModel.ProjectId);
            int feeNum = lieTotalNum + 2;
            int serviceresultNum = feeNum + 2;
            decimal feeTotal = 0;
            ExcelHandle.WriteCell(excel.CurSheet, "A" + feeNum, "实际服务费(B)");

            ExcelHandle.WriteCell(excel.CurSheet, "A" + serviceresultNum, "Under(+)/Over(-) Service" + "\n" + "(B - A) / B");
            if (dsFee != null & dsFee.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFee in dsFee.Tables[0].Rows)
                {
                    yearmonthBeginCell = 3;//初始化起始列
                    string cellFee = string.Empty;
                    string cellService = string.Empty;
                    string cellChagerate = string.Empty;
                    DateTime dtforfee = beginTime;

                    decimal dcFee = 0;
                    if (!string.IsNullOrEmpty(drFee["fee"].ToString()) || Convert.ToDecimal(drFee["fee"].ToString()) != 0)
                    {
                        dcFee = Convert.ToDecimal(drFee["fee"].ToString());
                        feeTotal += dcFee;
                    }
                    while (dtforfee.Year <= salayendTime.Year && dtforfee.Month <= salayendTime.Month)
                    {
                        cellFee = arrayMonth[yearmonthBeginCell] + feeNum;
                        cellChagerate = arrayMonth[yearmonthBeginCell] + (feeNum - 2);
                        cellService = arrayMonth[yearmonthBeginCell] + serviceresultNum;
                        if ((dtforfee.Year + "-" + dtforfee.Month.ToString("00")) == drFee["yearvalue"].ToString() + "-" + Convert.ToInt32(drFee["monthvalue"].ToString()).ToString("00"))
                        {
                            ExcelHandle.WriteCell(excel.CurSheet, cellFee, dcFee.ToString("###,###"));

                            if (Convert.ToInt32(dcFee) <= 0)
                            {
                                ExcelHandle.WriteCell(excel.CurSheet, cellService, "无效工时");
                                ExcelHandle.SetColor(excel.CurSheet, cellService, System.Drawing.Color.Red);
                            }
                            else
                            {
                                decimal percent = (dcFee - tslist.Where(x => x.Month == dtforfee.Year + "-" + dtforfee.Month.ToString()).Sum(x => x.ChargeTotal)) / dcFee * 100;

                                ExcelHandle.WriteCell(excel.CurSheet, cellService, percent.ToString() + "%");
                                //  , "=(" + cellFee + "-" + cellChagerate + ")/" + cellFee + "*100");

                            }
                            break;
                        }

                        dtforfee = dtforfee.AddMonths(1);
                        yearmonthBeginCell += 1;
                    }
                }
                ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[yearmonthBeginCell + 1] + feeNum, feeTotal.ToString("###,###"));    //Fee合计
                decimal percenttotal = (feeTotal - tslist.Sum(x => x.ChargeTotal)) / feeTotal * 100;
                ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[yearmonthBeginCell + 1] + serviceresultNum, percenttotal.ToString() + "%");
                // , "=(" + arrayMonth[yearmonthBeginCell + 1] + feeNum + "-" + arrayMonth[yearmonthBeginCell +1] + (feeNum - 2) + ")/" + arrayMonth[yearmonthBeginCell +1] + feeNum + "*100"); //Chargerate合计和Fee合计相计算
            }
            #endregion

            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        public static string ExportDeptSalayForFinance(int userid, DataSet ds, System.Web.HttpResponse response, DateTime beginTime, DateTime endTime, string groupids)
        {
            string[] arrayGroupids = groupids.Split(',');
            if (arrayGroupids.Length == 0)
                return string.Empty;

            ESP.Finance.Entity.DepartmentViewInfo deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(int.Parse(arrayGroupids[0]));
            IList<ReportJoinInfo> joinlist = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupids + ") and (a.joindate between '" + beginTime.AddDays(-9) + "' and '" + endTime.AddDays(-9) + "')", " a.NewGroupId in(" + groupids + ") and (a.TransInDate between '" + beginTime.AddDays(-9) + "' and '" + endTime.AddDays(-9) + "')");
            IList<ReportDimissionInfo> dimissionlist = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupids + ") and (a.lastday between '" + beginTime + "' and '" + endTime.AddDays(11) + "')", "a.OldGroupId in(" + groupids + ") and (a.TransOutDate between '" + beginTime + "' and '" + endTime.AddDays(11) + "')");
            IList<DeadLineInfo> deadlinelist = DeadLineManager.GetAllList();
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
            DataTable dtable = ds.Tables[0];
            string temppath = "/Tmp/Salary/DepartmentSalayTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            string[] arrayMonth = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            string cell = string.Empty;

            int yearmonthBeginCell = 1;
            string yearmonthCellNum = "1";

            DateTime salaybeginTime = beginTime;
            DateTime salayendTime = endTime;
            Hashtable hashYearMonth = new Hashtable();
            DateTime dtfee = beginTime;
            DateTime dttitle = beginTime;

            #region 写列头的年月
            while (dttitle.Year <= endTime.Year && dttitle.Month <= endTime.Month)
            {
                cell = arrayMonth[yearmonthBeginCell] + yearmonthCellNum;
                ExcelHandle.WriteCell(excel.CurSheet, cell, dttitle.Year.ToString() + "年" + dttitle.Month + "月");
                hashYearMonth.Add(cell, dttitle.Year.ToString() + "年" + dttitle.Month + "月");
                if (dttitle.Month == 12)
                {
                    yearmonthBeginCell = yearmonthBeginCell + 1;
                    cell = arrayMonth[yearmonthBeginCell] + yearmonthCellNum;
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dttitle.Year.ToString() + "年13月");
                    hashYearMonth.Add(cell, dttitle.Year.ToString() + "年13月");
                }

                dttitle = dttitle.AddMonths(1);
                yearmonthBeginCell = yearmonthBeginCell + 1;
            }
            ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[yearmonthBeginCell + 1] + yearmonthCellNum, "合计");
            #endregion

            #region 写各个团队明细
            for (int i = 0; i < arrayGroupids.Length; i++)
            {
                string salayCellNum = (i + 2).ToString();
                int salayBeginCell = 0;

                ESP.Framework.Entity.DepartmentInfo depinfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(Convert.ToInt32(arrayGroupids[i]));
                if (depinfo == null)
                    continue;
                DataSet dsSalay = ESP.HumanResource.BusinessLogic.DeptSalaryManager.GetDataSetForExportFinance(arrayGroupids[i], salaybeginTime, salayendTime);
                if (dsSalay != null && dsSalay.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsSalay.Tables[0];
                    cell = arrayMonth[salayBeginCell] + salayCellNum;
                    if (salayBeginCell == 0)
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, cell, depinfo.DepartmentName);
                    }
                    salayBeginCell = 1;

                    dtfee = beginTime;
                    while (dtfee.Year <= endTime.Year && dtfee.Month <= endTime.Month)
                    {
                        cell = arrayMonth[salayBeginCell] + salayCellNum;

                        int year = beginTime.AddMonths(salayBeginCell - 1).Year;
                        int month = beginTime.AddMonths(salayBeginCell - 1).Month;
                        IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + arrayGroupids[i] + ")" + " AND [Year]=" + year + " AND [Month]=" + month);
                        if (listFee != null && listFee.Count > 0)
                        {
                            //ExcelHandle.SetFormula(excel.CurSheet, cell, listFee[0].TotalFee.ToString("0.00")); //0或者是实际数？
                            ExcelHandle.SetFormula(excel.CurSheet, cell, "0.00"); //0或者是实际数？
                        }
                        else
                        {
                            DataRow[] dr = dt.Select("yearvalue=" + dtfee.Year + " and monthvalue=" + dtfee.Month);
                            if (dr.Count() == 0)
                                ExcelHandle.WriteCell(excel.CurSheet, cell, "0.00");
                            else
                                ExcelHandle.WriteCell(excel.CurSheet, cell, dr[0]["Fee"].ToString());
                        }

                        if (dtfee.Month == 12)
                        {
                            salayBeginCell = salayBeginCell + 1;
                            cell = arrayMonth[salayBeginCell] + salayCellNum;

                            int year13 = beginTime.AddMonths(salayBeginCell - 1).Year;
                            int month13 = beginTime.AddMonths(salayBeginCell - 1).Month;
                            IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee13 = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + arrayGroupids[i] + ")" + " AND [Year]=" + year13 + " AND [Month]=" + 13);
                            if (listFee13 != null && listFee13.Count > 0)
                            {
                                //ExcelHandle.SetFormula(excel.CurSheet, cell, listFee[0].TotalFee.ToString("0.00")); //0或者是实际数？
                                ExcelHandle.SetFormula(excel.CurSheet, cell, "0.00"); //0或者是实际数？
                            }
                            else
                            {
                                DataRow[] dr = dt.Select("yearvalue=" + dtfee.Year + " and monthvalue=" + 13);
                                if (dr.Count() == 0)
                                    ExcelHandle.WriteCell(excel.CurSheet, cell, "0.00");
                                else
                                    ExcelHandle.WriteCell(excel.CurSheet, cell, dr[0]["Fee"].ToString());
                            }
                        }
                        dtfee = dtfee.AddMonths(1);
                        salayBeginCell = salayBeginCell + 1;
                    }
                }
                ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + salayCellNum, "=ROUND(SUM(" + arrayMonth[1] + salayCellNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + salayCellNum.ToString() + "),2)");
            }
            #endregion

            #region 服务费小计
            int feeTotalNum = arrayGroupids.Length + 4;
            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string feeTotalCell = string.Empty;
                feeTotalCell += arrayMonth[i] + feeTotalNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, feeTotalCell, "服务费小计：");
                }
                else
                {
                    int year = beginTime.AddMonths(i - 1).Year;
                    int month = beginTime.AddMonths(i - 1).Month;
                    IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + groupids + ")" + " AND [Year]=" + year + " AND [Month]=" + month);
                    if (listFee != null && listFee.Count > 0)
                    {
                        decimal fee = 0;
                        foreach (ESP.HumanResource.Entity.DeptFeeInfo de in listFee)
                        {
                            fee += de.TotalFee;
                        }
                        ExcelHandle.SetFormula(excel.CurSheet, feeTotalCell, fee.ToString("0.00"));
                    }
                    else
                        ExcelHandle.SetFormula(excel.CurSheet, feeTotalCell, "=ROUND(SUM(" + arrayMonth[i] + "2:" + arrayMonth[i] + (feeTotalNum - 1).ToString() + "),2)");
                }
            }
            #endregion

            //fee 总计
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + feeTotalNum, "=ROUND(SUM(" + arrayMonth[1] + feeTotalNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + feeTotalNum.ToString() + "),2)");

            #region 工资小计
            int salayTotalNum = feeTotalNum + 3;

            DateTime allreadyDate = new DateTime();
            DateTime notallreadyDate = new DateTime();
            decimal previousSalary = 0;
            //计算公式在FinanceWeb/images/a.jpg
            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string salayTotalCell = string.Empty;
                string YuGuCell = string.Empty;
                salayTotalCell += arrayMonth[i] + salayTotalNum;
                YuGuCell = arrayMonth[i] + (salayTotalNum - 1);
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, salayTotalCell, "费用小计：");
                }
                else
                {
                    int year = beginTime.AddMonths(i - 1).Year;
                    int month = beginTime.AddMonths(i - 1).Month;
                    IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + groupids + ")" + " AND [Year]=" + year + " AND [Month]=" + month);
                    if (listFee != null && listFee.Count > 0)
                    {
                        DateTime readydate = new DateTime(year, month, 1);
                        allreadyDate = readydate;
                        //实填数工资
                        decimal salary = 0;
                        foreach (ESP.HumanResource.Entity.DeptFeeInfo de in listFee)
                        {
                            salary += de.TotalSalary;
                        }
                        ExcelHandle.WriteCell(excel.CurSheet, salayTotalCell, salary);
                        previousSalary = salary;
                    }
                    else
                    {
                        //预估情况
                        DateTime notreadydate = new DateTime(year, month, 1);
                        notallreadyDate = notreadydate;
                        decimal theSalary = previousSalary;

                        //本月初的日期
                        DateTime theMonthStart = new DateTime(year, month, 1);
                        //本月末日期
                        DateTime theMonthLast = theMonthStart.AddMonths(1).AddDays(-1);
                        //上月初日期
                        DateTime priMonthStart = theMonthStart.AddMonths(-1);
                        //上月末日期
                        DateTime priMonthLast = theMonthStart.AddDays(-1);

                        DeadLineInfo deadline = deadlinelist.Where(x => x.DeadLineYear == theMonthStart.Year && x.DeadLineMonth == theMonthStart.Month).FirstOrDefault();
                        DateTime salarydt = theMonthLast;
                        if (deadline != null)
                            salarydt = deadline.SalaryDate.AddDays(-1);
                        salarydt = new DateTime(salarydt.Year, salarydt.Month, salarydt.Day, 23, 59, 59);

                        #region "相差一个月"
                        if (allreadyDate.AddMonths(1) == notallreadyDate) //相差一个月
                        {

                            //本月入职
                            var joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupids + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupids + ") and (a.TransInDate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')");
                            if (joinds != null && joinds.Count() > 0)
                            {
                                foreach (ReportJoinInfo drJoin in joinds)
                                {
                                    if (drJoin.JoinDate.Month == theMonthStart.Month)
                                    {
                                        TimeSpan sp = (System.TimeSpan)(theMonthLast - drJoin.JoinDate.AddDays(-1));
                                        theSalary += drJoin.SalaryHigh / theMonthLast.Day * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        TimeSpan sp = (System.TimeSpan)(theMonthStart - drJoin.JoinDate);
                                        theSalary += drJoin.SalaryHigh / theMonthStart.AddDays(-1).Day * (sp.Days) * deptModel.SalaryAmount;
                                        theSalary += drJoin.SalaryHigh * deptModel.SalaryAmount;
                                    }
                                }
                            }
                            //上月入职
                            var prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupids + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupids + ") and (a.TransInDate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                            if (prijoinds != null && prijoinds.Count() > 0)
                            {
                                foreach (ReportJoinInfo pridrJoin in prijoinds)
                                {
                                    if (pridrJoin.JoinDate.Month == priMonthStart.Month)
                                    {
                                        TimeSpan prisp = (System.TimeSpan)(pridrJoin.JoinDate - priMonthStart);
                                        theSalary += pridrJoin.SalaryHigh / priMonthLast.Day * (prisp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        TimeSpan prisp = (System.TimeSpan)(priMonthStart - pridrJoin.JoinDate);
                                        theSalary -= pridrJoin.SalaryHigh / priMonthStart.AddDays(-1).Day * (prisp.Days) * deptModel.SalaryAmount;
                                    }
                                }
                            }

                            //本月离职
                            var lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupids + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')", "a.OldGroupId in(" + groupids + ") and (a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                            if (lastds != null && lastds.Count() > 0)
                            {
                                foreach (ReportDimissionInfo drLast in lastds)
                                {
                                    TimeSpan sp;
                                    DateTime auditdate = drLast.AuditDate;
                                    if (drLast.Status != 12)
                                    {
                                        auditdate = salarydt;
                                    }
                                    DateTime lastday = drLast.LastDay;

                                    //下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastday >= theMonthStart && lastday < theMonthStart.AddDays(10) && auditdate.Month == lastday.Month)
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        sp = (System.TimeSpan)(lastday.AddDays(1) - theMonthStart);
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastday >= theMonthStart.AddDays(10) && lastday < salarydt)
                                    {
                                        if (auditdate <= salarydt || drLast.Status != 12)
                                            sp = (System.TimeSpan)(theMonthLast - lastday);
                                        else
                                            sp = theMonthLast.AddDays(1) - theMonthStart;
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastday >= salarydt && lastday < theMonthLast.AddDays(11))
                                    {
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    }

                                }
                            }
                            //本月审批结束的离职单
                            var prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupids + ") and a.lastday<='" + priMonthStart + "' and ((f.auditdate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "') and a.status =12)", "a.OldGroupId in(" + groupids + ") and a.TransOutDate<='" + priMonthStart + "' and ((f.auditdate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "') and a.status =9)");
                            if (prelastds != null && prelastds.Count() > 0)
                            {
                                foreach (ReportDimissionInfo drLast in prelastds)
                                {
                                    DateTime lastdaydt = drLast.LastDay;
                                    DateTime lastdaydt2 = new DateTime(lastdaydt.Year, lastdaydt.Month, 1);
                                    DateTime lastdaydt3 = lastdaydt2.AddMonths(1).AddDays(-1);
                                    TimeSpan sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);

                                    // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；

                                    if (lastdaydt >= lastdaydt2 && lastdaydt < lastdaydt2.AddDays(10))
                                    {

                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastdaydt >= lastdaydt2.AddDays(10) && lastdaydt < lastdaydt3.AddDays(1))
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    }


                                }
                            }

                            //上月离职的
                            DeadLineInfo predeadline2 = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.AddDays(-1).Year && x.DeadLineMonth == priMonthStart.AddDays(-1).Month).FirstOrDefault();
                            DateTime predeadlinedt2 = priMonthStart.AddDays(-1);
                            if (predeadline2 != null)
                                predeadlinedt2 = predeadline2.SalaryDate.AddDays(-1);
                            var prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupids + ") and ((a.lastday between '" + priMonthStart + "' and '" + priMonthLast + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))", " a.OldGroupId in(" + groupids + ") and ((a.TransOutDate between '" + priMonthStart + "' and '" + priMonthLast + "') or ((a.TransOutDate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "')))");
                            if (prilastds != null && prilastds.Count() > 0)
                            {
                                //获得上月发薪日
                                DeadLineInfo predeadline = null;
                                DateTime salaryday = priMonthLast;

                                DeadLineInfo lastdeadline = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.Year && x.DeadLineMonth == priMonthStart.Month).FirstOrDefault();
                                salaryday = salaryday.AddDays(-1);

                                DateTime lastsalaryday = priMonthLast;
                                if (lastdeadline != null && lastdeadline.SalaryDate != null)
                                {
                                    lastsalaryday = lastdeadline.SalaryDate;
                                }
                                lastsalaryday = lastsalaryday.AddDays(-1);
                                lastsalaryday = new DateTime(lastsalaryday.Year, lastsalaryday.Month, lastsalaryday.Day, 23, 59, 59);
                                foreach (ReportDimissionInfo drPreLast in prilastds)
                                {
                                    DateTime lastday = drPreLast.LastDay;
                                    DateTime lastdays = new DateTime(lastday.Year, lastday.Month, 1);
                                    lastdays = lastdays.AddMonths(1).AddDays(-1);

                                    //predeadline = DeadLineManager.GetMonthModel(lastday.Year, lastday.Month);
                                    predeadline = deadlinelist.Where(x => x.DeadLineYear == lastday.Year && x.DeadLineMonth == lastday.Month).FirstOrDefault();
                                    if (predeadline != null && predeadline.SalaryDate != null)
                                    {
                                        salaryday = predeadline.SalaryDate;
                                    }
                                    salaryday = salaryday.AddDays(-1);
                                    salaryday = new DateTime(salaryday.Year, salaryday.Month, salaryday.Day, 23, 59, 59);

                                    DateTime auditdate = drPreLast.AuditDate;
                                    if (drPreLast.Status != 12)
                                    {
                                        auditdate = salaryday;
                                    }

                                    // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastday >= priMonthStart && lastday < priMonthStart.AddDays(10))
                                    {
                                        if (drPreLast.Status == 12)
                                        {
                                            if (auditdate <= salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                                TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                            }
                                            else if (auditdate <= salarydt)
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                                TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                            }
                                        }
                                        else
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                        }

                                    }
                                    else if (lastday >= priMonthStart.AddDays(10) && lastday < priMonthLast.AddDays(1))
                                    {
                                        //上月工资发放日前办理离职手续的
                                        if (drPreLast.Status == 12)
                                        {
                                            if (auditdate < salaryday)
                                            {
                                                //减去上月工作应得工资
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                            //上月工资发放日后办理完离职手续的
                                            else
                                            {
                                                if (auditdate < lastsalaryday)
                                                    theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                                else if (auditdate <= salarydt)
                                                {
                                                    theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (lastday < salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                            else
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                        }

                                    }
                                    else if (lastday >= priMonthLast.AddDays(1) && lastday < priMonthLast.AddDays(11) && auditdate.Month == priMonthLast.AddDays(11).Month)
                                    {
                                        theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                    }
                                    else if (lastday < priMonthStart)
                                    {
                                        if (auditdate > lastsalaryday)
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                        else
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                    }


                                }
                            }
                        }
                        #endregion

                        # region "相差更多"
                        else //相差更多
                        {
                            //本月入职
                            var joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupids + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupids + ") and (a.TransInDate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')");
                            if (joinds != null && joinds.Count() > 0)
                            {
                                foreach (ReportJoinInfo drJoin in joinds)
                                {
                                    if (drJoin.JoinDate.Month == theMonthStart.Month)
                                    {
                                        TimeSpan sp = theMonthLast - drJoin.JoinDate.AddDays(-1);
                                        theSalary += drJoin.SalaryHigh / theMonthLast.Day * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        TimeSpan sp = (System.TimeSpan)(theMonthStart - drJoin.JoinDate);
                                        theSalary += drJoin.SalaryHigh / theMonthStart.AddDays(-1).Day * (sp.Days) * deptModel.SalaryAmount;
                                        theSalary += drJoin.SalaryHigh * deptModel.SalaryAmount;
                                    }
                                }
                            }

                            //上月入职
                            var prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupids + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupids + ") and (a.TransInDate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");

                            if (prijoinds != null && prijoinds.Count() > 0)
                            {
                                foreach (ReportJoinInfo pridrJoin in prijoinds)
                                {
                                    if (pridrJoin.JoinDate.Month == priMonthStart.Month)
                                    {
                                        TimeSpan prisp = (System.TimeSpan)(pridrJoin.JoinDate - priMonthStart);
                                        theSalary += pridrJoin.SalaryHigh / priMonthLast.Day * (prisp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        TimeSpan prisp = (System.TimeSpan)(priMonthStart - pridrJoin.JoinDate);
                                        theSalary -= pridrJoin.SalaryHigh / priMonthStart.AddDays(-1).Day * (prisp.Days) * deptModel.SalaryAmount;
                                    }
                                }
                            }

                            //本月离职
                            var lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupids + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')", " a.OldGroupId in(" + groupids + ") and (a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                            if (lastds != null && lastds.Count() > 0)
                            {
                                foreach (ReportDimissionInfo drLast in lastds)
                                {
                                    TimeSpan sp;
                                    DateTime auditdate = drLast.AuditDate;

                                    if (drLast.Status != 12)
                                    {
                                        auditdate = salarydt;
                                    }

                                    DateTime lastday = drLast.LastDay;

                                    //下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastday >= theMonthStart && lastday < theMonthStart.AddDays(10) && auditdate.Month == lastday.Month)
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        sp = (System.TimeSpan)(lastday.AddDays(1) - theMonthStart);
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastday >= theMonthStart.AddDays(10) && lastday < salarydt)
                                    {
                                        if (auditdate <= salarydt || drLast.Status != 12)
                                            sp = theMonthLast - lastday;
                                        else
                                            sp = theMonthLast.AddDays(1) - theMonthStart;

                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;

                                    }
                                    else if (lastday >= salarydt && lastday < theMonthLast.AddDays(11))
                                    {
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    }
                                }
                            }
                            //本月审批结束的离职单
                            var prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupids + ") and a.lastday<='" + priMonthStart.AddDays(-1) + "' and (( f.auditdate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "') and a.status =12)", " a.OldGroupId in(" + groupids + ") and a.TransOutDate<='" + priMonthStart.AddDays(-1) + "' and (( a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "'))"
);
                            if (prelastds != null && prelastds.Count() > 0)
                            {
                                foreach (ReportDimissionInfo drLast in prelastds)
                                {
                                    DateTime lastdaydt = drLast.LastDay;
                                    DateTime lastdaydt2 = new DateTime(lastdaydt.Year, lastdaydt.Month, 1);
                                    DateTime lastdaydt3 = lastdaydt2.AddMonths(1).AddDays(-1);

                                    // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastdaydt >= lastdaydt2 && lastdaydt < lastdaydt2.AddDays(10))
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        TimeSpan sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastdaydt >= lastdaydt2.AddDays(10) && lastdaydt < lastdaydt3.AddDays(1))
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (lastdaydt.Day) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    }
                                }
                            }
                            //shang月离职
                            DeadLineInfo predeadline2 = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.AddDays(-1).Year && x.DeadLineMonth == priMonthStart.AddDays(-1).Month).FirstOrDefault();
                            DateTime predeadlinedt2 = priMonthStart.AddDays(-1);
                            if (predeadline2 != null)
                                predeadlinedt2 = predeadline2.SalaryDate.AddDays(-1);
                            predeadlinedt2 = new DateTime(predeadlinedt2.Year, predeadlinedt2.Month, predeadlinedt2.Day, 23, 59, 59);
                            var prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupids + ") and ((a.lastday between '" + priMonthStart + "' and '" + priMonthLast.AddDays(1) + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))", " a.OldGroupId in(" + groupids + ") and ((a.TransOutDate between '" + priMonthStart + "' and '" + priMonthLast.AddDays(1) + "') or ((a.TransOutDate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "')))");
                            if (prilastds != null && prilastds.Count() > 0)
                            {
                                //获得上月发薪日
                                DeadLineInfo predeadline = null;
                                DateTime salaryday = priMonthLast;
                                DeadLineInfo lastdeadline = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.Year && x.DeadLineMonth == priMonthStart.Month).FirstOrDefault();
                                salaryday = salaryday.AddDays(-1);

                                DateTime lastsalaryday = priMonthLast;
                                if (lastdeadline != null && lastdeadline.SalaryDate != null)
                                {
                                    lastsalaryday = lastdeadline.SalaryDate;
                                }
                                lastsalaryday = lastsalaryday.AddDays(-1);
                                foreach (ReportDimissionInfo drPreLast in prilastds)
                                {
                                    DateTime lastday = drPreLast.LastDay;
                                    DateTime lastdays = new DateTime(lastday.Year, lastday.Month, 1);
                                    lastdays = lastdays.AddMonths(1).AddDays(-1);

                                    predeadline = deadlinelist.Where(x => x.DeadLineYear == lastday.Year && x.DeadLineMonth == lastday.Month).FirstOrDefault();
                                    if (predeadline != null && predeadline.SalaryDate != null)
                                    {
                                        salaryday = predeadline.SalaryDate;
                                    }
                                    salaryday = salaryday.AddDays(-1);
                                    salaryday = new DateTime(salaryday.Year, salaryday.Month, salaryday.Day, 23, 59, 59);
                                    //上月工资发放日前办理离职手续的
                                    DateTime auditdate = drPreLast.AuditDate;
                                    if (drPreLast.Status != 12)
                                    {
                                        auditdate = salaryday;
                                    }
                                    // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastday >= priMonthStart && lastday < priMonthStart.AddDays(10))
                                    {
                                        if (auditdate <= salaryday || drPreLast.Status != 12)
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                        }
                                        else if (auditdate <= salarydt && drPreLast.Status == 12)
                                        {
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                        }

                                    }
                                    else if (lastday >= priMonthStart.AddDays(10) && lastday < priMonthLast.AddDays(1))
                                    {

                                        if (drPreLast.Status == 12)
                                        {
                                            if (auditdate < lastsalaryday || auditdate <= salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                            else if (auditdate > salaryday && auditdate < salarydt)
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                        }
                                        else
                                        {
                                            if (lastday < salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                            else
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                        }
                                    }
                                    else if ((lastday < priMonthStart))
                                    {
                                        if (drPreLast.Status == 12)
                                        {
                                            if (auditdate > lastsalaryday)
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                            }
                                            else
                                            {
                                                DateTime lastmonth = new DateTime(lastday.Year, lastday.Month, 1);
                                                if (lastday >= lastmonth && lastday < lastmonth.AddDays(10))
                                                {
                                                    theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                                    theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                                }
                                                else
                                                {
                                                    theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion


                        previousSalary = theSalary;

                        if (hashYearMonth[arrayMonth[i] + "1"] != null && hashYearMonth[arrayMonth[i] + "1"].ToString().IndexOf("13月") > 0)
                            ExcelHandle.WriteCell(excel.CurSheet, salayTotalCell, "0.00");
                        else
                        {
                            ExcelHandle.WriteCell(excel.CurSheet, salayTotalCell, theSalary);
                        }
                        ExcelHandle.WriteCell(excel.CurSheet, YuGuCell, "预估费用");

                    }
                }
            }
            #endregion

            //工资 总计
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + salayTotalNum, "=ROUND(SUM(" + arrayMonth[1] + salayTotalNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + salayTotalNum.ToString() + "),2)");

            #region 写PBT
            int pbtNum = salayTotalNum + 2;
            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string pbtTotalCell = string.Empty;
                pbtTotalCell += arrayMonth[i] + pbtNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, pbtTotalCell, "PBT：");
                }
                else
                {
                    ExcelHandle.SetFormula(excel.CurSheet, pbtTotalCell, "=ROUND(" + arrayMonth[i] + feeTotalNum + "-" + arrayMonth[i] + salayTotalNum + ",2)");
                }
            }
            #endregion


            WriteLastEmployees(beginTime, endTime, groupids, excel, userid);
            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }


        private static void WriteLastEmployees(DateTime beginTime, DateTime endTime, string groupids, ExcelHandle excel, int userid)
        {
            string[] arrayGroupids = groupids.Split(',');
            if (arrayGroupids.Length == 0)
                return;
            ESP.HumanResource.Entity.DeptFeeInfo feeModel = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetMaxMonthModel();
            int FeeModelMaxMonth = feeModel.Month == 13 ? 12 : feeModel.Month;
            DateTime FeeDate = new DateTime(feeModel.Year, FeeModelMaxMonth, 1);

            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[2];
            string cell = string.Empty;

            DateTime salayendTime = new DateTime(endTime.Year, endTime.Month, 1);
            salayendTime = salayendTime.AddMonths(1).AddDays(-1);

            DeadLineInfo deadline = DeadLineManager.GetMonthModel(FeeDate.Year, FeeDate.Month - 1);
            DateTime salaryDate = FeeDate;
            if (deadline != null)
                salaryDate = deadline.SalaryDate.AddDays(-1);

            ExcelHandle.WriteCell(excel.CurSheet, "A1", "员工编号");
            ExcelHandle.WriteCell(excel.CurSheet, "B1", "姓名");
            ExcelHandle.WriteCell(excel.CurSheet, "C1", "Title");
            ExcelHandle.WriteCell(excel.CurSheet, "D1", "部门");
            ExcelHandle.WriteCell(excel.CurSheet, "E1", "入离职日期");
            ExcelHandle.WriteCell(excel.CurSheet, "F1", "工资上限");
            ExcelHandle.WriteCell(excel.CurSheet, "G1", "工资下限");
            ExcelHandle.WriteCell(excel.CurSheet, "H1", "操作类型");
            ExcelHandle.WriteCell(excel.CurSheet, "I1", "当月工资发放日");
            ExcelHandle.WriteCell(excel.CurSheet, "J1", "HeadCount");

            int rowno = 2;
            //期间内入职
            var joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupids + ") and (a.joindate between '" + salaryDate.AddDays(-9) + "' and '" + salayendTime + "')", "a.NewGroupId in(" + groupids + ") and (a.TransInDate between '" + salaryDate.AddDays(-9) + "' and '" + salayendTime + "')");
            foreach (ReportJoinInfo drjoin in joinds)
            {
                ExcelHandle.WriteCell(excel.CurSheet, "A" + rowno, "'" + drjoin.Code);
                ExcelHandle.WriteCell(excel.CurSheet, "B" + rowno, drjoin.UserName);
                ExcelHandle.WriteCell(excel.CurSheet, "C" + rowno, drjoin.DepartmentPositionName);
                ExcelHandle.WriteCell(excel.CurSheet, "D" + rowno, drjoin.DepartmentName);
                ExcelHandle.WriteCell(excel.CurSheet, "E" + rowno, drjoin.JoinDate.ToString("yyyy-MM-dd"));
                if ( userid == int.Parse(System.Configuration.ConfigurationManager.AppSettings["DavidZhangID"]) )
                {
                    ExcelHandle.WriteCell(excel.CurSheet, "F" + rowno, drjoin.SalaryHigh.ToString());
                    ExcelHandle.WriteCell(excel.CurSheet, "G" + rowno, drjoin.SalaryLow.ToString());
                }
                ExcelHandle.WriteCell(excel.CurSheet, "H" + rowno, drjoin.OperationType);
                ExcelHandle.WriteCell(excel.CurSheet, "I" + rowno, drjoin.SalaryDate.ToString("yyyy-MM-dd"));

                string hcId = drjoin.HeadCountId.ToString();
                while (hcId.Length < 5)
                {
                    hcId = "0" + hcId;
                }
                if (hcId == "00000")
                    hcId = string.Empty;
                else
                    hcId = "HC" + hcId;

                ExcelHandle.WriteCell(excel.CurSheet, "J" + rowno, hcId);
                rowno += 1;
            }

            var lastds2 = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupids + ") and ((a.lastday between '" + salaryDate + "' and '" + salayendTime.AddDays(11) + "') or (auditdate between '" + salaryDate + "' and '" + salayendTime.AddDays(11) + "'))", " a.OldGroupId in(" + groupids + ") and ((a.TransOutDate between '" + salaryDate + "' and '" + salayendTime.AddDays(11) + "') or (a.TransOutDate between '" + salaryDate + "' and '" + salayendTime.AddDays(11) + "'))"
);

            foreach (ReportDimissionInfo drlast in lastds2)
            {
                ExcelHandle.WriteCell(excel.CurSheet, "A" + rowno, "'" + drlast.Code);
                ExcelHandle.WriteCell(excel.CurSheet, "B" + rowno, drlast.UserName);
                ExcelHandle.WriteCell(excel.CurSheet, "C" + rowno, drlast.DepartmentPositionName);
                ExcelHandle.WriteCell(excel.CurSheet, "D" + rowno, drlast.DepartmentName);
                ExcelHandle.WriteCell(excel.CurSheet, "E" + rowno, drlast.LastDay.ToString("yyyy-MM-dd"));
                if (userid == int.Parse(System.Configuration.ConfigurationManager.AppSettings["DavidZhangID"]) )
                {
                    ExcelHandle.WriteCell(excel.CurSheet, "F" + rowno, drlast.SalaryHigh.ToString());
                    ExcelHandle.WriteCell(excel.CurSheet, "G" + rowno, drlast.SalaryLow.ToString());
                }
                ExcelHandle.WriteCell(excel.CurSheet, "H" + rowno, drlast.OperationType);
                ExcelHandle.WriteCell(excel.CurSheet, "I" + rowno, drlast.SalaryDate.ToString("yyyy-MM-dd"));
                rowno += 1;
            }

        }

        public static string ExportDeptSalayForProject(int currentuserid, string groupid, string groupname, int userid, DataTable dt, System.Web.HttpResponse response, DateTime beginTime, DateTime endTime)
        {
            string[] groupids = groupid.Split(',');
            ESP.Finance.Entity.DepartmentViewInfo deptModel = null;
            if (groupids.Count() == 1)
                deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(int.Parse(groupids[0]));
            IList<ESP.Finance.Entity.ProjectScheduleInfo> schedulelist = ESP.Finance.BusinessLogic.ProjectScheduleManager.GetList(" YearValue in(" + beginTime.Year.ToString() + "," + endTime.Year.ToString() + ")");

            IList<ESP.Finance.Entity.ProjectReportFeeInfo> reportFeeList = ESP.Finance.BusinessLogic.ProjectReportFeeManager.GetList(" deptid in(" + groupid + ") and year in(" + beginTime.Year.ToString() + "," + endTime.Year.ToString() + ")", null);

            IList<ESP.Finance.Entity.SupporterScheduleInfo> supportSchedule = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" YearValue in(" + beginTime.Year.ToString() + "," + endTime.Year.ToString() + ")");
            ESP.HumanResource.Entity.DeptFeeInfo feeModel = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetMaxMonthModel();
            IList<DeadLineInfo> deadlinelist = DeadLineManager.GetAllList();
            int FeeModelMaxMonth = feeModel.Month == 13 ? 12 : feeModel.Month;

            DateTime FeeDate = new DateTime(feeModel.Year, FeeModelMaxMonth, 1);
            string temppath = "/Tmp/Salary/ProjectPBTTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            // IList<ReportJoinInfo> joinlist = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + FeeDate.AddDays(-9) + "' and '" + endTime.AddDays(-9) + "')");
            // IList<ReportDimissionInfo> dimissionlist = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + FeeDate + "' and '" + endTime.AddDays(11) + "')");
            IList<DeptTargetInfo> targetlist = ESP.Finance.BusinessLogic.DeptTargetManager.GetList("deptid in(" + groupid + ") and year =" + beginTime.Year.ToString());
            decimal deptTarget = 0;
            foreach (DeptTargetInfo dept in targetlist)
            {
                deptTarget += dept.Target;
            }
            string[] arrayMonth = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            string cell = string.Empty;

            int yearmonthBeginCell = 2;
            string yearmonthCellNum = "4";

            DateTime salaybeginTime = beginTime;
            DateTime salayendTime = endTime;
            Hashtable hashYearMonth = new Hashtable();

            cell = "A1";
            ExcelHandle.WriteCell(excel.CurSheet, cell, "团队：");
            cell = "B1";
            if (deptModel != null)
                ExcelHandle.WriteCell(excel.CurSheet, cell, deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3);
            cell = "A4";
            ExcelHandle.WriteCell(excel.CurSheet, cell, "项目号");
            cell = "B4";
            ExcelHandle.WriteCell(excel.CurSheet, cell, "项目名称");

            DateTime begintime1 = beginTime;
            DateTime begintime2 = beginTime;
            DateTime begintime3 = beginTime;
            DateTime begintimeTax = beginTime;


            while (begintime2.Year <= endTime.Year)
            {
                cell = arrayMonth[yearmonthBeginCell] + yearmonthCellNum;
                ExcelHandle.WriteCell(excel.CurSheet, cell, begintime2.Year.ToString() + "年" + begintime2.Month + "月");
                hashYearMonth.Add(cell, begintime2.Year.ToString() + "年" + begintime2.Month + "月");

                if (begintime2.Month == 12)
                {
                    yearmonthBeginCell = yearmonthBeginCell + 1;
                    cell = arrayMonth[yearmonthBeginCell] + yearmonthCellNum;
                    ExcelHandle.WriteCell(excel.CurSheet, cell, begintime2.Year.ToString() + "年13月");
                    hashYearMonth.Add(cell, begintime2.Year.ToString() + "年13月");
                }

                begintime2 = begintime2.AddMonths(1);
                yearmonthBeginCell = yearmonthBeginCell + 1;
                if (begintime2.Year == endTime.Year && begintime2.Month > endTime.Month)
                    break;
            }
            cell = arrayMonth[yearmonthBeginCell] + yearmonthCellNum;
            ExcelHandle.WriteCell(excel.CurSheet, cell, "合计");
            ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[yearmonthBeginCell + 1] + yearmonthCellNum, "是否支持方");
            ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[yearmonthBeginCell + 2] + yearmonthCellNum, "项目组别");

            #region write project info
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string salayCellNum = (i + 6).ToString();
                    int salayBeginCell = 2;

                    cell = "A" + salayCellNum;
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ProjectCode"].ToString());
                    if (dt.Rows[i]["status"].ToString() != "32" && dt.Rows[i]["status"].ToString() != "33" && dt.Rows[i]["status"].ToString() != "34")
                        ExcelHandle.SetBackGroundColor(excel.CurSheet, cell, cell, System.Drawing.Color.Green);
                    cell = "B" + salayCellNum;
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["Businessdescription"].ToString());
                    if (dt.Rows[i]["status"].ToString() != "32" && dt.Rows[i]["status"].ToString() != "33" && dt.Rows[i]["status"].ToString() != "34")
                        ExcelHandle.SetBackGroundColor(excel.CurSheet, cell, cell, System.Drawing.Color.Green);
                    begintime1 = beginTime;
                    while (begintime1.Year <= endTime.Year && begintime1.Month <= endTime.Month)
                    {//begintime1.Month <= endTime.Month这个判断有问题，跨年错误
                        ESP.Finance.Entity.ProjectScheduleInfo projectSchuleModel = null;
                        ESP.Finance.Entity.ProjectReportFeeInfo reportFeeModel = null;
                        ESP.Finance.Entity.SupporterScheduleInfo supportScheduleModel = null;
                        string projectType = string.Empty;

                        if (dt.Rows[i]["Projecttype"].ToString().Trim() == "主")
                        {
                            projectType = "主";
                            supportScheduleModel = null;
                            if (int.Parse(dt.Rows[i]["GroupId"].ToString()) == deptModel.level3Id)
                                projectSchuleModel = schedulelist.Where(x => x.ProjectID == int.Parse(dt.Rows[i]["ProjectId"].ToString()) && x.YearValue == begintime1.Year && x.monthValue == begintime1.Month).FirstOrDefault();
                        }
                        else
                        {
                            projectType = "支";
                            projectSchuleModel = null;
                            supportScheduleModel = supportSchedule.Where(x => x.SupporterID == int.Parse(dt.Rows[i]["SupportID"].ToString()) && x.YearValue == begintime1.Year && x.monthValue == begintime1.Month).FirstOrDefault();
                        }
                        //&& x.DeptId == int.Parse(dt.Rows[i]["GroupId"].ToString())
                        reportFeeModel = reportFeeList.Where(x => x.ProjectCode == dt.Rows[i]["ProjectCode"].ToString().Trim() && x.DeptId == deptModel.level3Id && x.Year == begintime1.Year && x.Month == begintime1.Month && x.ProjectType == projectType).FirstOrDefault();

                        decimal fee = 0;
                        if (projectSchuleModel != null)
                            fee = projectSchuleModel.Fee == null ? 0 : projectSchuleModel.Fee.Value;
                        else if (supportScheduleModel != null)
                            fee = supportScheduleModel.Fee == null ? 0 : supportScheduleModel.Fee.Value;
                        else
                            fee = 0;

                        if (reportFeeModel != null)
                        {
                            if (reportFeeModel.DeptId == deptModel.level3Id)
                                fee = reportFeeModel.Fee;
                            else
                                fee = 0;
                        }

                        cell = arrayMonth[salayBeginCell] + salayCellNum;
                        //IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + groupid + ")" + " AND [Year]=" + begintime1.Year + " AND [Month]=" + begintime1.Month);

                        ExcelHandle.WriteCell(excel.CurSheet, cell, fee.ToString("0.00"));

                        if (begintime1.Month == 12)
                        {
                            salayBeginCell = salayBeginCell + 1;
                            cell = arrayMonth[salayBeginCell] + salayCellNum;


                            IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee13 = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + groupid + ")" + " AND [Year]=" + begintime1.Year + " AND [Month]=" + 13);
                            if (listFee13 != null && listFee13.Count > 0)
                            {
                                ExcelHandle.SetFormula(excel.CurSheet, cell, "0.00"); //0或者是实际数？
                            }
                            else
                            {

                                decimal fee13 = 0;
                                if (dt.Rows[i]["Projecttype"].ToString().Trim() == "主")
                                {
                                    projectType = "主";
                                    var s13 = schedulelist.Where(x => x.ProjectID == int.Parse(dt.Rows[i]["ProjectId"].ToString()) && x.YearValue == begintime1.Year && x.monthValue == 13).FirstOrDefault();
                                    fee13 = s13 == null ? 0 : s13.Fee.Value;
                                }
                                else
                                {
                                    projectType = "支";
                                    var supporter13 = supportSchedule.Where(x => x.SupporterID == int.Parse(dt.Rows[i]["SupportID"].ToString()) && x.YearValue == begintime1.Year && x.monthValue == 13).FirstOrDefault();
                                    fee13 = supporter13 == null ? 0 : supporter13.Fee.Value;
                                }

                                reportFeeModel = reportFeeList.Where(x => x.ProjectCode == dt.Rows[i]["ProjectCode"].ToString().Trim() && x.DeptId == deptModel.level3Id && x.Year == begintime1.Year && x.Month == 13 && x.ProjectType == projectType).FirstOrDefault();

                                if (reportFeeModel != null)
                                {
                                    if (reportFeeModel.DeptId == deptModel.level3Id)
                                        fee13 = reportFeeModel.Fee;
                                    else
                                        fee13 = 0;
                                }

                                ExcelHandle.WriteCell(excel.CurSheet, cell, fee13.ToString("f2"));
                            }
                        }


                        begintime1 = begintime1.AddMonths(1);
                        salayBeginCell++;
                    }
                    cell = arrayMonth[salayBeginCell + 1] + salayCellNum;
                    if (dt.Rows[i]["Projecttype"].ToString().Trim() != "主")
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ProjectType"].ToString());
                    }
                    ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[salayBeginCell + 2] + salayCellNum, dt.Rows[i]["DeptName"].ToString());

                    ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + salayCellNum, "=ROUND(SUM(" + arrayMonth[2] + salayCellNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + salayCellNum.ToString() + "),2)");
                }

            }
            #endregion

            //2019修改，增加税费及其他
            int taxNum = dt.Rows.Count + 8;
            string taxCell = string.Empty;
            taxCell = arrayMonth[1] + taxNum;
            ExcelHandle.WriteCell(excel.CurSheet, taxCell, "税费及其它：");
            int taxBeginCell = 2;
            while (begintimeTax.Year <= endTime.Year && begintimeTax.Month <= endTime.Month)
            {
                taxCell = arrayMonth[taxBeginCell] + taxNum;

                ESP.Finance.Entity.TeamTaxInfo taxModel = ESP.Finance.BusinessLogic.TeamTaxManager.GetModel(int.Parse(groupids[0]), begintimeTax.Year, begintimeTax.Month);

                decimal tax = 0;
                if (taxModel != null)
                    tax = taxModel.Tax;
                ExcelHandle.WriteCell(excel.CurSheet, taxCell, tax.ToString("0.00"));
                taxBeginCell++;
                begintimeTax = begintimeTax.AddMonths(1);
            }
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + taxNum, "=ROUND(SUM(" + arrayMonth[2] + taxNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + taxNum.ToString() + "),2)");



            //int feeTotalNum = dt.Rows.Count + 8;
            int feeTotalNum = dt.Rows.Count + 9;
            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string feeTotalCell = string.Empty;
                feeTotalCell = arrayMonth[i + 1] + feeTotalNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, feeTotalCell, "服务费小计：");
                }
                else
                {
                    ExcelHandle.SetFormula(excel.CurSheet, feeTotalCell, "=ROUND(SUM(" + arrayMonth[i + 1] + "6:" + arrayMonth[i + 1] + (feeTotalNum - 1).ToString() + "),2)");
                }
            }
            //fee 总计
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + feeTotalNum, "=ROUND(SUM(" + arrayMonth[2] + feeTotalNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + feeTotalNum.ToString() + "),2)");


            #region 费用小计
            int salayTotalNum = dt.Rows.Count + 10;
            DateTime allreadyDate = new DateTime();
            DateTime notallreadyDate = new DateTime();
            decimal previousSalary = 0;

            //****************begin****************//
            DateTime differBegintime = beginTime;
            DateTime differFeeDate = FeeDate;

            while (differBegintime > differFeeDate)
            {
                int year = differFeeDate.Year;
                int month = differFeeDate.Month;
                IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + groupid + ")" + " AND [Year]=" + year + " AND [Month]=" + month);
                if (listFee != null && listFee.Count > 0)
                {
                    DateTime readydate = new DateTime(year, month, 1);
                    allreadyDate = readydate;
                    //实填数工资
                    decimal salary = 0;
                    foreach (ESP.HumanResource.Entity.DeptFeeInfo de in listFee)
                    {
                        salary += de.TotalSalary;
                    }
                    previousSalary = salary;

                    differFeeDate = differFeeDate.AddMonths(1);
                }
                else
                {
                    //预估情况
                    DateTime notreadydate = new DateTime(year, month, 1);
                    notallreadyDate = notreadydate;
                    decimal theSalary = previousSalary;

                    //本月初的日期
                    DateTime theMonthStart = new DateTime(year, month, 1);
                    //本月末日期
                    DateTime theMonthLast = theMonthStart.AddMonths(1).AddDays(-1);
                    //上月初日期
                    DateTime priMonthStart = theMonthStart.AddMonths(-1);
                    //上月末日期
                    DateTime priMonthLast = theMonthStart.AddDays(-1);

                    //DeadLineInfo deadline = DeadLineManager.GetMonthModel(theMonthStart.Year, theMonthStart.Month);
                    DeadLineInfo deadline = deadlinelist.Where(x => x.DeadLineYear == theMonthStart.Year && x.DeadLineMonth == theMonthStart.Month).FirstOrDefault();

                    DateTime salarydt = theMonthLast;
                    if (deadline != null)
                        salarydt = deadline.SalaryDate.AddDays(-1);
                    salarydt = new DateTime(salarydt.Year, salarydt.Month, salarydt.Day, 23, 59, 59);
                    //

                    #region "相差一个月"
                    if (allreadyDate.AddMonths(1) == notallreadyDate) //相差一个月
                    {

                        //本月入职
                        //"b.departmentid in(" + groupid + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')"
                        var joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupid + ") and (a.TransInDate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')");
                        //joinlist.Where(x => x.JoinDate >= theMonthStart.AddDays(-9) && x.JoinDate < theMonthLast.AddDays(-9));
                        if (joinds != null && joinds.Count() > 0)
                        {
                            foreach (ReportJoinInfo drJoin in joinds)
                            {
                                if (drJoin.JoinDate.Month == theMonthStart.Month)
                                {
                                    TimeSpan sp = (System.TimeSpan)(theMonthLast - drJoin.JoinDate.AddDays(-1));
                                    theSalary += drJoin.SalaryHigh / theMonthLast.Day * (sp.Days) * deptModel.SalaryAmount;
                                }
                                else
                                {
                                    TimeSpan sp = (System.TimeSpan)(theMonthStart - drJoin.JoinDate);
                                    theSalary += drJoin.SalaryHigh / theMonthStart.AddDays(-1).Day * (sp.Days) * deptModel.SalaryAmount;
                                    theSalary += drJoin.SalaryHigh * deptModel.SalaryAmount;
                                }
                            }
                        }
                        //上月入职
                        // DataSet prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                        var prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupid + ") and (a.TransInDate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                        //joinlist.Where(x => x.JoinDate >= priMonthStart.AddDays(-9) && x.JoinDate < priMonthLast.AddDays(-9));
                        if (prijoinds != null && prijoinds.Count() > 0)
                        {
                            foreach (ReportJoinInfo pridrJoin in prijoinds)
                            {
                                if (pridrJoin.JoinDate.Month == priMonthStart.Month)
                                {
                                    TimeSpan prisp = (System.TimeSpan)(pridrJoin.JoinDate - priMonthStart);
                                    theSalary += pridrJoin.SalaryHigh / priMonthLast.Day * (prisp.Days) * deptModel.SalaryAmount;
                                }
                                else
                                {
                                    TimeSpan prisp = (System.TimeSpan)(priMonthStart - pridrJoin.JoinDate);
                                    theSalary -= pridrJoin.SalaryHigh / priMonthStart.AddDays(-1).Day * (prisp.Days) * deptModel.SalaryAmount;
                                }
                            }
                        }

                        //本月离职
                        // DataSet lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                        var lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')", " a.OldGroupId in(" + groupid + ") and (a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                        //dimissionlist.Where(x => x.LastDay >= theMonthStart && x.LastDay < theMonthLast.AddDays(11));
                        if (lastds != null && lastds.Count() > 0)
                        {
                            foreach (ReportDimissionInfo drLast in lastds)
                            {
                                TimeSpan sp;
                                DateTime auditdate = drLast.AuditDate;
                                if (drLast.Status != 12)
                                {
                                    auditdate = salarydt;
                                }
                                DateTime lastday = drLast.LastDay;

                                //下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                if (lastday >= theMonthStart && lastday < theMonthStart.AddDays(10) && auditdate.Month == lastday.Month)
                                {
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    sp = (System.TimeSpan)(lastday.AddDays(1) - theMonthStart);
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                }
                                else if (lastday >= theMonthStart.AddDays(10) && lastday < salarydt)
                                {
                                    if (auditdate <= salarydt || drLast.Status != 12)
                                        sp = (System.TimeSpan)(theMonthLast - lastday);
                                    else
                                        sp = theMonthLast.AddDays(1) - theMonthStart;
                                    theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                }
                                else if (lastday >= salarydt && lastday < theMonthLast.AddDays(11))
                                {
                                    theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                }

                            }
                        }
                        //本月审批结束的离职单
                        //strSql.Append(" Where emp.status not in(6) and a.status=12 and (f.auditdate between '" + monthSalaryDate.ToString("yyyy-MM-dd hh:ss:mm") + "' and '" + monthSalaryDateEnd.ToString("yyyy-MM-dd hh:ss:mm") + "') ");

                        //DataSet prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetPreMonthLastDS("a.departmentid in(" + groupid + ") and a.lastday<='" + priMonthStart + "'", theMonthStart, theMonthLast.AddDays(1));
                        var prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and a.lastday<='" + priMonthStart + "' and (f.auditdate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "')", " a.OldGroupId in(" + groupid + ") and a.TransOutDate<='" + priMonthStart + "' and (a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "')"
);
                        //dimissionlist.Where(x => x.Status == 12 && (x.AuditDate >= theMonthStart && x.AuditDate < theMonthLast.AddDays(1)) && x.LastDay <= priMonthStart);
                        if (prelastds != null && prelastds.Count() > 0)
                        {
                            foreach (ReportDimissionInfo drLast in prelastds)
                            {
                                DateTime lastdaydt = drLast.LastDay;
                                DateTime lastdaydt2 = new DateTime(lastdaydt.Year, lastdaydt.Month, 1);
                                DateTime lastdaydt3 = lastdaydt2.AddMonths(1).AddDays(-1);
                                TimeSpan sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);

                                // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；

                                if (lastdaydt >= lastdaydt2 && lastdaydt < lastdaydt2.AddDays(10))
                                {

                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                }
                                else if (lastdaydt >= lastdaydt2.AddDays(10) && lastdaydt < lastdaydt3.AddDays(1))
                                {
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                }
                                else
                                {
                                    theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                }


                            }
                        }

                        //上月离职的
                        //                            DeadLineInfo predeadline2 = DeadLineManager.GetMonthModel(priMonthStart.AddDays(-1).Year, priMonthStart.AddDays(-1).Month);
                        DeadLineInfo predeadline2 = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.AddDays(-1).Year && x.DeadLineMonth == priMonthStart.AddDays(-1).Month).FirstOrDefault();
                        DateTime predeadlinedt2 = priMonthStart.AddDays(-1);
                        if (predeadline2 != null)
                            predeadlinedt2 = predeadline2.SalaryDate.AddDays(-1);
                        // DataSet prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and ((a.lastday between '" + priMonthStart + "' and '" + priMonthLast + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))");
                        var prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and ((a.lastday between '" + priMonthStart + "' and '" + priMonthLast + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))", " a.OldGroupId in(" + groupid + ") and ((a.TransOutDate between '" + priMonthStart + "' and '" + priMonthLast + "') or ((a.TransOutDate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "')))");
                        //dimissionlist.Where(x => ((x.LastDay >= priMonthStart && x.LastDay < priMonthLast.AddDays(1)) || (x.AuditDate >= predeadlinedt2 && x.AuditDate < priMonthLast.AddDays(1))));
                        if (prilastds != null && prilastds.Count() > 0)
                        {
                            //获得上月发薪日
                            DeadLineInfo predeadline = null;
                            DateTime salaryday = priMonthLast;

                            //DeadLineInfo lastdeadline = DeadLineManager.GetMonthModel(priMonthStart.Year, priMonthStart.Month); ;
                            DeadLineInfo lastdeadline = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.Year && x.DeadLineMonth == priMonthStart.Month).FirstOrDefault();
                            salaryday = salaryday.AddDays(-1);

                            DateTime lastsalaryday = priMonthLast;
                            if (lastdeadline != null && lastdeadline.SalaryDate != null)
                            {
                                lastsalaryday = lastdeadline.SalaryDate;
                            }
                            lastsalaryday = lastsalaryday.AddDays(-1);
                            lastsalaryday = new DateTime(lastsalaryday.Year, lastsalaryday.Month, lastsalaryday.Day, 23, 59, 59);
                            foreach (ReportDimissionInfo drPreLast in prilastds)
                            {
                                DateTime lastday = drPreLast.LastDay;
                                DateTime lastdays = new DateTime(lastday.Year, lastday.Month, 1);
                                lastdays = lastdays.AddMonths(1).AddDays(-1);

                                //predeadline = DeadLineManager.GetMonthModel(lastday.Year, lastday.Month);
                                predeadline = deadlinelist.Where(x => x.DeadLineYear == lastday.Year && x.DeadLineMonth == lastday.Month).FirstOrDefault();
                                if (predeadline != null && predeadline.SalaryDate != null)
                                {
                                    salaryday = predeadline.SalaryDate;
                                }
                                salaryday = salaryday.AddDays(-1);
                                salaryday = new DateTime(salaryday.Year, salaryday.Month, salaryday.Day, 23, 59, 59);

                                DateTime auditdate = drPreLast.AuditDate;
                                if (drPreLast.Status != 12)
                                {
                                    auditdate = salaryday;
                                }

                                // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                if (lastday >= priMonthStart && lastday < priMonthStart.AddDays(10))
                                {
                                    if (drPreLast.Status == 12)
                                    {
                                        if (auditdate <= salaryday)
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                        }
                                        else if (auditdate <= salarydt)
                                        {
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                        }
                                    }
                                    else
                                    {
                                        theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                        theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }

                                }
                                else if (lastday >= priMonthStart.AddDays(10) && lastday < priMonthLast.AddDays(1))
                                {
                                    //上月工资发放日前办理离职手续的

                                    if (drPreLast.Status == 12)
                                    {
                                        if (auditdate < lastsalaryday || auditdate <= salaryday)
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                        }
                                        else if (auditdate > salaryday && auditdate < salarydt)
                                        {
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                        }
                                    }
                                    else
                                    {
                                        if (lastday < salaryday)
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                        }
                                        else
                                        {
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                        }
                                    }

                                }
                                else if (lastday >= priMonthLast.AddDays(1) && lastday < priMonthLast.AddDays(11) && auditdate.Month == priMonthLast.AddDays(11).Month)
                                {
                                    theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                }
                                else if (lastday < priMonthStart)
                                {
                                    if (auditdate > lastsalaryday)
                                        theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                    else
                                        theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                }


                            }
                        }
                    }
                    #endregion

                    # region "相差更多"
                    else //相差更多
                    {
                        //本月入职
                        //DataSet joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')");
                        var joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupid + ") and (a.TransInDate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')");
                        //joinlist.Where(x => x.JoinDate >= theMonthStart.AddDays(-9) && x.JoinDate < theMonthLast.AddDays(-9));
                        if (joinds != null && joinds.Count() > 0)
                        {
                            foreach (ReportJoinInfo drJoin in joinds)
                            {
                                if (drJoin.JoinDate.Month == theMonthStart.Month)
                                {
                                    TimeSpan sp = theMonthLast - drJoin.JoinDate.AddDays(-1);
                                    theSalary += drJoin.SalaryHigh / theMonthLast.Day * (sp.Days) * deptModel.SalaryAmount;
                                }
                                else
                                {
                                    TimeSpan sp = (System.TimeSpan)(theMonthStart - drJoin.JoinDate);
                                    theSalary += drJoin.SalaryHigh / theMonthStart.AddDays(-1).Day * (sp.Days) * deptModel.SalaryAmount;
                                    theSalary += drJoin.SalaryHigh * deptModel.SalaryAmount;
                                }
                            }
                        }

                        //上月入职
                        //DataSet prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                        var prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupid + ") and (a.TransInDate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                        //joinlist.Where(x => x.JoinDate >= priMonthStart.AddDays(-9) && x.JoinDate < priMonthLast.AddDays(-9));

                        if (prijoinds != null && prijoinds.Count() > 0)
                        {
                            foreach (ReportJoinInfo pridrJoin in prijoinds)
                            {
                                if (pridrJoin.JoinDate.Month == priMonthStart.Month)
                                {
                                    TimeSpan prisp = (System.TimeSpan)(pridrJoin.JoinDate - priMonthStart);
                                    theSalary += pridrJoin.SalaryHigh / priMonthLast.Day * (prisp.Days) * deptModel.SalaryAmount;
                                }
                                else
                                {
                                    TimeSpan prisp = (System.TimeSpan)(priMonthStart - pridrJoin.JoinDate);
                                    theSalary -= pridrJoin.SalaryHigh / priMonthStart.AddDays(-1).Day * (prisp.Days) * deptModel.SalaryAmount;
                                }
                            }
                        }

                        //本月离职
                        //DataSet lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                        var lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')", " a.OldGroupId in(" + groupid + ") and (a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                        //dimissionlist.Where(x => x.LastDay >= theMonthStart && x.LastDay < theMonthLast.AddDays(11));
                        if (lastds != null && lastds.Count() > 0)
                        {
                            foreach (ReportDimissionInfo drLast in lastds)
                            {
                                TimeSpan sp;
                                DateTime auditdate = drLast.AuditDate;

                                if (drLast.Status != 12)
                                {
                                    auditdate = salarydt;
                                }

                                DateTime lastday = drLast.LastDay;

                                //下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                if (lastday >= theMonthStart && lastday < theMonthStart.AddDays(10) && auditdate.Month == lastday.Month)
                                {
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    sp = (System.TimeSpan)(lastday.AddDays(1) - theMonthStart);
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                }
                                else if (lastday >= theMonthStart.AddDays(10) && lastday < salarydt)
                                {
                                    if (auditdate <= salarydt || drLast.Status != 12)
                                        sp = theMonthLast - lastday;
                                    else
                                        sp = theMonthLast.AddDays(1) - theMonthStart;

                                    theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;

                                }
                                else if (lastday >= salarydt && lastday < theMonthLast.AddDays(11))
                                {
                                    theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                }
                            }
                        }
                        //本月审批结束的离职单
                        // //strSql.Append(" Where emp.status not in(6) and a.status=12 and (f.auditdate between '" + monthSalaryDate.ToString("yyyy-MM-dd hh:ss:mm") + "' and '" + monthSalaryDateEnd.ToString("yyyy-MM-dd hh:ss:mm") + "') ");
                        //DataSet prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetPreMonthLastDS("a.departmentid in(" + groupid + ") and a.lastday<='" + priMonthStart.AddDays(-1) + "'", theMonthStart, theMonthLast.AddDays(1));
                        var prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and a.lastday<='" + priMonthStart.AddDays(-1) + "' and ( f.auditdate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "')", " a.OldGroupId in(" + groupid + ") and a.TransOutDate<='" + priMonthStart.AddDays(-1) + "' and (a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "')");
                        //dimissionlist.Where(x => x.Status == 12 && (x.AuditDate >= theMonthStart && x.AuditDate < theMonthLast.AddDays(1)) && (x.LastDay < priMonthStart));
                        if (prelastds != null && prelastds.Count() > 0)
                        {
                            foreach (ReportDimissionInfo drLast in prelastds)
                            {
                                DateTime lastdaydt = drLast.LastDay;
                                DateTime lastdaydt2 = new DateTime(lastdaydt.Year, lastdaydt.Month, 1);
                                DateTime lastdaydt3 = lastdaydt2.AddMonths(1).AddDays(-1);

                                // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                if (lastdaydt >= lastdaydt2 && lastdaydt < lastdaydt2.AddDays(10))
                                {
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    TimeSpan sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                }
                                else if (lastdaydt >= lastdaydt2.AddDays(10) && lastdaydt < lastdaydt3.AddDays(1))
                                {
                                    theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (lastdaydt.Day) * deptModel.SalaryAmount;
                                }
                                else
                                {
                                    theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                }
                            }
                        }
                        //shang月离职
                        //DeadLineInfo predeadline2 = DeadLineManager.GetMonthModel(priMonthStart.AddDays(-1).Year, priMonthStart.AddDays(-1).Month);
                        DeadLineInfo predeadline2 = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.AddDays(-1).Year && x.DeadLineMonth == priMonthStart.AddDays(-1).Month).FirstOrDefault();
                        DateTime predeadlinedt2 = priMonthStart.AddDays(-1);
                        if (predeadline2 != null)
                            predeadlinedt2 = predeadline2.SalaryDate.AddDays(-1);
                        predeadlinedt2 = new DateTime(predeadlinedt2.Year, predeadlinedt2.Month, predeadlinedt2.Day, 23, 59, 59);
                        //DataSet prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and 
                        //((a.lastday between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))");
                        var prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and ((a.lastday between '" + priMonthStart + "' and '" + priMonthLast.AddDays(1) + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))", " a.OldGroupId in(" + groupid + ") and ((a.TransOutDate between '" + priMonthStart + "' and '" + priMonthLast.AddDays(1) + "') or ((a.TransOutDate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "')))"
);
                        //dimissionlist.Where(x => ((x.LastDay >= predeadlinedt2 && x.LastDay < priMonthLast.AddDays(1)) || (x.AuditDate >= predeadlinedt2 && x.AuditDate < priMonthLast.AddDays(1))));
                        if (prilastds != null && prilastds.Count() > 0)
                        {
                            //获得上月发薪日
                            DeadLineInfo predeadline = null;
                            DateTime salaryday = priMonthLast;
                            //DeadLineInfo lastdeadline = DeadLineManager.GetMonthModel(priMonthStart.Year, priMonthStart.Month); ;
                            DeadLineInfo lastdeadline = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.Year && x.DeadLineMonth == priMonthStart.Month).FirstOrDefault();
                            salaryday = salaryday.AddDays(-1);

                            DateTime lastsalaryday = priMonthLast;
                            if (lastdeadline != null && lastdeadline.SalaryDate != null)
                            {
                                lastsalaryday = lastdeadline.SalaryDate;
                            }
                            lastsalaryday = lastsalaryday.AddDays(-1);
                            foreach (ReportDimissionInfo drPreLast in prilastds)
                            {
                                DateTime lastday = drPreLast.LastDay;
                                DateTime lastdays = new DateTime(lastday.Year, lastday.Month, 1);
                                lastdays = lastdays.AddMonths(1).AddDays(-1);

                                //predeadline = DeadLineManager.GetMonthModel(lastday.Year, lastday.Month);
                                predeadline = deadlinelist.Where(x => x.DeadLineYear == lastday.Year && x.DeadLineMonth == lastday.Month).FirstOrDefault();
                                if (predeadline != null && predeadline.SalaryDate != null)
                                {
                                    salaryday = predeadline.SalaryDate;
                                }
                                salaryday = salaryday.AddDays(-1);
                                salaryday = new DateTime(salaryday.Year, salaryday.Month, salaryday.Day, 23, 59, 59);
                                //上月工资发放日前办理离职手续的
                                DateTime auditdate = drPreLast.AuditDate;
                                if (drPreLast.Status != 12)
                                {
                                    auditdate = salaryday;
                                }
                                // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                if (lastday >= priMonthStart && lastday < priMonthStart.AddDays(10))
                                {
                                    if (auditdate <= salaryday || drPreLast.Status != 12)
                                    {
                                        theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                        theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (auditdate <= salarydt && drPreLast.Status == 12)
                                    {
                                        theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                        theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }

                                }
                                else if (lastday >= priMonthStart.AddDays(10) && lastday < priMonthLast.AddDays(1))
                                {

                                    if (drPreLast.Status == 12)
                                    {
                                        if (auditdate < lastsalaryday || auditdate <= salaryday)
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                        }
                                        else if (auditdate > salaryday && auditdate < salarydt)
                                        {
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                        }
                                    }
                                    else
                                    {
                                        if (lastday < salaryday)
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                        }
                                        else
                                        {
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                        }
                                    }
                                }
                                else if ((lastday < priMonthStart))
                                {
                                    if (drPreLast.Status == 12)
                                    {
                                        if (auditdate > lastsalaryday)
                                        {
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                        }
                                        else
                                        {
                                            DateTime lastmonth = new DateTime(lastday.Year, lastday.Month, 1);
                                            if (lastday >= lastmonth && lastday < lastmonth.AddDays(10))
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            }
                                            else
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    previousSalary = theSalary;

                    differFeeDate = differFeeDate.AddMonths(1);
                }
            }

            //*****************end****************//

            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string YuGuCell = string.Empty;
                string salayTotalCell = string.Empty;
                salayTotalCell = arrayMonth[i + 1] + salayTotalNum.ToString();
                YuGuCell = arrayMonth[i + 1] + "5";
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, salayTotalCell, "费用小计：");
                }
                else
                {
                    int year = beginTime.AddMonths(i - 1).Year;
                    int month = beginTime.AddMonths(i - 1).Month;
                    IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + groupid + ")" + " AND [Year]=" + year + " AND [Month]=" + month);
                    if (listFee != null && listFee.Count > 0)
                    {
                        DateTime readydate = new DateTime(year, month, 1);
                        allreadyDate = readydate;
                        //实填数工资
                        decimal salary = 0;
                        foreach (ESP.HumanResource.Entity.DeptFeeInfo de in listFee)
                        {
                            salary += de.TotalSalary;
                        }
                        ExcelHandle.WriteCell(excel.CurSheet, salayTotalCell, salary);
                        ExcelHandle.WriteCell(excel.CurSheet, YuGuCell, "实际");
                        previousSalary = salary;
                    }
                    else
                    {
                        //预估情况
                        DateTime notreadydate = new DateTime(year, month, 1);
                        notallreadyDate = notreadydate;
                        decimal theSalary = previousSalary;

                        //本月初的日期
                        DateTime theMonthStart = new DateTime(year, month, 1);
                        //本月末日期
                        DateTime theMonthLast = theMonthStart.AddMonths(1).AddDays(-1);
                        //上月初日期
                        DateTime priMonthStart = theMonthStart.AddMonths(-1);
                        //上月末日期
                        DateTime priMonthLast = theMonthStart.AddDays(-1);

                        //DeadLineInfo deadline = DeadLineManager.GetMonthModel(theMonthStart.Year, theMonthStart.Month);
                        DeadLineInfo deadline = deadlinelist.Where(x => x.DeadLineYear == theMonthStart.Year && x.DeadLineMonth == theMonthStart.Month).FirstOrDefault();
                        DateTime salarydt = theMonthLast;
                        if (deadline != null)
                            salarydt = deadline.SalaryDate.AddDays(-1);
                        salarydt = new DateTime(salarydt.Year, salarydt.Month, salarydt.Day, 23, 59, 59);

                        #region "相差一个月"
                        if (allreadyDate.AddMonths(1) == notallreadyDate) //相差一个月
                        {

                            //本月入职
                            //"b.departmentid in(" + groupid + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')"
                            var joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupid + ") and (a.TransInDate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')");
                            //joinlist.Where(x => x.JoinDate >= theMonthStart.AddDays(-9) && x.JoinDate < theMonthLast.AddDays(-9));
                            if (joinds != null && joinds.Count() > 0)
                            {
                                foreach (ReportJoinInfo drJoin in joinds)
                                {
                                    if (drJoin.JoinDate.Month == theMonthStart.Month)
                                    {
                                        TimeSpan sp = (System.TimeSpan)(theMonthLast - drJoin.JoinDate.AddDays(-1));
                                        theSalary += drJoin.SalaryHigh / theMonthLast.Day * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        TimeSpan sp = (System.TimeSpan)(theMonthStart - drJoin.JoinDate);
                                        theSalary += drJoin.SalaryHigh / theMonthStart.AddDays(-1).Day * (sp.Days) * deptModel.SalaryAmount;
                                        theSalary += drJoin.SalaryHigh * deptModel.SalaryAmount;
                                    }
                                }
                            }
                            //上月入职
                            // DataSet prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                            var prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupid + ") and (a.TransInDate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                            //joinlist.Where(x => x.JoinDate >= priMonthStart.AddDays(-9) && x.JoinDate < priMonthLast.AddDays(-9));
                            if (prijoinds != null && prijoinds.Count() > 0)
                            {
                                foreach (ReportJoinInfo pridrJoin in prijoinds)
                                {
                                    if (pridrJoin.JoinDate.Month == priMonthStart.Month)
                                    {
                                        TimeSpan prisp = (System.TimeSpan)(pridrJoin.JoinDate - priMonthStart);
                                        theSalary += pridrJoin.SalaryHigh / priMonthLast.Day * (prisp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        TimeSpan prisp = (System.TimeSpan)(priMonthStart - pridrJoin.JoinDate);
                                        theSalary -= pridrJoin.SalaryHigh / priMonthStart.AddDays(-1).Day * (prisp.Days) * deptModel.SalaryAmount;
                                    }
                                }
                            }

                            //本月离职
                            // DataSet lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                            var lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')", " a.OldGroupId in(" + groupid + ") and (a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                            //dimissionlist.Where(x => x.LastDay >= theMonthStart && x.LastDay < theMonthLast.AddDays(11));
                            if (lastds != null && lastds.Count() > 0)
                            {
                                foreach (ReportDimissionInfo drLast in lastds)
                                {
                                    TimeSpan sp;
                                    DateTime auditdate = drLast.AuditDate;
                                    if (drLast.Status != 12)
                                    {
                                        auditdate = salarydt;
                                    }
                                    DateTime lastday = drLast.LastDay;

                                    //下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastday >= theMonthStart && lastday < theMonthStart.AddDays(10) && auditdate.Month == lastday.Month)
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        sp = (System.TimeSpan)(lastday.AddDays(1) - theMonthStart);
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastday >= theMonthStart.AddDays(10) && lastday < salarydt)
                                    {
                                        if (auditdate <= salarydt || drLast.Status != 12)
                                            sp = (System.TimeSpan)(theMonthLast - lastday);
                                        else
                                            sp = theMonthLast.AddDays(1) - theMonthStart;
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastday >= salarydt && lastday < theMonthLast.AddDays(11))
                                    {
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    }

                                }
                            }
                            //本月审批结束的离职单
                            //strSql.Append(" Where emp.status not in(6) and a.status=12 and (f.auditdate between '" + monthSalaryDate.ToString("yyyy-MM-dd hh:ss:mm") + "' and '" + monthSalaryDateEnd.ToString("yyyy-MM-dd hh:ss:mm") + "') ");

                            //DataSet prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetPreMonthLastDS("a.departmentid in(" + groupid + ") and a.lastday<='" + priMonthStart + "'", theMonthStart, theMonthLast.AddDays(1));
                            var prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and a.lastday<='" + priMonthStart + "' and ((f.auditdate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "') and a.status =12)", " a.OldGroupId in(" + groupid + ") and a.TransOutDate<='" + priMonthStart + "' and ((a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "'))");
                            //dimissionlist.Where(x => x.Status == 12 && (x.AuditDate >= theMonthStart && x.AuditDate < theMonthLast.AddDays(1)) && x.LastDay <= priMonthStart);
                            if (prelastds != null && prelastds.Count() > 0)
                            {
                                foreach (ReportDimissionInfo drLast in prelastds)
                                {
                                    DateTime lastdaydt = drLast.LastDay;
                                    DateTime lastdaydt2 = new DateTime(lastdaydt.Year, lastdaydt.Month, 1);
                                    DateTime lastdaydt3 = lastdaydt2.AddMonths(1).AddDays(-1);
                                    TimeSpan sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);

                                    // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；

                                    if (lastdaydt >= lastdaydt2 && lastdaydt < lastdaydt2.AddDays(10))
                                    {

                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastdaydt >= lastdaydt2.AddDays(10) && lastdaydt < lastdaydt3.AddDays(1))
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    }


                                }
                            }

                            //上月离职的
                            //                            DeadLineInfo predeadline2 = DeadLineManager.GetMonthModel(priMonthStart.AddDays(-1).Year, priMonthStart.AddDays(-1).Month);
                            DeadLineInfo predeadline2 = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.AddDays(-1).Year && x.DeadLineMonth == priMonthStart.AddDays(-1).Month).FirstOrDefault();
                            DateTime predeadlinedt2 = priMonthStart.AddDays(-1);
                            if (predeadline2 != null)
                                predeadlinedt2 = predeadline2.SalaryDate.AddDays(-1);
                            // DataSet prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and ((a.lastday between '" + priMonthStart + "' and '" + priMonthLast + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))");
                            var prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and ((a.lastday between '" + priMonthStart + "' and '" + priMonthLast + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))", " a.OldGroupId in(" + groupid + ") and ((a.TransOutDate between '" + priMonthStart + "' and '" + priMonthLast + "') or ((a.TransOutDate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "')))");
                            //dimissionlist.Where(x => ((x.LastDay >= priMonthStart && x.LastDay < priMonthLast.AddDays(1)) || (x.AuditDate >= predeadlinedt2 && x.AuditDate < priMonthLast.AddDays(1))));
                            if (prilastds != null && prilastds.Count() > 0)
                            {
                                //获得上月发薪日
                                DeadLineInfo predeadline = null;
                                DateTime salaryday = priMonthLast;

                                //DeadLineInfo lastdeadline = DeadLineManager.GetMonthModel(priMonthStart.Year, priMonthStart.Month); ;
                                DeadLineInfo lastdeadline = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.Year && x.DeadLineMonth == priMonthStart.Month).FirstOrDefault();
                                salaryday = salaryday.AddDays(-1);

                                DateTime lastsalaryday = priMonthLast;
                                if (lastdeadline != null && lastdeadline.SalaryDate != null)
                                {
                                    lastsalaryday = lastdeadline.SalaryDate;
                                }
                                lastsalaryday = lastsalaryday.AddDays(-1);
                                lastsalaryday = new DateTime(lastsalaryday.Year, lastsalaryday.Month, lastsalaryday.Day, 23, 59, 59);
                                foreach (ReportDimissionInfo drPreLast in prilastds)
                                {
                                    DateTime lastday = drPreLast.LastDay;
                                    DateTime lastdays = new DateTime(lastday.Year, lastday.Month, 1);
                                    lastdays = lastdays.AddMonths(1).AddDays(-1);

                                    //predeadline = DeadLineManager.GetMonthModel(lastday.Year, lastday.Month);
                                    predeadline = deadlinelist.Where(x => x.DeadLineYear == lastday.Year && x.DeadLineMonth == lastday.Month).FirstOrDefault();
                                    if (predeadline != null && predeadline.SalaryDate != null)
                                    {
                                        salaryday = predeadline.SalaryDate;
                                    }
                                    salaryday = salaryday.AddDays(-1);
                                    salaryday = new DateTime(salaryday.Year, salaryday.Month, salaryday.Day, 23, 59, 59);

                                    DateTime auditdate = drPreLast.AuditDate;
                                    if (drPreLast.Status != 12)
                                    {
                                        auditdate = salaryday;
                                    }

                                    // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastday >= priMonthStart && lastday < priMonthStart.AddDays(10))
                                    {
                                        if (drPreLast.Status == 12)
                                        {
                                            if (auditdate <= salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                                TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                            }
                                            else if (auditdate <= salarydt)
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                                TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                            }
                                        }
                                        else
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                        }

                                    }
                                    else if (lastday >= priMonthStart.AddDays(10) && lastday < priMonthLast.AddDays(1))
                                    {
                                        //上月工资发放日前办理离职手续的

                                        if (drPreLast.Status == 12)
                                        {
                                            if (auditdate < lastsalaryday || auditdate <= salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                            else if (auditdate > salaryday && auditdate < salarydt)
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                        }
                                        else
                                        {
                                            if (lastday < salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                            else
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                        }



                                    }
                                    else if (lastday >= priMonthLast.AddDays(1) && lastday < priMonthLast.AddDays(11) && auditdate.Month == priMonthLast.AddDays(11).Month)
                                    {
                                        theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                    }
                                    else if (lastday < priMonthStart)
                                    {
                                        if (auditdate > lastsalaryday)
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                        else
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                    }


                                }
                            }
                        }
                        #endregion

                        # region "相差更多"
                        else //相差更多
                        {
                            //本月入职
                            //DataSet joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')");
                            var joinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupid + ") and (a.TransInDate between '" + theMonthStart.AddDays(-9) + "' and '" + theMonthLast.AddDays(-9) + "')");
                            //joinlist.Where(x => x.JoinDate >= theMonthStart.AddDays(-9) && x.JoinDate < theMonthLast.AddDays(-9));
                            if (joinds != null && joinds.Count() > 0)
                            {
                                foreach (ReportJoinInfo drJoin in joinds)
                                {
                                    if (drJoin.JoinDate.Month == theMonthStart.Month)
                                    {
                                        TimeSpan sp = theMonthLast - drJoin.JoinDate.AddDays(-1);
                                        theSalary += drJoin.SalaryHigh / theMonthLast.Day * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        TimeSpan sp = (System.TimeSpan)(theMonthStart - drJoin.JoinDate);
                                        theSalary += drJoin.SalaryHigh / theMonthStart.AddDays(-1).Day * (sp.Days) * deptModel.SalaryAmount;
                                        theSalary += drJoin.SalaryHigh * deptModel.SalaryAmount;
                                    }
                                }
                            }

                            //上月入职
                            //DataSet prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                            var prijoinds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetJoinDS("b.departmentid in(" + groupid + ") and (a.joindate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')", "a.NewGroupId in(" + groupid + ") and (a.TransInDate between '" + priMonthStart.AddDays(-9) + "' and '" + priMonthLast.AddDays(-9) + "')");
                            //joinlist.Where(x => x.JoinDate >= priMonthStart.AddDays(-9) && x.JoinDate < priMonthLast.AddDays(-9));

                            if (prijoinds != null && prijoinds.Count() > 0)
                            {
                                foreach (ReportJoinInfo pridrJoin in prijoinds)
                                {
                                    if (pridrJoin.JoinDate.Month == priMonthStart.Month)
                                    {
                                        TimeSpan prisp = (System.TimeSpan)(pridrJoin.JoinDate - priMonthStart);
                                        theSalary += pridrJoin.SalaryHigh / priMonthLast.Day * (prisp.Days) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        TimeSpan prisp = (System.TimeSpan)(priMonthStart - pridrJoin.JoinDate);
                                        theSalary -= pridrJoin.SalaryHigh / priMonthStart.AddDays(-1).Day * (prisp.Days) * deptModel.SalaryAmount;
                                    }
                                }
                            }

                            //本月离职
                            //DataSet lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                            var lastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and (a.lastday between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')", " a.OldGroupId in(" + groupid + ") and (a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(11) + "')");
                            //dimissionlist.Where(x => x.LastDay >= theMonthStart && x.LastDay < theMonthLast.AddDays(11));
                            if (lastds != null && lastds.Count() > 0)
                            {
                                foreach (ReportDimissionInfo drLast in lastds)
                                {
                                    TimeSpan sp;
                                    DateTime auditdate = drLast.AuditDate;

                                    if (drLast.Status != 12)
                                    {
                                        auditdate = salarydt;
                                    }

                                    DateTime lastday = drLast.LastDay;

                                    //下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastday >= theMonthStart && lastday < theMonthStart.AddDays(10) && auditdate.Month == lastday.Month)
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        sp = (System.TimeSpan)(lastday.AddDays(1) - theMonthStart);
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastday >= theMonthStart.AddDays(10) && lastday < salarydt)
                                    {
                                        if (auditdate <= salarydt || drLast.Status != 12)
                                            sp = theMonthLast - lastday;
                                        else
                                            sp = theMonthLast.AddDays(1) - theMonthStart;

                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / theMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;

                                    }
                                    else if (lastday >= salarydt && lastday < theMonthLast.AddDays(11))
                                    {
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    }
                                }
                            }
                            //本月审批结束的离职单
                            // //strSql.Append(" Where emp.status not in(6) and a.status=12 and (f.auditdate between '" + monthSalaryDate.ToString("yyyy-MM-dd hh:ss:mm") + "' and '" + monthSalaryDateEnd.ToString("yyyy-MM-dd hh:ss:mm") + "') ");
                            //DataSet prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetPreMonthLastDS("a.departmentid in(" + groupid + ") and a.lastday<='" + priMonthStart.AddDays(-1) + "'", theMonthStart, theMonthLast.AddDays(1));
                            var prelastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and a.lastday<='" + priMonthStart.AddDays(-1) + "' and (( f.auditdate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "') and a.status =12)", " a.OldGroupId in(" + groupid + ") and a.TransOutDate<='" + priMonthStart.AddDays(-1) + "' and ((a.TransOutDate between '" + theMonthStart + "' and '" + theMonthLast.AddDays(1) + "'))");
                            //dimissionlist.Where(x => x.Status == 12 && (x.AuditDate >= theMonthStart && x.AuditDate < theMonthLast.AddDays(1)) && (x.LastDay < priMonthStart));
                            if (prelastds != null && prelastds.Count() > 0)
                            {
                                foreach (ReportDimissionInfo drLast in prelastds)
                                {
                                    DateTime lastdaydt = drLast.LastDay;
                                    DateTime lastdaydt2 = new DateTime(lastdaydt.Year, lastdaydt.Month, 1);
                                    DateTime lastdaydt3 = lastdaydt2.AddMonths(1).AddDays(-1);

                                    // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastdaydt >= lastdaydt2 && lastdaydt < lastdaydt2.AddDays(10))
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                        TimeSpan sp = (System.TimeSpan)(lastdaydt.AddDays(1) - lastdaydt2);
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                    }
                                    else if (lastdaydt >= lastdaydt2.AddDays(10) && lastdaydt < lastdaydt3.AddDays(1))
                                    {
                                        theSalary += (drLast.SalaryLow + drLast.SalaryHigh) / lastdaydt3.Day / 2 * (lastdaydt.Day) * deptModel.SalaryAmount;
                                    }
                                    else
                                    {
                                        theSalary -= (drLast.SalaryLow + drLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                    }
                                }
                            }
                            //shang月离职
                            //DeadLineInfo predeadline2 = DeadLineManager.GetMonthModel(priMonthStart.AddDays(-1).Year, priMonthStart.AddDays(-1).Month);
                            DeadLineInfo predeadline2 = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.AddDays(-1).Year && x.DeadLineMonth == priMonthStart.AddDays(-1).Month).FirstOrDefault();
                            DateTime predeadlinedt2 = priMonthStart.AddDays(-1);
                            if (predeadline2 != null)
                                predeadlinedt2 = predeadline2.SalaryDate.AddDays(-1);
                            predeadlinedt2 = new DateTime(predeadlinedt2.Year, predeadlinedt2.Month, predeadlinedt2.Day, 23, 59, 59);
                            //DataSet prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and 
                            //((a.lastday between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))");
                            var prilastds = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetLastDS("a.departmentid in(" + groupid + ") and ((a.lastday between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') or ((f.auditdate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') and a.status =12 ))", " a.OldGroupId in(" + groupid + ") and ((a.TransOutDate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "') or ((a.TransOutDate between '" + predeadlinedt2 + "' and '" + priMonthLast.AddDays(1) + "')))");
                            //dimissionlist.Where(x => ((x.LastDay >= predeadlinedt2 && x.LastDay < priMonthLast.AddDays(1)) || (x.AuditDate >= predeadlinedt2 && x.AuditDate < priMonthLast.AddDays(1))));
                            if (prilastds != null && prilastds.Count() > 0)
                            {
                                //获得上月发薪日
                                DeadLineInfo predeadline = null;
                                DateTime salaryday = priMonthLast;
                                //DeadLineInfo lastdeadline = DeadLineManager.GetMonthModel(priMonthStart.Year, priMonthStart.Month); ;
                                DeadLineInfo lastdeadline = deadlinelist.Where(x => x.DeadLineYear == priMonthStart.Year && x.DeadLineMonth == priMonthStart.Month).FirstOrDefault();
                                salaryday = salaryday.AddDays(-1);

                                DateTime lastsalaryday = priMonthLast;
                                if (lastdeadline != null && lastdeadline.SalaryDate != null)
                                {
                                    lastsalaryday = lastdeadline.SalaryDate;
                                }
                                lastsalaryday = lastsalaryday.AddDays(-1);
                                foreach (ReportDimissionInfo drPreLast in prilastds)
                                {
                                    DateTime lastday = drPreLast.LastDay;
                                    DateTime lastdays = new DateTime(lastday.Year, lastday.Month, 1);
                                    lastdays = lastdays.AddMonths(1).AddDays(-1);

                                    //predeadline = DeadLineManager.GetMonthModel(lastday.Year, lastday.Month);
                                    predeadline = deadlinelist.Where(x => x.DeadLineYear == lastday.Year && x.DeadLineMonth == lastday.Month).FirstOrDefault();
                                    if (predeadline != null && predeadline.SalaryDate != null)
                                    {
                                        salaryday = predeadline.SalaryDate;
                                    }
                                    salaryday = salaryday.AddDays(-1);
                                    salaryday = new DateTime(salaryday.Year, salaryday.Month, salaryday.Day, 23, 59, 59);
                                    //上月工资发放日前办理离职手续的
                                    DateTime auditdate = drPreLast.AuditDate;
                                    if (drPreLast.Status != 12)
                                    {
                                        auditdate = salaryday;
                                    }
                                    // 下月10日（含）前离职员工，当月工资缓发，并入下月工资一起发放；
                                    if (lastday >= priMonthStart && lastday < priMonthStart.AddDays(10))
                                    {
                                        if (auditdate <= salaryday || drPreLast.Status != 12)
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                        }
                                        else if (auditdate <= salarydt && drPreLast.Status == 12)
                                        {
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                            TimeSpan sp = (System.TimeSpan)(lastday.AddDays(1) - priMonthStart);
                                            theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / priMonthLast.Day / 2 * (sp.Days) * deptModel.SalaryAmount;
                                        }

                                    }
                                    else if (lastday >= priMonthStart.AddDays(10) && lastday < priMonthLast.AddDays(1))
                                    {

                                        if (drPreLast.Status == 12)
                                        {
                                            if (auditdate < lastsalaryday || auditdate <= salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                            else if (auditdate > salaryday && auditdate < salarydt)
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                        }
                                        else
                                        {
                                            if (lastday < salaryday)
                                            {
                                                theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                            else
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * drPreLast.LastDay.Day * deptModel.SalaryAmount;
                                            }
                                        }
                                    }
                                    else if ((lastday < priMonthStart))
                                    {
                                        if (drPreLast.Status == 12)
                                        {
                                            if (auditdate > lastsalaryday)
                                            {
                                                theSalary += (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                            }
                                            else
                                            {
                                                DateTime lastmonth = new DateTime(lastday.Year, lastday.Month, 1);
                                                if (lastday >= lastmonth && lastday < lastmonth.AddDays(10))
                                                {
                                                    theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                                    theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / 2 * deptModel.SalaryAmount;
                                                }
                                                else
                                                {
                                                    theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            theSalary -= (drPreLast.SalaryLow + drPreLast.SalaryHigh) / lastdays.Day / 2 * lastday.Day * deptModel.SalaryAmount;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        previousSalary = theSalary;
                        if (hashYearMonth[arrayMonth[i + 1] + "4"] != null && hashYearMonth[arrayMonth[i + 1] + "4"].ToString().IndexOf("13月") > 0)
                            ExcelHandle.WriteCell(excel.CurSheet, salayTotalCell, "0.00");
                        else
                        {
                            ExcelHandle.WriteCell(excel.CurSheet, salayTotalCell, theSalary);
                        }
                        ExcelHandle.WriteCell(excel.CurSheet, YuGuCell, "预计");
                    }
                }
            }
            #endregion

            int pbtRowCount = 0;
            int newRowCount = 0;
            int newChangeCount = 0;
            //工资 总计
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + salayTotalNum, "=ROUND(SUM(" + arrayMonth[2] + salayTotalNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + salayTotalNum.ToString() + "),2)");

            #region 其他收益
            int otherFeeNum = dt.Rows.Count + 11;
            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string otherFeeCell = string.Empty;
                otherFeeCell = arrayMonth[i + 1] + otherFeeNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, otherFeeCell, "其他收益：");
                }
                else
                {
                    int year = beginTime.AddMonths(i - 1).Year;
                    int month = beginTime.AddMonths(i - 1).Month;
                    IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + groupid + ")" + " AND [Year]=" + year + " AND [Month]=" + month);
                    if (listFee != null && listFee.Count > 0)
                    {
                        decimal otherFee = 0;
                        foreach (ESP.HumanResource.Entity.DeptFeeInfo de in listFee)
                        {
                            otherFee += de.OtherFee;
                        }
                        ExcelHandle.WriteCell(excel.CurSheet, otherFeeCell, otherFee);
                    }

                }
            }
            //其他收益 总计
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + otherFeeNum, "=ROUND(SUM(" + arrayMonth[2] + otherFeeNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + otherFeeNum.ToString() + "),2)");

            #endregion

            #region 资金垫付费用
            int advCostTotalNum = dt.Rows.Count + 12;
            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string advCostTotalCell = string.Empty;
                advCostTotalCell = arrayMonth[i + 1] + advCostTotalNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, advCostTotalCell, "资金垫付费用：");
                }
                else
                {
                    int year = beginTime.AddMonths(i - 1).Year;
                    int month = beginTime.AddMonths(i - 1).Month;
                    IList<ESP.HumanResource.Entity.DeptFeeInfo> listFee = ESP.HumanResource.BusinessLogic.DeptFeeManager.GetList(" DeptId in (" + groupid + ")" + " AND [Year]=" + year + " AND [Month]=" + month);
                    if (listFee != null && listFee.Count > 0)
                    {
                        decimal advCost = 0;
                        foreach (ESP.HumanResource.Entity.DeptFeeInfo de in listFee)
                        {
                            advCost += de.AdvancePaid;
                        }
                        ExcelHandle.WriteCell(excel.CurSheet, advCostTotalCell, advCost);
                    }

                }
            }
            //资金垫付费用 总计
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + advCostTotalNum, "=ROUND(SUM(" + arrayMonth[2] + advCostTotalNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + advCostTotalNum.ToString() + "),2)");
            #endregion

            #region PBT
            int pbtNum = dt.Rows.Count + 13;
            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string pbtTotalCell = string.Empty;
                pbtTotalCell = arrayMonth[i + 1] + pbtNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, pbtTotalCell, "PBT：");
                }
                else
                {
                    //ExcelHandle.SetFormula(excel.CurSheet, pbtTotalCell, "=ROUND(" + arrayMonth[i + 1] + feeTotalNum + "-" + arrayMonth[i + 1] + salayTotalNum + "-" + arrayMonth[i + 1] + taxNum + ",2)");
                    ExcelHandle.SetFormula(excel.CurSheet, pbtTotalCell, "=ROUND(" + arrayMonth[i + 1] + feeTotalNum + "+" + arrayMonth[i + 1] + otherFeeNum + "-" + arrayMonth[i + 1] + salayTotalNum + "-" + arrayMonth[i + 1] + advCostTotalNum + ",2)");
                }
                // ExcelHandle.SetBold(excel.CurSheet, pbtTotalCell);
                pbtRowCount = pbtNum;
            }
            //PBT
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum), "=ROUND(SUM(" + arrayMonth[2] + pbtNum.ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + pbtNum.ToString() + "),2)");
            ExcelHandle.SetBold(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum));
            #endregion

            //新增项目服务费情况
            pbtNum = pbtNum + 2;
            ExcelHandle.WriteCell(excel.CurSheet, "A" + pbtNum.ToString(), "新增项目服务费情况：");

            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 1).ToString(), "1");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 2).ToString(), "2");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 3).ToString(), "3");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 4).ToString(), "4");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 5).ToString(), "5");

            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 1).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 1).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 2).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 2).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 3).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 3).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 4).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 4).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 5).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 5).ToString(), System.Drawing.Color.LightBlue);

            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 1), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 1).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 1).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 2), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 2).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 2).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 3), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 3).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 3).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 4), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 4).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 4).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 5), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 5).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 5).ToString() + "),2)");

            pbtNum = pbtNum + 6;
            for (int i = 0; i <= hashYearMonth.Count + 1; i++)
            {
                string pbtTotalCell = string.Empty;
                pbtTotalCell = arrayMonth[i + 1] + pbtNum;
                newRowCount = pbtNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, pbtTotalCell, "新增项目服务费小计：");
                }
                else
                {
                    ExcelHandle.SetFormula(excel.CurSheet, pbtTotalCell, "=ROUND(sum(" + arrayMonth[i + 1] + (pbtNum - 5) + ":" + arrayMonth[i + 1] + (pbtNum - 1) + "),2)");
                }
                ExcelHandle.SetBold(excel.CurSheet, pbtTotalCell);

            }
            //员工变化
            pbtNum = pbtNum + 2;
            ExcelHandle.WriteCell(excel.CurSheet, "A" + pbtNum.ToString(), "员工变化：");

            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 1).ToString(), "1");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 2).ToString(), "2");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 3).ToString(), "3");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 4).ToString(), "4");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 5).ToString(), "5");

            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 1).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 1).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 2).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 2).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 3).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 3).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 4).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 4).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (pbtNum + 5).ToString(), arrayMonth[arrayMonth.Length - 1] + (pbtNum + 5).ToString(), System.Drawing.Color.LightBlue);

            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 1), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 1).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 1).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 2), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 2).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 2).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 3), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 3).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 3).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 4), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 4).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 4).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 5), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 5).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 5).ToString() + "),2)");

            pbtNum = pbtNum + 6;

            //费用变化：
            ExcelHandle.WriteCell(excel.CurSheet, "A" + pbtNum.ToString(), "费用变化：");

            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 1).ToString(), "1");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 2).ToString(), "2");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 3).ToString(), "3");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 4).ToString(), "4");
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (pbtNum + 5).ToString(), "5");

            for (int i = 1; i <= hashYearMonth.Count; i++)
            {
                ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[i + 1] + (pbtNum + 1), "=" + arrayMonth[i + 1] + (pbtNum + 1 - 6) + "*" + deptModel.SalaryAmount.ToString());
                ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[i + 1] + (pbtNum + 2), "=" + arrayMonth[i + 1] + (pbtNum + 2 - 6) + "*" + deptModel.SalaryAmount.ToString());
                ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[i + 1] + (pbtNum + 3), "=" + arrayMonth[i + 1] + (pbtNum + 3 - 6) + "*" + deptModel.SalaryAmount.ToString());
                ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[i + 1] + (pbtNum + 4), "=" + arrayMonth[i + 1] + (pbtNum + 4 - 6) + "*" + deptModel.SalaryAmount.ToString());
                ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[i + 1] + (pbtNum + 5), "=" + arrayMonth[i + 1] + (pbtNum + 5 - 6) + "*" + deptModel.SalaryAmount.ToString());

            }
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 1), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 1).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 1).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 2), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 2).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 2).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 3), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 3).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 3).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 4), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 4).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 4).ToString() + "),2)");
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 5), "=ROUND(SUM(" + arrayMonth[2] + (pbtNum + 5).ToString() + ":" + arrayMonth[hashYearMonth.Count + 1] + (pbtNum + 5).ToString() + "),2)");


            pbtNum = pbtNum + 6;
            for (int i = 0; i <= hashYearMonth.Count + 1; i++)
            {
                string pbtTotalCell = string.Empty;
                pbtTotalCell = arrayMonth[i + 1] + pbtNum;
                newChangeCount = pbtNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, pbtTotalCell, "新增费用小计：");
                }
                else
                {
                    ExcelHandle.SetFormula(excel.CurSheet, pbtTotalCell, "=ROUND(sum(" + arrayMonth[i + 1] + (pbtNum - 5) + ":" + arrayMonth[i + 1] + (pbtNum - 1) + "),2)");
                }
                ExcelHandle.SetBold(excel.CurSheet, pbtTotalCell);
            }

            //调整后PBT
            pbtNum += 2;
            for (int i = 0; i <= hashYearMonth.Count; i++)
            {
                string pbtTotalCell = string.Empty;
                pbtTotalCell = arrayMonth[i + 1] + pbtNum;
                if (i == 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, pbtTotalCell, "调整后PBT：");
                }
                else
                {
                    ExcelHandle.SetFormula(excel.CurSheet, pbtTotalCell, "=" + arrayMonth[i + 1] + pbtRowCount.ToString() + "+" + arrayMonth[i + 1] + newRowCount.ToString() + "-" + arrayMonth[i + 1] + newChangeCount.ToString());
                }
                ExcelHandle.SetBold(excel.CurSheet, pbtTotalCell);
            }

            //合计
            ExcelHandle.SetBold(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + pbtNum);
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + pbtNum, "=ROUND(SUM(" + arrayMonth[2] + pbtNum + ":" + arrayMonth[hashYearMonth.Count + 1] + pbtNum + "),2)");
            //系统自动带出
            //ExcelHandle.SetBold(excel.CurSheet, arrayMonth[hashYearMonth.Count + 3] + pbtNum);
            //ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[hashYearMonth.Count + 3] + pbtNum, deptTarget.ToString());

            ExcelHandle.SetBold(excel.CurSheet, "B" + (pbtNum + 1));
            ExcelHandle.WriteCell(excel.CurSheet, "B" + (pbtNum + 1), "Target");
            ExcelHandle.SetBold(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 1));
            ExcelHandle.WriteCell(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 1), deptTarget.ToString());
            ExcelHandle.SetBackGroundColor(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 1), arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 1), System.Drawing.Color.Yellow);

            //=P78-Q78
            //ExcelHandle.SetBold(excel.CurSheet, arrayMonth[hashYearMonth.Count + 4] + pbtNum);
            //ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 4] + pbtNum, "=" + arrayMonth[hashYearMonth.Count + 2] + pbtNum + "-" + arrayMonth[hashYearMonth.Count + 3] + pbtNum);
            ExcelHandle.SetBold(excel.CurSheet, "B" + (pbtNum + 2));
            ExcelHandle.WriteCell(excel.CurSheet, "B" + (pbtNum + 2), "Difference");
            ExcelHandle.SetBold(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 2));
            ExcelHandle.SetFormula(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 2), "=" + arrayMonth[hashYearMonth.Count + 2] + pbtNum + "-" + arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 1));
            ExcelHandle.SetBackGroundColor(excel.CurSheet, arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 2), arrayMonth[hashYearMonth.Count + 2] + (pbtNum + 2), System.Drawing.Color.Yellow);


            WriteLastEmployees(beginTime, endTime, groupid.ToString(), excel, userid);


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

        private static decimal GetUsedCost(ESP.Finance.Entity.ProjectInfo projectModel)
        {
            decimal UsedCost;
            IList<ESP.Purchase.Entity.GeneralInfo> PRList;
            IList<ReturnInfo> ReturnList;
            IList<ExpenseAccountDetailInfo> ExpenseDetails;
            Dictionary<int, int> TypeMappings;
            IList<ESP.Purchase.Entity.PaymentPeriodInfo> Periods;
            IList<ESP.Purchase.Entity.MediaPREditHisInfo> MediaPREditHises;
            List<ESP.Purchase.Entity.OrderInfo> Orders;
            List<CostRecordInfo> ExpenseRecords;
            List<CostRecordInfo> PRRecords;

            Dictionary<int, decimal> CostMappings = new Dictionary<int, decimal>();
            Dictionary<int, decimal> ExpenseMappings = new Dictionary<int, decimal>();
            decimal TraficFee;

            if (projectModel == null || projectModel.GroupID == null || projectModel.GroupID.Value == 0)
                return 0;

            var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
            typelvl2[0] = "OOP";
            typelvl2[-1] = "[未知]";

            projectModel.CostDetails = ContractCostManager.GetListByProject(projectModel.ProjectId, null, null);
            projectModel.Expenses = ProjectExpenseManager.GetListByProject(projectModel.ProjectId);

            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(projectModel.ProjectId, projectModel.GroupID.Value);
            ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(projectModel.ProjectId, projectModel.GroupID.Value);
            ExpenseDetails = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(ReturnList.Select(x => x.ReturnID).ToArray());

            TypeMappings = ESP.Purchase.BusinessLogic.TypeManager.GetTypeMappings(/*projectModel.CostDetails.Select(x => x.CostTypeID ?? 0).ToArray()*/);
            Periods = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetList(PRList.Select(x => x.id).ToArray());
            MediaPREditHises = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRIDs(PRList.Where(x => x.PRType == 1 || x.PRType == 6).Select(x => x.id).ToArray());
            Orders = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralIds(PRList.Select(x => x.id).ToArray());

            ExpenseRecords = new List<CostRecordInfo>();
            PRRecords = new List<CostRecordInfo>();


            foreach (var pr in PRList)
            {
                if (pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                    continue;

                decimal paid = 0;
                var orders = Orders.Where(x => x.general_id == pr.id);

                var relationModel = MediaPREditHises.Where(x => x.OldPRId == pr.id).FirstOrDefault();
                if (relationModel != null)
                {
                    var costTypeId = orders.Select(x => x.producttype).FirstOrDefault();
                    if (!TypeMappings.TryGetValue(costTypeId, out costTypeId)) costTypeId = 0;

                    var r = ReturnList.Where(x => x.ReturnID == relationModel.NewPNId).FirstOrDefault();
                    if (r != null)
                    {
                        AddValue(CostMappings, costTypeId, r.PreFee ?? 0);
                        paid += r.FactFee ?? 0;
                    }
                    var newpr = PRList.Where(x => x.id == relationModel.NewPRId).FirstOrDefault();
                    if (newpr != null)
                    {
                        AddValue(CostMappings, costTypeId, newpr.totalprice);
                        var pnofnewpr = ReturnList.Where(x => x.PRID == newpr.id).FirstOrDefault();
                        if (pnofnewpr != null)
                            paid += pnofnewpr.FactFee ?? 0;
                    }
                }
                else
                {
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
                }

                if (pr.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)
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

            foreach (var record in PRRecords)
            {
                decimal v = 0M;
                CostMappings.TryGetValue(record.TypeID, out v);
                record.TypeTotalAmount = v;
            }

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

            TraficFee = ReturnList.Where(x => x.ReturnType == 20).Sum(x => (x.FactFee ?? (x.PreFee ?? 0)));

            UsedCost = TraficFee + CostMappings.Sum(x => x.Value) + ExpenseMappings.Sum(x => x.Value);

            return UsedCost;
        }


        public static string ExportCostView(DataTable dt, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/Salary/CostViewTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            int rownum = 1;
            string cell = "A1";
            int groupBeginCell = 0;

            string deptName = string.Empty;

            if (dt != null && dt.Rows.Count > 0)
            {
                deptName = dt.Rows[0]["deptname"].ToString();
                ExcelHandle.WriteCell(excel.CurSheet, cell, "资金垫付报告---" + deptName);
                ExcelHandle.WriteCell(excel.CurSheet, "A2", DateTime.Now.ToString("yyyy-MM-dd"));
                rownum = rownum + 3;
                groupBeginCell = rownum;
            }
            #region 数据写入
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (deptName != dt.Rows[i]["deptname"].ToString())
                {
                    cell = string.Format("A{0}", rownum);

                    ExcelHandle.WriteCell(excel.CurSheet, cell, "合计");

                    string endCell = string.Empty;

                    //已付金额
                    cell = string.Format("K{0}", rownum);
                    endCell = string.Format("K{0}", rownum - 1);
                    ExcelHandle.SetFormula(excel.CurSheet, cell, "=SUBTOTAL(9,K" + groupBeginCell + ":" + endCell + ")");

                    //回款金额
                    cell = string.Format("L{0}", rownum);
                    endCell = string.Format("L{0}", rownum - 1);
                    ExcelHandle.SetFormula(excel.CurSheet, cell, "=SUBTOTAL(9,L" + groupBeginCell + ":" + endCell + ")");

                    //汇票回款金额
                    cell = string.Format("N{0}", rownum);
                    endCell = string.Format("N{0}", rownum - 1);
                    ExcelHandle.SetFormula(excel.CurSheet, cell, "=K" + rownum + "-L" + rownum);

                    //ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + rownum, "O" + rownum, System.Drawing.Color.Orange);

                    rownum++;

                    deptName = dt.Rows[i]["deptname"].ToString();
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "资金垫付报告---" + deptName);
                    rownum++;

                    groupBeginCell = rownum;

                }
                //项目号	项目名称	负责人	成本所属组	项目总额	成本总额	已使用成本	成本结余	毛利率	服务费
                ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(dt.Rows[i]["projectid"].ToString()));

                decimal taxfee = 0;
                decimal totalNoVAT = 0;

                ESP.Finance.Entity.TaxRateInfo rateModel = null;
                if (project.ContractTaxID != null && project.ContractTaxID != 0)
                {
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);

                    if (project.IsCalculateByVAT == 1)
                    {
                        totalNoVAT = Convert.ToDecimal(project.TotalAmount / rateModel.VATParam1);
                        taxfee = CheckerManager.GetTaxByVAT(project, rateModel);
                    }
                    else
                    {
                        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(project, rateModel);
                    }
                }

                decimal serviceFee = 0;
                if (project.IsCalculateByVAT == 1)
                    serviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel);
                else
                    serviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rateModel);

                var ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(project.ProjectId, project.GroupID.Value);

                decimal prPaid = ReturnList.Where(x => x.PRID > 0 && x.ReturnType != 11 && x.ReturnType != 34 && x.ReturnStatus == 140).Sum(x => x.FactFee.Value == 0 ? x.PreFee.Value : x.FactFee.Value);

                //    decimal oopPaid = ReturnList.Where(x => (x.ReturnType == 30
                //|| x.ReturnType == 32
                //|| x.ReturnType == 31
                //|| x.ReturnType == 33
                //|| x.ReturnType == 40
                //|| x.ReturnType == 35) && x.ReturnStatus == 140).Sum(x => x.PreFee ?? 0);


                decimal oopPaid = ReturnList.Where(x => ((x.ReturnType == 30
              || x.ReturnType == 31
              || x.ReturnType == 37
              || x.ReturnType == 33
              || x.ReturnType == 40
              || x.ReturnType == 35) && x.ReturnStatus == 140)
              || (x.ReturnType == 36 && x.ReturnStatus == 139)).Sum(x => x.PreFee ?? 0);


                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["projectcode"].ToString());

                cell = string.Format("B{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["businessdescription"].ToString());

                cell = string.Format("C{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["applicantemployeename"].ToString());

                cell = string.Format("D{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["deptname"].ToString());

                decimal totalamount = 0;
                if (dt.Rows[i]["totalamount"] != System.DBNull.Value)
                    totalamount = decimal.Parse(dt.Rows[i]["totalamount"].ToString());

                decimal supporter = 0;
                if (dt.Rows[i]["supporter"] != System.DBNull.Value)
                    supporter = decimal.Parse(dt.Rows[i]["supporter"].ToString());
                decimal cost = 0;
                if (dt.Rows[i]["cost"] != System.DBNull.Value)
                    cost = decimal.Parse(dt.Rows[i]["cost"].ToString());

                decimal oop = 0;
                if (dt.Rows[i]["oop"] != System.DBNull.Value)
                    oop = decimal.Parse(dt.Rows[i]["oop"].ToString());


                decimal CostTotal = taxfee + supporter + cost + oop;
                decimal CostUsed = GetUsedCost(project);

                decimal paymentFee = decimal.Parse(dt.Rows[i]["payment"].ToString());
                decimal billPayment = decimal.Parse(dt.Rows[i]["billPayment"].ToString());

                cell = string.Format("E{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, totalamount.ToString("f2"));

                cell = string.Format("F{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, CostTotal.ToString("f2"));

                cell = string.Format("G{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, CostUsed.ToString("f2"));

                cell = string.Format("H{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, (CostTotal - CostUsed).ToString("f2"));

                cell = string.Format("I{0}", rownum);
                if (project.TotalAmount != 0)
                    ExcelHandle.WriteCell(excel.CurSheet, cell, (serviceFee / Convert.ToDecimal(project.TotalAmount) * 100).ToString("f2") + "%");
                else
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "0.00%");
                cell = string.Format("J{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, serviceFee.ToString("f2"));

                cell = string.Format("K{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, (prPaid + oopPaid).ToString("f2"));

                cell = string.Format("L{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, paymentFee.ToString("f2"));

                cell = string.Format("M{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, billPayment.ToString("f2"));

                cell = string.Format("N{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "=K" + rownum + "-L" + rownum);


                cell = string.Format("O{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, project.BeginDate.Value.ToString("yyyy-MM-dd"));
                cell = string.Format("P{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, project.EndDate.Value.ToString("yyyy-MM-dd"));

                rownum++;

                if (i == dt.Rows.Count - 1)
                {
                    cell = string.Format("A{0}", rownum);

                    ExcelHandle.WriteCell(excel.CurSheet, cell, "合计");

                    string endCell = string.Empty;

                    //已付金额
                    cell = string.Format("K{0}", rownum);
                    endCell = string.Format("K{0}", rownum - 1);
                    ExcelHandle.SetFormula(excel.CurSheet, cell, "=SUBTOTAL(9,K" + groupBeginCell + ":" + endCell + ")");

                    //回款金额
                    cell = string.Format("L{0}", rownum);
                    endCell = string.Format("L{0}", rownum - 1);
                    ExcelHandle.SetFormula(excel.CurSheet, cell, "=SUBTOTAL(9,L" + groupBeginCell + ":" + endCell + ")");

                    //汇票回款金额
                    cell = string.Format("N{0}", rownum);
                    endCell = string.Format("N{0}", rownum - 1);
                    ExcelHandle.SetFormula(excel.CurSheet, cell, "=K" + rownum + "-L" + rownum);

                }
            }
            #endregion



            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;

        }

        public static int ExportCostViewSaving(int userid, DataTable dt)
        {
            int icount = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(dt.Rows[i]["projectid"].ToString()));

                decimal taxfee = 0;
                decimal totalNoVAT = 0;

                ESP.Finance.Entity.TaxRateInfo rateModel = null;
                if (project.ContractTaxID != null && project.ContractTaxID != 0)
                {
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);

                    if (project.IsCalculateByVAT == 1)
                    {
                        totalNoVAT = Convert.ToDecimal(project.TotalAmount / rateModel.VATParam1);
                        taxfee = CheckerManager.GetTaxByVAT(project, rateModel);
                    }
                    else
                    {
                        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(project, rateModel);
                    }
                }

                decimal serviceFee = 0;
                if (project.IsCalculateByVAT == 1)
                    serviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel);
                else
                    serviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rateModel);

                var ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(project.ProjectId, project.GroupID.Value);

                //decimal prPaid = ReturnList.Where(x => x.PRID > 0 && x.ReturnType != 11 && x.ReturnStatus == 140).Sum(x => x.PreFee ?? 0);
                decimal prPaid = ReturnList.Where(x => x.PRID > 0 && x.ReturnType != 11 && x.ReturnType != 34 && x.ReturnStatus == 140).Sum(x => x.FactFee.Value == 0 ? x.PreFee.Value : x.FactFee.Value);

                decimal oopPaid = ReturnList.Where(x => ((x.ReturnType == 30
|| x.ReturnType == 31
|| x.ReturnType == 37
|| x.ReturnType == 33
|| x.ReturnType == 40
|| x.ReturnType == 35) && x.ReturnStatus == 140)
|| (x.ReturnType == 36 && x.ReturnStatus == 139)).Sum(x => x.PreFee ?? 0);

                decimal totalamount = 0;
                if (dt.Rows[i]["totalamount"] != System.DBNull.Value)
                    totalamount = decimal.Parse(dt.Rows[i]["totalamount"].ToString());

                decimal supporter = 0;
                if (dt.Rows[i]["supporter"] != System.DBNull.Value)
                    supporter = decimal.Parse(dt.Rows[i]["supporter"].ToString());

                decimal cost = 0;
                if (dt.Rows[i]["cost"] != System.DBNull.Value)
                    cost = decimal.Parse(dt.Rows[i]["cost"].ToString());

                decimal oop = 0;
                if (dt.Rows[i]["oop"] != System.DBNull.Value)
                    oop = decimal.Parse(dt.Rows[i]["oop"].ToString());


                decimal CostTotal = taxfee + supporter + cost + oop;
                decimal CostUsed = GetUsedCost(project);

                decimal paymentFee = decimal.Parse(dt.Rows[i]["payment"].ToString());
                decimal billPayment = decimal.Parse(dt.Rows[i]["billPayment"].ToString());

                CostReportSavingInfo model = new CostReportSavingInfo();
                model.Creator = userid;
                model.CreatTime = DateTime.Now;
                model.ProjectCode = project.ProjectCode;
                model.ProjectContent = project.BusinessDescription;
                model.GroupName = project.GroupName;
                model.ApplicantName = project.ApplicantEmployeeName;
                model.TotalAmount = project.TotalAmount.Value;
                model.TotalCost = CostTotal;
                model.CostUsed = CostUsed;
                model.CostBalance = (CostTotal - CostUsed);
                model.CostPercent = (serviceFee / Convert.ToDecimal(project.TotalAmount) * 100);
                model.ServiceFee = serviceFee;
                model.TotalPaid = (prPaid + oopPaid);
                model.PaymentCash = paymentFee;
                model.PaymentBill = billPayment;
                model.BeginDate = project.BeginDate.Value;
                model.EndDate = project.EndDate.Value;

                if (CostReportSavingManager.Add(model) > 0)
                    icount++;

            }

            return icount;
        }


        public static int PRProcessSync()
        {
            int retvalue = 0;
            //已经完成收货的PR单
            var prlist = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" status =11", new List<SqlParameter>());
            foreach (ESP.Purchase.Entity.GeneralInfo pr in prlist)
            {
                //pr单内的付款帐期
                var paymentlist = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + pr.id.ToString());
                //PR单内的已经完成付款的付款申请
                var returnlist = GetList(" returnstatus=140 and prid=" + pr.id.ToString());
                if (paymentlist.Count == returnlist.Count)
                {
                    pr.status = 17;//状态改为已经付款
                    ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(pr);
                    retvalue++;
                }
            }
            return retvalue;
        }


        public static int AddOOPLog()
        {
            var batchList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(" PaymentUserID =13414 and Status in(136,140) and BatchType =2", null);
            int ret = 0;
            foreach (var batch in batchList)
            {
                var detailList = PNBatchRelationManager.GetList(" batchid = " + batch.BatchID, null);
                foreach (var detail in detailList)
                {
                    var loglist = ExpenseAuditDetailManager.GetList(" and ExpenseAuditID =" + detail.ReturnID + " and AuditorUserID=13414");
                    if (loglist == null || loglist.Count == 0)
                    {
                        ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                        logModel.AuditeDate = batch.Lastupdatetime;
                        logModel.AuditorEmployeeName = "李彦娥";
                        logModel.AuditorUserID = 13414;
                        logModel.AuditorUserCode = "00045";
                        logModel.AuditorUserName = "yane.li";
                        logModel.AuditType = 13;
                        logModel.ExpenseAuditID = detail.ReturnID;
                        logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                        logModel.Suggestion = "";

                        ExpenseAuditDetailManager.Add(logModel);

                        ret++;
                    }
                }
            }

            return ret;
        }


        public static string ExportDeptSalayForFinance(int batchid, System.Web.HttpResponse response)
        {

            string temppath = "/Tmp/Salary/TicketTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            int rownum = 5;
            string cell = "A5";
            var returnList = ESP.Finance.BusinessLogic.ReturnManager.GetTicketBatch(batchid);
            var batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);

            int rowNo = 0;
            foreach (ReturnInfo model in returnList)
            {
                IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> details = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTicketDetail(model.ReturnID);
                foreach (ExpenseAccountDetailInfo detail in details)
                {
                    if (detail.TicketStatus == 1)
                        continue;
                    rowNo++;
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, rowNo.ToString());

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReturnCode);

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ProjectCode);

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, detail.ExpenseDate.Value.ToString("yyyy-MM-dd"));

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestEmployeeName);

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, detail.Boarder + " " + detail.TicketSource + "-" + detail.TicketDestination);

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, detail.ExpenseMoney.Value.ToString("#,##0.00"));

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);

                    rownum++;
                }
            }

            cell = string.Format("A{0}", rownum);
            ExcelHandle.SetBold(excel.CurSheet, cell);
            ExcelHandle.SetBorderAll(excel.CurSheet, cell, "I" + rownum.ToString());
            ExcelHandle.SetHAlignRight(excel.CurSheet, cell);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, cell, "I" + rownum.ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, "I" + rownum.ToString(), batchModel.Amounts.Value.ToString("#,##0.00"));

            rownum++;
            cell = string.Format("A{0}", rownum);
            ExcelHandle.SetBold(excel.CurSheet, cell);
            ExcelHandle.SetBorderAll(excel.CurSheet, cell, "I" + rownum.ToString());
            ExcelHandle.SetHAlignRight(excel.CurSheet, cell);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, cell, "I" + rownum.ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, "I" + rownum.ToString(), batchModel.SupplierName);
            rownum++;
            cell = string.Format("A{0}", rownum);
            ExcelHandle.SetBold(excel.CurSheet, cell);
            ExcelHandle.SetBorderAll(excel.CurSheet, cell, "I" + rownum.ToString());
            ExcelHandle.SetHAlignRight(excel.CurSheet, cell);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, cell, "I" + rownum.ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, "I" + rownum.ToString(), batchModel.SupplierBankName);

            rownum++;
            cell = string.Format("A{0}", rownum);
            ExcelHandle.SetBold(excel.CurSheet, cell);
            ExcelHandle.SetBorderAll(excel.CurSheet, cell, "I" + rownum.ToString());
            ExcelHandle.SetHAlignRight(excel.CurSheet, cell);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, cell, "I" + rownum.ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, "I" + rownum.ToString(), "'" + batchModel.SupplierBankAccount);


            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        public static string ExportDeptSalayForHR(int batchid, System.Web.HttpResponse response)
        {

            string temppath = "/Tmp/Salary/TicketForHrTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            excel.Load(sourceFile);

            int rownum = 5;
            string cell = "A5";
            var returnList = ESP.Finance.BusinessLogic.ReturnManager.GetTicketBatch(batchid);
            var batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            int rowNo = 0;
            foreach (ReturnInfo model in returnList)
            {
                IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> details = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTicketDetail(model.ReturnID);
                foreach (ExpenseAccountDetailInfo detail in details)
                {
                    if (detail.TicketStatus == 1)
                        continue;
                    rowNo++;

                    //-----------------------2------------------------------------------------------------------------
                    excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[2];

                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, rowNo.ToString());

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ReturnCode);

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.ProjectCode);

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, detail.ExpenseDate.Value.ToString("yyyy-MM-dd"));

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, model.RequestEmployeeName);

                    //new add
                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, detail.Boarder);

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, detail.TicketSource + "-" + detail.TicketDestination);

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, detail.ExpenseMoney.Value.ToString("#,##0.00"));

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, detail.ExpenseDesc);

                    cell = string.Format("J{0}", rownum);
                    ExcelHandle.SetBorderAll(excel.CurSheet, cell, cell);


                    rownum++;
                }
            }

            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);

            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;

        }

        public static DataTable GetFactoringList(string term)
        {
            return DataProvider.GetFactoringList(term);
        }
    }

    public class RetrunInfoCompareCode : IComparer<ReturnInfo>
    {
        public int Compare(ReturnInfo x, ReturnInfo y)
        {
            return y.ReturnCode.CompareTo(x.ReturnCode);
        }

    }

}