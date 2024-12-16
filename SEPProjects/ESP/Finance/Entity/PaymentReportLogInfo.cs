using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class PaymentReportLogInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ReadTime { get; set; }

    }
}
