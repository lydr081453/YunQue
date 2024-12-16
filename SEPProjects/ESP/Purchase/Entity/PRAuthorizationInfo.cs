using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class PRAuthorizationInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string Remark { get; set; }
    }
}
