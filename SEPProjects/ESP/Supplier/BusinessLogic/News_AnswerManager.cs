using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.DataAccess;
using System.Data.SqlClient;
using Supply.Entity;

namespace Supply.BusinessLogic
{
    public class AnswerManager
    {
        private readonly static AnswerProvider dal = new AnswerProvider();

        public static int Add(Answer model)
        {
            return dal.Add(model);
        }

        public static void Update(Answer model)
        {
            dal.Update(model);
        }

        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public static Answer GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public static List<Answer> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }

        public static List<Answer> GetListByQuestionId(int questionId)
        {
            string str = " and questionId=" + questionId;
            return dal.GetList(str, new List<SqlParameter>());
        }

        public static string GetCountByQuestionId(int questionId)
        {
            return dal.GetCountByQuestionId(questionId);
        }
    }
}
