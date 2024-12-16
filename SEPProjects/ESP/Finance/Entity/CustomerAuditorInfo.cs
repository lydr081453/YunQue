using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
   public class CustomerAuditorInfo
    {
       public int Id { get; set; }
       public int CustomerId { get; set; }
       public string CustomerCode { get; set; }
       public int ProjectAuditor { get; set; }
       public int BranchId { get; set; }
       public string BranchCode { get; set; }
    }
}
