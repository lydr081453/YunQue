using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.BusinessLogic
{
    public    class HeadAccountLogManager
    {

        private ESP.HumanResource.DataAccess.HeadAccountLogProvider dataProvider = new ESP.HumanResource.DataAccess.HeadAccountLogProvider();
        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.HeadAccountLogInfo> GetList(string sqlWhere)
        {
            return dataProvider.GetModelList(sqlWhere);
        }


        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.HeadAccountLogInfo GetModel(int id)
        {
            return dataProvider.GetModel(id);
        }

        /// <summary>
        /// 添加培训活动
        /// </summary>
        /// <param name="HeadAccountLogInfo">要添加的活动对象</param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.HeadAccountLogInfo model)
        {
            return dataProvider.Add(model);
        }

        public int Add(ESP.HumanResource.Entity.HeadAccountLogInfo model,System.Data.SqlClient.SqlTransaction trans)
        {
            return dataProvider.Add(model,trans);
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
        public int Update(ESP.HumanResource.Entity.HeadAccountLogInfo model)
        {
            return dataProvider.Update(model);
        }
    }
}
