using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.UserPoint.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [ESP.Configuration.Provider]
    public interface  IGiftDataProvider
    {

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ESP.UserPoint.Entity.GiftInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(ESP.UserPoint.Entity.GiftInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int GiftID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ESP.UserPoint.Entity.GiftInfo GetModel(int GiftID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<ESP.UserPoint.Entity.GiftInfo> GetList(string strWhere);
      
        #endregion  成员方法
    }
}
