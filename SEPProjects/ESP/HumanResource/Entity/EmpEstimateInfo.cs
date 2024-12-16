using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    [Serializable]
    public class EmpEstimateInfo
    {
        public int EstimateId { get; set; }
        public int UserId { get; set; }
        public string EstimateType { get; set; }
        public DateTime EstimateDate { get; set; }
        public string Result { get; set; }
        public string Remark { get; set; }

        public int CreatorId { get; set; }
        public string Creator { get; set; }
        public int Status { get; set; }

    }
}
