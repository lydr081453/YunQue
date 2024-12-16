using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class ITAssetReceivingManager
    {
        private static ESP.Finance.IDataAccess.IITAssetReceivingProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IITAssetReceivingProvider>.Instance; } }
        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ITAssetReceivingInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Add(ESP.Finance.Entity.ITAssetReceivingInfo model,string serverstring)
        {
            return DataProvider.Add(model, serverstring);
        }
        /// <summary>
        /// 更新其他系统领用状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="serverstring"></param>
        /// <returns></returns>
        public static int UpdateReturnStatus(ESP.Finance.Entity.ITAssetReceivingInfo model, string serverstring)
        {
            return DataProvider.UpdateReturnStatus(model, serverstring);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ESP.Finance.Entity.ITAssetReceivingInfo model)
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
        public static ESP.Finance.Entity.ITAssetReceivingInfo GetModel(int id)
        {
            return DataProvider.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ITAssetReceivingInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static ESP.Finance.Entity.ITAssetReceivingInfo getLastModel(int AssetId)
        {
            return DataProvider.getLastModel(AssetId);
        }

        #endregion  成员方法

    }
}
