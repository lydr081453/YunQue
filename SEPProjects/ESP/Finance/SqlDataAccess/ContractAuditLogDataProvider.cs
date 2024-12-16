using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    internal class ContractAuditLogDataProvider : ESP.Finance.IDataAccess.IContractAuditLogProvider
    {
        public int Add(ESP.Finance.Entity.ContractAuditLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ContractAuditLog(");
            strSql.Append("ContractorId,Contractor,ProjectId,AuditDesc,AuditDate)");
            strSql.Append(" values (");
            strSql.Append("@ContractorId,@Contractor,@ProjectId,@AuditDesc,@AuditDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractorId", SqlDbType.Int,4),
					new SqlParameter("@Contractor", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@AuditDesc", SqlDbType.NVarChar,500),
					new SqlParameter("@AuditDate", SqlDbType.DateTime,8)
                                        };
            parameters[0].Value = model.ContractorId;
            parameters[1].Value = model.Contractor;
            parameters[2].Value = model.ProjectId;
            parameters[3].Value = model.AuditDesc;
            parameters[4].Value = model.AuditDate;
   
            object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public IList<ESP.Finance.Entity.ContractAuditLogInfo> GetList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * ");
            strSql.Append(" FROM f_contractauditlog ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<Entity.ContractAuditLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
