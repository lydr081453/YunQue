using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.ComponentModel;
using System.Transactions;

namespace ESP.Logging
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// 记录一条日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="category">日志类别</param>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        /// <param name="extendedProperties">其它信息</param>
        /// <param name="suppressTransaction">是否暂时抑制环境事务。</param>
        public static void Add(string message, string category, LogLevel level, Exception exception, IDictionary<string, object> extendedProperties, bool suppressTransaction)
        {
            string exceptionType = exception == null ? null : exception.GetType().FullName;
            string exceptionInfo = exception == null ? null : FormatException(exception);

            WebSiteInfo website = WebSiteManager.Get();
            string applicationName = website.WebSiteName;
            string applicationVersion = "0";
            int userId = UserManager.GetCurrentUserID();
            int processId = Process.GetCurrentProcess().Id;
            int threadId = Thread.CurrentThread.ManagedThreadId;

            string serverHostName = null;
            string serverAddress = null;
            int serverPort = 0;
            string clientHostName = null;
            string clientAddress = null;
            int clientPort = 0;
            string userAgent = null;
            string requestMethod = null;
            string url = null;
            string pagePath = null;

            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                serverHostName = context.Request.ServerVariables["SERVER_NAME"];
                serverAddress = context.Request.ServerVariables["LOCAL_ADDR"];
                int.TryParse(context.Request.ServerVariables["SERVER_PORT"], out serverPort);
                clientAddress = context.Request.UserHostAddress;
                clientHostName = context.Request.UserHostName;
                int.TryParse(context.Request.ServerVariables["REMOTE_PORT"], out clientPort);
                userAgent = context.Request.UserAgent;
                requestMethod = context.Request.HttpMethod;
                pagePath = context.Request.AppRelativeCurrentExecutionFilePath;
                url = context.Request.RawUrl;
            }

            string extendedPropertiesString = FormatExtendedProperties(extendedProperties);

            if (suppressTransaction)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ESP.Configuration.ProviderHelper<ILogProvider>.Instance.Add(message, category, level,
                        processId, threadId,
                        applicationName, applicationVersion,
                        url, pagePath, userId,
                        serverHostName, serverAddress, serverPort, clientHostName, clientAddress, clientPort, userAgent, requestMethod,
                        exceptionType, exceptionInfo, extendedPropertiesString);
                }
            }
            else
            {
                ESP.Configuration.ProviderHelper<ILogProvider>.Instance.Add(message, category, level,
                    processId, threadId,
                    applicationName, applicationVersion,
                    url, pagePath, userId,
                    serverHostName, serverAddress, serverPort, clientHostName, clientAddress, clientPort, userAgent, requestMethod,
                    exceptionType, exceptionInfo, extendedPropertiesString);
            }
        }

                /// <summary>
        /// 记录一条日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="category">日志类别</param>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        /// <param name="extendedProperties">其它信息</param>
        public static void Add(string message, string category, LogLevel level, Exception exception, IDictionary<string, object> extendedProperties)
        {
            Add(message, category, level, exception, extendedProperties, false);
        }

        private static string FormatExtendedProperties(IDictionary<string, object> extendedProperties)
        {
            if (extendedProperties == null || extendedProperties.Count == 0)
                return null;

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, object> entry in extendedProperties)
            {
                sb.Append(entry.Key).Append(":").Append(entry.Value).AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 记录一条日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="category">日志类别</param>
        /// <param name="level">日志级别</param>
        public static void Add(string message, string category, LogLevel level)
        {
            Add(message, category, level, null, null);
        }

        /// <summary>
        /// 记录一条消息日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="category">日志类别</param>
        public static void Add(string message, string category)
        {
            Add(message, category, LogLevel.Information, null, null);
        }

        /// <summary>
        /// 默认日志类别
        /// </summary>
        public const string DefaultCategory = "default";

        /// <summary>
        /// 记录一条消息日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Add(string message)
        {
            Add(message, DefaultCategory, LogLevel.Information, null, null);
        }

        /// <summary>
        /// 记录一条日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="category">日志类别</param>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        public static void Add(string message, string category, LogLevel level, Exception exception)
        {
            Add(message, category, level, exception, null);
        }

        /// <summary>
        /// 记录一条日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="category">日志类别</param>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        /// <param name="extendedProperties">扩展属性</param>
        /// <remarks>
        /// 参数 extendedProperties 可以为匿名类型的本地变量，
        /// 如 var obj = new { Property1:"value1", Property2:"value2" }
        /// 也可以是非匿名类变量，如
        /// class Class1
        /// {
        ///     public string Property1 { get; set; }
        ///     public string Property2 { get; set; }
        /// }
        /// var obj = new Class1();
        /// obj.Property1 = "value1";
        /// obj.Property2 = "value2";
        /// </remarks>
        public static void Add(string message, string category, LogLevel level, Exception exception, object extendedProperties)
        {
            IDictionary<string, object> dic = null;
            if (extendedProperties != null)
            {
                if (extendedProperties is IDictionary<string, object>)
                {
                    dic = (IDictionary<string, object>)extendedProperties;
                }
                else
                {
                    PropertyDescriptorCollection col = TypeDescriptor.GetProperties(extendedProperties);
                    dic = new Dictionary<string, object>(col.Count);
                    foreach (PropertyDescriptor descriptor in col)
                    {
                        object obj2 = descriptor.GetValue(extendedProperties);
                        dic.Add(descriptor.Name, obj2);
                    }
                }
            }
            Add(message, category, level, exception, dic);
        }


        private static string FormatException(Exception exception)
        {
            if (exception == null)
                return null;

            StringBuilder sb = new StringBuilder();
            while (exception != null)
            {
                sb.Append(exception.GetType().FullName).Append(":").AppendLine(exception.Message);
                sb.AppendLine(exception.StackTrace);
                sb.AppendLine();

                exception = exception.InnerException;
            }

            return sb.ToString();
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
        public static IList<LogEntry> Search(long pageIndex, int pageSize,
            string category, LogLevel logLevel,
            string applicationName, string applicationVersion,
            string pagePath, int userId,
            string serverAddress, int serverPort,
            string clientAddress, int clientPort, string userAgent, string requestMethod,
            string exceptionType, DateTime logTimeStart, DateTime logTimeEnd,
            ref long recordCount)
        {
            if (logTimeStart < new DateTime(2000, 1, 1))
                logTimeStart = new DateTime(2000, 1, 1);
            if (logTimeEnd > new DateTime(2200, 1, 1))
                logTimeEnd = new DateTime(2200, 1, 1);

            return ESP.Configuration.ProviderHelper<ILogProvider>.Instance.Search(pageIndex, pageSize, category, logLevel,
                applicationName, applicationVersion, pagePath, userId,
                serverAddress, serverPort, clientAddress, clientPort, userAgent, requestMethod,
                exceptionType, logTimeStart, logTimeEnd, ref recordCount);
        }

        /// <summary>
        /// 根据ID获取日志条目
        /// </summary>
        /// <param name="logId">日志条目的ID</param>
        /// <returns>日志条目</returns>
        public static LogEntry Get(long logId)
        {
            return ESP.Configuration.ProviderHelper<ILogProvider>.Instance.Get(logId);
        }
    }
}
