using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    public class RecommendManager
    {
        private static readonly ESP.HumanResource.DataAccess.RecommendDataProvider dataProvider = new ESP.HumanResource.DataAccess.RecommendDataProvider();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public static IList<RecommendInfo> GetList(string top)
        {
            return dataProvider.GetList(top);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public static IList<RecommendInfo> GetModels(string where)
        {
            return dataProvider.GetModels(where);
        }
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RecommendInfo Get(int id)
        {
            return dataProvider.Get(id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="recommendInfo"></param>
        /// <returns></returns>
        public static int Update(RecommendInfo recommendInfo)
        {
            return dataProvider.Update(recommendInfo);
        }
        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public static int GetCount(string userCode)
        {
            return dataProvider.GetCount(userCode);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="recommendInfo"></param>
        /// <returns></returns>
        public static int Add(RecommendInfo recommendInfo)
        {
            return dataProvider.Add(recommendInfo);

        }
    }
}
