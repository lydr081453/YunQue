using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class ContractAuditLogInfo
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public string Contractor { get; set; }
        public int ProjectId { get; set; }
        public string AuditDesc { get; set; }
        public DateTime AuditDate { get; set; }

    }
}
