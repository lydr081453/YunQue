using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class ForeGiftLinkManager
    {
        private static ESP.Finance.IDataAccess.IForeGiftProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IForeGiftProvider>.Instance; } }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ForeGiftLinkInfo model)
        {
            return DataProvider.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ESP.Finance.Entity.ForeGiftLinkInfo model)
        {
            return DataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int linkId)
        {
            return DataProvider.Delete(linkId);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ForeGiftLinkInfo GetModel(int linkId)
        {
            return DataProvider.GetModel(linkId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ForeGiftLinkInfo> GetList(string strWhere, List<SqlParameter> paramList)
        {
            return DataProvider.GetList(strWhere, paramList);
        }

        /// <summary>
        /// 获得已和押金关联的付款申请
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parmList"></param>
        /// <returns></returns>
        public static DataTable GetKillList(string strWhere, List<SqlParameter> parmList)
        {
            return DataProvider.GetKillList(strWhere, parmList);
        }
    }
}
