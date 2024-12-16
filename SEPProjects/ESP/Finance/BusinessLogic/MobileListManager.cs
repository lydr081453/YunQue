using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
namespace ESP.Finance.BusinessLogic
{
   public  class MobileListManager
    {
       private static ESP.Finance.IDataAccess.IMobileListProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IMobileListProvider>.Instance; } }
        private const string tablename = "MobileList";

        public static MobileListInfo GetModel(int userid)
        {
            return DataProvider.GetModel(userid);
        }
 
    }
}
