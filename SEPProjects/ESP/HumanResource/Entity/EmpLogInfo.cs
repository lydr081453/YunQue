using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    [Serializable]
    public class EmpLogInfo
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        public string Operator { get; set; }
        public int UserId { get; set; }
        public DateTime EditTime { get; set; }
        public int OperateType { get; set; }
        public string Remark { get; set; }

    }
}
