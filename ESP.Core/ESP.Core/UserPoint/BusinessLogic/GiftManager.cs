using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
  public static  class GiftManager
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.UserPoint.Entity.GiftInfo model)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IGiftDataProvider>.Instance.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ESP.UserPoint.Entity.GiftInfo model)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IGiftDataProvider>.Instance.Update(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int ID)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IGiftDataProvider>.Instance.Delete(ID);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.UserPoint.Entity.GiftInfo GetModel(int ID)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IGiftDataProvider>.Instance.GetModel(ID);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.UserPoint.Entity.GiftInfo> GetList(string strWhere)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IGiftDataProvider>.Instance.GetList(strWhere);
        }
    }
}
