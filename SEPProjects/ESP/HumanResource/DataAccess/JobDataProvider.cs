using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESP.HumanResource.DataAccess
{
    public class JobDataProvider
    {
        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.JobInfo> GetList(string sqlWhere)
        {
            List<JobInfo> list = new List<JobInfo>();
            string sql = "select * from SEP_JobInfo";
            if (!string.IsNullOrEmpty(sqlWhere))
                sql += sqlWhere;
            sql += " order by CreateTime desc";
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            JobInfo jobInfo = null;
            foreach (DataRow row in drs)
            {
                jobInfo = new JobInfo
                {
                    JobId = int.Parse(row["JobId"].ToString()),
                    JobName = row["JobName"].ToString(),
                    WorkingPlace = row["WorkingPlace"].ToString(),
                    Responsibilities = row["Responsibilities"].ToString(),
                    Requirements = row["Requirements"].ToString(),
                    SerToCustomer = row["SerToCustomer"].ToString(),
                    UrgentRecruitment = bool.Parse(row["UrgentRecruitment"].ToString()),
                    Creater = int.Parse(row["Creater"].ToString()),
                    Ordinal = int.Parse(row["Ordinal"].ToString()),
                    CreateTime = DateTime.Parse(row["CreateTime"].ToString()),
                    UpdateTime = DateTime.Parse(row["UpdateTime"].ToString())
                };
                list.Add(jobInfo);
            }
            return list;
        }

        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.JobInfo GetModel(int id)
        {
            string sql = "select * from SEP_JobInfo where JobId=" + id;
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            JobInfo jobInfo = null;
            foreach (DataRow row in drs)
            {
                jobInfo = new JobInfo
                {
                    JobId = int.Parse(row["JobId"].ToString()),
                    JobName = row["JobName"].ToString(),
                    WorkingPlace = row["WorkingPlace"].ToString(),
                    Responsibilities = row["Responsibilities"].ToString(),
                    Requirements = row["Requirements"].ToString(),
                    UrgentRecruitment = bool.Parse(row["UrgentRecruitment"].ToString()),
                    SerToCustomer = row["SerToCustomer"].ToString(),
                    Creater = int.Parse(row["Creater"].ToString()), 
                    Ordinal = int.Parse(row["Ordinal"].ToString()),
                    CreateTime = DateTime.Parse(row["CreateTime"].ToString()),
                    UpdateTime = DateTime.Parse(row["UpdateTime"].ToString())
                };
            }
            return jobInfo;
        }

        /// 添加培训活动
        /// </summary>
        /// <param name="actityInfo">要添加的活动对象</param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.JobInfo jobInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_JobInfo(");
            strSql.Append("JobName,WorkingPlace,Responsibilities,Requirements,SerToCustomer,UrgentRecruitment,Creater,Ordinal,CreateTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@JobName,@WorkingPlace,@Responsibilities,@Requirements,@SerToCustomer,@UrgentRecruitment,@Creater,@Ordinal,@CreateTime,@UpdateTime)");
            SqlParameter[] parameters = {					
					new SqlParameter("@JobName", SqlDbType.NVarChar),
					new SqlParameter("@WorkingPlace", SqlDbType.NVarChar),
					new SqlParameter("@Responsibilities", SqlDbType.NVarChar),
					new SqlParameter("@Requirements", SqlDbType.NVarChar),
                    new SqlParameter("@SerToCustomer",SqlDbType.NVarChar),
					new SqlParameter("@UrgentRecruitment", SqlDbType.Bit),
					new SqlParameter("@Creater", SqlDbType.Int),
					new SqlParameter("@Ordinal", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = jobInfo.JobName;
            parameters[1].Value = jobInfo.WorkingPlace;
            parameters[2].Value = jobInfo.Responsibilities;
            parameters[3].Value = jobInfo.Requirements;
            parameters[4].Value = jobInfo.SerToCustomer;
            parameters[5].Value = jobInfo.UrgentRecruitment;
            parameters[6].Value = jobInfo.Creater;
            parameters[7].Value = jobInfo.Ordinal;
            parameters[8].Value = jobInfo.CreateTime;
            parameters[9].Value = jobInfo.UpdateTime;
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
            strSql.Append("delete SEP_JobInfo ");
            strSql.Append(" where JobId=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int)
				};
            parameters[0].Value = id;
            int result = ESP.HumanResource.Common.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }

        
        /// <summary>
        /// 修改活动
        /// </summary>
        public int Update(ESP.HumanResource.Entity.JobInfo jobInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_JobInfo set ");
            strSql.Append("JobName=@JobName,");
            strSql.Append("WorkingPlace=@WorkingPlace,");
            strSql.Append("Responsibilities=@Responsibilities,");
            strSql.Append("Requirements=@Requirements,");
            strSql.Append("UrgentRecruitment=@UrgentRecruitment,");
            strSql.Append("Creater=@Creater,");
            strSql.Append("Ordinal=@Ordinal,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("SerToCustomer=@SerToCustomer");
            strSql.Append(" where JobId=@JobId");
            SqlParameter[] parameters = {					
					new SqlParameter("@JobName", SqlDbType.NVarChar),
					new SqlParameter("@WorkingPlace", SqlDbType.NVarChar),
					new SqlParameter("@Responsibilities", SqlDbType.NVarChar),
					new SqlParameter("@Requirements", SqlDbType.NVarChar),
					new SqlParameter("@UrgentRecruitment",SqlDbType.Bit),
					new SqlParameter("@Creater", SqlDbType.Int),
					new SqlParameter("@Ordinal", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@SerToCustomer", SqlDbType.NVarChar),
                    new SqlParameter("@JobId", SqlDbType.Int)};
            parameters[0].Value = jobInfo.JobName;
            parameters[1].Value = jobInfo.WorkingPlace;
            parameters[2].Value = jobInfo.Responsibilities;
            parameters[3].Value = jobInfo.Requirements;
            parameters[4].Value = jobInfo.UrgentRecruitment;
            parameters[5].Value = jobInfo.Creater;
            parameters[6].Value = jobInfo.Ordinal;
            parameters[7].Value = jobInfo.CreateTime;
            parameters[8].Value = jobInfo.UpdateTime;
            parameters[9].Value = jobInfo.SerToCustomer;
            parameters[10].Value = jobInfo.JobId;

            int result = ESP.HumanResource.Common.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }
    }
}
