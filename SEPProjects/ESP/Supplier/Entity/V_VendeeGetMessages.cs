using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class V_VendeeGetMessages
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Type { get; set; }
        public bool IsReaded { get; set; }
        public int CreatedUserID { get; set; }
        public bool IsDel { get; set; }
        public string ProductTypeIDs { get; set; }
        public int InfoID { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }
        public bool IsAnSupView { get; set; }
        public bool IsApporved { get; set; }
        public int OtherUserID { get; set; }
    }
}
