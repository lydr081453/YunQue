using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace ESP.Finance.BusinessLogic
{
    public static class ProjectHistLogManager
    {
        private static ESP.Finance.IDataAccess.IProjectHistLogProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectHistLogProvider>.Instance; } }
      
        public static int Add(ESP.Finance.Entity.ProjectHistLogInfo model, SqlTransaction trans)
        {
            return DataProvider.Add(model, trans);
        }
        public static int Add(ESP.Finance.Entity.ProjectHistLogInfo model)
        {
            return DataProvider.Add(model);
        }
        public static ESP.Finance.Entity.ProjectHistLogInfo GetModel(int LogID)
        {
            return DataProvider.GetModel(LogID);
        }
        public static IList<ESP.Finance.Entity.ProjectHistLogInfo> GetList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetList(strWhere,parms);
        }
    }
}
