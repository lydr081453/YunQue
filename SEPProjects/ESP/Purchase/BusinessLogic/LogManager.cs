using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    ///T_Log 的摘要说明
    /// </summary>
    public static class LogManager
    {
        private static LogDataProvider dal = new LogDataProvider();

        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int AddLog(LogInfo model, System.Web.HttpRequest request)
        {
            return dal.Add(model,request);
        }
        public static int AddLog(LogInfo model)
        {
            return dal.Add(model);
        }
        public static int AddLog(LogInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            return dal.Add(model, trans);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// Deletes the specified STR where.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        public static void Delete(string strWhere)
        {
            dal.Delete(strWhere);
        }

        /// <summary>
        /// Deletes the by gid.
        /// </summary>
        /// <param name="GId">The G id.</param>
        public static void DeleteByGid(int GId)
        {
            Delete(" GId="+GId.ToString());
        }

        /// <summary>
        /// Cancels the by gid.
        /// </summary>
        /// <param name="GId">The G id.</param>
        public static void CancelByGid(int GId)
        {
            dal.Cancel((" GId=" + GId.ToString()));
        }

        /// <summary>
        /// Gets the loglist.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<LogInfo> GetLoglist(string strWhere, List<SqlParameter> parms)
        {
            return LogDataProvider.GetLoglist(strWhere, parms);
        }
        public static LogInfo GetModel(int logId)
        {
            return LogDataProvider.GetModel(logId);
        }

        public static int Update(LogInfo model)
        {
           return LogDataProvider.Update(model);
        }

        /// <summary>
        /// Gets the loglist by G id.
        /// </summary>
        /// <param name="gid">The gid.</param>
        /// <returns></returns>
        public static List<LogInfo> GetLoglistByGId(int gid)
        {
            return LogDataProvider.GetLoglistByGId(" and GId=" + gid.ToString() + " and Status=" + State.log_status_normal.ToString());
        }

        public static List<LogInfo> GetLoglistWithFinance(int gid)
        {
            return LogDataProvider.GetLoglistByGId(" and GId=" + gid.ToString() + " and des not like '%修改PR信息%' and Status=" + State.log_status_normal.ToString());
        }
    }
}