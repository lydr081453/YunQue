using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类RefundInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RefundInfo
    {
        public RefundInfo()
        { }
        #region Model
        public int Id { get; set; }
        public int PRID { get; set; }
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public decimal Amounts { get; set; }
        public DateTime RefundDate { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public int CostId { get; set; }
        public string RefundCode { get; set; }
        public int RequestorID { get; set; }
        public string RequestEmployeeName { get; set; }
        public int DeptId { get; set; }
        public DateTime RequestDate { get; set; }
        public string SupplierName { get; set; }
        public string SupplierBank { get; set; }
        public string SupplierAccount { get; set; }
        public string ProjectName { get; set; }
        public string PRNO { get; set; }
        public int PaymentUserID { get; set; }
        #endregion
    }
}
