using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.Entity
{
    /// <summary>
    /// 代理人设置信息
    /// </summary>
    public class AuditBackUpInfo
    {
        /// <summary>
        /// 初始化 <see cref="AuditBackUpInfo"/> 类实例。
        /// </summary>
        public AuditBackUpInfo()
        { 
        }

        /// <summary>
        /// 流水号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 候补UserID
        /// </summary>
        public int BackupUserID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 委托是否有效，1为有效，0为无效
        /// </summary>
        public int Status {get;set; }
        /// <summary>
        /// 委托类型：1为代理 2为离职接收
        /// </summary>
        public int Type { get; set; }
    }
}
