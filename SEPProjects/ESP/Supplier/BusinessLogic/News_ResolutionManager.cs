using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.DataAccess;
using System.Data.SqlClient;
using Supply.Entity;

namespace Supply.BusinessLogic
{
    public class ResolutionManager
    {
        private readonly static ResolutionProvider dal = new ResolutionProvider();

        public static int Add(Resolution model)
        {
            return dal.Add(model);
        }

        public static void Update(Resolution model)
        {
            dal.Update(model);
        }

        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public static Resolution GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public static List<Resolution> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(0,strWhere, parms);
        }

        public static List<Resolution> GetList(int Top,string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(Top,strWhere, parms);
        }

        public static int GetListCount(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetListCount(strWhere, parms);
        }

        public static int GetChildCount(int parentId)
        {
            return dal.GetChildCount(parentId);
        }

        public static List<Resolution> GetList(int pageSize, int pageIndex, string strWhere, string orderBy, List<SqlParameter> parms)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, orderBy, parms);
        }
    }
}
