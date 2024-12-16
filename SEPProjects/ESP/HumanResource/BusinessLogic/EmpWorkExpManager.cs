using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class EmpWorkExpManager
    {
         private static EmpWorkExpProvider dal = new EmpWorkExpProvider();
         public EmpWorkExpManager()
        { }
        public static  int Add(ESP.HumanResource.Entity.EmpWorkExpInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.EmpWorkExpInfo model)
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
        public static ESP.HumanResource.Entity.EmpWorkExpInfo GetModel(int memberid)
        {
            return dal.GetModel(memberid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.HumanResource.Entity.EmpWorkExpInfo> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
    }
}
