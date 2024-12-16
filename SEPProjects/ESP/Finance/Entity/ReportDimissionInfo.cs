using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class ReportDimissionInfo
    {
        public int DimissionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Code { get; set; }
        public DateTime LastDay { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentPositionName { get; set; }
        public DateTime ChargeDate { get; set; }
        public decimal SalaryHigh { get; set; }
        public decimal SalaryLow { get; set; }
        public string LevelName { get; set; }
        public string DepartmentName { get; set; }
        public int Status { get; set; }
        public DateTime SalaryDate { get; set; }
        public DateTime AuditDate { get; set; }
        public string OperationType { get; set; }
    }
}
