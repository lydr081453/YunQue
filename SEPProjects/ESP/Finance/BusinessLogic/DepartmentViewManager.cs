using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.BusinessLogic
{
    public static class DepartmentViewManager
    {
        private static ESP.Finance.IDataAccess.IDepartmentViewProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IDepartmentViewProvider>.Instance; } }
        public static string GetBranchnameByDeptId(int groupId)
        {
            return DataProvider.GetBranchnameByDeptId(groupId);
        }
        public static ESP.Finance.Entity.DepartmentViewInfo GetModel(int level3Id)
        {
            return DataProvider.GetModel(level3Id);
        }
        public static DataTable GetList()
        {
            return DataProvider.GetList();
        }

        public static List<DepartmentViewInfo> GetList(string strWhere)
        {
            return DataProvider.GetList(strWhere);
        }
    }
}
