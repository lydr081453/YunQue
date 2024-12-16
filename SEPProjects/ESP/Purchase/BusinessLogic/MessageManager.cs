using System.Data;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类MessageManager 的摘要说明。
    /// </summary>
    public static class MessageManager
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Add(MessageInfo model)
        {
             MessageDataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(MessageInfo model)
        {
            MessageDataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            MessageDataProvider.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static MessageInfo GetModel(int id)
        {
            return MessageDataProvider.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return MessageDataProvider.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        public static List<MessageInfo> GetList()
        {
            return MessageDataProvider.GetList();
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="num">The num.</param>
        /// <returns></returns>
        public static DataSet GetList(int num,int areaid)
        {
            return MessageDataProvider.GetList(num,areaid);
        }
        #endregion  成员方法
    }
}