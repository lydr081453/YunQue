using System.Data;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类T_AuditBackUp 的摘要说明。
    /// </summary>
    public static class AuditBackUpManager
    {
        private static AuditBackUpDataProvider dal = new AuditBackUpDataProvider();

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(AuditBackUpInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(AuditBackUpInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static AuditBackUpInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static AuditBackUpInfo GetModelByUserId(int userId)
        {
            return dal.GetModelByUserId(userId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 检查是否是可用代初审人
        /// </summary>
        /// <param name="sysUserId">The sys user id.</param>
        /// <returns>
        /// 	<c>true</c> if [is back up user] [the specified sys user id]; otherwise, <c>false</c>.
        /// </returns>
        public static bool isBackUpUser(int sysUserId)
        {
            return dal.isBackUpUser(sysUserId);
        }
        #endregion  成员方法
    }
}