using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISupporterScheduleDataProvider
    {
        int Add(ESP.Finance.Entity.SupporterScheduleInfo model);
        int Update(ESP.Finance.Entity.SupporterScheduleInfo model);
        int Delete(int projectScheduleId);
        ESP.Finance.Entity.SupporterScheduleInfo GetModel(int projectScheduleId);

        //IList<ESP.Finance.Entity.CustomerIndustryInfo> GetAllList();
        //IList<ESP.Finance.Entity.CustomerIndustryInfo> GetList(string term);
        IList<ESP.Finance.Entity.SupporterScheduleInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.SupporterScheduleInfo> GetList(int supportId, SqlTransaction trans);
    }
}
