using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESP.DataAccessAuthorization.Entity;

namespace ESP.DataAccessAuthorization.DataAccess
{
    /// <summary>
    /// 数据表单操作权限数据操作
    /// </summary>
    public interface IWebPageDataAccess
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(WebPageDataAccess model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(WebPageDataAccess model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int DataFormActionID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        WebPageDataAccess GetModel(int DataFormActionID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<WebPageDataAccess> GetList(string strWhere);
        #endregion  成员方法

        /// <summary>
        /// 获取当前页DataAccessAction列表
        /// </summary>
        /// <param name="webPageId"></param>
        /// <returns></returns>
        IList<Entity.DataAccessAction> GetDataAccessActionList(int webPageId);
    }
}
