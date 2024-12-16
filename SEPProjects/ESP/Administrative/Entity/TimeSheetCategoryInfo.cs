using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class TimeSheetCategoryInfo
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Remark { get; set; }
        public int Billable { get; set; }
        public int ParentId { get; set; }
    }
}
