using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class DeptTargetInfo
    {
        public int Id { get; set; }
        public int DeptId { get; set; }
        public int Year { get; set; }
        public decimal Target { get; set; }
        public string UserIds { get; set; }

    }
}
