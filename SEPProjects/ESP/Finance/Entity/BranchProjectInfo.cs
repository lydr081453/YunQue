using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class BranchProjectInfo
    {
        public BranchProjectInfo()
        { }

        public int Id { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int AuditorID { get; set; }
        public string Auditor { get; set; }



    }
}
