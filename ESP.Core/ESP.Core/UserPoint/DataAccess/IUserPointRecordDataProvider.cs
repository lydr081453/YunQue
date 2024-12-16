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
    public interface IUserPointRecordDataProvider
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ESP.UserPoint.Entity.UserPointRecordInfo model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modellist"></param>
        /// <returns></returns>
        int Add(IList<ESP.UserPoint.Entity.UserPointRecordInfo> modellist);
                  
        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<ESP.UserPoint.Entity.UserPointRecordInfo> GetList(string strWhere);
        #endregion  成员方法
    }
}
