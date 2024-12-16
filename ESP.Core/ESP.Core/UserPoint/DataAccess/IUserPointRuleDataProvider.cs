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
    public interface IUserPointRuleDataProvider
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ESP.UserPoint.Entity.UserPointRuleInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(ESP.UserPoint.Entity.UserPointRuleInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int RuleID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ESP.UserPoint.Entity.UserPointRuleInfo GetModel(int RuleID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keycode"></param>
        /// <returns></returns>
        ESP.UserPoint.Entity.UserPointRuleInfo GetModelByKey(string keycode);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<ESP.UserPoint.Entity.UserPointRuleInfo> GetList(string strWhere);

        #endregion  成员方法
    }
}
