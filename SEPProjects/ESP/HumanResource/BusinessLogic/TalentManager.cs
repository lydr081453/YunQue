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
    public class TalentManager
    {
        private ESP.HumanResource.DataAccess.TalentDataProvider dataProvider = new ESP.HumanResource.DataAccess.TalentDataProvider();
        /// <summary>
        /// 将答题结果保存到数据库中
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.TalentInfo info)
        {
            return dataProvider.Add(info);
        }

        public bool Update(TalentInfo model)
        {
            return dataProvider.Update(model);
        }

        public bool Delete(int Id)
        {
            return dataProvider.Delete(Id);
        }
        public TalentInfo GetModel(int Id)
        {
            return dataProvider.GetModel(Id);
        }

        public IList<TalentInfo> GetList(string strWhere)
        {
            return dataProvider.GetList(strWhere);
        }
    }
}
