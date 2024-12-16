using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.BusinessLogic
{
    /// <summary>
    /// 培训活动dll
    /// </summary>
    public class ActityManager
    {
        private ESP.HumanResource.DataAccess.ActityDataProvider dataProvider = new ESP.HumanResource.DataAccess.ActityDataProvider();
        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.ActityInfo> GetList(string sqlWhere)
        {
            return dataProvider.GetList(sqlWhere);
        }

        /// <summary>
        /// 获取最近一次将要开始的培训活动
        /// </summary>
        /// <returns></returns>
        public ESP.HumanResource.Entity.ActityInfo GetModel()
        {
            return dataProvider.GetModel();
        }

        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.ActityInfo GetModel(int id)
        {
            return dataProvider.GetModel(id);
        }

        /// <summary>
        /// 添加培训活动
        /// </summary>
        /// <param name="actityInfo">要添加的活动对象</param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.ActityInfo actityInfo)
        {
            return dataProvider.Add(actityInfo);
        }

        /// <summary>
        /// 根据id删除活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            return dataProvider.Delete(id);
        }

        /// <summary>
        /// 修改活动
        /// </summary>
        public int Update(ESP.HumanResource.Entity.ActityInfo actityInfo)
        {
            return dataProvider.Update(actityInfo);
        }
    }
}
