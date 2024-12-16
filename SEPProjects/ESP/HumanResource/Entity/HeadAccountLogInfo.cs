using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class HeadAccountLogInfo
    {
        public int Id { get; set; }
        public int HeadAccountId { get; set; }
        public string Remark { get; set; }
        public DateTime AuditDate { get; set; }
        public int Status { get; set; }
        public int AuditorId { get; set; }
        public string Auditor { get; set; }
        public int AuditType { get; set; }

    }
}
