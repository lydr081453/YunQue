using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IBizTypeDataProvider
    {
        int Add(ESP.Finance.Entity.BizTypeInfo model);
        int Update(ESP.Finance.Entity.BizTypeInfo model);
        int Delete(int bizTypeId);
        ESP.Finance.Entity.BizTypeInfo GetModel(int bizTypeId);

        //IList<ESP.Finance.Entity.BizTypeInfo> GetAllList();
        //IList<ESP.Finance.Entity.BizTypeInfo> GetList(string term);
        IList<ESP.Finance.Entity.BizTypeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
