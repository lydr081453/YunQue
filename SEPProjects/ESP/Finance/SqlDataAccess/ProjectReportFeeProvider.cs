using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
namespace ESP.Finance.DataAccess
{
    internal class ProjectReportFeeProvider : ESP.Finance.IDataAccess.IProjectReportFeeProvider
    {

        public int Add(ESP.Finance.Entity.ProjectReportFeeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ProjectReportFee(");
            strSql.Append("projectCode,year,month,fee,deptId,projectType)");
            strSql.Append(" values (");
            strSql.Append("@projectCode,@year,@month,@fee,@deptId,@projectType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@projectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@year", SqlDbType.Int),
					new SqlParameter("@month", SqlDbType.Int),
					new SqlParameter("@fee", SqlDbType.Decimal,20),
					new SqlParameter("@deptId", SqlDbType.Int),
                    new SqlParameter("@projectType", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.ProjectCode;
            parameters[1].Value = model.Year;
            parameters[2].Value = model.Month;
            parameters[3].Value = model.Fee;
            parameters[4].Value = model.DeptId;
            parameters[5].Value = model.ProjectType;


            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Delete(string projectcode, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ProjectReportFee ");
            strSql.Append(" where projectCode=@projectCode and year=@year and month=@month ");
            SqlParameter[] parameters = {
					new SqlParameter("@projectCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@year", SqlDbType.Int),
                    new SqlParameter("@month", SqlDbType.Int)
                                        };
            parameters[0].Value = projectcode;
            parameters[1].Value = year ;
            parameters[2].Value = month ;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int Delete(int groupid, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ProjectReportFee ");
            strSql.Append(" where deptId=@deptId and year=@year and month=@month ");
            SqlParameter[] parameters = {
					new SqlParameter("@deptId", SqlDbType.Int),
                    new SqlParameter("@year", SqlDbType.Int),
                    new SqlParameter("@month", SqlDbType.Int)
                                        };
            parameters[0].Value = groupid;
            parameters[1].Value = year;
            parameters[2].Value = month;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.ProjectReportFeeInfo GetModel(string projectcode, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_ProjectReportFee ");
            strSql.Append(" where projectCode=@projectCode and year=@year and month=@month ");

            SqlParameter[] parameters = {
					new SqlParameter("@projectCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@year", SqlDbType.Int),
                    new SqlParameter("@month", SqlDbType.Int)
                                        };
            parameters[0].Value = projectcode;
            parameters[1].Value = year;
            parameters[2].Value = month;

            return CBO.FillObject<ProjectReportFeeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        public IList<ESP.Finance.Entity.ProjectReportFeeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_ProjectReportFee ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ProjectReportFeeInfo>(DbHelperSQL.Query(strSql.ToString(), (param == null ? null : param.ToArray())));
	
        }
    }
}
