using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data;

namespace ESP.HumanResource.DataAccess
{
    /// <summary>
    /// 分数规则表数据类
    /// </summary>
    public class ScoreStandardDataProvider
    {
        /// <summary>
        /// 获取分数规则集合
        /// </summary>
        public List<ScoreStandardInfo> GetList()
        {
            List<ScoreStandardInfo> list = new List<ScoreStandardInfo>();
            string sql = "select * from T_ScoreStandard";
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            ScoreStandardInfo scoreStandardInfo = null;
            foreach (DataRow row in drs)
            {
                scoreStandardInfo = new ScoreStandardInfo
                {
                    Id = int.Parse(row["Id"].ToString()),
                    ScoreDesc = row["ScoreDesc"].ToString(),
                    Score = row["Score"].ToString()
                };
                list.Add(scoreStandardInfo);
            }
            return list;
        }
    }
}
