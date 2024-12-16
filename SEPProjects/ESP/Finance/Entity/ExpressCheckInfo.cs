using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class ExpressCheckInfo
    {
        public ExpressCheckInfo()
        { }
        public int Id { get; set; }
        public int Sort { get; set; }
        public string ExpressNo { get; set; }
        public string Sender { get; set; }
        public string City { get; set; }
        public DateTime SendTime { get; set; }
        public decimal Weight { get; set; }
        public decimal ExpPrice { get; set; }
        public decimal PackPrice { get; set; }
        public decimal InsureFee { get; set; }
        public decimal OtherFee { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public int ExpYear { get; set; }
        public int ExpMonth { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public string UserCode { get; set; }
        public int UserId { get; set; }
        public string ExpCompany { get; set; }

    }
}
