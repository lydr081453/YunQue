using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class RejectLogInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Reject描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int Operator { get; set; }

        /// <summary>
        /// Reject时间
        /// </summary>
        public DateTime OperateDate { get; set; }
    }
}
