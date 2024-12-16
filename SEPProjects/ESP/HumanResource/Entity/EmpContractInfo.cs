using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    [Serializable]
    public class EmpContractInfo
    {
        public int ContractId { get; set; }
        public int UserId { get; set; }
        public string Branch { get; set; }
        public int ContractYear { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SignDate { get; set; }
        public int Status { get; set; }
    }
}
