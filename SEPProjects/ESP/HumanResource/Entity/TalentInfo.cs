using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class TalentInfo
    {
        public int Id { get; set; }
        public string NameCN { get; set; }
        public string Mobile { get; set; }
        public string Position { get; set; }
        public string Education { get; set; }
        public string DeptShunya { get; set; }
        public string PositionShunya { get; set; }
        public DateTime WorkBegin { get; set; }
        public string HRInterview { get; set; }
        public string GroupInterview { get; set; }
        public string Resume { get; set; }

        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }

        public int Dept1Id { get; set; }
        public string Dept1 { get; set; }

        public int Dept2Id { get; set; }
        public string Dept2 { get; set; }

        public int Dept3Id { get; set; }
        public string Dept3 { get; set; }
        public string Customer { get; set; }

        public string Professional { get; set; }
        public string ResumeFiles { get; set; }
        public DateTime BirthDay { get; set; }
        public string Language { get; set; }

        public int UserId { get; set; }
        public int Status { get; set; }
        public string EMail { get; set; }

    }
}
