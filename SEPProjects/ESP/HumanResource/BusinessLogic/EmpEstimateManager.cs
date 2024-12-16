﻿using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class EmpEstimateManager
    {
        private static EmpEstimateProvider dal = new EmpEstimateProvider();
          public EmpEstimateManager()
        { }
        public static int Add(ESP.HumanResource.Entity.EmpEstimateInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.EmpEstimateInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int memberid)
        {
            dal.Delete(memberid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.HumanResource.Entity.EmpEstimateInfo GetModel(int memberid)
        {
            return dal.GetModel(memberid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.HumanResource.Entity.EmpEstimateInfo> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
    }
}
