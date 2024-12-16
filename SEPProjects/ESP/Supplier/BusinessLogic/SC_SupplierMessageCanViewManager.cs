﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
   public class SC_SupplierMessageCanViewManager
    {
        private static readonly SC_SupplierMessageCanViewDataProvider dal = new SC_SupplierMessageCanViewDataProvider();
                public SC_SupplierMessageCanViewManager()
        { }

        #region  成员方法
        /// <summary>
        /// 添加一条数据
        /// </summary>
        public static int Add(SC_SupplierMessageCanView model)
        {
            return dal.Add(model);
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
        public static SC_SupplierMessageCanView GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static IList<SC_SupplierMessageCanView> GetList(string strWhere, SqlParameter[] parameters)
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

        public static IList<SC_SupplierMessageCanView> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierMessageCanView>(GetAllList());
        }
        #endregion  成员方法
    }
}