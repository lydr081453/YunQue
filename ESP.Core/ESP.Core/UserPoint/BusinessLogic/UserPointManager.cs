using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
  public static class UserPointManager
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
      public static int Add(ESP.UserPoint.Entity.UserPointInfo model)
      {
          return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointDataProvider>.Instance.Add(model);
      }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ESP.UserPoint.Entity.UserPointInfo model)
        {
         return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointDataProvider>.Instance.Update(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int ID)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointDataProvider>.Instance.Delete(ID);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
       public static  ESP.UserPoint.Entity.UserPointInfo GetModel(int ID)
       {
           return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointDataProvider>.Instance.GetModel(ID);
       }
        /// <summary>
        /// 获得数据列表
        /// </summary>
       public static IList<ESP.UserPoint.Entity.UserPointInfo> GetList(string strWhere)
      {
          return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointDataProvider>.Instance.GetList(strWhere);
      }

    }
}
