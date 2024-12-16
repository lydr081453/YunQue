using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
    internal class AuditLogDataProvider : ESP.Finance.IDataAccess.IAuditLogDataProvider
    {


        #region IAuditLogProvider 成员
        public int Add(ESP.Finance.Entity.AuditLogInfo model)
        {
            return Add(model, null);
        }

        public int Add(ESP.Finance.Entity.AuditLogInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_AuditLog(");
            strSql.Append(@"AuditorSysID,AuditorUserCode,AuditorUserName,AuditorEmployeeName,Suggestion,
                            FormID,FormType,AuditDate,AuditStatus)");

            strSql.Append(" values (");
            strSql.Append(@"@AuditorSysID,@AuditorUserCode,@AuditorUserName,@AuditorEmployeeName,@Suggestion,
                            @FormID,@FormType,@AuditDate,@AuditStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AuditorSysID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,4000),
					new SqlParameter("@FormID", SqlDbType.Int,4),
					new SqlParameter("@FormType", SqlDbType.Int,4),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.AuditorSysID;
            parameters[1].Value = model.AuditorUserCode;
            parameters[2].Value = model.AuditorUserName;
            parameters[3].Value = model.AuditorEmployeeName;
            parameters[4].Value = model.Suggestion;
            parameters[5].Value = model.FormID;
            parameters[6].Value = model.FormType;
            parameters[7].Value = model.AuditDate;
            parameters[8].Value = model.AuditStatus;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public ESP.Finance.Entity.AuditLogInfo GetModel(int logid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  * FROM F_AuditLog ");
            strSql.Append(" where AuditLogID=@AuditLogID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AuditLogID", SqlDbType.Int,4)};
            parameters[0].Value = logid;

            return CBO.FillObject<ESP.Finance.Entity.AuditLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.AuditLogInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_AuditLog ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where 1=1 " + term);
            }
            strSql.Append(" order by AuditDate asc");
            return CBO.FillCollection<ESP.Finance.Entity.AuditLogInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        private IList<ESP.Finance.Entity.AuditLogInfo> GetListByFormType(string term, List<SqlParameter> param)
        {
            string allTerm = " ";
            if (!string.IsNullOrEmpty(term))
            {
                allTerm += (term.TrimStart().StartsWith("and") || term.TrimStart().StartsWith("or")) ? term : " and " + term;
            }
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            return GetList(allTerm, param);
        }

        public IList<ESP.Finance.Entity.AuditLogInfo> GetProjectList(string term, List<SqlParameter> param)
        {
            return GetListByFormType( term, param);
        }

        public IList<ESP.Finance.Entity.AuditLogInfo> GetProjectList(int projectId)
        {
            return GetListByFormType("and FormID=" + projectId, null);
        }


        public IList<ESP.Finance.Entity.AuditLogInfo> GetProxyPNList(string term, List<SqlParameter> param)
        {
            return GetListByFormType( term, param);
        }


        public IList<ESP.Finance.Entity.AuditLogInfo> GetSupporterList(string term, List<SqlParameter> param)
        {
            return GetListByFormType( term, param);
        }

        public IList<ESP.Finance.Entity.AuditLogInfo> GetPaymentList(string term, List<SqlParameter> param)
        {
            return GetListByFormType( term, param);
        }

        public IList<ESP.Finance.Entity.AuditLogInfo> GetReturnList(string term, List<SqlParameter> param)
        {
            return GetListByFormType( term, param);
        }
        public IList<ESP.Finance.Entity.AuditLogInfo> GetBatchList(string term, List<SqlParameter> param)
        {
            return GetListByFormType(term, param);
        }
        #endregion
    }
}
