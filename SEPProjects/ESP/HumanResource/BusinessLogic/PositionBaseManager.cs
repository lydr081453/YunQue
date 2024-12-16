using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;

namespace ESP.HumanResource.BusinessLogic
{
    public class PositionBaseManager
    {
        public readonly static PositionBaseDataProvider Provider = new PositionBaseDataProvider();

        public static int Add(PositionBaseInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(PositionBaseInfo model)
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

        public static PositionBaseInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static List<PositionBaseInfo> GetList(string strWhere,List<SqlParameter> parms)
        {
            return Provider.GetList(strWhere,parms);
        }

        public static List<PositionBaseInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<PositionBaseInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
    }
}
