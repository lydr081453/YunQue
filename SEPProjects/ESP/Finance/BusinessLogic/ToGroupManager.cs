using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
    public static class ToGroupManager
    {
        private static ESP.Finance.IDataAccess.IToGroupProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IToGroupProvider>.Instance; } }

        public static int Add(ToGroupInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Update(ToGroupInfo model)
        {
            return DataProvider.Update(model);
        }

        public static int Delete(int toId)
        {
            return DataProvider.Delete(toId);
        }

        public static ToGroupInfo GetModel(int DepartmentId)
        {
            return DataProvider.GetModel(DepartmentId);
        }

        public static IList<ToGroupInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
    }
}
