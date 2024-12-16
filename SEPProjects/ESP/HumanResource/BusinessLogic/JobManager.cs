using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.BusinessLogic
{
    public class JobManager
    {
        private ESP.HumanResource.DataAccess.JobDataProvider dataProvider = new ESP.HumanResource.DataAccess.JobDataProvider();
        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.JobInfo> GetList(string sqlWhere)
        {
            return dataProvider.GetList(sqlWhere);
        }

        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.JobInfo GetModel(int id)
        {
            return dataProvider.GetModel(id);
        }

        /// <summary>
        /// 添加培训活动
        /// </summary>
        /// <param name="JobInfo">要添加的活动对象</param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.JobInfo jobInfo)
        {
            return dataProvider.Add(jobInfo);
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
        public int Update(ESP.HumanResource.Entity.JobInfo jobInfo)
        {
            return dataProvider.Update(jobInfo);
        }
    }
}
