using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类CreditBLL 的摘要说明。
    /// </summary>


    public static class CredenceManager
    {
        private static ESP.Finance.IDataAccess.ICredeneProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICredeneProvider>.Instance; } }

        public static int Add(CredenceInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Update(CredenceInfo model)
        {
            return DataProvider.Update(model);
        }

        public static int Delete(int credenceId)
        {
          return  DataProvider.Delete(credenceId);
        }

        public static CredenceInfo GetModel(int userid)
        {
            return DataProvider.GetModel(userid);
        }

        public static IList<CredenceInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

    }
}
