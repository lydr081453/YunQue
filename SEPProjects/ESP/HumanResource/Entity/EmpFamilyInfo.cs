using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ESP.HumanResource.Entity
{
    public class EmpFamilyInfo
    {
        public int MemberId { get; set; }
        public int UserId { get; set; }
        public string MemberName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Relation { get; set; }
        public string Company { get; set; }
    }
}
