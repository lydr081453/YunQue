using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;
using System.Data;

namespace ESP.Administrative.BusinessLogic
{
    public class OverTimeRestManager
    {
        private OverTimeRestDataProvider dal = new OverTimeRestDataProvider();
        public OverTimeRestManager()
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
        public int Add(OverTimeRestInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(OverTimeRestInfo model)
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
        public OverTimeRestInfo GetModel(int ID)
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

        /// <summary>
        /// 通过调休单编号删除相对应的调休OT关联时间信息
        /// </summary>
        /// <param name="offTuneId">调休单编号</param>
        public void DeleteOffTuneInfos(int offTuneId)
        {
            dal.DeleteOffTuneInfos(offTuneId);
        }

        #endregion  成员方法
    }
}