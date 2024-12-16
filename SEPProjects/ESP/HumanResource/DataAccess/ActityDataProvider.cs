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
    public class ActityDataProvider
    {
        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.ActityInfo> GetList(string sqlWhere)
        {
            List<ActityInfo> list = new List<ActityInfo>();
            string sql = "select * from T_Actity";
            if (!string.IsNullOrEmpty(sqlWhere))
                sql += sqlWhere;
            sql += " order by ActityTime desc";
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            ActityInfo actityInfo = null;
            foreach (DataRow row in drs)
            {
                actityInfo = new ActityInfo
                {
                    Id = int.Parse(row["Id"].ToString()),
                    ActityTitle = row["ActityTitle"].ToString(),
                    ActityContent = row["ActityContent"].ToString(),
                    ActityTime = DateTime.Parse(row["ActityTime"].ToString()),
                    Lecturer = row["Lecturer"].ToString()
                };
                list.Add(actityInfo);
            }
            return list;
        }

        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.ActityInfo GetModel(int id)
        {
            string sql = "select * from T_Actity where Id=" + id;
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            ActityInfo actityInfo = null;
            foreach (DataRow row in drs)
            {
                actityInfo = new ActityInfo
                {
                    Id = int.Parse(row["Id"].ToString()),
                    ActityTitle = row["ActityTitle"].ToString(),
                    ActityContent = row["ActityContent"].ToString(),
                    ActityTime = DateTime.Parse(row["ActityTime"].ToString()),
                    Lecturer = row["Lecturer"].ToString()
                };
            }
            return actityInfo;
        }

        /// <summary>
        /// 获取最近一次将要开始的培训活动
        /// </summary>
        /// <returns></returns>
        public ESP.HumanResource.Entity.ActityInfo GetModel()
        {
            string sql = "select * from T_Actity where ActityTime>getdate()";
            sql += " order by ActityTime asc";
            List<ActityInfo> list = new List<ActityInfo>();
           // List<string> diff_id = new List<string>();
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            ActityInfo actityInfo = null;
            foreach (DataRow row in drs)
            {
                actityInfo = new ActityInfo
                {
                    Id = int.Parse(row["Id"].ToString()),
                    ActityTitle = row["ActityTitle"].ToString(),
                    ActityContent = row["ActityContent"].ToString(),
                    ActityTime = DateTime.Parse(row["ActityTime"].ToString()),
                    Lecturer = row["Lecturer"].ToString()
                };
                list.Add(actityInfo);
            }
            if (list.Count > 0)
            {
                return list.First<ActityInfo>();
                //if (list.Count == 1)
                //{
                //    actityInfo = list.First<ActityInfo>();
                //    return actityInfo;
                //}
                //else
                //{
                //    foreach (ActityInfo item in list)
                //    {
                //        int dateDiff = DateDiff(item.ActityTime);
                //        diff_id.Add(dateDiff + "/" + item.Id);
                //    }
                //    int resultId = GetModelId(diff_id);
                //    return GetModel(resultId);
                //}
            }
            return null;
        }

        /// <summary>
        /// 获取最近一次将要培训的活动id
        /// </summary>
        /// <param name="list">list中的每一个元素是所有将要培训的活动时间与当前时间的时间差+“/”+活动id</param>
        /// <returns>最小时间差的id值</returns>
        private int GetModelId(List<string> list)
        {
            int result = 0;
            List<int> ids = new List<int>();
            foreach (string item in list)
            {
                ids.Add(int.Parse(item.Substring(0, item.IndexOf('/'))));
            }
            int sortResult = GetIdBySort(ids);
            foreach (string item in list)
            {
                if (item.Contains(sortResult + ""))
                {
                    result = int.Parse(item.Substring(item.IndexOf('/') + 1));
                }
            }
            return result;
        }

        /// <summary>
        /// 排序集合，并获取最小时间差
        /// </summary>
        /// <param name="ids">所以将要培训的活动与当前时间的时间差的集合</param>
        /// <returns></returns>
        private int GetIdBySort(List<int> ids)
        {
            if (ids.Count < 3)
            {
                int first = ids.First<int>();
                int last = ids.Last<int>();
                return first > last ? last : first;
            }
            else
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    for (int j = 1; j < ids.Count - 1; j++)
                    {
                        if (ids[i] >= ids[j])
                        {
                            int temp = 0;
                            temp = ids[i];
                            ids[i] = ids[j];
                            ids[j] = temp;
                        }
                    }
                }
                int result = ids.First<int>();
                return result;
            }
        }

        /// <summary>
        /// 获取时间差
        /// </summary>
        /// <param name="DateTime1">活动培训时间</param>
        /// <param name="DateTime2">当前时间</param>
        /// <returns></returns>
        private string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                dateDiff = ts.Days.ToString() + "天" +
                        ts.Hours.ToString() + "小时"
                        + ts.Minutes.ToString() + "分钟"
                        + ts.Seconds.ToString() + "秒";
            }
            catch
            {

            }
            return dateDiff;
        }

        private int DateDiff(DateTime data)
        {
            DateTime executeStart = DateTime.Now;
            System.TimeSpan executeSpan = data.Subtract(executeStart);
            string result = executeSpan.TotalSeconds.ToString().Substring(0, executeSpan.TotalSeconds.ToString().IndexOf('.'));
            return int.Parse(result);
        }



        /// <summary>
        /// 添加培训活动
        /// </summary>
        /// <param name="actityInfo">要添加的活动对象</param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.ActityInfo actityInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Actity(");
            strSql.Append("ActityTitle,ActityContent,ActityTime,Lecturer)");
            strSql.Append(" values (");
            strSql.Append("@ActityTitle,@ActityContent,@ActityTime,@Lecturer)");
            SqlParameter[] parameters = {					
					new SqlParameter("@ActityTitle", SqlDbType.NVarChar),
					new SqlParameter("@ActityContent", SqlDbType.NVarChar),
					new SqlParameter("@ActityTime", SqlDbType.DateTime),
					new SqlParameter("@Lecturer", SqlDbType.NVarChar)};
            parameters[0].Value = actityInfo.ActityTitle;
            parameters[1].Value = actityInfo.ActityContent;
            parameters[2].Value = actityInfo.ActityTime;
            parameters[3].Value = actityInfo.Lecturer;
            object obj = ESP.HumanResource.Common.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 根据id删除活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Actity ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int)
				};
            parameters[0].Value = id;
            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }

        /// <summary>
        /// 修改活动
        /// </summary>
        public int Update(ESP.HumanResource.Entity.ActityInfo actityInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Actity set ");
            strSql.Append("ActityTitle=@ActityTitle,");
            strSql.Append("ActityContent=@ActityContent,");
            strSql.Append("ActityTime=@ActityTime,");
            strSql.Append("Lecturer=@Lecturer");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@ActityTitle", SqlDbType.NVarChar),
					new SqlParameter("@ActityContent", SqlDbType.NVarChar),
					new SqlParameter("@ActityTime", SqlDbType.DateTime),
					new SqlParameter("@Lecturer", SqlDbType.NVarChar),
					new SqlParameter("@Id", SqlDbType.Int)};
            parameters[0].Value = actityInfo.ActityTitle;
            parameters[1].Value = actityInfo.ActityContent;
            parameters[2].Value = actityInfo.ActityTime;
            parameters[3].Value = actityInfo.Lecturer;
            parameters[4].Value = actityInfo.Id;

            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }

    }
}
