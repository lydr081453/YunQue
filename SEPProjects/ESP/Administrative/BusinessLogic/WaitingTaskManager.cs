using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data;

namespace ESP.Administrative.BusinessLogic
{
    public class WaitingTaskManager
    {
        private readonly WaitingTaskDataProvider dal = new WaitingTaskDataProvider();
        public WaitingTaskManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WaitingTaskInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(WaitingTaskInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WaitingTaskInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }
        #endregion  成员方法
    }
}