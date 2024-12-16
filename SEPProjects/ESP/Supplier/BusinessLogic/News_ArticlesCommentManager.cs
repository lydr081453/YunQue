using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.DataAccess;
using System.Data.SqlClient;
using Supply.Entity;

namespace Supply.BusinessLogic
{
    public class ArticlesCommentManager
    {
        private readonly static ArticlesCommentProvider dal = new ArticlesCommentProvider();

        public static int Add(ArticlesComment model)
        {
            return dal.Add(model);
        }

        public static void Update(ArticlesComment model)
        {
            dal.Update(model);
        }

        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public static ArticlesComment GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public static List<ArticlesComment> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }

        public static List<ArticlesComment> GetListByArticleID(int ArticleID)
        {
            string strWhere = " and ArticleID=" + ArticleID;
            return dal.GetList(strWhere, new List<SqlParameter>());
        }

        public static int GetListCountByArticeleID(int articleId)
        {
            return GetListCount(" and ArticleID=" + articleId, new List<SqlParameter>());
        }

        public static int GetListCount(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetListCount(strWhere, parms);
        }

        public static List<ArticlesComment> GetList(int pageSize, int pageIndex, string strWhere, string orderBy, List<SqlParameter> parms)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, orderBy, parms);
        }
    }
}
