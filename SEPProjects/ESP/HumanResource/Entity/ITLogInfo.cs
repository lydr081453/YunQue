using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class ITLogInfo
    {
        public int Id { get; set; }
        public int ITItemId { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public DateTime LogTime { get; set; }
        public string remark { get; set; }
        public int Status { get; set; }


    }
}
