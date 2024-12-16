using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectReportFeeProvider
    {
        int Add(ESP.Finance.Entity.ProjectReportFeeInfo model);
        int Delete(string projectcode, int year ,int month);
        int Delete(int groupid, int year, int month);
        ESP.Finance.Entity.ProjectReportFeeInfo GetModel(string projectcode, int year, int month);
        IList<ESP.Finance.Entity.ProjectReportFeeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
