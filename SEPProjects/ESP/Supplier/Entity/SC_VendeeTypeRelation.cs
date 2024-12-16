using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_VendeeTypeRelation
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanySystemUserID { get; set; }
        public int TypeId { get; set; }
        public DateTime CreatTime { get; set; }
        public string CreatIP { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string LastUpdateIP { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int state { get; set; }
    }
}
