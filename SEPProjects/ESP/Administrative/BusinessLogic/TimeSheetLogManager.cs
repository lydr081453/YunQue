using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;

namespace ESP.Administrative.BusinessLogic
{
    public class TimeSheetLogManager
    {
        public readonly static TimeSheetLogDataProvider Provider = new TimeSheetLogDataProvider();

        public static int Add(TimeSheetLogInfo model,System.Data.SqlClient.SqlTransaction trans)
        {
            return Provider.Add(model, trans);
        }

        public static bool Update(TimeSheetLogInfo model)
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

        public static TimeSheetLogInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static List<TimeSheetLogInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static List<TimeSheetLogInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<TimeSheetLogInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
    }
}
