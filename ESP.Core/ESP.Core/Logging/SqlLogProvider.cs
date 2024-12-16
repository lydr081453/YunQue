using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using ESP.Framework.DataAccess.Utilities;
using System.Data.Common;
using ESP.Data;

namespace ESP.Logging
{
    /// <summary>
    /// 日志操作提供程序类
    /// </summary>
    public class SqlLogProvider : ILogProvider
    {
        #region ILogProvider 成员

        /// <summary>
        /// 记录一条日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="category">日志类别</param>
        /// <param name="logLevel">日志级别</param>
        /// <param name="processId">当前进程ID</param>
        /// <param name="threadId">当前线程ID</param>
        /// <param name="applicationName">当前应用程序名称</param>
        /// <param name="applicationVersion">当前应用程序版本</param>
        /// <param name="url">当前请求的Url</param>
        /// <param name="pagePath">当前正在执行的页面的路径</param>
        /// <param name="userId">当前用户ID</param>
        /// <param name="serverHostName">服务器名称</param>
        /// <param name="serverAddress">服务器站IP，如果主机为多IP配置，则该参数指定当前请求发生的IP</param>
        /// <param name="serverPort">当前请求的端口号</param>
        /// <param name="clientHostName">客户机名称</param>
        /// <param name="clientAddress">客户机IP地址</param>
        /// <param name="clientPort">客户端端口</param>
        /// <param name="userAgent">客户端浏览器</param>
        /// <param name="requestMethod">HTTP请求方法（GET, POST, ...）</param>
        /// <param name="exceptionType">异常类型</param>
        /// <param name="exceptionInfo">异常的详细信息</param>
        /// <param name="extendedProperties">其它信息</param>
        public void Add(string message, string category, LogLevel logLevel, int processId, int threadId,
            string applicationName, string applicationVersion, string url, string pagePath, int userId,
            string serverHostName, string serverAddress, int serverPort,
            string clientHostName, string clientAddress, int clientPort, string userAgent, string requestMethod,
            string exceptionType, string exceptionInfo, string extendedProperties)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(delegate(object obj)
            {
                AddInternal(message, category, logLevel, processId, threadId, applicationName, applicationVersion, url, pagePath, userId,
                    serverHostName, serverAddress, serverPort, clientAddress, clientAddress, clientPort, userAgent, requestMethod,
                    exceptionType, exceptionInfo, extendedProperties);
            }));
        }


        private void AddInternal(string message, string category, LogLevel logLevel, int processId, int threadId, string applicationName, string applicationVersion, string url, string pagePath, int userId,
    string serverHostName, string serverAddress, int serverPort, string clientHostName, string clientAddress, int clientPort, string userAgent, string requestMethod,
    string exceptionType, string exceptionInfo, string extendedProperties)
        {
            try
            {
                using (DbConnectionHolder holder = new DbConnectionHolder(ESP.Configuration.ConfigurationManager.ConnectionStringName))
                {
                    using (DbCommand cmd = holder.CreateCommand("sep_SysLogAdd", CommandType.StoredProcedure))
                    {
                        cmd.Parameters.Add(holder.CreateParameter("@Message", message));
                        cmd.Parameters.Add(holder.CreateParameter("@Category", category));
                        cmd.Parameters.Add(holder.CreateParameter("@LogLevel", logLevel));
                        cmd.Parameters.Add(holder.CreateParameter("@ProcessId", processId));
                        cmd.Parameters.Add(holder.CreateParameter("@ThreadId", threadId));
                        cmd.Parameters.Add(holder.CreateParameter("@ApplicationName", applicationName));
                        cmd.Parameters.Add(holder.CreateParameter("@ApplicationVersion", applicationVersion));
                        cmd.Parameters.Add(holder.CreateParameter("@Url", url));
                        cmd.Parameters.Add(holder.CreateParameter("@PagePath", pagePath));
                        cmd.Parameters.Add(holder.CreateParameter("@UserId", userId));
                        cmd.Parameters.Add(holder.CreateParameter("@ServerHostName", serverHostName));
                        cmd.Parameters.Add(holder.CreateParameter("@ServerAddress", serverAddress));
                        cmd.Parameters.Add(holder.CreateParameter("@ServerPort", serverPort));
                        cmd.Parameters.Add(holder.CreateParameter("@ClientHostName", clientHostName));
                        cmd.Parameters.Add(holder.CreateParameter("@ClientAddress", clientAddress));
                        cmd.Parameters.Add(holder.CreateParameter("@ClientPort", clientPort));
                        cmd.Parameters.Add(holder.CreateParameter("@UserAgent", userAgent));
                        cmd.Parameters.Add(holder.CreateParameter("@RequestMethod", requestMethod));
                        cmd.Parameters.Add(holder.CreateParameter("@ExceptionType", exceptionType));
                        cmd.Parameters.Add(holder.CreateParameter("@ExceptionInfo", exceptionInfo));
                        cmd.Parameters.Add(holder.CreateParameter("@ExtendedProperties", extendedProperties));
                        cmd.Parameters.Add(holder.CreateParameter("@LogTime", DateTime.UtcNow));

                        holder.DbConnection.Open();
                        cmd.ExecuteNonQuery();
                        holder.DbConnection.Close();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 搜索日志条目
        /// </summary>
        /// <param name="pageIndex">分码，从0开始</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="category">日志类别</param>
        /// <param name="logLevel">要检索的日志的级别</param>
        /// <param name="applicationName">应用程序名称（模糊匹配）</param>
        /// <param name="applicationVersion">应用程序版本</param>
        /// <param name="pagePath">页面路径（模糊匹配）</param>
        /// <param name="userId">用户ID</param>
        /// <param name="serverAddress">服务器站IP</param>
        /// <param name="serverPort">当前请求的端口号</param>
        /// <param name="clientAddress">客户端地址</param>
        /// <param name="clientPort">客户端端口</param>
        /// <param name="userAgent">客户端浏览器（模糊匹配）</param>
        /// <param name="requestMethod">HTTP请求方法（GET, POST, ...）</param>
        /// <param name="exceptionType">异常类型（模糊匹配）</param>
        /// <param name="logTimeStart">要检索的日志记录的起始时间</param>
        /// <param name="logTimeEnd">要检索的日志记录的结束时间</param>
        /// <param name="recordCount">当前总记录数</param>
        /// <returns>检索到的日志列表</returns>
        /// <remarks>
        /// 除userId外对于所有的过滤条件参数，如果为0或空引用或空字符串或
        /// ESP.Framework.DataAccess.Utilities.NullValues.DateTime，则忽略该参数。
        /// userId 为 -1 时忽略 userId 参数。
        /// 
        /// recordCount 用于保持分页状态。因为日志表中的数据可能会快速增长，在
        /// 查询完第一次后再次查询第二页时，需要知道第一页的位置，以免快速增长
        /// 的日志将第一页淹没，而使第二页返回更晚的记录。在第一次查询时使用0作
        /// 为recordCount参数的值，后序翻页调用使用第一次调用返回的recordCount，
        /// 即可在多次翻页中保持分页的相对位置。
        /// </remarks>
        public IList<LogEntry> Search(long pageIndex, int pageSize,
            string category, LogLevel logLevel,
            string applicationName, string applicationVersion,
            string pagePath, int userId,
            string serverAddress, int serverPort,
            string clientAddress, int clientPort, string userAgent, string requestMethod,
            string exceptionType, DateTime logTimeStart, DateTime logTimeEnd,
            ref long recordCount)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ESP.Configuration.ConfigurationManager.ConnectionStringName))
            {
                using (DbCommand cmd = holder.CreateCommand("sep_SysLogSearch", CommandType.StoredProcedure))
                {
                    DbParameter recordCountParameter = holder.CreateParameter("@RecordCount", recordCount, ParameterDirection.InputOutput, DbType.Int64);
                    cmd.Parameters.Add(holder.CreateParameter("@PageIndex", pageIndex));
                    cmd.Parameters.Add(holder.CreateParameter("@PageSize", pageSize));
                    cmd.Parameters.Add(holder.CreateParameter("@Category", category));
                    cmd.Parameters.Add(holder.CreateParameter("@LogLevel", logLevel));
                    cmd.Parameters.Add(holder.CreateParameter("@ApplicationName", applicationName));
                    cmd.Parameters.Add(holder.CreateParameter("@ApplicationVersion", applicationVersion));
                    cmd.Parameters.Add(holder.CreateParameter("@PagePath", pagePath));
                    cmd.Parameters.Add(holder.CreateParameter("@UserID", userId));
                    cmd.Parameters.Add(holder.CreateParameter("@ServerAddress", serverAddress));
                    cmd.Parameters.Add(holder.CreateParameter("@ServerPort", serverPort));
                    cmd.Parameters.Add(holder.CreateParameter("@ClientAddress", clientAddress));
                    cmd.Parameters.Add(holder.CreateParameter("@ClientPort", clientPort));
                    cmd.Parameters.Add(holder.CreateParameter("@UserAgent", userAgent));
                    cmd.Parameters.Add(holder.CreateParameter("@RequestMethod", requestMethod));
                    cmd.Parameters.Add(holder.CreateParameter("@ExceptionType", exceptionType));
                    cmd.Parameters.Add(holder.CreateParameter("@LogTimeStart", logTimeStart));
                    cmd.Parameters.Add(holder.CreateParameter("@LogTimeEnd", logTimeEnd));
                    cmd.Parameters.Add(recordCountParameter);
                    holder.DbConnection.Open();

                    IList<LogEntry> entries = null;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        entries = CBO.LoadList<LogEntry>(reader);
                    }

                    recordCount = (long)recordCountParameter.Value;
                    holder.DbConnection.Close();

                    return entries;

                }
            }
        }

        /// <summary>
        /// 根据ID获取日志条目
        /// </summary>
        /// <param name="logId">日志条目的ID</param>
        /// <returns>日志条目</returns>
        public LogEntry Get(long logId)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ESP.Configuration.ConfigurationManager.ConnectionStringName))
            {
                using (DbCommand cmd = holder.CreateCommand(@"SELECT * FROM sep_SysLog WHERE LogID = @LogID", CommandType.StoredProcedure))
                {
                    cmd.Parameters.Add(holder.CreateParameter("@LogID", logId));
                    holder.DbConnection.Open();
                    var ret = CBO.LoadObject<LogEntry>(cmd.ExecuteReader());
                    holder.DbConnection.Close();

                    return ret;
                }
            }
        }

        #endregion
    }
}
