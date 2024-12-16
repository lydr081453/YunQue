using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class ITAssetsManager
    {

        private static ESP.Finance.IDataAccess.IITAssetsProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IITAssetsProvider>.Instance; } }
        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ITAssetsInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(ESP.Finance.Entity.ITAssetsInfo model)
        {
            return DataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int id)
        {
            return DataProvider.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ITAssetsInfo GetModel(int id)
        {
            return DataProvider.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ITAssetsInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        #endregion  成员方法


        public static string GetStatus(int status)
        {
            string ret = string.Empty;
            switch (status)
            {
                case 0:
                    ret = "已删除";
                    break;
                case 1:
                    ret = "在库";
                    break;
                case 2:
                    ret = "领用待确认";
                    break;
                case 3:
                    ret = "已领用";
                    break;
                case 4:
                    ret = "报废待财务确认";
                    break;
                case 5:
                    ret = "已报废";
                    break;
                case 6:
                    ret = "报损";
                    break;
                case 7:
                    ret = "报废待组长确认";
                    break;
            }
            return ret;
        }
    }
}
