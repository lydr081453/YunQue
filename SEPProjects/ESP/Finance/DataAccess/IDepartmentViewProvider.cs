using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IDepartmentViewProvider
    {
        string GetBranchnameByDeptId(int groupId);
        ESP.Finance.Entity.DepartmentViewInfo GetModel(int level3Id);
        DataTable GetList();
        List<DepartmentViewInfo> GetList(string strWhere);
    }
}
