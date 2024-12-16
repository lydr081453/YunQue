using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IDonationProvider
    {
        ESP.Finance.Entity.DonationInfo GetModel(int userId);
        IList<ESP.Finance.Entity.DonationInfo> GetList(string term);
        int Add(ESP.Finance.Entity.DonationInfo model);
    }
}
