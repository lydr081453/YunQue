using System;
using System.Data;
using System.Collections.Generic;


namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类T_BuzzWatchWinService 的摘要说明。
    /// </summary>
    public class sepArticleManager
    {
        private readonly ESP.Purchase.DataAccess.sepArticleDateProvider dal = new ESP.Purchase.DataAccess.sepArticleDateProvider();
        public sepArticleManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Purchase.Entity.sepArticleInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Purchase.Entity.sepArticleInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Purchase.Entity.sepArticleInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据集合
        /// </summary>
        public IList<ESP.Purchase.Entity.sepArticleInfo> GetList(string condition)
        {
            return dal.GetList(condition);
        }

        #endregion  成员方法
    }
}

