using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Entity;
using ESP.Purchase.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Purchase.BusinessLogic
{
    public static class BlackListManager
    {
        private static BlackListDataProvider dal = new BlackListDataProvider();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(BlackListInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(BlackListInfo model)
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
        public static BlackListInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static IList<BlackListInfo> GetList(string strSql,List<SqlParameter> paramList)
        {
            return dal.GetList(strSql,paramList);
        }

    }
}
