using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Administrative.BusinessLogic
{
    public class LogManager
    {
        private readonly ESP.Administrative.DataAccess.LogDataProvider dal = new ESP.Administrative.DataAccess.LogDataProvider();
        public LogManager()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 添加日志记录
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <returns>返回添加后的记录ID值</returns>
        public int Add(string content)
        {
            ESP.Administrative.Entity.LogInfo model = new ESP.Administrative.Entity.LogInfo();
            model.Content = "";
            if (!string.IsNullOrEmpty(content))
            {
                model.Content = content;
            }
            model.IsSuccess = true;
            model.Time = DateTime.Now;
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.LogInfo model)
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
        public ESP.Administrative.Entity.LogInfo GetModel(int ID)
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

