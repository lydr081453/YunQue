using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;

namespace ESP.HumanResource.BusinessLogic
{
    public class PositionLogManager
    {
        public readonly static PositionLogDataProvider Provider = new PositionLogDataProvider();

        public static int Add(PositionLogInfo model,SqlTransaction trans)
        {
            return Provider.Add(model,trans);
        }


        public static int Add(PositionLogInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(PositionLogInfo model)
        {
            return Provider.Update(model);
        }

        public static bool Update(PositionLogInfo model,SqlTransaction trans)
        {
            return Provider.Update(model,trans);
        }

        public static bool Delete(int Id)
        {
            return Provider.Delete(Id);
        }

        public static bool DeleteList(string Idlist)
        {
            return Provider.DeleteList(Idlist);
        }

        public static PositionLogInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static PositionLogInfo GetModel(int UserId, int DepartmentPositionId )
        {
            return Provider.GetModel(UserId, DepartmentPositionId);
        }

        public static List<PositionLogInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static List<PositionLogInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<PositionLogInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
    }
}
