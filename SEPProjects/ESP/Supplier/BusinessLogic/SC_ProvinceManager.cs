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
    public class SC_ProvinceManager
    {
        private readonly SC_ProvinceDataProvider dal = new SC_ProvinceDataProvider();
        public SC_ProvinceManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Province model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_Province model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ProvinceId)
        {
            dal.Delete(ProvinceId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Province GetModel(int ProvinceId)
        {
            return dal.GetModel(ProvinceId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public List<SC_Province> GetList(string strWhere, SqlParameter[] parameters)
        {
            return dal.GetList(strWhere, parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  成员方法
    }
}