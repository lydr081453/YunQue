using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESP.DataAccessAuthorization.Entity;

namespace ESP.DataAccessAuthorization.DataAccess
{
    /// <summary>
    /// 权限成员数据操作
    /// </summary>
    public interface IDataAccessMember
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(DataAccessMember model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(DataAccessMember model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int AccessMemberID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        DataAccessMember GetModel(int AccessMemberID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<DataAccessMember> GetList(string strWhere);
        #endregion  成员方法
    }
}
