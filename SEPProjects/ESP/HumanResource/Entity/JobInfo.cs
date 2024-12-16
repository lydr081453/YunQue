using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    /// <summary>
    /// 工作表实体类
    /// </summary>
    public class JobInfo
    {
        public int JobId { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string WorkingPlace { get; set; }
        /// <summary>
        /// 工作职责
        /// </summary>
        public string Responsibilities { get; set; }
        /// <summary>
        /// 职位要求
        /// </summary>
        public string Requirements { get; set; }
        /// <summary>
        /// 服务客户
        /// </summary>
        public string SerToCustomer { get; set; }
        /// <summary>
        /// 是否紧急
        /// </summary>
        public bool UrgentRecruitment { get; set; }
        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 记录最后修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creater { get; set; }
        /// <summary>
        /// 记录排序
        /// </summary>
        public int Ordinal { get; set; }
    }
}
