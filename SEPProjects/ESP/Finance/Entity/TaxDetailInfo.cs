using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public partial class TaxDetailInfo
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ReturnId { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime TaxDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public string ProjectCode { get; set; }
        public string ReturnCode { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int AuditerId { get; set; }
        public string Auditer { get; set; }
        public DateTime AuditDate { get; set; }
    }
}
