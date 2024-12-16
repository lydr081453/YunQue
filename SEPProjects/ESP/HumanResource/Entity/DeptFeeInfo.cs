using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    [Serializable]
    public class DeptFeeInfo
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal TotalFee { get; set; }
        public decimal AdvancePaid { get; set; }
        public DateTime CurrentDate { get; set; }
        public int EmpAmounts { get; set; }
        public decimal OtherFee { get; set; }
    }
}
