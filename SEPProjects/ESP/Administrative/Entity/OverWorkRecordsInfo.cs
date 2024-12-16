using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Common;

namespace ESP.Administrative.Entity
{
    public class OverWorkRecordsInfo
    {
        public int Id { get; set; }
        public int OverWorkId { get; set; }
        public int TakeOffId { get; set; }
        public decimal Hours { get; set; }
        public OverWorkRecords_Types Type { get; set; }
    }
}
