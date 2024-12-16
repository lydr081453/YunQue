using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Data.SqlClient;


namespace ESP.Purchase.BusinessLogic
{
    public class OrderMsgManager
    {
        private static readonly OrderMsgDataProvider dal = new OrderMsgDataProvider();
        public OrderMsgManager()
        { }

        #region  成员方法

        /// <summary>
        /// 添加一条数据
        /// </summary>
        public static int Add(OrderMsg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(OrderMsg model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int Id)
        {
            dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static OrderMsg GetModel(int Id)
        {
            return dal.GetModel(Id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static OrderMsg GetModelByOrderId(int orderid)
        {
            return dal.GetModelByOrderId(orderid);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static IList<OrderMsg> GetList(string strWhere, SqlParameter[] parameters)
        {
            return dal.GetList(strWhere, parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        public static IList<OrderMsg> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<OrderMsg>(GetAllList());
        }
        #endregion  成员方法
    }
}
