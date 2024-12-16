using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public static class UserPointRecordManager
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.UserPoint.Entity.UserPointRecordInfo model)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRecordDataProvider>.Instance.Add(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modellist"></param>
        /// <returns></returns>
        public static int Add(IList<ESP.UserPoint.Entity.UserPointRecordInfo> modellist)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRecordDataProvider>.Instance.Add(modellist);
        }
       
       
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.UserPoint.Entity.UserPointRecordInfo> GetList(string strWhere)
        {
            return ESP.Configuration.ProviderHelper<ESP.UserPoint.DataAccess.IUserPointRecordDataProvider>.Instance.GetList(strWhere);
        }
    }
}
