using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    /// <summary>
    /// 分数规则dll
    /// </summary>
    public class ScoreStandardManager
    {
        private ScoreStandardDataProvider dataProvider = new ScoreStandardDataProvider();
        /// <summary>
        /// 获取分数规则集合
        /// </summary>
        public List<ScoreStandardInfo> GetList()
        {
            return dataProvider.GetList();
        }
    }
}
