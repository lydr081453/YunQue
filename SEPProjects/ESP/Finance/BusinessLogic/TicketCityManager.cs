using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class TicketCityManager
    {
        private static ESP.Finance.IDataAccess.ITicketCityProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ITicketCityProvider>.Instance; } }
       
        public static ESP.Finance.Entity.TicketCityInfo GetModel(int id)
        {
            return DataProvider.GetModel(id);
        }
        public static IList<ESP.Finance.Entity.TicketCityInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }
    }
}
