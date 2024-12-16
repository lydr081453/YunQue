using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{


    public static class ConsumptionAuditManager
    {

        private static ESP.Finance.IDataAccess.IConsumptionAuditProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IConsumptionAuditProvider>.Instance; } }



        #region IAuditHistoryProvider 成员



        public static int Add(ESP.Finance.Entity.ConsumptionAuditInfo model)
        {
            int res = DataProvider.Add(model);

            return res;
        }

        public static int Add(ESP.Finance.Entity.ConsumptionAuditInfo model, SqlTransaction trans)
        {
            int res = DataProvider.Add(model,trans);

            return res;
        }

        public static int Add(List<ESP.Finance.Entity.ConsumptionAuditInfo> models)
        {
            if (models == null || models.Count == 0) return 0;
            int counter = 0;
            int BatchID = models[0].BatchID;
            int FormType = models[0].FormType.Value;

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //删除旧的授权
                   new DataAccess.ConsumptionAuditDataProvider().DeleteByBatchID(BatchID,FormType, trans);
                   foreach (var model in models)
                   {
                       Add(model, trans);
                       counter++;
                   }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            return counter;
        }

        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.ConsumptionAuditInfo model)
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
                        log.FormID = model.BatchID;
                        log.FormType = (int)Utility.FormType.Consumption;
                        log.Suggestion = model.Suggestion;
                        new DataAccess.AuditLogDataProvider().Add(log, trans);

                    }

                    trans.Commit();
                    return ESP.Finance.Utility.UpdateResult.Succeed;
                }
                catch
                {
                    trans.Rollback();
                    return ESP.Finance.Utility.UpdateResult.Failed;
                }
            }
        }

        public static int Update(ESP.Finance.Entity.ConsumptionAuditInfo model, SqlTransaction trans,Utility.FormType logFormType)
        {

            int res = DataProvider.Update(model, trans);
            if (res > 0 && model.AuditStatus != (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing)
            {
                Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                log.AuditDate = model.AuditDate;
                log.AuditorEmployeeName = model.AuditorEmployeeName;
                log.AuditorSysID = model.AuditorUserID;
                log.AuditorUserCode = model.AuditorUserCode;
                log.AuditorUserName = model.AuditorUserName;
                log.AuditStatus = model.AuditStatus;
                log.FormID = model.BatchID;
                log.FormType = (int)logFormType;
                log.Suggestion = model.Suggestion;
                new DataAccess.AuditLogDataProvider().Add(log, trans);

            }
            return res;
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

        public static ESP.Finance.Entity.ConsumptionAuditInfo GetModel(int AuditId)
        {
            return DataProvider.GetModel(AuditId);
        }



        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ConsumptionAuditInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ConsumptionAuditInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ConsumptionAuditInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param).OrderBy(n => n.SquenceLevel).ToList<ESP.Finance.Entity.ConsumptionAuditInfo>();
        }

        public static IList<ESP.Finance.Entity.ConsumptionAuditInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans)
        {
            return DataProvider.GetList(term, trans, param.ToArray()).OrderBy(n => n.SquenceLevel).ToList<ESP.Finance.Entity.ConsumptionAuditInfo>();
        }

        /// <summary>
        /// 根据BatchID 获取 根据auditType删除 后的列表 
        /// </summary>
        /// <param name="BatchID">BatchID</param>
        /// <param name="types">auditType</param>
        /// <returns>根据BatchID 获取 根据auditType删除 后的列表</returns>
        public static IList<ESP.Finance.Entity.ConsumptionAuditInfo> GetUnDelList(int BatchID, int formType, string types)
        {
            string term = string.Empty;
            if (!string.IsNullOrEmpty(types))
            {
                if (types.Trim().Length > 0)
                {
                    term = " and AuditType in (" + types + ")";
                    DataProvider.DeleteByBatchID(BatchID,formType, term, null, null);
                }
            }
            term = " BatchID = @BatchID and FormType=@FormType ";
            List<System.Data.SqlClient.SqlParameter> param = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter sp = new System.Data.SqlClient.SqlParameter("@BatchID", System.Data.SqlDbType.Int, 4);
            sp.Value = BatchID;
            param.Add(sp);

            System.Data.SqlClient.SqlParameter sp2 = new System.Data.SqlClient.SqlParameter("@FormType", System.Data.SqlDbType.Int, 4);
            sp2.Value = BatchID;
            param.Add(sp2);

            return GetList(term, param);
        }

        #endregion 获得数据列表

        #endregion

    }
}
