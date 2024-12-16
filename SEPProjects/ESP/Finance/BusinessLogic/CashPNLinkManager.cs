using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
    public static class CashPNLinkManager
    {
        private static ESP.Finance.IDataAccess.ICashPNLinkProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICashPNLinkProvider>.Instance; } }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.CashPNLinkInfo model)
        {
            return DataProvider.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ESP.Finance.Entity.CashPNLinkInfo model)
        {
            return DataProvider.Update(model);
        }

        /// <summary>
        /// 现金抵消关联和更新Return
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="returnModel"></param>
        /// <returns></returns>
        public static bool AddAndUpdateReturn(List<ESP.Finance.Entity.CashPNLinkInfo> modelList, List<ESP.Finance.Entity.ReturnInfo> returnModel)
        {
            return DataProvider.AddAndUpdateReturn(modelList, returnModel);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int CashPNLinkId)
        {
            DataProvider.Delete(CashPNLinkId);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.CashPNLinkInfo GetModel(int CashPNLinkId)
        {
            return DataProvider.GetModel(CashPNLinkId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ESP.Finance.Entity.CashPNLinkInfo> GetList(string terms, List<SqlParameter> parms)
        {
            return DataProvider.GetList(terms, parms);
        }
    }
}
