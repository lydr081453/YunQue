using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    /// <summary>
    /// 消耗导入审批流实体类
    /// </summary>
    [Serializable]
    public class ConsumptionAuditInfo
    {

        public ConsumptionAuditInfo()
        { }
        #region"model"
        public int? FormType{get;set;}
        public int? AuditType{get;set;}
        public int? Version{get;set;}
        /// <summary>
        /// 
        /// </summary>
        public int AuditID{get;set;}
        /// <summary>
        /// 审批状态：1审批通过 2审批驳回
        /// </summary>
        public int? AuditStatus{get;set;}
        /// <summary>
        /// 
        /// </summary>
        public int BatchID{get;set;}
        /// <summary>
        /// 审批人信息
        /// </summary>
        public int AuditorUserID{get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string AuditorUserName{get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string AuditorUserCode{get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string AuditorEmployeeName{get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string Suggestion{get;set;}
        /// <summary>
        /// 所处审批级别
        /// </summary>
        public int? SquenceLevel{get;set;}
        /// <summary>
        /// 审批总级别
        /// </summary>
        public int? TotalLevel{get;set;}

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditDate{get;set;}


        #endregion Model

    }
}
