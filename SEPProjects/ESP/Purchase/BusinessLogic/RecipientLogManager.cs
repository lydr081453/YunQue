using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    ///RecipientLogManager 的摘要说明
    /// </summary>
    public static class RecipientLogManager
    {
        private static RecipientLogDataHelper dal = new RecipientLogDataHelper();

        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int AddLog(RecipientLogInfo model, System.Web.HttpRequest request)
        {
            return dal.Add(model,request);
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

        public static ESP.Purchase.Entity.RecipientLogInfo GetModel(int logId)
        {
            return dal.GetModel(logId);
        }

        public static int Update(RecipientLogInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// Deletes the by gid.
        /// </summary>
        /// <param name="RId">The R id.</param>
        public static void DeleteByGid(int RId)
        {
            Delete(" RId=" + RId.ToString());
        }

        /// <summary>
        /// Cancels the by gid.
        /// </summary>
        /// <param name="RId">The R id.</param>
        public static void CancelByGid(int RId)
        {
            dal.Cancel((" RId=" + RId.ToString()));
        }

        /// <summary>
        /// Gets the loglist.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<RecipientLogInfo> GetLoglist(string strWhere, List<SqlParameter> parms)
        {
            return RecipientLogDataHelper.GetLoglist(strWhere, parms);
        }

        /// <summary>
        /// Gets the loglist by R id.
        /// </summary>
        /// <param name="RId">The R id.</param>
        /// <returns></returns>
        public static List<RecipientLogInfo> GetLoglistByRId(int RId)
        {
            return RecipientLogDataHelper.GetLoglistByRId(" and RId=" + RId.ToString() + " and Status=" + Common.State.log_status_normal.ToString());
        }
    }
}