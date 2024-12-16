using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
     
    public static class ProjectReportFeeManager
    {
        private static ESP.Finance.IDataAccess.IProjectReportFeeProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectReportFeeProvider>.Instance; } }

        public static int Add(ESP.Finance.Entity.ProjectReportFeeInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Delete(string projectcode, int year, int month)
        {
            return DataProvider.Delete(projectcode, year, month);
        }

        public static int Delete(int groupid, int year, int month)
        {
            return DataProvider.Delete(groupid, year, month);
        }

        public static ESP.Finance.Entity.ProjectReportFeeInfo GetModel(string projectcode, int year, int month)
        {
            return DataProvider.GetModel(projectcode,year,month);
        }

        public static IList<ESP.Finance.Entity.ProjectReportFeeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static int  ImportData(DataSet ds)
        {
            return 0;
        }

    }
}
