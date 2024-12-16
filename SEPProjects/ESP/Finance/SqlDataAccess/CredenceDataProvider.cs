using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
	/// <summary>
	/// 数据访问类CreditDAL。
	/// </summary>
    internal class CredenceDataProvider:ESP.Finance.IDataAccess.ICredeneProvider
    {
        #region ICredeneProvider 成员

        public int Add(CredenceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Credence(");
            strSql.Append("Code,UserId,UserName,Remark)");
            strSql.Append(" values (");
            strSql.Append("@Code,@UserId,@UserName,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.Code;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.Remark;
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

        public int Update(CredenceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Credence set ");
            strSql.Append("Code=@Code,UserId=@UserId,UserName=@UserName,Remark=@Remark");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.Code;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
        }

        public int Delete(int credenceId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Credence ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = credenceId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public CredenceInfo GetModel(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Code,UserId,UserName,Remark from F_Credence ");
            strSql.Append(" where UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)};
            parameters[0].Value = userid;

            return CBO.FillObject<CredenceInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public IList<CredenceInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Code,UserId,UserName,Remark from F_Credence");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<CredenceInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
    }
}
