using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IMobileListProvider
    {
       MobileListInfo GetModel(int userid);
    }

}
