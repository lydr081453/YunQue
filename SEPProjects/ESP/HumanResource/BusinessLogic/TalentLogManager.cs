using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    /// <summary>
    /// 答案dll
    /// </summary>
    public class TalentLogManager
    {
        private ESP.HumanResource.DataAccess.TalentLogProvider dataProvider = new ESP.HumanResource.DataAccess.TalentLogProvider();
        /// <summary>
        /// 将答题结果保存到数据库中
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.TalentLogInfo info)
        {
            return dataProvider.Add(info);
        }


        public bool Delete(int Id)
        {
            return dataProvider.Delete(Id);
        }
        public TalentLogInfo GetModel(int Id)
        {
            return dataProvider.GetModel(Id);
        }

        public IList<TalentLogInfo> GetList(string strWhere)
        {
            return dataProvider.GetList(strWhere);
        }

    }
}
