using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    public class InsuranceBaseInfoManager
    {
        private static ESP.HumanResource.DataAccess.InsuranceBaseInfoDataProvider dataProvider = new ESP.HumanResource.DataAccess.InsuranceBaseInfoDataProvider();

        public static InsuranceBaseInfo GetModel(string cityName)
        {
            return dataProvider.GetModel(cityName);
        }

        /// <summary>
        /// 获取社保缴费上下线
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="insuranceType"></param>
        /// <returns></returns>
        public static List<InsuranceBase> GetBaseList(string cityName)
        {
            return dataProvider.GetBaseList(cityName);
        }
    }
}
