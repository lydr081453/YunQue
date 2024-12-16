using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Entity;
using ESP.Finance.Utility;


namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类CreditDAL。
    /// </summary>
    internal class CredenceTypeDataProvider : ESP.Finance.IDataAccess.ICredeneTypeProvider
    {
        #region ICredeneTypeProvider 成员

        public int Add(ESP.Finance.Entity.CredenceTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_CredenceType(");
            strSql.Append("Code,Name,Remark)");
            strSql.Append(" values (");
            strSql.Append("@Code,@Name,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.Code;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Remark;
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

        public int Update(ESP.Finance.Entity.CredenceTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_CredenceType set ");
            strSql.Append("Code=@Code,Name=@Name,Remark=@Remark");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.Code;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
        }

        public int Delete(int typeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_CredenceType ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = typeId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.CredenceTypeInfo GetModel(int typed)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Code,Name,Remark from F_CredenceType ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = typed;

            return CBO.FillObject<CredenceTypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.CredenceTypeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Code,Name,Remark from F_CredenceType");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<CredenceTypeInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
    }
}
