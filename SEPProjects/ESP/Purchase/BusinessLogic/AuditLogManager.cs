using System;
using System.Collections.Generic;
using System.Data;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类T_auditLog 的摘要说明。
    /// </summary>
    public static class AuditLogManager
    {
        private static AuditLogDataProvider dal = new AuditLogDataProvider();

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(AuditLogInfo model, System.Web.HttpRequest request)
        {
            return dal.Add(model, request, null);
        }

        public static int Add(AuditLogInfo model,System.Data.SqlClient.SqlTransaction trans)
        {
            return dal.Add(model, trans);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(AuditLogInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static AuditLogInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        public static IList<ESP.Purchase.Entity.AuditLogInfo> GetModelListByGID(int Gid)
        {
            return dal.GetModelListByGID(Gid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="auditType">0:驳回 1:通过 2:全部</param>
        /// <returns></returns>
        public static DataTable GetauditLog(int gid, int auditType)
        {
            string where = " gid=" + gid;
            if (auditType <= 1)
            {
                where += " and audittype=" + auditType;
            }

            DataSet ds = dal.GetList(where);
            return ds.Tables[0];
        }

/// <summary>
/// get audit log 
/// </summary>
/// <param name="isOk"></param>
/// <param name="prNo"></param>
/// <param name="gid"></param>
/// <param name="remark"></param>
/// <param name="CurrentUser"></param>
/// <param name="AuditUser"></param>
/// <returns></returns>
        public static AuditLogInfo getNewAuditLog(int isOk, string prNo, int gid, string remark, ESP.Compatible.Employee CurrentUser, int AuditUser)
        {
            //审核日志
            AuditLogInfo auditLog = new AuditLogInfo();
            auditLog.gid = gid;
            auditLog.prNo = prNo;
            auditLog.auditUserId = int.Parse(CurrentUser.SysID);
            auditLog.auditUserName = CurrentUser.Name;
            if (AuditUser != 0 && Convert.ToInt32(CurrentUser.SysID) != AuditUser)
            {
                ESP.Compatible.Employee emp =new ESP.Compatible.Employee(AuditUser);
                auditLog.remark = remark.Trim()+"["+CurrentUser.Name+"为"+emp.Name+"代理人]";
            }
            else
                auditLog.remark = remark.Trim();
            auditLog.remarkDate = DateTime.Now;
           // if (isOk)
            auditLog.auditType = isOk;
           // else
            //    auditLog.auditType = (int)ESP.Purchase.Common.State.operationAudit_status.No;

            return auditLog;
        }
        public static AuditLogInfo getNewAuditLog(bool isOk, string prNo, int gid, string remark, ESP.Compatible.Employee CurrentUser)
        {
            //审核日志
            AuditLogInfo auditLog = new AuditLogInfo();
            auditLog.gid = gid;
            auditLog.prNo = prNo;
            auditLog.auditUserId = int.Parse(CurrentUser.SysID);
            auditLog.auditUserName = CurrentUser.Name;
            auditLog.remark = remark.Trim();
            auditLog.remarkDate = DateTime.Now;
            if (isOk)
                auditLog.auditType = (int)ESP.Purchase.Common.State.operationAudit_status.Yes;
            else
                auditLog.auditType = (int)ESP.Purchase.Common.State.operationAudit_status.No;

            return auditLog;
        }
        #endregion  成员方法
    }
}