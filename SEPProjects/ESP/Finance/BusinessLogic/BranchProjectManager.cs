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


    public static class BranchProjectManager
    {
        private static ESP.Finance.IDataAccess.IBranchProjectProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IBranchProjectProvider>.Instance; } }

        public static ESP.Finance.Entity.BranchProjectInfo GetModel(int branchid, int deptid)
        {
            return DataProvider.GetModel(branchid, deptid);
        }

        public static IList<BranchProjectInfo> GetList(int userId)
        {
            return DataProvider.GetList(userId);
        }
    }
}
