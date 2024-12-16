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


    public static class RefundManager
    {
        private static ESP.Finance.IDataAccess.IRefundDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IRefundDataProvider>.Instance; } }

        #region  成员方法

        public static string CreateRefundCode()
        {
            return DataProvider.CreateRefundCode();
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.RefundInfo model)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int RefundID = DataProvider.Add(model, trans);
                    ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
                    datainfo.DataType = (int)Utility.FormType.Refund;
                    datainfo.DataId = RefundID;
                    List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
                    ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                    p.UserId = model.RequestorID;
                    p.IsEditor = true;
                    p.IsViewer = true;
                    permissionList.Add(p);
                    ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList, trans);
                    trans.Commit();
                    return RefundID;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static int Add(ESP.Finance.Entity.RefundInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            int RefundID = DataProvider.Add(model, trans);

            return RefundID;
        }


        public static int UpdateDismission(ESP.Finance.Entity.RefundInfo model, SqlTransaction trans)
        {
            return DataProvider.Update(model, trans);

        }
        public static int UpdateDismission(ESP.Finance.Entity.RefundInfo model)
        {
            return DataProvider.Update(model);

        }

        public static int Update(ESP.Finance.Entity.RefundInfo model)
        {
            return DataProvider.Update(model);
        }

        public static UpdateResult Update(ESP.Finance.Entity.RefundInfo model, SqlTransaction trans)
        {
            int res = 0;

                res = DataProvider.Update(model, trans);

            ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
            datainfo.DataType = (int)Utility.FormType.Refund;
            datainfo.DataId = model.Id;
            List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
            IList<ESP.Finance.Entity.WorkFlowInfo> auditList = ESP.Finance.BusinessLogic.WorkFlowManager.GetList(" ModelType="+(int)Utility.FormType.Refund+" and ModelId=" + model.Id.ToString(), null, trans);
            ESP.Purchase.Entity.DataPermissionInfo prequest = new ESP.Purchase.Entity.DataPermissionInfo();
            prequest.UserId = model.RequestorID;
            prequest.IsEditor = true;
            prequest.IsViewer = true;
            permissionList.Add(prequest);
            foreach (ESP.Finance.Entity.WorkFlowInfo audit in auditList)
            {
                ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                p.UserId = audit.AuditorUserID;
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
  
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int RefundID)
        {

            int res = 0;
            try
            {
                res = DataProvider.Delete(RefundID);
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

        public static int RefundAudit(ESP.Finance.Entity.RefundInfo refundModel, ESP.Compatible.Employee currentUser, int status, string suggestion, int NextFinanceAuditor)
        {
            return DataProvider.RefundAudit(refundModel, currentUser, status, suggestion, NextFinanceAuditor);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.RefundInfo GetModel(int RefundID)
        {

            return DataProvider.GetModel(RefundID);
        }
        public static ESP.Finance.Entity.RefundInfo GetModel(int RefundID, SqlTransaction trans)
        {

            return DataProvider.GetModel(RefundID, trans);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<RefundInfo> GetList(string term)
        {
            return GetList(term, null);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<RefundInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static int CommitWorkflow(RefundInfo refundModel, List<ESP.Finance.Entity.WorkFlowInfo> operationList)
        {
            return DataProvider.CommitWorkflow(refundModel, operationList);
        }

        public static IList<RefundInfo> GetWaitAuditList(int[] userIds)
        {
            return DataProvider.GetWaitAuditList(userIds);
        }
        #endregion  成员方法

    }
}
