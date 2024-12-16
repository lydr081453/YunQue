using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ScoreContentManager 的摘要说明。
    /// </summary>
    public class ScoreContentManager
    {
        private static readonly ESP.Purchase.DataAccess.ScoreContentDataProvider dal = new ESP.Purchase.DataAccess.ScoreContentDataProvider();
        public ScoreContentManager()
        { }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int ScoreContentID)
        {
            return dal.Exists(ScoreContentID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Entity.ScoreContentInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(Entity.ScoreContentInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int ScoreContentID)
        {
            dal.Delete(ScoreContentID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Entity.ScoreContentInfo GetModel(int ScoreContentID)
        {
            return dal.GetModel(ScoreContentID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<Entity.ScoreContentInfo> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<Entity.ScoreContentInfo> GetAllList()
        {
            return dal.GetList("");
        }
    }
}
