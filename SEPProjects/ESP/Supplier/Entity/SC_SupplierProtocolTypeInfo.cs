using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    [Serializable]
    public class SC_SupplierProtocolTypeInfo
    {
        public int id { get; set; }
        public int supplierId { get; set; }
        public int typeId { get; set; }
        public int supplierType { get; set; }
        public int addUserId { get; set; }
        public DateTime addUserTime { get; set; }

    }
}
