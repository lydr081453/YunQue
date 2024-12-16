using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.Entity;
using System.Data;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 代理审核人数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IAuditBackUpDataProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        int Add(AuditBackUpInfo model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        int Update(AuditBackUpInfo model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        void Delete(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        void DeleteByUserId(int userid);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        AuditBackUpInfo GetModel(int id);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        AuditBackUpInfo GetModelByUserID(int userId);
        /// <summary>
        /// 获取离职委托实例
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        AuditBackUpInfo GetLayOffModelByUserID(int userId);
        /// <summary>
        /// 检查是否是可用代初审人
        /// </summary>
        /// <param name="sysUserId">The sys user id.</param>
        /// <returns>
        /// 	<c>true</c> if [is back up user] [the specified sys user id]; otherwise, <c>false</c>.
        /// </returns>
        bool IsBackUpUser(int sysUserId);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 根据BackUpUserID获取代理设置对象列表
        /// </summary>
        /// <param name="backUpUserId">要获取的代理设置记录的BackUpUserID</param>
        /// <returns>代理设置记录对象列表</returns>
        IList<AuditBackUpInfo> GetModelsByBackUpUserID(int backUpUserId);
        /// <summary>
        ///  获取离职委托列表
        /// </summary>
        /// <param name="backUpUserId"></param>
        /// <returns></returns>
        IList<AuditBackUpInfo> GetLayOffModelsByBackUpUserID(int backUpUserId);
    }
}
