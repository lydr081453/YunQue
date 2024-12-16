using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class BranchDeptInfo
    {
        public BranchDeptInfo()
        { }

        public int Id { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int FianceFirstAuditorID { get; set; }
        public string FianceFirstAuditor { get; set; }



    }
}
