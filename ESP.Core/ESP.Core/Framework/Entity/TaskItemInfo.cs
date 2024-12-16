using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.Entity
{
    /// <summary>
    /// 代办事宜工作项
    /// </summary>
    public class TaskItemInfo
    {
        /// <summary>
        /// 单据标识ID
        /// </summary>
        public int FormID { get; set; }

        /// <summary>
        /// 申请人ID
        /// </summary>
        public int ApplicantID { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; }

        /// <summary>
        /// 待审批人ID
        /// </summary>
        public int ApproverID { get; set; }

        /// <summary>
        /// 待审批人姓名
        /// </summary>
        public string ApproverName { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime AppliedTime { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string FormType { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string FormNumber { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 单据页面的Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 显示所有审核人的页面的Url
        /// </summary>
        public string ApproversUrl { get; set; }
    }

}
