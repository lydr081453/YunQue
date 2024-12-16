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
   public  class SalaryConfirmDataProvider
    {

        public int Add( SalaryConfirmInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_SalaryConfirm(");
            strSql.Append("UserId,SalaryType,ConfirmTime,IPAddress,SalaryYear,SalaryMonth)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@SalaryType,@ConfirmTime,@IPAddress,@SalaryYear,@SalaryMonth)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int),
					new SqlParameter("@SalaryType", SqlDbType.Int),
					new SqlParameter("@ConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@IPAddress", SqlDbType.NVarChar,50),
                    new SqlParameter("@SalaryYear", SqlDbType.Int),
                    new SqlParameter("@SalaryMonth", SqlDbType.Int)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.SalaryType;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.IPAddress;
            parameters[4].Value = model.SalaryYear;
            parameters[5].Value = model.SalaryMonth;
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

        public bool Exists(int userid,int year,int month )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_SalaryConfirm");
            strSql.Append(" where UserId=@UserId and SalaryYear=@SalaryYear and SalaryMonth=@SalaryMonth");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@SalaryYear", SqlDbType.Int,4),
                    new SqlParameter("@SalaryMonth", SqlDbType.Int,4)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool SalaryDataExists(int userid, int year,int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  count(1) from F_Salary ");
            strSql.Append(" where UserId=@UserId and SalaryYear=@SalaryYear and SalaryMonth=@SalaryMonth");

            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@SalaryYear", SqlDbType.Int,4),
                     new SqlParameter("@SalaryMonth", SqlDbType.Int,4)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool PaymentReportExists(int userid, DateTime dt1, DateTime dt2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_PaymentReportLog");
            strSql.Append(" where userid=@userid and (ReadTime between @dt1 and @dt2)");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@dt1", SqlDbType.DateTime,8),
                    new SqlParameter("@dt2", SqlDbType.DateTime,8)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = dt1;
            parameters[2].Value = dt2;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

    }
}
