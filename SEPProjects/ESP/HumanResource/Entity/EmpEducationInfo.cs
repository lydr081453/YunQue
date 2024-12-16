using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.HumanResource.Entity
{
    public class EmpEducationInfo
    {
        public int EducationId { get; set; }
        public int UserId { get; set; }
        public string School { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Degree { get; set; }
        public string Profession { get; set; }

    }
}
