using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class BranchVATInfo
    {
        public BranchVATInfo()
        { }

        public int Id { get; set; }
        public int AuditType { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public bool IsEdit { get; set; }
        public bool IsAudit { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}
