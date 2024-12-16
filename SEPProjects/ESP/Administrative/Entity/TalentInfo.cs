using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class TalentInfo
    {
        public TalentInfo() { }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string NameEN { get; set; }
        public string Phone2 { get; set; }
        public string InternalEmail { get; set; }
        public string IDNumber { get; set; }
        public string PrivateEmail { get; set; }
        public DateTime joinDate { get; set; }
        public DateTime LastDay { get; set; }
        public string WorkExperience { get; set; }
        public string WorkSpecialty { get; set; }
        public string Resume { get; set; }
        public string DeptName { get; set; }
        public string DepartmentPositionName { get; set; }
        public int UserID { get; set; }
        public int DeptId { get; set; }
        public int Status { get; set; }
        public string GraduatedFrom { get; set; }
        public string ThisYearSalary { get; set; }
        public string ContractCompany { get; set; }

        public DateTime WorkBegin { get; set; }

        public string Customer { get; set; }
    }
}
