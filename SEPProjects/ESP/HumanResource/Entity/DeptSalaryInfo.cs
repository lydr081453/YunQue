using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    [Serializable]
    public class DeptSalaryInfo
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int DepartmentId { get; set; }
        public decimal SalaryAverage { get; set; }

    }
}
