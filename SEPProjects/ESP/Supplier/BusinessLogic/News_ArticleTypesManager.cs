using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.DataAccess;
using System.Data.SqlClient;
using Supply.Entity;

namespace Supply.BusinessLogic
{
    public class ArticleTypesManager
    {
        private readonly static ArticleTypesProvider dal = new ArticleTypesProvider();

        public static int Add(ArticleTypes model)
        {
            return dal.Add(model);
        }

        public static void Update(ArticleTypes model)
        {
            dal.Update(model);
        }

        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public static ArticleTypes GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public static List<ArticleTypes> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }
    }
}
