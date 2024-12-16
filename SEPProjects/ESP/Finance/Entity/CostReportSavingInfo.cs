using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类AreaInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    /// 
    [Serializable]
    public partial class CostReportSavingInfo
    {
        public CostReportSavingInfo()
        { }

        public int Id { get; set; }
        public int Creator { get; set; }
        public DateTime CreatTime { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectContent { get; set; }
        public string ApplicantName { get; set; }
        public string GroupName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalCost { get; set; }
        public decimal CostUsed { get; set; }
        public decimal CostBalance { get; set; }
        public decimal CostPercent { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal PaymentCash { get; set; }
        public decimal PaymentBill { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
