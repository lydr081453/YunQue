using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class PaymentScheduleInfo
    {
        public int Id { get; set; }
        public int PaymentID { get; set; }
        public DateTime PaymentPreDate { get; set; }
        public DateTime PaymentFactDate { get; set; }
        public string PaymentContent { get; set; }
        public int Status { get; set; }
        public decimal PaymentBudget { get; set; }
        public decimal PaymentFee { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Remark { get; set; }
        public int PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentTypeCode { get; set; }
        public int BankID { get; set; }
        public string CreditCode { get; set; }
        public decimal USDDiffer { get; set; }
        public int OperationType { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
