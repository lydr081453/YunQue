using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
namespace ESP.Finance.BusinessLogic
{
  public static   class TimingLogManager
    {
      private static ESP.Finance.IDataAccess.ITimingLogProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ITimingLogProvider>.Instance; } }
     
        public static bool Exists(int ID)
        {
            return DataProvider.Exists(ID);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(TimingLogInfo model)
        {
            return DataProvider.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(TimingLogInfo model)
        {
            return DataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int ID)
        {
            return DataProvider.Delete(ID);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static TimingLogInfo GetModel(int ID)
        {
            return DataProvider.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ESP.Finance.Entity.TimingLogInfo> GetList(string strWhere)
        {
            return DataProvider.GetList(strWhere);
        }
    }
}
