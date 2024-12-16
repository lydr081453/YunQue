using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Media.Entity;
using System.Data.SqlClient;
using System.Data;
using ESP.Media.Access.Utilities;

namespace ESP.Media.DataAccess
{
    public class ReporterEvaluationDataProvider
    {
        public void Insert(ReporterEvaluation model)
        {
            string sql = @"INSERT INTO media_ReporterEvaluation(ReporterId,UserId,UserName,CreateDate,Evaluation,Reason)
                            VALUES(@ReporterId,@UserId,@UserName,@CreateDate,@Evaluation,@Reason)";
            SqlParameter[] parms = {
                        new SqlParameter("@ReporterId",SqlDbType.Int),
                        new SqlParameter("@UserId",SqlDbType.Int),
                        new SqlParameter("@UserName",SqlDbType.NVarChar,30),
                        new SqlParameter("@CreateDate",SqlDbType.DateTime),
                        new SqlParameter("@Evaluation",SqlDbType.NText),
                        new SqlParameter("@Reason",SqlDbType.NVarChar,1000)
                                   };
            parms[0].Value = model.ReporterId;
            parms[1].Value = model.UserID;
            parms[2].Value = model.UserName;
            parms[3].Value = model.CreateDate;
            parms[4].Value = model.Evaluation;
            parms[5].Value = model.Reason;

            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                try
                {
                    SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, parms);
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }


        public ReporterEvaluation Get(int id)
        {
            string sql = "select * from media_ReporterEvaluation where id=" + id;
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                try
                {
                    return ESP.Finance.Utility.CBO.FillObject<ReporterEvaluation>(SqlHelper.ExecuteDataset(conn, CommandType.Text, sql));
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        public DataSet GetReporterEvaluation(int reporterId)
        {
            return GetReporterEvaluation(reporterId,"");
        }

        public DataSet GetReporterEvaluation(int reporterId,string userName)
        {
            string sql = "select * from media_ReporterEvaluation where reporterid=@reporterid {0} order by id desc";

            List<SqlParameter> parms = new List<SqlParameter>();
            SqlParameter parm = new SqlParameter("@reporterid", reporterId);
            parms.Add(parm);
            if (!string.IsNullOrEmpty(userName))
            {
                SqlParameter parm1 = new SqlParameter("@username", userName);
                sql = string.Format(sql, " and username like '%'+@username+'%'");
                parms.Add(parm1);
            }
            sql = string.Format(sql, "");
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                try
                {
                    return SqlHelper.ExecuteDataset(conn, CommandType.Text, sql, parms.ToArray());
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        public DataSet GetReporterEvaluation(string logIds)
        {
            string sql = "select * from media_ReporterEvaluation where id in( "+logIds+") order by id desc";
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                try
                {
                    return SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }
    }
}
