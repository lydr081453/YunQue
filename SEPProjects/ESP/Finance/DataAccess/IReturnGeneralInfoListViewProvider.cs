using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IGetReturnGeneralInfoListDataProvider
    {
        ESP.Finance.Entity.ReturnGeneralInfoListViewInfo GetModel(int ReturnID);
        IList<ESP.Finance.Entity.ReturnGeneralInfoListViewInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
