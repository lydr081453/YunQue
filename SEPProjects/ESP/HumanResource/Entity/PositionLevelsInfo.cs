using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class PositionLevelsInfo
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public string Remark { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ChargeRate { get; set; }
        public decimal SalaryHigh { get; set; }
        public decimal SalaryLow { get; set; }
        public decimal BillableRate { get; set; }
    }
}
