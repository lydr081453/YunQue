using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    [Serializable]
    public class SupplierLogInfo
    {
        public int Id { get; set; }
        public int EspSupplierId { get; set; }
        public int SupplySupplierId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public DateTime ChangeDate { get;set; }
        public string Remark { get; set; }
        public string Operator { get; set; }
    }
}
