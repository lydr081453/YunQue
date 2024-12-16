using System.Data;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Text;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类TypeBaseHelperManager 的摘要说明。
    /// </summary>
    public static class TypeManager
    {
        private static TypeDataProvider dal = new TypeDataProvider();

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(TypeInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(TypeInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 停用或启用物料类别
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        public static void BlockUpOrUse(int typeid, int level, int updateStatus)
        {
            dal.Delete(typeid, level, updateStatus);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        /// <returns></returns>
        public static TypeInfo GetModel(int typeid)
        {
            return dal.GetModel(typeid);
        }

        public static TypeInfo GetModelByPTBId(string ptbId)
        {
            return dal.GetModelByBTBId(ptbId);
        }

        /// <summary>
        /// Gets the name of the model by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static TypeInfo GetModelByName(string name)
        {
            return dal.GetModelByName(name);
        }


        /// <summary>
        /// Gets the model by operation flow.
        /// </summary>
        /// <param name="operationflow">The operationflow.</param>
        /// <returns></returns>
        public static TypeInfo GetModelByOperationFlow(int operationflow)
        {
            return dal.GetModelByOperationFlow(operationflow);
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

        public static List<TypeInfo> GetModelList(string strWhere)
        {
            return dal.GetModelList(strWhere);
        }

        /// <summary>
        /// 根据parentId获得列表(仅供基础数据维护处使用）
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <returns></returns>
        public static List<TypeInfo> GetListForBase(int parentId)
        {
            return dal.GetListForBase(parentId);
        }

        /// <summary>
        /// Gets the list by parent id.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <returns></returns>
        public static List<TypeInfo> GetListByParentId(int parentId)
        {
            return dal.GetListByParentId(parentId, 0, "");
        }

        /// <summary>
        /// Gets the list by parent id.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <param name="typeid">The typeid.</param>
        /// <returns></returns>
        public static List<TypeInfo> GetListByParentId(int parentId, int typeid)
        {
            return dal.GetListByParentId(parentId, typeid, "");
        }

        /// <summary>
        /// Gets the list by parent id.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <param name="typeids">The typeids.</param>
        /// <returns></returns>
        public static List<TypeInfo> GetListByParentId(int parentId, string typeids)
        {
            return dal.GetListByParentId(parentId, 0, typeids);
        }

        /// <summary>
        /// 根据供应商ID获得一级产品类型列表
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <returns></returns>
        public static List<TypeInfo> getLevel1ListBySupplierId(int supplierId)
        {
            return TypeDataProvider.getLevel1ListBySupplierId(supplierId);
        }

        /// <summary>
        /// 根据供应商ID和一级产品类型ID获得二级产品类型列表
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <param name="level1Id">The level1 id.</param>
        /// <returns></returns>
        public static List<TypeInfo> getLevel2ListBySupplierIdAndLevel1ID(int supplierId, int level1Id)
        {
            return TypeDataProvider.getLevel2ListBySupplierIdAndLevel1ID(supplierId, level1Id);
        }

        /// <summary>
        /// 根据供应商ID和二级产品类型ID获得三级产品类型列表
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <param name="level2Id">The level2 id.</param>
        /// <returns></returns>
        public static List<TypeInfo> getLevel3ListBySupplierIdAndLevel2ID(int supplierId, int level2Id)
        {
            return TypeDataProvider.getLevel3ListBySupplierIdAndLevel2ID(supplierId, level2Id);
        }

        /// <summary>
        /// 获得同级Type列表
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <returns></returns>
        public static List<TypeInfo> GetListByTypeId(int typeId)
        {
            return dal.GetListByTypeId(typeId);
        }

        /// <summary>
        /// 模糊查询第三级类型
        /// </summary>
        /// <param name="likeKey">The like key.</param>
        /// <param name="isLike">if set to <c>true</c> [is like].</param>
        /// <returns></returns>
        public static List<TypeInfo> GetTypeListByLike(string likeKey, bool isLike)
        {
            return dal.GetTypeListByLike(likeKey, isLike);
        }

        //取得所有上海分公司的物料审核人
        /// <summary>
        /// Gets the SH auditor.
        /// </summary>
        /// <returns></returns>
        public static string getSHAuditor()
        {
            return TypeDataProvider.getSHAuditor();
        }

        //取得所有广州分公司的物料审核人
        /// <summary>
        /// Gets the GZ auditor.
        /// </summary>
        /// <returns></returns>
        public static string getGZAuditor()
        {
            return TypeDataProvider.getGZAuditor();
        }

        public static DataTable getAllPaymentUser()
        {
            return TypeDataProvider.getAllPaymentUser();
        }
        #endregion  成员方法


        /// <summary>
        /// 获取物料类别的子/父 ID 对照表。
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, int> GetTypeMappings()
        {
            return GetTypeMappings(null);
        }


        /// <summary>
        /// 获取物料类别的子/父 ID 对照表。
        /// </summary>
        /// <param name="childrenIds"></param>
        /// <returns></returns>
        public static Dictionary<int, int> GetTypeMappings(int[] childrenIds)
        {
            Dictionary<int, int> mappings = new Dictionary<int, int>();

            if (childrenIds == null)
            {

                using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader("select typeid, parentId from T_Type"))
                {
                    while (reader.Read())
                    {
                        mappings[reader.GetInt32(0)] = reader.GetInt32(1);
                    }
                }
                return mappings;
            }

            if (childrenIds.Length == 0)
                return mappings;

            StringBuilder sql = new StringBuilder("select typeid, parentId from T_Type where status = 1 and parentId in (").Append(childrenIds[0]);
            for (var i = 1; i < childrenIds.Length; i++)
            {
                sql.Append(",").Append(childrenIds[i]);
            }
            sql.Append(")");

            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                while (reader.Read())
                {
                    mappings[reader.GetInt32(0)] = reader.GetInt32(1);
                }
            }

            return mappings;
        }

        public static Dictionary<int, string> GetListLvl2()
        {
            Dictionary<int, string> d = new Dictionary<int, string>();

            var sql = "select typeid, typename from T_Type where typelevel = 2";
            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);

                    d[id] = name;
                }
            }

            return d;
        }
    }
}