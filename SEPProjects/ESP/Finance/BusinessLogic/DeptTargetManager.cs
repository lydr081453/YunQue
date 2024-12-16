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
     
     
    public static class DeptTargetManager
    {
        private static ESP.Finance.IDataAccess.IDeptTargetProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IDeptTargetProvider>.Instance; } }


        public static ESP.Finance.Entity.DeptTargetInfo GetModel(int deptid,int year)
        {

            return DataProvider.GetModel(deptid,year);
        }


        public static IList<ESP.Finance.Entity.DeptTargetInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }

    }
}
