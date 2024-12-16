using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
   public static class DonationManager
    {
       private static ESP.Finance.IDataAccess.IDonationProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IDonationProvider>.Instance; } }

       public static ESP.Finance.Entity.DonationInfo GetModel(int userId)
       {
           return DataProvider.GetModel(userId);
       }

       public static IList<ESP.Finance.Entity.DonationInfo> GetList(string term)
       {
           return DataProvider.GetList(term);
       }

       public static int Add(ESP.Finance.Entity.DonationInfo model)
       {
           return DataProvider.Add(model);
       }

    }
}
