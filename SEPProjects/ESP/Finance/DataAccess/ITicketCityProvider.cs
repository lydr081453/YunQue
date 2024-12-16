using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ITicketCityProvider
    {
        ESP.Finance.Entity.TicketCityInfo GetModel(int id);
        IList<ESP.Finance.Entity.TicketCityInfo> GetList(string term);
    }
}

