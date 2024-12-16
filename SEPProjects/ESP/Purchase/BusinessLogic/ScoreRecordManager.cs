using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
	/// 业务逻辑类ScoreRecordManager 的摘要说明。
	/// </summary>
	public class ScoreRecordManager
	{
		private static readonly ESP.Purchase.DataAccess.ScoreRecordDataProvider dal=new ESP.Purchase.DataAccess.ScoreRecordDataProvider();
		public ScoreRecordManager()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int RecordID)
		{
			return dal.Exists(RecordID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(Entity.ScoreRecordInfo model)
		{
			return dal.Add(model,null);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static void Update(Entity.ScoreRecordInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static void Delete(int RecordID)
		{
			dal.Delete(RecordID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static Entity.ScoreRecordInfo GetModel(int RecordID)
		{
			return dal.GetModel(RecordID);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Entity.ScoreRecordInfo> GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<Entity.ScoreRecordInfo> GetAllList()
		{
			return dal.GetList("");
		}
    }
}
