using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Framework.Entity;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 代理审核人控制类
    /// </summary>
    public static class AuditBackUpManager
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(AuditBackUpInfo model)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(AuditBackUpInfo model)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.Update(model);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.Delete(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        public static void DeleteByUserId(int userid)
        {
            ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.DeleteByUserId(userid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static AuditBackUpInfo GetModel(int id)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static AuditBackUpInfo GetModelByUserID(int userId)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.GetModelByUserID(userId);
        }

        /// <summary>
        /// 根据BackUpUserID获取代理设置对象列表
        /// </summary>
        /// <param name="backUpUserId">要获取的代理设置记录的BackUpUserID</param>
        /// <returns>代理设置记录对象列表</returns>
        public static IList<AuditBackUpInfo> GetModelsByBackUpUserID(int backUpUserId)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.GetModelsByBackUpUserID(backUpUserId);
        }
        /// <summary>
        ///  根据主用户获取离职委托实例
        /// </summary>
        /// <param name="userId">主用户</param>
        /// <returns></returns>
        public static AuditBackUpInfo GetLayOffModelByUserID(int userId)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.GetLayOffModelByUserID(userId);
        }
        /// <summary>
        /// 根据委托人获取离职委托列表
        /// </summary>
        /// <param name="backUpUserId">委托人</param>
        /// <returns></returns>
        public static IList<AuditBackUpInfo> GetLayOffModelsByBackUpUserID(int backUpUserId)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.GetLayOffModelsByBackUpUserID(backUpUserId);
        }

        /// <summary>
        /// 检查是否是可用代初审人
        /// </summary>
        /// <param name="userId">The sys user id.</param>
        /// <returns>
        /// 	<c>true</c> if [is back up user] [the specified sys user id]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBackUpUser(int userId)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.IsBackUpUser(userId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.IAuditBackUpDataProvider>.Instance.GetList(strWhere);
        }
    }
}
