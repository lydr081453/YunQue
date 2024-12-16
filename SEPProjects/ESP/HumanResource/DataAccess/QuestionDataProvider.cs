using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data;

namespace ESP.HumanResource.DataAccess
{
    /// <summary>
    /// 问题表数据类
    /// </summary>
    public class QuestionDataProvider
    {
        /// <summary>
        /// 根据titleid获取问题集合
        /// </summary>
        /// <returns></returns>
        public List<QuestionInfo> GetList(int titleId)
        {
            List<QuestionInfo> list = new List<QuestionInfo>();
            string sql = "select * from T_Question where Titleid="+titleId;
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            QuestionInfo questionInfo = null;
            foreach (DataRow row in drs)
            {
                questionInfo = new QuestionInfo
                {
                    Id = int.Parse(row["Id"].ToString()),
                    Title = row["Title"].ToString(),
                    TitleId=int.Parse(row["TitleId"].ToString()),
                    Subject = row["Subject"].ToString()
                };
                list.Add(questionInfo);
            }
            return list;
        }

        /// <summary>
        /// 获取所以不重复的问题Title集合
        /// </summary>
        /// <returns></returns>
        public List<QuestionInfo> GetListTitle()
        {
            List<QuestionInfo> list = new List<QuestionInfo>();
            string sql = "select distinct titleid,title from t_question where Status=1 order by titleid";
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            QuestionInfo info = null;
            foreach (DataRow row in drs)
            {
                info = new QuestionInfo();
                info.Title = row["Title"].ToString();
                info.TitleId = int.Parse(row["TitleId"].ToString());
                list.Add(info);
            }
            return list;
        }
    }
}
