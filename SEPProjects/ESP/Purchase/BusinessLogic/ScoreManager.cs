using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
	/// 业务逻辑类ScoreManager 的摘要说明。
	/// </summary>
	public class ScoreManager
	{
		private static readonly Purchase.DataAccess.ScoreDataProvider dal=new Purchase.DataAccess.ScoreDataProvider();
		public ScoreManager()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int ScoreID)
		{
			return dal.Exists(ScoreID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(Entity.ScoreInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static void Update(Entity.ScoreInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public static void Delete(int ScoreID)
		{
			dal.Delete(ScoreID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static Entity.ScoreInfo GetModel(int ScoreID)
		{
			return dal.GetModel(ScoreID);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<Entity.ScoreInfo> GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<Entity.ScoreInfo> GetAllList()
		{
			return dal.GetList("");
		}

        public static List<Entity.ScoreInfo> GetListByContentId(int contentId)
        {
            return dal.GetList(" and ScoreContentID="+contentId);
        }
    }
}
