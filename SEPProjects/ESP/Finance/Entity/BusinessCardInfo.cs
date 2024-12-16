using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class BusinessCardInfo
    {
        public int BusinessCardId { get; set; }
        public string BusinessCardNo { get; set; }
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public int CardStatus { get; set; }
        public string HouseholdNo { get; set; }
        public decimal LineOfCredit { get; set; }
        public decimal AvailableCredit { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DrawStatus { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public DateTime CancellationDate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}