using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class SupportHistoryInfo
    {
        public int SupportId { get; set; }
        public DateTime CommitDate { get; set; }
        public byte[] HistoryData { get; set; }
    }
}
