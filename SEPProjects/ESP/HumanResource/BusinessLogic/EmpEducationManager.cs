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
    public class EmpEducationManager
    {
        private static EmpEducationProvider dal = new EmpEducationProvider();
        public EmpEducationManager()
        { }
        public static int Add(ESP.HumanResource.Entity.EmpEducationInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.EmpEducationInfo model)
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
        public static ESP.HumanResource.Entity.EmpEducationInfo GetModel(int memberid)
        {
            return dal.GetModel(memberid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.HumanResource.Entity.EmpEducationInfo> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
    }
}
