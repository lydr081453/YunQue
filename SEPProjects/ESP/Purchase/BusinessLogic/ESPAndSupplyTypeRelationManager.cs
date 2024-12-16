using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
	/// 业务逻辑类ESPAndSupplyTypeRelationManager 的摘要说明。
	/// </summary>
	public class ESPAndSupplyTypeRelationManager
	{
        private static readonly Purchase.DataAccess.ESPAndSupplyTypeRelationDataProvider dal = new Purchase.DataAccess.ESPAndSupplyTypeRelationDataProvider();
		public ESPAndSupplyTypeRelationManager()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(Entity.ESPAndSupplyTypeRelationInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static void Update(Entity.ESPAndSupplyTypeRelationInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public static void Delete(int id)
		{
            dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static Entity.ESPAndSupplyTypeRelationInfo GetModel(int id)
		{
            return dal.GetModel(id);
		}

        public static Entity.ESPAndSupplyTypeRelationInfo GetModelByEid(int ESPTypeId)
        {
            return dal.GetModelByEid(ESPTypeId); 
        }

        public static Entity.ESPAndSupplyTypeRelationInfo GetModelBySid(int SupplyTypeId)
        {
            return dal.GetModelBySid(SupplyTypeId);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<Entity.ESPAndSupplyTypeRelationInfo> GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<Entity.ESPAndSupplyTypeRelationInfo> GetAllList()
		{
			return dal.GetList("");
		}
    }
}
