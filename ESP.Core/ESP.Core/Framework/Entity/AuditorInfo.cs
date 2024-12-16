using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.Entity
{
    /// <summary>
    /// 部门审核人、负责人信息
    /// </summary>
    public class AuditorInfo
    {
        /// <summary>
        /// 记录流水号
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 部门 ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 记录类型，如 Director, Manager, CEO, etc.
        /// </summary>
        public string AuditorType { get; set; }

        /// <summary>
        /// 用户 ID
        /// </summary>
        public int AuditorId { get; set; }

        /// <summary>
        /// 用户中文全名
        /// </summary>
        public string AuditorName { get; set; }
    }
}
