using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.BusinessLogic
{
    /// <summary>
    /// 答案dll
    /// </summary>
    public class AnswerManager
    {
        private ESP.HumanResource.DataAccess.AnswerDataProvider dataProvider = new ESP.HumanResource.DataAccess.AnswerDataProvider();
        /// <summary>
        /// 将答题结果保存到数据库中
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.AnswerInfo info)
        {
            return dataProvider.Add(info);
        }

        /// <summary>
        /// 判断当前用户是否参与过答题
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public bool IsAnswer(int userId)
        {
            return dataProvider.IsAnswer(userId);
        }
    }
}
