using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.DataAccess;
using System.Data.SqlClient;
using Supply.Entity;

namespace Supply.BusinessLogic
{
    public class QuestionManager
    {
        private readonly static QuestionProvider dal = new QuestionProvider();

        public static int Add(Question model)
        {
            return dal.Add(model);
        }

        public static void Update(Question model)
        {
            dal.Update(model);
        }

        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public static Question GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public static List<Question> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }
    }
}
