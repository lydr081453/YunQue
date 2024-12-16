using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    [Serializable]
    public class RiskConsultationInfo
    {
        public RiskConsultationInfo()
        { }
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CommitDate { get; set; }
        public int Total { get; set; }
        public int Prid { get; set; }
    }
}
