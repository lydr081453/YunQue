using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class AuditHistoryManager
    {
         //private readonly ESP.Finance.DataAccess.AreaDAL dal = new ESP.Finance.DataAccess.AreaDAL();

        private static ESP.Finance.IDataAccess.IAuditHistoryDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IAuditHistoryDataProvider>.Instance;}}
        //private const string _dalProviderName = "AuditHistoryProvider";

        


        #region IAuditHistoryProvider 成员



        public static int Add(ESP.Finance.Entity.AuditHistoryInfo model)
        {
            //trans//return DataProvider.Add(model, true);
            int res = DataProvider.Add(model);

            return res;
        }

        public static int Add(List<ESP.Finance.Entity.AuditHistoryInfo> models)
        {
            if (models == null || models.Count == 0) return 0;
            int counter = 0;
            int projectID = models[0].ProjectID ?? 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //删除旧的授权
                    IList<ESP.Finance.Entity.AuditHistoryInfo> oldList = new ESP.Finance.DataAccess.AuditHistoryDataProvider().GetList(" projectid=@projectid", trans, new SqlParameter("@projectid", projectID));
                    new DataAccess.AuditHistoryDataProvider().DeleteByProjectId(projectID, trans);
                    ESP.Purchase.Entity.DataInfo dataInfo = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Project, projectID,trans);
                    //foreach (ESP.Finance.Entity.AuditHistoryInfo audit in oldList)
                    //{
                    //    new ESP.Purchase.DataAccess.DataPermissionProvider().DeletePermissionByUserID(dataInfo.Id, audit.AuditorUserID.Value, trans);
                    //}
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();

                    foreach (Entity.AuditHistoryInfo model in models)
                    {
                        int res = DataProvider.Add(model,trans);
                        ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                        p.UserId = model.AuditorUserID.Value;
                        p.IsEditor = true;
                        p.IsViewer = true;
                        if (dataInfo != null)
                            p.DataInfoId = dataInfo.Id;
                        permissionList.Add(p);
                        if (res > 0)
                        {
                            counter++;
                        }
                    }
                    //增加新的授权
                    if (dataInfo != null)
                        new ESP.Purchase.DataAccess.DataPermissionProvider().AppendDataPermission(dataInfo, permissionList, trans);
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            return counter;
        }

        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.AuditHistoryInfo model)
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
                        log.FormID = model.ProjectID;
                        log.FormType = (int)Utility.FormType.Project;
                        log.Suggestion = model.Suggestion;
                        new DataAccess.AuditLogDataProvider().Add(log,trans);
                      
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

        public static ESP.Finance.Entity.AuditHistoryInfo GetModel(int AuditId)
        {
            return DataProvider.GetModel(AuditId);
        }



        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.AuditHistoryInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.AuditHistoryInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.AuditHistoryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param).OrderBy(n => n.SquenceLevel).ToList<ESP.Finance.Entity.AuditHistoryInfo>();
        }

        public static IList<ESP.Finance.Entity.AuditHistoryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,SqlTransaction trans)
        {
            return DataProvider.GetList(term,trans, param.ToArray()).OrderBy(n => n.SquenceLevel).ToList<ESP.Finance.Entity.AuditHistoryInfo>();
        }

        /// <summary>
        /// 根据ProjectId 获取 根据auditType删除 后的列表 
        /// </summary>
        /// <param name="ProjectId">ProjectId</param>
        /// <param name="types">auditType</param>
        /// <returns>根据ProjectId 获取 根据auditType删除 后的列表</returns>
        public static IList<ESP.Finance.Entity.AuditHistoryInfo> GetUnDelList(int ProjectId, string types)
        {
            string term = string.Empty;
            if (!string.IsNullOrEmpty(types))
            {
                if (types.Trim().Length > 0)
                {
                    term = " and AuditType in (" + types + ")";
                    DataProvider.DeleteByProjectId(ProjectId, term, null,null);
                }
            }
            term = " ProjectID = @ProjectID ";
            List<System.Data.SqlClient.SqlParameter> param = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter sp = new System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4);
            sp.Value = ProjectId;
            param.Add(sp);
            return GetList(term, param);
        }

        #endregion 获得数据列表

        #endregion

    }
}
