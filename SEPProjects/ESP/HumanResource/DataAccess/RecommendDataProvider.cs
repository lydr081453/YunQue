using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data;
using System.Data.SqlClient;
using ESP.HumanResource.Common;

namespace ESP.HumanResource.DataAccess
{
    public class RecommendDataProvider
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<RecommendInfo> GetList(string top)
        {
            IList<RecommendInfo> list = new List<RecommendInfo>();
            string sql = "select top 3 * from T_Recommend where 1=1 ";
            if (top != null)
            {
                sql += top;
            }
            sql += "order by CreateDate desc";
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            foreach (DataRow row in drs)
            {
                RecommendInfo item = new RecommendInfo();
                if (row["Id"].ToString() != "")
                    item.Id = int.Parse(row["Id"].ToString());
                item.UserName = row["UserName"].ToString();
                item.UserCode = row["UserCode"].ToString();
                item.RecommendName = row["RecommendName"].ToString();
                item.Tel = row["Tel"].ToString();
                item.Job = row["Job"].ToString();
                item.Detail = row["Detail"].ToString();
                item.FileName = row["FileName"].ToString();
                item.FileUrl = row["FileUrl"].ToString();
                item.Status = row["Status"].ToString();
                item.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                list.Add(item);
            }
            return list;
        }
        public IList<RecommendInfo> GetModels(string where)
        {
            IList<RecommendInfo> list = new List<RecommendInfo>();
            string sql = "select * from T_Recommend where 1=1 ";
            if (where != null && where != "")
            {
                sql += where;
            }
            sql += " order by CreateDate desc";
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            foreach (DataRow row in drs)
            {
                RecommendInfo item = new RecommendInfo();
                if (row["Id"].ToString() != "")
                    item.Id = int.Parse(row["Id"].ToString());
                item.UserName = row["UserName"].ToString();
                item.UserCode = row["UserCode"].ToString();
                item.RecommendName = row["RecommendName"].ToString();
                item.Tel = row["Tel"].ToString();
                item.Job = row["Job"].ToString();
                item.Detail = row["Detail"].ToString();
                item.FileName = row["FileName"].ToString();
                item.FileUrl = row["FileUrl"].ToString();
                item.Status = row["Status"].ToString();
                item.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RecommendInfo Get(int id)
        {
            string sql = "select * from T_Recommend where Id=" + id;
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            RecommendInfo recommend = new RecommendInfo();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                recommend.Id = id;
                recommend.UserName = row["UserName"].ToString();
                recommend.UserCode = row["UserCode"].ToString();
                recommend.RecommendName = row["RecommendName"].ToString();
                recommend.Job = row["Job"].ToString();
                recommend.Status = row["Status"].ToString();
                recommend.Tel = row["Tel"].ToString();
                recommend.Detail = row["Detail"].ToString();
                recommend.FileName = row["FileName"].ToString();
                recommend.FileUrl = row["FileUrl"].ToString();
                recommend.CreateDate = Convert.ToDateTime(row["CreateDate"].ToString());
            }
            return recommend;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="recommendInfo"></param>
        /// <returns></returns>
        public int Update(RecommendInfo recommendInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Recommend set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.NVarChar),
					new SqlParameter("@Id", SqlDbType.Int)};
            parameters[0].Value = recommendInfo.Status;
            parameters[1].Value = recommendInfo.Id;
            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }


        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int GetCount(string userCode)
        {
            IList<RecommendInfo> list = new List<RecommendInfo>();
            string sql = "select count(*) from T_Recommend where usercode=@usercode";
            SqlParameter param = new SqlParameter("@usercode", userCode);
            int num = (int)ESP.HumanResource.Common.DbHelperSQL.GetSingle(sql, param);
            return num;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="recommendInfo"></param>
        /// <returns></returns>
        public int Add(RecommendInfo recommendInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Recommend(");
            strSql.Append("UserName,UserCode,RecommendName,Tel,Job,Detail,FileName,FileUrl,Status,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@UserCode,@RecommendName,@Tel,@Job,@Detail,@FileName,@FileUrl,@Status,@CreateDate)");
            strSql.Append("SELECT @@IDENTITY");
            SqlParameter[] parameters = {					
					new SqlParameter("@UserName", recommendInfo.UserName),
					new SqlParameter("@UserCode", recommendInfo.UserCode),
                    new SqlParameter("@RecommendName", recommendInfo.RecommendName),
                    new SqlParameter("@Tel", recommendInfo.Tel),
                    new SqlParameter("@Job", recommendInfo.Job),
                    new SqlParameter("@Detail", recommendInfo.Detail),
                    new SqlParameter("@FileName", recommendInfo.FileName),
                    new SqlParameter("@FileUrl", recommendInfo.FileUrl),
                    new SqlParameter("@Status", recommendInfo.Status),
                    new SqlParameter("@CreateDate", recommendInfo.CreateDate)};
            object obj = ESP.HumanResource.Common.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            return 0;
        }
    }
}
