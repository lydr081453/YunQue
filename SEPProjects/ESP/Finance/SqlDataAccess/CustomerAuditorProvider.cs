using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class CustomerAuditorProvider : ESP.Finance.IDataAccess.ICustomerAuditorProvider
    {
       public int Add(ESP.Finance.Entity.CustomerAuditorInfo model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("insert into F_CustomerAuditor(");
           strSql.Append("CustomerId,CustomerCode,ProjectAuditor,BranchId,BranchCode)");
           strSql.Append(" values (");
           strSql.Append("@CustomerId,@CustomerCode,@ProjectAuditor,@BranchId,@BranchCode)");
           strSql.Append(";select @@IDENTITY");
           SqlParameter[] parameters = {
					new SqlParameter("@CustomerId", SqlDbType.Int,4),
					new SqlParameter("@CustomerCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectAuditor", SqlDbType.Int,4),
					new SqlParameter("@BranchId", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50)
		                                        };
           parameters[0].Value = model.CustomerId;
           parameters[1].Value = model.CustomerCode;
           parameters[2].Value = model.ProjectAuditor;
           parameters[3].Value = model.BranchId;
           parameters[4].Value = model.BranchCode;
         
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
       public int Update(ESP.Finance.Entity.CustomerAuditorInfo model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("update F_CustomerAuditor(");
           strSql.Append("set CustomerId=@CustomerId,CustomerCode=@CustomerCode,ProjectAuditor=@ProjectAuditor,BranchId=@BranchId,BranchCode=@BranchCode)");
           strSql.Append(" where id=@id");
           SqlParameter[] parameters = {
					new SqlParameter("@CustomerId", SqlDbType.Int,4),
					new SqlParameter("@CustomerCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectAuditor", SqlDbType.Int,4),
					new SqlParameter("@BranchId", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@id", SqlDbType.Int,4)
		                                        };
           parameters[0].Value = model.CustomerId;
           parameters[1].Value = model.CustomerCode;
           parameters[2].Value = model.ProjectAuditor;
           parameters[3].Value = model.BranchId;
           parameters[4].Value = model.BranchCode;
           parameters[5].Value = model.Id;
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
       public int Delete(int id)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("delete F_CustomerAuditor ");
           strSql.Append(" where id=@id ");
           SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
           parameters[0].Value = id;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
       }
       public ESP.Finance.Entity.CustomerAuditorInfo GetModel(int id)
       {

           StringBuilder strSql = new StringBuilder();
           strSql.Append(@"select  top 1 * from F_CustomerAuditor ");
           strSql.Append(" where id=@id ");
           SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
           parameters[0].Value = id;

           return CBO.FillObject<CustomerAuditorInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
       }
       public IList<ESP.Finance.Entity.CustomerAuditorInfo> GetList(string term)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(@"select * ");
           strSql.Append(" FROM F_CustomerAuditor ");
           if (!string.IsNullOrEmpty(term))
           {
               strSql.Append(" where " + term);
           }
           return CBO.FillCollection<CustomerAuditorInfo>(DbHelperSQL.Query(strSql.ToString()));
       }
    }
}
