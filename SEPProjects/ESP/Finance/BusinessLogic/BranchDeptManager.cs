using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类AreaInfo 的摘要说明。
    /// </summary>


    public static class BranchDeptManager
    {
        private static ESP.Finance.IDataAccess.IBranchDeptProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IBranchDeptProvider>.Instance; } }
       
        public static ESP.Finance.Entity.BranchDeptInfo GetModel(int branchid, int deptid)
        {
            return DataProvider.GetModel(branchid, deptid);
        }

        public static IList<ESP.Finance.Entity.BranchDeptInfo> GetList(int userid)
        {
            return DataProvider.GetList(userid);
        }
    }
}
