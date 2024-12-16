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
    internal class MaterialFinanceRelationDataProvider:ESP.Finance.IDataAccess.IMaterialFinanceRelationProvider
    {

        #region IMaterialFinanceRelationProvider 成员

        public int Add(ESP.Finance.Entity.MaterialFinanceRelationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_MaterialFinanceRelation(");
            strSql.Append("MaterialId,FinanceObjectName,MaterialType)");
            strSql.Append(" values (");
            strSql.Append("@MaterialId,@FinanceObjectName,@MaterialType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialId", SqlDbType.Int,4),
					new SqlParameter("@FinanceObjectName", SqlDbType.NVarChar,50),
					new SqlParameter("@MaterialType", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.MaterialId;
            parameters[1].Value = model.FinanceObjectName;
            parameters[2].Value = model.MaterialType;
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

        public int Update(ESP.Finance.Entity.MaterialFinanceRelationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_MaterialFinanceRelation set ");
            strSql.Append("MaterialId=@MaterialId,FinanceObjectName=@FinanceObjectName,MaterialType=@MaterialType");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialId", SqlDbType.NVarChar,50),
					new SqlParameter("@FinanceObjectName", SqlDbType.Int,4),
					new SqlParameter("@MaterialType", SqlDbType.NVarChar,50),
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.MaterialId;
            parameters[1].Value = model.FinanceObjectName;
            parameters[2].Value = model.MaterialType;
            parameters[3].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
        }

        public int Delete(int MaterialId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_MaterialFinanceRelation ");
            strSql.Append(" where MaterialId=@MaterialId ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialId", SqlDbType.Int,4)};
            parameters[0].Value = MaterialId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.MaterialFinanceRelationInfo GetModel(int MaterialId, int MaterialType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Id,MaterialId,FinanceObjectName,MaterialType from F_MaterialFinanceRelation ");
            strSql.Append(" where MaterialId=@MaterialId and MaterialType=@MaterialType");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialId", SqlDbType.Int,4),
                    new SqlParameter("@MaterialType", SqlDbType.Int,4),
                                        };
            parameters[0].Value = MaterialId;
            parameters[1].Value = MaterialType;

            return CBO.FillObject<ESP.Finance.Entity.MaterialFinanceRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.MaterialFinanceRelationInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,MaterialId,FinanceObjectName,MaterialType from F_MaterialFinanceRelation");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<MaterialFinanceRelationInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
    }
}
