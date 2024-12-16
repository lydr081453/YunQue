using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.DataAccess;
using Supply.Entity;
using System.Data;

namespace Supply.BusinessLogic
{
    public class LogsManager
    {
        private readonly static LogsProvider dal = new LogsProvider();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Logs model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(Logs model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Logs GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
    }
}
