using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

namespace ESP.Finance.DataAccess
{
    internal class ToGroupDataProvider:ESP.Finance.IDataAccess.IToGroupProvider
    {
        #region IToGroupProvider 成员

        public int Add(ESP.Finance.Entity.ToGroupInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ToGroup(");
            strSql.Append("ToCode,ToName,Description)");
            strSql.Append(" values (");
            strSql.Append("@ToCode,@ToName,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ToCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ToName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Description", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.ToCode;
            parameters[1].Value = model.ToName;
            parameters[2].Value = model.Description;
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

        public int Update(ESP.Finance.Entity.ToGroupInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ToGroup set ");
            strSql.Append("ToCode=@ToCode,ToName=@ToName,Description=@Description,DepartmentId=@DepartmentId");
            strSql.Append(" where ToId=@ToId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ToCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ToName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@DepartmentId", SqlDbType.Int,4),
                    new SqlParameter("@ToId", SqlDbType.Int,4)
                    
                                        };

            parameters[0].Value = model.ToCode;
            parameters[1].Value = model.ToName;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.ToId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
        }

        public int Delete(int toId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ToGroup ");
            strSql.Append(" where ToId=@ToId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ToId", SqlDbType.Int,4)};
            parameters[0].Value = toId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.ToGroupInfo GetModel(int DepartmentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ToId,ToCode,ToName,Description,DepartmentId from F_ToGroup ");
            strSql.Append(" where DepartmentId=@DepartmentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentId", SqlDbType.Int,4)};
            parameters[0].Value = DepartmentId;

            return CBO.FillObject<ToGroupInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.ToGroupInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ToId,ToCode,ToName,Description,DepartmentId from F_ToGroup");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ToGroupInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
    }
}
