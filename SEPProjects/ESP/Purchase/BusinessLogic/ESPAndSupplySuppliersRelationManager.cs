using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.BusinessLogic
{
    public class ESPAndSupplySuppliersRelationManager
    {
        private static readonly Purchase.DataAccess.ESPAndSupplySuppliersRelationProvider dal = new Purchase.DataAccess.ESPAndSupplySuppliersRelationProvider();
        public ESPAndSupplySuppliersRelationManager()
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
        public static int Add(Entity.ESPAndSupplySuppliersRelation model)
		{
			return dal.Add(model);
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
        public static Entity.ESPAndSupplySuppliersRelation GetModel(int id)
		{
            return dal.GetModel(id);
		}

        public static Entity.ESPAndSupplySuppliersRelation GetModelByEid(int ESPId)
        {
            return dal.GetModelByEid(ESPId); 
        }

        public static Entity.ESPAndSupplySuppliersRelation GetModelBySid(int SupplyId)
        {
            return dal.GetModelBySid(SupplyId);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<Entity.ESPAndSupplySuppliersRelation> GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<Entity.ESPAndSupplySuppliersRelation> GetAllList()
		{
			return dal.GetList("");
		}
    }
}
