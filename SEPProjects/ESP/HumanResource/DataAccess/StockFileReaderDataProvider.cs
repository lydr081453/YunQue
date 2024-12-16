using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data;
using System.Data.SqlClient;
using ESP.Administrative.Common;
using System.Security.Policy;
using Microsoft.Office.Interop.Word;
using System.Collections;

namespace ESP.HumanResource.DataAccess
{
    /// <summary>
    /// 培训活动数据类
    /// </summary>
    public class StockFileReaderDataProvider
    {
        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.StockFileReaderInfo> GetList(string sqlWhere)
        {
            List<StockFileReaderInfo> list = new List<StockFileReaderInfo>();
            string sql = "select * from SEP_StockFileReader";
            if (!string.IsNullOrEmpty(sqlWhere))
                sql += sqlWhere;
            sql += " order by userid desc";
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            StockFileReaderInfo StockFileReaderInfo = null;
            foreach (DataRow row in drs)
            {
                StockFileReaderInfo = new StockFileReaderInfo
                {
                    Id = int.Parse(row["Id"].ToString()),
                    UserId = int.Parse(row["UserId"].ToString()),
                    FileIndex = int.Parse(row["FileIndex"].ToString()),
                    ReadTime = DateTime.Parse(row["ReadTime"].ToString()),
                };
                list.Add(StockFileReaderInfo);
            }
            return list;
        }



        /// <summary>
        /// 添加培训活动
        /// </summary>
        /// <param name="StockFileReaderInfo">要添加的活动对象</param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.StockFileReaderInfo StockFileReaderInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_StockFileReader(");
            strSql.Append("userid,fileIndex,readTime)");
            strSql.Append(" values (");
            strSql.Append("@userid,@fileIndex,@readTime)");
            SqlParameter[] parameters = {					
					new SqlParameter("@userid", SqlDbType.Int),
					new SqlParameter("@fileIndex", SqlDbType.Int),
					new SqlParameter("@readTime", SqlDbType.DateTime)};
            parameters[0].Value = StockFileReaderInfo.UserId;
            parameters[1].Value = StockFileReaderInfo.FileIndex;
            parameters[2].Value = StockFileReaderInfo.ReadTime;

            object obj = ESP.HumanResource.Common.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return 1;
            }
            return 0;
        }

    }
}
