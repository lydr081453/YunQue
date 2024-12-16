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
    public class SC_AdviceManager
    {
        private static readonly SC_AdviceDataProvider dal = new SC_AdviceDataProvider();
        public SC_AdviceManager()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SC_Advice model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SC_Advice model)
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
        public static SC_Advice GetModel(int Id)
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

        public static List<SC_Advice> GetList(string strWhere, SqlParameter[] parameters)
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

        public static List<SC_Advice> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Advice>(GetAllList());
        }
        #endregion  成员方法
    }
}