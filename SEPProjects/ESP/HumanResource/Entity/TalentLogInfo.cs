using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class TalentLogInfo
    {
        public int Id { get; set; }
        public int TalentId { get; set; }
        public int AuditorId { get; set; }
        public string AuditorName { get; set; }
        public string Remark { get; set; }
        public DateTime auditDate { get; set; }
    }
}
