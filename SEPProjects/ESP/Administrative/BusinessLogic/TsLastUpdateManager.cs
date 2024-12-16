using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;

namespace ESP.Administrative.BusinessLogic
{
    public class TsLastUpdateManager
    {
        public readonly static TsLastUpdateDataProvider Provider = new TsLastUpdateDataProvider();

        public static int Add(TsLastUpdateInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(TsLastUpdateInfo model)
        {
            return Provider.Update(model);
        }

        public static bool Delete(int Id)
        {
            return Provider.Delete(Id);
        }

        public static bool DeleteList(string Idlist)
        {
            return Provider.DeleteList(Idlist);
        }

        public static TsLastUpdateInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static List<TsLastUpdateInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static List<TsLastUpdateInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<TsLastUpdateInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 获得最新的更新时间
        /// </summary>
        public static TsLastUpdateInfo GetMaxModelByUserId(int userId)
        {
            return Provider.GetMaxModelByUserId(userId);
        }

    }
}
