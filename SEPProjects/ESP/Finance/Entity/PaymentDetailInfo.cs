using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类PaymentDetailInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PaymentDetailInfo
    {
        public PaymentDetailInfo()
        { }
        public int Id { get; set; }
        public int PaymentID { get; set; }
        public DateTime PaymentPredate { get; set; }
        public decimal PaymentPreAmount { get; set; }
        public string PaymentContent { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
        public string FileUrl { get; set; }

    }
}
