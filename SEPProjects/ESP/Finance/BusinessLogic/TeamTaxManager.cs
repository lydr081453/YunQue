using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Linq;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类BranchBLL 的摘要说明。
    /// </summary>


    public static class TeamTaxManager
    {
        private static ESP.Finance.IDataAccess.ITeamTaxProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ITeamTaxProvider>.Instance; } }

        public static int Add(ESP.Finance.Entity.TeamTaxInfo model)
        {
            return DataProvider.Add(model);
        }
        public static int Update(ESP.Finance.Entity.TeamTaxInfo model)
        {
            return DataProvider.Update(model);
        }
        public static int Delete(int deptId, int year, int month)
        {
            return DataProvider.Delete(deptId, year, month);
        }
        public static ESP.Finance.Entity.TeamTaxInfo GetModel(int deptId, int year, int month)
        {
            return DataProvider.GetModel(deptId, year, month);
        }
        public static IList<ESP.Finance.Entity.TeamTaxInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
    }
}
