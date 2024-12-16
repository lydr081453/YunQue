using System.Data;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类OperationAuditLogManager 的摘要说明。
    /// </summary>
    public static class OperationAuditLogManager
    {
        private static OperationAuditLogDataProvider dal = new OperationAuditLogDataProvider();

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(OperationAuditLogInfo model,System.Web.HttpRequest request)
        {
            return dal.Add(model,request);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(OperationAuditLogInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="ID">The ID.</param>
        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }
        
        /// <summary>
        /// 删除申请单的业务审核日志
        /// </summary>
        /// <param name="gid"></param>
        public static void DeleteByGid(int gid)
        {
            dal.DeleteByGid(gid);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static OperationAuditLogInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得申请单的业务审核信息
        /// </summary>
        /// <param name="gid">申请单ID</param>
        /// <param name="auditorId">业务审核人ID</param>
        /// <returns></returns>
        public static OperationAuditLogInfo GetModel(int gid, int auditorId)
        {
            return dal.GetModel(gid, auditorId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得审核人的审核历史
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="auditorId"></param>
        /// <returns></returns>
        public static DataSet GetAuditLog(int auditorId)
        {
            return dal.GetAuditLog(auditorId);
        }

        /// <summary>
        /// Gets the loglist by G id.
        /// </summary>
        /// <param name="gid">The gid.</param>
        /// <returns></returns>
        public static List<OperationAuditLogInfo> GetLoglistByGId(int gid)
        {
            return OperationAuditLogDataProvider.GetLoglistByGId(" and gid=" + gid.ToString() + " and audtiStatus=" + State.log_status_normal.ToString());
        }
        #endregion  成员方法
    }
}