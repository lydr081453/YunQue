using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESP.DataAccessAuthorization.Entity;

namespace ESP.DataAccessAuthorization.DataAccess
{
    /// <summary>
    /// 操作权限数据操作
    /// </summary>
    public interface IDataAccessAction
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(DataAccessAction model);
        /// <summary>
        /// 更新一条数据
        /// 返回实际更新的数据数量
        /// </summary>
        int Update(DataAccessAction model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int AccessActionID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        DataAccessAction GetModel(int AccessActionID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<DataAccessAction> GetList(string strWhere);
        #endregion  成员方法

        /// <summary>
        /// 获取当前Action的Member列表
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        IList<Entity.DataAccessMember> GetDataAccessMemberList(int actionId);
    }
}
