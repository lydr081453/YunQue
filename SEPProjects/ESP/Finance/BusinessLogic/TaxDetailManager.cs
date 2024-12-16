using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace ESP.Finance.BusinessLogic
{
    public static class TaxDetailManager
    {
        private static ESP.Finance.IDataAccess.ITaxDetailProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ITaxDetailProvider>.Instance; } }


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.TaxDetailInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ESP.Finance.Entity.TaxDetailInfo model)
        {
            return DataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int id)
        {
          return DataProvider.Delete(id);
          
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.TaxDetailInfo GetModel(int id)
        {
            return DataProvider.GetModel(id);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<TaxDetailInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<TaxDetailInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<TaxDetailInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表
        #endregion  成员方法
    }
}
