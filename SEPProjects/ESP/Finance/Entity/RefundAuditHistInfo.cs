using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类RefundAuditHistInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class WorkFlowInfo
    {
        public WorkFlowInfo()
        { }
        #region Model
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int AuditorUserID { get; set; }
        public string AuditorUserName { get; set; }
        public string AuditorUserCode { get; set; }
        public string AuditorEmployeeName { get; set; }
        public string Suggestion { get; set; }
        public DateTime? AuditDate { get; set; }
        public int AuditStatus { get; set; }
        public int AuditType { get; set; }
        public int ModelType { get; set; }
        #endregion
    }
}
