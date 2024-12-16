using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    [Serializable]
    public class SupplierShareInfo
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime AppDate { get; set; }
        public int SupplySupplierId { get; set; }
        public int ReceiverId { get; set; }
        public string Receiver { get; set; }
        public int Status { get; set; }

    }
}
