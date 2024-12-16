using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class DepartmentViewInfo
    {
        public int level3Id { get; set; }
        public int level2Id { get; set; }
        public int level1Id { get; set; }
        public string level1 { get; set; }
        public string level2 { get; set; }
        public string level3 { get; set; }
        public decimal SalaryAmount { get; set; }
        public int Ordinal { get; set; }
    }
}
