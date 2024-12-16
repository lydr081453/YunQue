using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class EmpWorkExpInfo
    {
        public int WorkId { get; set; }
        public int UserId { get; set; }
        public string Company { get; set; }
        public string Dept { get; set; }
        public string Position { get; set; }
        public DateTime JoinDate { get; set; }
        public string Email { get; set; }
        public string Skill { get; set; }
        public string Experience { get; set; }
        public string ServeYear { get; set; }
        public string Director { get; set; }

    }
}
