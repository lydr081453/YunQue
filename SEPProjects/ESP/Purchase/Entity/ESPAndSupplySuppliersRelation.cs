using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class ESPAndSupplySuppliersRelation
    {
        public int ID { get; set; }
        public int ESPSupplierId { get; set; }
        public int SupplySupplierId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
    }
}
