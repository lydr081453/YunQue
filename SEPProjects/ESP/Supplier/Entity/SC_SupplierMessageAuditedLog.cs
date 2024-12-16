using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierMessageAuditedLog
    {
        public int Id { get; set; }
        public int msgId { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateIp { get; set; }
        public string CreateDesc { get; set; }
    }
}
