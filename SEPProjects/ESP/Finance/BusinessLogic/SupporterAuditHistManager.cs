using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class SupporterAuditHistManager
    {

        private static ESP.Finance.IDataAccess.ISupporterAuditHistDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ISupporterAuditHistDataProvider>.Instance;}}
        //private const string _dalProviderName = "SupporterAuditHistProvider";

        


        #region IAuditHistoryProvider 成员

         
         
        public static int Add(ESP.Finance.Entity.SupporterAuditHistInfo model)
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
        public static int Add(List<ESP.Finance.Entity.SupporterAuditHistInfo> models)
        {
            if (models == null || models.Count == 0) return 0;
            int supporterid = models[0].SupporterID == null ? 0 : models[0].SupporterID.Value;
            IList<ESP.Finance.Entity.SupporterAuditHistInfo> oldList = ESP.Finance.BusinessLogic.SupporterAuditHistManager.GetList(" SupporterID=" + supporterid.ToString());
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DataProvider.DeleteBySupporterId(supporterid,"",null,trans);
                    ESP.Purchase.Entity.DataInfo dataInfo = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Supporter, supporterid, trans);
                    
                    foreach (ESP.Finance.Entity.SupporterAuditHistInfo audit in oldList)
                    {
                        new ESP.Purchase.DataAccess.DataPermissionProvider().DeletePermissionByUserID(dataInfo.Id, audit.AuditorUserID.Value, trans);
                    }
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();

                    int counter = 0;
                    foreach (Entity.SupporterAuditHistInfo model in models)
                    {
                        int res = Add(model);
                        ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                        p.UserId = model.AuditorUserID.Value;
                        p.IsEditor = true;
                        p.IsViewer = true;
                        p.DataInfoId = dataInfo.Id;
                        permissionList.Add(p);
                        if (res > 0)
                        {
                            counter++;
                        }
                    }
                    //增加新的授权
                    ESP.Purchase.BusinessLogic.DataPermissionManager.AppendDataPermission(dataInfo, permissionList,trans);
                    trans.Commit();
                    return counter;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.SupporterAuditHistInfo model)
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
                        log.AuditDate = model.AuditDate;
                        log.AuditorEmployeeName = model.AuditorEmployeeName;
                        log.AuditorSysID = model.AuditorUserID;
                        log.AuditorUserCode = model.AuditorUserCode;
                        log.AuditorUserName = model.AuditorUserName;
                        log.AuditStatus = model.AuditStatus;
                        log.FormID = model.SupporterID;
                        log.FormType = (int)Utility.FormType.Supporter;
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
                catch(Exception ex)
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

        public static ESP.Finance.Entity.SupporterAuditHistInfo GetModel(int AuditId)
        {
            return DataProvider.GetModel(AuditId);
        }



        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.SupporterAuditHistInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.SupporterAuditHistInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.SupporterAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param).OrderBy(n => n.SquenceLevel).ToList<ESP.Finance.Entity.SupporterAuditHistInfo>();
        }

        /// <summary>
        /// 根据SupporterId 获取 根据auditType删除 后的列表 
        /// </summary>
        /// <param name="SupporterId">SupporterId</param>
        /// <param name="types">auditType</param>
        /// <returns>根据SupporterId 获取 根据auditType删除 后的列表</returns>
        public static IList<ESP.Finance.Entity.SupporterAuditHistInfo> GetUnDelList(int SupporterId, string types)
        {
            string term = string.Empty;
            if (!string.IsNullOrEmpty(types))
            {
                if (types.Trim().Length > 0)
                {
                    term = " and AuditType in (" + types + ")";
                    DataProvider.DeleteBySupporterId(SupporterId, term, null);
                }
            }
            term = " SupporterID = @SupporterID ";
            List<System.Data.SqlClient.SqlParameter> param = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter sp = new System.Data.SqlClient.SqlParameter("@SupporterID", System.Data.SqlDbType.Int, 4);
            sp.Value = SupporterId;
            param.Add(sp);
            return GetList(term, param);
        }
        #endregion 获得数据列表

        #endregion


    }
}
