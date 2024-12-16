using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
   public class ExpenseBoarderInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Boarder { get; set; }
        public string Mobile { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
    }
}
