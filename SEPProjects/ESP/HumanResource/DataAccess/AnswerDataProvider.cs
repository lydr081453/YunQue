using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ESP.HumanResource.DataAccess
{
    /// <summary>
    /// 答案表数据类
    /// </summary>
    public class AnswerDataProvider
    {
        /// <summary>
        /// 将答题结果保存到数据库中
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.AnswerInfo info)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Answer(");
            strSql.Append("QuestionId,Score,UserId,AnswerTime,Ip,AnswerText)");
            strSql.Append(" values (");
            strSql.Append("@QuestionId,@Score,@UserId,@AnswerTime,@Ip,@AnswerText)");
            SqlParameter[] parameters = {					
					new SqlParameter("@QuestionId", SqlDbType.Int),
					new SqlParameter("@Score", SqlDbType.Int),
					new SqlParameter("@UserId", SqlDbType.Int),
					new SqlParameter("@AnswerTime", SqlDbType.DateTime),
					new SqlParameter("@Ip", SqlDbType.NVarChar),
                    new SqlParameter("@AnswerText", SqlDbType.NVarChar,4000)
                                        };
            parameters[0].Value = info.QuestionId;
            parameters[1].Value = info.Score;
            parameters[2].Value = info.UserId;
            parameters[3].Value = info.AnswerTime;
            parameters[4].Value = info.Ip;
            parameters[5].Value = info.AnswerText;
            object obj = ESP.HumanResource.Common.DbHelperSQL.GetSingle(strSql.ToString(),parameters);
            if (obj != null)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 判断当前用户是否参与过答题
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public bool IsAnswer(int userId)
        {
            string sql = "select * from T_Answer where userId=" + userId;
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            if (ds.Tables[0].Rows.Count>0)
                return true;
            return false;
        }
    }
}
