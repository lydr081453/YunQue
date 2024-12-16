using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.DataAccess;
using System.Data;
using System.Data.SqlClient;
using Supply.Entity;

namespace Supply.BusinessLogic
{
    public class ArticlesSupplierUsersManager
    {
        private readonly static ArticlesSupplierUsersProvider dal = new ArticlesSupplierUsersProvider();

        public static int Add(ArticlesSupplierUsers model)
        {
            return dal.Add(model);
        }

        public static void Update(ArticlesSupplierUsers model)
        {
            dal.Update(model);
        }

        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public static ArticlesSupplierUsers GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public static List<ArticlesSupplierUsers> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(0, strWhere, parms, "");
        }

        public static List<ArticlesSupplierUsers> GetList(int Top, string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(Top, strWhere, parms,"");
        }

        public static List<ArticlesSupplierUsers> GetList(string strWhere, List<SqlParameter> parms, string orderBy)
        {
            return dal.GetList(0, strWhere, parms, orderBy);
        }

        public static List<ArticlesSupplierUsers> GetList(int Top, string strWhere, List<SqlParameter> parms, string orderBy)
        {
            return dal.GetList(Top, strWhere, parms, orderBy);
        }

        public static int GetListCount(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetListCount(strWhere, parms);
        }

        public static List<ArticlesSupplierUsers> GetList(int pageSize, int pageIndex, string strWhere, string orderBy, List<SqlParameter> parms)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, orderBy, parms);
        }
    }
}
