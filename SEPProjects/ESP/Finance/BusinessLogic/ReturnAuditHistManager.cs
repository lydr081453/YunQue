using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{


    public static class ReturnAuditHistManager
    {

        private static ESP.Finance.IDataAccess.IReturnAuditHistDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IReturnAuditHistDataProvider>.Instance; } }
        //private const string _dalProviderName = "ReturnAuditHistProvider";




        #region IAuditHistoryProvider 成员
        public static int Add(ESP.Finance.Entity.ReturnAuditHistInfo model,SqlTransaction trans)
        {
            int res = DataProvider.Add(model,trans);
            if (model.AuditeStatus != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)
            {
                Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                log.AuditDate = model.AuditeDate;
                log.AuditorEmployeeName = model.AuditorEmployeeName;
                log.AuditorSysID = model.AuditorUserID;
                log.AuditorUserCode = model.AuditorUserCode;
                log.AuditorUserName = model.AuditorUserName;
                log.AuditStatus = model.AuditeStatus;
                log.FormID = model.ReturnID;
                log.FormType = (int)Utility.FormType.Return;
                log.Suggestion = model.Suggestion;
                new DataAccess.AuditLogDataProvider().Add(log, trans);
            }
            ESP.Purchase.Entity.DataInfo dataInfo = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Return, model.ReturnID, trans);
            List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
            ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
            p.UserId = model.AuditorUserID.Value;
            p.IsEditor = true;
            p.IsViewer = true;
            p.DataInfoId = dataInfo.Id;
            permissionList.Add(p);
            //增加新的授权
            ESP.Purchase.BusinessLogic.DataPermissionManager.AppendDataPermission(dataInfo, permissionList,trans);
            return res;
        }

        public static int Add(ESP.Finance.Entity.ReturnAuditHistInfo model)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int res = DataProvider.Add(model,trans);
                    if (model.AuditeStatus != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)
                    {
                        Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                        log.AuditDate = model.AuditeDate;
                        log.AuditorEmployeeName = model.AuditorEmployeeName;
                        log.AuditorSysID = model.AuditorUserID;
                        log.AuditorUserCode = model.AuditorUserCode;
                        log.AuditorUserName = model.AuditorUserName;
                        log.AuditStatus = model.AuditeStatus;
                        log.FormID = model.ReturnID;
                        log.FormType = (int)Utility.FormType.Return;
                        log.Suggestion = model.Suggestion;
                        new DataAccess.AuditLogDataProvider().Add(log, trans);
                    }
                    ESP.Purchase.Entity.DataInfo dataInfo = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Return, model.ReturnID, trans);
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                    ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                    p.UserId = model.AuditorUserID.Value;
                    p.IsEditor = true;
                    p.IsViewer = true;
                    p.DataInfoId = dataInfo.Id;
                    permissionList.Add(p);
                    //增加新的授权
                    ESP.Purchase.BusinessLogic.DataPermissionManager.AppendDataPermission(dataInfo, permissionList,trans);
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
        public static int Add(ESP.Finance.Entity.ReturnInfo returnModel,List<ESP.Finance.Entity.ReturnAuditHistInfo> models)
        {
            if (models == null || models.Count == 0) return 0;
            int counter = 0;
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> oldList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(" ReturnID=" + returnModel.ReturnID.ToString(), null);
           
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
                    returnModel.PaymentUserID = models[0].AuditorUserID;
                    returnModel.PaymentCode = models[0].AuditorUserCode;
                    returnModel.PaymentEmployeeName = models[0].AuditorEmployeeName;
                    returnModel.PaymentUserName = models[0].AuditorUserName;

                    ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel,trans);
                    new DataAccess.ReturnAuditHistDataProvider().DeleteByReturnID(returnModel.ReturnID, "", trans, null);
                    ESP.Purchase.Entity.DataInfo dataInfo = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Return, returnModel.ReturnID, trans);
                    foreach (ESP.Finance.Entity.ReturnAuditHistInfo audit in oldList)
                    {
                        new ESP.Purchase.DataAccess.DataPermissionProvider().DeletePermissionByUserID(dataInfo.Id, audit.AuditorUserID.Value, trans);
                    }

                    foreach (Entity.ReturnAuditHistInfo model in models)
                    {
                        int res = Add(model,trans);
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

        public static int Update(ESP.Finance.Entity.ReturnAuditHistInfo model, SqlTransaction trans)
        {
            int res = DataProvider.Update(model, trans);
            if (res > 0 && model.AuditeStatus.Value != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)//如果更新成功,添加到审批日志中
            {
                Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                log.AuditDate = model.AuditeDate;
                log.AuditorEmployeeName = model.AuditorEmployeeName;
                log.AuditorSysID = model.AuditorUserID;
                log.AuditorUserCode = model.AuditorUserCode;
                log.AuditorUserName = model.AuditorUserName;
                log.AuditStatus = model.AuditeStatus;
                log.FormID = model.ReturnID;
                log.FormType = (int)Utility.FormType.Return;
                log.Suggestion = model.Suggestion;
                new DataAccess.AuditLogDataProvider().Add(log, trans);
            }
            return res;
        }

        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.ReturnAuditHistInfo model)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    res = DataProvider.Update(model,trans);
                    if (res > 0 && model.AuditeStatus.Value != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)//如果更新成功,添加到审批日志中
                    {
                        Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                        log.AuditDate = model.AuditeDate;
                        log.AuditorEmployeeName = model.AuditorEmployeeName;
                        log.AuditorSysID = model.AuditorUserID;
                        log.AuditorUserCode = model.AuditorUserCode;
                        log.AuditorUserName = model.AuditorUserName;
                        log.AuditStatus = model.AuditeStatus;
                        log.FormID = model.ReturnID;
                        log.FormType = (int)Utility.FormType.Return;
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
        /// 根据ReturnID删除
        /// </summary>
        /// <param name="ReturnID"></param>
        /// <returns></returns>

        public static ESP.Finance.Utility.DeleteResult DeleteByReturnID(int ReturnID)
        {
            int res = 0;
            try
            {
                res = DataProvider.DeleteByReturnID(ReturnID);
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

        public static ESP.Finance.Utility.DeleteResult DeleteByReturnID(int ReturnID,SqlTransaction trans)
        {
            int res = 0;
            try
            {
                res = DataProvider.DeleteByReturnID(ReturnID, "", trans, null);
                
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

        public static int DeleteNotAudit(int returnId)
        {
            return DataProvider.DeleteNotAudit(returnId);
        }

        /// <summary>
        /// 根据ReturnID及其它条件删除
        /// </summary>
        /// <param name="ReturnID"></param>
        /// <param name="term"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public static ESP.Finance.Utility.DeleteResult DeleteByReturnID(int ReturnID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            int res = 0;
            try
            {
                res = DataProvider.DeleteByReturnID(ReturnID, term, param);
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

        public static ESP.Finance.Entity.ReturnAuditHistInfo GetModel(int AuditId)
        {
            return DataProvider.GetModel(AuditId);
        }



        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ReturnAuditHistInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ReturnAuditHistInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ReturnAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param).OrderBy(n => n.ReturnAuditID).ToList<ESP.Finance.Entity.ReturnAuditHistInfo>();
        }

        public static IList<ESP.Finance.Entity.ReturnAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,SqlTransaction trans)
        {
            return DataProvider.GetList(term,param,trans).OrderBy(n => n.ReturnAuditID).ToList<ESP.Finance.Entity.ReturnAuditHistInfo>();
        }

        /// <summary>
        /// 根据ReturnID 获取 根据auditType删除 后的列表 
        /// </summary>
        /// <param name="ReturnId">ReturnID</param>
        /// <param name="types">auditType</param>
        /// <returns>根据ReturnID 获取 根据auditType删除 后的列表</returns>
        public static IList<ESP.Finance.Entity.ReturnAuditHistInfo> GetUnDelList(int ReturnID, string types)
        {
            string term = string.Empty;
            if (!string.IsNullOrEmpty(types))
            {
                if (types.Trim().Length > 0)
                {
                    term = " and AuditType in (" + types + ")";
                    DataProvider.DeleteByReturnID(ReturnID, term, null);
                }
            }
            term = " ReturnID = @ReturnID ";
            List<System.Data.SqlClient.SqlParameter> param = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter sp = new System.Data.SqlClient.SqlParameter("@ReturnID", System.Data.SqlDbType.Int, 4);
            sp.Value = ReturnID;
            param.Add(sp);
            return GetList(term, param);
        }

        #endregion 获得数据列表

        #endregion

    }
}
