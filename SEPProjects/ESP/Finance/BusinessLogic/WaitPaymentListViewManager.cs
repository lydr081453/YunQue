using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class WaitPaymentListViewManager
    {
        private static ESP.Finance.IDataAccess.IGetWaitPaymentListDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IGetWaitPaymentListDataProvider>.Instance;}}
        //private const string _dalProviderName = "WaitPaymentListDALProviderViewInfo";

        


        #region  成员方法

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.WaitPaymentListViewInfo GetModel(int costtypeId)
        {

            return DataProvider.GetModel(costtypeId);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.WaitPaymentListViewInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.WaitPaymentListViewInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.WaitPaymentListViewInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }



        #endregion 获得数据列表
        #endregion  成员方法
    }
}
