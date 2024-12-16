using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Logging
{
    /// <summary>
    /// 日志项
    /// </summary>
    public class LogEntry
    {

        /// <summary>
        /// 日志的自动编号
        /// </summary>
        public long LogID { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// 当前进程ID
        /// </summary>
        public int ProcessID { get; set; }

        /// <summary>
        /// 当前线程ID
        /// </summary>
        public int ThreadID { get; set; }

        /// <summary>
        /// 当前应用程序名称
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// 当前应用程序版本
        /// </summary>
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// 当前请求的Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 当前正在执行的页面的路径（建议使用相对于应用程序根的虚拟路径）
        /// </summary>
        public string PagePath { get; set; }

        /// <summary>
        /// 当前用户的ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string serverName { get; set; }

        /// <summary>
        /// 服务器站点
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// 是否安全连接
        /// </summary>
        public bool IsSecure { get; set; }

        /// <summary>
        /// 客户端地址
        /// </summary>
        public string ClientAddress { get; set; }

        /// <summary>
        /// 客户端端口
        /// </summary>
        public int ClientPort { get; set; }

        /// <summary>
        /// 客户端浏览器
        /// </summary>
        public string UserAgent{ get; set; }

        /// <summary>
        /// HTTP 动作(GET, POST, ...)
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 异常的具体信息
        /// </summary>
        public string ExceptionInfo { get; set; }

        /// <summary>
        /// 其它相关信息
        /// </summary>
        public string ExtendedProperties { get; set; }

        /// <summary>
        /// 日志的记录时间
        /// </summary>
        public System.DateTime LogTime { get; set; }
    }

    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 无效，在查询时表示不过滤级别
        /// </summary>
        None = 0,

        /// <summary>
        /// 一般性信息
        /// </summary>
        Information = 1,
        
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 2,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 3,

        /// <summary>
        /// 致使错误
        /// </summary>
        Fatal = 4
    }
}
