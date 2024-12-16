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
    public class Linq2EntitiesLogProvider : ILogProvider
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
        public void Add(string message, string category, LogLevel logLevel, int processId, int threadId, string applicationName, string applicationVersion, string url, string pagePath, int userId,
            string serverHostName, string serverAddress, int serverPort, string clientHostName, string clientAddress, int clientPort, string userAgent, string requestMethod,
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
            LogEntryInfo entry = new LogEntryInfo();
            entry.Message = message;
            entry.Category = category;
            entry.LogLevel = (int)logLevel;
            entry.ProcessID = processId;
            entry.ThreadID = threadId;
            entry.ApplicationName = applicationName;
            entry.ApplicationVersion = applicationVersion;
            entry.Url = url;
            entry.PagePath = pagePath;
            entry.UserID = userId;
            entry.ServerHostName = serverHostName;
            entry.ServerAddress = serverAddress;
            entry.ServerPort = serverPort;
            entry.ClientHostName = clientHostName;
            entry.ClientAddress = clientAddress;
            entry.ClientPort = clientPort;
            entry.UserAgent = userAgent;
            entry.RequestMethod = requestMethod;
            entry.ExceptionType = exceptionType;
            entry.ExceptionInfo = exceptionInfo;
            entry.ExtendedProperties = extendedProperties;

            using (DbConnectionHolder holder = new DbConnectionHolder())
            {
                LoggingEntities context = holder.GetObjectContext<LoggingEntities>();
                context.AddToLogEntries(entry);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="category"></param>
        /// <param name="logLevel"></param>
        /// <param name="applicationName"></param>
        /// <param name="applicationVersion"></param>
        /// <param name="pagePath"></param>
        /// <param name="userId"></param>
        /// <param name="serverAddress"></param>
        /// <param name="serverPort"></param>
        /// <param name="clientAddress"></param>
        /// <param name="clientPort"></param>
        /// <param name="userAgent"></param>
        /// <param name="requestMethod"></param>
        /// <param name="exceptionType"></param>
        /// <param name="logTimeStart"></param>
        /// <param name="logTimeEnd"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<LogEntry> Search(long pageIndex, int pageSize, string category, LogLevel logLevel, string applicationName, string applicationVersion, string pagePath, int userId, string serverAddress, int serverPort, string clientAddress, int clientPort, string userAgent, string requestMethod, string exceptionType, DateTime logTimeStart, DateTime logTimeEnd, ref long recordCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public LogEntry Get(long logId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}