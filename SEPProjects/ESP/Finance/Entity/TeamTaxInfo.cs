using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class TeamTaxInfo
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int TaxYear { get; set; }
        public int TaxMonth { get; set; }
        public decimal Tax { get; set; }

    }
}
