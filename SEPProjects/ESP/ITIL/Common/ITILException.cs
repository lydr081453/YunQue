using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ESP.ITIL.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ITILException : Exception
    {
        /// <summary>
        /// 异常站点
        /// 对应ESP数据库中的WebSites表
        /// </summary>
        public int WebSiteID { get; set; }
        /// <summary>
        /// 异常模块
        /// 对应ESP数据库中的Modules表
        /// </summary>
        public int ModuleID { get; set; }
        /// <summary>
        /// 当前Url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 当前操作人
        /// </summary>
        public int Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public int OperateTime { get; set; }
    }
}
