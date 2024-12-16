using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class ToGroupInfo
    {
        public int ToId { get; set; }
        public string ToCode { get; set; }
        public string ToName { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
    }
}
