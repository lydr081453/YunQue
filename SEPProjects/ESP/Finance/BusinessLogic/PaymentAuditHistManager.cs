using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{

     
     
    public static class PaymentAuditHistManager
    {

        private static ESP.Finance.IDataAccess.IPaymentAuditHistDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPaymentAuditHistDataProvider>.Instance;}}
        //private const string _dalProviderName = "PaymentAuditHistProvider";

        


        #region IAuditHistoryProvider 成员

         
         
        public static int Add(ESP.Finance.Entity.PaymentAuditHistInfo model)
        {
            //trans//return DataProvider.Add(model, true);
            int res = DataProvider.Add(model);
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <returns>成功执行的条数</returns>
        public static int Add(List<ESP.Finance.Entity.PaymentAuditHistInfo> models)
        {
            if (models == null || models.Count == 0) return 0;
            int counter = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DataProvider.DeleteByPaymentId(models[0].PaymentID == null ? 0 : models[0].PaymentID.Value,"",null,trans);
                    foreach (Entity.PaymentAuditHistInfo model in models)
                    {
                        int res = DataProvider.Add(model,trans);
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

        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.PaymentAuditHistInfo model)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    res = DataProvider.Update(model,trans);
                    if (res > 0)//如果更新成功,添加到审批日志中
                    {
                        Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                        log.AuditDate = model.AuditeDate;
                        log.AuditorEmployeeName = model.AuditorEmployeeName;
                        log.AuditorSysID = model.AuditorUserID;
                        log.AuditorUserCode = model.AuditorUserCode;
                        log.AuditorUserName = model.AuditorUserName;
                        log.AuditStatus = model.AuditeStatus;
                        log.FormID = model.PaymentID;
                        log.FormType = (int)Utility.FormType.Payment;
                        log.Suggestion = model.Suggestion;
                        new DataAccess.AuditLogDataProvider().Add(log, trans);
                    }
                    trans.Commit();
                }
                catch(Exception ex)
                {
                    trans.Rollback();
                    Console.WriteLine(ex.Message);
                    return ESP.Finance.Utility.UpdateResult.Failed;
                }
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.UpdateResult.UnExecute;
            }
            return ESP.Finance.Utility.UpdateResult.Failed;
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

        public static ESP.Finance.Entity.PaymentAuditHistInfo GetModel(int AuditId)
        {
            return DataProvider.GetModel(AuditId);
        }



        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.PaymentAuditHistInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.PaymentAuditHistInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.PaymentAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param).OrderBy(n => n.PaymentAuditID).ToList<ESP.Finance.Entity.PaymentAuditHistInfo>();
        }


        /// <summary>
        /// 根据PaymentId 获取 根据auditType删除 后的列表 
        /// </summary>
        /// <param name="PaymentId">PaymentId</param>
        /// <param name="types">auditType</param>
        /// <returns>根据PaymentId 获取 根据auditType删除 后的列表</returns>
        public static IList<ESP.Finance.Entity.PaymentAuditHistInfo> GetUnDelList(int PaymentId, string types)
        {
            string term = string.Empty;
            if (!string.IsNullOrEmpty(types))
            {
                if (types.Trim().Length > 0)
                {
                    term = " and AuditType in (" + types + ")";
                    DataProvider.DeleteByPaymentId(PaymentId, term, null);
                }
            }
            term = " PaymentID = @PaymentID ";
            List<System.Data.SqlClient.SqlParameter> param = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter sp = new System.Data.SqlClient.SqlParameter("@PaymentID", System.Data.SqlDbType.Int, 4);
            sp.Value = PaymentId;
            param.Add(sp);
            return GetList(term, param);
        }

        #endregion 获得数据列表

        #endregion

    }
}
