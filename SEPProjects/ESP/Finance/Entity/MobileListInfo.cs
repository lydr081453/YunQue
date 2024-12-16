using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
   public class MobileListInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
