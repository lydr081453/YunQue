﻿using System.Data;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.BusinessLogic
{
    public static class OtherMediumForOrderManager
    {
        private static OtherMediumForOrderDataProvider dal = new OtherMediumForOrderDataProvider();
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(OtherMediumForOrderInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(OtherMediumForOrderInfo model)
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
        public static OtherMediumForOrderInfo GetModel(int id)
        {
            return dal.GetModel(id);
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
        /// 获得数据对象列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static IList<OtherMediumForOrderInfo> GetInfoList(string strWhere)
        {
            return dal.GetInfoList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }
        #endregion  成员方法
    }
}