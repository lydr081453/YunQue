using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using Excel = Microsoft.Office.Interop.Excel;
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


    public static class WorkFlowManager
    {

        private static ESP.Finance.IDataAccess.IWorkFlowDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IWorkFlowDataProvider>.Instance; } }

        #region IAuditHistoryProvider 成员
        public static int Add(ESP.Finance.Entity.WorkFlowInfo model, SqlTransaction trans)
        {
            int res = DataProvider.Add(model, trans);
            if (model.AuditStatus != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)
            {
                Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                log.AuditDate = model.AuditDate;
                log.AuditorEmployeeName = model.AuditorEmployeeName;
                log.AuditorSysID = model.AuditorUserID;
                log.AuditorUserCode = model.AuditorUserCode;
                log.AuditorUserName = model.AuditorUserName;
                log.AuditStatus = model.AuditStatus;
                log.FormID = model.ModelId;
                log.FormType = model.ModelType;
                log.Suggestion = model.Suggestion;
                new DataAccess.AuditLogDataProvider().Add(log, trans);
            }
            ESP.Purchase.Entity.DataInfo dataInfo = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel(model.ModelType, model.ModelId, trans);
            List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
            ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
            p.UserId = model.AuditorUserID;
            p.IsEditor = true;
            p.IsViewer = true;
            p.DataInfoId = dataInfo.Id;
            permissionList.Add(p);
            //增加新的授权
            ESP.Purchase.BusinessLogic.DataPermissionManager.AppendDataPermission(dataInfo, permissionList, trans);
            return res;
        }

        public static int Add(ESP.Finance.Entity.WorkFlowInfo model)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int res = DataProvider.Add(model, trans);
                    if (model.AuditStatus != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)
                    {
                        Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                        log.AuditDate = model.AuditDate;
                        log.AuditorEmployeeName = model.AuditorEmployeeName;
                        log.AuditorSysID = model.AuditorUserID;
                        log.AuditorUserCode = model.AuditorUserCode;
                        log.AuditorUserName = model.AuditorUserName;
                        log.AuditStatus = model.AuditStatus;
                        log.FormID = model.ModelId;
                        log.FormType = (int)Utility.FormType.Refund;
                        log.Suggestion = model.Suggestion;
                        new DataAccess.AuditLogDataProvider().Add(log, trans);
                    }
                    ESP.Purchase.Entity.DataInfo dataInfo = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel(model.ModelType, model.ModelId, trans);
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                    ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                    p.UserId = model.AuditorUserID;
                    p.IsEditor = true;
                    p.IsViewer = true;
                    p.DataInfoId = dataInfo.Id;
                    permissionList.Add(p);
                    //增加新的授权
                    ESP.Purchase.BusinessLogic.DataPermissionManager.AppendDataPermission(dataInfo, permissionList, trans);
                    trans.Commit();
                    return res;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <returns>成功执行的条数</returns>
        public static int CommitRefund(ESP.Finance.Entity.RefundInfo refundModel, List<ESP.Finance.Entity.WorkFlowInfo> models)
        {
            if (models == null || models.Count == 0) return 0;
            int counter = 0;
            IList<ESP.Finance.Entity.WorkFlowInfo> oldList = ESP.Finance.BusinessLogic.WorkFlowManager.GetList(" ModelId=" + refundModel.Id.ToString(), null);

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    refundModel.Status = (int)ESP.Finance.Utility.PaymentStatus.Submit;

                    ESP.Finance.BusinessLogic.RefundManager.Update(refundModel, trans);
                    new DataAccess.WorkFlowDataProvider().DeleteByModelId(refundModel.Id, (int)Utility.FormType.Refund, "", trans, null);
                    ESP.Purchase.Entity.DataInfo dataInfo = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)Utility.FormType.Refund, refundModel.Id, trans);
                    foreach (ESP.Finance.Entity.WorkFlowInfo audit in oldList)
                    {
                        new ESP.Purchase.DataAccess.DataPermissionProvider().DeletePermissionByUserID(dataInfo.Id, audit.AuditorUserID, trans);
                    }

                    foreach (Entity.WorkFlowInfo model in models)
                    {
                        int res = Add(model, trans);
                        if (res > 0)
                        {
                            counter++;
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
            return counter;
        }

        public static int Update(ESP.Finance.Entity.WorkFlowInfo model, SqlTransaction trans)
        {
            int res = DataProvider.Update(model, trans);
            if (res > 0 && model.AuditStatus != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)//如果更新成功,添加到审批日志中
            {
                Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                log.AuditDate = model.AuditDate;
                log.AuditorEmployeeName = model.AuditorEmployeeName;
                log.AuditorSysID = model.AuditorUserID;
                log.AuditorUserCode = model.AuditorUserCode;
                log.AuditorUserName = model.AuditorUserName;
                log.AuditStatus = model.AuditStatus;
                log.FormID = model.ModelId;
                log.FormType = model.ModelType;
                log.Suggestion = model.Suggestion;
                new DataAccess.AuditLogDataProvider().Add(log, trans);
            }
            return res;
        }

        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.WorkFlowInfo model)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    res = DataProvider.Update(model, trans);
                    if (res > 0 && model.AuditStatus != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)//如果更新成功,添加到审批日志中
                    {
                        Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                        log.AuditDate = model.AuditDate;
                        log.AuditorEmployeeName = model.AuditorEmployeeName;
                        log.AuditorSysID = model.AuditorUserID;
                        log.AuditorUserCode = model.AuditorUserCode;
                        log.AuditorUserName = model.AuditorUserName;
                        log.AuditStatus = model.AuditStatus;
                        log.FormID = model.ModelId;
                        log.FormType = model.ModelType;
                        log.Suggestion = model.Suggestion;
                        new DataAccess.AuditLogDataProvider().Add(log, trans);
                        trans.Commit();
                        return ESP.Finance.Utility.UpdateResult.Succeed;
                    }
                    else if (res == 0)
                    {
                        trans.Rollback();
                        return ESP.Finance.Utility.UpdateResult.UnExecute;
                    }
                    else
                    {
                        trans.Rollback();
                        return ESP.Finance.Utility.UpdateResult.Failed;
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Console.WriteLine(ex.Message);
                    return ESP.Finance.Utility.UpdateResult.Failed;
                }
            }
        }

        public static ESP.Finance.Utility.DeleteResult Delete(int AuditId)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(AuditId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.DeleteResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.DeleteResult.UnExecute;
            }
            return ESP.Finance.Utility.DeleteResult.Failed;
        }

        public static ESP.Finance.Utility.DeleteResult DeleteByParameters(string term, List<System.Data.SqlClient.SqlParameter> parms)
        {
            int res = 0;
            try
            {
                res = DataProvider.DeleteByParameters(term, parms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.DeleteResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.DeleteResult.UnExecute;
            }
            return ESP.Finance.Utility.DeleteResult.Failed;
        }


        /// <summary>
        /// 根据ModelId删除
        /// </summary>
        /// <param name="ModelId"></param>
        /// <returns></returns>

        public static ESP.Finance.Utility.DeleteResult DeleteByModelId(int ModelId,int ModelType)
        {
            int res = 0;
            try
            {
                res = DataProvider.DeleteByModelId(ModelId, ModelType);
            }
            catch (Exception ex)
            {
                return ESP.Finance.Utility.DeleteResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.DeleteResult.UnExecute;
            }
            return ESP.Finance.Utility.DeleteResult.Failed;
        }

        public static ESP.Finance.Utility.DeleteResult DeleteByModelId(int ModelId,int ModelType, SqlTransaction trans)
        {
            int res = 0;
            try
            {
                res = DataProvider.DeleteByModelId(ModelId,ModelType, "", trans, null);

            }
            catch (Exception ex)
            {
                return ESP.Finance.Utility.DeleteResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.DeleteResult.UnExecute;
            }
            return ESP.Finance.Utility.DeleteResult.Failed;
        }

        public static int DeleteNotAudit(int ModelId,int ModelType)
        {
            return DataProvider.DeleteNotAudit(ModelId,ModelType);
        }

        /// <summary>
        /// 根据ModelId及其它条件删除
        /// </summary>
        /// <param name="ModelId"></param>
        /// <param name="term"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public static ESP.Finance.Utility.DeleteResult DeleteByModelId(int ModelId,int ModelType, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            int res = 0;
            try
            {
                res = DataProvider.DeleteByModelId(ModelId,ModelType, term, param);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.DeleteResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.DeleteResult.UnExecute;
            }
            return ESP.Finance.Utility.DeleteResult.Failed;
        }

        public static ESP.Finance.Entity.WorkFlowInfo GetModel(int AuditId)
        {
            return DataProvider.GetModel(AuditId);
        }



        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.WorkFlowInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.WorkFlowInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.WorkFlowInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param).OrderBy(n => n.Id).ToList<ESP.Finance.Entity.WorkFlowInfo>();
        }

        public static IList<ESP.Finance.Entity.WorkFlowInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans)
        {
            return DataProvider.GetList(term, param, trans).OrderBy(n => n.Id).ToList<ESP.Finance.Entity.WorkFlowInfo>();
        }


        #endregion 获得数据列表

        #endregion

    }
}
