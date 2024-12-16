using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    /// <summary>
    /// 问题dll
    /// </summary>
    public class QuestionManager
    {
        private QuestionDataProvider dataProvider = new QuestionDataProvider();
        /// <summary>
        /// 根据titleid获取问题集合
        /// </summary>
        /// <returns></returns>
        public List<QuestionInfo> GetList(int titleId)
        {
            return dataProvider.GetList(titleId);
        }

        /// <summary>
        /// 获取所以不重复的问题Title集合
        /// </summary>
        /// <returns></returns>
        public List<QuestionInfo> GetListTitle()
        {
            return dataProvider.GetListTitle();
        }
    }
}
