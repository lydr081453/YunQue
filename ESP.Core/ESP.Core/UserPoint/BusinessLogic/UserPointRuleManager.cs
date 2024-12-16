using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
  public static class UserPointRuleManager
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.UserPoint.Entity.UserPointRuleInfo model)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRuleDataProvider>.Instance.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ESP.UserPoint.Entity.UserPointRuleInfo model)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRuleDataProvider>.Instance.Update(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int ID)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRuleDataProvider>.Instance.Delete(ID);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.UserPoint.Entity.UserPointRuleInfo GetModel(int ID)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRuleDataProvider>.Instance.GetModel(ID);
        }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="keycode"></param>
      /// <returns></returns>
        public static ESP.UserPoint.Entity.UserPointRuleInfo GetModelByKey(string keycode)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRuleDataProvider>.Instance.GetModelByKey(keycode);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.UserPoint.Entity.UserPointRuleInfo> GetList(string strWhere)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRuleDataProvider>.Instance.GetList(strWhere);
        }
    }
}
