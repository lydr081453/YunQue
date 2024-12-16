using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    public class PositionLevelsManager
    {
        public readonly static PositionLevelsDataProvider Provider = new PositionLevelsDataProvider();

        public static int Add(PositionLevelsInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(PositionLevelsInfo model)
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

        public static PositionLevelsInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static List<PositionLevelsInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static List<PositionLevelsInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<PositionLevelsInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
    }
}
