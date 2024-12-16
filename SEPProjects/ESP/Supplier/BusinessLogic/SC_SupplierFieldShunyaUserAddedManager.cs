using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_SupplierFieldShunyaUserAddedManager
    {
        private static readonly SC_SupplierFieldShunyaUserAddedDataProvider dal = new SC_SupplierFieldShunyaUserAddedDataProvider();
        public SC_SupplierFieldShunyaUserAddedManager()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        //public static int Add(SC_SupplierFieldShunyaUserAdded model)
        //{
        //    return dal.Add(model);
        //}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SC_SupplierFieldShunyaUserAdded model)
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
        public static SC_SupplierFieldShunyaUserAdded GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SC_SupplierFieldShunyaUserAdded> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<SC_SupplierFieldShunyaUserAdded> GetAllList()
        {
            return dal.GetList("");
        }
        #endregion  成员方法
    }
}
