using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;

namespace ESP.Administrative.BusinessLogic
{
    public class TimeSheetCategoryManager
    {
        public readonly static TimeSheetCategoryDataProvider Provider = new TimeSheetCategoryDataProvider();

        public static int Add(TimeSheetCategoryInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(TimeSheetCategoryInfo model)
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

        public static TimeSheetCategoryInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static List<TimeSheetCategoryInfo> GetList(int parentId)
        {
            return Provider.GetList(parentId);
        }

        public static List<TimeSheetCategoryInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static List<TimeSheetCategoryInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<TimeSheetCategoryInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
    }
}
