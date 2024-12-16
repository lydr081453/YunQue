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
    internal class SupplierFinanceRelationDataProvider:ESP.Finance.IDataAccess.ISupplierFinanceRelationProvider
    {
        #region ISupplierFinanceRelationProvider 成员

        public int Add(ESP.Finance.Entity.SupplierFinanceRelationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_SupplierFinanceRelation(");
            strSql.Append("SupplierId,ShortName,FianceObjectName,Remark)");
            strSql.Append(" values (");
            strSql.Append("@SupplierId,@ShortName,@FianceObjectName,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ShortName", SqlDbType.NVarChar,50),
					new SqlParameter("@FianceObjectName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.ShortName;
            parameters[2].Value = model.FianceObjectName;
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

        public int Update(ESP.Finance.Entity.SupplierFinanceRelationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_SupplierFinanceRelation set ");
            strSql.Append("SupplierId=@SupplierId,ShortName=@ShortName,FianceObjectName=@FianceObjectName,Remark=@Remark");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ShortName", SqlDbType.NVarChar,50),
					new SqlParameter("@FianceObjectName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.ShortName;
            parameters[2].Value = model.FianceObjectName;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
        }

        public int Delete(int SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_SupplierFinanceRelation ");
            strSql.Append(" where SupplierId=@SupplierId ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = SupplierId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.SupplierFinanceRelationInfo GetModel(int supplierid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Id,SupplierId,ShortName,FianceObjectName,Remark from F_SupplierFinanceRelation ");
            strSql.Append(" where SupplierId=@SupplierId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4)};
            parameters[0].Value = supplierid;

            return CBO.FillObject<ESP.Finance.Entity.SupplierFinanceRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.SupplierFinanceRelationInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,SupplierId,ShortName,FianceObjectName,Remark from F_SupplierFinanceRelation");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<SupplierFinanceRelationInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
    }
}
