using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class TimingLogInfo
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public DateTime Time { get; set; }
        public string Remark { get; set; }
    }
}
