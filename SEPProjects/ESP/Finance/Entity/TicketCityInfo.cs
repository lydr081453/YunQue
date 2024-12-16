using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
   public  class TicketCityInfo
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string CityEN { get; set; }
        public string ShortName { get; set; }
        public string Area { get; set; }

    }
}
