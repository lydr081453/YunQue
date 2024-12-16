using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
   public partial class ReportJoinInfo
    {
        public int UserId { get; set; }
        public string UserName {get;set;}
        public string Code {get;set;}
        public DateTime JoinDate{get;set;}
        public int DepartmentId {get;set;}
        public string DepartmentPositionName{get;set;}
        public DateTime ChargeDate{get;set;}
        public Decimal SalaryHigh{get;set;}
        public Decimal SalaryLow{get;set;}
        public string LevelName {get;set;}
        public string DepartmentName {get;set;}
        public DateTime SalaryDate { get; set; }
        public int HeadCountId { get; set; }
        public string OperationType { get; set; }
    }
}
