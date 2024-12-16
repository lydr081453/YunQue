using System;
using System.Data;
using System.Collections.Generic;


namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类T_BuzzWatchWinService 的摘要说明。
    /// </summary>
    public class BaiduManager
    {
        private readonly ESP.Purchase.DataAccess.BaiduDataProvider dal = new ESP.Purchase.DataAccess.BaiduDataProvider();
        public BaiduManager()
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
        public int Add(ESP.Purchase.Entity.BaiduInfo model,string conn)
        {
            return dal.Add(model, conn);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Purchase.Entity.BaiduInfo model)
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
        public ESP.Purchase.Entity.BaiduInfo GetModel(int id)
        {
            return dal.GetModel(id);
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


        public DataSet GetListAdapter(string strWhere, int startRecord, int PageItem)
        {
            return dal.GetListAdapter(strWhere, startRecord, PageItem);
        }
    
        public int GetListCount(string strWhere)
        {
            return dal.GetListCount(strWhere);
        }

        public List<string> getAllUrlList()
        {
            return dal.getAllUrlList();
        }

        public DataTable getTitleList()
        {
            return dal.getTitleList();
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        public int AddWatch(ESP.Purchase.Entity.BaiduInfo model)
        {
            return dal.AddWatch(model);
        }

        public void UpdateWatch(ESP.Purchase.Entity.BaiduInfo model)
        {
            dal.UpdateWatch(model);
        }

        public DataSet GetWatchList(string strWhere)
        {
            return dal.GetWatchList(strWhere);
        }

        public int GetWatchListCount(string strWhere)
        {
            return dal.GetWatchListCount(strWhere);
        }

        public void DeleteWatchInfo(int id)
        {
            dal.DeleteWatchInfo(id);
        }

        public void DeleteWatchInfo(string sqlwhere)
        {
            dal.DeleteWatchInfo(sqlwhere);
        }

        public int AddSearchEngine(ESP.Purchase.Entity.BaiduInfo model)
        {
            return dal.AddSearchEngine(model);
        }

        public void UpdateSearchEngine(ESP.Purchase.Entity.BaiduInfo model)
        {
            dal.UpdateSearchEngine(model);
        }

        public DataSet GetSearchEngineList(string strWhere)
        {
            return dal.GetSearchEngineList(strWhere);
        }

        public int GetSearchEngineListCount(string strWhere)
        {
            return dal.GetSearchEngineListCount(strWhere);
        }
        

        public void DeleteSearchEngineInfo(int id)
        {
            dal.DeleteSearchEngineInfo(id);
        }

        public void DeleteSearchEngineInfo(string sqlWhere)
        {
            dal.DeleteSearchEngineInfo(sqlWhere);
        }

        public void DeleteRepeatWatchInfo()
        {
            try
            {
                dal.DeleteRepeatWatchInfo();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion  成员方法
    }
}

