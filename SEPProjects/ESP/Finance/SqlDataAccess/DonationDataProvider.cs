using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    public class DonationDataProvider : ESP.Finance.IDataAccess.IDonationProvider
    {

        #region IDonationProvider 成员

        public DonationInfo GetModel(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 DonationId,UserId,UserCode,UserName,DepartmentId,Department,BranchCode,Donation,CommitDate,IP from YuShu_Donation ");
            strSql.Append(" where UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)};
            parameters[0].Value = userId;

            return CBO.FillObject<DonationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<DonationInfo> GetList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DonationId,UserId,UserCode,UserName,DepartmentId,Department,BranchCode,Donation,CommitDate,IP from YuShu_Donation ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
           return CBO.FillCollection<DonationInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public int Add(DonationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YuShu_Donation(");
            strSql.Append("UserId,UserCode,UserName,DepartmentId,Department,BranchCode,Donation,CommitDate,IP)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@UserCode,@UserName,@DepartmentId,@Department,@BranchCode,@Donation,@CommitDate,@IP)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@Department", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Donation", SqlDbType.Decimal,20),
					new SqlParameter("@CommitDate", SqlDbType.DateTime,8),
					new SqlParameter("@IP", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.Department;
            parameters[5].Value = model.BranchCode;
            parameters[6].Value = model.Donation;
            parameters[7].Value = model.CommitDate;
            parameters[8].Value = model.IP;
         
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

        #endregion
    }
}
