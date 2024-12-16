using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectScheduleDataProvider
    {
        int Add(ESP.Finance.Entity.ProjectScheduleInfo model);
        int Update(ESP.Finance.Entity.ProjectScheduleInfo model);
        int Delete(int projectScheduleId);
        int Delete(string condition);
        ESP.Finance.Entity.ProjectScheduleInfo GetModel(int projectScheduleId);

        //IList<ESP.Finance.Entity.CustomerIndustryInfo> GetAllList();
        //IList<ESP.Finance.Entity.CustomerIndustryInfo> GetList(string term);
        IList<ESP.Finance.Entity.ProjectScheduleInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
